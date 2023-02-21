using System.Threading.Tasks;
using udemy_dotnet5_rpg.Models;

namespace udemy_dotnet5_rpg.Data
{
	public class AuthRepository : IAuthRepository
	{
		public Task<ServiceResponse<string>> Login(User user, string password)
		{
			throw new System.NotImplementedException();
		}

		public Task<ServiceResponse<int>> Register(User user, string password)
		{
			throw new System.NotImplementedException();
		}

		public Task<bool> UserExists(string username)
		{
			throw new System.NotImplementedException();
		}
	}
}
