using Calculadora_Worker.Interfaces;
using System;
using System.Collections.Generic;

namespace Calculator.Worker.Queues.SumOperation
{
    public class SumOperationQueue : IQueueConfiguration
    {
        #region [Constructor]

        IQueueConfiguration SumOperationQueueConfiguration;

        public SumOperationQueue()
        {
            this.SumOperationQueueConfiguration = new SumOperationDeadLetterQueue();
        }

        #endregion

        #region [Properties]

        public string Name
        {
            get { return "sumoperation.queue"; }
        }

        public string Key
        {
            get { return ""; }
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
            get
            {
                var args = new Dictionary<String, Object>();
                args.Add("x-dead-letter-exchange", SumOperationQueueConfiguration.Name);
                args.Add("x-dead-letter-routing-key", SumOperationQueueConfiguration.Key);
                return args;
            }
        }

        public List<int> retryConfig
        {
            get { return null; }
        }

        #endregion
    }
}
