using System;
using System.Threading.Tasks;
using DnsWatcher.Application.Contracts.Data.Servers;
using DnsWatcher.Application.Contracts.Dto.Servers;
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
	public class DnsServersController : ControllerBase
	{
		#region construction
		
		private readonly IDnsServersService _dnsServersService;

		public DnsServersController(IDnsServersService dnsServersService)
		{
			_dnsServersService = dnsServersService;
		}

		#endregion

		[HttpGet("")]
		public Task<DnsServersDto> GetDnsServers()
		{
			return _dnsServersService.GetDnsServersAsync();
		}

		[HttpGet("{id:Guid}")]
		public Task<DnsServerDto> GetDnsServer(Guid id)
		{
			return _dnsServersService.GetDnsServerByIdAsync(id);
		}
		
		[HttpPost("")]
		public Task<DnsServerDto> CreateDnsServer(CreateDnsServerData data)
		{
			return _dnsServersService.CreateDnsServerAsync(data);
		}

		[HttpPut("{id:Guid}")]
		public Task<DnsServerDto> UpdateDnsServer(Guid id, UpdateDnsServerData data)
		{
			if (id != data.Id)
				throw new BadDataException(ErrorCode.IdsDontMatch);
			return _dnsServersService.UpdateDnsServerAsync(data);
		}

		[HttpDelete("{id:Guid}")]
		public Task DeleteDnsServer(Guid id)
		{
			return _dnsServersService.DeleteDnsServerAsync(id);
		}
	}
}