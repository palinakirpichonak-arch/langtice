using System.ComponentModel.DataAnnotations;

namespace MainService.PL.Services.Options
{
	public class PostgresOptions
	{
		public string Host { get; set; } = null!;
		public string Port { get; set; } = null!;
		public string Database { get; set; } = null!;
		public string User { get; set; } = null!;
		public string Password { get; set; } = null!;
		public string ConnectionString => $"Host={Host};Port={Port};Database={Database};Username={User};Password={Password}";
	}
}



