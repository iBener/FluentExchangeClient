using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentExchangeClient
{
    /// <summary>
    /// Borsa istek limit kontrolorü.
    /// </summary>
    public class RequestLimiter
    {
        private readonly int _limit;
        private readonly int recWindow = 5;

        private readonly Dictionary<int, int> _sliceWeights;

        public RequestLimiter(int limit)
        {
            _limit = limit;
            _sliceWeights = new Dictionary<int, int>();

            // Varsayılan zaman dilimlerini oluştur
            for (int i = 0; i < 60 / recWindow + 1; i++)
            {
                int slice = i * recWindow;
                if (slice < 60)
                {
                    // Başlangıç zaman dilimi değeri
                    _sliceWeights[i] = 0;
                }
            }
        }

        /// <summary>
        /// Borsa'nın istek limitinin aşılıp aşılmadığını kontrol eder. Aşılıyorsa yeni istek yapılabilecek zamana kadar bekletir.
        /// </summary>
        public async Task CheckRequestLimit(int weight)
        {
            // Şuanki zaman dilimi
            int slice = DateTime.Now
                .AddSeconds(recWindow - DateTime.Now.Second % recWindow)
                .Second;

            // Gerçekleşecek olan toplam istek ağırlığı
            int totalTo = _sliceWeights.Sum(x => x.Value) + weight;

            // Gerçekleşecek olan toplam istek ağırlığı limitten büyükse
            while (totalTo >= _limit)
            {
                Debug.WriteLine("ExchangeClient waiting next receive window to send request. Slice: {0}", slice);
                // Şuanki dilimi değerini sıfırla
                _sliceWeights[slice] = 0;
                // Sonraki zaman dilimine kadar bekle
                await Task.Delay(recWindow);
                // Yeni zaman dilimi
                slice = DateTime.Now
                    .AddSeconds(recWindow - DateTime.Now.Second % recWindow)
                    .Second;
                // Gerçekleşecek olan toplam istek ağırlığını yeniden hesapla
                totalTo = _sliceWeights.Sum(x => x.Value) + weight;
            }

            // Zaman dilimine yeni istek ağırlığını ekle
            _sliceWeights[slice] += weight;
        }
    }
}
