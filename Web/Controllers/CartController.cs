using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.DTOs;
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
        private IRepository<Product> _productRepository;
        private IMapper _mapper;
        private Cart _cart;

        public CartController(IMapper mapper, IRepository<Product> productRepository, Cart cart)
        {
            _mapper = mapper;
            _productRepository = productRepository;
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
            Product product = _productRepository.Get()
                .FirstOrDefault(p => p.ProductId == productId);
            var productDto = _mapper.Map<Product, ProductDto>(product);
            if (product != null)
            {
                _cart.AddItem(productDto, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToActionResult RemoveFromCart(int productId,
                string returnUrl)
        {
            Product product = _productRepository.Get()
                .FirstOrDefault(p => p.ProductId == productId);
            var productDto = _mapper.Map<Product, ProductDto>(product);
            if (product != null)
            {
                _cart.RemoveLine(productDto);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
    }
}