using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DefaultAPI.Application.Interfaces
{
    public abstract class IRabbitMQService<T> where T : class
    {
        protected string HostName;
        protected string UserName;
        protected string UserPsw;

        protected virtual ConnectionFactory ConfigConnectionFactory(string HostName = "localhost", string UserName = "guest", string Password = "guest")
        {
            return new ConnectionFactory()
            {
                HostName = HostName,
                UserName = UserName,
                Password = Password,
                DispatchConsumersAsync = true
            };
        }

        public abstract Task SendMessageToQueue(string QueueName, T ObjectValue, bool QueueIsDurable);

        public abstract Task ReceiveMessageToQueue(string QueueName, bool QueueIsDurable);

        public abstract Task SendMessageToQueueInSameTime(string ExchangeName, T ObjectValue);

        public abstract Task ReceiveMessageToQueueInSameTime(string ExchangeName);

        public abstract Task SendMessageToQueueRouting(string ExchangeName, string RoutingKey, T ObjectValue);

        public abstract Task ReceiveMessageToQueueRouting(string ExchangeName, string RoutingKey);
    }
}
