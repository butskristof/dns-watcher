using System.Threading.Tasks;
using DnsWatcher.Common.Constants;
using DnsWatcher.Common.Enumerations;
using FluentValidation;

namespace DnsWatcher.Common.Extensions
{
	public static class RuleBuilderExtensions
	{
		public static IRuleBuilder<T, string> Password<T>(this IRuleBuilder<T, string> ruleBuilder,
			int minimumLength = 8)
		{
			var options = ruleBuilder
				.NotNull()
				.NotEmpty()
				.MinimumLength(minimumLength)
				.Matches("[A-Z]")
				.Matches("[a-z]")
				.Matches("[0-9]")
				.Matches("[^a-zA-Z0-9]");
			return options;
		}

		public static IRuleBuilderOptions<T, string> DomainName<T>(
			this IRuleBuilder<T, string> ruleBuilder
		)
		{
			return ruleBuilder
				.NotNull()
				.WithErrorCode(ErrorCode.Required)
				.NotEmpty()
				.WithErrorCode(ErrorCode.Required)
				.Matches(DomainConstants.DomainRegex)
				.WithErrorCode(ErrorCode.InvalidDomainName);
		}

		public static IRuleBuilderOptions<T, string> IpAddress<T>(
			this IRuleBuilder<T, string> ruleBuilder
		)
		{
			return ruleBuilder
				.NotNull()
				.WithErrorCode(ErrorCode.Required)
				.NotEmpty()
				.WithErrorCode(ErrorCode.Required)
				.Matches(DomainConstants.IpRegex)
				.WithErrorCode(ErrorCode.InvalidIpAddress);
		}

		public static IRuleBuilder<T, int> Port<T>(
			this IRuleBuilder<T, int> ruleBuilder
		)
		{
			return ruleBuilder
				.GreaterThanOrEqualTo(DomainConstants.MinimumPort)
				.WithErrorCode(ErrorCode.Invalid)
				.LessThanOrEqualTo(DomainConstants.MaximumPort)
				.WithErrorCode(ErrorCode.Invalid);
		}
	}
}