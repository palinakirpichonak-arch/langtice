using System.ComponentModel.DataAnnotations;

namespace MainService.BLL.Services.Options
{
	public class MongoOptions
	{
		[Required, Url]
		public string MongoConnection { get; set; } = null!;
		
		[Required]
		public string MongoDatabase { get; set; } = null!;
	}
}



