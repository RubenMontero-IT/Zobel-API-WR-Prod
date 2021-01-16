using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DhubSolutions.Common.Domain.Entities.Admin;
using DhubSolutions.WealthReport.Application.Dtos;
using DhubSolutions.WealthReport.Domain.Entities;

namespace DhubSolutions.WealthReport.Application.Services.Base
{
    public interface IProductService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="organization"></param>
        /// <param name="productID"></param>
        /// <param name="productData"></param>      
        void AddHistoricalInfo(Organization organization, string productID, IEnumerable<ProductDataDto> investmentProductData);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="organization"></param>
        /// <param name="productID"></param>
        /// <param name="productData"></param>        
        void UpdateHistoricalInfo(Organization organization, string productID, IEnumerable<ProductDataUpdateDto> productData);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="organization"></param>
        /// <param name="productId"></param>
        /// <param name="historicalPricesID"></param>       
        void RemoveHistoricalInfo(Organization organization, string productId, IEnumerable<ProductDto> historicalPricesID);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="organization"></param>
        /// <param name="productId"></param>
        /// <param name="transactionCollectionID"></param>       
        void RemoveProductTransaction(Organization organization, string productId, IEnumerable<ProductDto> transactionCollectionID);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organization"></param>
        /// <param name="ProductGeneralInfo"></param>
        /// <exception cref="InvalidOperationException">
        /// the exception is thrown when the number of affected rows is less than zero. </exception>
        /// <returns> returns the product identifier created. </returns>
        Task<dynamic> AddGeneralInfo(Organization organization, ProductGeneralInfoDto ProductGeneralInfo, IEnumerable<ProductDto> parentPortfolio);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="organization"></param>
        /// <param name="productID"></param>
        /// <param name="transactionCollection"></param>        
        void AddProductTransaction(Organization organization, string productID, IEnumerable<TransactionDto> transactionCollection);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="organization"></param>
        /// <param name="productCreation"></param>
        /// <exception cref="InvalidOperationException"></exception>
        /// <returns>returns the product identifier created</returns>
        Task<dynamic> CreateProduct(Organization organization, ProductCreationDto productCreation);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="organization"></param>
        /// <param name="productId"></param>
        /// <param name="productUpdate"></param>
        /// <exception cref="InvalidOperationException"></exception>       
        void UpdateProduct(Organization organization, string productId, ProductUpdateDto productUpdate);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="organization"></param>
        /// <param name="productID"></param>
        /// <param name="transactionCollection"></param>        
        void UpdateTransaction(Organization organization, string productID, IEnumerable<TransactionUpdateDto> transactionCollection);



        /// <summary>
        /// 
        /// </summary>
        /// <param name="productID"></param>
        /// <param name="organization"></param>
        /// <param name="productGeneralInfo"></param>
        void UpdateGeneralInfo( Organization organization, string productID, ProductGeneralInfoDto productGeneralInfo, IEnumerable<ProductDto> parentPortfolio);


        /// <summary>
        /// 
        /// </summary> 
        /// <param name="organization"></param>
        /// <param name="productId"></param>
        /// <param name="productHistoricalinfoCollection"></param>        
        void UpdateHistoricalInfoCollection(Organization organization, string productId, IEnumerable<ProductHistoricalInfoCollectionDto> productHistoricalinfoCollections);


        /// <summary>
        /// 
        /// </summary> 
        /// <param name="organization"></param>
        /// <param name="productId"></param>
        /// <param name="productTransactionsCollection"></param>        
        void UpdateProductTransactionsCollection(Organization organization, string productId, IEnumerable<ProductTransactionsCollectionDto> productTransactionsCollection);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="organization"></param>        
        /// <param name="uploadJson"></param>        
        void UploadHistoricalPrices(Organization organization, IEnumerable<UploadHistoricalPricesDto> uploadJson);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TViewModel"></typeparam>
        /// <param name="organization"></param>
        /// <param name="asNoTracking"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        IEnumerable<TViewModel> GetAllInvestmentProducts<TViewModel>(
            Organization organization,
            Expression<Func<Product, bool>> filter = null,
            bool asNoTracking = true,
            params Expression<Func<Product, object>>[] includes) where TViewModel : class;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="Dto"></typeparam>
        /// <param name="organization"></param>
        /// <param name="filter"></param>
        /// <param name="asNoTracking"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        Dto GetInvestementProduct<Dto>(
            Organization organization,
            Expression<Func<Product, bool>> filter = null,
            bool asNoTracking = true,
            params Expression<Func<Product, object>>[] includes) where Dto : class;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organization"></param>
        /// <returns></returns>
        Task<IEnumerable<dynamic>> GetAllProducts(Organization organization);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organization"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        Task<dynamic> GetProduct(Organization organization, string productId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organization"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        Task<IEnumerable<dynamic>> GetProductTransactions(Organization organization, string productId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organization"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        Task<IEnumerable<dynamic>> GetProductHistoricalPrices(Organization organization, string productId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organization"></param>
        /// <returns></returns>
        dynamic GetLastSuccessfulMetricCalculation(Organization organization);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organization"></param>
        /// <returns></returns>
        dynamic GetLastMetricCalculationExecutionStatus(Organization organization);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organization"></param>
        void MetricRecalc(Organization organization);
    }
}
