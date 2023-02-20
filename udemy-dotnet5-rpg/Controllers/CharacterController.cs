using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using udemy_dotnet5_rpg.Models;

namespace udemy_dotnet5_rpg.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CharacterController : ControllerBase
	{
		private static List<Character> characters = new List<Character> {
		   new Character(),
		   new Character { Name = "Sam"}
		};

		[HttpGet]
		public ActionResult<Character> GetSingle()
		{
			return Ok(characters[0]);
		}
	}
}
