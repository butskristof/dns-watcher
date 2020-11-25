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
	[Route("[controller]")]
	public class DomainsController : ControllerBase
	{
		#region construction

		private readonly IWatchedDomainsService _domainsService;

		public DomainsController(IWatchedDomainsService domainsService)
		{
			_domainsService = domainsService;
		}

		#endregion
		
		[HttpGet("")]
		public Task<WatchedDomainsDto> GetWatchedDomains()
		{
			return _domainsService.GetWatchedDomainsAsync();
		}

		[HttpGet("{id:Guid}")]
		public Task<WatchedDomainDto> GetWatchedDomain(Guid id)
		{
			return _domainsService.GetWatchedDomainByIdAsync(id);
		}
		
		[HttpPost("")]
		public Task<WatchedDomainDto> CreateWatchedDomain(CreateWatchedDomainData data)
		{
			return _domainsService.CreateWatchedDomainAsync(data);
		}

		[HttpPut("{id:Guid}")]
		public Task<WatchedDomainDto> UpdateWatchedDomain(Guid id, UpdateWatchedDomainData data)
		{
			if (id != data.Id)
				throw new BadDataException(ErrorCode.IdsDontMatch);
			return _domainsService.UpdateWatchedDomainAsync(data);
		}

		[HttpDelete("{id:Guid}")]
		public Task DeleteWatchedDomain(Guid id)
		{
			return _domainsService.DeleteWatchedDomainAsync(id);
		}
	}
}