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
            if (Request.IsAjaxRequest())
            {
                return PartialView("VehiclesList", Garage.GetVehicles());
            }
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
            if (Request.IsAjaxRequest())
            {
                return PartialView("Checkin", new Vehicle());
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Checkin([Bind(Include ="RegNr, Owner, Color, VehicleType, NumberOfWheels, InTime, OutTime")]Vehicle newVehicle)
        {
            if (ModelState.IsValid && Request.IsAjaxRequest())
            {
                if (!newVehicle.InTime.HasValue) // server side check for inTime
                {
                    TempData["ErrorMessage"] = "Invalid Data!";
                    return PartialView("Checkin", newVehicle);
                }
                else
                {
                    Garage.AddVehicle(newVehicle);
                    return Json(new { type = true, message = string.Format("Vehicle {0} was checked in", newVehicle.RegNr) });
                }
            }
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
            if (Garage.RemoveVehicle(id))
            {
                return Json(new { type = true, message = "Vechicle was checkedout!" });
            }
            else
            {
                return Json(new { type = false, message = "Vechicle was not checkedout!" });
            }
        }

        public ActionResult Edit(int id)
        {
            return PartialView("Edit", Garage.GetVehicle(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include ="Id,RegNr, Owner, Color, VehicleType, NumberOfWheels, InTime, OutTime")]Vehicle newVehicle)
        {
            if (ModelState.IsValid && Request.IsAjaxRequest())
            {
                if (!newVehicle.InTime.HasValue) // server side check for inTime
                {
                    TempData["ErrorMessage"] = "Invalid Data!";
                    return PartialView("Edit", newVehicle);
                }
                else
                {
                    Garage.EditVehicle(newVehicle);
                    return Json(new { type = true, message = "Item Edited!" });
                }
            }
            
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            return View(Garage.GetVehicle(id));
        }
    }
}