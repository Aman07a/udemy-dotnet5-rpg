using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using udemy_dotnet5_rpg.Data;
using udemy_dotnet5_rpg.DTOS.User;
using udemy_dotnet5_rpg.Models;

namespace udemy_dotnet5_rpg.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly IAuthRepository _authRepo;

		public AuthController(IAuthRepository authRepo)
		{
			_authRepo = authRepo;
		}

		[HttpPost("Register")]
		public async Task<ActionResult<ServiceResponse<int>>> Register(UserRegisterDTO request)
		{
			var response = await _authRepo.Register(
				new User { Username = request.Username }, request.Password
			);


			if (!response.Success)
			{
				return BadRequest(response);
			}

			return Ok(response);
		}
	}
}
