using Calculadora_Worker.Domain.RabbitMQ;
using Calculadora_Worker.DTO;
using Calculadora_Worker.Interfaces;
using Calculadora_Worker.Queues;
using Calculadora_Worker.Services;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Threading.Tasks;

namespace Calculadora_Worker.Listerners
{
    public class SumOperationListener: CreateAndConsumeQueue
    {
        public SumOperationListener(IConnection _rabbitmqConnection, IQueueConfiguration _queueConfiguration): base (_rabbitmqConnection, _queueConfiguration) {}

        internal override async Task<RabbitMQResponseTask> MessageProcessAsync(object _model, BasicDeliverEventArgs _eventArgs, RabbitMQMessage _message)
        {

            SumHandler Payload = JsonConvert.DeserializeObject<SumHandler>(_message.Payload);

            SumOperationService sumOperationService = new SumOperationService();

            return await sumOperationService.SumOperation(Payload._Id);

        }
    }
}
