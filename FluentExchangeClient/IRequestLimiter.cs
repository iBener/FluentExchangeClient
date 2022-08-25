using System.Threading.Tasks;

namespace FluentExchangeClient
{
    public interface IRequestLimiter
    {
        Task WaitRequestLimit(int weight);
    }
}