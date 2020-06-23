using Microsoft.AspNetCore.Mvc;

namespace SSCMS.Hits.Controllers
{
    [Route("api/hits/ping")]
    public class PingController : ControllerBase
    {
        private const string Route = "";

        [HttpGet, Route(Route)]
        public string Get()
        {
            return "pong";
        }
    }
}
