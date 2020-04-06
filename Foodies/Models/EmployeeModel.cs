using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Foodies.Models
{
    public class EmployeeModel
    {
        private readonly string _key;
        public EmployeeModel()
        {
            _key = Guid.NewGuid().ToString();
        }

        [Key]
        public string EmployeeModelPrimaryKey { get { return _key; } set { EmployeeModelPrimaryKey = _key; } }

        [ForeignKey("IdentityUser")]
        public string IdentityUserId { get; set; }
        //Add the relationship here


        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }


        //Did not add the User Reports GUID, better suited for an employee ID to exist on that table. 

    }
}
