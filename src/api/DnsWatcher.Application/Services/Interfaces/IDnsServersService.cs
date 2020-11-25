using System;
using System.Threading.Tasks;
using DnsWatcher.Application.Contracts.Data.Servers;
using DnsWatcher.Application.Contracts.Dto.Servers;

namespace DnsWatcher.Application.Services.Interfaces
{
	public interface IDnsServersService
	{
		Task<DnsServersDto> GetDnsServersAsync();
		Task<DnsServerDto> GetDnsServerByIdAsync(Guid id);
		Task<DnsServerDto> CreateDnsServerAsync(CreateDnsServerData data);
		Task<DnsServerDto> UpdateDnsServerAsync(UpdateDnsServerData data);
		Task DeleteDnsServerAsync(Guid id);
	}
}