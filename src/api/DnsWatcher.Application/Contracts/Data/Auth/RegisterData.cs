namespace DnsWatcher.Application.Contracts.Data.Auth
{
	public class RegisterData
	{
		public string Username { get; set; }
		public string Password { get; set; }
		public string ConfirmPassword { get; set; }
	}
}