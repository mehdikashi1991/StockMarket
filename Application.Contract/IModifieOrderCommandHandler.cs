using Application.Contract.Commands;
using Domain;

namespace Application.Contract
{
    public interface ICommandHandler<T1>
    {
        Task<ProcessedOrder?> Handle(T1 modifieOrcommandderCommand);
    }
    public interface IModifieOrderCommandHandler : ICommandHandler<ModifieOrderCommand>
    {
    }
}
