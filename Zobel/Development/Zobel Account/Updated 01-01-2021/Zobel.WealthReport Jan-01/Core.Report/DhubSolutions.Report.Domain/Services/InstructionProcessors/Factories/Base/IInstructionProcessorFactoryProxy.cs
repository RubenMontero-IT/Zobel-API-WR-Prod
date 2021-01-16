using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DhubSolutions.Reports.Domain.Services.InstructionProcessors.Factories.Base
{
    public interface IInstructionProcessorFactoryProxy
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameters"></param>
        void AddLocalParameters(params (string name, dynamic @object)[] parameters);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataSegments"></param>
        /// <param name="parameters"></param>
        /// <param name="dataResults"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        Task ReadBlockDataSegments(JObject dataSegments,
            (string propertyName, dynamic @object)[] resultPair,
            int index, Dictionary<string, dynamic> @params = null);
    }
}



