using DefaultAPI.Application.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace DefaultAPI.Application.Services
{
    public sealed class RabbitMQService<T> : IRabbitMQService<T> where T : class
    {
        public RabbitMQService()
        {
        }

        /// <summary>
        /// When param QueueIsDurable is True, the message send to queue will be consumed one or more consume.
        /// </summary>
        /// <param name="QueueName"></param>
        /// <param name="ObjectValue"></param>
        /// <param name="QueueIsDurable"></param>
        /// <returns></returns>
        public override async Task SendMessageToQueue(string QueueName, T ObjectValue, bool QueueIsDurable)
        {
            var factory = ConfigConnectionFactory();
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: QueueName,
                                 durable: QueueIsDurable,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

                    var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(ObjectValue));
                    var properties = channel.CreateBasicProperties();

                    if (QueueIsDurable)
                        properties.Persistent = true;

                    channel.BasicPublish(exchange: "",
                                         routingKey: QueueName,
                                         basicProperties: QueueIsDurable ? properties : null,
                                         body: body);

                    Console.WriteLine($"Enviado mensagem {body} para a fila {QueueName} com sucesso");
                    await Task.CompletedTask;
                }
            }
        }

        /// <summary>
        /// When param QueueIsDurable is True, the message send to queue will be consumed one or more consume. 
        /// </summary>
        /// <param name="queueName"></param>
        /// <param name="objectValue"></param>
        /// <param name="QueueIsDurable"></param>
        /// <returns></returns>
        public override async Task ReceiveMessageToQueue(string QueueName, bool QueueIsDurable)
        {
            var factory = ConfigConnectionFactory();
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            channel.QueueDeclare(queue: QueueName,
                         durable: QueueIsDurable,
                         exclusive: false,
                         autoDelete: false,
                         arguments: null);

            #region [Make Consumer get one message to process until end]
            channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);
            #endregion

            var consumer = new AsyncEventingBasicConsumer(channel);
            string messages = string.Empty;

            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                if (QueueIsDurable)
                {
                    int dots = message.Split('.').Length - 1;
                    Thread.Sleep(dots * 1000);
                    channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                }
            };

            channel.BasicConsume(queue: QueueName,
                                 autoAck: QueueIsDurable ? false : true,
                                 consumer: consumer);
        }


        /// <summary>
        /// Publisher FanOut to send same message from queues in sameTime. ExchangeName must be the same in SendandReceive
        /// </summary>
        /// <param name="ExchangeName"></param>
        /// <param name="QueueName"></param>
        /// <param name="ObjectValue"></param>
        /// <returns></returns>
        public override async Task SendMessageToQueueInSameTime(string ExchangeName, T ObjectValue)
        {
            var factory = ConfigConnectionFactory();
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare(exchange: ExchangeName, type: ExchangeType.Fanout);
                    var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(ObjectValue));
                    var properties = channel.CreateBasicProperties();
                    channel.BasicPublish(exchange: ExchangeName,
                                routingKey: "",
                                basicProperties: null,
                                body: body);
                    Console.WriteLine($"Enviado mensagem {body} para o publicador {ExchangeName} com sucesso");
                    await Task.CompletedTask;
                }
            }
        }

        /// <summary>
        /// Publisher FanOut to send same message from queues in sameTime. ExchangeName must be the same in SendandReceive
        /// </summary>
        /// <param name="ExchangeName"></param>
        /// <param name="QueueName"></param>
        /// <param name="ObjectValue"></param>
        /// <returns></returns>
        public override async Task ReceiveMessageToQueueInSameTime(string ExchangeName)
        {
            var factory = ConfigConnectionFactory();
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            channel.ExchangeDeclare(exchange: ExchangeName, type: ExchangeType.Fanout);

            #region [Will generate Random Queue]
            var queueName = channel.QueueDeclare().QueueName;
            #endregion

            channel.QueueBind(queue: queueName,
                              exchange: ExchangeName,
                              routingKey: "");

            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
            };

            channel.BasicConsume(queue: queueName,
                                 autoAck: true,
                                 consumer: consumer);

            await Task.CompletedTask;
        }

        /// <summary>
        /// Publisher Direct to send message from queues with same routing key and exchangeName. 
        /// </summary>
        /// <param name="ExchangeName"></param>
        /// <param name="RoutingKey"></param>
        /// <param name="ObjectValue"></param>
        /// <returns></returns>
        public override async Task SendMessageToQueueRouting(string ExchangeName, string RoutingKey, T ObjectValue)
        {
            var factory = ConfigConnectionFactory();
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare(exchange: ExchangeName, type: ExchangeType.Direct);
                    channel.QueueDeclare(queue: "Excel",
                                durable: false,
                                exclusive: false,
                                autoDelete: false,
                                arguments: null);
                    var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(ObjectValue));
                    channel.BasicPublish(
                                 exchange: ExchangeName,
                                 routingKey: "Excel",
                                 basicProperties: null,
                                 body: body);
                    Console.WriteLine($"Enviado mensagem {JsonSerializer.Serialize(ObjectValue)} para o publicador {ExchangeName} com destino a rota {RoutingKey} com sucesso");
                    await Task.CompletedTask;
                }
            }
        }

        /// <summary>
        /// Publisher Direct to send message from queues with same routing key and exchangeName. 
        /// </summary>
        /// <param name="ExchangeName"></param>
        /// <param name="RoutingKey"></param>
        /// <param name="ObjectValue"></param>
        /// <returns></returns>
        public override async Task ReceiveMessageToQueueRouting(string ExchangeName, string RoutingKey)
        {
            var factory = ConfigConnectionFactory();
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            channel.ExchangeDeclare(exchange: ExchangeName, type: ExchangeType.Direct);
            var queueName = channel.QueueDeclare().QueueName;
            channel.QueueBind(queue: queueName, exchange: ExchangeName, routingKey: RoutingKey);
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var routingKey = ea.RoutingKey;
            };

            channel.BasicConsume(queue: queueName,
                                 autoAck: true,
                                 consumer: consumer);

            await Task.CompletedTask;
        }
    }
}
