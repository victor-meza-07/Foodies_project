using Foodies.Data;
using Foodies.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace Foodies
{
    public class SMSTwilio
    {

        private ApplicationDbContext _context;
        public SMSTwilio(ApplicationDbContext context)
        {
            _context = context;
        }

        public void sendMessage(string toPhoneNumber, RestaurantModel matchingRestaurant )
        {
            string theAddress = getRestaurantAddress(matchingRestaurant);
            string theBody = $"You have been matched\nRestaurant details\n{matchingRestaurant.RestaurantName}\n{theAddress}\n{matchingRestaurant.RestaurantPhone}";
            TwilioClient.Init(Api_Keys.TwilioAccountSID, Api_Keys.TwilioAuthenticationProperty);
            var message = MessageResource.Create(

            body: theBody,
            from: new Twilio.Types.PhoneNumber(Api_Keys.fromPhoneNumber),
            to: new Twilio.Types.PhoneNumber(Api_Keys.toPhoneNumber)
                );

        }
        public void notifyUsers(List<CustomerModel> customers, RestaurantModel matchingRestaurant)


        {
            foreach ( CustomerModel customer in customers)
            {
                sendMessage(customer.PhoneNumber.ToString(), matchingRestaurant);
            }
            
        }
        private string getRestaurantAddress(RestaurantModel restaurant)
        {
            string address = "";
            var streetName = _context.Addresses.Where(a => a.AddressKey == restaurant.AddressKey)
                .Select(b => b.StreetName)
                .FirstOrDefault();

            address += streetName;


            return address;
        }
        
        [Key]
        private string RestuarantModelPrimaryKey { get; set; }

        [ForeignKey("IdentityAddress")]
        public string AddressKey { get; set; }

        public int BuildingNumber { get; set; } //313 
        public string StreetName { get; set; } // N Plankton
        public int ZipCode { get; set; } //53222
        public string City { get; set; } //Milwaukee
        public string StateCode { get; set; } // WI

        //sample: "313" "N Plankton" "Milwaukee", "WI" "53222"


    }
}
