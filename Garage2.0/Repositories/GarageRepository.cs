using Garage2._0.DataAccessLayer;
using Garage2._0.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
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
            List<Vehicle> vehicles = Context.Vehicles.Where(v=> v.Checkedin).OrderBy(v => v.VehicleType).ThenByDescending(v => v.InTime).ToList();

            return vehicles;
        }

        public IEnumerable<Vehicle> GetVehicles(VehicleQuery target)
        {
            IEnumerable<Vehicle> svar = Context.Vehicles.Where(v =>
                                            v.Color.Contains(target.SearchColor) &&
                                            v.Owner.Contains(target.SearchOwner) &&
                                            v.RegNr.Contains(target.SearchRegNr) &&
                                            target.VehicleType.Any(t => t.Equals(v.VehicleType.ToString())) &&
                                            (v.InTime == null ? default(DateTime) <= target.InTimeFilter : v.InTime >= target.InTimeFilter)  &&
                                            (v.OutTime == null ? new DateTime(9999, 12, 12) >= target.OutTimeFilter : v.OutTime <= target.OutTimeFilter) && 
                                            (v.Checkedin == target.Checkedin ||
                                            v.Checkedin != target.Checkedout)
                                          ).OrderBy(v => v.VehicleType).ThenByDescending(v => v.InTime);
            return svar;
        }

        public Vehicle GetVehicle(int id)
        {
            return Context.Vehicles.Find(id);
        }

        public Vehicle GetVehicle(Vehicle V)
        {
            return Context.Vehicles.Single(v => v == V);
        }

        public dynamic AddVehicle(Vehicle newVehicle)
        {
            //Vehicle oldVehicle = Context.Vehicles.First(v => v.RegNr == newVehicle.RegNr);
            //if(oldVehicle != null)
            //{
            //    oldVehicle = newVehicle;
            //    /// All properties except RegNr
            //}
            //else
            //{

            bool Exist = GetVehicles().ToList().Exists(v => v.RegNr == newVehicle.RegNr);

            if (Exist)
            {
                return new { exist = Exist, Id = getVehicleByRegNr(newVehicle.RegNr).Id };
            }
            else
            {
                Context.Vehicles.Add(newVehicle);
                Context.SaveChanges();
                return new { exist = Exist, Id = getVehicleByRegNr(newVehicle.RegNr).Id };
            }

            //}
            
        }

        public Vehicle getVehicleByRegNr(string regnr)
        {
            return Context.Vehicles.First(v => v.RegNr == regnr);
        }

        public bool RemoveVehicle(int id)
        {
            Vehicle VToRemove = Context.Vehicles.Find(id);
            if (VToRemove != null)
            {
                Context.Vehicles.Remove(VToRemove);
                Context.SaveChanges();
                return true;
            }
            return false;
        }

        public void EditVehicle(Vehicle newVehicle)
        {
            Vehicle v =  Context.Vehicles.Find(newVehicle.Id);

            //Context.Entry(v).State = EntityState.Modified;
            
            foreach (PropertyInfo p in typeof(Vehicle).GetProperties())
            {
                PropertyInfo vehicleproperty = typeof(Vehicle).GetProperty(p.Name);

                vehicleproperty.SetValue(v, vehicleproperty.GetValue(newVehicle));
            }
            
            //v = newVehicle;

            Context.SaveChanges();
        }

        public void CheckInVehicle(CheckInViewModel newVehicle)
        {
            Vehicle v = Context.Vehicles.Find(newVehicle.Id);

            //Context.Entry(v).State = EntityState.Modified;

            foreach (PropertyInfo p in typeof(Vehicle).GetProperties())
            {
                PropertyInfo vehicleproperty = typeof(Vehicle).GetProperty(p.Name);
                PropertyInfo newvehicleproperty = typeof(CheckInViewModel).GetProperty(p.Name);
                if(newvehicleproperty != null)
                    if (newvehicleproperty.GetValue(newVehicle) != null)
                        vehicleproperty.SetValue(v, newvehicleproperty.GetValue(newVehicle));
            }

            if (!newVehicle.InTime.HasValue) // server side check for inTime
                v.InTime = DateTime.Now;

            if (v.InTime >= (newVehicle.OutTime ?? default(DateTime)))
                v.OutTime = null;

            //Context.Vehicles.Find(newVehicle.Id) = v;

            Context.SaveChanges();
        }

        public bool CheckOutVehicle(int id)
        {
            Vehicle VToRemove = Context.Vehicles.Find(id);
            if (VToRemove != null)
            {
                VToRemove.Checkedin = false;
                VToRemove.OutTime = DateTime.Now;
                Context.SaveChanges();
                return true;
            }
            return false;
        }
        
    }
}