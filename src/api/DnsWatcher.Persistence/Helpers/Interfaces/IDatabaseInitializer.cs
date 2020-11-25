using System.Threading.Tasks;

namespace DnsWatcher.Persistence.Helpers.Interfaces
{
	public interface IDatabaseInitializer
	{
		Task SeedAsync();
	}
}