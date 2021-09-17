using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.Entities
{
    public class BasketCart
    {
        public BasketCart()
        {
            
        }
        public string Username { get; set; }
        public List<BasketCartItem> Items { get; set; } = new List<BasketCartItem>();

        public BasketCart(string username)
        {
            Username = username;
        }

        public decimal TotalPrice
        {
            get
            {
                return Items.Select(x => x.Price * x.Quantity).Sum();
            }
        }
    }
}
