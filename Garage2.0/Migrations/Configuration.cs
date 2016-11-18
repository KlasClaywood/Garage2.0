namespace Garage2._0.Migrations
{
    using Garage2._0.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Garage2._0.DataAccessLayer.GarageContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Garage2._0.DataAccessLayer.GarageContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            // 2016, 11, 14, 10, 42, 0
            //context.Vehicles.AddOrUpdate(r => r.RegNr,
            //    new Vehicle { RegNr = "ABC123", Color = "Black", Owner = "Donald Trump", VehicleType = Vehicles.Car, NumberOfWheels = 4, InTime = new DateTime(2016, 11, 14, 10, 42, 0) },
            //    new Vehicle { RegNr = "ABC124", Color = "Black", Owner = "Donald Trum", VehicleType = Vehicles.Car, NumberOfWheels = 4, InTime = new DateTime(2016, 11, 14, 11, 14, 0) });
        }
    }
}
