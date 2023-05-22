
using Application.Contract.Commands;
using Domain;

namespace FacadeProvider
{
    public interface IOrderCommandFacade
    {
        Task<ProcessedOrder> ProcessOrder(AddOrderCommand orderCommand);
        Task<ProcessedOrder> ModifyOrder(ModifieOrderCommand orderCommand);
        Task<ProcessedOrder> CancelOrder(long id);
        Task<ProcessedOrder> CancelAllOrders();


    }
}