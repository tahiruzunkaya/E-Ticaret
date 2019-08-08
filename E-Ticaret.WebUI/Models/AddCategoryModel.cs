using ETicaret.WebUI.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ETicaret.WebUI.Models
{
    public class AddCategoryModel
    {
        [Required]
        public string CategoryName { get; set; }
    }
}
