using System.Collections.Generic;
using SSCMS.Context;
using SSCMS.Enums;
using SSCMS.Hits.Abstractions;
using SSCMS.Plugins;

namespace SSCMS.Hits.Core
{
    public class CreateStart : IPluginCreateStart
    {
        private readonly IHitsManager _hitsManager;

        public CreateStart(IHitsManager hitsManager)
        {
            _hitsManager = hitsManager;
        }

        public void Parse(IParseContext context)
        {
            if (context.TemplateType != TemplateType.ContentTemplate || context.ContentId <= 0) return;

            var apiUrl = $"/api/hits/{context.SiteId}/{context.ChannelId}/{context.ContentId}";
            context.FootCodes.TryAdd(_hitsManager.PluginId, $@"<script src=""{apiUrl}"" type=""text/javascript""></script>");
        }
    }
}
