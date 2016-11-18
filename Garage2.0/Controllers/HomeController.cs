using Garage2._0.DataAccessLayer;
using Garage2._0.Models;
using Garage2._0.Repositories;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
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
        public ActionResult Search([Bind(Include = "SearchOwner, SearchRegNr, SearchColor, VehicleType")]VehicleQuery target, string InTimeFilter, string OutTimeFilter, string Checkedin, string Checkedout)
        {
            HttpContext.Request.InputStream.Position = 0;
            var result = new System.IO.StreamReader(HttpContext.Request.InputStream).ReadToEnd();
            if (!string.IsNullOrEmpty(InTimeFilter))
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
                if (Checkedin == "on")
                    target.Checkedin = true;
                else
                    target.Checkedin = false;

                if (Checkedout == "on")
                    target.Checkedout = true;
                else
                    target.Checkedout = false;
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


        public ActionResult Create()
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView("Create", new CreateViewModel());
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RegNr, Owner, Color, VehicleType, NumberOfWheels, InTime, OutTime")]CreateViewModel newVehicle)
        {
            if (ModelState.IsValid && Request.IsAjaxRequest())
            {
                /// default vehicle prices from the model Prices.cs
                string vtype = Enum.GetNames(typeof(prices)).Single(n => n.Equals(newVehicle.VehicleType.ToString()));
                int price = (int)Enum.Parse(typeof(prices), vtype);
                

                //newVehicle.TotalMoneyAmount = price;
                Vehicle v = new Vehicle();

                foreach (PropertyInfo p in typeof(CreateViewModel).GetProperties())
                {
                    PropertyInfo modelproperty = typeof(CreateViewModel).GetProperty(p.Name);
                    PropertyInfo vehicleproperty = typeof(Vehicle).GetProperty(p.Name);

                    vehicleproperty.SetValue(v, modelproperty.GetValue(newVehicle));
                }

                if(Garage.AddVehicle(v).exist == false)
                    return Json(new { type = true, message = string.Format("Vehicle {0} was created", v.RegNr), function = "Checkin", id = v.Id });
                else
                    return Json(new { type = true, message = string.Format("Vehicle {0} Already exist", v.RegNr), function = "Checkin", id = v.Id });
            }
            return RedirectToAction("Index");
        }

        public ActionResult Checkin(int? id)
        {
            if (Request.IsAjaxRequest())
            {
                if (id != null)
                {
                    Vehicle v = Garage.GetVehicle(id.Value);
                    CheckInViewModel checkinviewmodel = new CheckInViewModel();


                    foreach (PropertyInfo p in typeof(CheckInViewModel).GetProperties())
                    {
                        PropertyInfo modelproperty = typeof(CheckInViewModel).GetProperty(p.Name);
                        PropertyInfo vehicleproperty = typeof(Vehicle).GetProperty(p.Name);

                        if (vehicleproperty != null)
                            if (vehicleproperty.GetValue(v) != null)
                                modelproperty.SetValue(checkinviewmodel, vehicleproperty.GetValue(v));
                    }

                    checkinviewmodel.vehicle = v;

                    return PartialView("Checkin", checkinviewmodel);
                }
                else
                {
                    return PartialView("Create", new Vehicle());
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Checkin([Bind(Include = "Id, InTime, OutTime, Checkedin")]CheckInViewModel newVehicle)
        {
            if (ModelState.IsValid && Request.IsAjaxRequest())
            {
                Garage.CheckInVehicle(newVehicle);

                return Json(new { type = true, message = "Vehicle Checked in!" });
            }

            return RedirectToAction("Index");
        }

        public ActionResult Checkout(int? id)
        {
            if (id != null)
            {
                Vehicle v = Garage.GetVehicle(id.Value);
                CheckOutViewModel checkoutviewmodel = new CheckOutViewModel { vehicle = v };

                return PartialView("Checkout", checkoutviewmodel);
            }
            else
                return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Checkout(int id)
        {
            if (Garage.CheckOutVehicle(id))
            {
                return Json(new { type = true, message = "Vechicle was checkedout!" });
            }
            else
            {
                return Json(new { type = false, message = "Vechicle was not checkedout!" });
            }
        }

        public ActionResult Remove(int? id)
        {
            if (id != null)
            {
                Vehicle v = Garage.GetVehicle(id.Value);
                return PartialView("Remove", v);
            }
            else
                return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Remove(int id)
        {
            if (Garage.RemoveVehicle(id))
            {
                return Json(new { type = true, message = "Vechicle was removed!" });
            }
            else
            {
                return Json(new { type = false, message = "Vechicle was not removed!" });
            }
        }

        public ActionResult Edit(int id)
        {
            return PartialView("Edit", Garage.GetVehicle(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,RegNr, Owner, Color, VehicleType, NumberOfWheels, InTime, OutTime")]Vehicle newVehicle)
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
            if (Request.IsAjaxRequest())
                return PartialView("Details", Garage.GetVehicle(id));

            return RedirectToAction("Index");
        }


        public ActionResult Delete(int id = 0)
        {
            Garage.RemoveVehicle(id);
            return RedirectToAction("Index");
        }
       
    }
}