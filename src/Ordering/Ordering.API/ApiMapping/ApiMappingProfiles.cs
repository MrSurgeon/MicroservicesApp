using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EventBusRabbitMq.Events;
using Ordering.Application.Commands;

namespace Ordering.API.ApiMapping
{
    public class ApiMappingProfiles : Profile
    {
        public ApiMappingProfiles()
        {
            CreateMap<BasketCheckoutEvent, CheckoutOrderCommand>().ReverseMap();
        }
    }
}
