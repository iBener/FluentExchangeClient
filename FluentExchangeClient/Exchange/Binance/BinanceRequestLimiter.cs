using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FluentExchangeClient.Exchange.Binance;

/// <summary>
/// Borsa istek limit kontrolorü.
/// </summary>
class BinanceRequestLimiter : IRequestLimiter
{
    private readonly int _limit;

    private static readonly SemaphoreSlim semaphore;
    private static readonly Dictionary<long, int> _sliceWeights;

    public int UsedLimit => _sliceWeights.Sum(x => x.Value);

    static BinanceRequestLimiter()
    {
        semaphore = new SemaphoreSlim(1, 1);
        _sliceWeights = new Dictionary<long, int>();
    }

    public BinanceRequestLimiter(int limit)
    {
        _limit = limit;
    }

    /// <summary>
    /// Borsa'nın istek limitinin aşılıp aşılmadığını kontrol eder. Aşılıyorsa yeni istek yapılabilecek zamana kadar bekletir.
    /// </summary>
    public async Task WaitRequestLimit(int weight)
    {
        // Thread race durumunu engelle
        await semaphore.WaitAsync();

        // Şimdiki zaman dilimi
        long now = DateTimeOffset.Now.ToUnixTimeSeconds();

        // Süresi geçmiş istek ağırlık değerlerini sil
        ClearExpiredUsings(now);

        // Gerçekleşecek olan toplam istek ağırlığı
        int totalTo = UsedLimit + weight;

        try
        {
            // Gerçekleşecek olan toplam istek ağırlığı limitten büyükse
            while (totalTo > _limit)
            {
                // Süresi geçmiş değerleri temizle
                int cleared = ClearExpiredUsings(now);
                Debug.WriteLine("Request limit reached. Time: {0}, Removed: {1} Used: {2}", Truncate(DateTime.Now, TimeSpan.FromMilliseconds(1)), cleared, UsedLimit);
                // Sonraki zaman dilimine kadar bekle
                await Task.Delay(1000);
                // Yeni zaman dilimi
                now = DateTimeOffset.Now.ToUnixTimeSeconds();
                // Gerçekleşecek olan toplam istek ağırlığını yeniden hesapla
                totalTo = UsedLimit + weight;
            }

            // Zaman dilimine yeni istek ağırlığını ekle
            if (_sliceWeights.ContainsKey(now))
            {
                _sliceWeights[now] += weight;
            }
            else
            {
                _sliceWeights[now] = weight;
            }
        }
        finally
        {
            semaphore.Release();
        }

        static DateTime Truncate(DateTime dateTime, TimeSpan timeSpan)
        {
            if (timeSpan == TimeSpan.Zero) return dateTime; // Or could throw an ArgumentException
            if (dateTime == DateTime.MinValue || dateTime == DateTime.MaxValue) return dateTime; // do not modify "guard" values
            return dateTime.AddTicks(-(dateTime.Ticks % timeSpan.Ticks));
        }
    }

    private static int ClearExpiredUsings(long now)
    {
        int cleared = 0;
        foreach (var slice in _sliceWeights.Where(x => x.Key < now - 60))
        {
            cleared += slice.Value;
            _sliceWeights.Remove(slice.Key);
        }
        return cleared;
    }

    /// <summary>
    /// 5 saniyelik zaman dilimini döner
    /// </summary>
    private static (DateTime Start, DateTime End) GetSlice(DateTime time) => (
        time.AddSeconds(-1 * time.Second % 5),
        time.AddSeconds(5 - time.Second % 5
    ));
}
