using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Foodies.Models
{
    public class RestaurantModel
    {

        public RestaurantModel()
        {
            RestaurantModelPrimaryKey = Guid.NewGuid().ToString();
        }

        [Key]
        public string RestaurantModelPrimaryKey { get; set; }

        public string RestaurantName { get; set; }
        public string RestaurantPhone { get; set; }
        public string AddressKey { get; set; }
        public int PriceRangeIndex { get; set; } // a value from 0-4
        public string WebsiteUrl { get; set; }
        public string MenuUrl { get; set; }
        public string GoogleGeoLocationData { get; set; } // Will be a FK to google GEOLOCation data. //Lat, Long

        //TODO: ADD THE OTHER MODELS.


    }
}
//check that they live in the same city, if so; do not make tha api call, 
//if not, make the api call and save all the data to our db