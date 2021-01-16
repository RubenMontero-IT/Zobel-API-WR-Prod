using DhubSolutions.Reports.Domain.Services.InstructionProcessors.Factories.Base;
using System.Collections.Generic;
using System.Linq;

namespace DhubSolutions.Reports.Domain.Services.InstructionProcessors.Factories
{
    public class InstructionProcessorFactoryProvider : IInstructionProcessorFactoryProvider
    {
        private readonly Dictionary<InstructionProcessorType, IInstructionProcessorFactory> _factories;

        public InstructionProcessorFactoryProvider(IEnumerable<IInstructionProcessorFactory> factories)
        {
            _factories = factories.ToDictionary(factory => factory.InstructionProcessorType);
        }

        public IInstructionProcessorFactory GetFactory(InstructionProcessorType type)
        {
            _factories.TryGetValue(type, out IInstructionProcessorFactory instructionProcessorFactory);

            return instructionProcessorFactory;
        }
    }


}
