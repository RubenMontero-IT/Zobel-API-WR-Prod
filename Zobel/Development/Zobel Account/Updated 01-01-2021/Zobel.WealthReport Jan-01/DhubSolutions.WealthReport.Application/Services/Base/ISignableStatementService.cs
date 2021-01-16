using DhubSolutions.Common.Domain.Entities.Admin;
using DhubSolutions.Core.Domain.Services;
using DhubSolutions.Reports.Domain.Entities.ReportManager;
using DhubSolutions.WealthReport.Application.Dtos;
using DhubSolutions.WealthReport.Domain.Entities;
using System.Collections.Generic;

namespace DhubSolutions.WealthReport.Application.Services.Base
{
    public interface ISignableStatementService : IServiceMapper<SignableStatement>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="report"></param>
        /// <param name="organization"></param>
        /// <param name="user"></param>
        /// <param name="signableStatements"></param>
        void AddOrUpdate(
            Report report,
            Organization organization,
            User user,
            ICollection<SignableStatementDto> signableStatements);
    }
}
