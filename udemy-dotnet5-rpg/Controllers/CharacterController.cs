using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using udemy_dotnet5_rpg.Models;

namespace udemy_dotnet5_rpg.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CharacterController : ControllerBase
	{
		private static List<Character> characters = new List<Character> {
		   new Character(),
		   new Character { Id = 1, Name = "Sam"}
		};

		[HttpGet("{id}")]
		public ActionResult<Character> GetSingle(int id)
		{
			return Ok(characters.FirstOrDefault(c => c.Id == id));
		}
	}
}
