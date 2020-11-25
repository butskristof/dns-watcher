using System.Reflection;
using AutoMapper;
using DnsWatcher.Application.Helpers;
using DnsWatcher.Application.Helpers.Interfaces;
using DnsWatcher.Application.Services;
using DnsWatcher.Application.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace DnsWatcher.Application
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddApplication(this IServiceCollection services)
		{
			services.AddAutoMapper(Assembly.GetExecutingAssembly());

			services.AddScoped<IJwtHelper, JwtHelper>();

			services.AddScoped<IAuthService, AuthService>();

			return services;
		}
	}
}