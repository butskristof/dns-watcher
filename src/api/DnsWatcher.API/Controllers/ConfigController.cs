using System;
using DnsWatcher.Common.Configuration;
using DnsWatcher.Domain.Enumerations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DnsWatcher.API.Controllers
{
	[AllowAnonymous]
	[ApiController]
	[Route("[controller]")]
	public class ConfigController : ControllerBase
	{
		#region construction
		private readonly IApplicationOptions _applicationInfo;

		public ConfigController(IApplicationOptions applicationInfo)
		{
			_applicationInfo = applicationInfo;
		}

		#endregion

		[Route("")]
		[HttpGet]
		public IActionResult GetConfig()
		{
			var ret = new
			{
				ApplicationInfo = new
				{
					_applicationInfo.Version,
					_applicationInfo.Environment,
					_applicationInfo.Build,
				},
				RecordType = Enum.GetNames(typeof(RecordType))
			};
			return Ok(ret);
		}
	}
}