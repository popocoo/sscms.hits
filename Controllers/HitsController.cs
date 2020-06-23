using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SSCMS.Repositories;

namespace SSCMS.Hits.Controllers
{
    [Route("api/hits")]
    public class HitsController : ControllerBase
    {
        private readonly IPluginConfigRepository _pluginConfigRepository;
        private readonly IContentRepository _contentRepository;

        public HitsController(IPluginConfigRepository pluginConfigRepository, IContentRepository contentRepository)
        {
            _pluginConfigRepository = pluginConfigRepository;
            _contentRepository = contentRepository;
        }

        [HttpGet, Route("{siteId:int}/{channelId:int}/{contentId:int}")]
        public async Task Hits(int siteId, int channelId, int contentId)
        {
            var configInfo = await _pluginConfigRepository.GetConfigAsync<Config>(Utils.PluginId, siteId) ?? new Config();

            //var tableName = Context.ContentApi.GetTableName(siteId, channelId);

            await AddContentHitsAsync(siteId, channelId, contentId, configInfo);
        }

        private async Task AddContentHitsAsync(int siteId, int channelId, int contentId, Config config)
        {
            if (siteId <= 0 || channelId <= 0 || contentId <= 0 || config.IsHitsDisabled) return;
            var contentInfo = await _contentRepository.GetAsync(siteId, channelId, contentId);
            if (contentInfo == null) return;

            if (config.IsHitsCountByDay)
            {
                var now = DateTime.Now;

                if (contentInfo.LastHitsDate != null)
                {
                    var lastHitsDate = contentInfo.LastHitsDate.Value;

                    contentInfo.HitsByDay = now.Day != lastHitsDate.Day || now.Month != lastHitsDate.Month || now.Year != lastHitsDate.Year ? 1 : contentInfo.HitsByDay + 1;
                    contentInfo.HitsByWeek = now.Month != lastHitsDate.Month || now.Year != lastHitsDate.Year || now.DayOfYear / 7 != lastHitsDate.DayOfYear / 7 ? 1 : contentInfo.HitsByWeek + 1;
                    contentInfo.HitsByMonth = now.Month != lastHitsDate.Month || now.Year != lastHitsDate.Year ? 1 : contentInfo.HitsByMonth + 1;
                }
                else
                {
                    contentInfo.HitsByDay = contentInfo.HitsByWeek = contentInfo.HitsByMonth = 1;
                }

                contentInfo.Hits += 1;

                contentInfo.LastHitsDate = now;

                await _contentRepository.UpdateAsync(contentInfo);
            }
            else
            {
                contentInfo.Hits += 1;

                contentInfo.LastHitsDate = DateTime.Now;

                await _contentRepository.UpdateAsync(contentInfo);
            }
        }
    }
}