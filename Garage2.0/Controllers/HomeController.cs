﻿using Garage2._0.DataAccessLayer;
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
            if (ModelState.IsValid)
            {
                if (!newVehicle.InTime.HasValue) // server side check for inTime
                {
                    TempData["ErrorMessage"] = "Invalid Data!";
                }
                else
                {
                    Garage.AddVehicle(newVehicle);
                }
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("Checkin", newVehicle);
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
                return Json(new { message = "Success!" });
            }
            else
            {
                return Json(new { message = "Failed!" });
            }
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