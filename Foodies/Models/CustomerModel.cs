using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Foodies.Models
{
    public class CustomerModel
    {
        private readonly string _key;
        public CustomerModel()
        {
            _key = Guid.NewGuid().ToString();
        }

        [Key]
        public string CustomerModelPrimaryKey { get; set; }

        [ForeignKey("IdentityUser")]
        public string IdentityUserId { get; set; }
        public IdentityUser IdentityUser { get; set; }


        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string AddressKey { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }
        public int AgeRange { get; set; }
        public string Gender { get; set; }

    }
}
