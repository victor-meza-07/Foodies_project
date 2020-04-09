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
        public string RestaurantPhone { get; set; } // formatted phone
        public string AddressKey { get; set; }
        public int PriceRangeIndex { get; set; } // a value from 0-4
        public string WebsiteUrl { get; set; }
        public bool open_now { get; set; }
        public float lat { get; set; }
        public float lng { get; set; }
        public PhotosFromGoogle[] photos { get; set; }
        public int price_level { get; set; }
        public float rating { get; set; }
      //deleted reviews
        
       

        //TODO: ADD THE OTHER MODELS.


    }
}
//check that they live in the same city, if so; do not make tha api call, 
//if not, make the api call and save all the data to our db