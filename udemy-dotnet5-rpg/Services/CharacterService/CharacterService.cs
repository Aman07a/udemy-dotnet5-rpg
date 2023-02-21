using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using udemy_dotnet5_rpg.Data;
using udemy_dotnet5_rpg.DTOS.Character;
using udemy_dotnet5_rpg.Models;
using Microsoft.EntityFrameworkCore;

namespace udemy_dotnet5_rpg.Services.CharacterService
{
	public class CharacterService : ICharacterService
	{
		private readonly IMapper _mapper;
		private readonly DataContext _context;

		public CharacterService(IMapper mapper, DataContext context)
		{
			_mapper = mapper;
			_context = context;
		}

		public async Task<ServiceResponse<List<GetCharacterDTO>>> AddCharacter(AddCharacterDTO newCharacter)
		{
			var serviceResponse = new ServiceResponse<List<GetCharacterDTO>>();
			Character character = _mapper.Map<Character>(newCharacter);

			_context.Characters.Add(character);
			await _context.SaveChangesAsync();
			serviceResponse.Data = await _context.Characters
				.Select(c => _mapper.Map<GetCharacterDTO>(c))
				.ToListAsync();
			return serviceResponse;
		}

		public async Task<ServiceResponse<List<GetCharacterDTO>>> DeleteCharacter(int id)
		{
			ServiceResponse<List<GetCharacterDTO>> response = new ServiceResponse<List<GetCharacterDTO>>();

			try
			{
				Character character = await _context.Characters.FirstAsync(c => c.Id == id);
				_context.Characters.Remove(character);
				await _context.SaveChangesAsync();

				response.Data = _context.Characters.Select(c => _mapper.Map<GetCharacterDTO>(c)).ToList();
			}
			catch (Exception ex)
			{
				response.Success = false;
				response.Message = ex.Message;
			}

			return response;
		}

		public async Task<ServiceResponse<List<GetCharacterDTO>>> GetAllCharacters()
		{
			var response = new ServiceResponse<List<GetCharacterDTO>>();
			var dbCharacters = await _context.Characters.ToListAsync();
			response.Data = dbCharacters.Select(c => _mapper.Map<GetCharacterDTO>(c)).ToList();
			return response;
		}

		public async Task<ServiceResponse<GetCharacterDTO>> GetCharacterById(int id)
		{
			var serviceResponse = new ServiceResponse<GetCharacterDTO>();
			var dbCharacter = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id);
			serviceResponse.Data = _mapper.Map<GetCharacterDTO>(dbCharacter);
			return serviceResponse;
		}

		public async Task<ServiceResponse<GetCharacterDTO>> UpdateCharacter(UpdateCharacterDTO updatedCharacter)
		{
			var serviceResponse = new ServiceResponse<GetCharacterDTO>();

			try
			{
				var character = await _context.Characters
					.FirstOrDefaultAsync(c => c.Id == updatedCharacter.Id);

				character.Name = updatedCharacter.Name;
				character.HitPoints = updatedCharacter.HitPoints;
				character.Strength = updatedCharacter.Strength;
				character.Defense = updatedCharacter.Defense;
				character.Intelligence = updatedCharacter.Intelligence;
				character.Class = character.Class;

				await _context.SaveChangesAsync();

				serviceResponse.Data = _mapper.Map<GetCharacterDTO>(character);
			}
			catch (Exception ex)
			{
				serviceResponse.Success = false;
				serviceResponse.Message = ex.Message;
			}

			return serviceResponse;
		}
	}
}
