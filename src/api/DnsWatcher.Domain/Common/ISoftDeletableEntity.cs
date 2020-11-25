using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DnsWatcher.Domain.Common
{
	public interface ISoftDeletableEntity
	{
		[NotMapped] bool Deleted { get; }

		string DeletedBy { get; set; }
		DateTime? DeletedOn { get; set; }
	}
}