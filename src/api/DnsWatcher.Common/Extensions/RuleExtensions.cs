using DnsWatcher.Common.Enumerations;
using FluentValidation;

namespace DnsWatcher.Common.Extensions
{
	public static class RuleExtensions
	{
		public static IRuleBuilderOptions<T, TProperty> WithErrorCode<T, TProperty>(
			this IRuleBuilderOptions<T, TProperty> rule,
			ErrorCode errorCode)
		{
			return rule.WithMessage(errorCode.ToString());
		}
	}
}