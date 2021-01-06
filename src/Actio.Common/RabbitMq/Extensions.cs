using System.Reflection;
using System.Threading.Tasks;
using Actio.Common.Commnads;
using Actio.Common.Events;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RawRabbit;
using RawRabbit.Instantiation;
using RawRabbit.Pipe;
using RawRabbit.Pipe.Middleware;

namespace Actio.Common.RabbitMq
{
    public static class Extensions
    {
        public static Task WithCommandHandlerAsync<TCommand>(this IBusClient bus, ICommandHandler<TCommand> handler)
            where TCommand : ICommand
        {
            return bus.SubscribeAsync<TCommand>(
                msg => handler.HandleAsync(msg),
                ctx => ctx.UseConsumeConfiguration(cfg => cfg.FromQueue(GetQueueName<TCommand>()))
            );
        }

        public static Task WithEventHandlerAsync<TEvent>(this IBusClient bus, IEventHandler<TEvent> handler)
            where TEvent : IEvent
        {
            return bus.SubscribeAsync<TEvent>(
                msg => handler.HandleAsync(msg),
                ctx => ctx.UseConsumeConfiguration(cfg => cfg.FromQueue(GetQueueName<TEvent>()))
            );
        }

        public static string GetQueueName<T>()
        {
            return $"{Assembly.GetEntryAssembly()?.GetName()}/{typeof(T).Name}";
        }

        public static void AddRabbitMq(this IServiceCollection service, IConfiguration configuration)
        {
            var option = new RabbitMqOptions();
            var section = configuration.GetSection("rabbitmq");
            section.Bind(option);
            var client = RawRabbitFactory.CreateSingleton(new RawRabbitOptions()
            {
                ClientConfiguration = option
            });
            
            service.AddSingleton<IBusClient>(_ => client);
        }
    }
}