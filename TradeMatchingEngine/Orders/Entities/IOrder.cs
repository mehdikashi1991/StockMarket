﻿using Framework.Contracts.Common;

namespace Domain.Orders.Entities
{
    public interface IOrder : IBaseEntity<long>
    {
        int Amount { get; }
        DateTime ExpireTime { get; }
        bool HasCompleted { get; }
        long Id { get; }
        bool IsExpired { get; }
        bool? IsFillAndKill { get; }
        long? OrderParentId { get; }
        OrderStates? OrderState { get; }
        int? OriginalAmount { get; }
        int Price { get; }
        Side Side { get; }

        int DecreaseAmount(int amount);
        OrderStates GetOrderState();
        void SetStateCancelled();
        void SetStateModified();
        void SetStateRegistered();
        void UpdateBy(IOrder order);
    }
}