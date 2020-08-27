using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SSCMS.Dto;
using SSCMS.Hits.Models;

namespace SSCMS.Hits.Controllers.Admin
{
    public partial class SettingsController
    {
        [HttpGet, Route(Route)]
        public async Task<ActionResult<Settings>> Get([FromQuery] SiteRequest request)
        {
            if (!await _authManager.HasSitePermissionsAsync(request.SiteId, "site_hits"))
            {
                return Unauthorized();
            }

            var settings = await _hitsManager.GetSettingsAsync(request.SiteId);

            return settings;
        }
    }
}
