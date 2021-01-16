using AutoMapper;
using DhubSolutions.Reports.Application.ViewModels.ReportManager;
using DhubSolutions.Reports.Domain.Entities.ReportManager.ObjectValues;

namespace DhubSolutions.Reports.Application.Adapters
{
    public class ReportTemplateActivePeriodProfile : Profile
    {
        public ReportTemplateActivePeriodProfile()
        {
            CreateMap<ReportTemplateActivePeriodObjectValue, ReportTemplateActivePeriodVM>();

            CreateMap<PeriodStatusObjectValue, PeriodStatusVM>();

            CreateMap<CreationPeriodObjectValue, CreationPeriodVM>();
        }
    }
}
