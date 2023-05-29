using Domain;

namespace Application.Contract.CommandHandlerContracts
{
    public interface ICancellOrderCommandHandler : ICommandHandler<long>
    {
    }
}