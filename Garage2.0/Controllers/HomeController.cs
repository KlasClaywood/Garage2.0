using Garage2._0.DataAccessLayer;
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

        public ActionResult Results()
        {
            return View();
        }

        public ActionResult Checkin()
        {
            return View();
        }

        public ActionResult Checkout()
        {
            return View();
        }

        public ActionResult Edit()
        {
            return View();
        }



        
        
    }
}