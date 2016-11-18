using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Garage2._0.Models
{
    public class CheckInViewModel
    {
        [Key]
        public int Id { get; set; }

        public Vehicle vehicle { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? InTime { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? OutTime { get; set; }

        [Required]
        public bool Checkedin { get; set; }

    }
}