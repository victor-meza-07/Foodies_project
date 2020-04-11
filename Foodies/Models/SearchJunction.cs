using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Foodies.Models
{
    public class SearchJunction
    {
        public SearchJunction()
        {

        }
        [Key]
        public int JunctionPrimaryKey { get; set; }
        
        public string RestaurantModelPrimaryKey { get; set; }
        
        public int ApiPrimaryKey { get; set; }
    }
}
