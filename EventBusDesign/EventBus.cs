using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EventBusDesign
{
    public class EventBus : IEventBus
    {
        protected readonly ConcurrentDictionary<Type, ConcurrentBag<Type>> _handlerMaps;  //ConcurrentDictionary<IEventData,
        protected IServiceProvider _serviceProvider;
        protected readonly Type _eventHandlerType;
        public EventBus() : this(null)
        {

        }
        public void SetServiceProvider(IServiceProvider serviceProvider)
        {
            if (_serviceProvider == null)
            {
                _serviceProvider = serviceProvider;
            }
        }
        public EventBus(IServiceProvider serviceProvider)
        {
            _handlerMaps = new ConcurrentDictionary<Type, ConcurrentBag<Type>>();
            _serviceProvider = serviceProvider;
            _eventHandlerType = typeof(IEventHandler);
            Init();
        }
        public void Init()
        {
            AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(p => p.GetTypes())
                .Where(p => _eventHandlerType
                .IsAssignableFrom(p))
                .ToList()
                .ForEach(p => Subscribe(p));
        }
        public virtual void Publish<TEvent>(TEvent @event) where TEvent : IEventData
        {
            var eventType = typeof(TEvent);
            if (_handlerMaps.TryGetValue(eventType, out var handlers))
            {
                if (handlers.Count == 0)
                {
                    return;
                }
                if (_serviceProvider == null)
                {
                    throw new InvalidOperationException("service provider can not be null");
                }
                using (var scope = _serviceProvider.CreateScope())
                {
                    foreach (var handler in handlers)
                    {
                        var instance = scope.ServiceProvider.GetService(handler) as IEventHandler<TEvent>;
                        if (instance != null)
                        {
                            try
                            {
                                instance.Handle(@event);
                            }
                            catch (Exception ex)
                            {

                            }
                        }
                    }
                }

            }
        }

        public virtual void Subscribe(Type eventHandlerType)
        {
            ExecCore(eventHandlerType, eventType =>
            {
                var handlers = _handlerMaps.GetOrAdd(eventType, t => new ConcurrentBag<Type>());
                if (!handlers.Any(p => p == eventHandlerType))
                {
                    handlers.Add(eventHandlerType);
                    Console.WriteLine(eventHandlerType.Name + "Subscribe");
                }
            });
        }

        public virtual void UnSubscribe(Type eventHandlerType)
        {
            ExecCore(eventHandlerType, eventType =>
            {
                if (_handlerMaps.TryGetValue(eventType, out var handlers))
                {
                    handlers.TryTake(out var handler);
                }
            });
        }
        protected void ExecCore(Type eventHandlerType, Action<Type> operation)
        {
            if (_eventHandlerType.IsAssignableFrom(eventHandlerType))
            {
                var handlerInterface = eventHandlerType.GetInterface("IEventHandler`1");
                if (handlerInterface != null)
                {
                    var eventType = handlerInterface.GetGenericArguments()[0];
                    operation(eventType);
                }
            }
        }
    }
}
