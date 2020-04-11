using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Foodies.Models
{
    public class AddressModel
    {
        public AddressModel()
        {
            AddressKey = Guid.NewGuid().ToString();
        }

        [Key]
        public string AddressKey { get; set; }

        public int BuildingNumber { get; set; } //313 
        public string StreetName { get; set; } // N Plankton
        public int ZipCode { get; set; } //53222
        public string City { get; set; } //Milwaukee
        public string StateCode { get; set; } // WI
        public string RestaurantGuid { get; set; }
        //sample: "313" "N Plankton" "Milwaukee", "WI" "53222"
    }
}