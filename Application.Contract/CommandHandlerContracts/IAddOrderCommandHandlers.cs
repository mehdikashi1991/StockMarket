using Application.Contract.Commands;
using Domain;

namespace Application.Contract.CommandHandlerContracts
{
    public interface IAddOrderCommandHandlers : ICommandHandler<AddOrderCommand>
    {
    }
}