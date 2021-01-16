using DhubSolutions.Common.Domain.Entities.Admin;
using DhubSolutions.Core.Application.Services;
using DhubSolutions.Core.Domain.Adapters;
using DhubSolutions.Core.Domain.Data;
using DhubSolutions.Reports.Domain.Entities.ReportManager;
using DhubSolutions.WealthReport.Application.Dtos;
using DhubSolutions.WealthReport.Application.Services.Base;
using DhubSolutions.WealthReport.Domain.Entities;
using DhubSolutions.WealthReport.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DhubSolutions.WealthReport.Application.Services
{
    public class SignableStatementService : ServiceMapper<SignableStatement>, ISignableStatementService
    {
        private readonly IStatementSignerRepository _statementSignerRepository;
        private readonly IStatementCategoryRepository _statementCategoryRepository;

        public SignableStatementService(
            IUnitOfWork unitOfWork,
            ITypeAdapter typeAdapter,
            ISignableStatementRepository repository,
            IStatementSignerRepository statementSignerRepository,
            IStatementCategoryRepository statementCategoryRepository)
            : base(unitOfWork, typeAdapter, repository)
        {
            _statementSignerRepository = statementSignerRepository;
            _statementCategoryRepository = statementCategoryRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="report"></param>
        /// <param name="organization"></param>
        /// <param name="user"></param>
        /// <param name="signableStatements"></param>
        public void AddOrUpdate(
            Report report,
            Organization organization,
            User user,
            ICollection<SignableStatementDto> signableStatements)
        {
            foreach (SignableStatementDto signableStatementDto in signableStatements)
            {
                StatementCategory statementCategory = _statementCategoryRepository.Get(
                                                                   category => category.StatementCategoryName == signableStatementDto.Name);
                if (statementCategory == null)
                    throw new InvalidOperationException($"{nameof(statementCategory)} not found");

                SignableStatement signableStatement = _repository.Get(
                                                                 statement => statement.OrganizationId == organization.Id &&
                                                                              statement.ReportTemplateId == report.TemplateId &&
                                                                              statement.StatementCategoryId == statementCategory.Id,
                                                                 asNoTracking: true,
                                                                 statement => statement.StatementSigners,
                                                                 statement => statement.StatementCategory);

                if (signableStatement == null)
                {
                    signableStatement = _repository.Create();
                    signableStatement.StatementCategoryId = statementCategory.Id;
                    signableStatement.ReportTemplateId = report.TemplateId;
                    signableStatement.OrganizationId = organization.Id;
                    signableStatement.Content = signableStatementDto.Content;

                    if (!string.IsNullOrEmpty(signableStatementDto.SignedBy))
                    {
                        StatementSigner statementSigner = new StatementSigner
                        {
                            SignableStatementId = signableStatement.Id,
                            SignedById = user.Id,
                            SignedDate = DateTime.Now
                        };

                        _statementSignerRepository.Add(statementSigner);
                    }

                    _repository.Add(signableStatement);
                }

                else if (!signableStatement.IsSignedOff())
                {
                    signableStatement.Content = signableStatementDto.Content;

                    if (!string.IsNullOrEmpty(signableStatementDto.SignedBy) &&
                        signableStatement.StatementSigners.All(
                            statementSigner => statementSigner.SignedById != user.Id))
                    {
                        StatementSigner statementSigner = new StatementSigner
                        {
                            SignableStatementId = signableStatement.Id,
                            SignedById = user.Id,
                            SignedDate = DateTime.Now
                        };

                        _statementSignerRepository.Add(statementSigner);
                    }

                    _repository.Update(signableStatement);

                }
            }
        }
    }
}
