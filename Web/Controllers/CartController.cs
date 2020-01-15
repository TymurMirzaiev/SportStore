using Core.DTOs;
using Core.Interfaces.Services;
using Data.Entities;
using Data.Repositories;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Web.Models;
using Web.Models.ViewModels;

namespace Web.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductService _productService;
        private IMapper _mapper;
        private Cart _cart;

        public CartController(IMapper mapper, IProductService productService,
            IRepository<Product> productRepository,
            Cart cart)
        {
            _mapper = mapper;
            _productService = productService;
            _cart = cart;
        }

        public ViewResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = _cart,
                ReturnUrl = returnUrl
            });
        }

        public RedirectToActionResult AddToCart(int productId, string returnUrl)
        {
            ProductDto product = _productService.GetById(productId);
            if (product != null)
            {
                _cart.AddItem(product, 1);
            }

            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToActionResult RemoveFromCart(int productId,
                string returnUrl)
        {
            ProductDto product = _productService.GetById(productId);
            if (product != null)
            {
                _cart.RemoveLine(product);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
    }
}