using System.Threading.Tasks;
using udemy_dotnet5_rpg.Models;

namespace udemy_dotnet5_rpg.Data
{
	public interface IAuthRepository
	{
		Task<ServiceResponse<int>> Register(User user, string password);
		Task<ServiceResponse<string>> Login(User user, string password);
		Task<bool> UserExists(string username);
	}
}
