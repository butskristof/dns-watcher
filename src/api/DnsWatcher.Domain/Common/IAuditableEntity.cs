using System;

namespace DnsWatcher.Domain.Common
{
	public interface IAuditableEntity
	{
		string CreatedBy { get; set; }
		DateTime CreatedOn { get; set; }

		string ModifiedBy { get; set; }
		DateTime? ModifiedOn { get; set; }
	}
}