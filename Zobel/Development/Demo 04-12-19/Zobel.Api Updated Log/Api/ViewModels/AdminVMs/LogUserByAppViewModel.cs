using System;
using System.Collections.Generic;

namespace Api.ViewModels.IdentityVMs
{
    public class UserViewModel
    {
        public UserViewModel()
        {
            
        }

        public string Login { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Description { get; set; }

        public DateTime InitDate { get; set; }
        public bool Active { get; set; }
        public string Email { get; set; }
        public string CellPhone { get; set; }
        public string DeskPhone { get; set; }
        public bool Internal { get; set; }
        public string UserPicture { get; set; }
        public bool IsAdemoUser { get; set; }
        public int? ResourceId { get; set; }

    }
}
