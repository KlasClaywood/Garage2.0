using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Garage2._0.Repositories
{
    public class VehicleQuery
    {
        public string SearchOwner { get; set; }
        public string SearchRegNr { get; set; }
        public string SearchColor { get; set; }

        public IEnumerable<string> VehicleType { get; set; }

        public DateTime? InTimeFilter { get; set; }
        public DateTime? OutTimeFilter { get; set; }

        public bool Checkedin { get; set; }
        public bool Checkedout { get; set; }
    }
}
