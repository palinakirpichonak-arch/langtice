using System.ComponentModel.DataAnnotations;

namespace MainService.PL.Services.Options
{
	public class MongoOptions
	{
		[Required]
		public string ConnectionString { get; set; } = string.Empty;
		[Required]
		public string Database { get; set; } = string.Empty;
	}
}



