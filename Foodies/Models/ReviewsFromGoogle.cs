using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Foodies.Models
{
    public class ReviewsFromGoogle
    {
        [Key]
        public int ReviewsPrimaryKey { get; set; }
        public string RestaurantGuid { get; set; }
        public string Author_name { get; set; }
        public string Author_url { get; set; }
        public string Language { get; set; }
        public string Profile_photo_url { get; set; }
        public int Rating { get; set; }
        public string Relative_time_description { get; set; }
        public string Text { get; set; }
        public int Time { get; set; }
    }
}

