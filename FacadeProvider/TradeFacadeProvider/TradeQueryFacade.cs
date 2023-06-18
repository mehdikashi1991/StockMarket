﻿using Domain.Contract.Trades.Repository.Query;
using Domain.Trades.Entities;
using Facade.Contract;
using Framework.Contracts.Common;

namespace FacadeProvider.TradeFacadeProvider
{
    public class TradeQueryFacade : ITradeQueryFacade
    {
        private readonly ITradeQueryRespository tradeQuery;

        public TradeQueryFacade(ITradeQueryRespository tradeQuery)
        {
            this.tradeQuery = tradeQuery;
        }
        public async Task<IEnumerable<ITrade>> GetAllTrades()
        {
            return await tradeQuery.GetAll().ConfigureAwait(false);
        }

        public async Task<PageResult<Trade>> GetAllTradesWithPaging(int page, int pageSize, int currentPage, long lastId)
        {
            return await tradeQuery.GetPaging(page, pageSize, currentPage, lastId).ConfigureAwait(false);
        }

        public async Task<ITrade> GetTrade(long id)
        {
            return await tradeQuery.Get(t => t.Id == id);
        }
    }
}
