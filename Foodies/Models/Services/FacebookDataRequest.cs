using Foodies.Contracts;
using Foodies.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Foodies.Models.Services
{
    public class FacebookDataRequest : IFacebookDataRequest
    {
        private string _FacebookAppSecret = Api_Keys.FacebookAppSecret;
        private string _FacebokAppId = Api_Keys.FacebookAppID;
        private ApplicationDbContext _context;
        private IFacebookDataRequest _FacebookDataRequest; 
        public FacebookDataRequest(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<FacebookData> GetFacebookData(string FacebookUserToken)
        {


            string _RequestUrl = $"https://graph.facebook.com/me?fields=id,name,age_range,gender,payment_pricepoints,likes.summary(true)&access_token={FacebookUserToken}";
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(_RequestUrl);

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                FacebookData data = JsonConvert.DeserializeObject<FacebookData>(json);
                return data;
            }
            else 
            {
                return null;
            }
        }

        /// <summary>
        /// Will return T/F if a users Facebook Data has been pulled.
        /// </summary>
        /// <param name="CustomerGUID"></param>
        /// <returns></returns>
        public bool CheckTableInformation(string CustomerGUID)
        {
            //this is where we check
            var CustomerData = _context.CustomerFacebookLink.Where(a => a.CustomerGUID == CustomerGUID);

            if (CustomerData != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<FacebookData> RetrieveCustomerFacebookData(string CustomerGUID)
        {
            //this method will only be called when we know that there is a profile.
            var userToken = _context.CustomerFacebookLink.Where(a => a.CustomerGUID == CustomerGUID).Select(b => b.UserAccessToken).FirstOrDefault();
            FacebookData dataForSpecifiedUser = await GetFacebookData(userToken);

            return dataForSpecifiedUser;
        }
    }
}
