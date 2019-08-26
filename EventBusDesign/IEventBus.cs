using System;
using System.Collections.Generic;
using System.Text;

namespace EventBusDesign
{
    /// <summary>
    /// 事件总线设计
    /// </summary>
    public interface IEventBus
    {
        void Subscribe(Type eventhandlerType);
        void UnSubscribe(Type eventHandlerType);
        void Publish<TEvent>(TEvent @event) where TEvent : IEventData;
    }
}
