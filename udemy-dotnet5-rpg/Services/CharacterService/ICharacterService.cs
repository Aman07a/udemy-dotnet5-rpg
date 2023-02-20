using System.Collections.Generic;
using udemy_dotnet5_rpg.Models;

namespace udemy_dotnet5_rpg.Services.CharacterService
{
	public interface ICharacterService
	{
		List<Character> GetAllCharacters();
		Character GetCharacterById(int id);
		List<Character> AddCharacter(Character newCharacter);
	}
}
