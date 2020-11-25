using FluentValidation;

namespace DnsWatcher.Application.Contracts.Data.Auth.Validators
{
	public class LoginDataValidator : AbstractValidator<LoginData>
	{
		public LoginDataValidator()
		{
			RuleFor(e => e.Username)
				.NotNull()
				.NotEmpty();
			RuleFor(e => e.Password)
				.NotNull()
				.NotEmpty();
		}
	}
}