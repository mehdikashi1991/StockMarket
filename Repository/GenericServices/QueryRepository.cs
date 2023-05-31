using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Framework.Contracts.GenericRepositories;
using Framework.Contracts.Common;

namespace Infrastructure.GenericServices
{
    public class QueryRepository<T, TInterface> : IQueryRepository<T, TInterface> where T : class, IBaseEntity<long>, TInterface
    {
        protected readonly DbContext _dbContext;
        protected readonly IQueryable<T> _querySet;
        public QueryRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _querySet = dbContext.Set<T>().AsNoTracking();
        }
        public async Task<IEnumerable<TInterface>> GetAll(Expression<Func<T, bool>>? predicate = null)
        {
            return await (predicate != null ? _querySet.Where(predicate) : _querySet).ToListAsync();
        }

        public async Task<TInterface?> Get(Expression<Func<T, bool>> predicate)
        {
            return await _querySet.Where(predicate).FirstOrDefaultAsync();
        }

        public async Task<long> GetMax(Expression<Func<T, long?>> selector)
        {
            return await _querySet.MaxAsync(selector) ?? 0;
        }

        public async Task<PageResult<T>> GetPaging(int page, int pageSize, int currentPage = 1, long lastId = 0)
        {
            var result = new PageResult<T>();
            result.CurrentPage = page;
            result.PageSize = pageSize;

            var change = Math.Abs(currentPage - page);
            var skip = (page - 1) * pageSize;
            if (change > 1)
            {
                result.RowCount = _querySet.Count();
                var pageCount = (double)result.RowCount / pageSize;
                result.PageCount = (int)Math.Ceiling(pageCount);
                result.Result = await _querySet.OrderBy(i => i.Id).Skip(skip).Take(pageSize).ToListAsync();
                return result;
            }

            if (currentPage > page)
                lastId -= (2 * pageSize);

            result.Result = await _querySet.Where(i => i.Id > lastId).OrderBy(i => i.Id).Take(pageSize).ToListAsync();

            return result;

        }
    }
}
