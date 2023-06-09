﻿using Domain;
using Domain.Orders.Entities;
using Domain.Trades.Entities;
using Hal;
using Hal.Builders;

namespace Controllers
{
    public static class OutputGenerator
    {
        public static Resource ProcessOrderLink(ProcessedOrder order)
        {
            var builder = new ResourceBuilder();

            var resource = builder
                .WithState(order)
                .AddSelfLink().WithLinkItem("/Orders")
                .AddLink("find").WithLinkItem("/Orders/{id}", null, true)
                .AddLink("get").WithLinkItem($"/Orders/{order.OrderId}", "getOrder", null, "GET")
                .AddLink("modify").WithLinkItem($"/Orders/{order.OrderId}", "modifyOrder", null, "PUT")
                .AddLink("cancel").WithLinkItem($"/Orders/{order.OrderId}", "cancelOrder", null, "DELETE")
                .AddLink("cancel").WithLinkItem($"/Orders", "cancelAllOrders", null, "DELETE")
                .AddEmbedded("trades")
                .Resource(new ResourceBuilder()
                .WithState(order.Trades)
                .AddSelfLink().WithLinkItem("/Trades")
                .AddLink("find").WithLinkItem("/Trades/{id}", null, true, "GET")
                .AddLink("get").WithLinkItem("/Trades", "getAllTrades", null, "GET"))
                .Build();

            return resource;
        }

        public static Resource ModifyOrderLink(ProcessedOrder order)
        {
            var builder = new ResourceBuilder();

            var resource = builder
                .WithState(order)
                .AddSelfLink().WithLinkItem("/Orders")
                .AddLink("find").WithLinkItem("/Orders/{id}", null, true)
                .AddLink("cancel").WithLinkItem($"/Orders/{order.OrderId}", "cancelOrder", null, "DELETE")
                .AddLink("cancel").WithLinkItem($"/Orders", "cancelAllOrders", null, "DELETE")
                .AddLink("get").WithLinkItem($"/Orders/{order.OrderId}", "getOrder", null, "GET")
                .AddLink("add").WithLinkItem($"/Orders", "processOrder", null, "POST")
                .AddEmbedded("trades")
                .Resource(new ResourceBuilder()
                .WithState(order.Trades)
                .AddSelfLink().WithLinkItem("/Trades")
                .AddLink("find").WithLinkItem("/Trades/{id}", null, true, "GET")
                .AddLink("get").WithLinkItem("/Trades", "getAllTrades", null, "GET"))
                .Build();

            return resource;
        }

        public static Resource CancelOrderLink(ProcessedOrder order)
        {
            var builder = new ResourceBuilder();

            var resource = builder
                .WithState(order)
                .AddSelfLink().WithLinkItem("/Orders")
                .AddLink("find").WithLinkItem("/Orders/{id}", null, true)
                .AddLink("add").WithLinkItem($"/Orders", "processOrder", null, "POST")
                .Build();

            return resource;
        }

        public static Resource CancellAllOrdersLink(ProcessedOrder order)
        {
            var builder = new ResourceBuilder();

            var resource = builder
                .WithState(order)
                .AddSelfLink().WithLinkItem("/Orders")
                .AddLink("add").WithLinkItem($"/Orders", "processOrder", null, "POST")
                .Build();

            return resource;
        }

        public static Resource GetOrderLink(IOrder order)
        {
            var builder = new ResourceBuilder();

            var resource = builder
                .WithState(order)
                .AddSelfLink().WithLinkItem("/Orders")
                .AddLink("find").WithLinkItem("/Orders/{id}", null, true)
                .AddLink("cancel").WithLinkItem($"/Orders/{order.Id}", "cancelOrder", null, "DELETE")
                .AddLink("cancel").WithLinkItem($"/Orders", "cancelAllOrders", null, "DELETE")
                .AddLink("modify").WithLinkItem($"/Orders/{order.Id}", "modifyOrder", null, "PUT")
                .Build();

            return resource;
        }

        public static Resource GetAllTradeLink(IEnumerable<ITrade> trade)
        {
            var builder = new ResourceBuilder();

            var resource = builder
                .WithState(trade)
                .AddSelfLink().WithLinkItem("/Trades")
                .AddLink("find").WithLinkItem("/Trades/{id}", null, true)
                .Build();

            return resource;
        }
    }
}
