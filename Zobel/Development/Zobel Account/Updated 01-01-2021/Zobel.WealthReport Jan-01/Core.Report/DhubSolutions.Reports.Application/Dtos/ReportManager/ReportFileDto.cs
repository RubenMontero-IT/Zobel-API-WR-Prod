using System;

namespace DhubSolutions.Reports.Application.Dtos.ReportManager
{
    public class ReportFileDto
    {
        public string Id { get; set; }
        public string FileName { get; set; }
        public DateTime CreationDate { get; set; }
        public string Creator { get; set; }
        public string Period { get; set; }
        public string OrganizationId { get; set; }
        public string OrganizationName { get; set; }
    }
}