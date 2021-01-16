namespace DhubSolutions.Reports.Domain.Services.InstructionProcessors.Factories.Base
{
    public interface IInstructionProcessorFactory
    {
        /// <summary>
        /// 
        /// </summary>
        InstructionProcessorType InstructionProcessorType { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IInstructionProcessor GetInstructionProcessor();

    }
}
