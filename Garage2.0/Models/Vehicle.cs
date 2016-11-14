using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Garage2._0.Models
{
    public class Vehicle
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Owner { get; set; }
        [Required]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "Registration Number must have exactly six characters")]
        public string RegNr { get; set; }
        [Required]
        public string Color { get; set; }
        [Required]
        [Range(0, 20, ErrorMessage = "Wrong number of wheels")]
        public int NumberOfWheels { get; set; }

        [Required]
        public Vehicles VehicleType { get; set; } 

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime? InTime { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? OutTime { get; set; }
    }

    public enum Vehicles
    {
        Car,
        Truck,
        Boat,
        Motorcycle
    }
    
}