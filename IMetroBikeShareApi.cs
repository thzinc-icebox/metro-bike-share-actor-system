using System.Threading.Tasks;
using RestEase;

namespace bikeshare
{
    public interface IMetroBikeShareApi
    {
        [Get("stations/json")]
        Task<StationsResponse> GetStationsAsync();
    }
}
