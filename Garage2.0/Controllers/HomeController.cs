using Garage2._0.DataAccessLayer;
using Garage2._0.Models;
using Garage2._0.Repositories;
using System;
using System.Collections.Generic;
using System.Globalization;
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

        
        [ValidateAntiForgeryToken]
        public ActionResult Search([Bind(Include = "SearchOwner, SearchRegNr, SearchColor, VehicleType")]VehicleQuery target, string InTimeFilter, string OutTimeFilter)
        {
            //HttpContext.Request.InputStream.Position = 0;
            //var result = new System.IO.StreamReader(HttpContext.Request.InputStream).ReadToEnd();
            if(!string.IsNullOrEmpty(InTimeFilter))
                target.InTimeFilter = DateTime.ParseExact(InTimeFilter, "d/M, H:m", CultureInfo.InvariantCulture);
            if (!string.IsNullOrEmpty(OutTimeFilter))
                target.OutTimeFilter = DateTime.ParseExact(OutTimeFilter, "d/M, H:m", CultureInfo.InvariantCulture);
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
                if (target.VehicleType == null)
                {
                    target.VehicleType = typeof(Vehicles).GetEnumNames().Select(v => v);
                }
                if (target.InTimeFilter == null)
                {
                    target.InTimeFilter = new DateTime(2000, 1, 1);
                }
                if (target.OutTimeFilter == null)
                {
                    target.OutTimeFilter = new DateTime(3000, 1, 1);
                }
            }
            if (Request.IsAjaxRequest())
            {
                return PartialView("VehiclesList", Garage.GetVehicles(target));
            }

            return RedirectToAction("Index");
            //return View("Results", Garage.GetVehicles(target));
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
                    
                    /* /// default vehicle prices from the model Prices.cs
                    string priceName = Enum.GetNames(typeof(prices)).Single(n => n.Equals(newVehicle.VehicleType.ToString()));
                    int price = (int)Enum.Parse(typeof(prices), priceName);

                    newVehicle.price = price;*/

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
            if(Request.IsAjaxRequest())
                return PartialView("Details", Garage.GetVehicle(id));

            return RedirectToAction("Index");
        }

        public ActionResult PFilterVehicle(string fordon = "")
        {            
            if(fordon == "")
                return View();
            else
                return View("Index", Garage.FilterVehicle(fordon));
        }

        public ActionResult PFilterInDate(string indatum = "")
        {
            if (indatum == "")
                return View();
            else
                return View("Index", Garage.FilterInDate(indatum));
        }
    }
}