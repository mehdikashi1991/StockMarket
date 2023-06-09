﻿using Application.OrderService.OrderCommandHandlers;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;

using Xunit;

namespace Test
{
    public class AddOrderCommandHandlerTest
    {

        //[Fact]
        //public async Task Handle_ConcurrentThreadInPublishEvent()
        //{
        //    //Arrange
        //    var mockOrderCommand = new Mock<IOrderCommandRepository>();

        //    var buyOrder = new Order(1, Side.Buy, 100, 1, DateTime.Now.AddDays(1));

        //    mockOrderCommand.Setup(x => x.Add(buyOrder)).Returns(() => 1);

        //    var mockOrderQuery = new Mock<IOrderQuery>();
        //    mockOrderQuery.Setup(x => x.GetLastOrder()).ReturnsAsync(() => 0);
        //    var orderList = new List<Order>();
        //    mockOrderQuery.Setup(x => x.GetAllOrders()).ReturnsAsync(() => orderList);

        //    var mockTradeQuery = new Mock<ITradeQuery>();
        //    mockTradeQuery.Setup(x => x.GetLastTrade()).ReturnsAsync(() => 0);

        //    var mockUnitOfWork = new Mock<IUnitOfWork>();
        //    mockUnitOfWork.Setup(x => x.SaveChange()).ReturnsAsync(() => 1);

        //    var sut1 = new AddOrderCommandHandlers(
        //        mockOrderCommand.Object, mockOrderQuery.Object,
        //        mockTradeQuery.Object, mockTradeCommand.Object, mockUnitOfWork.Object);





        //    //Act
        //    var task1 = await sut1.Handle(100, 1, Side.Buy, DateTime.Now.AddDays(1), false);

        //    //Assert

        //    Assert.Equal(1, task1);

        //    Assert.NotNull(task1);
        //}

        //var mockOrder1Command = new Mock<IOrderCommandRepository>();

        //var sellOrder = new Order(2, Side.Sell, 101, 2, DateTime.Now.AddDays(1));

        //mockOrder1Command.Setup(x => x.Add(sellOrder)).Returns(() => 1);

        //var mockOrder1Query = new Mock<IOrderQuery>();
        //mockOrderQuery.Setup(x => x.GetLastOrder()).ReturnsAsync(() => 1);
        //var orderList1 = new List<Order>();
        //orderList1.Add(buyOrder);
        //mockOrderQuery.Setup(x => x.GetAllOrders()).ReturnsAsync(() => orderList1);

        //var mockTrade1Query = new Mock<ITradeQuery>();
        //mockTrade1Query.Setup(x => x.GetLastTrade()).ReturnsAsync(() => 0);


        //var sut2 = new AddOrderCommandHandlers(mockOrderCommand.Object, mockOrderQuery.Object, mockTradeQuery.Object, mockTradeCommand.Object, mockUnitOfWork.Object);
        //var task2 = Task.Run(() => sut2.Handle(101, 2, Side.Sell, DateTime.Now.AddDays(1), false));



        //var mockOrder2Command = new Mock<IOrderCommandRepository>();

        //var buy1Order = new Order(3, Side.Buy, 99, 1, DateTime.Now.AddDays(1));

        //mockOrderCommand.Setup(x => x.Add(buy1Order)).Returns(() => 1);

        //var sut3 = new AddOrderCommandHandlers(mockOrderCommand.Object, mockOrderQuery.Object, mockTradeQuery.Object, mockTradeCommand.Object, mockUnitOfWork.Object);
        //var task3 = Task.Run(() => sut1.Handle(99, 1, Side.Buy, DateTime.Now.AddDays(1), false));


        //[Fact]
        //public async Task Handle_ConcurrentThreadInPublishEvent1()
        //{
        //    //Arrage

        //    var buyOrder = new Order(1, Side.Buy, 100, 1, DateTime.Now.AddDays(1));


        //    var mockTradeCommand = new Mock<ITradeCommand>();
        //    mockTradeCommand.Setup(x => x.AddTrade(new Trade(1, 1, 2, 1, 100))).Returns(() => 0);

        //    var mockUnitOfWork = new Mock<IUnitOfWork>();
        //    mockUnitOfWork.Setup(x => x.SaveChange()).ReturnsAsync(() => 1);


        //    var sut1 = new AddOrderCommandHandlers(orderCommand, orderQuery, tradeQuery, mockTradeCommand.Object, mockUnitOfWork.Object);
        //    var task1 = Task.Run(() => sut1.Handle(100, 1, Side.Buy, DateTime.Now.AddDays(1), false));




        //    var sellOrder = new Order(2, Side.Sell, 101, 2, DateTime.Now.AddDays(1));


        //    var sut2 = new AddOrderCommandHandlers(orderCommand, orderQuery, tradeQuery, mockTradeCommand.Object, mockUnitOfWork.Object);
        //    var task2 = Task.Run(() => sut2.Handle(101, 2, Side.Sell, DateTime.Now.AddDays(1), false));





        //    var buy1Order = new Order(3, Side.Buy, 99, 1, DateTime.Now.AddDays(1));



        //    var sut3 = new AddOrderCommandHandlers(orderCommand, orderQuery, tradeQuery, mockTradeCommand.Object, mockUnitOfWork.Object);
        //    var task3 = Task.Run(() => sut1.Handle(99, 1, Side.Buy, DateTime.Now.AddDays(1), false));



        //    //Action
        //    await Task.WhenAll(task1, task2, task3);

        //    //Assert

        //}

    }
}
