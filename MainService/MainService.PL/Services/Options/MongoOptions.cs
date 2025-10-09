using System.ComponentModel.DataAnnotations;

namespace MainService.PL.Services.Options
{
	public class MongoOptions
	{
		public string Connection { get; set; } = null!;
		public string Database { get; set; } = null!;
	}
}



