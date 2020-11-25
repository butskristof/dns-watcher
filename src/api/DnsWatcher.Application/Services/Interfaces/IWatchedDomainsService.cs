using System;
using System.Threading.Tasks;
using DnsWatcher.Application.Contracts.Data.Domains;
using DnsWatcher.Application.Contracts.Dto.Domains;
using DnsWatcher.Domain.Entities.Domains;

namespace DnsWatcher.Application.Services.Interfaces
{
	public interface IWatchedDomainsService
	{
		Task<WatchedDomainsDto> GetWatchedDomainsAsync();
		Task<WatchedDomain> GetDomainAsync(Guid id);
		Task<WatchedDomainDto> GetWatchedDomainByIdAsync(Guid id);
		Task<WatchedDomainDto> CreateWatchedDomainAsync(CreateWatchedDomainData data);
		Task<WatchedDomainDto> UpdateWatchedDomainAsync(UpdateWatchedDomainData data);
		Task DeleteWatchedDomainAsync(Guid id);
	}
}