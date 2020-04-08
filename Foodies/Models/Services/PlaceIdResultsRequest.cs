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
    public class PlaceIdResultsRequest : IPlaceResultsRequest
    {
        ApplicationDbContext _context;
        public PlaceIdResultsRequest(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<GooglePlacesAPI_PlaceIDSearchResults> GetPlaceIDResults(string APIKEY, string PLACE_ID)
        {
            string url = $"https://maps.googleapis.com/maps/api/place/details/json?place_id={PLACE_ID}&fields=name,rating,formatted_phone_number,permanently_closed,opening_hours,photos,price_level,vicinity,website,reviews&key={APIKEY}";


            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                GooglePlacesAPI_PlaceIDSearchResults searchByIdResults = JsonConvert.DeserializeObject<GooglePlacesAPI_PlaceIDSearchResults>(json);
                return searchByIdResults;
            }
            else 
            {
                return null;
            } 
        }
       
    }
}
