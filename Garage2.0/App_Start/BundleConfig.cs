﻿using System.Web;
using System.Web.Optimization;

namespace Garage2._0
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js",
                      "~/Scripts/bootstrap-multiselect.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/bootstrap-multiselect.css"));


            // Bootstrap Table

            bundles.Add(new ScriptBundle("~/bundles/bootstrap-table").Include(
                      "~/Scripts/bootstrap-table.js",
                      "~/Scripts/bootstrap-table-locale-all.js"));

            bundles.Add(new StyleBundle("~/Content/bootstrap-table").Include(
                      "~/Content/bootstrap-table.css"));

            //Datepicker
            BundleTable.Bundles.Add(new ScriptBundle("~/bundles/datepicker").Include(
            "~/Scripts/bootstrap-datetimepicker.js"));

            BundleTable.Bundles.Add(new StyleBundle("~/Content/datepicker").Include(
            "~/Content/bootstrap-datetimepicker.css"));

            BundleTable.Bundles.Add(new ScriptBundle("~/bundles/momentjs").Include(
            "~/Scripts/moment.js"));
        }
    }
}
