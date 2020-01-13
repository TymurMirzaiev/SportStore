using Data.Context;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Repositories
{
    public class OrderRepository: IRepository<Order>, IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Create(Order item)
        {
            _context.Orders.Add(item);
            _context.SaveChanges();
        }

        public Order FindById(int id)
        {
            return _context.Orders.Find(id);
        }

        public IEnumerable<Order> Get()
        {
            return _context.Orders.AsNoTracking().AsQueryable();
        }

        public IEnumerable<Order> Get(Func<Order, bool> predicate)
        {
            return _context.Orders.AsNoTracking().Where(predicate).AsEnumerable();
        }

        public void Remove(Order item)
        {
            _context.Orders.Remove(item);
            _context.SaveChanges();
        }

        public void SaveOrder(Order order)
        {
            _context.AttachRange(order.Lines.Select(l => l.Product));
            if (order.OrderId == 0)
            {
                _context.Orders.Add(order);
            }
            _context.SaveChanges();
        }

        public void Update(Order item)
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
