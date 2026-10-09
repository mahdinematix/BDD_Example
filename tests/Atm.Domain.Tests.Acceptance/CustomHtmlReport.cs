using TestStack.BDDfy.Reporters.Html;

namespace Atm.Domain.Tests.Acceptance
{
    public class CustomHtmlReport : DefaultHtmlReportConfiguration
    {
        public override string ReportHeader => "Atm Features";
        public override string ReportDescription => "This repost contains all scenarios for Atm features";
        public override string OutputFileName => "Atm.html";
    }
}
