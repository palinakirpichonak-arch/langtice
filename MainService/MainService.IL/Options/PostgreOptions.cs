using System.ComponentModel.DataAnnotations;

namespace MainService.BLL.Options
{
	public class PostgreOptions
	{
		[Required]
		public string PostgreHost { get; set; } = null!;

		[Range(1, 65535)]
		public int PostgrePort { get; set; }

		[Required]
		public string PostgreDatabase { get; set; } = null!;

		[Required]
		public string PostgreUser { get; set; } = null!;

		[Required]
		public string PostgrePassword { get; set; } = null!;
		public string ConnectionString => 
			$"Host={PostgreHost};Port={PostgrePort};Database={PostgreDatabase};Username={PostgreUser};Password={PostgrePassword}";
	}
}



