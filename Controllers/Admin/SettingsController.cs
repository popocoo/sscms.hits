using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SSCMS;
using SSCMS.Dto;
using SSCMS.Repositories;
using SSCMS.Services;
using SSCMS.Utils;

namespace Hits.Controllers.Admin
{
    [Authorize(Roles = AuthTypes.Roles.Administrator)]
    [Route(Constants.ApiAdminPrefix)]
    public partial class SettingsController : ControllerBase
    {
        private const string Route = "hits/settings/{siteId:int}";

        private readonly IAuthManager _authManager;
        private readonly IPluginConfigRepository _pluginConfigRepository;

        public SettingsController(IAuthManager authManager, IPluginConfigRepository pluginConfigRepository)
        {
            _authManager = authManager;
            _pluginConfigRepository = pluginConfigRepository;
        }

        [HttpGet, Route(Route)]
        public async Task<ActionResult<Config>> GetConfig(int siteId)
        {
            if (!await _authManager.HasSitePermissionsAsync(siteId, "hits"))
            {
                return Unauthorized();
            }

            var configInfo = await _pluginConfigRepository.GetConfigAsync<Config>(Utils.PluginId, siteId) ?? new Config();

            return configInfo;
        }

        [HttpPost, Route(Route)]
        public async Task<ActionResult<BoolResult>> Submit([FromRoute]int siteId, [FromBody]SubmitRequest request)
        {
            if (!await _authManager.HasSitePermissionsAsync(siteId, "hits"))
            {
                return Unauthorized();
            }

            var configInfo = await _pluginConfigRepository.GetConfigAsync<Config>(Utils.PluginId, siteId) ?? new Config();

            configInfo.IsHitsDisabled = request.IsHitsDisabled;
            configInfo.IsHitsCountByDay = request.IsHitsCountByDay;

            await _pluginConfigRepository.SetConfigAsync(Utils.PluginId, siteId, configInfo);

            return new BoolResult
            {
                Value = true
            };
        }
    }
}
