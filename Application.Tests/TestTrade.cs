using Domain.Trades.Entities;
using Framework.Contracts.Events;
using System.Collections.Generic;

namespace Application.Tests
{
    public class TestTrade : ITrade
    {
        public int Amount { get; set; }

        public long BuyOrderId { get; set; }

        public int Price { get; set; }

        public long SellOrderId { get; set; }

        public long Id { get; set; }

        public IEnumerable<IDomainEvent> DomainEvents => throw new System.NotImplementedException();

        public void AddDomainEvent(IDomainEvent eventItem)
        {
            throw new System.NotImplementedException();
        }

        public void ClearEvents()
        {
            throw new System.NotImplementedException();
        }

        public void RemoveDomainEvent(IDomainEvent eventItem)
        {
            throw new System.NotImplementedException();
        }
    }
}