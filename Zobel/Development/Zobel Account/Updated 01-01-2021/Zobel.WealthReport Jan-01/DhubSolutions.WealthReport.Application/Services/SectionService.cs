using DhubSolutions.Core.Domain.Adapters;
using DhubSolutions.WealthReport.Application.Services.Base;
using DhubSolutions.WealthReport.Domain.Entities;
using DhubSolutions.WealthReport.Domain.Repositories;

namespace DhubSolutions.WealthReport.Application.Services
{
    public class SectionService : WealthReportService<Section>, ISectionService
    {
        public SectionService(ITypeAdapter typeAdapter, IWealthReportRepository<Section> reportRepository)
           : base(typeAdapter, reportRepository)
        {
        }
    }
}
