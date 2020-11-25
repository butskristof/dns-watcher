using DnsWatcher.Common.Extensions;
using FluentValidation;

namespace DnsWatcher.Application.Contracts.Data.Domains.Validators
{
	public abstract class WatchedDomainDataValidator<T>
		: AbstractValidator<T>
		where T : WatchedDomainData
	{
		public WatchedDomainDataValidator()
		{
			RuleFor(e => e.DomainName)
				.Cascade(CascadeMode.Stop)
				.DomainName();
		}
	}

	public class CreateWatchedDomainDataValidator 
		: WatchedDomainDataValidator<CreateWatchedDomainData>
	{
	}
	
	public class UpdateWatchedDomainDataValidator 
		: WatchedDomainDataValidator<UpdateWatchedDomainData>
	{
	}
}