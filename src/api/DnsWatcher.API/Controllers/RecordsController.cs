using System;
using System.Threading.Tasks;
using DnsWatcher.Application.Contracts.Data.Domains;
using DnsWatcher.Application.Contracts.Dto.Domains;
using DnsWatcher.Application.Services.Interfaces;
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

		[HttpPost("")]
		public Task<WatchedRecordDto> CreateWatchedRecord(Guid domainId, CreateWatchedRecordData data)
		{
			return _recordsService.CreateWatchedRecordAsync(domainId, data);
		}
	}
}