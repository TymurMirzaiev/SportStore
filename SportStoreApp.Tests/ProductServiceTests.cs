using Core.DTOs;
using Core.Services;
using Data.Entities;
using Data.Repositories;
using Infrastructure.Core.AutoMapper;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SportStoreApp.Tests
{
    public class ProductServiceTests
    {
        SportStoreAutoMapper _mapper = new SportStoreAutoMapper();

        [Fact]
        public void GetById_IsCorrect()
        {
            //arrange
            ProductDto expected = new ProductDto { ProductId = 2, Name = "2" };
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(p => p.Get())
                .Returns((new Product[]{
                    new Product { ProductId = 2, Name = "2"},
                    new Product { ProductId = 2, Name = "3"}
                }).AsQueryable());
            //act
            ProductService productService = new ProductService(_mapper, mock.Object);
            ProductDto actual = productService.GetById(2);
            //assert
            Assert.Equal(expected.Name, actual.Name);
            mock.Verify(p => p.Get(), Times.Once);
        }

        [Fact]
        public void GetAll_IsCorrect()
        {
            //arrange
            IEnumerable<ProductDto> expected = new ProductDto[]
            {
                new ProductDto { ProductId = 2, Name = "2"},
                new ProductDto { ProductId = 3, Name = "3"}
            };
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(p => p.Get())
                .Returns((new Product[]{
                    new Product { ProductId = 2, Name = "2"},
                    new Product { ProductId = 3, Name = "3"}
                }).AsQueryable());
            //act
            ProductService productService = new ProductService(_mapper, mock.Object);
            IEnumerable<ProductDto> actual = productService.GetAll();
            //assert
            Assert.Equal(expected.Count(), actual.Count());
            mock.Verify(p => p.Get(), Times.Once);
        }
    }
}
