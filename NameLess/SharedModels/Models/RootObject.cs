using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModels.Models
{
    [NotMapped]
    public class Rootobject
    {
        public Result[] results { get; set; }
        public string status { get; set; }
    }
    [NotMapped]
    public class Result
    {
        public Address_Components[] address_components { get; set; }
        public string formatted_address { get; set; }
        public Geometry geometry { get; set; }
        public string place_id { get; set; }
        public string[] types { get; set; }
    }

    [NotMapped]
    public class Geometry
    {
        public Location location { get; set; }
        public string location_type { get; set; }
        public Viewport viewport { get; set; }
    }

    [NotMapped]
    public class Location
    {
        public float lat { get; set; }
        public float lng { get; set; }
    }

    [NotMapped]
    public class Viewport
    {
        public Northeast northeast { get; set; }
        public Southwest southwest { get; set; }
    }

    [NotMapped]
    public class Northeast
    {
        public float lat { get; set; }
        public float lng { get; set; }
    }

    [NotMapped]
    public class Southwest
    {
        public float lat { get; set; }
        public float lng { get; set; }
    }

    [NotMapped]
    public class Address_Components
    {
        public string long_name { get; set; }
        public string short_name { get; set; }
        public string[] types { get; set; }
    }
}
