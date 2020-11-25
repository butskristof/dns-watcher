using System.Threading.Tasks;
using DnsWatcher.Domain.Entities.Domains;

namespace DnsWatcher.Application.Services.Interfaces
{
	public interface IDnsService
	{
		Task UpdateDnsResultsForRecordAsync(WatchedRecord record);
	}
}