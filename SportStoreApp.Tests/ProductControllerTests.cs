using Core.DTOs;
using Core.Interfaces.Services;
using Data.Entities;
using Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Web.Controllers;
using Web.Models.ViewModels;
using Xunit;

namespace SportStore.Tests
{
    public class ProductControllerTests
    {
        [Fact]
        public void Can_Paginate()
        {
            //arrange
            Mock<IProductService> mock = new Mock<IProductService>();
            mock.Setup(rn => rn.GetAll()).Returns((new ProductDto[]
            {
                new ProductDto { ProductId = 1, Name= "P1" },
                new ProductDto { ProductId = 2, Name= "P2" },
                new ProductDto { ProductId = 3, Name= "P3" },
                new ProductDto { ProductId = 4, Name= "P4" },
                new ProductDto { ProductId = 5, Name= "P5" }
            }).AsQueryable<ProductDto>());
            ProductController controller = new ProductController(null, mock.Object);
            //act
            IEnumerable<ProductDto> actual = (controller.List(null, 2) as ViewResult).ViewData.Model as IEnumerable<ProductDto>;
            //assert
            ProductDto[] productDtos = actual.ToArray();
            Assert.True(productDtos.Length == 2);
            Assert.Equal("P3", productDtos[0].Name);
            Assert.Equal("P4", productDtos[1].Name);
        }

        [Fact]
        public void Can_Send_Pagination_View_Model()
        {

            // Arrange
            Mock<IProductService> mock = new Mock<IProductService>();
            mock.Setup(rn => rn.GetAll()).Returns((new ProductDto[]
            {
                new ProductDto { ProductId = 1, Name= "P1" },
                new ProductDto { ProductId = 2, Name= "P2" },
                new ProductDto { ProductId = 3, Name= "P3" },
                new ProductDto { ProductId = 4, Name= "P4" },
                new ProductDto { ProductId = 5, Name= "P5" }
            }).AsQueryable<ProductDto>());

            // Arrange
            ProductController controller =
                new ProductController(null, mock.Object) { PageSize = 3 };

            // Act
            ProductsListViewModel result =
                (controller.List(null, 2) as ViewResult).ViewData.Model as ProductsListViewModel;

            // Assert
            PagingInfo pageInfo = result.PagingInfo;
            Assert.Equal(2, pageInfo.CurrentPage);
            Assert.Equal(3, pageInfo.ItemsPerPage);
            Assert.Equal(5, pageInfo.TotalItems);
            Assert.Equal(2, pageInfo.TotalPages);
        }
    }
}
