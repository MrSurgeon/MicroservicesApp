using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Basket.API.Data.Interfaces;
using Basket.API.Entities;
using Basket.API.Repositories.Interfaces;
using Newtonsoft.Json;
using JsonConverter = System.Text.Json.Serialization.JsonConverter;

namespace Basket.API.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IBasketContext _basketContext;

        public BasketRepository(IBasketContext basketContext)
        {
            _basketContext = basketContext;
        }

        public async Task<BasketCart> GetBasketAsync(string username)
        {
            var basket = await _basketContext
                                            .Redis
                                            .StringGetAsync(username);
            if (basket.IsNullOrEmpty) return null;

            return JsonConvert.DeserializeObject<BasketCart>(basket.ToString());
        }

        public async Task<BasketCart> UpdateBasketAsync(BasketCart basket)
        {
            var updated =
                await _basketContext.Redis.StringSetAsync(basket.Username, JsonConvert.SerializeObject(basket));
            if (!updated) return null;
            
            return await GetBasketAsync(basket.Username);
        }

        public async Task<bool> DeleteBasketAsync(string username)
        {
            return await _basketContext
                            .Redis
                            .KeyDeleteAsync(username);
        }
    }
}
