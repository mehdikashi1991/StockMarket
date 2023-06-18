using Domain.Orders.Entities;
using Framework.Contracts;

namespace Domain
{
    public class StockMarketBlockingQueueDecorator : StockMarketMatchEngine
    {
        private readonly BlockingQueue queue;
        public StockMarketBlockingQueueDecorator(
            List<Order>? orders = null,
            long lastOrderId = 0,
            long lastTradeId = 0) : base(orders, lastOrderId, lastTradeId)
        {
            queue = new BlockingQueue();

        }


        private async Task<T?> executeAsync<T>(Func<Task<T>> function)
        {
            return await queue.ExecuteAsync(async () =>
            {
                //setupEvents(events);
                //try
                //{
                return await function().ConfigureAwait(false);
                //}
                //finally
                //{
                //    clearEvents();
                //}
            }).ConfigureAwait(false);
        }
        public virtual async Task<IStockMarketMatchingEngineProcessContext?> CancelOrderAsync(long orderId)
        {
            return await executeAsync(() => Task.FromResult(cancelOrderAsync(orderId, null))).ConfigureAwait(false);
        }
        public virtual async Task<IStockMarketMatchingEngineProcessContext?> ModifieOrder(long orderId, int price, int amount, DateTime? expirationDate)
        {
            return await executeAsync(() => Task.FromResult(modifieOrder(orderId, price, amount, expirationDate))).ConfigureAwait(false);
        }

        public virtual async Task<IStockMarketMatchingEngineProcessContext?> PreModifieOrder(long orderId, int price, int amount, DateTime? expirationDate)
        {
            return await executeAsync(() => Task.FromResult(preModifieOrder(orderId, price, amount, expirationDate))).ConfigureAwait(false);
        }

        public virtual async Task<IStockMarketMatchingEngineProcessContext> ProcessOrderAsync(int price, int amount, Side side, DateTime? expireTime = null, bool? fillAndKill = null, long? orderParentId = null)
        {
            return await executeAsync(() =>
            {
                return Task.FromResult(processOrderAsync(price, amount, side, expireTime, fillAndKill, orderParentId));
            }).ConfigureAwait(false);
        }

        public async ValueTask DisposeAsync()
        {
            await queue.DisposeAsync().ConfigureAwait(false);
        }

    }
}
