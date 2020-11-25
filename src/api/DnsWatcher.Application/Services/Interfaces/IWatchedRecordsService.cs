using System;
using System.Threading.Tasks;
using DnsWatcher.Application.Contracts.Data.Domains;
using DnsWatcher.Application.Contracts.Dto.Domains;

namespace DnsWatcher.Application.Services.Interfaces
{
	public interface IWatchedRecordsService
	{
		Task<WatchedRecordDto> CreateWatchedRecordAsync(Guid domainId, CreateWatchedRecordData data);
		Task<WatchedRecordDto> UpdateWatchedRecordAsync(Guid domainId, UpdateWatchedRecordData data);
		Task DeleteWatchedRecordAsync(Guid domainId, Guid id);
	}
}