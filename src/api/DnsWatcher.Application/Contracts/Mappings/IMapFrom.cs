using AutoMapper;

namespace DnsWatcher.Application.Contracts.Mappings
{
	internal interface IMapFrom<T>
	{
		void Mapping(Profile profile)
		{
			profile.CreateMap(typeof(T), GetType());
		}
	}
}