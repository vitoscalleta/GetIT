using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GetIT.Models.ViewModels
{
    public class EditRoleVM
    {
        public string RoleID { get; set; }

        public string RoleName { get; set; }

        public List<string> Users { get; set; }

    }
}
