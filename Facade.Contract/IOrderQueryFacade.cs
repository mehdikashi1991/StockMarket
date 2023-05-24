using Domain.Orders.Entities;


namespace Facade.Contract
{
    public interface IOrderQueryFacade
    {
        Task<IOrder> Get(long id);
    }
}
