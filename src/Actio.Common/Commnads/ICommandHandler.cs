using System.Threading.Tasks;

namespace Actio.Common.Commnads
{
    public interface ICommandHandler<in T> where T : ICommand
    {
        // will be implemented in the services
        Task HandleAsync(T command);
    }
}