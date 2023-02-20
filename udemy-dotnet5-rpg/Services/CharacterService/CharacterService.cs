using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using udemy_dotnet5_rpg.DTOS.Character;
using udemy_dotnet5_rpg.Models;

namespace udemy_dotnet5_rpg.Services.CharacterService
{
	public class CharacterService : ICharacterService
	{
		private static List<Character> characters = new List<Character> {
		   new Character(),
		   new Character { Id = 1, Name = "Sam"}
		};

		public async Task<ServiceResponse<List<GetCharacterDTO>>> AddCharacter(AddCharacterDTO newCharacter)
		{
			var serviceResponse = new ServiceResponse<List<GetCharacterDTO>>();
			characters.Add(newCharacter);
			serviceResponse.Data = characters;
			return serviceResponse;
		}

		public async Task<ServiceResponse<List<GetCharacterDTO>>> GetAllCharacters()
		{
			var serviceResponse = new ServiceResponse<List<GetCharacterDTO>>();
			serviceResponse.Data = characters;
			return serviceResponse;
		}

		public async Task<ServiceResponse<GetCharacterDTO>> GetCharacterById(int id)
		{
			var serviceResponse = new ServiceResponse<GetCharacterDTO>();
			serviceResponse.Data = characters.FirstOrDefault(c => c.Id == id);
			return serviceResponse;
		}
	}
}
