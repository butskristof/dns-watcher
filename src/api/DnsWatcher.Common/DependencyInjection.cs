using Microsoft.Extensions.DependencyInjection;

namespace DnsWatcher.Common
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddCommon(this IServiceCollection services)
		{
			return services;
		}
	}
}