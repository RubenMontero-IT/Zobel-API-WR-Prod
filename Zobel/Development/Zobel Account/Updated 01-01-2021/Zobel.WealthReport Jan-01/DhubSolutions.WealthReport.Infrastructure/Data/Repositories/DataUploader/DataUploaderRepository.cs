using DhubSolutions.Core.Infrastructure.Data;
using DhubSolutions.Reports.Domain.Entities.DataUploader.DataRow.Base;
using DhubSolutions.Reports.Domain.Repositories.DataUploader;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace DhubSolutions.WealthReport.Infrastructure.Data.Repositories.DataUploader
{
    public class DataUploaderRepository : IDataUploaderRepository
    {
        private readonly IEntityFrameworkUnitOfWork _unitOfWork;

        public DataUploaderRepository(IEntityFrameworkUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Delete<T>(string organizationId, string periodId) where T : class, new()
        {
            Type entityType = typeof(T);
            var parameters = new
            {
                PeriodID = periodId,
                OrganisatioID = organizationId
            };

            var query = $"DELETE from [rmg].[{entityType.Name}] WHERE OrganisationID=@OrganisationID  AND  PeriodID=@PeriodID ";
            var result = _unitOfWork.ExecuteQuery<T>(query, parameters);
        }

        public object GetDataUploadChecks(string organisationId, string periodId)
        {
            var parameters = new { PerioID = periodId, OrganisationID = organisationId };
            ;
            var query = $"SELECT distinct * FROM [rmg].[DataUploadChecks] (@OrganisationID, @PeriodID) ORDER BY Period, AccountID, ExcelID";
            return _unitOfWork.ExecuteQuery<dynamic>(query, parameters, commandTimeout: 600);
        }



        public object GetAccountsByOrganisationID(string organisationId)
        {
            var parameters = new { OrganisationID = organisationId };
            var query = $"SELECT DISTINCT AccountName,AccountID,OrganisationID,(CASE WHEN AccountID=OrganisationID THEN 'true' ELSE 'false' END ) AS Consolidated FROM [lnet].[SumaryField] where OrganisationID=@OrganisationID ";
            return _unitOfWork.ExecuteQuery<dynamic>(query, parameters);
        }

        public object GetExcelFields()
        {

            var query = $"SELECT ExcelID,[Account Name-Excel],[ReportType],[IsFormula] FROM [rmg].[ExcelRefId]";
            return _unitOfWork.ExecuteQuery<dynamic>(query);
        }

        public object GetOrganisations()
        {
            var query = $"SELECT DISTINCT OrganisationName,OrganisationID FROM [lnet].[SumaryField]";
            return _unitOfWork.ExecuteQuery<dynamic>(query);
        }

        public object GetAllPeriods()
        {
            var query = $"SELECT DISTINCT PeriodID FROM [lnet].[SumaryField] ORDER BY PeriodID";

            return _unitOfWork.ExecuteQuery<dynamic>(query);

        }

        public void Select<T>(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public void Insert<T>(T entity) where T : class, IDataUploaderDataRow, new()
        {
            Type entityType = typeof(T);
            PropertyInfo[] props = entityType.GetProperties();
            string Columns = "";
            string aColumns = "";
            foreach (var prop in props)
            {
                Columns += $"{prop.Name} ,";
                aColumns += $"@{prop.Name} ,";
            }
            Columns = Columns.Remove(Columns.Length - 1);
            aColumns = aColumns.Remove(aColumns.Length - 1);

            string query = $"Insert into [rmg].[{entityType.Name}] ({Columns}) values ({aColumns})";
            _unitOfWork.ExecuteCommand(query, entity);

        }

        public IEnumerable<object> GetExcelRefItems()
        {
            string query = @"SELECT
	                                rmg.ExcelRefID.ExcelID,
	                                rmg.ExcelRefID.[Account Name-Excel],
	                                rmg.ExcelRefID.IsFormula,
	                                rmg.ExcelRefID.ReportType 
                                FROM
	                                rmg.ExcelRefID";

            return _unitOfWork.ExecuteQuery<dynamic>(query, commandTimeout: 600);


        }

        public IEnumerable<object> GetAccounts(int? organization = null)
        {
            string query = @"SELECT DISTINCT
	                                lnet.SumaryField.AccountName,
	                                lnet.SumaryField.AccountID,
	                                lnet.SumaryField.OrganisationID,
	                                ( CASE WHEN lnet.SumaryField.AccountID = lnet.SumaryField.OrganisationID THEN 'true' ELSE 'false' END ) AS Consolidated 
                                FROM
	                                lnet.SumaryField";

            return _unitOfWork.ExecuteQuery<dynamic>(query, commandTimeout: 600);

        }
    }
}
