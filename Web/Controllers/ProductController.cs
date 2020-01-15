using Core.Interfaces.Services;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using Web.Infrastructure;
using Web.Models.ViewModels;

namespace Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        public int PAGE_SIZE = 4;

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
                .Skip((productPage - 1) * PAGE_SIZE)
                .Take(PAGE_SIZE);

            var pagingInfo = new PagingInfo
            {
                CurrentPage = productPage,
                ItemsPerPage = PAGE_SIZE,
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
