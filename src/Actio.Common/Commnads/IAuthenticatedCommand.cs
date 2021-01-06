using System;

namespace Actio.Common.Commnads
{
    public interface IAuthenticatedCommand: ICommand
    {
        Guid UserId { get; set; }
    }
}