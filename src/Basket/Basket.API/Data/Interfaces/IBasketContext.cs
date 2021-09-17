using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace Basket.API.Data.Interfaces
{
    public interface IBasketContext
    {
        IDatabase Redis
        {
            get;
        }
    }
}
