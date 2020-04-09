using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Foodies.Models
{
    public class PhotosFromGoogle
    {
        [Key]
        public int PhotosPrimaryKey { get; set; }
        public string RestaurantGuid { get; set; }
        public int height { get; set; }
      //deleted html attributions
        public string photo_reference { get; set; }
        public int width { get; set; }
    }
}
