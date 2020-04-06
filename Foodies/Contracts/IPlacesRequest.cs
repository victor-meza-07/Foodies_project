using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Foodies.Models;

namespace Foodies.Contracts
{
    public interface IPlacesRequest
    {
        Task<GooglePlacesAPI> GetPlaces();
    }
}
