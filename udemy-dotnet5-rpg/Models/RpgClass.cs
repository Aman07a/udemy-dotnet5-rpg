using System.Text.Json.Serialization;

namespace udemy_dotnet5_rpg.Models
{
	[JsonConverter(typeof(JsonStringEnumConverter))]
	public enum RpgClass
	{
		Knight = 1,
		Mage = 2,
		Cleric = 3
	}
}
