using Domain.Common;
using System.Linq.Expressions;

namespace Framework.Contracts.GenericRepositories
{
    public interface IQueryRepository<T, TInterface>
    {
        Task<TInterface?> Get(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<TInterface>> GetAll(Expression<Func<T, bool>>? predicate = null);
        Task<long> GetMax(Expression<Func<T, long?>> selector);
        Task<PageResult<T>> GetPaging(int page, int pageSize, int currentPage, long lastId);
    }
}
