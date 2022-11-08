﻿using System.Collections.Generic;

using CareJobApplicationv2.ViewModels;

namespace CareJobApplicationv2.Areas.Admin.ViewModels
{
    public class UserRolesViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }

        public List<RoleViewModel> Roles { get; set; } 
    }
}
