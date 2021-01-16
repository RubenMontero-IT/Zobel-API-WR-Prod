using DhubSolutions.Common.Domain.Entities.Admin;
using DhubSolutions.WealthReport.Application.Services.Tools;
using DhubSolutions.WealthReport.Domain.Entities;

namespace DhubSolutions.WealthReport.Application.Services.Base
{
    public interface IProcessManagerService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="organization"></param>
        /// <param name="product"></param>
        /// <returns></returns>
        Process GetLastProcess(Organization organization, Product product);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organization"></param>
        /// <param name="product"></param>
        /// <returns></returns>
        ProcessStatus GetProcessStatus(Organization organization, Product product);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="product"></param>
        /// <param name="process"></param>
        /// <returns></returns>
        void AddProcess(Product product, Process process);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="product"></param>
        void RemoveProcess(Product product);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organization"></param>
        /// <param name="processName"></param>
        /// <returns></returns>
        bool AllProcessFinished(Organization organization, string processName = "TL_PAO");
    }
}
