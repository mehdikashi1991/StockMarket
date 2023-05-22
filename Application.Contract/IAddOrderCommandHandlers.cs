using Application.Contract.Commands;
using Domain;

namespace Application.Contract
{
    public interface IAddOrderCommandHandlers : ICommandHandler<AddOrderCommand>
    {
    }
}