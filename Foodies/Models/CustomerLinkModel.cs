using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Foodies.Models
{
    public class CustomerLinkModel
    {
        private readonly string key; 
        public CustomerLinkModel(string customerOneKey, string CustomerTwoKey)
        {
            key = customerOneKey + CustomerTwoKey;
        }
        
        [Key]
        public string CustomerLinkModelPrimaryKey { get { return key; } set { CustomerLinkModelPrimaryKey = key; } }
 
        public string CustomerOneKey { get; set; }
        public string CustomerTwoKey { get; set; }
    }
}
