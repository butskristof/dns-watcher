using DnsWatcher.Common.Extensions;
using FluentValidation;

namespace DnsWatcher.Application.Contracts.Data.Domains.Validators
{
	public class WatchedRecordDataValidator<T>
		: AbstractValidator<T>
		where T : WatchedRecordData
	{
		public WatchedRecordDataValidator()
		{
			RuleFor(e => e.ExpectedIpAddress)
				.Cascade(CascadeMode.Stop)
				.IpAddress();
			
			RuleFor(e => e.ExpectedPort)
				.Cascade(CascadeMode.Stop)
				.Port();
		}
	}

	public class CreateWatchedRecordDataValidator
		: WatchedRecordDataValidator<CreateWatchedRecordData>
	{
	}

	public class UpdateWatchedRecordDataValidator
		: WatchedRecordDataValidator<UpdateWatchedRecordData>
	{
	}
}