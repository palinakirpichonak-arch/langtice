using System.ComponentModel.DataAnnotations;

namespace MainService.BLL.Options
{
	public class MongoOptions
	{
		[Required]
		public string MongoConnection { get; set; } = null!;
		
		[Required]
		public string MongoDatabase { get; set; } = null!;
	}
}



