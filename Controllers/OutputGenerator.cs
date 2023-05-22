using Domain;
using Hal;
using Hal.Builders;

namespace Controllers
{
    public static class OutputGenerator
    {
        public static Resource ProcessOrderLink(ProcessedOrder order)
        {
            var builder = new ResourceBuilder();

            var resource = builder.WithState(order)
                .AddSelfLink().WithLinkItem("/Orders")
                .AddCuriesLink().WithLinkItem("http://example.com/docs/rels/{rel}", "ea", true)
                .AddLink("ea:find").WithLinkItem($"/Orders/{order.OrderId}")
                .AddEmbedded("ea:trades")
                    .Resource(new ResourceBuilder()
                    .WithState(order.Trades)
                    .AddSelfLink().WithLinkItem("/Trades")
                    .AddLink("ea:trade").WithLinkItem(href: $"/Trades"))
                    .Build();

            return resource;
        }
    }
}
