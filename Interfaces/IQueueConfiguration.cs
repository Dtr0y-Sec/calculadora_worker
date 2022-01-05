using System;
using System.Collections.Generic;

namespace Calculadora_Worker.Interfaces
{
    public interface IQueueConfiguration
    {
        string Name { get; }
        string Key { get; }
        bool Durable { get; }
        bool Exclusive { get; }
        bool AutoDelete { get; }
        Dictionary<String, Object> Arguments { get; }
        List<int> retryConfig { get; }
    }
}
