namespace DhubSolutions.Reports.Domain.Services.InstructionProcessors.Factories.Base
{
    public interface IInstructionProcessorFactoryProvider
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        IInstructionProcessorFactory GetFactory(InstructionProcessorType type);


    }

}
