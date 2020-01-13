using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.DTOs;
using Data.Entities;
using Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class OrderController : Controller
    {
        private IOrderRepository _orderRepository;
        private IMapper _mapper;
        private Cart _cart;

        public OrderController(IMapper mapper, IOrderRepository orderRepository, Cart cart)
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
            _cart = cart;
        }

        public ViewResult List()
        {
            var orders = _orderRepository.Get()
                .Where(o => !o.Shipped);
            return View(orders);
        }

        [HttpPost]
        public IActionResult MarkShipped(int orderID)
        {
            Order order = _orderRepository.Get()
                .FirstOrDefault(o => o.OrderId == orderID);
            if (order != null)
            {
                order.Shipped = true;
                _orderRepository.SaveOrder(order);
            }
            return RedirectToAction(nameof(List));
        }

        public ViewResult Checkout()
        {
            return View(new Order());
        }

        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            if (_cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your cart is empty!");
            }
            if (ModelState.IsValid)
            {
                var cartLines = _cart.Lines.ToArray();
                order.Lines = _mapper.Map<ICollection<CartLineDto>, ICollection<CartLine>>(cartLines);
                _orderRepository.SaveOrder(order);
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