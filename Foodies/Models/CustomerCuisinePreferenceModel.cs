using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Foodies.Models
{
    public class CustomerCuisinePreferenceModel
    {
        [Key]
        public int CCPMPrimaryKey { get; set; }

        public string CustomerPK { get;  set; }
        public string CuisineType { get; set; }
        public DateTime PreferredOn { get;  set; }
    }
}
