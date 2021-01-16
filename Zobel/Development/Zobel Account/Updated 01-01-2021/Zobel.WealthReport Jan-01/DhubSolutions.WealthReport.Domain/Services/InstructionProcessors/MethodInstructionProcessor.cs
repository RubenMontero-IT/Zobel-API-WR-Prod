using DhubSolutions.Common.Domain.Entities.Admin;
using DhubSolutions.Reports.Domain.Entities.ReportManager;
using DhubSolutions.Reports.Domain.Entities.ReportManager.Helpers;
using DhubSolutions.Reports.Domain.Services.InstructionProcessors;
using DhubSolutions.WealthReport.Domain.Repositories;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace DhubSolutions.WealthReport.Domain.Services.InstructionProcessors
{
    public class MethodInstructionProcessor : InstructionProcessor
    {
        private readonly IConfiguration configuration;
        private readonly IConnectionByOrganizationRepository repository;

        public MethodInstructionProcessor(IConfiguration configuration, IConnectionByOrganizationRepository repository)
        {

            this.configuration = configuration;
            this.repository = repository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reportDataSegment"></param>
        /// <returns></returns>
        public override async Task<object> ReadDataSegment(ReportDataSegment reportDataSegment)
        {
            if (!_localParameters.TryGetValue("organization", out dynamic organization))
                throw new NullReferenceException(nameof(organization));

            string connectionId = repository.GetConnectionId((Organization)organization);
            if (string.IsNullOrEmpty(connectionId) || string.IsNullOrWhiteSpace(connectionId))
                throw new NullReferenceException(nameof(connectionId));

            Type interfaceType = typeof(IWealthReportDataRepository);

            Type reportDataRepositoryType = AppDomain.CurrentDomain.GetAssemblies()
              .SelectMany(x => x.GetTypes())
              .First(x => interfaceType.IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract);

            Type[] types = new Type[reportDataSegment.Params.Count];
            object[] @params = new object[reportDataSegment.Params.Count];

            for (int i = 0; i < reportDataSegment.Params.Count; i++)
            {
                ReportDataParam param = reportDataSegment.Params[i];

                dynamic paramValue;

                if (!string.IsNullOrEmpty(param.ParamValue) && param.ParamValue.StartsWith('@'))
                {
                    var boolean = _parameters.TryGetValue(
                        reportDataSegment.Params[i].ParamValue.Substring(1), out paramValue);
                    //to do something if paramValue is Null
                }
                else
                    paramValue = reportDataSegment.Params[i].ParamValue;

                switch (param.ParamType)
                {
                    case ReportParamType.INT32:
                        @params[i] = NullableHelper.Parse<int>($"{paramValue}", int.TryParse);
                        types[i] = typeof(int?);
                        break;

                    case ReportParamType.INT64:
                        @params[i] = NullableHelper.Parse<long>($"{paramValue}", long.TryParse);
                        types[i] = typeof(long?);
                        break;

                    case ReportParamType.STRING:
                        @params[i] = paramValue;
                        types[i] = typeof(string);
                        break;

                    case ReportParamType.DATETIME:
                        @params[i] = NullableHelper.Parse<DateTime>($"{paramValue}", DateTime.TryParse);
                        types[i] = typeof(DateTime?);
                        break;

                    case ReportParamType.BOOL:
                        @params[i] = NullableHelper.Parse<bool>($"{paramValue}", bool.TryParse);
                        types[i] = typeof(bool?);
                        break;
                    default:
                        break;
                }
            }

            object reportDataRepository = Activator.CreateInstance(reportDataRepositoryType, configuration, connectionId);

            string methodName = reportDataSegment.Name;


            MethodInfo method = reportDataRepositoryType.GetMethod(methodName, types);

            if (method is null)
            {
                Console.WriteLine($"{methodName} not found");
                throw new NullReferenceException($"{nameof(method)}: { methodName } not found");
            }

            object result = null;

            result = await (Task<string>)method.Invoke(reportDataRepository, @params);

            return JsonConvert.DeserializeObject<dynamic>(result.ToString());
        }
    }

}
