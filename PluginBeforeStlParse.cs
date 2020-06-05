using System.Collections.Generic;
using SSCMS;
using SSCMS.Enums;
using SSCMS.Plugins;

namespace Hits
{
    public class PluginBeforeStlParse : IPluginBeforeStlParse
    {
        public PluginBeforeStlParse()
        {

        }

        public void BeforeStlParse(IStlParseContext context)
        {
            if (context.TemplateType != TemplateType.ContentTemplate || context.ContentId <= 0) return;

            var apiUrl = $"/api/hits/{context.SiteId}/{context.ChannelId}/{context.ContentId}";
            context.FootCodes.TryAdd(Utils.PluginId, $@"<script src=""{apiUrl}"" type=""text/javascript""></script>");
        }
    }
}
