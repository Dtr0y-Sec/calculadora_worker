using System;
using System.Threading.Tasks;

namespace Calculadora_Worker.Domain.RabbitMQ
{
    public class RabbitMQResponseTask
    {

        #region [Properties]

        public bool Result { get; set; }
        public string msgError { get; set; }

        #endregion

        #region [Constructor]
        public RabbitMQResponseTask(bool result, string messageError)
        {
            this.Result = result;
            this.msgError = messageError;
        }

        #endregion

        #region [Methods]

        public static implicit operator Task<object>(RabbitMQResponseTask v)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

}
