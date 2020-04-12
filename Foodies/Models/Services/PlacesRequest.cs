using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Foodies.Contracts;
using Foodies.Data;
using Foodies.Models;
using Foodies;
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

        //**** THIS IS THE ENTRY POINT **//
        public async Task<List<RestaurantModel>> GetListOfRestaurants(string cuisine, CustomerModel customer)
        {

            //generate the potential URL;
            string customerAddKey = customer.AddressKey;
            AddressModel customerAddress = _context.Addresses.Where(a => a.AddressKey == customerAddKey).FirstOrDefault();

            string potentialQuery = GenerateSearchQuery(cuisine, customerAddress.City, customerAddress.StateCode);
            var restaurants = await CompareApiCall(potentialQuery); // this method doesn't return anything but... 

            return restaurants;

        }



        //getting stuff for the kitchen
        public async Task<GooglePlacesAPI> GetPlaces(string GetRequestURL)
        {


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
        private async Task<List<RestaurantModel>> CompareApiCall(string url)
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
                var googlePlacesObject = await GetPlaces(url);
                MapGoogleresultToRestaurantModel(parameterDictionary, googlePlacesObject, url);
                var listofRestaurants = GetRestaurantList(parameterDictionary);

                return listofRestaurants;
            }
            else
            {
                //we realize we have the right amount.
                var listofRestaurants = GetRestaurantList(parameterDictionary);

                return listofRestaurants;
            }


        }

        private void SaveSearchParamsToLocalDB(Dictionary<string, string> param, string url)
        {
            string url2 = url.Substring(0, 50);

            APICalls call = new APICalls();
            call.FoodType = param["FoodType"];
            call.Cuisine = param["Cuisine"];
            call.SearchedCity = param["SearchedCity"];
            call.SearchedState = param["SearchedState"];
            call.Url = url2;
            _context.RegisteredApiCalls.Add(call);
            _context.SaveChanges();
        }

        //prepare the meal for the customer with ingredients in our kitchen.
        private List<RestaurantModel> GetRestaurantList(Dictionary<string, string> parameterDictionary)
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
              
                 var searchq = GenerateSearchQuery(parameterDictionary["Cuisine"], parameterDictionary["SearchedCity"], parameterDictionary["SearchedState"]);
                SaveSearchParamsToLocalDB(parameterDictionary, searchq);
                // 2. map the results to data base
                //a. map searchresult to searchjunction table

               

                 var listOfRestaurantKeys = _context.SearchJunctions.Where(s => s.ApiPrimaryKey == queryKey).Select(k => k.RestaurantModelPrimaryKey);




                foreach (var restaurantKey in listOfRestaurantKeys)
                {
                    var currentRestaurantModel = _context.Restaurants.Where(r => r.RestaurantModelPrimaryKey == restaurantKey).FirstOrDefault();
                    restaurants.Add(currentRestaurantModel);
                }
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
        private async void MapGoogleresultToRestaurantModel(Dictionary<string,string> param, GooglePlacesAPI placesObject, string url)
        {
            List<string> placeIds = new List<string>();
            Dictionary<string, string> idAdd = new Dictionary<string, string>();

            foreach (Result result in placesObject.results)
            {
                RestaurantModel restaurant = new RestaurantModel();
                restaurant.RestaurantName = result.name;
                restaurant.PriceRangeIndex = result.price_level;
                restaurant.Open_now = result.opening_hours.open_now;
                restaurant.Lat = result.geometry.location.lat;
                restaurant.Lng = result.geometry.location.lng;
                restaurant.Price_level = result.price_level;
                restaurant.Rating = result.rating;
                restaurant.Place_Id = result.place_id;

                placeIds.Add(result.place_id);
                idAdd.Add(result.place_id, result.formatted_address);

                _context.Restaurants.Add(restaurant);
                //save place id to use in search by id query                 
                //map addressKey table to Restaurant model address key              
                //photos mapped at different time
            }
            _context.SaveChanges();

            List<string> resaurantGuid = new List<string>();

            foreach (string id in placeIds)
            {
                var guid = _context.Restaurants.Where(r => r.Place_Id == id).Select(r => r.RestaurantModelPrimaryKey).FirstOrDefault();
                resaurantGuid.Add(guid);
               
            }

            MapQueriesToSearchJunctionTable(param, resaurantGuid);


            Mapresultphotocollection(placesObject);


            foreach (string id in placeIds)
            {
                RestaurantModel restaurant = _context.Restaurants.Where(r => r.Place_Id == id).FirstOrDefault();
                GooglePlacesAPI_PlaceIDSearchResults place = await GetResultsById(id);
                restaurant.RestaurantPhone = place.result.formatted_phone_number;
                restaurant.WebsiteUrl = place.result.website;
                string addr = idAdd[id];
                MapAddressToLocalDb(id, addr); // Adds the new addresses to the context; 

            }

            _context.SaveChanges();
        }

        private void MapAddressToLocalDb(string id, string formatted_address)
        {
            //sample formatted ADD: "2352 S Kinnickinnic Ave, Milwaukee, WI 53207, United States"
            string BuildingNumber, StreetName, ZipCode, City, StateCode;
            StreetName = "";


            string[] streetParam = formatted_address.Split(',');

            string[] BuildingStreet = streetParam[0].Split(' ');
            BuildingNumber = BuildingStreet[0];

            for (int i = 1; i < BuildingStreet.Length; i++)
            {
                StreetName += BuildingStreet[i];
                if (i != BuildingStreet.Length)
                {
                    StreetName += " ";
                }
            }


            City = streetParam[1].Trim();

            StateCode = streetParam[2].Trim();
            string[] stateZip = StateCode.Split(' ');
            StateCode = stateZip[0];

            ZipCode = stateZip[1];


            var restaurantGUID = _context.Restaurants.Where(r => r.Place_Id == id).Select(re => re.RestaurantModelPrimaryKey).FirstOrDefault();


            AddressModel newAdd = new AddressModel();
            newAdd.RestaurantGuid = restaurantGUID;
            newAdd.BuildingNumber = Convert.ToInt32(BuildingNumber);
            newAdd.StreetName = StreetName;
            newAdd.City = City;
            newAdd.StateCode = StateCode;
            newAdd.ZipCode = Convert.ToInt32(ZipCode);

            _context.Addresses.Add(newAdd);
        }

        //NAMESPACE CONFLICT ERROR
        private void Mapresultphotocollection(GooglePlacesAPI place)
        {
            
            foreach (Result result in place.results)
            {
                var restaurantwhosphotosbelongtoguid = _context.Restaurants.Where(r => r.Place_Id == result.place_id).Select(re => re.RestaurantModelPrimaryKey).FirstOrDefault();

                foreach (Photo individualphoto  in result.Photos)
                {
                    PhotosFromGoogle photoToSAve = new PhotosFromGoogle();
                    photoToSAve.Height = individualphoto.height;
                    photoToSAve.Width = individualphoto.width;
                    photoToSAve.Photo_reference = individualphoto.photo_reference;
                    photoToSAve.RestaurantGuid = restaurantwhosphotosbelongtoguid;
                    //loop through all the photos. 
                    _context.PhotosFromGoogle.Add(photoToSAve);
                }
               
            }
            _context.SaveChanges();
        }

        private async Task<GooglePlacesAPI_PlaceIDSearchResults> GetResultsById(string id)
        {
            string url = $"https://maps.googleapis.com/maps/api/place/details/json?place_id={id}&fields=formatted_phone_number,name,price_level,rating,reviews,vicinity,website&key={Api_Keys.googlePlacesApiKey}";

            HttpClient client = new HttpClient();

            HttpResponseMessage response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                GooglePlacesAPI_PlaceIDSearchResults googleplacesapi = JsonConvert.DeserializeObject<GooglePlacesAPI_PlaceIDSearchResults>(json);

                return googleplacesapi;
            }
            else
            {
                return null;
            }
        }

        private void MapQueriesToSearchJunctionTable(Dictionary<string,string> param, List<string> restaurantGuid)
        {
             string FoodType = param["FoodType"];
             string Cuisine = param["Cuisine"];
             string SearchedCity = param["SearchedCity"];
             string SearchedState = param["SearchedState"];

            var apiPk = _context.RegisteredApiCalls.Where(r => r.FoodType == FoodType && r.Cuisine == Cuisine && r.SearchedCity == SearchedCity && r.SearchedState == SearchedState).Select(r => r.PrimaryKey).FirstOrDefault();

            foreach  (string guid in restaurantGuid)
            {
                SearchJunction junky = new  SearchJunction();
                junky.RestaurantModelPrimaryKey = guid;
                junky.ApiPrimaryKey = apiPk;
                _context.SearchJunctions.Add(junky);
            }
            _context.SaveChanges();
        }

        //   TODO:
        //      X 1. Map Data From Google Searches
        //          X a.Launch a Get Request to Google and obtain a "Google Places Object"
        //          X b. for every "place" object inside the "Google Places Object" map the informations we care about
        //              to our Restaurant Model
        //                 * Set every property in our RestaurantModel equal to its counterpart from the GooglePlacesModel
        //                 Ex: Restaurant.lat = GooglePlacesApi.results[i(iterating through all the results)].geometry.location.lat;
        //   X c.insertonsubmit these mappings

        //   X d. submit to the db 
        //          ----Important note, Photos is still not functional! ---- 


        //X 2. Save the search parameters to the API Calls Table
        //       3. Save the Search Results (ie: search parameter pk, and the restaurant pk to the search juntion table)
        //       X  4. Make A list of restaurants that fit the criteria passed.


        //       X 5. Do not forget to Add-Migration
        //       X 6. Do not Forget to Update-Database.
        //   */


    }
}