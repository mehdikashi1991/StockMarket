using Domain.Orders.Repositories.Command;

namespace Domain.Contract.Trades.Repository.Command
{
    public interface ITradeCommandRepository : ICommandRepository<Trade, ITrade>
    {
    }
}
