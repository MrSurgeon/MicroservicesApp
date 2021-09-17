using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventBusRabbitMq.Events;
using Newtonsoft.Json;

namespace EventBusRabbitMq.Producer
{
    public class EventBusRabbitMqProducer
    {
        private readonly IRabbitMQConnection _rabbitConnection;

        public EventBusRabbitMqProducer(IRabbitMQConnection rabbitConnection)
        {
            _rabbitConnection = rabbitConnection;
        }

        public void PublishBasketCheckout(string queueName, BasketCheckoutEvent publishModel)
        {
            using (var channel = _rabbitConnection.CreateModel())
            {
                channel.QueueDeclare(queue: queueName, false, false, false, null);
                var message = JsonConvert.SerializeObject(publishModel);
                var body = Encoding.UTF8.GetBytes(message);

                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;
                properties.DeliveryMode = 2;

                channel.ConfirmSelect();
                channel.BasicPublish(exchange: "", routingKey: queueName, mandatory: true, basicProperties: properties, body);
                channel.WaitForConfirmsOrDie();

                channel.BasicAcks += (sender, ea) =>
                {
                    Console.WriteLine("Sent RabbitMq");
                };
                channel.ConfirmSelect();
            }
        }
    }
}
