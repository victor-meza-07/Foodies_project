using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Foodies.Models
{
    public class CustomerViewModel
    {
        public CustomerModel CurrentCustomer { get; set; }
        public RestaurantModel[] CollectionOfRestaurantRecomendations { get; set; }
        public string CuisineFavored { get; set; }
        public LikeHistoryModel[] LikesList { get; set; }
    }
}