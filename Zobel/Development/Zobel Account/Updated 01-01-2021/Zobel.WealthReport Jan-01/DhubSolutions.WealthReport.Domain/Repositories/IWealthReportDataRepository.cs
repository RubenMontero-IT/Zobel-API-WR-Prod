using System;
using System.Threading.Tasks;

namespace DhubSolutions.WealthReport.Domain.Repositories
{
    public interface IWealthReportDataRepository
    {
        Task<string> Sample(string currentPeriod, int OrganisationId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="OrganisationId"></param>
        /// <param name="status"></param>
        /// <param name="visible"></param>
        /// <returns></returns>
        Task<string> GetProductListByStatus(string OrganisationId, string status, string visible);

        /// <summary>
        /// 
        /// </summary>       
        /// <returns></returns>
        Task<string> GetPortfolioCategory();

        /// <summary>
        /// 
        /// </summary> 
        /// <param name="visibleDetails"></param>
        /// <param name="visibleFacts"></param>
        /// <returns></returns>
        Task<string> GetPortfolioCategory(bool? visibleDetails, bool? visibleFacts);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="OrganisationId"></param>
        /// <param name="status"></param>
        /// <param name="visible"></param>
        /// <param name="section"></param>
        /// <param name="underlying"></param>
        /// <param name="isPortfolio"></param> 
        /// <returns></returns>
        Task<string> GetProductListByStatus(string OrganisationId, string status, string visible, string section, bool? underlying, bool? isPortfolio);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="OrganisationId"></param>
        /// <param name="currentDate"></param>
        /// <returns></returns>
        Task<string> GetBenchmarkMetrics(string OrganisationId, DateTime? currentDate);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="OrganisationId"></param>
        /// <param name="currentDate"></param>
        /// <param name="metricCode"></param>
        /// <returns></returns>
        Task<string> GetMonthlyBenchmarkMetrics(string OrganisationId, DateTime? currentDate, string metricCode);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="OrganisationId"></param>
        /// <param name="currentDate"></param>
        /// <param name="mainCurrency"></param>
        /// <param name="metricCode"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        Task<string> GetProductMetricsTopDate(string OrganisationId, DateTime? currentDate, string mainCurrency, string metricCode, string status);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="OrganisationId"></param>
        /// <param name="currentDate"></param>
        /// <param name="metricCode"></param>
        /// <returns></returns>
        Task<string> GetOrganizationProductsHistoricalMonthlyMetrics(string OrganisationId, DateTime? currentDate, string metricCode);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="OrganisationId"></param>
        /// <param name="currentDate"></param>
        /// <param name="metricCode"></param>
        /// <returns></returns>
        Task<string> GetProductsHistoricalMonthlyMetrics(string OrganisationId, DateTime? currentDate, string metricCode);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="OrganisationId"></param>
        /// <param name="currentDate"></param>
        /// <param name="metricCode"></param>
        /// <param name="mainCurrency"></param>
        /// <param name="status"></param>        
        /// <returns></returns>
        Task<string> GetAllocationPortfolio(string OrganisationId, DateTime? currentDate, string metricCode, string mainCurrency, string status);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="OrganisationId"></param>
        /// <param name="currentDate"></param>
        /// <param name="metricCode"></param>
        /// <param name="mainCurrency"></param>
        /// <param name="status"></param>       
        /// <returns></returns>
        Task<string> GetAllocationLiquidity(string OrganisationId, DateTime? currentDate, string metricCode, string mainCurrency, string status);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="OrganisationId"></param>
        /// <param name="currentDate"></param>
        /// <param name="metricCode"></param>
        /// <param name="status"></param>        
        /// <returns></returns>
        Task<string> GetAllocationCurrency(string OrganisationId, DateTime? currentDate, string metricCode, string mainCurrency, string status);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="OrganisationId"></param>
        /// <param name="currentDate"></param>
        /// <param name="metricCode"></param>
        /// <param name="status"></param>        
        /// <returns></returns>
        Task<string> GetAllocationAssetClass(string OrganisationId, DateTime? currentDate, string metricCode, string mainCurrency, string status);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="OrganisationId"></param>
        /// <param name="currentDate"></param>
        /// <param name="productID"></param>
        /// <param name="metricCode"></param>
        /// <param name="currency"></param>
        /// <returns></returns>
        Task<string> GetDetailedPerformanceProductGeneralInfo(string OrganisationId, DateTime? currentDate, string productID, string metricCode, string currency);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="OrganisationId"></param>
        /// <param name="currentDate"></param>
        /// <param name="productID"></param>
        /// <param name="metricCode"></param>
        /// <param name="currency"></param>
        /// <returns></returns>
        Task<string> GetDetaliedPerformanceProductMetric(string OrganisationId, DateTime? currentDate, string productID, string metricCode, string currency);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="OrganisationId"></param>
        /// <param name="productID"></param>
        /// <returns></returns>
        Task<string> GetFactsheetProductGeneralInfo(string OrganisationId, string productID);


        /// <summary>
        /// 
        /// </summary>        
        /// <param name="categoryCode"></param>
        /// <returns></returns>
        Task<string> GetFactsheetPortfolioGeneralInfo(string categoryCode);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="OrganisationId"></param>
        /// <param name="productID"></param>
        /// <returns></returns>
        Task<string> GetFactsheetProductDescription(string OrganisationId, string productID);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="OrganisationId"></param>
        /// <param name="currentDate"></param>
        /// <returns></returns>
        Task<string> GetActiveProductsTransactionsAndValuation(string OrganisationId, DateTime? currentDate);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="OrganisationId"></param>
        /// <returns></returns>
        Task<string> GetClosedProductsTransactions(string OrganisationId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="OrganisationId"></param>
        /// <param name="currentDate"></param>
        /// <returns></returns>
        Task<string> GetCapitalMovements(string OrganisationId, DateTime? currentDate);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="OrganisationId"></param>
        /// <param name="currentDate"></param>
        /// <returns></returns>
        Task<string> GetCashBalanceByAcount(string OrganisationId, DateTime? currentDate);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="OrganisationId"></param>
        /// <param name="currentDate"></param>
        /// <returns></returns>
        Task<string> GetITDPLCapitalBalance(string OrganisationId, DateTime? currentDate);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="OrganisationId"></param>
        /// <param name="currentDate"></param>
        /// <param name="metricCode"></param>
        /// <param name="currency"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        Task<string> GetPorfolioMetricsTopDate(string OrganisationId, DateTime? currentDate, string metricCode, string currency, string category);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="OrganisationId"></param>
        /// <param name="currentDate"></param>
        /// <param name="metricCode"></param>        
        /// <param name="category"></param>
        /// <returns></returns>
        Task<string> GetPortfolioHistoricalMonthlyMetrics(string OrganisationId, DateTime? currentDate, string metricCode, string category);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="OrganisationId"></param>
        /// <param name="currentDate"></param>
        /// <param name="metricCode"></param>               
        /// <returns></returns>
        Task<string> GetPortfolioHistoricalMonthlyMetrics(string OrganisationId, DateTime? currentDate, string metricCode);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="OrganisationId"></param>
        /// <param name="currentDate"></param>
        /// <param name="metricCode"></param>
        /// <param name="currency"></param>   
        /// <param name="category"></param>
        /// <returns></returns>
        Task<string> GetDetailedPerformancePortfolioGeneralInfo(string OrganisationId, DateTime? currentDate, string metricCode, string currency, string category);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="OrganisationId"></param>      
        /// <param name="metricCode"></param>
        /// <param name="currency"></param>  
        /// <param name="currentDate"></param>  
        /// <param name="categoryCode"></param>  
        /// <returns></returns>
        Task<string> GetSummarizedProductsMetricsByPortfolio(string OrganisationId, string metricCode, string currency, DateTime? currentDate, string categoryCode);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="OrganisationId"></param>
        /// <param name="currentDate"></param>       
        /// <param name="metricCode"></param>        
        /// <returns></returns>
        Task<string> GetTopDateMetricsByProduct(string OrganisationId, DateTime? currentDate, string metricCode);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="OrganisationId"></param>
        /// <param name="currentDate"></param>       
        /// <param name="metricCode"></param>        
        /// <returns></returns>
        Task<string> GetTopDateMetricsByPortfolio(string OrganisationId, DateTime? currentDate, string metricCode);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="OrganisationId"></param>
        /// <param name="currentDate"></param>       
        /// <param name="metricCode"></param>        
        /// <returns></returns>
        Task<string> GetHistoricalMetricsByProduct(string OrganisationId, DateTime? currentDate, string metricCode);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="OrganisationId"></param>
        /// <param name="mainCurrency"></param>
        /// <param name="currentDate"></param>  
        /// <returns></returns>
        Task<string> GetMonthlyPerformanceAttribution(string OrganisationId, string mainCurrency, DateTime? currentDate);
    }
}
