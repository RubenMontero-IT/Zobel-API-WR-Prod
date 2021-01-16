using DhubSolutions.Core.Domain.Data;
using DhubSolutions.Reports.Application.Services.DataUploader.Base;
using DhubSolutions.Reports.Domain.Entities.DataUploader.DataRow.Base;
using DhubSolutions.Reports.Domain.Repositories.DataUploader;
using System;
using System.Collections.Generic;

namespace DhubSolutions.Reports.Application.Services.DataUploader
{
    /// <summary>
    /// 
    /// </summary>
    public class DataUploaderService : IDataUploaderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDataUploaderRepository _dataUploaderRepository;
        public DataUploaderService(IUnitOfWork unitOfWork, IDataUploaderRepository dataUploaderRepository)
        {
            _unitOfWork = unitOfWork;
            _dataUploaderRepository = dataUploaderRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="organisationId"></param>
        /// <param name="periodId"></param>
        /// <param name="insertItems"></param>
        public void Insert<T>(string organisationId, string periodId, List<T> insertItems)
            where T : class, IDataUploaderDataRow, new()
        {
            if (insertItems != null)
            {
                Delete<T>(organisationId, periodId);
                var labdae = new Action<IDataUploaderDataRow>(e =>
                {
                    e.OrganisationId = organisationId;
                    e.PeriodId = periodId;
                });
                insertItems.ForEach(labdae);
                foreach (T item in insertItems)
                {
                    _dataUploaderRepository.Insert<T>(item);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organisationId"></param>
        /// <param name="periodId"></param>
        /// <returns></returns>
        public object GetDataUploadChecks(string organisationId, string periodId)
        {
            return _dataUploaderRepository.GetDataUploadChecks(organisationId, periodId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="OrganisationId"></param>
        /// <returns></returns>
        public object GetAccountsByOrganisationID(string OrganisationId)
        {
            return _dataUploaderRepository.GetAccountsByOrganisationID(OrganisationId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public object GetExcelFields()
        {
            return _dataUploaderRepository.GetExcelFields();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public object GetOrganisations()
        {
            return _dataUploaderRepository.GetOrganisations();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public object GetAllPeriods()
        {
            return _dataUploaderRepository.GetAllPeriods();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="organizationId"></param>
        /// <param name="periodId"></param>
        public void Delete<T>(string organizationId, string periodId) where T : class, new()
        {
            _dataUploaderRepository.Delete<T>(organizationId, periodId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<object> GetExcelRefItems()
        {
            return _dataUploaderRepository.GetExcelRefItems();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organization"></param>
        /// <returns></returns>
        public IEnumerable<object> GetAccounts(int? organization = null)
        {
            return _dataUploaderRepository.GetAccounts(organization);
        }
    }
}
