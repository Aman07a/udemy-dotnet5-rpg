using AutoMapper;
using udemy_dotnet5_rpg.DTOS.Character;
using udemy_dotnet5_rpg.Models;

namespace udemy_dotnet5_rpg
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile()
		{
			CreateMap<Character, GetCharacterDTO>();
			CreateMap<AddCharacterDTO, Character>();
		}
	}
}
