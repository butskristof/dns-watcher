using AutoMapper;
using DnsWatcher.Application.Contracts.Mappings;
using DnsWatcher.Domain.Entities.Domains;
using DnsWatcher.Domain.Enumerations;

namespace DnsWatcher.Application.Contracts.Dto.Domains
{
	public class WatchedRecordDto : EntityBaseDto, IMapFrom<WatchedRecord>
	{
		public RecordType RecordType { get; set; }
		public string ExpectedIpAddress { get; set; }
		public int ExpectedPort { get; set; }

		public void Mapping(Profile profile)
		{
			profile
				.CreateMap<WatchedRecord, WatchedRecordDto>();
		}
	}
}