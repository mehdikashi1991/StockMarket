using Domain;

namespace Application.Contract.CommandHandlerContracts
{
    public interface ICommandHandler<T1>
    {
        Task<ProcessedOrder?> Handle(T1 modifieOrcommandderCommand);
    }
}
