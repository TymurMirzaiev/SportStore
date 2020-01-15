using Core.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces.Services
{
    public interface IOrderService
    {
        IEnumerable<OrderDto> GetOrdersShipped();
        OrderDto SaveOrder(int orderId);
        OrderDto SaveOrder(OrderDto order);
    }
}
