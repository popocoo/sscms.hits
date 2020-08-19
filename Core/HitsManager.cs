using System.Threading.Tasks;
using SSCMS.Hits.Abstractions;
using SSCMS.Hits.Models;
using SSCMS.Repositories;
using SSCMS.Services;

namespace SSCMS.Hits.Core
{
    public class HitsManager : IHitsManager
    {
        private readonly IPlugin _plugin;
        private readonly IPluginConfigRepository _pluginConfigRepository;

        public HitsManager(IPluginManager pluginManager, IPluginConfigRepository pluginConfigRepository)
        {
            _pluginConfigRepository = pluginConfigRepository;
            _plugin = pluginManager.Current;
        }

        public string PluginId => _plugin.PluginId;

        public async Task<Settings> GetSettingsAsync(int siteId)
        {
            var pluginId = _plugin.PluginId;
            return await _pluginConfigRepository.GetConfigAsync<Settings>(pluginId, siteId) ?? new Settings();
        }

        public async Task<bool> SetSettingsAsync(int siteId, Settings settings)
        {
            var pluginId = _plugin.PluginId;
            return await _pluginConfigRepository.SetConfigAsync(pluginId, siteId, settings);
        }
    }
}
