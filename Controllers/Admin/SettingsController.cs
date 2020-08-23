using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SSCMS.Configuration;
using SSCMS.Dto;
using SSCMS.Hits.Abstractions;
using SSCMS.Hits.Models;
using SSCMS.Services;
using SSCMS.Utils;

namespace SSCMS.Hits.Controllers.Admin
{
    [Authorize(Roles = Types.Roles.Administrator)]
    [Route(Constants.ApiAdminPrefix)]
    public partial class SettingsController : ControllerBase
    {
        private const string Route = "hits/settings/{siteId:int}";

        private readonly IAuthManager _authManager;
        private readonly IHitsManager _hitsManager;

        public SettingsController(IAuthManager authManager, IHitsManager hitsManager)
        {
            _authManager = authManager;
            _hitsManager = hitsManager;
        }

        [HttpGet, Route(Route)]
        public async Task<ActionResult<Settings>> GetConfig(int siteId)
        {
            if (!await _authManager.HasSitePermissionsAsync(siteId, "hits"))
            {
                return Unauthorized();
            }

            var settings = await _hitsManager.GetSettingsAsync(siteId);

            return settings;
        }

        [HttpPost, Route(Route)]
        public async Task<ActionResult<BoolResult>> Submit([FromRoute]int siteId, [FromBody]SubmitRequest request)
        {
            if (!await _authManager.HasSitePermissionsAsync(siteId, "hits"))
            {
                return Unauthorized();
            }

            var settings = await _hitsManager.GetSettingsAsync(siteId);

            settings.IsHitsDisabled = request.IsHitsDisabled;
            settings.IsHitsCountByDay = request.IsHitsCountByDay;

            await _hitsManager.SetSettingsAsync(siteId, settings);

            return new BoolResult
            {
                Value = true
            };
        }
    }
}
