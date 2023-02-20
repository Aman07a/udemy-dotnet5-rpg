using Microsoft.EntityFrameworkCore;
using udemy_dotnet5_rpg.Models;

namespace udemy_dotnet5_rpg.Data
{
	public class DataContext : DbContext
	{
		public DataContext(DbContextOptions<DataContext> options) : base(options) { }

		public DbSet<Character> Characters { get; set; }
	}
}
