using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FacadeProvider
{
    public interface IOrderQueryFacade
    {
        Task<IOrder> Get(long id);
    }
}
