using DhubSolutions.Reports.Domain.Services.InstructionProcessors.Factories.Base;

namespace DhubSolutions.Reports.Domain.Services.InstructionProcessors.Factories
{
    public abstract class InstructionProcessorFactory : IInstructionProcessorFactory
    {
        /// <summary>
        /// 
        /// </summary>
        public abstract InstructionProcessorType InstructionProcessorType { get; }

        public InstructionProcessorFactory()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public abstract IInstructionProcessor GetInstructionProcessor();

    }
}
