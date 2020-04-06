using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Foodies.Models
{
    public class UserReportsModel
    {
        private readonly string _key;
        public UserReportsModel()
        {
            _key = Guid.NewGuid().ToString();
        }

        public string UserReportsModelPrimaryKey { get { return _key; } set { UserReportsModelPrimaryKey = _key; } }
        public string ReportingCustomerGUID { get; set; }
        public string ReportedCustomerGUID { get; set; }
        public string ReportCode { get; set; } // Come up with a table of report codes? We can only fit so much data in the field

    }
}
