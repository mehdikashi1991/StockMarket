using Domain;
using Domain.Common;

namespace FacadeProvider
{
    public interface IOrderQueryFacade
    {
        Task<IOrder> Get(long id);
        //Task<PageResult<Order>> GetAllWithPaging(int page, int pageSize, int currentPage = 1, long lastId = 0);
    }
}
