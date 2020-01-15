using Core.DTOs;
using Core.Interfaces.Services;
using Infrastructure.Core.AutoMapper;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Web.Controllers;
using Web.Models;
using Xunit;

namespace SportStoreApp.Tests
{
    public class CartControllerTests
    {
        SportStoreAutoMapper _mapper = new SportStoreAutoMapper();

        [Fact]
        public void AddToCart_IsCorrect()
        {
            //arrange
            ProductDto product = new ProductDto { ProductId = 1, Name = "product" };
            Mock<Cart> mockCart = new Mock<Cart>();
            mockCart.Setup(c => c.Lines)
                .Returns(new CartLineDto[]
                {
                    new CartLineDto { CartLineID = 1, Quantity = 1, Product = product }
                });
            Mock<IProductService> mockProductService = new Mock<IProductService>();
            mockProductService.Setup(p => p.GetById(It.IsAny<int>()))
                .Returns(new ProductDto { ProductId = 1 });
            //act
            CartController cartController = new CartController(_mapper, mockProductService.Object, 
                null, mockCart.Object);
            cartController.AddToCart(1, null);
            //assert
            mockCart.Verify(m => m.AddItem(It.IsAny<ProductDto>(), It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public void RemoveFromCart_IsCorrect()
        {
            //arrange
            ProductDto product = new ProductDto { ProductId = 1, Name = "product" };
            Mock<Cart> mockCart = new Mock<Cart>();
            mockCart.Setup(c => c.Lines)
                .Returns(new CartLineDto[]
                {
                    new CartLineDto { CartLineID = 1, Quantity = 1, Product = product }
                });
            Mock<IProductService> mockProductService = new Mock<IProductService>();
            mockProductService.Setup(p => p.GetById(It.IsAny<int>()))
                .Returns(new ProductDto { ProductId = 1 });
            //act
            CartController cartController = new CartController(_mapper, mockProductService.Object,
                null, mockCart.Object);
            cartController.RemoveFromCart(1, "");
            //assert
            mockCart.Verify(m => m.RemoveLine(It.IsAny<ProductDto>()), Times.Once);
        }
    }
}
