using AutoMapper;
using Infrastructure.Core.AutoMapper.Transform;

namespace Infrastructure.Core.AutoMapper
{
    public class SportStoreAutoMapper : Infrastructure.Interfaces.IMapper
    {
        private readonly IMapper mapper;

        public SportStoreAutoMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<EntityTo>();
                cfg.AddProfile<DtoTo>();
                cfg.AddProfile<ViewModelTo>();
            });
            mapper = config.CreateMapper();
        }

        public TDestination Map<TSource, TDestination>(TSource source)
        {
            return mapper.Map<TSource, TDestination>(source);
        }

        public TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            return mapper.Map<TSource, TDestination>(source, destination);
        }
    }
}
