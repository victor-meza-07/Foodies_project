using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Foodies.Models
{
    public class APICalls
    {
        public APICalls()
        {

        }
        [Key]
        public int PrimaryKey { get; set; }
        
        public string Url { get; set; }
        public string FoodType { get; set; }
        public string Cuisine { get; set; }
        public string SearchedCity { get; set; }
        public string SearchedState{ get; set; }

    }// search query={american}+{breakfast}+restaurant+bakery+cafe+in+{milwaukee}+{wi}
}
