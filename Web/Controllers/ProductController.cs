using Core.Interfaces.Services;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using Web.Models.ViewModels;

namespace Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        public int PageSize = 4;

        public ProductController(IMapper mapper, IProductService productService)
        {
            _mapper = mapper;
            _productService = productService;
        }

        public ViewResult List(string category, int productPage = 1)
        {
            var productsDto = _productService.GetAll();
            var products = productsDto
                .Where(p => category == null || p.Category == category)
                .OrderBy(p => p.ProductId)
                .Skip((productPage - 1) * PageSize)
                .Take(PageSize);

            var pagingInfo = new PagingInfo
            {
                CurrentPage = productPage,
                ItemsPerPage = PageSize,
                TotalItems = category == null ?
                       productsDto.Count() :
                       productsDto.Where(e =>
                           e.Category == category).Count()
            };
            var productListViewModel = new ProductsListViewModel()
            {
                Products = products,
                PagingInfo = pagingInfo,
                CurrentCategory = category
            };

            return View(productListViewModel);
        }
    }
}
