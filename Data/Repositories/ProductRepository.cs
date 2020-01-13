using Data.Context;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Data.Repositories
{
    public class ProductRepository : IRepository<Product>
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Create(Product item)
        {
            _context.Products.Add(item);
            _context.SaveChanges();
        }

        public Product FindById(int id)
        {
            return _context.Products.Find(id);
        }

        public IEnumerable<Product> Get()
        {
            return _context.Products.Skip(1).AsNoTracking().AsQueryable();
        }

        public IEnumerable<Product> Get(Func<Product, bool> predicate)
        {
            return _context.Products.AsNoTracking().Where(predicate).AsEnumerable();
        }

        public void Remove(Product item)
        {
            _context.Products.Remove(item);
            _context.SaveChanges();
        }

        public void Update(Product item)
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
