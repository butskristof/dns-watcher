using DnsWatcher.Application.Contracts.Dto.Auth;
using DnsWatcher.Domain.Entities.Identity;

namespace DnsWatcher.Application.Helpers.Interfaces
{
	public interface IJwtHelper
	{
		TokenDto GenerateJwtToken(User user);
	}
}