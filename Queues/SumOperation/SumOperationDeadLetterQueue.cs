using Calculadora_Worker.Interfaces;
using System.Collections.Generic;

namespace Calculator.Worker.Queues.SumOperation
{
    public class SumOperationDeadLetterQueue : IQueueConfiguration
    {
        public string Name
        {
            get { return "sumoperation.exchange.dead-letter"; }
        }
        public string Key
        {
            get { return "sumoperation.queue.dead-letter"; }
        }

        public bool Durable
        {
            get { return true; }
        }

        public bool Exclusive
        {
            get { return false; }
        }

        public bool AutoDelete
        {
            get { return false; }
        }

        public Dictionary<string, object> Arguments
        {
            get { return null; }
        }

        public List<int> retryConfig
        {
            get
            {
                var retryCfg = new List<int>();
                retryCfg.Add(60 * 1); //1 minutos
                retryCfg.Add(60 * 15); //15 minutos
                retryCfg.Add(60 * 30); //30 minutos
                return retryCfg;
            }
        }
    }
}
