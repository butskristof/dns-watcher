using System.Threading.Tasks;
using DnsWatcher.Application.Contracts.Data.Helpers;

namespace DnsWatcher.Application.Helpers.Interfaces
{
	public interface IDnsResolver
	{
		Task<DnsQueryResult> ResolveDnsQuery(DnsQueryData data);
	}
}