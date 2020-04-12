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
        public PredictiveModel(ApplicationDbContext context)
        {
            _context = context;
        }



        public void GetRecomendations(CustomerModel customer) 
        {
            //getting info from the likes data table
            string guid = customer.CustomerModelPrimaryKey;
            var likesByThisCustomer = _context.Likes.Where(l => l.CustomerModelPrimaryKey == guid);

            //Getting the RestaurantModels
            List<RestaurantModel> restaurants = new List<RestaurantModel>();
            foreach (LikeHistoryModel like in likesByThisCustomer)
            {
                restaurants.Add(_context.Restaurants.Where(r => r.RestaurantModelPrimaryKey == like.RestaurantModelPrimaryKey).FirstOrDefault());
            }

            //on to do some maths!


            
        }

        private double GetOverallZipCodeAffinity(List<RestaurantModel> restaurantsLiked, IQueryable<LikeHistoryModel> likes) 
        {
            double overallAffinity = 0.0;

            //Count the total amount of repeat zipcodes in the system.
            Dictionary<string, int> ZipCodesAndFrequencies = CountRepeatZipCodes(restaurantsLiked, likes);
            double fullWeight = AssignWeightsToIndividalZipCodeInstances(restaurantsLiked, likes);


            return overallAffinity;
        }

        /// <summary>
        /// Returns a dictionary of Zipcode, numericalRepeats
        /// </summary>
        /// <param name="restaurantsLiked"></param>
        /// <param name="likes"></param>
        /// <returns></returns>
        private Dictionary<string, int> CountRepeatZipCodes(List<RestaurantModel> restaurantsLiked, IQueryable<LikeHistoryModel> likes) 
        {
            Dictionary<string, int> zipCodeFrequency = new Dictionary<string, int>();


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
                }
            }



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
    
    
    
    }
}
