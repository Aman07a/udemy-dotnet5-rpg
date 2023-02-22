using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using udemy_dotnet5_rpg.Services.FightService;

namespace udemy_dotnet5_rpg.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class FightController : ControllerBase
	{
		private readonly IFightService _fightService;

		public FightController(IFightService fightService)
		{
			_fightService = fightService;
		}
	}
}
