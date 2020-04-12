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
        public int Height { get; set; }
        //deleted html attributions
        public string Photo_reference { get; set; }
        public int Width { get; set; }
    }
}
