using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DnsWatcher.Application.Contracts.Mappings;
using DnsWatcher.Domain.Common;
using DnsWatcher.Domain.Entities.Domains;
using DnsWatcher.Domain.Enumerations;

namespace DnsWatcher.Application.Contracts.Dto.Domains
{
	public class WatchedRecordDto : EntityBaseDto, IMapFrom<WatchedRecord>
	{
		public RecordType RecordType { get; set; }
		public string Prefix { get; set; }
		public string ExpectedValue { get; set; }
		public int ExpectedTimeToLive { get; set; }

		public IEnumerable<RecordServerResultDto> Results { get; set; }

		public double Propagation
		{
			get
			{
				var total = Results.Count();
				return total > 0
					? Math.Round(Results.Count(e => e.Value == ExpectedValue
													&& e.TimeToLive <= ExpectedTimeToLive)
						/ (double) total * 100)
					: 0;
			}
		}

		public void Mapping(Profile profile)
		{
			profile
				.CreateMap<WatchedRecord, WatchedRecordDto>();
		}
	}
}