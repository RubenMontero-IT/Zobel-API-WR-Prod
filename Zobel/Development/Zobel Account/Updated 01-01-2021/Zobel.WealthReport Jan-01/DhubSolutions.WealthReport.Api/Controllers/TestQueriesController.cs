using System;
using System.Threading.Tasks;
using DhubSolutions.WealthReport.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DhubSolutions.WealthReport.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestQueriesController : ControllerBase
    {
        private readonly IWealthReportDataRepository _wealthReportDataRepository;
        public TestQueriesController(IWealthReportDataRepository wealthReportDataRepository) : base()
        {
            _wealthReportDataRepository = wealthReportDataRepository;
        }

        [HttpGet("Sample")]
        public async Task<string> GetProductListByStatus()
        {
            var a = await _wealthReportDataRepository.Sample(default, default);
            return a;
        }

        [HttpGet("GetPortfolioCategory")]
        public async Task<string> GetPortfolioCategory()
        {
            var a = await _wealthReportDataRepository.GetPortfolioCategory();
            return a;
        }

        [HttpGet("GetPortfolioCategoryWithParameters")]
        public async Task<string> GetPortfolioCategory(bool? visibleDetails, bool? visibleFacts)
        {
            var a = await _wealthReportDataRepository.GetPortfolioCategory(visibleDetails, visibleFacts);
            return a;
        }

        [HttpGet("GetProductListByStatus")]
        public async Task<string> GetProductListByStatus(string OrganisationId, string status, string visible, string section, bool? underlying, bool? isPortfolio)
        {
            var a = await _wealthReportDataRepository.GetProductListByStatus(OrganisationId, status, visible, section, underlying, isPortfolio);
            return a;
        }

        [HttpGet("GetBenchmarkMetrics")]
        public async Task<string> GetBenchmarkMetrics(string OrganisationId, DateTime currentDate)
        {
            var a = await _wealthReportDataRepository.GetBenchmarkMetrics(OrganisationId, currentDate);
            return a;
        }

        [HttpGet("GetMonthlyBenchmarkMetrics")]
        public async Task<string> GetMonthlyBenchmarkMetrics(string OrganisationId, DateTime currentDate, string metricCode)
        {
            var a = await _wealthReportDataRepository.GetMonthlyBenchmarkMetrics(OrganisationId, currentDate, metricCode);
            return a;
        }

        [HttpGet("GetProductMetricsTopDate")]
        public async Task<string> GetProductMetricsTopDate(string OrganisationId, DateTime currentDate, string mainCurrency, string metricCode, string status)
        {
            var a = await _wealthReportDataRepository.GetProductMetricsTopDate(OrganisationId, currentDate, mainCurrency, metricCode, status);
            return a;
        }

        [HttpGet("GetOrganizationProductsHistoricalMonthlyMetrics")]
        public async Task<string> GetOrganizationProductsHistoricalMonthlyMetrics(string OrganisationId, DateTime currentDate, string metricCode)
        {
            var a = await _wealthReportDataRepository.GetOrganizationProductsHistoricalMonthlyMetrics(OrganisationId, currentDate, metricCode);
            return a;
        }

        [HttpGet("GetProductsHistoricalMonthlyMetrics")]
        public async Task<string> GetProductsHistoricalMonthlyMetrics(string OrganisationId, DateTime currentDate, string metricCode)
        {
            var a = await _wealthReportDataRepository.GetProductsHistoricalMonthlyMetrics(OrganisationId, currentDate, metricCode);
            return a;
        }

        [HttpGet("GetAllocationPortfolio")]
        public async Task<string> GetAllocationPortfolio(string OrganisationId, DateTime currentDate, string metricCode, string mainCurrency, string status)
        {
            var a = await _wealthReportDataRepository.GetAllocationPortfolio(OrganisationId, currentDate, metricCode, mainCurrency, status);
            return a;
        }

        [HttpGet("GetAllocationLiquidity")]
        public async Task<string> GetAllocationLiquidity(string OrganisationId, DateTime currentDate, string metricCode, string mainCurrency, string status)
        {
            var a = await _wealthReportDataRepository.GetAllocationLiquidity(OrganisationId, currentDate, metricCode, mainCurrency, status);
            return a;
        }

        [HttpGet("GetAllocationCurrency")]
        public async Task<string> GetAllocationCurrency(string OrganisationId, DateTime currentDate, string metricCode, string mainCurrency, string status)
        {
            var a = await _wealthReportDataRepository.GetAllocationCurrency(OrganisationId, currentDate, metricCode, mainCurrency, status);
            return a;
        }

        [HttpGet("GetAllocationAssetClass")]
        public async Task<string> GetAllocationAssetClass(string OrganisationId, DateTime currentDate, string metricCode, string mainCurrency, string status)
        {
            var a = await _wealthReportDataRepository.GetAllocationAssetClass(OrganisationId, currentDate, metricCode, mainCurrency, status);
            return a;
        }

        [HttpGet("GetDetailedPerformanceProductGeneralInfo")]
        public async Task<string> GetDetailedPerformanceProductGeneralInfo(string OrganisationId, DateTime currentDate, string productID, string metricCode, string currency)
        {
            var a = await _wealthReportDataRepository.GetDetailedPerformanceProductGeneralInfo(OrganisationId, currentDate, productID, metricCode, currency);
            return a;
        }

        [HttpGet("GetDetaliedPerformanceProductMetric")]
        public async Task<string> GetDetaliedPerformanceProductMetric(string OrganisationId, DateTime currentDate, string productID, string metricCode, string currency)
        {
            var a = await _wealthReportDataRepository.GetDetaliedPerformanceProductMetric(OrganisationId, currentDate, productID, metricCode, currency);
            return a;
        }

        [HttpGet("GetFactsheetProductGeneralInfo")]
        public async Task<string> GetFactsheetProductGeneralInfo(string OrganisationId, string productID)
        {
            var a = await _wealthReportDataRepository.GetFactsheetProductGeneralInfo(OrganisationId, productID);
            return a;
        }

        [HttpGet("GetFactsheetPortfolioGeneralInfo")]
        public async Task<string> GetFactsheetPortfolioGeneralInfo(string categoryCode)
        {
            var a = await _wealthReportDataRepository.GetFactsheetPortfolioGeneralInfo(categoryCode);
            return a;
        }

        [HttpGet("GetFactsheetProductDescription")]
        public async Task<string> GetFactsheetProductDescription(string OrganisationId, string productID)
        {
            var a = await _wealthReportDataRepository.GetFactsheetProductDescription(OrganisationId, productID);
            return a;
        }

        [HttpGet("GetActiveProductsTransactionsAndValuation")]
        public async Task<string> GetActiveProductsTransactionsAndValuation(string OrganisationId, DateTime currentDate)
        {
            var a = await _wealthReportDataRepository.GetActiveProductsTransactionsAndValuation(OrganisationId, currentDate);
            return a;
        }

        [HttpGet("GetClosedProductsTransactions")]
        public async Task<string> GetClosedProductsTransactions(string OrganisationId)
        {
            var a = await _wealthReportDataRepository.GetClosedProductsTransactions(OrganisationId);
            return a;
        }

        [HttpGet("GetCapitalMovements")]
        public async Task<string> GetCapitalMovements(string OrganisationId, DateTime currentDate)
        {
            var a = await _wealthReportDataRepository.GetCapitalMovements(OrganisationId, currentDate);
            return a;
        }

        [HttpGet("GetCashBalanceByAcount")]
        public async Task<string> GetCashBalanceByAcount(string OrganisationId, DateTime currentDate)
        {
            var a = await _wealthReportDataRepository.GetCashBalanceByAcount(OrganisationId, currentDate);
            return a;
        }

        [HttpGet("GetITDPLCapitalBalance")]
        public async Task<string> GetITDPLCapitalBalance(string OrganisationId, DateTime currentDate)
        {
            var a = await _wealthReportDataRepository.GetITDPLCapitalBalance(OrganisationId, currentDate);
            return a;
        }

        [HttpGet("GetPorfolioMetricsTopDate")]
        public async Task<string> GetPorfolioMetricsTopDate(string OrganisationId, DateTime currentDate, string metricCode, string currency, string category)
        {
            var a = await _wealthReportDataRepository.GetPorfolioMetricsTopDate(OrganisationId, currentDate, metricCode, currency, category);
            return a;
        }

        [HttpGet("GetPortfolioHistoricalMonthlyMetrics")]
        public async Task<string> GetPortfolioHistoricalMonthlyMetrics(string OrganisationId, DateTime currentDate, string metricCode, string category)
        {
            var a = await _wealthReportDataRepository.GetPortfolioHistoricalMonthlyMetrics(OrganisationId, currentDate, metricCode, category);
            return a;
        }

        [HttpGet("GetPortfolioHistoricalMonthlyMetrics/withoutCategory")]
        public async Task<string> GetPortfolioHistoricalMonthlyMetrics(string OrganisationId, DateTime currentDate, string metricCode)
        {
            var a = await _wealthReportDataRepository.GetPortfolioHistoricalMonthlyMetrics(OrganisationId, currentDate, metricCode);
            return a;
        }

        [HttpGet("GetDetailedPerformancePortfolioGeneralInfo")]
        public async Task<string> GetDetailedPerformancePortfolioGeneralInfo(string OrganisationId, DateTime currentDate, string metricCode, string currency, string category)
        {
            var a = await _wealthReportDataRepository.GetDetailedPerformancePortfolioGeneralInfo(OrganisationId, currentDate, metricCode, currency, category);
            return a;
        }

        [HttpGet("GetSummarizedProductsMetricsByPortfolio")]
        public async Task<string> GetSummarizedProductsMetricsByPortfolio(string OrganisationId, string metricCode, string currency, DateTime? currentDate, string categoryCode)
        {
            var a = await _wealthReportDataRepository.GetSummarizedProductsMetricsByPortfolio(OrganisationId, metricCode, currency, currentDate, categoryCode);
            return a;
        }

        [HttpGet("GetTopDateMetricsByProduct")]
        public async Task<string> GetTopDateMetricsByProduct(string OrganisationId, DateTime? currentDate, string metricCode)
        {
            var a = await _wealthReportDataRepository.GetTopDateMetricsByProduct(OrganisationId, currentDate, metricCode);
            return a;
        }

        [HttpGet("GetTopDateMetricsByPortfolio")]
        public async Task<string> GetTopDateMetricsByPortfolio(string OrganisationId, DateTime? currentDate, string metricCode)
        {
            var a = await _wealthReportDataRepository.GetTopDateMetricsByPortfolio(OrganisationId, currentDate, metricCode);
            return a;
        }

        [HttpGet("GetHistoricalMetricsByProduct")]
        public async Task<string> GetHistoricalMetricsByProduct(string OrganisationId, DateTime? currentDate, string metricCode)
        {
            var a = await _wealthReportDataRepository.GetHistoricalMetricsByProduct(OrganisationId, currentDate, metricCode);
            return a;
        }

        [HttpGet("GetMonthlyPerformanceAttribution")]
        public async Task<string> GetMonthlyPerformanceAttribution(string OrganisationId, string mainCurrency, DateTime? currentDate)
        {
            var a = await _wealthReportDataRepository.GetMonthlyPerformanceAttribution(OrganisationId, mainCurrency, currentDate);
            return a;
        }
    }
}