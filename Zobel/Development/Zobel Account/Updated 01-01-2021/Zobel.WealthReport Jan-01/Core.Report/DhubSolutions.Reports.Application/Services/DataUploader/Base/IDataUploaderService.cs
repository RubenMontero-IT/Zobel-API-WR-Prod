using DhubSolutions.Reports.Domain.Entities.DataUploader.DataRow.Base;
using System.Collections.Generic;

namespace DhubSolutions.Reports.Application.Services.DataUploader.Base
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDataUploaderService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="organisationId"></param>
        /// <param name="periodId"></param>
        /// <param name="insertItems"></param>
        void Insert<T>(string organisationId, string periodId, List<T> insertItems)
            where T : class, IDataUploaderDataRow, new();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organisationId"></param>
        /// <param name="periodId"></param>
        /// <returns></returns>
        object GetDataUploadChecks(string organisationId, string periodId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="OrganisationId"></param>
        /// <returns></returns>
        object GetAccountsByOrganisationID(string OrganisationId);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        object GetExcelFields();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        object GetOrganisations();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        object GetAllPeriods();

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="organizationId"></param>
        /// <param name="periodId"></param>
        void Delete<T>(string organizationId, string periodId) where T : class, new();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<object> GetExcelRefItems();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organization"></param>
        /// <returns></returns>
        IEnumerable<object> GetAccounts(int? organization = null);

    }
}
