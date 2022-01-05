using Calculadora_Worker.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculadora_Worker.Domain.RabbitMQ;
using Newtonsoft.Json;

namespace Calculadora_Worker.Queues
{
    public abstract class CreateAndConsumeQueue
    {
        private IModel channel;
        private IQueueConfiguration queueConfiguration;
        public CreateAndConsumeQueue(IConnection rabbitmqConnection, IQueueConfiguration _queueConfiguration)
        {
            Console.WriteLine($"Starting queue configuration: {_queueConfiguration.GetType()}");

            channel = rabbitmqConnection.CreateModel();
            queueConfiguration = _queueConfiguration;
            channel.QueueDeclare(queue: _queueConfiguration.Name, durable: _queueConfiguration.Durable, exclusive: _queueConfiguration.Exclusive, autoDelete: _queueConfiguration.AutoDelete, arguments: _queueConfiguration.Arguments);
            channel.BasicQos(0, 50, false);
            EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
            consumer.Received += ConsumerReceived;
            channel.BasicConsume(queue: _queueConfiguration.Name, autoAck: false, consumer: consumer);

            Console.WriteLine($"Finishing queue configuration: {_queueConfiguration.GetType()}");
        }

        public async void ConsumerReceived(object model, BasicDeliverEventArgs ea)
        {
            try
            {

                RabbitMQMessage msg = new RabbitMQMessage()
                {
                    Payload = Encoding.UTF8.GetString(ea.Body.Span)
                };

                RabbitMQResponseTask taskResult = await MessageProcessAsync(model, ea, msg);

                if (taskResult.Result)
                {
                    channel.BasicAck(ea.DeliveryTag, false);
                }
                else
                {
                    RabbitMQMessagePayloadBase originalMsg = JsonConvert.DeserializeObject<RabbitMQMessagePayloadBase>(msg.Payload);

                    originalMsg.msgError = taskResult.msgError;

                    msg.Payload = JsonConvert.SerializeObject(originalMsg);

                    List<byte> msgByte = new List<byte>();

                    foreach (char item in msg.Payload)
                    {
                        msgByte.Add(Convert.ToByte(item));
                    }

                    ea.Body = msgByte.ToArray();

                    channel.BasicNack(ea.DeliveryTag, false, false);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error on process msg: {queueConfiguration.GetType()}", ex.ToString());
                Console.WriteLine($"Finishing queue configuration: {queueConfiguration.GetType()}");
                throw ex;
            }
        }

        internal abstract Task<RabbitMQResponseTask> MessageProcessAsync(object model, BasicDeliverEventArgs ea, RabbitMQMessage msg);
    }
}
