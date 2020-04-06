using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Foodies.Contracts
{
    interface IPlaceResultsRequest
    {
        Task<GooglePlacesAPI_PlaceIDSearchResults> GetPlaceIDResults(string APIKEY, string PLACE_ID);
    }
}
