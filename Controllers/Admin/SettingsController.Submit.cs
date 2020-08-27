using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SSCMS.Dto;

namespace SSCMS.Hits.Controllers.Admin
{
    public partial class SettingsController
    {
        [HttpPost, Route(Route)]
        public async Task<ActionResult<BoolResult>> Submit([FromBody] SubmitRequest request)
        {
            if (!await _authManager.HasSitePermissionsAsync(request.SiteId, "site_hits"))
            {
                return Unauthorized();
            }

            var settings = await _hitsManager.GetSettingsAsync(request.SiteId);

            settings.IsHitsDisabled = request.IsHitsDisabled;
            settings.IsHitsCountByDay = request.IsHitsCountByDay;

            await _hitsManager.SetSettingsAsync(request.SiteId, settings);

            return new BoolResult
            {
                Value = true
            };
        }
    }
}
