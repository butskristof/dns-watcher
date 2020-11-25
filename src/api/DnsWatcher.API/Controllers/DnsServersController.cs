using System;
using System.Threading.Tasks;
using DnsWatcher.Application.Contracts.Data.Servers;
using DnsWatcher.Application.Contracts.Dto.Servers;
using DnsWatcher.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DnsWatcher.API.Controllers
{
	[Authorize]
	[ApiController]
	[Route("[controller]")]
	public class DnsServersController
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
	}
}