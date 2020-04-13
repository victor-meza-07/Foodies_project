using Foodies.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Foodies.Models
{
    public class PredictiveModel
    {
        private ApplicationDbContext _context;
        private DateTime _ModelStarting;
        public PredictiveModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public CustomerViewModel GetRestaurantRecomendations(CustomerViewModel customer) 
        {
            List<RestaurantModel> restaurants = new List<RestaurantModel>();
            CustomerModel MainCustomer = customer.CurrentCustomer;
            var FoodieGUID = _context.Foodies.Where(foodie => foodie.CustomerOneKey == MainCustomer.CustomerModelPrimaryKey).Select(g => g.CustomerTwoKey).FirstOrDefault();
            CustomerModel Foodie = _context.Customers.Where(a => a.CustomerModelPrimaryKey == FoodieGUID).FirstOrDefault();

            List<RestaurantModel> MainCustomerRecomendations = GetRecomendations(MainCustomer);
            List<RestaurantModel> FoodieRecomendations = GetRecomendations(Foodie);

            foreach (RestaurantModel restaurant in FoodieRecomendations) 
            {
                if (!(MainCustomerRecomendations.Contains(restaurant))) 
                {
                    FoodieRecomendations.Remove(restaurant);
                }
            }

            if (FoodieRecomendations.Count < 1)
            {
                FoodieRecomendations = MainCustomerRecomendations;
            }

            //assemble the list here.
            //Remeber that with the customer Model you also have access to their foodie. through the customer Link Model
            //do something like foreach item in list one if list 2 !.contains(item) then list one.remove(item) where list one is the foodies 
            //list and list 2 is the main customers list. 
            restaurants = FoodieRecomendations;

            customer.CollectionOfRestaurantRecomendations = restaurants;


            return customer; 
        }








        private List<RestaurantModel> GetRecomendations(CustomerModel customer) 
        {
            //getting info from the likes data table
            string guid = customer.CustomerModelPrimaryKey;
            var likesByThisCustomer = _context.Likes.Where(l => l.CustomerModelPrimaryKey == guid);
            List<RestaurantModel> recomendations = new List<RestaurantModel>();


            //Getting the RestaurantModels
            List<RestaurantModel> restaurants = new List<RestaurantModel>();
            foreach (LikeHistoryModel like in likesByThisCustomer)
            {
                restaurants.Add(_context.Restaurants.Where(r => r.RestaurantModelPrimaryKey == like.RestaurantModelPrimaryKey).FirstOrDefault());
            }

            double ZipCodeAffinity = GetOverallZipCodeAffinity(restaurants, likesByThisCustomer);
            double CuisineAffinity = GetOverallCuisineAffinity(likesByThisCustomer);
            double PriceLevelAffinity = GetOverallPriceLevelAffinity(restaurants, likesByThisCustomer);
            double RatingAffinity = GetOverallRatingAffinity(restaurants, likesByThisCustomer);

            //Now that we have their affinities, we can search which one they would like the most.
            //According to their affinity level, we can make a query based on thataffinity level!

            List<double> affinityList = new List<double>();
            affinityList.Add(ZipCodeAffinity);
            affinityList.Add(CuisineAffinity);
            affinityList.Add(PriceLevelAffinity);
            affinityList.Add(RatingAffinity);

            //Now the highest is at the top which means we will add the top Affinity restaurants at the beginning of the list. 
            affinityList.Sort();

            
            //checking what order they are in now; 
            if (affinityList[0] == ZipCodeAffinity) 
            {
                //get the specific instance they like for example, what specific zip code?
                int zipCodeLoved = ZipCodeLiked(likesByThisCustomer, restaurants);
                List<RestaurantModel> restaurantsInThisZip = new List<RestaurantModel>();
                var AddsInZip = _context.Addresses.Where(z => z.ZipCode == zipCodeLoved).Select(r => r.RestaurantGuid);
                foreach (string guids in AddsInZip) 
                {
                    if (guids == _context.Restaurants.Where(r => r.RestaurantModelPrimaryKey == guids).Select(re => re.RestaurantModelPrimaryKey).FirstOrDefault()) 
                    {
                        restaurantsInThisZip.Add(_context.Restaurants.Where(r => r.RestaurantModelPrimaryKey == guids).FirstOrDefault());
                    }
                }
                recomendations = restaurantsInThisZip;
            }
            else if (affinityList[0] == PriceLevelAffinity) 
            {
                int beloedPL = PriceLevelLiked(likesByThisCustomer, restaurants);
                
                var RestaurantsAtPLOrLower = _context.Restaurants.Where(z => z.PriceRangeIndex <= beloedPL);

                recomendations = RestaurantsAtPLOrLower.ToList();

            }
            else if (affinityList[0] == CuisineAffinity) 
            {
                string belovedCuisine = CuisineLiked(likesByThisCustomer, restaurants);
                //first get the api keys that have this cuisine
                var keysWithcuisine = _context.RegisteredApiCalls.Where(c => c.Cuisine == belovedCuisine);
                List<int> apiCallKeys = new List<int>();
                List<string> restaurantGUIDS = new List<string>();
                List<RestaurantModel> restaurantModels = new List<RestaurantModel>();
                foreach (APICalls call in keysWithcuisine) 
                {
                    apiCallKeys.Add(call.PrimaryKey);
                }
                foreach (int key in apiCallKeys) 
                {
                    restaurantGUIDS.Add(_context.SearchJunctions.Where(s => s.JunctionPrimaryKey == key).Select(r => r.RestaurantModelPrimaryKey).FirstOrDefault());
                }
                foreach (string id in restaurantGUIDS) 
                {
                    restaurantModels.Add(_context.Restaurants.Where(r => r.RestaurantModelPrimaryKey == id).FirstOrDefault());
                }

                recomendations = restaurantModels;
            }
            else if (affinityList[0] == RatingAffinity) 
            {
                float belovedRatingMin = RatingLiked(likesByThisCustomer, restaurants);
                var restaurantsAtRatingOrHigher = _context.Restaurants.Where(r => r.Rating >= belovedRatingMin);
                recomendations = restaurantsAtRatingOrHigher.ToList();
            }



            return recomendations;
        }
        private double GetOverallZipCodeAffinity(List<RestaurantModel> restaurantsLiked, IQueryable<LikeHistoryModel> likes) 
        {
            double overallAffinity = 0.0;

            List<string> zipCodesListed = new List<string>();
            //Count the total amount of repeat zipcodes in the system.
            Dictionary<string, int> ZipCodesAndFrequencies = CountRepeatZipCodes(restaurantsLiked, likes, out zipCodesListed);
            double fullWeight = AssignWeightsToIndividalZipCodeInstances(restaurantsLiked, likes);
            int totalCodeRepetitions = 0; 
            foreach (string code in zipCodesListed) 
            {
                totalCodeRepetitions += ZipCodesAndFrequencies[code];
            }


            overallAffinity = (totalCodeRepetitions * fullWeight * 5);
            


            return overallAffinity;
        }
        private double GetOverallCuisineAffinity(IQueryable<LikeHistoryModel> likes) 
        {
            double overallCusineAffinity = 0.0;

            Dictionary<string, int> cuisineFrequencies = GetCuisineFrequencies(likes);

            overallCusineAffinity = AssignWeightsToCuisines(cuisineFrequencies, likes);
            


            return overallCusineAffinity;
        }
        private double GetOverallPriceLevelAffinity(List<RestaurantModel> restaurants, IQueryable<LikeHistoryModel> likes) 
        {
            double  affinity = 0.0;

            Dictionary<int, int> plFrequencies = GetPriceLevelFrequencies(restaurants, likes);
            affinity = AssignPriceLevelWeights(plFrequencies, likes);


            return affinity;
        }
        private double GetOverallRatingAffinity(List<RestaurantModel> restaurants, IQueryable<LikeHistoryModel> likes) 
        {
            double affinity = 0.0;



            Dictionary<float, int> ratingFrequency = GetRatingFrequencies(restaurants, likes);
            affinity = GetOverallRatingAffinity(restaurants, likes);



            return affinity;
        }



        private int PriceLevelLiked(IQueryable<LikeHistoryModel> likes, List<RestaurantModel> restaurantModels) 
        {
            //Yes I understand that if these all inhereted from an interface it would make some of this process a little less tedious.
            int pl = 0;
            
            Dictionary<int, int> priceL = GetPriceLevelFrequencies(restaurantModels, likes);
            int belovedPL = 0; // because we all love free things
            int belovedPLCount = 0; //jk
            
            foreach (KeyValuePair<int, int> codes in priceL)
            {
                if (codes.Value > belovedPLCount)
                {
                    belovedPLCount = codes.Value;
                    belovedPL = codes.Key;
                }
            }

            pl = belovedPL;
            return pl;
        }
        private int ZipCodeLiked(IQueryable<LikeHistoryModel> likes, List<RestaurantModel> restaurantModels) 
        {
            int zipcode = 0;

            List<string> zips = new List<string>();

            Dictionary<string, int> zipcodes = CountRepeatZipCodes(restaurantModels, likes, out zips);
            string belovedZipCode = "";
            int belovedZipCodeCount = 0;
            foreach (KeyValuePair<string, int> codes in zipcodes) 
            {
                if (codes.Value > belovedZipCodeCount) 
                {
                    belovedZipCodeCount = codes.Value;
                    belovedZipCode = codes.Key;
                }
            }

            zipcode = Int32.Parse(belovedZipCode);
            return zipcode;
        }
        private string CuisineLiked(IQueryable<LikeHistoryModel> likes, List<RestaurantModel> restaurantModels) 
        {
            string cuisine = "";
            int cuisineCount = 0;
            Dictionary<string, int> CuisineFrequencies = GetCuisineFrequencies(likes);
            foreach (KeyValuePair<string, int> cf in CuisineFrequencies) 
            {
                if (cf.Value > cuisineCount) 
                {
                    cuisineCount = cf.Value;
                    cuisine = cf.Key;
                }
            }



            return cuisine;
        }
        private float RatingLiked(IQueryable<LikeHistoryModel> likes, List<RestaurantModel> restaurantModels) 
        {
            float rating = 0;
            int ratingF = 0;
            Dictionary<float, int> rf = GetRatingFrequencies(restaurantModels, likes);
            foreach (KeyValuePair<float, int> rfs in rf) 
            {
                if (rfs.Value > ratingF) 
                {
                    ratingF = rfs.Value;
                    rating = rfs.Key;
                }
            }

            return rating;
        }





        private Dictionary<string, int> GetCuisineFrequencies(IQueryable<LikeHistoryModel> likes) 
        {
            Dictionary<string, int> cuisineFrequencies = new Dictionary<string, int>();

            ///we have to query the search juntion table for any restaurant GUId's that match the ones for this specific cuisine.
            var listofguids = likes.Select(l => l.RestaurantModelPrimaryKey);
            List<int> apiKeys = new List<int>(); 
            
            foreach (string guid in listofguids) 
            {
                if (guid == _context.SearchJunctions.Where(r => r.RestaurantModelPrimaryKey == guid).Select(a => a.RestaurantModelPrimaryKey).FirstOrDefault()) 
                {
                    apiKeys.Add(_context.SearchJunctions.Where(sj => sj.RestaurantModelPrimaryKey == guid).Select(sj => sj.JunctionPrimaryKey).FirstOrDefault());
                }
            }


            //now that we have primary keys of search juncitions
            //we can compare the cusines that get repeated the most.
            foreach (int id in apiKeys) 
            {
                var cuisine = _context.RegisteredApiCalls.Where(api => api.PrimaryKey == id).Select(a => a.Cuisine).FirstOrDefault();

                if (cuisineFrequencies.ContainsKey(cuisine))
                {
                    cuisineFrequencies[cuisine]++;
                }
                else 
                {
                    cuisineFrequencies.Add(cuisine, 0);
                }
            }

            //now that we have a dictionary with some frequencies off to do some math. 

            return cuisineFrequencies;
        }
        private Dictionary<int, int> GetPriceLevelFrequencies(List<RestaurantModel> restaurants, IQueryable<LikeHistoryModel> likes) 
        {
            Dictionary<int, int> PriceLevelFrequency = new Dictionary<int, int>();
            foreach (RestaurantModel restaurant in restaurants) 
            {
                if (PriceLevelFrequency.ContainsKey(restaurant.Price_level))
                {
                    PriceLevelFrequency[restaurant.Price_level]++;
                }
                else 
                {
                    PriceLevelFrequency.Add(restaurant.Price_level, 0);
                }
            }
            return PriceLevelFrequency;
        }
        private Dictionary<float, int> GetRatingFrequencies(List<RestaurantModel> restaurants, IQueryable<LikeHistoryModel> likes) 
        {
            Dictionary<float, int> ratingFrequencies = new Dictionary<float, int>();

            foreach (RestaurantModel restaurant in restaurants) 
            {
                if (ratingFrequencies.ContainsKey(restaurant.Rating))
                {
                    ratingFrequencies[restaurant.Rating]++;
                }
                else 
                {
                    ratingFrequencies.Add(restaurant.Rating, 0);
                }
            }
            
            return ratingFrequencies;
        }


        /// <summary>
        /// Returns a dictionary of Zipcode, numericalRepeats
        /// </summary>
        /// <param name="restaurantsLiked"></param>
        /// <param name="likes"></param>
        /// <returns></returns>
        private Dictionary<string, int> CountRepeatZipCodes(List<RestaurantModel> restaurantsLiked, IQueryable<LikeHistoryModel> likes, out List<string> zipcodes) 
        {
            Dictionary<string, int> zipCodeFrequency = new Dictionary<string, int>();
            List<string> zipCodes = new List<string>();

            //get all instances of this one zip code.
            foreach (RestaurantModel restaurant in restaurantsLiked) 
            {
                //try to find this zipcode in our dictionary.
                int zipCodeCount = 0;
                string RegisteredZipCode = _context.Addresses.Where(a => a.AddressKey == restaurant.AddressKey).Select(z => z.ZipCode).FirstOrDefault().ToString();
                
                if (zipCodeFrequency.TryGetValue(RegisteredZipCode, out zipCodeCount)) 
                {
                    zipCodeFrequency[RegisteredZipCode] = zipCodeFrequency[RegisteredZipCode] + 1; // this is increasing the frequency of a repeat.
                }
                else
                {
                    zipCodeFrequency.Add(RegisteredZipCode, 0);// add it to the dictionary without a frequency value.
                    zipCodes.Add(RegisteredZipCode);
                }
            }

            zipcodes = zipCodes;

            return zipCodeFrequency;
        }
        private double AssignWeightsToIndividalZipCodeInstances(List<RestaurantModel> restaurants, IQueryable<LikeHistoryModel> likes)
        {
            

            DateTime ModelStart = DateTime.Today; 
            double uniqueZipcodeWeight = 0.0;
            //do some math here.

            //first we should find the amount of time their first like happened to give us a starting point.
            foreach (LikeHistoryModel like in likes) 
            {
                if (ModelStart > like.TimeStamp) 
                {
                    ModelStart = like.TimeStamp; // gets the earliest possible date!
                    _ModelStarting = ModelStart;
                }
            }

            //once we have the earliest possible date we create a Timespan Object and do time math.

            TimeSpan timeElapsed = DateTime.Today - ModelStart;
            int daysElapsed = timeElapsed.Days;

            double totalWeight = 0.0;


            //now we do the individual weighing and adding math.
            foreach (LikeHistoryModel like in likes) 
            {
                TimeSpan dateSinceLiked = like.TimeStamp - ModelStart;
                int daysSinceDecided = dateSinceLiked.Days;

                double individualWeight = (daysSinceDecided/daysElapsed);

                totalWeight = totalWeight + individualWeight;
            }






            return totalWeight;
        }
        private double AssignWeightsToCuisines(Dictionary<string, int> frequencies, IQueryable<LikeHistoryModel> likes) 
        {
            double weight = 0.0;
            int totalOccurrences = 0; 

            foreach (KeyValuePair<string, int> keys in frequencies) 
            {
                totalOccurrences += keys.Value;
            }


            //calculate the weight of the likes for this particular cuisine.
            TimeSpan timeSpan = DateTime.Today - _ModelStarting;
            int daysElapsed = timeSpan.Days;
            foreach (LikeHistoryModel liked in likes) 
            {
                TimeSpan dateSinceLiked = liked.TimeStamp - _ModelStarting;
                int daysSinceLiked = dateSinceLiked.Days;
                double individualWeight = (daysSinceLiked / daysElapsed);
                weight += individualWeight;
            }

            weight = (weight * totalOccurrences * 5);

            return weight; 
        }
        private double AssignPriceLevelWeights(Dictionary<int, int> frequencies, IQueryable<LikeHistoryModel> likes) 
        {
            double weight = 0.0;


            TimeSpan timeElapsed = DateTime.Today - _ModelStarting;
            int daysElapsed = timeElapsed.Days;

            foreach (LikeHistoryModel like in likes) 
            {
                TimeSpan timeSinceLiked = DateTime.Today - like.TimeStamp;
                int daysSinceLiked = timeSinceLiked.Days;
                double individualWeight = (daysSinceLiked / daysElapsed);
                weight += individualWeight;
            }

            int frequencySigma = 0;
            foreach (KeyValuePair<int,int> pair in frequencies) 
            {
                frequencySigma += pair.Value;
            }


            weight = (frequencySigma * weight * 5);

            return weight;
        }
        private double AssingRatingWeights(Dictionary<float, int> ratingFrequencies, IQueryable<LikeHistoryModel> likes) 
        {
            double weight = 0.0;

            TimeSpan timeElapsed = DateTime.Today - _ModelStarting;
            int daysElapsed = timeElapsed.Days;

            foreach (LikeHistoryModel like in likes)
            {
                TimeSpan timeSinceLiked = DateTime.Today - like.TimeStamp;
                int daysSinceLiked = timeSinceLiked.Days;
                double individualWeight = (daysSinceLiked / daysElapsed);
                weight += individualWeight;
            }

            int frequencySigma = 0;
            foreach (KeyValuePair<float, int> pair in ratingFrequencies)
            {
                frequencySigma += pair.Value;
            }


            weight = (frequencySigma * weight * 5);

            return weight;
        }
    }
}
