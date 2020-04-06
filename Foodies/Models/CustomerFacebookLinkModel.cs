using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Foodies.Models
{
    public class CustomerFacebookLinkModel
    {
        private readonly string _key;

        public CustomerFacebookLinkModel()
        {
            _key = Guid.NewGuid().ToString();
        }


        [Key]
        public string CustomerFacebookKey { get { return _key; } set { CustomerFacebookKey = _key; } }

        public string CustomerGUID { get; set; }
        public int FacebookProfileId { get; set; }
    }
}
