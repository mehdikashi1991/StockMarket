using Application.Contract.Commands;
using Controllers.Model;
using Facade.Contract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EndPoints.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderQueryFacade _orderQueryFacade;
        private readonly IOrderCommandFacade orderFacade;
        private readonly ILogger<OrdersController> _logger;

        public OrdersController
            (
            IOrderCommandFacade orderFacade,
            IOrderQueryFacade orderQueryFacade,
            ILogger<OrdersController> logger

            )
        {
            this.orderFacade = orderFacade;
            _orderQueryFacade = orderQueryFacade;
            _logger = logger;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="orderVM"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> ProcessOrder([FromBody] OrderVM orderVM)
        {
            _logger.LogError("ProcessOrder");

            var command = new AddOrderCommand()
            {
                Amount = orderVM.Amount,
                ExpDate = orderVM.ExpireTime,
                Side = orderVM.Side,
                Price = orderVM.Price,
                IsFillAndKill = (bool)orderVM.IsFillAndKill,
            };

            return CreatedAtAction(
               "ProcessOrder",
                "Orders",
                null,
                await orderFacade.ProcessOrder(command));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="modifieOrderVM"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpPut]
        public async Task<IActionResult> ModifyOrder([FromBody] ModifiedOrderVM modifieOrderVM)
        {
            var modifieCommand = new ModifieOrderCommand()
            {
                Amount = modifieOrderVM.Amount,
                Price = modifieOrderVM.Price,
                OrderId = modifieOrderVM.OrderId,
                ExpDate = modifieOrderVM.ExpDate,
            };

            var result = await orderFacade.ModifyOrder(modifieCommand);

            if (result != null)
            {
                return AcceptedAtAction("ModifyOrder",
                              "Orders",
                null, result.OrderId);
            }

            return BadRequest(modifieOrderVM);
        }

        /// <summary>
        /// CancellOrder
        /// </summary>
        /// <param name="handler"></param>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [HttpDelete("{orderId}")]
        public async Task<IActionResult> CancellOrder(long orderId)
        {
            try
            {
                var result = await orderFacade.CancelOrder(orderId);

                if (result != null)
                {
                    return AcceptedAtAction(
                                           "CancellOrder",
                                         "Orders",
                                            null, result.OrderId);
                }

                return BadRequest(orderId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// CancellAllOrders
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> CancellAllOrders()
        {
            var result = await orderFacade.CancelAllOrders(null);

            if (result != null)
            {
                return AcceptedAtAction(
                    "CancellAllOrders",
                      "Orders",
                        null, result.CancelledOrders);
            }

            return BadRequest();
        }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrder(long orderId)
        {
            return Ok(await _orderQueryFacade.Get(orderId));
        }
    }
}