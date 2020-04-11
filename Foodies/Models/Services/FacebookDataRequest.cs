using Foodies.Contracts;
using Foodies.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Facebook;
using RestSharp;

namespace Foodies.Models.Services
{
    public class FacebookDataRequest : IFacebookDataRequest
    {
        private string _FacebookAppSecret = Api_Keys.FacebookAppSecret;
        private string _FacebokAppId = Api_Keys.FacebookAppID;
        private dynamic _token;
        private ApplicationDbContext _context;
        private IFacebookDataRequest _FacebookDataRequest;
        public FacebookDataRequest(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<FacebookData> GetFacebookData(string FacebookUserToken)
        {


            string _RequestUrl = $"https://graph.facebook.com/me?fields=id,name,age_range,gender,payment_pricepoints,likes.summary(true)&access_token=";
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




        /* THE POSTMAN METHOD */
        public void postman() 
        {
            var client = new RestClient("https://www.facebook.com/v6.0/dialog/oauth?client_id=2844860895551149&redirect_uri=https://localhost:44355&state=test123&response_type=token");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Cookie", "sb=e4WOXkUkul4op-qjZkt-itfM; fr=1piSrlzsGHyJ6Wubn..BejoV7.ya.AAA.0.0.BekJx2.AWU7XW7n");
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);
        }























        //The Model for the response
        public class AccessUser 
        {
            public string access_token { get; set; }
            public string token_type { get; set; }
            public string expires_in { get; set; }
        }

        public void CheckAuthorization() 
        {
            string app_Id = Api_Keys.FacebookAppID.ToString();
            string app_secret = Api_Keys.FacebookAppSecret.ToString();
            string sample = "https://www.facebook.com/v6.0/dialog/oauth?client_id=2844860895551149&redirect_uri=https://localhost:44355&state=test123&response_type=token";

            FacebookClient facebook = new FacebookClient();
            dynamic userToken = facebook.Get("/oauth",
                new
                {
                    client_id = app_Id,
                    redirect_uri = "https://localhost:44355",
                    state = "test123",
                    response_type = "token"
                }) ;

            //get the user token.




            dynamic user = facebook.Get("/me",
                new { fields = "id, name",
                      access_token = userToken
                });
        }


    } 
}
