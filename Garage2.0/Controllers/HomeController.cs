using Garage2._0.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Garage2._0.Controllers
{
    public class HomeController : Controller
    {
        public GarageContext garageContext = new GarageContext();

        public ActionResult Index()
        {
            return View(garageContext.Vehicles);
        }

        public ActionResult Index2()
        {
            return View(garageContext.Vehicles);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}