using Dapper;
using DhubSolutions.Common.Domain.Entities.Admin;
using DhubSolutions.Core.Domain.Adapters;
using DhubSolutions.WealthReport.Application.Dtos;
using DhubSolutions.WealthReport.Application.Services.Base;
using DhubSolutions.WealthReport.Application.Services.Tools;
using DhubSolutions.WealthReport.Domain.Entities;
using DhubSolutions.WealthReport.Domain.Repositories;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DhubSolutions.WealthReport.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IConfiguration _configuration;
        private readonly ITypeAdapter _typeAdapter;
        private readonly IProcessManagerService _processManagerService;
        private readonly IOrganizationManagerService _organizationManagerService;
        private readonly IQueryableRepository _queryableRepository;
        private readonly IWealthReportRepository<Product> _repository;
        private readonly int? _commandTimeout;

        public ProductService(
            IConfiguration configuration,
            ITypeAdapter typeAdapter,
            IProcessManagerService processManagerService,
            IOrganizationManagerService organizationManagerService,
            IQueryableRepository queryableRepository,
            IWealthReportRepository<Product> repository)
        {
            _configuration = configuration;
            _typeAdapter = typeAdapter;
            _processManagerService = processManagerService;
            _organizationManagerService = organizationManagerService;
            _queryableRepository = queryableRepository;
            _repository = repository;
            _commandTimeout = configuration.GetValue<int?>("commandTimeout", defaultValue: default);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organization"></param>        
        /// <returns></returns>
        public async Task<IEnumerable<dynamic>> GetAllProducts(Organization organization)
        {
            string connectionString = _organizationManagerService.GetConnectionString(organization);

            _queryableRepository.SetConnectionString(connectionString);

            var @params = new { OrganisationId = organization.Id };

            string query = $"EXEC [invp].[GetProductsByOrganization] @OrganisationId ";

            IEnumerable<dynamic> products = await _queryableRepository.ExecuteQueryAsync<dynamic>(query, @params, commandTimeout: GetCommandTimeout());

            return products;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organization"></param>        
        /// <param name="productId"></param>         
        /// <returns></returns>
        public async Task<dynamic> GetProduct(Organization organization, string productId)
        {
            string connectionString = _organizationManagerService.GetConnectionString(organization);

            _queryableRepository.SetConnectionString(connectionString);

            var @params = new { OrganisationId = organization.Id, productId };

            string query = $"EXEC [invp].[GetProducts]   @OrganisationId, @productId";

            IEnumerable<dynamic> product = await _queryableRepository.ExecuteQueryAsync<dynamic>(query, @params, commandTimeout: GetCommandTimeout());            

            return product.SingleOrDefault();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organization"></param>        
        /// <param name="productId"></param>         
        /// <returns></returns>
        public async Task<IEnumerable<dynamic>> GetProductTransactions(Organization organization, string productId)
        {
            string connectionString = _organizationManagerService.GetConnectionString(organization);
            _queryableRepository.SetConnectionString(connectionString);

            var @params = new { OrganisationId = organization.Id, productId };

            string query = $"EXEC [invp].[GetProductTransactions]  @OrganisationId, @productId";

            IEnumerable<dynamic> productTransactions = await _queryableRepository.ExecuteQueryAsync<dynamic>(query, @params, commandTimeout: GetCommandTimeout());

            return productTransactions;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organization"></param>        
        /// <param name="productId"></param>         
        /// <returns></returns>
        public async Task<IEnumerable<dynamic>> GetProductHistoricalPrices(Organization organization, string productId)
        {
            string connectionString = _organizationManagerService.GetConnectionString(organization);
            _queryableRepository.SetConnectionString(connectionString);

            var @params = new { OrganisationId = organization.Id, productId };

            string query = $"EXEC [invp].[GetProductHistoricalPrices]  @OrganisationId, @productId";

            IEnumerable<dynamic> productHistoricalPrices = await _queryableRepository.ExecuteQueryAsync<dynamic>(query, @params, commandTimeout: GetCommandTimeout());

            return productHistoricalPrices;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organization"></param>
        /// <param name="productCreation"></param>
        /// <exception cref="InvalidOperationException"></exception>
        /// <returns>returns the product identifier created</returns>
        public async Task<dynamic> CreateProduct(Organization organization, ProductCreationDto productCreation)
        {
            dynamic product = await AddGeneralInfo(organization, productCreation.ProductGeneralInfo, productCreation.ParentPortfolio);

            if (productCreation.ProductHistoricalInfo != null)
            {
                IEnumerable<ProductHistoricalInfoDto> productHistoricalInfo = productCreation.ProductHistoricalInfo;
                foreach (ProductHistoricalInfoDto productHistorical in productHistoricalInfo)
                {
                    AddHistoricalInfo(organization, product.ProductID, productHistorical.ProductData);
                }
            }
            if (productCreation.Transactions != null)
                AddProductTransaction(organization, product.ProductID, productCreation.Transactions);
            return product;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organization"></param>
        /// <param name="productId"></param>
        /// <param name="productUpdate"></param>
        /// <exception cref="InvalidOperationException"></exception>       
        public void UpdateProduct(Organization organization, string productId, ProductUpdateDto productUpdate)
        {
            if (productUpdate.ProductGeneralInfo  !=  null)
                UpdateGeneralInfo( organization, productId, productUpdate.ProductGeneralInfo, productUpdate.ParentPortfolio);

            if (productUpdate.ProductHistoricalInfoCollection != null)
                UpdateHistoricalInfoCollection(organization, productId, productUpdate.ProductHistoricalInfoCollection);

            if (productUpdate.transactions != null)
                UpdateProductTransactionsCollection(organization, productId, productUpdate.transactions);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organization"></param>
        /// <param name="generalInfo"></param>
        /// <exception cref="InvalidOperationException"></exception>
        /// <returns>returns the product identifier created</returns>
        public async Task<dynamic> AddGeneralInfo(Organization organization, ProductGeneralInfoDto generalInfo, IEnumerable<ProductDto> parentPortfolio)
        {
            using (IDbConnection connection = GetConnection(organization))
            {
                DynamicParameters parameters = new DynamicParameters(generalInfo);
                parameters.Add("@OrganizationID", organization.Id, dbType: DbType.String, size: 100);
                parameters.Add("@PortfolioIDJSON", JsonConvert.SerializeObject(parentPortfolio), dbType: DbType.String, size: 100);
                parameters.Add("@RowsAffected", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("@NewProductID", dbType: DbType.String, size: 100, direction: ParameterDirection.Output);

                connection.Open();
                connection.Execute(
                    sql: "[invp].[CreateProductTypesByAssetClass]",
                    param: parameters,
                    commandType: CommandType.StoredProcedure);

                int affectedRows = parameters.Get<int>("@RowsAffected");
                if (affectedRows <= 0)
                    throw new InvalidOperationException("Error when trying to save general product information");

                string productId = parameters.Get<string>("@NewProductID");
                dynamic product = await GetProduct(organization, productId);

                return product;
            }
        }

        /// <summary>
        /// 
        /// </summary>       
        /// <param name="organization"></param>
        /// <param name="productID"></param>
        /// <param name="productGeneralInfo"></param>
        public void UpdateGeneralInfo( Organization organization, string productID, ProductGeneralInfoDto productGeneralInfo, IEnumerable<ProductDto> parentPortfolio)
        {
            using (IDbConnection connection = GetConnection(organization))
            {
                DynamicParameters parameters = new DynamicParameters(productGeneralInfo);
                parameters.Add("@OrganizationID", organization.Id, dbType: DbType.String, size: 100);
                parameters.Add("@PortfolioIDJSON", JsonConvert.SerializeObject(parentPortfolio), dbType: DbType.String, size: 1000000);
                parameters.Add("@ProductID", productID, dbType: DbType.String, size: 100);
                parameters.Add("@RowsAffected", dbType: DbType.Int32, direction: ParameterDirection.Output);

                connection.Open();
                connection.Execute(
                    sql: "[invp].[UpdateProductTypesByAssetClass]",
                    param: parameters,
                    commandType: CommandType.StoredProcedure);

                int affectedRows = parameters.Get<int>("@RowsAffected");
                if (affectedRows <= 0)
                    throw new InvalidOperationException("Error when trying to update general product information");
            }
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <param name="organization"></param>
        /// <param name="productId"></param>
        /// <param name="productHistoricalinfoCollection"></param>        
        public void UpdateHistoricalInfoCollection(Organization organization, string productId, IEnumerable<ProductHistoricalInfoCollectionDto> productHistoricalinfoCollection)
        {
            foreach (ProductHistoricalInfoCollectionDto productHistoricalCollection in productHistoricalinfoCollection)
            {
                if (productHistoricalCollection.ProductDataCollection.Remove.Count() > 0)
                    RemoveHistoricalInfo(organization, productId, productHistoricalCollection.ProductDataCollection.Remove);

                if (productHistoricalCollection.ProductDataCollection.Add.Count() > 0)
                    AddHistoricalInfo(organization, productId, productHistoricalCollection.ProductDataCollection.Add);

                if (productHistoricalCollection.ProductDataCollection.Update.Count() > 0)
                    UpdateHistoricalInfo(organization, productId, productHistoricalCollection.ProductDataCollection.Update);
            }
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <param name="organization"></param>
        /// <param name="productId"></param>
        /// <param name="productTransactionsCollection"></param>        
        public void UpdateProductTransactionsCollection(Organization organization, string productId, IEnumerable<ProductTransactionsCollectionDto> productTransactionsCollection)
        {
            foreach (ProductTransactionsCollectionDto transactionsCollection in productTransactionsCollection)
            {
                if (transactionsCollection.Remove.Count() > 0)
                    RemoveProductTransaction(organization, productId, transactionsCollection.Remove);

                if (transactionsCollection.Add.Count() > 0)
                    AddProductTransaction(organization, productId, transactionsCollection.Add);

                if (transactionsCollection.Update.Count() > 0)
                    UpdateTransaction(organization, productId, transactionsCollection.Update);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organization"></param>        
        /// <param name="uploadJson"></param>        
        public void UploadHistoricalPrices(Organization organization, IEnumerable<UploadHistoricalPricesDto> uploadJson)
        {
            foreach (UploadHistoricalPricesDto info in uploadJson)
            {
                RemoveHistoricalInfo(organization, info.ProductId);
                AddHistoricalInfo(organization, info.ProductId, info.Data);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organization"></param>
        /// <param name="productID"></param>
        /// <param name="productData"></param>        
        public void AddHistoricalInfo(Organization organization, string productID, IEnumerable<ProductDataDto> productData)
        {
            using (IDbConnection connection = GetConnection(organization))
            {
                connection.Open();

                foreach (ProductDataDto data in productData)
                {
                    DynamicParameters parameters = new DynamicParameters(data);
                    parameters.Add("@OrganizationID", organization.Id, dbType: DbType.String, size: 100);
                    parameters.Add("@ProductID", productID, dbType: DbType.String, size: 100);
                    parameters.Add("@RowsAffected", dbType: DbType.Int32, direction: ParameterDirection.Output);
                    parameters.Add("@NewID", dbType: DbType.String, size: 100, direction: ParameterDirection.Output);

                    connection.Execute(
                        sql: "[invp].[CreatetProductHistoricalPrice]",
                        param: parameters,
                        commandType: CommandType.StoredProcedure);

                    int affectedRows = parameters.Get<int>("@RowsAffected");
                    if (affectedRows <= 0)
                        throw new InvalidOperationException("Error when trying to save Historical Price information");
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organization"></param>
        /// <param name="productID"></param>
        /// <param name="transactionCollection"></param>        
        public void AddProductTransaction(Organization organization, string productID, IEnumerable<TransactionDto> transactionCollection)
        {
            using (IDbConnection connection = GetConnection(organization))
            {
                connection.Open();

                foreach (TransactionDto data in transactionCollection)
                {
                    DynamicParameters parameters = new DynamicParameters(data);
                    parameters.Add("@OrganizationID", organization.Id, dbType: DbType.String, size: 100);
                    parameters.Add("@ProductID", productID, dbType: DbType.String, size: 100);
                    parameters.Add("@RowsAffected", dbType: DbType.Int32, direction: ParameterDirection.Output);
                    parameters.Add("@NewID", dbType: DbType.String, size: 100, direction: ParameterDirection.Output);

                    connection.Execute(
                        sql: "[invp].[CreateProductTransactions]",
                        param: parameters,
                        commandType: CommandType.StoredProcedure);

                    int affectedRows = parameters.Get<int>("@RowsAffected");
                    if (affectedRows <= 0)
                        throw new InvalidOperationException("Error when trying to save transactions information");
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organization"></param>
        /// <param name="productID"></param>
        /// <param name="productData"></param>        
        public void UpdateHistoricalInfo(Organization organization, string productID, IEnumerable<ProductDataUpdateDto> productData)
        {
            using (IDbConnection connection = GetConnection(organization))
            {
                connection.Open();

                foreach (ProductDataUpdateDto data in productData)
                {
                    DynamicParameters parameters = new DynamicParameters(data);
                    parameters.Add("@OrganizationID", organization.Id, dbType: DbType.String, size: 100);
                    parameters.Add("@ProductID", productID, dbType: DbType.String, size: 100);
                    parameters.Add("@RowsAffected", dbType: DbType.Int32, direction: ParameterDirection.Output);

                    connection.Execute(
                        sql: "[invp].[UpdateProductHistoricalPrice]",
                        param: parameters,
                        commandType: CommandType.StoredProcedure);

                    int affectedRows = parameters.Get<int>("@RowsAffected");
                    if (affectedRows <= 0)
                        throw new InvalidOperationException("error when trying to update hitorical product information");
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organization"></param>
        /// <param name="productID"></param>
        /// <param name="transactionCollection"></param>        
        public void UpdateTransaction(Organization organization, string productID, IEnumerable<TransactionUpdateDto> transactionCollection)
        {
            using (IDbConnection connection = GetConnection(organization))
            {
                connection.Open();

                foreach (TransactionUpdateDto data in transactionCollection)
                {
                    DynamicParameters parameters = new DynamicParameters(data);
                    parameters.Add("@OrganizationID", organization.Id, dbType: DbType.String, size: 100);
                    parameters.Add("@ProductID", productID, dbType: DbType.String, size: 100);
                    parameters.Add("@RowsAffected", dbType: DbType.Int32, direction: ParameterDirection.Output);

                    connection.Execute(
                        sql: "[invp].[UpdateProductTransactions]",
                        param: parameters,
                        commandType: CommandType.StoredProcedure);

                    int affectedRows = parameters.Get<int>("@RowsAffected");
                    if (affectedRows <= 0)
                        throw new InvalidOperationException("Error when trying to update product transactions information");
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organization"></param>
        /// <param name="productId"></param>
        /// <param name="historicalPricesCollectionID"></param>       
        public void RemoveHistoricalInfo(Organization organization, string productId, IEnumerable<ProductDto> historicalPricesCollectionID)
        {
            using (IDbConnection connection = GetConnection(organization))
            {
                connection.Open();

                foreach (ProductDto data in historicalPricesCollectionID)
                {

                    DynamicParameters parameters = new DynamicParameters(data);
                    parameters.Add("@OrganizationID", organization.Id, dbType: DbType.String, size: 100);
                    parameters.Add("@ProductID", productId, dbType: DbType.String, size: 100);
                    parameters.Add("@RowsAffected", dbType: DbType.Int32, direction: ParameterDirection.Output);

                    connection.Execute(
                        sql: "[invp].[RemoveProductHistoricalPrice]",
                        param: parameters,
                        commandType: CommandType.StoredProcedure);

                    int affectedRows = parameters.Get<int>("@RowsAffected");
                    if (affectedRows <= 0)
                        throw new InvalidOperationException("Error when trying to remove historical product information");
                }
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="organization"></param>
        /// <param name="productId"></param>             
        public void RemoveHistoricalInfo(Organization organization, string productId)
        {
            using (IDbConnection connection = GetConnection(organization))
            {
                var @params = new { productId, OrganisationId = organization.Id };
                string query = @"DELETE FROM  [invp].[ProductRegistry]  WHERE ProductID = @productId  AND  OrganizationID = @OrganisationId ";
                connection.Open();

                connection.Execute(query, @params, commandTimeout: _commandTimeout);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organization"></param>
        /// <param name="productId"></param>
        /// <param name="transactionCollectionID"></param>       
        public void RemoveProductTransaction(Organization organization, string productId, IEnumerable<ProductDto> transactionCollectionID)
        {
            using (IDbConnection connection = GetConnection(organization))
            {
                connection.Open();

                foreach (ProductDto data in transactionCollectionID)
                {
                    DynamicParameters parameters = new DynamicParameters(data);
                    parameters.Add("@OrganizationID", organization.Id, dbType: DbType.String, size: 100);
                    parameters.Add("@ProductID", productId, dbType: DbType.String, size: 100);
                    parameters.Add("@RowsAffected", dbType: DbType.Int32, direction: ParameterDirection.Output);

                    connection.Execute(
                        sql: "[invp].[RemoveProductTransactions]",
                        param: parameters,
                        commandType: CommandType.StoredProcedure);

                    int affectedRows = parameters.Get<int>("@RowsAffected");
                    if (affectedRows <= 0)
                        throw new InvalidOperationException("Error when trying to remove product transaction information");
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="Dto"></typeparam>
        /// <param name="filter"></param>      
        /// <param name="asNoTracking"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        public IEnumerable<Dto> GetAllInvestmentProducts<Dto>(
        Organization organization,
        Expression<Func<Product, bool>> filter = null,
        bool asNoTracking = true,
        params Expression<Func<Product, object>>[] includes) where Dto : class
        {
            IEnumerable<Product> products = _repository.GetAll(organization, filter, asNoTracking, includes);

            return _typeAdapter.Adapt<IEnumerable<Dto>>(products);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="Dto"></typeparam>
        /// <param name="organization"></param>
        /// <param name="filter"></param>
        /// <param name="asNoTracking"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        public Dto GetInvestementProduct<Dto>(
            Organization organization,
            Expression<Func<Product, bool>> filter = null,
            bool asNoTracking = true,
            params Expression<Func<Product, object>>[] includes) where Dto : class
        {
            Product product = _repository.Get(organization, filter, asNoTracking, includes);

            return _typeAdapter.Adapt<Dto>(product);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organization"></param>
        public void MetricRecalc(Organization organization)
        {
            ExecuteStoreProcedureAsync(organization, "[invp].[UpdateMetricsAndUnitsFromPrices]");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organization"></param>
        /// <returns></returns>
        public dynamic GetLastSuccessfulMetricCalculation(Organization organization)
        {
            return InvokeFunction(organization, "[isetting].[GetLastSuccessfulMetricCalculation]()");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organization"></param>
        /// <returns></returns>
        public dynamic GetLastMetricCalculationExecutionStatus(Organization organization)
        {
            return InvokeFunction(organization, "[isetting].[GetLastMetricCalculationExecutionStatus]()");
        }

        #region Private Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organization"></param>
        /// <param name="storeProcedure"></param>
        private async Task ExecuteStoreProcedureAsync(Organization organization, string storeProcedure)
        {
            using (IDbConnection connection = GetConnection(organization))
            {
                connection.Open();

                await connection.ExecuteAsync(
                     sql: $"{storeProcedure}",
                     commandType: CommandType.StoredProcedure,
                     commandTimeout: _commandTimeout);
            }




        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organization"></param>
        /// <param name="functions"></param>
        private dynamic InvokeFunction(Organization organization, string function)
        {
            using (IDbConnection connection = GetConnection(organization))
            {
                connection.Open();

                dynamic scalar = connection.ExecuteScalar<dynamic>(
                    sql: $"select {function}",
                    commandTimeout: _commandTimeout);

                return scalar;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organization"></param>
        /// <returns></returns>
        private IDbConnection GetConnection(Organization organization)
        {
            try
            {
                string connectionString = _organizationManagerService.GetConnectionString(organization);

                return new SqlConnection(connectionString);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private int? GetCommandTimeout()
        {
            return _configuration.GetValue<int?>("commandTimeout", defaultValue: default);
        }

        #endregion

    }
}

