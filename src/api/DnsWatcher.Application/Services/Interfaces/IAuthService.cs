using System.Threading.Tasks;
using DnsWatcher.Application.Contracts.Data.Auth;
using DnsWatcher.Application.Contracts.Dto.Auth;

namespace DnsWatcher.Application.Services.Interfaces
{
	public interface IAuthService
	{
		Task<TokenDto> Authenticate(LoginData data);
		Task Register(RegisterData data);
		Task<TokenDto> RefreshToken(RefreshData data);
	}
}