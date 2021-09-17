using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Ordering.Core.Entities;

namespace Ordering.Infrastructure.Data
{
    public class OrderContextSeed
    {
        public static async void Seed(OrderContext _context, ILoggerFactory _loggerFactory, int? retry = 0)
        {
            int retryForAvailability = retry.Value;

            try
            {
                if (!(await _context.Database.GetPendingMigrationsAsync()).Any())
                {
                    if (!_context.Orders.Any())
                    {
                        _context.Orders.AddRange(GetPreconfiguredOrders());
                        await _context.SaveChangesAsync();
                    }
                    return;
                }

                await _context.Database.MigrateAsync();
                Seed(_context, _loggerFactory);
            }
            catch (Exception e)
            {
                if (retryForAvailability < 3)
                {
                    retryForAvailability++;
                    var log = _loggerFactory.CreateLogger<OrderContextSeed>();
                    log.LogError(e.Message);
                    Seed(_context, _loggerFactory, retryForAvailability);
                }
            }
        }

        private static IEnumerable<Order> GetPreconfiguredOrders()
        {
            return new List<Order>()
            {
                new Order()
                {
                    Username = "Err",
                    FirstName = "Enes",
                    LastName = "Cerrah",
                    AddressLine = "İzmir",
                    CardName = "Enes Cerrah"
                }
            };
        }
    }
}
