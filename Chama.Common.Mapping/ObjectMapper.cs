using AutoMapper;

namespace Chama.Common.Logging
{
    public abstract class ObjectMapper : IObjectMapper
    {
        private IMapper mapper;

        public void Configure()
        {
            // Use AutoMapper
            MapperConfiguration store = new MapperConfiguration(CreateMappings);
            store.AssertConfigurationIsValid();
            mapper = store.CreateMapper();
        }

        protected abstract void CreateMappings(IMapperConfiguration cfg);

        public TDestination Map<TDestination>(object input)
        {
            return mapper.Map<TDestination>(input);
        }

        public TDestination Map<TSource, TDestination>(TSource input)
        {
            return mapper.Map<TSource, TDestination>(input);
        }

        public TDestination Map<TSource, TDestination>(TSource input, TDestination destination)
        {
            return mapper.Map<TSource, TDestination>(input, destination);
        }
    }
}

