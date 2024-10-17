using AutoMapper;

namespace SmartBots.Application.Common.Mappings;
internal interface IMapFromWithReverse<T> : IMapFrom<T>
{
    new void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType()).ReverseMap();
}
