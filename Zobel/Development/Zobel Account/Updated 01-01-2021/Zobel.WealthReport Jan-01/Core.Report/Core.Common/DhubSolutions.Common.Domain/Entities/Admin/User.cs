using DhubSolutions.Common.Domain.Entities.Application;
using DhubSolutions.Core.Domain.Entity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace DhubSolutions.Common.Domain.Entities.Admin
{
    public class User : IdentityUser, IEntity
    {
        public User() : base()
        {
            AppSettingsByUser = new HashSet<AppSettingsByUser>();
            DefaultRolePermission = new HashSet<DefaultRolePermission>();
            PasswordRequests = new HashSet<PasswordRequests>();
            UserRoleOrg = new HashSet<UserRoleOrg>();
        }
        public string Login { get; set; }
        public string Description { get; set; }
        public DateTime InitDate { get; set; }
        public bool Active { get; set; }
        public string DeskPhone { get; set; }
        public bool Internal { get; set; }
        public string UserPicture { get; set; }
        public bool IsAdemoUser { get; set; }
        public string ResourceId { get; set; }

        public Resource Resources { get; set; }
        public ICollection<DefaultRolePermission> DefaultRolePermission { get; set; }
        public ICollection<PasswordRequests> PasswordRequests { get; set; }
        public ICollection<UserRoleOrg> UserRoleOrg { get; set; }
        public ICollection<AppSettingsByUser> AppSettingsByUser { get; set; }

    }
}
