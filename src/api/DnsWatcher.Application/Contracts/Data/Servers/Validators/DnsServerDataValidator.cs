using DnsWatcher.Common.Enumerations;
using DnsWatcher.Common.Extensions;
using FluentValidation;

namespace DnsWatcher.Application.Contracts.Data.Servers.Validators
{
	public abstract class DnsServerDataValidator<T>
		: AbstractValidator<T>
		where T : DnsServerData
	{
		public DnsServerDataValidator()
		{
			RuleFor(e => e.Name)
				.Cascade(CascadeMode.Stop)
				.NotNull()
				.WithErrorCode(ErrorCode.Required)
				.NotEmpty();

			RuleFor(e => e.IpAddress)
				.Cascade(CascadeMode.Stop)
				.IpAddress();

			RuleFor(e => e.Port)
				.Cascade(CascadeMode.Stop)
				.Port();
		}
	}

	public class CreateDnsServerDataValidator
		: DnsServerDataValidator<CreateDnsServerData>
	{
	}

	public class UpdateDnsServerDataValidator
		: DnsServerDataValidator<CreateDnsServerData>
	{
	}
}