using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Basket.API.Entities;

namespace Basket.API.Repositories.Interfaces
{
    public interface IBasketRepository
    {
        Task<BasketCart> GetBasketAsync(string username);
        Task<BasketCart> UpdateBasketAsync(BasketCart basket);
        Task<bool> DeleteBasketAsync(string username);
    }
}
