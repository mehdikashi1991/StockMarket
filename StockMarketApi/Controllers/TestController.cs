using Facade.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace StockMarketApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IOrderQueryFacade orderQuery;

        public TestController(IOrderQueryFacade orderQuery )
        {
            this.orderQuery = orderQuery;
        }
        [HttpGet]
        public async Task<IActionResult> Get(long id)
        {
            return Ok(await orderQuery.Get(id));
        }
    }
}
