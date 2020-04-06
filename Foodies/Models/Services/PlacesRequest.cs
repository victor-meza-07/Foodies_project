using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Foodies.Contracts;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Foodies.Models.Services
{
    public class PlacesRequest : IPlacesRequest
    {
        public PlacesRequest()
        {

        }

        public async Task<GooglePlacesAPI> GetPlaces()
        {
            var apiKey = "";
            string GetRequestURL = $"https://maps.googleapis.com/maps/api/place/textsearch/json?query=breakfast+restaurants+cafes+bakeries+in+Milwaukee&key={apiKey}";
            
            HttpClient client = new HttpClient();
            
            HttpResponseMessage response = await client.GetAsync(GetRequestURL);

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                GooglePlacesAPI googlePlacesAPI = JsonConvert.DeserializeObject<GooglePlacesAPI>(json);

                return googlePlacesAPI;
            }
            else 
            {
                return null;
            }
        }
    }
}
