﻿using System;

namespace SignalR.RabbitMQ
{
    public class RabbitConnectionBase : IDisposable
    {
        public RabbitConnectionBase(RabbitMqScaleoutConfiguration configuration)
        {
            if(configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            Configuration = configuration;
        }

        internal Action OnReconnectionAction { get; set; }
        internal Action OnDisconnectionAction { get; set; }
        public RabbitMqScaleoutConfiguration Configuration { get; set; }
        internal Action<RabbitMqMessageWrapper> OnMessageReceived { get; set; }

        public virtual void Dispose()
        {
            // do nothing?
        }

        public virtual void Send(RabbitMqMessageWrapper message)
        {
            throw new NotImplementedException("Implement the Send method in your Rabbit connection class.");
        }

        public virtual void StartListening()
        {
            throw new NotImplementedException("Implement the StartListening method in your Rabbit connection class.");
        }

        protected void OnReconnection(object sender, EventArgs e)
        {
	        OnReconnectionAction?.Invoke();
        }

        protected void OnDisconnection(object sender, EventArgs e)
        {
            if (OnDisconnectionAction != null)
            {
                OnDisconnectionAction.Invoke();
            }
        }

	    protected void OnMessage(RabbitMqMessageWrapper message)
        {
            OnMessageReceived.Invoke(message);
        }
    }
}