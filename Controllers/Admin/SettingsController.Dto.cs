namespace SSCMS.Hits.Controllers.Admin
{
    public partial class SettingsController
    {
        public class SubmitRequest
        {
            public bool IsHitsDisabled { get; set; }
            public bool IsHitsCountByDay { get; set; }
        }
    }
}
