using Core.DTOs;
using Core.Services;
using Data.Entities;
using Data.Repositories;
using Infrastructure.Core.AutoMapper;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace SportStoreApp.Tests
{
    public class OrderServiceTests
    {
        SportStoreAutoMapper _mapper = new SportStoreAutoMapper();

        [Fact]
        public void GetOrdersShipped_IsCorrect()
        {
            //arrange
            OrderDto expected = new OrderDto { OrderId = 2, Shipped = false, Name = "A" };
            Mock<IOrderRepository> mock = new Mock<IOrderRepository>();
            mock.Setup(p => p.Get())
                .Returns((new Order[]{
                    new Order{ OrderId = 2, Shipped = false, Name = "A" },
                    new Order{ OrderId = 2, Shipped =  false, Name = "B" }
                }).AsQueryable());
            mock.Setup(p => p.SaveOrder(It.IsAny<Order>()));
            //act
            OrderService orderService = new OrderService(_mapper, mock.Object);
            OrderDto actual = orderService.SaveOrder(2);
            //assert
            Assert.Equal(expected.Name, actual.Name);
            Assert.True(actual.Shipped == true);
            mock.Verify(p => p.Get(), Times.Once);
            mock.Verify(p => p.SaveOrder(It.IsAny<Order>()), Times.Once);
        }


    }
}
