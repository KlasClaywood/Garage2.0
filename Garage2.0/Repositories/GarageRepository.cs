﻿using Garage2._0.DataAccessLayer;
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
                                          v.RegNr.Contains(target.SearchRegNr) &&
                                          target.VehicleType.Any(t => t.Equals(v.VehicleType.ToString())) &&
                                          v.InTime >= target.InTimeFilter
                                          );

            IEnumerable<Vehicle> svar1 = Context.Vehicles.Where(v =>
                                        v.Color.Contains(target.SearchColor) &&
                                        v.Owner.Contains(target.SearchOwner) &&
                                        v.RegNr.Contains(target.SearchRegNr) &&
                                        target.VehicleType.Any(t => t.Equals(v.VehicleType.ToString())) &&
                                        v.InTime >= target.InTimeFilter &&
                                        v.OutTime <= target.OutTimeFilter
                ).Union(svar);


            return svar1;
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
    }
}