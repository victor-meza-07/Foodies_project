using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Foodies.Models
{
    public class CustomerViewModel
    {
        public CustomerModel CurrentCustomer { get; set; }
        public List<RestaurantModel> CollectionOfRestaurantRecomendations { get; set; }
        public string CuisineFavored { get; set; }
        public List<LikeHistoryModel> LikesList { get; set; }
    }
}