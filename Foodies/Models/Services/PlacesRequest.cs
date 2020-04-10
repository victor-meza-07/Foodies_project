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

        //getting stuff for the kitchen
        private async Task<GooglePlacesAPI> GetPlaces(string GetRequestURL)
        {
            GetRequestURL = $"https://maps.googleapis.com/maps/api/place/textsearch/json?query=american+breakfast+restaurants+cafes+bakeries+in+Milwaukee+wi&key={Api_Keys.googlePlacesApiKey}";

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


        //making sure we don't buy too many boxes of sugar if we already have sugar. 
        private async void CompareApiCall(string url)
        {
            Dictionary<string, string> parameterDictionary = new Dictionary<string, string>();
      
            int fieldsStart = url.IndexOf('=') + 1; //Find where a unique character starts before your fields
            int fieldEnd = url.IndexOf('&') - fieldsStart; // find where a character that is not included in your fields or before them starts.
            string fields = url.Substring(fieldsStart, fieldEnd); // make a substring. that starts and end propperly.
            string[] parameters = fields.Split('+'); // split them to a collection of parameters  //Do something with that collection wink wink wink --> put them in the db.
            parameterDictionary.Add("Cuisine", parameters[0]);
            parameterDictionary.Add("FoodType", parameters[1]);
            parameterDictionary.Add("SearchedCity", parameters[6]);
            parameterDictionary.Add("SearchedState", parameters[7]);



            string cuisineOut = "";
            parameterDictionary.TryGetValue("Cuisine", out cuisineOut);
            string foodTypeOut = "";
            parameterDictionary.TryGetValue("FoodType", out foodTypeOut);
            string searchedCityOut = "";
            parameterDictionary.TryGetValue("SearchedCity", out searchedCityOut);
            string searchedStateOut = "";
            parameterDictionary.TryGetValue("SearchedState", out searchedStateOut);
            string searchResults = _context.RegisteredApiCalls.Where(searched => searched.Cuisine == cuisineOut && searched.FoodType == foodTypeOut && searched.SearchedCity == searchedCityOut && searched.SearchedState == searchedStateOut).Select(u => u.Url).FirstOrDefault();

            if (searchResults == null)
            {
                //we don't have enough sugar
                var googlePlacesObject = await GetPlaces();
            }
            else
            {
                //we realize we have the right amount.
                var listofRestaurants = GetRestaurantList(parameterDictionary);
            }
        }

        //prepare the meal for the customer with ingredients in our kitchen.
        public List<RestaurantModel> GetRestaurantList(Dictionary<string, string> parameterDictionary)
        {
            List<RestaurantModel> restaurants = new List<RestaurantModel>();
            //we have made this search before by searching for these parameters in our API CALLS TABLE
            var queryKey = _context.RegisteredApiCalls.Where(r => r.FoodType == parameterDictionary["FoodType"] && r.Cuisine == parameterDictionary["Cuisine"] && r.SearchedCity == parameterDictionary["SearchedCity"] && r.SearchedState == parameterDictionary["SearchedState"]).Select(q => q.PrimaryKey).FirstOrDefault();

            if (queryKey != null)
            {
                var listOfRestaurantKeys = _context.SearchJunctions.Where(s => s.ApiPrimaryKey == queryKey).Select(k => k.RestaurantModelPrimaryKey);


                foreach (var restaurantKey in listOfRestaurantKeys)
                {
                    var currentRestaurantModel = _context.Restaurants.Where(r => r.RestaurantModelPrimaryKey == restaurantKey).FirstOrDefault();
                    restaurants.Add(currentRestaurantModel);
                }
            } // we had enough materials for the meal.
            else 
            {
                //Generating the Recipe
                var searchq = GenerateSearchQuery(parameterDictionary["Cuisine"], parameterDictionary["SearchedCity"], parameterDictionary["SearchedState"]);

                
                restaurants.la
                //*******************************TODO: 1 ***************************//

                //*******************************TODO: 2 ***************************//
                //*******************************TODO: 3 ***************************//
                //*******************************TODO: 4 ***************************//
                /*foreach (var restaurantKey in listOfRestaurantKeys)
                {
                    var currentRestaurantModel = _context.Restaurants.Where(r => r.RestaurantModelPrimaryKey == restaurantKey).FirstOrDefault();
                    restaurants.Add(currentRestaurantModel);
                }*/
            } //We don't know this order and we need to generate a recipe and buy ingredients we need.

            return restaurants;
        }

        //checking the recipe
        private string TimeOfDayFoodType()
        {
            DateTime now = DateTime.Now;
            string breakfast, lunch, dinner;
            breakfast = "breakfast";
            lunch = "lunch";
            dinner = "dinner";




            var breakFastStart = "05:00:00";
            var breakFastEnd = "11:00:00";
            var lunchStart = "11:00:00";
            var lunchEnd = "16:00:00";
            var dinnerStart = "16:00:00";
            var dinnerEnd = "20:00:00";

            if ((DateTime.Parse(now.ToString("HH:mm:ss")) >= DateTime.Parse(breakFastStart)) && (DateTime.Parse(now.ToString("HH:mm:ss")) < DateTime.Parse(breakFastEnd)))//08:00:00
            {
                //make a breakfast query based on the criteria passsed in
                return breakfast;
            }
            else if ((DateTime.Parse(now.ToString("HH:mm:ss")) >= DateTime.Parse(lunchStart)) && (DateTime.Parse(now.ToString("HH:mm:ss")) < DateTime.Parse(lunchEnd)))//12:00:00
            {
                //make a lunch query based on the criteria passsed in
                return lunch;
            }
            else if ((DateTime.Parse(now.ToString("HH:mm:ss")) >= DateTime.Parse(dinnerStart)) && (DateTime.Parse(now.ToString("HH:mm:ss")) < DateTime.Parse(dinnerEnd))) //20:12:59
            {
                //make a dinner query based on the criteria passsed in
                return dinner;
            }
            else
            {
                return dinner;
            }
        }

        //Generating the recipe for the customer.(with regard to our analogy)
        private string GenerateSearchQuery(string cuisine, string city, string state)
        {
            //cuisine, foodtype, restaurants+cafes+bakeries+in

            string foodtype = TimeOfDayFoodType(); // returns breakfast lunch or dinner. 

            
            string api_fields = $"{cuisine}+{foodtype}+restaurants+cafes+bakeries+in+{city}+{state}";
            string apiUrl = $"https://maps.googleapis.com/maps/api/place/textsearch/json?query={api_fields}&key={Api_Keys.googlePlacesApiKey}";

            return apiUrl;
        }





        /*
            TODO:
            1. Map Data From Google Searches
                a. Launch a Get Request to Google and obtain a "Google Places Object"
                b. for every "place" object inside the "Google Places Object" map the informations we care about
                   to our Restaurant Model
                      * Set every property in our RestaurantModel equal to its counterpart from the GooglePlacesModel
                      Ex: Restaurant.lat = GooglePlacesApi.results[i (iterating through all the results) ].geometry.location.lat;
                c. insertonsubmit these mappings
                d. submit to the db
            2. Save the search parameters to the API Calls Table
            3. Save the Search Results (ie: search parameter pk, and the restaurant pk to the search juntion table)
            4. Make A list of restaurants that fit the criteria passed.
            
        
            5. Do not forget to Add-Migration
            6. Do not Forget to Update-Database. 
        */
    }
}