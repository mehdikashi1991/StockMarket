using Controllers;
using Facade.Contract;
using Microsoft.AspNetCore.Mvc;

namespace EndPoints.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class TradesController : ControllerBase
    {
        private readonly ITradeQueryFacade tradeQueryFacade;

        public TradesController(ITradeQueryFacade tradeQueryFacade)
        {
            this.tradeQueryFacade = tradeQueryFacade;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllTrades()
        {
            var result = await tradeQueryFacade.GetAllTrades().ConfigureAwait(false);
            return AcceptedAtAction("GetAllTrade", "Trades", OutputGenerator.GetAllTradeLink(result));
        }

        [HttpGet("{page}/{pageSize}/{currentPage}/{lastId}")]
        public async Task<IActionResult> GetAllTradesWithPaging(int page, int pageSize, int currentPage, long lastId)
        {
            var result = await tradeQueryFacade.GetAllTradesWithPaging(page, pageSize, currentPage, lastId).ConfigureAwait(false);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTrade(long id)
        {
            return Ok(await tradeQueryFacade.GetTrade(id).ConfigureAwait(false));
        }
    }
}
