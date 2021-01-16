using AutoMapper;
using DhubSolutions.Reports.Domain.Entities.ReportManager;
using System;

namespace DhubSolutions.Reports.Domain.Services.Adapters
{
    class ReportCreationProfile : Profile
    {
        public ReportCreationProfile()
        {
            CreateMap<ReportTemplate, Report>()
                //Ignored Properties, because are auto calculated or not relevant for the report
                .ForMember(r => r.Data, opts => opts.Ignore()) //Auto calculated
                .ForMember(r => r.LastModified, opts => opts.Ignore())  //Auto calculated
                .ForMember(r => r.CreatedById, opts => opts.Ignore())   //Auto calculated
                .ForMember(r => r.CreatedBy, opts => opts.Ignore())     //Auto calculated
                .ForMember(r => r.CreationDate, opts => opts.Ignore())  //Auto calculated
                                                                        //.ForMember(r => r.ReportTemplate, opts => opts.Ignore())

                .ForMember(r => r.TemplateId, opts => opts.MapFrom(rt => rt.Id))
                //.ForMember(r => r.ReportPermissions, opt => opt.MapFrom(rt => rt.ReportTemplatePermissions))

                .AfterMap((template, report) =>
                {
                    //After finishing mapping we need to set some properties that must be calculated
                    report.Id = Guid.NewGuid().ToString();
                    report.CreationDate = DateTime.Now;
                    report.LastModified = DateTime.Now;
                    report.Template = template;
                    report.TemplateId = template.Id;
                    report.UpdateUpReferences();
                    //report.IndexReport();
                });

            CreateMap<ReportTemplateElement, ReportElement>()
                .ForMember(re => re.Id, opts => opts.Ignore())                  //Auto calculated
                .ForMember(re => re.CreationDate, opts => opts.Ignore())        //Auto calculated
                .ForMember(re => re.LastModified, opts => opts.Ignore())        //Auto calculated
                .ForMember(re => re.CreatedBy, opts => opts.Ignore())           //Auto calculated
                .ForMember(re => re.CreatedById, opts => opts.Ignore())         //Auto calculated
                .ForMember(re => re.LastModifiedBy, opts => opts.Ignore())      //Auto calculated
                .ForMember(re => re.LastModifiedById, opts => opts.Ignore())    //Auto calculated
            //.ForMember(r => r.ReportElementPermissions, opt => opt.MapFrom(rt => rt.ReportTemplateElementPermissions))


                .AfterMap((reportTemplateElement, ReportElement) =>
                {
                    //After finishing mapping we need to set some properties that must be calculated
                    ReportElement.Id = Guid.NewGuid().ToString();
                    ReportElement.CreationDate = DateTime.Now;
                    ReportElement.LastModified = DateTime.Now;
                    ReportElement.ReportTemplateElement = reportTemplateElement;
                    ReportElement.ReportTemplateElementId = reportTemplateElement.Id;
                });
        }
    }


}
