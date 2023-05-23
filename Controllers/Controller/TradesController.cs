using Microsoft.AspNetCore.Mvc;
using Domain;
using FacadeProvider;
using Controllers;

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
            var result = await tradeQueryFacade.GetAllTrades();
            return AcceptedAtAction("GetAllTrade", "Trades", OutputGenerator.GetAllTradeLink(result));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTrade(long id)
        {
            return Ok(await tradeQueryFacade.GetTrade(id));
        }
    }
}
