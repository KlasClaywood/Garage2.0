using Garage2._0.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Garage2._0.DataAccessLayer
{
    public class GarageContext : DbContext
    {
        public DbSet<Vehicle> Vehicles { get; set; }
        public GarageContext() :base ("DefaultConnection")
        {
            
        }
    }
}