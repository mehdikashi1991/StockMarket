using Microsoft.AspNetCore.Mvc;
using Domain;
using FacadeProvider;

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
            return Ok(await tradeQueryFacade.GetAllTrades());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTrade(long id)
        {
            return Ok(await tradeQueryFacade.GetTrade(id));
        }
    }
}
