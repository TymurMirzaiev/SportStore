using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.DTOs;
using Core.Interfaces.Services;
using Data.Entities;
using Data.Repositories;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;
        private readonly Cart _cart;

        public OrderController(IMapper mapper, IOrderService orderService, Cart cart)
        {
            _mapper = mapper;
            _orderService = orderService;
            _cart = cart;
        }

        public ViewResult List()
        {
            var orders = _orderService.GetOrdersShipped();
            return View(orders);
        }

        [HttpPost]
        public IActionResult MarkShipped(int orderID)
        {
            _orderService.SaveOrder(orderID);

            return RedirectToAction(nameof(List));
        }

        public ViewResult Checkout()
        {
            return View(new OrderDto());
        }

        [HttpPost]
        public IActionResult Checkout(OrderDto order)
        {
            if (_cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your cart is empty!");
            }
            if (ModelState.IsValid)
            {
                var cartLines = _cart.Lines.ToArray();
                order.Lines = cartLines;
                _orderService.SaveOrder(order);

                return RedirectToAction(nameof(Completed));
            }
            else
            {
                return View(order);
            }
        }

        public ViewResult Completed()
        {
            _cart.Clear();

            return View();
        }
    }
}