﻿using Application.Contract.Commands;
using Controllers;
using Controllers.Model;
using Facade.Contract;
using Microsoft.AspNetCore.Mvc;

namespace EndPoints.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {

        private readonly IOrderCommandFacade orderFacade;
        private readonly IOrderQueryFacade _orderQueryFacade;

        public OrdersController
            (
            IOrderCommandFacade orderFacade,
            IOrderQueryFacade orderQueryFacade

            )
        {
            this.orderFacade = orderFacade;
            _orderQueryFacade = orderQueryFacade;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderVM"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> ProcessOrder([FromBody] OrderVM orderVM)
        {
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
               OutputGenerator.ProcessOrderLink(
                   await orderFacade.ProcessOrder(command).ConfigureAwait(false)));
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

            var result = await orderFacade.ModifyOrder(modifieCommand).ConfigureAwait(false);

            if (result != null)
            {
                return AcceptedAtAction("ModifyOrder",
                              "Orders",
                null, OutputGenerator.ModifyOrderLink(result));
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
        public async Task<IActionResult> CancellOrder(CancelOrderCommand command)
        {
            try
            {
                var result = await orderFacade.CancelOrder(command).ConfigureAwait(false);

                if (result != null)
                {
                    return AcceptedAtAction(
                                           "CancellOrder",
                                         "Orders",
                                            null, OutputGenerator.CancelOrderLink(result));

                }

                return BadRequest(command.Id);
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
                        null, OutputGenerator.CancellAllOrdersLink(result));
            }

            return BadRequest();
        }


        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrder(long orderId)
        {
            var result = await _orderQueryFacade.Get(orderId).ConfigureAwait(false);
            return AcceptedAtAction("GetOrder", "Orders", OutputGenerator.GetOrderLink(result));
        }
    }
}
