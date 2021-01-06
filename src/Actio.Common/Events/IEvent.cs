using System.Threading.Tasks;

namespace Actio.Common.Events
{
    public interface IEventHandler <in T> where T: IEvent
    {
        Task HandleAsync(T @event);
    }
    // Marker inteface
    // For each of the commands we need to have an event
    public interface IEvent
    {
    }
}