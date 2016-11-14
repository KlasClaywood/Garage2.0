using Garage2._0.DataAccessLayer;
using Garage2._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Garage2._0.Repositories
{
    public class GarageRepository
    {
        private GarageContext Context { get; set; }

        public GarageRepository()
        {
            Context = new GarageContext();
        }

        public IEnumerable<Vehicle> GetVehicles()
        {
            return Context.Vehicles;
        }

        public IEnumerable<Vehicle> GetVehicles(VehicleQuery target)
        {
            IEnumerable<Vehicle> svar = Context.Vehicles.Where(v =>
                                          v.Color.Contains(target.SearchColor) &&
                                          v.Owner.Contains(target.SearchOwner) &&
                                          v.RegNr.Contains(target.SearchRegNr)
                                          );
            return svar;
        }

        public Vehicle GetVehicle(int id)
        {
            return Context.Vehicles.Find(id);
        }

        public void AddVehicle(Vehicle newVehicle)
        {
            Context.Vehicles.Add(newVehicle);
            Context.SaveChanges();
        }
    }
}