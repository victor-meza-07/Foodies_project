using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Foodies.Models
{
    public class LikeHistoryModel
    {

        private readonly string key; 
        public LikeHistoryModel()
        {
            key = Guid.NewGuid().ToString();
        }

        [Key]
        public string LikeHistoryKey { get { return key; } set { LikeHistoryKey = key; } }
        public string RestaurantModelPrimaryKey { get; set; }
        public string CustomerModelPrimaryKey { get; set; }
        public DateTime TimeStamp { get; set; } //when they hit that like button. (this is how we make our model adapt over time)
    }
}
    