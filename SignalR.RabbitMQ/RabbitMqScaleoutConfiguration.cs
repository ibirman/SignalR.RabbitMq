using System;
using EasyNetQ;
using Microsoft.AspNet.SignalR.Messaging;
using RabbitMQ.Client;

namespace SignalR.RabbitMQ
{
    public class RabbitMqScaleoutConfiguration : ScaleoutConfiguration
    {
        public RabbitMqScaleoutConfiguration(string ampqConnectionString, string exchangeName, string queueName = null, string stampExchangeName = "signalr-stamp")
        {
            if (string.IsNullOrEmpty(ampqConnectionString))
            {
                throw new ArgumentNullException(nameof(ampqConnectionString));
            }

            if (string.IsNullOrEmpty(exchangeName))
            {
                throw new ArgumentNullException(nameof(exchangeName));
            }

            AmpqConnectionString = ampqConnectionString;
            ExchangeName = exchangeName;
            QueueName = queueName;
            StampExchangeName = stampExchangeName;
        }
        public RabbitMqScaleoutConfiguration(ConnectionFactory connectionfactory, string exchangeName, string queueName = null, string stampExchangeName = "signalr-stamp")
        {
            if (connectionfactory == null)
            {
                throw new ArgumentNullException(nameof(connectionfactory));
            }

            if (string.IsNullOrEmpty(exchangeName))
            {
                throw new ArgumentNullException(nameof(exchangeName));
            }
#if DEBUG
            //no heartbeat for debugging
            var ampqConnectionString =
	            $"host={connectionfactory.HostName};virtualHost={connectionfactory.VirtualHost};username={connectionfactory.UserName};password={connectionfactory.Password}";
#else
            var ampqConnectionString = $"host={connectionfactory.HostName};virtualHost={connectionfactory.VirtualHost};username={connectionfactory.UserName};password={connectionfactory.Password};requestedHeartbeat=10";
#endif
            
            AmpqConnectionString = ampqConnectionString;
            ExchangeName = exchangeName;
            QueueName = queueName;
            StampExchangeName = stampExchangeName;
        }
        public RabbitMqScaleoutConfiguration(IBus bus, string exchangeName, string queueName = null, string stampExchangeName = "signalr-stamp")
        {
            if (bus == null)
            {
                throw new ArgumentNullException(nameof(bus));
            }
            
            if (string.IsNullOrEmpty(exchangeName))
            {
                throw new ArgumentNullException(nameof(exchangeName));
            }

            Bus = bus;
            ExchangeName = exchangeName;
            QueueName = queueName;
            StampExchangeName = stampExchangeName;
        }

        public string AmpqConnectionString { get; private set; }
        public string ExchangeName { get; private set; }
        public string StampExchangeName { get; private set; }
        public string QueueName { get; private set; }
        public IBus Bus { get; private set; }
    }
}