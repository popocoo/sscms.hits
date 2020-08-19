using System.Threading.Tasks;
using SSCMS.Hits.Models;

namespace SSCMS.Hits.Abstractions
{
    public interface IHitsManager
    {
        string PluginId { get; }
        Task<Settings> GetSettingsAsync(int siteId);

        Task<bool> SetSettingsAsync(int siteId, Settings settings);
    }
}
