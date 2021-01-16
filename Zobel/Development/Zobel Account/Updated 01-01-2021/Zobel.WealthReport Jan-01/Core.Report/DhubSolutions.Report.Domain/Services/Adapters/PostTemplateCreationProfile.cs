using AutoMapper;
using DhubSolutions.Reports.Domain.Entities.ReportManager;
using Newtonsoft.Json.Linq;

namespace DhubSolutions.Reports.Domain.Services.Adapters
{
    public class PostTemplateCreationProfile : Profile
    {
        public PostTemplateCreationProfile()
        {
            CreateMap<ReportTemplate, ReportTemplate>()
                .ForMember(dest => dest.Data, opt => opt.Ignore())

                .AfterMap((src, dest) =>
                {
                    JObject dataObject = new JObject();

                    foreach (JProperty jProperty in src.DataJObject.Properties())
                    {
                        string template = $"{ src.DataJObject[jProperty.Name]["template"]}";
                        if (string.IsNullOrEmpty(template) || string.IsNullOrWhiteSpace(template))
                        {
                            dataObject[jProperty.Name] = src.DataJObject[jProperty.Name];
                        }
                    }

                    dest.Data = $"{ dataObject}";
                });

            CreateMap<ReportTemplateElement, ReportTemplateElement>();
        }




    }
}
