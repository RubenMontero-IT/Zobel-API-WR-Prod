using AutoMapper;
using DhubSolutions.Common.Domain.Entities.Admin;
using DhubSolutions.WealthReport.Api.ViewModels;
using DhubSolutions.WealthReport.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace DhubSolutions.WealthReport.Api.Mappings
{

    public class StatementSignerConverter : ITypeConverter<StatementSigner, StatementSignerVM>
    {
        private readonly UserManager<User> userManager;

        public StatementSignerConverter(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        public StatementSignerVM Convert(StatementSigner statementSigner, StatementSignerVM destination, ResolutionContext context)
        {
            User user = userManager.FindByIdAsync(statementSigner.SignedById).Result;

            return new StatementSignerVM
            {
                SignedBy = $"{ user}",
                SignedDate = statementSigner.SignedDate
            };
        }
    }

}
