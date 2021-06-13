using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GetIT.Models.ViewModels
{
    public class CreateRole
    {
        [Required]
        public string RoleName { get; set; }

    }
}
