using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Foodies.Models
{
    public class DislikeHistoryModel
    {
        private readonly string key;
        public DislikeHistoryModel()
        {
            key = Guid.NewGuid().ToString();
        }

        [Key]
        public string DislikeHistoryModelPrimaryKey { get { return key; } set { DislikeHistoryModelPrimaryKey = key; } }

        public string RestaurantModelPrimaryKey { get; set; } 
        public string CustomerModelPrimaryKey { get; set; } // add the association to CustomerModel
        public DateTime TimeStamp { get; set; } // the type should be revisted.
    }
}
