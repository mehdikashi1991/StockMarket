using Domain.Orders.Repositories.Command;

namespace Domain.Contract.Orders.Repository.Command
{
    public interface IOrderCommandRepository : ICommandRepository<Order, IOrder>
    {
    }
}
