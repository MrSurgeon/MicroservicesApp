
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EventBusRabbitMq;
using EventBusRabbitMq.Common;
using EventBusRabbitMq.Events;
using MediatR;
using Newtonsoft.Json;
using Ordering.Application.Commands;
using Ordering.Core.Repositories;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Ordering.API.RabbitMQ
{
    public class EventBusRabbitMqConsumer
    {
        private readonly IRabbitMQConnection _rabbitConnection;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public EventBusRabbitMqConsumer(IMapper mapper, IMediator mediator, IRabbitMQConnection rabbitConnection)
        {
            _mapper = mapper;
            _mediator = mediator;
            _rabbitConnection = rabbitConnection;
        }

        public void Consume()
        {
            var channel = _rabbitConnection.CreateModel();
            channel.QueueDeclare(queue: EventBusConstants.BasketCheckoutQueue, durable: false, false, false, null);
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += ReceivedEvent;
            channel.BasicConsume(queue: EventBusConstants.BasketCheckoutQueue, autoAck: true, consumer: consumer);
        }

        private async void ReceivedEvent(object sender, BasicDeliverEventArgs e)
        {
            var message = Encoding.UTF8.GetString(e.Body.Span);
            var basketCheckoutEvent = JsonConvert.DeserializeObject<BasketCheckoutEvent>(message);

            var command = _mapper.Map<CheckoutOrderCommand>(basketCheckoutEvent);
            var result = await _mediator.Send(command);
        }

        public void Disconnect()
        {
            _rabbitConnection.Dispose();
        }
    }
}
