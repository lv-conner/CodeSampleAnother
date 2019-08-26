using System;
using System.Collections.Generic;
using System.Text;

namespace EventBusDesign
{
    /// <summary>
    /// 事件处理器
    /// </summary>
    public interface IEventHandler
    {
        
    }
    public interface IEventHandler<TEvent> where TEvent:IEventData
    {
        void Handle(TEvent @event);
    }
}
