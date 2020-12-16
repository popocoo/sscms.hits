using System.Threading.Tasks;
using SSCMS.Hits.Abstractions;
using SSCMS.Hits.Models;
using SSCMS.Repositories;

namespace SSCMS.Hits.Core
{
    public class HitsManager : IHitsManager
    {
        public const string PluginId = "sscms.hits";

        private readonly IPluginConfigRepository _pluginConfigRepository;

        public HitsManager(IPluginConfigRepository pluginConfigRepository)
        {
            _pluginConfigRepository = pluginConfigRepository;
        }

        public async Task<Settings> GetSettingsAsync(int siteId)
        {
            var settings = new Settings
            {
                IsHitsDisabled =
                    await _pluginConfigRepository.GetAsync<bool>(PluginId, siteId,
                        nameof(Settings.IsHitsDisabled)),
                IsHitsCountByDay =
                    await _pluginConfigRepository.GetAsync<bool>(PluginId, siteId,
                        nameof(Settings.IsHitsCountByDay))
            };
            return settings;
        }

        public async Task SetSettingsAsync(int siteId, Settings settings)
        {
            await _pluginConfigRepository.SetAsync(PluginId, siteId, nameof(Settings.IsHitsDisabled), settings.IsHitsDisabled);
            await _pluginConfigRepository.SetAsync(PluginId, siteId, nameof(Settings.IsHitsCountByDay), settings.IsHitsCountByDay);
        }
    }
}
