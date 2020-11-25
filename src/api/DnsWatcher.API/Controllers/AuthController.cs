using System.Threading.Tasks;
using DnsWatcher.Application.Contracts.Data.Auth;
using DnsWatcher.Application.Contracts.Dto.Auth;
using DnsWatcher.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DnsWatcher.API.Controllers
{
	[AllowAnonymous]
	[ApiController]
	[Route("[controller]")]
	public class AuthController : ControllerBase
	{
		private readonly IAuthService _authService;

		public AuthController(IAuthService authService)
		{
			_authService = authService;
		}

		[HttpPost("[action]")]
		public async Task<TokenDto> Authenticate([FromBody] LoginData data)
		{
			return await _authService.Authenticate(data);
		}

		[HttpPost("[action]")]
		public async Task Register([FromBody] RegisterData data)
		{
			await _authService.Register(data);
		}
	}
}