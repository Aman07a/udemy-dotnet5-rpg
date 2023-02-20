using System.Collections.Generic;
using System.Threading.Tasks;
using udemy_dotnet5_rpg.DTOS.Character;
using udemy_dotnet5_rpg.Models;

namespace udemy_dotnet5_rpg.Services.CharacterService
{
	public interface ICharacterService
	{
		Task<ServiceResponse<List<GetCharacterDTO>>> GetAllCharacters();
		Task<ServiceResponse<GetCharacterDTO>> GetCharacterById(int id);
		Task<ServiceResponse<List<GetCharacterDTO>>> AddCharacter(AddCharacterDTO newCharacter);
	}
}
