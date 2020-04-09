using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Foodies.Contracts;
using Foodies.Data;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Foodies.Models.Services
{
    public class PlacesRequest : IPlacesRequest
    {
        ApplicationDbContext _context;
        public PlacesRequest(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<GooglePlacesAPI> GetPlaces()
        {
            var apiKey = "";
            string GetRequestURL = $"https://maps.googleapis.com/maps/api/place/textsearch/json?query=american+breakfast+restaurants+cafes+bakeries+in+Milwaukee+wi&key={apiKey}";
            
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
        public  async void CompareApiCall()
        {
            Dictionary<string, string> parameterDictionary = new Dictionary<string,string>();
            string url = "";
            int fieldsStart = url.IndexOf('=') + 1; //Find where a unique character starts before your fields
            int fieldEnd = url.IndexOf('&') - fieldsStart; // find where a character that is not included in your fields or before them starts.
            string fields = url.Substring(fieldsStart, fieldEnd); // make a substring. that starts and end propperly.
            string[] parameters = fields.Split('+'); // split them to a collection of parameters  //Do something with that collection wink wink wink --> put them in the db.
            parameterDictionary.Add("Cuisine", parameters[0]);
            parameterDictionary.Add("FoodType", parameters[1]);
            parameterDictionary.Add("SearchedCity", parameters[6]);
            parameterDictionary.Add("SearchedState", parameters[7]);



            string cuisineOut = "";
                parameterDictionary.TryGetValue("Cuisine",  out cuisineOut);
            string foodTypeOut = "";
            parameterDictionary.TryGetValue("FoodType", out foodTypeOut);
            string searchedCityOut = "";
            parameterDictionary.TryGetValue("SearchedCity", out searchedCityOut);
            string searchedStateOut = "";
            parameterDictionary.TryGetValue("SearchedState", out searchedStateOut);
            string searchResults = _context.RegisteredApiCalls.Where(searched => searched.Cuisine == cuisineOut && searched.FoodType == foodTypeOut && searched.SearchedCity == searchedCityOut && searched.SearchedState == searchedStateOut).Select(u => u.Url).FirstOrDefault();
          
            if (searchResults == null)
            {
                var googlePlacesObject = await GetPlaces();

            }
            else
            {
                var listofRestaurants = GetRestaurantList(parameterDictionary);
            }


            //todo: transfer parameters array to dictionary
        }
        public void MapGoogleObjectToRestaurantModel(GooglePlacesAPI googlePlacesAPI)
        {


        }
        public List<RestaurantModel> GetRestaurantList(Dictionary<string, string> parameterDictionary)
        {
            List<RestaurantModel> sampleList = new List<RestaurantModel>();
            string cuisineOut = "";
            parameterDictionary.TryGetValue("Cuisine", out cuisineOut);
            string foodTypeOut = "";
            parameterDictionary.TryGetValue("FoodType", out foodTypeOut);
            string searchedCityOut = "";
            parameterDictionary.TryGetValue("SearchedCity", out searchedCityOut);
            string searchedStateOut = "";
            parameterDictionary.TryGetValue("SearchedState", out searchedStateOut);
            string searchResults = _context.RegisteredApiCalls.Where(searched => searched.Cuisine == cuisineOut && searched.FoodType == foodTypeOut && searched.SearchedCity == searchedCityOut && searched.SearchedState == searchedStateOut).Select(u => u.Url).FirstOrDefault();
            // 1. search apicalls table for apicall that matches  all perameterDictionary perameters int primary key
            // 2. Search junction table for restaurant PrimaryId's string restaurant primary key
            // 3. search restaurant model table that match all the id's return restaurant model
            // 4. return restaurant model list
            return sampleList;
            
        }
    }
}
