﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using udemy_dotnet5_rpg.DTOS.Character;
using udemy_dotnet5_rpg.Models;
using udemy_dotnet5_rpg.Services.CharacterService;
using Microsoft.AspNetCore.Mvc;

namespace udemy_dotnet5_rpg.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CharacterController : ControllerBase
	{
		private readonly ICharacterService _characterService;

		public CharacterController(ICharacterService characterService)
		{
			_characterService = characterService;
		}

		[HttpGet("GetAll")]
		public async Task<ActionResult<ServiceResponse<List<GetCharacterDTO>>>> Get()
		{
			return Ok(await _characterService.GetAllCharacters());
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<ServiceResponse<GetCharacterDTO>>> GetSingle(int id)
		{
			return Ok(await _characterService.GetCharacterById(id));
		}

		[HttpPost]
		public async Task<ActionResult<ServiceResponse<List<GetCharacterDTO>>>> AddCharacter(AddCharacterDTO newCharacter)
		{
			return Ok(await _characterService.AddCharacter(newCharacter));
		}

		[HttpPut]
		public async Task<ActionResult<ServiceResponse<GetCharacterDTO>>> UpdateCharacter(UpdateCharacterDTO updatedCharacter)
		{
			var response = await _characterService.UpdateCharacter(updatedCharacter);

			if (response == null)
			{
				return NotFound(response);
			}

			return Ok(response);
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult<ServiceResponse<List<GetCharacterDTO>>>> Delete(int id)
		{
			var response = await _characterService.DeleteCharacter(id);

			if (response.Data == null)
			{
				return NotFound(response);
			}

			return Ok(response);
		}
	}
}
