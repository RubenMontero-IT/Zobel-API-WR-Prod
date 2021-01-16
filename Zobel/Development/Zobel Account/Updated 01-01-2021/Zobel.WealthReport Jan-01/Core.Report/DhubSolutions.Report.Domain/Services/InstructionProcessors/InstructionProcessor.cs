using DhubSolutions.Reports.Domain.Entities.ReportManager;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DhubSolutions.Reports.Domain.Services.InstructionProcessors
{
    public abstract class InstructionProcessor : IInstructionProcessor
    {
        protected readonly Dictionary<string, dynamic> _parameters;
        protected readonly Dictionary<string, dynamic> _localParameters;
        public InstructionProcessor()
        {
            _parameters = new Dictionary<string, object>();
            _localParameters = new Dictionary<string, dynamic>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameters"></param>
        public void AddLocalParameters(params (string name, dynamic @object)[] parameters)
        {
            foreach (var (name, value) in parameters)
                _localParameters.Add(name, value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameters"></param>
        public virtual void AddParameters(params (string name, dynamic value)[] parameters)
        {
            foreach (var (name, value) in parameters)
                _parameters.Add(name, value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reportDataSegment"></param>
        /// <returns></returns>
        public abstract Task<object> ReadDataSegment(ReportDataSegment reportDataSegment);


    }
}
