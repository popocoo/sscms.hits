using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SSCMS.Configuration;
using SSCMS.Dto;
using SSCMS.Hits.Abstractions;
using SSCMS.Services;

namespace SSCMS.Hits.Controllers.Admin
{
    [Authorize(Roles = Types.Roles.Administrator)]
    [Route(Constants.ApiAdminPrefix)]
    public partial class SettingsController : ControllerBase
    {
        private const string Route = "hits/settings";

        private readonly IAuthManager _authManager;
        private readonly IHitsManager _hitsManager;

        public SettingsController(IAuthManager authManager, IHitsManager hitsManager)
        {
            _authManager = authManager;
            _hitsManager = hitsManager;
        }

        public class SubmitRequest : SiteRequest
        {
            public bool IsHitsDisabled { get; set; }
            public bool IsHitsCountByDay { get; set; }
        }
    }
}
