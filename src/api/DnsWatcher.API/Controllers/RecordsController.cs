using System;
using System.Threading.Tasks;
using DnsWatcher.Application.Contracts.Data.Domains;
using DnsWatcher.Application.Contracts.Dto.Domains;
using DnsWatcher.Application.Services.Interfaces;
using DnsWatcher.Common.Enumerations;
using DnsWatcher.Common.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DnsWatcher.API.Controllers
{
	[Authorize]
	[ApiController]
	[Route("domains/{domainId:Guid}/[controller]")]
	public class RecordsController
	{
		#region construction

		private readonly IWatchedRecordsService _recordsService;

		public RecordsController(IWatchedRecordsService recordsService)
		{
			_recordsService = recordsService;
		}

		#endregion

		[HttpGet("{id:Guid}")]
		public Task<WatchedRecordDto> GetWatchedRecord(Guid domainId, Guid id)
		{
			return _recordsService.GetWatchedRecordByIdAsync(domainId, id);
		}

		[HttpPost("")]
		public Task<WatchedRecordDto> CreateWatchedRecord(Guid domainId, CreateWatchedRecordData data)
		{
			return _recordsService.CreateWatchedRecordAsync(domainId, data);
		}

		[HttpPut("{id:Guid}")]
		public Task<WatchedRecordDto> UpdateWatchedRecord(Guid domainId, Guid id, UpdateWatchedRecordData data)
		{
			if (id != data.Id)
				throw new BadDataException(ErrorCode.IdsDontMatch);
			return _recordsService.UpdateWatchedRecordAsync(domainId, data);
		}

		[HttpDelete("{id:Guid}")]
		public Task DeleteWatchedRecord(Guid domainId, Guid id)
		{
			return _recordsService.DeleteWatchedRecordAsync(domainId, id);
		}

		[HttpPost("{id:Guid}/update")]
		public Task<WatchedRecordDto> UpdateRecordResults(Guid domainId, Guid id)
		{
			return _recordsService.UpdateWatchedRecordResultsAsync(domainId, id);
		}
	}
}