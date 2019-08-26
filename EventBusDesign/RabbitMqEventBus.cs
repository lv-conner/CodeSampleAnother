using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;


namespace EventBusDesign
{
    public class RabbitMqEventBus:EventBus
    {
        private IConnection connection;
        public RabbitMqEventBus()
        {

        }
        public override void Publish<TEvent>(TEvent @event)
        {
            IConnectionFactory factory = new ConnectionFactory()
            {
                HostName = "localhost"
            };
            IConnection connection = factory.CreateConnection();
            var model = connection.CreateModel();

            base.Publish(@event);
        }
    }

    public class RabbitMqEventBusOptions
    {
        public string VitualHost { get; set; }
    }
}
