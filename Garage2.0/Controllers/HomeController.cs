using Garage2._0.DataAccessLayer;
using Garage2._0.Models;
using Garage2._0.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Garage2._0.Controllers
{
    public class HomeController : Controller
    {
        public GarageRepository Garage = new GarageRepository();

        public ActionResult Index()
        {
            return View(Garage.GetVehicles());
        }

        public ActionResult Index2()
        {
            return View(Garage.GetVehicles());
        }

        public ActionResult Search()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Search([Bind(Include ="SearchOwner, SearchRegNr, SearchColor")]VehicleQuery target)
        {
            if (target != null)
            {
                if (target.SearchColor == null)
                {
                    target.SearchColor = "";
                }
                if (target.SearchOwner == null)
                {
                    target.SearchOwner = "";
                }
                if (target.SearchRegNr == null)
                {
                    target.SearchRegNr = "";
                }
            }
            return View("Results", Garage.GetVehicles(target));
        }

        public ActionResult Results(VehicleQuery target)
        {
            return View(Garage.GetVehicles(target));
        }

        public ActionResult Checkin()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Checkin([Bind(Include ="RegNr, Owner, Color, VehicleType, NumberOfWheels, InTime, OutTime")]Vehicle newVehicle)
        {
            //newVehicle.InTime = DateTime.Now;
            Garage.AddVehicle(newVehicle);
            return RedirectToAction("Index");
        }

        public ActionResult Checkout(int? id)
        {
            
            return View(Garage.GetVehicle(id.Value));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Checkout(int id)
        {
            Garage.RemoveVehicle(id);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            return View(Garage.GetVehicle(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include ="Id,RegNr, Owner, Color, VehicleType, NumberOfWheels, InTime, OutTime")]Vehicle newVehicle)
        {
            Garage.EditVehicle(newVehicle);
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            return View(Garage.GetVehicle(id));
        }

        
        
    }
}