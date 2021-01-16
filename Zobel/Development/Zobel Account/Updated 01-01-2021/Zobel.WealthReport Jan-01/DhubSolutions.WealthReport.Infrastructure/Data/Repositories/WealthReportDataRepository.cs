using Dapper;
using DhubSolutions.WealthReport.Domain.Repositories;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DhubSolutions.WealthReport.Infrastructure.Data.Repositories
{
    public class WealthReportDataRepository : IWealthReportDataRepository
    {
        private readonly string _connectionString;
        private readonly int? _commandTimeout;

        public WealthReportDataRepository(IConfiguration configuration, string connectionId = "455232FC-DE4D-42D5-9078-87A1575E3C03")
        {
            string connectionString = configuration.GetConnectionString(connectionId);
            if (string.IsNullOrEmpty(connectionString) || string.IsNullOrWhiteSpace(connectionString))
                throw new NullReferenceException(nameof(connectionString));

            _connectionString = connectionString;
            _commandTimeout = configuration.GetValue<int?>("commandTimeout", defaultValue: default);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentPeriod"></param>
        /// <param name="OrganisationId"></param>
        /// <returns></returns>
        public async Task<string> Sample(string currentPeriod, int OrganisationId)
        {
            //using (IDbConnection connection = GetConnection())
            //{
            //    var @params = new { currentPeriod, OrganisationId };

            //    string query = @"select @currentPeriod,
            //                            @organizationId 
            //                   from sometable";

            //    connection.Open();

            //    dynamic result = await connection.QueryAsync(query, @params);

            //    return JsonConvert.SerializeObject(result);
            //}
            return $"commandTimeout {_commandTimeout}";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="OrganisationId"></param>
        /// <param name="status"></param>
        /// <param name="visible"></param>
        /// <returns></returns>
        public async Task<string> GetProductListByStatus(string OrganisationId, string status, string visible)
        {
            using (IDbConnection connection = GetConnection())
            {
                var @params = new { status, OrganisationId, visible };
                string query;
                if (visible == "NULL")
                    query = @"SELECT * FROM [invp].[GetProductListByStatus] (@status, @OrganisationId, DEFAULT, DEFAULT, DEFAULT, DEFAULT)  ORDER BY DisplayName";
                else
                    query = @"SELECT * FROM [invp].[GetProductListByStatus] (@status, @OrganisationId, @visible, DEFAULT, DEFAULT, DEFAULT) ORDER BY DisplayName";

                connection.Open();

                dynamic result = await connection.QueryAsync(query, @params,commandTimeout: _commandTimeout);

                return JsonConvert.SerializeObject(result);
            }
        }


        /// <summary>
        /// 
        /// </summary>         
        /// <returns></returns>
        public async Task<string> GetPortfolioCategory()
        {
            using (IDbConnection connection = GetConnection())
            {                
                 string query = @"SELECT row_number() OVER(ORDER BY PortfolioCategory) as [index]
                                        ,PortfolioCategoryID ProductID
                                        ,PortfolioCategory PortfolioCategoryName
                                        ,CategoryCode
                                  FROM [gral].[PortfolioCategory]
                                  WHERE CategoryCode IS NOT NULL";
               
                connection.Open();

                dynamic result = await connection.QueryAsync(query, commandTimeout: _commandTimeout);

                return JsonConvert.SerializeObject(result);
            }
        }


        /// <summary>
        /// 
        /// </summary> 
        /// <param name="visibleDetails"></param>
        /// <param name="visibleFacts"></param>
        /// <returns></returns>
        public async Task<string> GetPortfolioCategory(bool? visibleDetails, bool? visibleFacts)
        {
            using (IDbConnection connection = GetConnection())
            {
                var @params = new { visibleDetails, visibleFacts };
                string query = @"SELECT row_number() OVER(ORDER BY PortfolioCategory) as [index]
                                       ,PortfolioCategoryID ProductID
                                       ,PortfolioCategory PortfolioCategoryName
                                       ,CategoryCode
                                 FROM   [gral].[PortfolioCategory]
                                 WHERE  CategoryCode IS NOT NULL
                                   AND  VisibleDetails IN ( SELECT VisibleDetails
                                                            FROM (VALUES(0),(1))x(VisibleDetails)
								                            WHERE @visibleDetails IS NULL
								                            UNION  ALL
								                            SELECT @visibleDetails)  
                                   AND  VisibleFacts   IN ( SELECT VisibleFacts
						                                    FROM (VALUES(0),(1))x(VisibleFacts)
						                                    WHERE @visibleFacts IS NULL
						                                    UNION  ALL
						                                    SELECT @visibleFacts)                              
                                ";

                connection.Open();

                dynamic result = await connection.QueryAsync(query, @params, commandTimeout: _commandTimeout);

                return JsonConvert.SerializeObject(result);
            }
        }


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
        public async Task<string> GetProductListByStatus(string OrganisationId, string status, string visible, string section, bool? underlying, bool? isPortfolio)
        {
            using (IDbConnection connection = GetConnection())
            {
                var @params = new { status, OrganisationId, visible, section, underlying, isPortfolio };
                string query;
                if (visible == "NULL")
                    query = @"SELECT * FROM [invp].[GetProductListByStatus] (@status, @OrganisationId, DEFAULT, @section, @underlying, @isPortfolio)  ORDER BY DisplayName";
                else
                    query = @"SELECT * FROM [invp].[GetProductListByStatus] (@status, @OrganisationId, @visible, @section, @underlying, @isPortfolio) ORDER BY DisplayName";

                connection.Open();

                dynamic result = await connection.QueryAsync(query, @params, commandTimeout: _commandTimeout);

                return JsonConvert.SerializeObject(result);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="OrganisationId"></param>
        /// <param name="currentDate"></param>
        /// <returns></returns>
        public async Task<string> GetBenchmarkMetrics(string OrganisationId, DateTime? currentDate)
        {
            using (IDbConnection connection = GetConnection())
            {
                var @params = new { currentDate, OrganisationId };

                string query = @"SELECT * FROM [invp].[GetBenchmarkMetrics](@currentDate, @OrganisationId) ORDER BY  ProductID, BaseCurrency, MetricCode,[Date]";

                connection.Open();

                var result = (await connection.QueryAsync(query, @params, commandTimeout: _commandTimeout))
                            .GroupBy(r => new { r.ProductID, r.DisplayName, r.BaseCurrency })
                            .Select(g => new
                            {
                                ProductID = g.Key.ProductID,
                                DisplayName = g.Key.DisplayName,
                                BaseCurrency = g.Key.BaseCurrency,
                                Metrics = g.GroupBy(p => new { p.MetricCode })
                                             .Select(row => new
                                             {
                                                 MetricCode = row.Key.MetricCode,
                                                 data = row.Select(entry => new
                                                 {
                                                     Date = entry.Date,
                                                     Value = entry.Value
                                                 })
                                             })
                            }).ToArray();
                return JsonConvert.SerializeObject(result);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="OrganisationId"></param>
        /// <param name="currentDate"></param>
        /// <param name="metricCode"></param>
        /// <returns></returns>
        public async Task<string> GetMonthlyBenchmarkMetrics(string OrganisationId, DateTime? currentDate, string metricCode)
        {
            using (IDbConnection connection = GetConnection())
            {
                var @params = new { currentDate, OrganisationId, metricCode };

                string query = @"SELECT * FROM [invp].[GetMonthlyBenchmarkMetrics](@currentDate, @OrganisationId, @metricCode) ORDER BY  ProductID, BaseCurrency, MetricCode, [Date]";

                connection.Open();

                var result = (await connection.QueryAsync(query, @params, commandTimeout: _commandTimeout))
                            .GroupBy(r => new { r.ProductID, r.DisplayName, r.BaseCurrency })
                            .Select(g => new
                            {
                                ProductID = g.Key.ProductID,
                                DisplayName = g.Key.DisplayName,
                                BaseCurrency = g.Key.BaseCurrency,
                                Metrics = g.GroupBy(p => new { p.MetricCode })
                                             .Select(row => new
                                             {
                                                 MetricCode = row.Key.MetricCode,
                                                 data = row.Select(entry => new
                                                 {
                                                     Date = entry.Date,
                                                     Value = entry.Value
                                                 })
                                             })
                            }).ToArray();
                return JsonConvert.SerializeObject(result);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="OrganisationId"></param>
        /// <param name="currentDate"></param>
        /// <param name="mainCurrency"></param>
        /// <param name="metricCode"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public async Task<string> GetProductMetricsTopDate(string OrganisationId, DateTime? currentDate, string mainCurrency, string metricCode, string status)
        {
            using (IDbConnection connection = GetConnection())
            {
                var @params = new { currentDate, OrganisationId, mainCurrency, metricCode, status };

                string query = @"SELECT * FROM [invp].[GetProductMetricsTopDate](@currentDate, @OrganisationId, @mainCurrency, @metricCode, @status) ORDER BY  ProductID, MetricCode, Date";

                connection.Open();

                var result = (await connection.QueryAsync(query, @params, commandTimeout: _commandTimeout))
                            .GroupBy(r => new { r.ProductID, r.DisplayName })
                            .Select(g => new
                            {
                                ProductID = g.Key.ProductID,
                                DisplayName = g.Key.DisplayName,
                                Metrics = g.GroupBy(p => new { p.MetricCode })
                                             .Select(row => new
                                             {
                                                 MetricCode = row.Key.MetricCode,
                                                 data = row.Select(entry => new
                                                 {
                                                     Date = entry.Date,
                                                     Value = entry.Value
                                                 })
                                             })
                            }).ToArray();
                return JsonConvert.SerializeObject(result);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="OrganisationId"></param>
        /// <param name="currentDate"></param>
        /// <param name="metricCode"></param>
        /// <returns></returns>
        public async Task<string> GetOrganizationProductsHistoricalMonthlyMetrics(string OrganisationId, DateTime? currentDate, string metricCode)
        {
            using (IDbConnection connection = GetConnection())
            {
                var @params = new { currentDate, OrganisationId, metricCode };

                string query = @"SELECT * FROM [invp].[GetOrganizationProductsHistoricalMonthlyMetrics](@currentDate, @OrganisationId, @metricCode) ORDER BY  ProductID, BaseCurrency, MetricCode, [Date]";

                connection.Open();

                var result = (await connection.QueryAsync(query, @params, commandTimeout: _commandTimeout))
                            .GroupBy(r => new { r.ProductID, r.DisplayName, r.BaseCurrency })
                            .Select(g => new
                            {
                                ProductID = g.Key.ProductID,
                                DisplayName = g.Key.DisplayName,
                                BaseCurrency = g.Key.BaseCurrency,
                                Metrics = g.GroupBy(p => new { p.MetricCode })
                                             .Select(row => new
                                             {
                                                 MetricCode = row.Key.MetricCode,
                                                 data = row.Select(entry => new
                                                 {
                                                     Date = entry.Date,
                                                     Value = entry.Value
                                                 })
                                             })
                            }).ToArray();
                return JsonConvert.SerializeObject(result);
            }
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="OrganisationId"></param>
        /// <param name="currentDate"></param>
        /// <param name="metricCode"></param>
        /// <returns></returns>
        public async Task<string> GetProductsHistoricalMonthlyMetrics(string OrganisationId, DateTime? currentDate, string metricCode)
        {
            using (IDbConnection connection = GetConnection())
            {
                var @params = new { currentDate, OrganisationId, metricCode };

                string query = @"SELECT * FROM [invp].[GetProductsHistoricalMonthlyMetrics](@currentDate, @OrganisationId, @metricCode) ORDER BY  ProductID, BaseCurrency, MetricCode, [Date]";

                connection.Open();

                var result = (await connection.QueryAsync(query, @params, commandTimeout: _commandTimeout))
                            .GroupBy(r => new { r.ProductID, r.DisplayName, r.BaseCurrency })
                            .Select(g => new
                            {
                                ProductID = g.Key.ProductID,
                                DisplayName = g.Key.DisplayName,
                                BaseCurrency = g.Key.BaseCurrency,
                                Metrics = g.GroupBy(p => new { p.MetricCode })
                                             .Select(row => new
                                             {
                                                 MetricCode = row.Key.MetricCode,
                                                 data = row.Select(entry => new
                                                 {
                                                     Date = entry.Date,
                                                     Value = entry.Value
                                                 })
                                             })
                            }).ToArray();
                return JsonConvert.SerializeObject(result);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="OrganisationId"></param>
        /// <param name="currentDate"></param>
        /// <param name="metricCode"></param>
        /// <param name="mainCurrency"></param>
        /// <param name="status"></param>        
        /// <returns></returns>
        public async Task<string> GetAllocationPortfolio(string OrganisationId, DateTime? currentDate, string metricCode, string mainCurrency, string status)
        {
            using (IDbConnection connection = GetConnection())
            {
                var @params = new { currentDate, OrganisationId, metricCode, mainCurrency, status };

                string query = @"EXEC [invp].[GetAllocationPortfolio] @currentDate, @OrganisationId, @metricCode, @mainCurrency, @status";

                connection.Open();

                var result = (await connection.QueryAsync(query, @params, commandTimeout: _commandTimeout))
                            .GroupBy(r => new { r.ProductID, r.DisplayName, r.BaseCurrency })
                            .Select(g => new
                            {
                                ProductID = g.Key.ProductID,
                                DisplayName = g.Key.DisplayName,
                                BaseCurrency = g.Key.BaseCurrency,
                                Metrics = g.GroupBy(p => new { p.MetricCode })
                                             .Select(row => new
                                             {
                                                 MetricCode = row.Key.MetricCode,
                                                 data = row.Select(entry => new
                                                 {
                                                     Date = entry.Date,
                                                     Value = entry.Value,
                                                     Percent = entry.Percent
                                                 })
                                             })
                            }).ToArray();
                return JsonConvert.SerializeObject(result);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="OrganisationId"></param>
        /// <param name="currentDate"></param>
        /// <param name="metricCode"></param>
        /// <param name="mainCurrency"></param>
        /// <param name="status"></param>        
        /// <returns></returns>
        public async Task<string> GetAllocationLiquidity(string OrganisationId, DateTime? currentDate, string metricCode, string mainCurrency, string status)
        {
            using (IDbConnection connection = GetConnection())
            {
                var @params = new { currentDate, OrganisationId, metricCode, mainCurrency, status };

                string query = @"EXEC [invp].[GetAllocationLiquidity] @currentDate, @OrganisationId, @metricCode, @mainCurrency, @status";

                connection.Open();

                var result = (await connection.QueryAsync(query, @params, commandTimeout: _commandTimeout))
                            .GroupBy(r => new { r.Liquidity, r.MetricCode })
                            .Select(g => new
                            {
                                Liquidity = g.Key.Liquidity,
                                Metrics = g.GroupBy(p => new { p.MetricCode })
                                             .Select(row => new
                                             {
                                                 MetricCode = row.Key.MetricCode,
                                                 data = row.Select(entry => new
                                                 {
                                                     Value = entry.Value,
                                                     Percent = entry.Percent
                                                 })
                                             })
                            }).ToArray();
                return JsonConvert.SerializeObject(result);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="OrganisationId"></param>
        /// <param name="currentDate"></param>
        /// <param name="metricCode"></param>
        /// <param name="status"></param>        
        /// <returns></returns>
        public async Task<string> GetAllocationCurrency(string OrganisationId, DateTime? currentDate, string metricCode, string mainCurrency, string status )
        {
            using (IDbConnection connection = GetConnection())
            {                
                var @params = new { currentDate, OrganisationId, metricCode, mainCurrency, status };
                
                string query = @"EXEC [invp].[GetAllocationCurrency] @currentDate, @OrganisationId, @metricCode, @mainCurrency, @status";

                connection.Open();

                var result = (await connection.QueryAsync(query, @params, commandTimeout: _commandTimeout))
                            .GroupBy(r => new { r.BaseCurrency, r.MetricCode })
                            .Select(g => new
                            {
                                Currency = g.Key.BaseCurrency,
                                Metrics = g.GroupBy(p => new { p.MetricCode })
                                             .Select(row => new
                                             {
                                                 MetricCode = row.Key.MetricCode,
                                                 data = row.Select(entry => new
                                                 {
                                                     Value   = entry.Value,
                                                     Percent = entry.Percent,
                                                     Info    = entry.ValueInfo
                                                 })
                                             })
                            }).ToArray();
                return JsonConvert.SerializeObject(result);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="OrganisationId"></param>
        /// <param name="currentDate"></param>
        /// <param name="metricCode"></param>
        /// <param name="mainCurrency"></param>
        /// <param name="status"></param>        
        /// <returns></returns>
        public async Task<string> GetAllocationAssetClass(string OrganisationId, DateTime? currentDate, string metricCode, string mainCurrency, string status)
        {
            using (IDbConnection connection = GetConnection())
            {
                var @params = new { currentDate, OrganisationId, metricCode, mainCurrency, status };

                string query = @"EXEC [invp].[GetAllocationAssetClass] @currentDate, @OrganisationId, @metricCode, @mainCurrency, @status";

                connection.Open();

                var result = (await connection.QueryAsync(query, @params, commandTimeout: _commandTimeout))
                            .GroupBy(r => new { r.AssetClass, r.MetricCode })
                            .Select(g => new
                            {
                                AssetClass = g.Key.AssetClass,
                                Metrics = g.GroupBy(p => new { p.MetricCode })
                                             .Select(row => new
                                             {
                                                 MetricCode = row.Key.MetricCode,
                                                 data = row.Select(entry => new
                                                 {
                                                     Value = entry.Value,
                                                     Percent = entry.Percent
                                                 })
                                             })
                            }).ToArray();
                return JsonConvert.SerializeObject(result);
            }
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="OrganisationId"></param>
        /// <param name="currentDate"></param>
        /// <param name="productID"></param>
        /// <param name="metricCode"></param>
        /// <param name="currency"></param>
        /// <returns></returns>
        public async Task<string> GetDetailedPerformanceProductGeneralInfo(string OrganisationId, DateTime? currentDate, string productID, string metricCode, string currency)
        {
            using (IDbConnection connection = GetConnection())
            {
                var @params = new { currentDate, OrganisationId, productID, currency, metricCode };

                string query = @"EXEC [invp].[GetDetailedPerformanceProductGeneralInfo] @currentDate, @OrganisationId, @productID, @currency, @metricCode";

                connection.Open();

                var result = (await connection.QueryAsync(query, @params, commandTimeout: _commandTimeout))
                           .GroupBy(r => new { r.ProductID, r.InvestmentDate, r.InvestmentAmount, r.LastTransactionDate, r.Status })
                           .Select(g => new
                           {
                               ProductID = g.Key.ProductID,
                               InvestmentDate = g.Key.InvestmentDate,
                               InvestmentAmount = g.Key.InvestmentAmount,
                               LastTransactionDate = g.Key.LastTransactionDate,
                               Status = g.Key.Status,
                               Metrics = g.GroupBy(p => new { p.MetricCode })
                                            .Select(row => new
                                            {
                                                MetricCode = row.Key.MetricCode,
                                                data = row.Select(entry => new
                                                {
                                                    Date = entry.Date,
                                                    Value = entry.Value
                                                })
                                            })
                           }).ToArray();
                return JsonConvert.SerializeObject(result);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="OrganisationId"></param>
        /// <param name="currentDate"></param>
        /// <param name="productID"></param>
        /// <param name="metricCode"></param>
        /// <param name="currency"></param>
        /// <returns></returns>
        public async Task<string> GetDetaliedPerformanceProductMetric(string OrganisationId, DateTime? currentDate, string productID, string metricCode, string currency)
        {
            using (IDbConnection connection = GetConnection())
            {
                var @params = new { currentDate, OrganisationId, productID, currency, metricCode };

                string query = @"EXEC [invp].[GetDetaliedPerformanceProductMetric] @currentDate, @OrganisationId, @productID, @currency, @metricCode";

                connection.Open();

                var result = (await connection.QueryAsync(query, @params, commandTimeout: _commandTimeout))
                           .GroupBy(r => new { r.ProductID })
                           .Select(g => new
                           {
                               ProductID = g.Key.ProductID,
                               Metrics = g.GroupBy(p => new { p.MetricCode })
                                            .Select(row => new
                                            {
                                                MetricCode = row.Key.MetricCode,
                                                data = row.Select(entry => new
                                                {
                                                    Date = entry.Date,
                                                    Value = entry.Value
                                                })
                                            })
                           }).ToArray();
                return JsonConvert.SerializeObject(result);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="OrganisationId"></param>
        /// <param name="productID"></param>
        /// <returns></returns>
        public async Task<string> GetFactsheetProductGeneralInfo(string OrganisationId, string productID)
        {
            using (IDbConnection connection = GetConnection())
            {
                var @params = new { OrganisationId, productID };

                string query = @"EXEC [invp].[GetFactsheetProductGeneralInfo] @OrganisationId, @productID";

                connection.Open();

                var result = await connection.QueryAsync(query, @params, commandTimeout: _commandTimeout);
                return JsonConvert.SerializeObject(result);
            }
        }


        /// <summary>
        /// 
        /// </summary>        
        /// <param name="categoryCode"></param>
        /// <returns></returns>
        public async Task<string> GetFactsheetPortfolioGeneralInfo(string categoryCode)
        {
            using (IDbConnection connection = GetConnection())
            {
                var @params = new { categoryCode };

                string query = @"SELECT * FROM [invp].[GetFactsheetPortfolioGeneralInfo] (@categoryCode) ";

                connection.Open();

                var result = await connection.QueryAsync(query, @params, commandTimeout: _commandTimeout);
                return JsonConvert.SerializeObject(result);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="OrganisationId"></param>
        /// <param name="productID"></param>
        /// <returns></returns>
        public async Task<string> GetFactsheetProductDescription(string OrganisationId, string productID)
        {
            using (IDbConnection connection = GetConnection())
            {
                var @params = new { OrganisationId, productID };

                string query = @"EXEC [invp].[GetFactsheetProductGeneralInfo] @OrganisationId, @productID";

                connection.Open();

                var result = (await connection.QueryAsync(query, @params, commandTimeout: _commandTimeout))
                             .Select(g => new
                             {
                                 ProductID = g.ProductID,
                                 Strategy = g.Strategy
                             }).ToArray();

                return JsonConvert.SerializeObject(result);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="OrganisationId"></param>
        /// <param name="currentDate"></param>
        /// <returns></returns>
        public async Task<string> GetActiveProductsTransactionsAndValuation(string OrganisationId, DateTime? currentDate)
        {
            using (IDbConnection connection = GetConnection())
            {
                var @params = new { OrganisationId, currentDate };

                string query = @"SELECT * FROM [invp].[GetActiveProductsTransactionsAndValuation](@OrganisationId, @currentDate) ORDER BY Product, Date DESC";

                connection.Open();

                var result = (await connection.QueryAsync(query, @params, commandTimeout: _commandTimeout))
                            .GroupBy(r => new { r.Product, r.AssetClassID, r.AssetClassName, r.ITDPL, r.YTDPL, r.GrossRoR })
                            .Select(g => new
                            {
                                Product = g.Key.Product,
                                AssetClassID = g.Key.AssetClassID,
                                AssetClassName = g.Key.AssetClassName,
                                ITDPL = g.Key.ITDPL,
                                YTDPL = g.Key.YTDPL,
                                GrossRoR = g.Key.GrossRoR,
                                Transactions = g.Select(entry => new
                                {
                                    Date = entry.Date,
                                    Transaction = entry.Transaction,
                                    NumberOfUnits = entry.NumberOfUnits,
                                    Price = entry.Price,
                                    BaseCurrency = entry.BaseCurrency,
                                    TotalAmountBaseCurrency = entry.TotalAmountBaseCurrency,
                                    FX = entry.FX,
                                    TotalAmountMainCurrency = entry.TotalAmountMainCurrency
                                })
                            }).ToArray();

                return JsonConvert.SerializeObject(result);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="OrganisationId"></param>
        /// <returns></returns>
        public async Task<string> GetClosedProductsTransactions(string OrganisationId)
        {
            using (IDbConnection connection = GetConnection())
            {
                var @params = new { OrganisationId };

                string query = @"SELECT * FROM [invp].[GetClosedProductsTransactions](@OrganisationId) ORDER BY Product, Date DESC";

                connection.Open();

                var result = (await connection.QueryAsync(query, @params, commandTimeout: _commandTimeout))
                            .GroupBy(r => new { r.Product, r.AssetClassID, r.AssetClassName, r.ITDPL, r.YTDPL, r.GrossRoR })
                            .Select(g => new
                            {
                                Product        = g.Key.Product,
                                AssetClassID   = g.Key.AssetClassID,
                                AssetClassName = g.Key.AssetClassName,
                                ITDPL          = g.Key.ITDPL,
                                YTDPL          = g.Key.YTDPL,
                                GrossRoR       = g.Key.GrossRoR,
                                Transactions = g.Select(entry => new
                                {
                                    Date = entry.Date,
                                    Transaction = entry.Transaction,
                                    NumberOfUnits = entry.NumberOfUnits,
                                    Price = entry.Price,
                                    BaseCurrency = entry.BaseCurrency,
                                    TotalAmountBaseCurrency = entry.TotalAmountBaseCurrency,
                                    FX = entry.FX,
                                    TotalAmountMainCurrency = entry.TotalAmountMainCurrency
                                })
                            }).ToArray();

                return JsonConvert.SerializeObject(result);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="OrganisationId"></param>
        /// <param name="currentDate"></param>
        /// <returns></returns>
        public async Task<string> GetCapitalMovements(string OrganisationId, DateTime? currentDate)
        {
            using (IDbConnection connection = GetConnection())
            {
                var @params = new { OrganisationId, currentDate };

                string query = @"SELECT * FROM [invp].[GetCapitalMovements] (@currentDate, @OrganisationId)";

                connection.Open();

                var result = (await connection.QueryAsync(query, @params, commandTimeout: _commandTimeout))
                             .GroupBy(r => new { r.ProductID, r.DisplayName })
                             .Select(g => new
                             {
                                 Product = g.Key.ProductID,
                                 DisplayName = g.Key.DisplayName,
                                 Transactions = g.Select(entry => new
                                 {
                                     CapitalTransactionName = entry.CapitalTransactionTypeName,
                                     Amount = entry.Amount,
                                     Currency = entry.Currency,
                                     FX = entry.FXRate,
                                     Date = entry.Date
                                 })
                             }).ToArray();

                return JsonConvert.SerializeObject(result);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="OrganisationId"></param>
        /// <param name="currentDate"></param>
        /// <returns></returns>
        public async Task<string> GetCashBalanceByAcount(string OrganisationId, DateTime? currentDate)
        {
            using (IDbConnection connection = GetConnection())
            {
                var @params = new { OrganisationId, currentDate };

                string query = @"SELECT ProductID
                                       ,DisplayName
	                                   ,BaseCurrency
	                                   ,BalanceBaseCurrency
	                                   ,FX
	                                   ,(SELECT TOP(1) ActiveInvestmentMarketValue FROM [invp].[GetActiveProductsTotalNAV] (@currentDate, @OrganisationId)) ActiveProductsTotalNAV
                                 FROM [invp].[GetCashBalanceByAcount]  (@currentDate, @OrganisationId)";

                connection.Open();

                var result = (await connection.QueryAsync(query, @params, commandTimeout: _commandTimeout))
                             .GroupBy(r => new { r.ActiveProductsTotalNAV })
                             .Select(g => new
                             {
                                 ActiveProductsTotalNAV = g.Key.ActiveProductsTotalNAV,
                                 CashBalanceByAcount = g.Select(entry => new
                                 {
                                     ProductID = entry.ProductID,
                                     DisplayName = entry.DisplayName,
                                     BaseCurrency = entry.BaseCurrency,
                                     BalanceBaseCurrency = entry.BalanceBaseCurrency,
                                     FX = entry.FX
                                 })
                             }).ToArray();

                return JsonConvert.SerializeObject(result);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="OrganisationId"></param>
        /// <param name="currentDate"></param>
        /// <returns></returns>
        public async Task<string> GetITDPLCapitalBalance(string OrganisationId, DateTime? currentDate)
        {
            using (IDbConnection connection = GetConnection())
            {
                var @params = new { OrganisationId, currentDate };

                string query = @"SELECT * FROM [invp].[GetITDPLCapitalBalance] (@OrganisationId, @currentDate)  ORDER BY DisplayName";                              

                connection.Open();

                var result = (await connection.QueryAsync(query, @params, commandTimeout: _commandTimeout))
                             .GroupBy(r => new { r.CapitalBalance, r.FirstTransactionDate })
                             .Select(g => new
                             {
                                 CapitalBalance = g.Key.CapitalBalance,
                                 InitialDate = g.Key.FirstTransactionDate,
                                 Products = g.Select(entry => new
                                 {
                                     DisplayName = entry.DisplayName,
                                     ITDPL_PreFX = entry.ITDPL_PreFX,
                                     PerformanceAttribution = entry.PerformanceAttribution,
                                     ITDPL_FX = entry.ITDPL_FX,
                                     FXAttribution = entry.FXAttribution
                                 })
                             }).ToArray();

                return JsonConvert.SerializeObject(result);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="OrganisationId"></param>
        /// <param name="currentDate"></param>
        /// <param name="metricCode"></param>
        /// <param name="currency"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        public async Task<string> GetPorfolioMetricsTopDate(string OrganisationId, DateTime? currentDate, string metricCode, string currency, string category)
        {
            using (IDbConnection connection = GetConnection())
            {
                var @params = new { OrganisationId, currentDate, metricCode, currency, category };

                string query = @"SELECT *  FROM [invpder].[GetPorfolioMetricsTopDate](@currentDate, @organisationId, @currency, @metricCode, @category) ORDER BY [Date]";

                connection.Open();

                var result = (await connection.QueryAsync(query, @params, commandTimeout: _commandTimeout))
                             .GroupBy(r => new { r.OrganizationID, r.OrganizationName, r.PortfolioCategoryID, r.PortfolioCategory })
                             .Select(g => new
                             {
                                 OrganizationID = g.Key.OrganizationID,
                                 OrganizationName = g.Key.OrganizationName,
                                 PortfolioCategoryID = g.Key.PortfolioCategoryID,
                                 PortfolioCategory = g.Key.PortfolioCategory,
                                 Metrics = g.GroupBy(p => new { p.MetricCode })
                                            .Select(row => new
                                            {
                                                MetricCode = row.Key.MetricCode,
                                                data = row.Select(entry => new
                                                {
                                                    Date = entry.Date,
                                                    Value = entry.Value
                                                })
                                            })
                             }).ToArray();

                return JsonConvert.SerializeObject(result);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="OrganisationId"></param>
        /// <param name="currentDate"></param>
        /// <param name="metricCode"></param>        
        /// <param name="category"></param>
        /// <returns></returns>
        public async Task<string> GetPortfolioHistoricalMonthlyMetrics(string OrganisationId, DateTime? currentDate, string metricCode, string category)
        {
            using (IDbConnection connection = GetConnection())
            {
                var @params = new { OrganisationId, currentDate, metricCode, category };

                string query = @"SELECT * FROM [invpder].[GetPortfolioHistoricalMonthlyMetrics](@currentDate, @organisationId, @metricCode, @category) ORDER BY MainCurrency,[Date]";

                connection.Open();

                var result = (await connection.QueryAsync(query, @params, commandTimeout: _commandTimeout))
                             .GroupBy(r => new { r.PortfolioCategoryID, r.PortfolioCategory, r.CategoryCode, r.BaseCurrency })
                             .Select(g => new
                             {                                 
                                 PortfolioCategoryID = g.Key.PortfolioCategoryID,
                                 PortfolioCategory   = g.Key.PortfolioCategory,
                                 CategoryCode        = g.Key.CategoryCode,
                                 BaseCurrency        = g.Key.BaseCurrency,
                                 Metrics = g.GroupBy(p => new { p.MetricCode })
                                            .Select(row => new
                                            {
                                                MetricCode = row.Key.MetricCode,
                                                Currencies = row.GroupBy(c => new { c.MainCurrency })
                                                             .Select(rowCurrency => new
                                                             {
                                                                 Currency = rowCurrency.Key.MainCurrency,
                                                                 data = rowCurrency.Select(entry => new
                                                                 {
                                                                     Date = entry.Date,
                                                                     Value = entry.Value
                                                                 })
                                                             })
                                            })
                             }).ToArray();

                return JsonConvert.SerializeObject(result);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="OrganisationId"></param>
        /// <param name="currentDate"></param>
        /// <param name="metricCode"></param>       
        /// <returns></returns>
        public async Task<string> GetPortfolioHistoricalMonthlyMetrics(string OrganisationId, DateTime? currentDate, string metricCode)
        {
            using (IDbConnection connection = GetConnection())
            {
                var @params = new { OrganisationId, currentDate, metricCode };

                string query = @"SELECT * FROM [invpder].[GetPortfolioHistoricalMonthlyMetrics](@currentDate, @organisationId, @metricCode, DEFAULT) ORDER BY MainCurrency,[Date]";

                connection.Open();

                var result = (await connection.QueryAsync(query, @params, commandTimeout: _commandTimeout))
                             .GroupBy(r => new { r.PortfolioCategoryID, r.PortfolioCategory, r.CategoryCode, r.BaseCurrency })
                             .Select(g => new
                             {                                 
                                 PortfolioCategoryID = g.Key.PortfolioCategoryID,
                                 PortfolioCategory   = g.Key.PortfolioCategory,
                                 CategoryCode        = g.Key.CategoryCode,
                                 BaseCurrency        = g.Key.BaseCurrency,
                                 Metrics = g.GroupBy(p => new { p.MetricCode })
                                            .Select(row => new
                                            {
                                                MetricCode = row.Key.MetricCode,
                                                Currencies = row.GroupBy(c => new { c.MainCurrency })
                                                             .Select(rowCurrency => new
                                                             {
                                                                 Currency = rowCurrency.Key.MainCurrency,
                                                                 data = rowCurrency.Select(entry => new
                                                                 {
                                                                     Date = entry.Date,
                                                                     Value = entry.Value
                                                                 })
                                                             })

                                            })
                             }).ToArray();

                return JsonConvert.SerializeObject(result);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="OrganisationId"></param>      
        /// <param name="metricCode"></param>
        /// <param name="currency"></param>  
        /// <param name="currentDate"></param>  
        /// <param name="categoryCode"></param>  
        /// <returns></returns>
        public async Task<string> GetSummarizedProductsMetricsByPortfolio(string OrganisationId, string metricCode, string currency, DateTime? currentDate, string categoryCode)
        {
            using (IDbConnection connection = GetConnection())
            {
                var @params = new { OrganisationId, metricCode, currency, currentDate, categoryCode };

                string query = @"SELECT SUM(VALUE) [VALUE] FROM [invp].[GetSummarizedProductsMetricsByPortfolio] (@metricCode, @OrganisationId, @currency, @currentDate, @categoryCode)";

                connection.Open();

                var result = (await connection.QueryAsync(query, @params, commandTimeout: _commandTimeout)).SingleOrDefault();
                return JsonConvert.SerializeObject(result);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="OrganisationId"></param>
        /// <param name="currentDate"></param>       
        /// <param name="metricCode"></param>
        /// <param name="currency"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        public async Task<string> GetDetailedPerformancePortfolioGeneralInfo(string OrganisationId, DateTime? currentDate, string metricCode, string currency, string category)
        {
            using (IDbConnection connection = GetConnection())
            {
                var @params = new { currentDate, OrganisationId, currency, metricCode, category };

                string query = @"SELECT  OrganizationID
                                        ,MetricCode
	                                    ,[Date]
	                                    ,[Value]                                        
	                                    ,ROUND((SELECT TOP(1) CapitalBalance FROM [invp].[GetCapitalBalance] (@currentDate, @OrganisationId)),4)   InvestmentAmount
	                                    ,(SELECT TOP(1) FirstTransactionDate FROM [invp].[GetCapitalBalance] (@currentDate, @OrganisationId))      InvestmentDate
                                 FROM [invpder].[GetPorfolioMetricsTopDate] (@currentDate, @OrganisationId, @currency, @metricCode, @category)    ";

                connection.Open();

                var result = (await connection.QueryAsync(query, @params, commandTimeout: _commandTimeout))
                           .GroupBy(r => new { r.OrganizationID, r.InvestmentDate, r.InvestmentAmount })
                           .Select(g => new
                           {
                               OrganizationID = g.Key.OrganizationID,
                               InvestmentDate = g.Key.InvestmentDate,
                               InvestmentAmount = g.Key.InvestmentAmount,
                               Metrics = g.GroupBy(p => new { p.MetricCode })
                                            .Select(row => new
                                            {
                                                MetricCode = row.Key.MetricCode,
                                                data = row.Select(entry => new
                                                {
                                                    Date = entry.Date,
                                                    Value = entry.Value
                                                })
                                            })
                           }).ToArray();
                return JsonConvert.SerializeObject(result);
            }
        }
    
        /// <summary>
        /// 
        /// </summary>
        /// <param name="OrganisationId"></param>
        /// <param name="currentDate"></param>       
        /// <param name="metricCode"></param>        
        /// <returns></returns>
        public async Task<string> GetTopDateMetricsByProduct(string OrganisationId, DateTime? currentDate, string metricCode)
        {
            using (IDbConnection connection = GetConnection())
            {
                var @params = new { OrganisationId, currentDate, metricCode };

                string query = @"SELECT * FROM [invp].[GetTopDateMetricsByProduct] (@currentDate, @OrganisationId, @metricCode) ORDER BY DisplayName";

                connection.Open();

                var result = (await connection.QueryAsync(query, @params, commandTimeout: _commandTimeout))
                             .GroupBy(r => new { r.ProductID, r.DisplayName, r.BaseCurrency, r.InitialDate, r.EndDate, r.PricingDate })
                             .Select(g => new
                             {
                                 ProductID    = g.Key.ProductID,
                                 DisplayName  = g.Key.DisplayName,
                                 BaseCurrency = g.Key.BaseCurrency,
                                 InitialDate  = g.Key.InitialDate,
                                 EndDate      = g.Key.EndDate,
                                 PricingDate  = g.Key.PricingDate,
                                 Metrics = g.GroupBy(p => new { p.MetricCode })
                                            .Select(row => new
                                            {
                                                MetricCode = row.Key.MetricCode,
                                                Currencies = row.GroupBy(c => new { c.MainCurrency })
                                                             .Select(rowCurrency => new
                                                             {
                                                                 Currency = rowCurrency.Key.MainCurrency,
                                                                 data = rowCurrency.Select(entry => new
                                                                 {
                                                                     Date = entry.Date,
                                                                     Value = entry.Value
                                                                 })
                                                             })
                                            })
                             }).ToArray();

                return JsonConvert.SerializeObject(result);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="OrganisationId"></param>
        /// <param name="currentDate"></param>       
        /// <param name="metricCode"></param>        
        /// <returns></returns>
        public async Task<string> GetTopDateMetricsByPortfolio(string OrganisationId, DateTime? currentDate, string metricCode)
        {
            using (IDbConnection connection = GetConnection())
            {
                var @params = new { OrganisationId, currentDate, metricCode };

                string query = @"SELECT * FROM [invpder].[GetTopDateMetricsByPortfolio] (@currentDate, @OrganisationId, @metricCode)";

                connection.Open();

                var result = (await connection.QueryAsync(query, @params, commandTimeout: _commandTimeout))
                             .GroupBy(r => new { r.PortfolioCategoryID, r.PortfolioCategory, r.CategoryCode, r.BaseCurrency, r.PricingDate })
                             .Select(g => new
                             {
                                 PortfolioCategoryID = g.Key.PortfolioCategoryID,
                                 PortfolioCategory   = g.Key.PortfolioCategory,
                                 CategoryCode        = g.Key.CategoryCode,
                                 BaseCurrency        = g.Key.BaseCurrency,
                                 PricingDate         = g.Key.PricingDate,
                                 Metrics = g.GroupBy(p => new { p.MetricCode })
                                            .Select(row => new
                                            {
                                                MetricCode = row.Key.MetricCode,
                                                Currencies = row.GroupBy(c => new { c.MainCurrency })
                                                             .Select(rowCurrency => new
                                                             {
                                                                 Currency = rowCurrency.Key.MainCurrency,
                                                                 data = rowCurrency.Select(entry => new
                                                                 {
                                                                     Date = entry.Date,
                                                                     Value = entry.Value
                                                                 })
                                                             })
                                            })
                             }).ToArray();

                return JsonConvert.SerializeObject(result);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="OrganisationId"></param>
        /// <param name="currentDate"></param>       
        /// <param name="metricCode"></param>        
        /// <returns></returns>
        public async Task<string> GetHistoricalMetricsByProduct(string OrganisationId, DateTime? currentDate, string metricCode)
        {
            using (IDbConnection connection = GetConnection())
            {
                var @params = new { currentDate, OrganisationId, metricCode };

                string query = @"SELECT * FROM [invp].[GetHistoricalMetricsByProduct] (@currentDate, @OrganisationId, @metricCode)  ORDER BY DisplayName, Type, MetricCode, MainCurrency, Date";

                connection.Open();

                var result = (await connection.QueryAsync(query, @params, commandTimeout: _commandTimeout))
                           .GroupBy(r => new { r.ProductID, r.DisplayName, r.BaseCurrency })
                           .Select(g => new
                           {
                               g.Key.ProductID,
                               g.Key.DisplayName,
                               g.Key.BaseCurrency,
                               Types = g.GroupBy(t => new { t.Type })
                                      .Select(rowType => new
                                      {
                                          Type = rowType.Key.Type,
                                          Metrics = rowType.GroupBy(p => new { p.MetricCode })
                                                   .Select(row => new
                                                   {
                                                        MetricCode = row.Key.MetricCode,
                                                        Currencies = row.GroupBy(c => new { c.MainCurrency })
                                                                     .Select(rowCurrency => new
                                                                     {
                                                                         Currency = rowCurrency.Key.MainCurrency,
                                                                         data = rowCurrency.Select(entry => new
                                                                         {
                                                                             Date = entry.Date,
                                                                             Value = entry.Value
                                                                         })
                                                                      })
                                                   })
                                      })
                           }).ToArray();
                return JsonConvert.SerializeObject(result);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="OrganisationId"></param>
        /// <param name="mainCurrency"></param>
        /// <param name="currentDate"></param>  
        /// <returns></returns>
        public async Task<string> GetMonthlyPerformanceAttribution(string OrganisationId, string mainCurrency,  DateTime? currentDate)
        {
            using (IDbConnection connection = GetConnection())
            {
                var @params = new { OrganisationId, currentDate, mainCurrency };

                string query = @"SELECT *  FROM [invp].[GetMonthlyPerformanceAttribution](@OrganisationId, @currentDate, @mainCurrency)  ORDER BY Date, DisplayName";               

                connection.Open();

                var result = (await connection.QueryAsync(query, @params, commandTimeout: _commandTimeout))
                              .GroupBy(r => new { r.Date, r.CapitalBalance })
                              .Select(g => new
                              {
                                  g.Key.Date,
                                  g.Key.CapitalBalance,
                                  Products = g.GroupBy(t => new { t.DisplayName })
                                             .Select(rowProduct => new
                                             {
                                                 Product = rowProduct.Key.DisplayName,
                                                 data = rowProduct.Select(entry => new
                                                 {
                                                     entry.MTDPL_PreFX,
                                                     entry.PerformanceAttribution,
                                                     entry.MTDPL_FX,
                                                     entry.FXAttribution
                                                 })

                                             })
                              }).ToArray();

                return JsonConvert.SerializeObject(result);
            }
        }


            #region Private Property

            private IDbConnection GetConnection() => new SqlConnection(_connectionString);

        #endregion

    }
}
