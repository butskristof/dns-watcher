using DnsWatcher.Common.Constants;
using DnsWatcher.Common.Enumerations;
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
			RuleFor(e => e.ExpectedValue)
				.Cascade(CascadeMode.Stop)
				.NotNull()
				.WithErrorCode(ErrorCode.Required)
				.NotEmpty()
				.WithErrorCode(ErrorCode.Required);
			
			RuleFor(e => e.ExpectedTimeToLive)
				.Cascade(CascadeMode.Stop)
				.GreaterThan(DomainConstants.Zero);
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