using Microsoft.Extensions.DependencyInjection;
using SSCMS.Hits.Abstractions;
using SSCMS.Hits.Core;
using SSCMS.Plugins;

namespace SSCMS.Hits
{
    public class Startup : IPluginConfigureServices
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IHitsManager, HitsManager>();
        }
    }
}
