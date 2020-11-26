using DnsWatcher.Common.Enumerations;
using DnsWatcher.Common.Extensions;
using FluentValidation;

namespace DnsWatcher.Application.Contracts.Data.Auth.Validators
{
	public class RegisterDataValidator : AbstractValidator<RegisterData>
	{
		public RegisterDataValidator()
		{
			RuleFor(e => e.Username)
				.NotNull()
				.NotEmpty()
				.EmailAddress();
			RuleFor(e => e.Password)
				.Password();
			RuleFor(e => e.ConfirmPassword)
				.Equal(e => e.Password)
				.WithErrorCode(ErrorCode.PasswordsDontMatch);
		}
	}
}