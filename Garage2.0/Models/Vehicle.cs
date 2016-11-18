using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Garage2._0.Models
{
    public class Vehicle
    {
        [Key]
        public int Id { get; set; }

        public string Owner { get; set; }

        public string RegNr { get; set; }

        public string Color { get; set; }

        public int NumberOfWheels { get; set; }

        public Vehicles VehicleType { get; set; } 

        public DateTime? InTime { get; set; }

        public DateTime? OutTime { get; set; }

        DateTime RentTill { get; set; }

        public bool Checkedin { get; set; }


    }

    public enum Vehicles
    {
        Car = 10,
        Truck = 20,
        Boat = 30,
        Motorcycle = 5
    }
    
}