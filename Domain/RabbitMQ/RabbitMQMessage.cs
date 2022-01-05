using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculadora_Worker.Domain.RabbitMQ
{
    public class RabbitMQMessage
    {
        #region [Constructor]

        public RabbitMQMessage()
        {
            this.Header = new Dictionary<string, string>();
        }

        #endregion

        public Dictionary<string, string> Header { get; set; }
        public IDictionary<string, object> HeaderOriginal { get; set; }
        public string Payload { get; set; }
        public RabbitMQMessagePayloadBase PayloadObjectBase { get; set; }
        public string Id { get; set; }

        public string XDeathCount
        {
            get
            {
                string valor = null;
                this.Header.TryGetValue("x-death.count", out valor);
                return valor;
            }
        }

        public string XDeathExchange
        {
            get
            {
                string valor = null;
                this.Header.TryGetValue("x-death.exchange", out valor);
                return valor;
            }
        }

        public string XDeathQueue
        {
            get
            {
                string valor = null;
                this.Header.TryGetValue("x-death.queue", out valor);
                return valor;
            }
        }

        public string XDeathReason
        {
            get
            {
                string valor = null;
                this.Header.TryGetValue("x-death.reason", out valor);
                return valor;
            }
        }

        public string XDeathRoutingKeys
        {
            get
            {
                string valor = null;
                this.Header.TryGetValue("x-death.routing-keys", out valor);
                return valor;
            }
        }

        public string XDeathRoutingTime
        {
            get
            {
                string valor = null;
                this.Header.TryGetValue("x-death.routing-time", out valor);
                return valor;
            }
        }

        public string XDeathFirstDeathExchange
        {
            get
            {
                string valor = null;
                this.Header.TryGetValue("x-first-death-exchange", out valor);
                return valor;
            }
        }

        public string XDeathFirstDeathQueue
        {
            get
            {
                string valor = null;
                this.Header.TryGetValue("x-first-death-queue", out valor);
                return valor;
            }
        }

        public string XDeathFirstDeathReason
        {
            get
            {
                string valor = null;
                this.Header.TryGetValue("x-first-death-reason", out valor);
                return valor;
            }
        }
    }

    public class RabbitMQMessagePayloadBase
    {
        #region [Properties]

        public string tenant { get; set; }
        public object body { get; set; }
        public string status { get; set; }
        public string msgError { get; set; }

        #endregion
    }
}
