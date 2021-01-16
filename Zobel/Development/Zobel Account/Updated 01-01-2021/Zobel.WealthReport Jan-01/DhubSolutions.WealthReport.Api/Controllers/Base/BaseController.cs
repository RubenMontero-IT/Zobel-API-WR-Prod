using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using DhubSolutions.Common.Domain.Entities.Admin;
using DhubSolutions.Core.Domain.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DhubSolutions.WealthReport.Api.Controllers.Base
{
    public abstract class BaseController : ControllerBase
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly UserManager<User> _userManager;

        protected BaseController(IUnitOfWork unitOfWork, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        protected async Task<User> GetCurrentUser()
        {
            List<Claim> claims = new List<Claim>(GetCurrentUserClaims());
            string currentUserId = claims.Find(r => r.Type == "UserId").Value;
            return await _userManager.FindByIdAsync(currentUserId);
            //return await Task.Factory.StartNew(()
            //    => new User { Id = "ebac744c-e050-4262-8c05-36b4a63a509c" });
        }

        protected IEnumerable<Claim> GetCurrentUserClaims()
        {
            return HttpContext.User.Claims;
        }
    }
}