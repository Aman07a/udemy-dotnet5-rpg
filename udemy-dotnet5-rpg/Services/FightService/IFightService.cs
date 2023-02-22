using System.Collections.Generic;
using System.Threading.Tasks;
using udemy_dotnet5_rpg.DTOS.Fight;
using udemy_dotnet5_rpg.Models;

namespace udemy_dotnet5_rpg.Services.FightService
{
	public interface IFightService
	{
		Task<ServiceResponse<AttackResultDTO>> WeaponAttack(WeaponAttackDTO request);
		Task<ServiceResponse<AttackResultDTO>> SkillAttack(SkillAttackDTO request);
		Task<ServiceResponse<FightResultDTO>> Fight(FightRequestDTO request);
		Task<ServiceResponse<List<HighscoreDTO>>> GetHighscore();
	}
}
