using Garage2._0.DataAccessLayer;
using Garage2._0.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            if(newVehicle != null)
            {
                Context.Vehicles.Add(newVehicle);
                Context.SaveChanges();
            }
                
            //}
            
        }

        public bool RemoveVehicle(int id)
        {
            Vehicle VToRemove = Context.Vehicles.Find(id);
            if (VToRemove != null)
            {
                Context.Vehicles.Remove(Context.Vehicles.Find(id));
                Context.SaveChanges();
                return true;
            }
            return false;
        }

        public void EditVehicle(Vehicle newVehicle)
        {
            Context.Entry(newVehicle).State = EntityState.Modified;

            Context.SaveChanges();
        }

        public IEnumerable<Vehicle> FilterVehicle(string fordon)
        {
            Vehicles answer = new Vehicles();

            if ("Car" == fordon)
                answer = Vehicles.Car;
            else if ("Truck" == fordon)
                answer = Vehicles.Truck;
            else if ("Boat" == fordon)
                answer = Vehicles.Boat;
            else if ("Motorcycle" == fordon)
                answer = Vehicles.Motorcycle;

            IEnumerable<Vehicle> outanswer = from i in Context.Vehicles
                                        where i.VehicleType == answer
                                        select i;
            return outanswer;
        }

        public IEnumerable<Vehicle> FilterInDate(string indatum)
        {
            var tmp = DateTime.Today.AddDays(7);
            DateTime MinusSeven = DateTime.Today.AddDays(-7);

            IEnumerable<Vehicle> outanswer; // = new IEnumerable<Vehicle>();
            if (indatum == "Today")
            {
                outanswer = from i in Context.Vehicles
                            where i.InTime.Value.Date == DateTime.Today.Date
                            select i;
            }
            else if (indatum == "Week")
            {
                outanswer = from i in Context.Vehicles
                            where i.InTime.Value.AddDays(0) >= MinusSeven
                            select i;
            }
            else
            { 
            outanswer = from i in Context.Vehicles
                        where i.InTime.Value.AddDays(0) == DateTime.Today.AddDays(7)
                        select i;
            }
             return outanswer;
        }
    }
}