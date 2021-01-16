using DhubSolutions.Reports.Domain.Services.InstructionProcessors;
using DhubSolutions.Reports.Domain.Services.InstructionProcessors.Factories;

namespace DhubSolutions.WealthReport.Domain.Services.InstructionProcessors.Factories
{
    public class DataInstructionProcessorFactory : InstructionProcessorFactory
    {
        public DataInstructionProcessorFactory() : base()
        {

        }

        public override InstructionProcessorType InstructionProcessorType => InstructionProcessorType.DATA;

        public override IInstructionProcessor GetInstructionProcessor()
        {
            return new DataInstructionProcessor();
        }
    }
}
