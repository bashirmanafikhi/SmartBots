using AutoMapper;

namespace SmartBots.Application.Common.Mappings
{
    public static class MappingExtensions
    {
        private static IMapper _mapper;

        public static void ConfigureMapper(IMapper mapper) => _mapper = mapper;

        public static T MapTo<T>(this object source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (_mapper == null) throw new InvalidOperationException("Mapper is not configured.");

            return _mapper.Map<T>(source);
        }
    }
}
