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
            //Vehicle oldVehicle = Context.Vehicles.First(v => v.RegNr == newVehicle.RegNr);
            //if(oldVehicle != null)
            //{
            //    oldVehicle = newVehicle;
            //    /// All properties except RegNr
            //}
            //else
            //{
                Context.Vehicles.Add(newVehicle);
            //}
            Context.SaveChanges();
        }

        internal void RemoveVehicle(int id)
        {
            Context.Vehicles.Remove(Context.Vehicles.Find(id));
            Context.SaveChanges();
        }
    }
}