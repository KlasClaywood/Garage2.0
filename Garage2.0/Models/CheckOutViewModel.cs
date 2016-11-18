using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Garage2._0.Models
{
    public class CheckOutViewModel
    {
        [Required]
        public Vehicle vehicle { get; set; }
    }
}