using Core.DTOs;
using Core.Interfaces.Services;
using Data.Entities;
using Data.Repositories;
using Infrastructure.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Core.Services
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Product> _productRepository;

        public ProductService(IMapper mapper, IRepository<Product> productRepository)
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }

        public IEnumerable<ProductDto> GetAll()
        {
            var products = _productRepository.Get();
            var productsDto = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(products);

            return productsDto;
        }
    }
}
