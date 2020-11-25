using System;

namespace DnsWatcher.Application.Contracts.Dto
{
	public class EntityBaseDto
	{
		public Guid Id { get; set; }
		public DateTime? ModifiedOn { get; set; }
	}
}