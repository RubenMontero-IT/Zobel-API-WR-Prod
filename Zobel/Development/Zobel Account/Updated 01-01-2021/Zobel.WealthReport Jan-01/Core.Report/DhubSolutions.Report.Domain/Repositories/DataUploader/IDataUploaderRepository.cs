using DhubSolutions.Reports.Domain.Entities.DataUploader.DataRow.Base;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DhubSolutions.Reports.Domain.Repositories.DataUploader
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDataUploaderRepository
    {
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
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        void Insert<T>(T entity) where T : class, IDataUploaderDataRow, new();

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate"></param>
        void Select<T>(Expression<Func<T, bool>> predicate);

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
        /// <param name="organisationId"></param>
        /// <returns></returns>
        object GetAccountsByOrganisationID(string organisationId);

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
        /// <returns></returns>
        IEnumerable<object> GetExcelRefItems();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<object> GetAccounts(int? organization = null);

    }
}
