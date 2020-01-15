using AutoMapper;
using Core.DTOs;
using Data.Entities;

namespace Infrastructure.Core.AutoMapper.Transform
{
    class EntityTo : Profile
    {
        public EntityTo()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(p => p.ProductId, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(p => p.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(p => p.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(p => p.Category, opt => opt.MapFrom(src => src.Category))
                .ForMember(p => p.Description, opt => opt.MapFrom(src => src.Description));
            CreateMap<Order, OrderDto>()
                .ForMember(o => o.City, opt => opt.MapFrom(src => src.City))
                .ForMember(o => o.Country, opt => opt.MapFrom(src => src.Country))
                .ForMember(o => o.GiftWrap, opt => opt.MapFrom(src => src.GiftWrap))
                .ForMember(o => o.Line1, opt => opt.MapFrom(src => src.Line1))
                .ForMember(o => o.Line2, opt => opt.MapFrom(src => src.Line2))
                .ForMember(o => o.Line3, opt => opt.MapFrom(src => src.Line3))
                .ForMember(o => o.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(o => o.OrderId, opt => opt.MapFrom(src => src.OrderId))
                .ForMember(o => o.Shipped, opt => opt.MapFrom(src => src.Shipped))
                .ForMember(o => o.State, opt => opt.MapFrom(src => src.State))
                .ForMember(o => o.Zip, opt => opt.MapFrom(src => src.Zip));
            CreateMap<CartLine, CartLineDto>()
                .ForMember(c => c.CartLineID, opt => opt.MapFrom(src => src.CartLineID))
                .ForMember(c => c.Product, opt => opt.MapFrom(src => src.Product))
                .ForMember(c => c.Quantity, opt => opt.MapFrom(src => src.Quantity));
        }
    }
}
