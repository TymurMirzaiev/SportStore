using AutoMapper;
using Core.DTOs;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

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
        }
    }
}
