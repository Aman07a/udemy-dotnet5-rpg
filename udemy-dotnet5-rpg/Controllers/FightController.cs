using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using udemy_dotnet5_rpg.DTOS.Fight;
using udemy_dotnet5_rpg.Models;
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

		[HttpPost("Weapon")]
		public async Task<ActionResult<ServiceResponse<AttackResultDTO>>> WeaponAttack(WeaponAttackDTO request)
		{
			return Ok(await _fightService.WeaponAttack(request));
		}

		[HttpPost("Skill")]
		public async Task<ActionResult<ServiceResponse<AttackResultDTO>>> SkillAttack(SkillAttackDTO request)
		{
			return Ok(await _fightService.SkillAttack(request));
		}

		[HttpPost]
		public async Task<ActionResult<ServiceResponse<FightResultDTO>>> Fight(FightRequestDTO request)
		{
			return Ok(await _fightService.Fight(request));
		}
	}
}
