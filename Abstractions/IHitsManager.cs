using System.Threading.Tasks;
using SSCMS.Hits.Models;

namespace SSCMS.Hits.Abstractions
{
    public interface IHitsManager
    {
        Task<Settings> GetSettingsAsync(int siteId);

        Task SetSettingsAsync(int siteId, Settings settings);
    }
}
