using Core.DTOs;
using Core.Interfaces.Services;
using Infrastructure.Core.AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Linq;
using Web.Controllers;
using Web.Infrastructure;
using Web.Models.ViewModels;
using Xunit;

namespace SportStoreApp.Tests
{
    public class ProductControllerTests
    {
        SportStoreAutoMapper _mapper = new SportStoreAutoMapper();

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
            ProductsListViewModel actual = (controller.List(null, 2) as ViewResult).ViewData.Model as ProductsListViewModel;
            //assert
            ProductDto[] productDtos = actual.Products.ToArray();
            Assert.True(productDtos.Length == 1);
            Assert.Equal("P5", productDtos[0].Name);
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
                new ProductController(null, mock.Object) { PAGE_SIZE = 3 };

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

        [Fact]
        public void Can_Filter_Products()
        {
            //arrange
            Mock<IProductService> mock = new Mock<IProductService>();
            mock.Setup(m => m.GetAll()).Returns((new ProductDto[] {
                new ProductDto {ProductId = 1, Name = "P1", Category = "Cat1"},
                new ProductDto {ProductId = 2, Name = "P2", Category = "Cat2"},
                new ProductDto {ProductId = 3, Name = "P3", Category = "Cat1"},
                new ProductDto {ProductId = 4, Name = "P4", Category = "Cat2"},
                new ProductDto {ProductId = 5, Name = "P5", Category = "Cat3"}
            }).AsQueryable<ProductDto>());
            ProductController controller = new ProductController(_mapper, mock.Object);
            controller.PAGE_SIZE = 3;
            //act
            ProductDto[] result = (controller.List("Cat2", 1).ViewData.Model as ProductsListViewModel)
                    .Products.ToArray();

            // Assert
            Assert.Equal(2, result.Length);
            Assert.True(result[0].Name == "P2" && result[0].Category == "Cat2");
            Assert.True(result[1].Name == "P4" && result[1].Category == "Cat2");
        }
    }
}
