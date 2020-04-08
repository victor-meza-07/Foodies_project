using Foodies.Models;
using Foodies.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Foodies.Contracts
{
    public interface IFacebookDataRequest
    {
        Task<FacebookData> GetFacebookData(string FacebookUserToken);
    }
}
