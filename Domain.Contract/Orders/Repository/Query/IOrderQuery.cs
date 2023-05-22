using Domain.GenericRepositories;

namespace Domain.Contract.Orders.Repository.Query
{
    public interface IOrderQueryRepository : IQueryRepository<Order, IOrder>
    {
    }
}
