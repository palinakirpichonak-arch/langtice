using System.ComponentModel.DataAnnotations;

namespace MainService.PL.Services.Options
{
	public class DatabaseOptions
	{
		[Required]
		public string Host { get; set; } = string.Empty;
		[Range(1, 65535)]
		public int Port { get; set; }
		[Required]
		public string Database { get; set; } = string.Empty;
		public string? Username { get; set; }
		public string? Password { get; set; }
	}
}



