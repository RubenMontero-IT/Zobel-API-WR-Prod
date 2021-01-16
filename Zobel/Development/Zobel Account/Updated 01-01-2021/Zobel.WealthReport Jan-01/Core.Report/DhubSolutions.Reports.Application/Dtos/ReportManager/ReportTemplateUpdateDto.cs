namespace DhubSolutions.Reports.Application.Dtos.ReportManager
{
    public class ReportTemplateUpdateDto
    {
        public string TemplateId { get; set; }

        public string UserId { get; set; }

        public ReportTemplateDto ReportTemplate { get; set; }
    }

}
