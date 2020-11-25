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
	}
}