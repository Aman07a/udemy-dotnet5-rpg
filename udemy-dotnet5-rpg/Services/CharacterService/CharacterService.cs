using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using udemy_dotnet5_rpg.Data;
using udemy_dotnet5_rpg.DTOS.Character;
using udemy_dotnet5_rpg.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace udemy_dotnet5_rpg.Services.CharacterService
{
	public class CharacterService : ICharacterService
	{
		private readonly IMapper _mapper;
		private readonly DataContext _context;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public CharacterService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor)
		{
			_mapper = mapper;
			_context = context;
			_httpContextAccessor = httpContextAccessor;
		}

		private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User
			.FindFirstValue(ClaimTypes.NameIdentifier));

		public async Task<ServiceResponse<List<GetCharacterDTO>>> AddCharacter(AddCharacterDTO newCharacter)
		{
			var serviceResponse = new ServiceResponse<List<GetCharacterDTO>>();
			Character character = _mapper.Map<Character>(newCharacter);
			character.User = await _context.Users.FirstOrDefaultAsync(u => u.Id == GetUserId());

			_context.Characters.Add(character);
			await _context.SaveChangesAsync();
			serviceResponse.Data = await _context.Characters
				.Where(c => c.User.Id == GetUserId())
				.Select(c => _mapper.Map<GetCharacterDTO>(c))
				.ToListAsync();
			return serviceResponse;
		}

		public async Task<ServiceResponse<List<GetCharacterDTO>>> DeleteCharacter(int id)
		{
			ServiceResponse<List<GetCharacterDTO>> response = new ServiceResponse<List<GetCharacterDTO>>();

			try
			{
				Character character = await _context.Characters
				   .FirstOrDefaultAsync(c => c.Id == id && c.User.Id == GetUserId());

				if (character != null)
				{
					_context.Characters.Remove(character);
					await _context.SaveChangesAsync();

					response.Data = _context.Characters
						.Where(c => c.User.Id == GetUserId())
						.Select(c => _mapper.Map<GetCharacterDTO>(c)).ToList();
				}
				else
				{
					response.Success = false;
					response.Message = "Character not found";
				}
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
			var dbCharacters = await _context.Characters
				.Include(c => c.Weapon)
				.Include(c => c.Skills)
				.Where(c => c.User.Id == GetUserId())
				.ToListAsync();
			response.Data = dbCharacters.Select(c => _mapper.Map<GetCharacterDTO>(c)).ToList();
			return response;
		}

		public async Task<ServiceResponse<GetCharacterDTO>> GetCharacterById(int id)
		{
			var serviceResponse = new ServiceResponse<GetCharacterDTO>();
			var dbCharacter = await _context.Characters
				.Include(c => c.Weapon)
				.Include(c => c.Skills)
				.FirstOrDefaultAsync(c => c.Id == id && c.User.Id == GetUserId());
			serviceResponse.Data = _mapper.Map<GetCharacterDTO>(dbCharacter);
			return serviceResponse;
		}

		public async Task<ServiceResponse<GetCharacterDTO>> UpdateCharacter(UpdateCharacterDTO updatedCharacter)
		{
			var serviceResponse = new ServiceResponse<GetCharacterDTO>();

			try
			{
				var character = await _context.Characters
					.Include(c => c.User)
					.FirstOrDefaultAsync(c => c.Id == updatedCharacter.Id);

				if (character.User.Id == GetUserId())
				{
					character.Name = updatedCharacter.Name;
					character.HitPoints = updatedCharacter.HitPoints;
					character.Strength = updatedCharacter.Strength;
					character.Defense = updatedCharacter.Defense;
					character.Intelligence = updatedCharacter.Intelligence;
					character.Class = character.Class;

					await _context.SaveChangesAsync();

					serviceResponse.Data = _mapper.Map<GetCharacterDTO>(character);
				}
				else
				{
					serviceResponse.Success = false;
					serviceResponse.Message = "Character not found.";
				}
			}
			catch (Exception ex)
			{
				serviceResponse.Success = false;
				serviceResponse.Message = ex.Message;
			}

			return serviceResponse;
		}

		public async Task<ServiceResponse<GetCharacterDTO>> AddCharacterSkill(AddCharacterSkillDTO newCharacterSkill)
		{
			var response = new ServiceResponse<GetCharacterDTO>();

			try
			{
				var character = await _context.Characters
					.Include(c => c.Weapon)
					.Include(c => c.Skills)
					.FirstOrDefaultAsync(c => c.Id == newCharacterSkill.CharacterId &&
						c.User!.Id == GetUserId());

				if (character == null)
				{
					response.Success = false;
					response.Message = "Character not found.";
					return response;
				}

				var skill = await _context.Skills
					.FirstOrDefaultAsync(s => s.Id == newCharacterSkill.SkillId);

				if (skill == null)
				{
					response.Success = false;
					response.Message = "Skill not found.";
					return response;
				}

				character.Skills!.Add(skill);
				await _context.SaveChangesAsync();

				response.Data = _mapper.Map<GetCharacterDTO>(character);
			}
			catch (Exception ex)
			{
				response.Success = false;
				response.Message = ex.Message;
			}

			return response;
		}
	}
}
