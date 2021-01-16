using DhubSolutions.Reports.Domain.Entities.ReportManager;
using DhubSolutions.Reports.Domain.Services.InstructionProcessors.Factories.Base;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DhubSolutions.Reports.Domain.Services.InstructionProcessors.Factories
{
    public class InstructionProcessorFactoryProxy : IInstructionProcessorFactoryProxy
    {
        private readonly IInstructionProcessorFactoryProvider _factoriesProvider;
        private readonly Dictionary<string, dynamic> _localParameters;
        public InstructionProcessorFactoryProxy(IInstructionProcessorFactoryProvider factoriesProvider)
        {
            _factoriesProvider = factoriesProvider;
            _localParameters = new Dictionary<string, dynamic>();
        }

        public void AddLocalParameters(params (string name, dynamic @object)[] parameters)
        {
            foreach (var (name, @object) in parameters)
                _localParameters[name] = @object;
        }

        public async Task ReadBlockDataSegments(JObject dataSegments,
            (string propertyName, dynamic @object)[] resultPair,
            int index, Dictionary<string, dynamic> @params = null)
        {
            JObject jObject = new JObject();

            foreach (JProperty jProperty in dataSegments.Properties())
            {
                ReportDataSegment dataSegment = JsonConvert.DeserializeObject<ReportDataSegment>($"{jProperty.Value}");

                IInstructionProcessorFactory factory = _factoriesProvider.GetFactory(dataSegment.Type);

                IInstructionProcessor instructionProcessor = factory.GetInstructionProcessor();

                if (@params != null)
                {
                    var parameters = @params.Select(pair => (pair.Key, pair.Value)).ToArray();

                    instructionProcessor.AddParameters(parameters);
                }

                var localParameters = _localParameters.Select(p => (p.Key, p.Value)).ToArray();

                instructionProcessor.AddLocalParameters(localParameters);

                dynamic result = await instructionProcessor.ReadDataSegment(dataSegment);

                jObject[jProperty.Name] = result;
            }

            resultPair[index].@object = jObject;

        }


    }
}


