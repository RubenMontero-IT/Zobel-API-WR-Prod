using DhubSolutions.Reports.Domain.Services.InstructionProcessors;
using DhubSolutions.Reports.Domain.Services.InstructionProcessors.Factories;
using DhubSolutions.WealthReport.Domain.Repositories;
using Microsoft.Extensions.Configuration;

namespace DhubSolutions.WealthReport.Domain.Services.InstructionProcessors.Factories
{
    public class MethodInstructionProcessorFactory : InstructionProcessorFactory
    {
        private readonly IConfiguration _configuration;
        private readonly IConnectionByOrganizationRepository _repository;

        public MethodInstructionProcessorFactory(IConfiguration configuration, IConnectionByOrganizationRepository repository)
            : base()
        {
            _configuration = configuration;
            _repository = repository;
        }

        public override InstructionProcessorType InstructionProcessorType => InstructionProcessorType.METHOD;

        public override IInstructionProcessor GetInstructionProcessor()
        {
            return new MethodInstructionProcessor(_configuration, _repository);
        }
    }
}
