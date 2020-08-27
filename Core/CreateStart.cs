using System.Collections.Generic;
using SSCMS.Enums;
using SSCMS.Parse;
using SSCMS.Plugins;

namespace SSCMS.Hits.Core
{
    public class CreateStart : IPluginCreateStart
    {
        public void Parse(IParseContext context)
        {
            if (context.TemplateType != TemplateType.ContentTemplate || context.ContentId <= 0) return;

            var apiUrl = $"/api/hits/{context.SiteId}/{context.ChannelId}/{context.ContentId}";
            context.FootCodes.TryAdd(HitsManager.PluginId, $@"<script src=""{apiUrl}"" type=""text/javascript""></script>");
        }
    }
}
