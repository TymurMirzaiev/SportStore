using Core.DTOs;
using Core.Interfaces.Services;
using Data.Entities;
using Data.Repositories;
using Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrderService(IMapper mapper, IOrderRepository orderRepository)
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
        }

        public IEnumerable<OrderDto> GetOrdersShipped()
        {
            var orders = _orderRepository.Get(r => !r.Shipped);
            var ordersDto = _mapper.Map<IEnumerable<Order>, IEnumerable<OrderDto>>(orders);

            return ordersDto;
        }

        public OrderDto SaveOrder(int orderId)
        {
            var order = _orderRepository.GetWithLines(orderId);
            if (order != null)
            {
                order.Shipped = true;
                _orderRepository.SaveOrder(order);
            }
            var orderDto = _mapper.Map<Order, OrderDto>(order);

            return orderDto;
        }

        public OrderDto SaveOrder(OrderDto orderDto)
        {
            var order = _mapper.Map<OrderDto, Order>(orderDto);
            _orderRepository.SaveOrder(order);

            return orderDto;
        }
    }
}
