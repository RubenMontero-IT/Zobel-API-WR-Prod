using DhubSolutions.Reports.Domain.Entities.ReportManager;
using DhubSolutions.Reports.Domain.Services.InstructionProcessors;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace DhubSolutions.WealthReport.Domain.Services.InstructionProcessors
{
    public class DataInstructionProcessor : InstructionProcessor
    {
        public DataInstructionProcessor() : base()
        { }

        public override async Task<object> ReadDataSegment(ReportDataSegment reportDataSegment)
        {
            return await Task.Factory.StartNew(
                 () =>
                 {
                     JToken jToken = JsonConvert.DeserializeObject<JToken>($"{reportDataSegment.Value}");
                     ProcessJToken(jToken);
                     return jToken;
                 });

            void ProcessJToken(JToken jToken)
            {
                switch (jToken)
                {
                    case JArray jArray:
                        foreach (var item in jArray)
                            ProcessJToken(item);
                        break;

                    case JObject json:
                        foreach (var item in json)
                            ProcessJToken(item.Value);
                        break;

                    case JValue jValue:
                        if (jValue.Value is string value && value.StartsWith('@') &&
                            _parameters.TryGetValue(value.Substring(1), out var str))
                        {
                            // search  value in dictionary
                            jValue.Value = str;
                        }
                        break;
                }
            }
        }
    }
}
