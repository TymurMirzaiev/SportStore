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
        private readonly IProductRepository _productRepository;

        public ProductService(IMapper mapper, IProductRepository productRepository)
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

        public ProductDto GetById(int productId)
        {
            var product = _productRepository.Get()
                .FirstOrDefault(p => p.ProductId == productId);
            var productDto = _mapper.Map<Product, ProductDto>(product);

            return productDto;
        }

        public ProductDto Remove(ProductDto productDto)
        {
            var product = _mapper.Map<ProductDto, Product>(productDto);
            var res = _productRepository.Remove(product);
            productDto = _mapper.Map<Product, ProductDto>(res);
            
            return productDto;
        }

        public void SaveOrder(ProductDto productDto)
        {
            var product = _mapper.Map<ProductDto, Product>(productDto);
            _productRepository.SaveOrder(product);
        }
    }
}
