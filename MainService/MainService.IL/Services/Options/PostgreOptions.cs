namespace MainService.PL.Services.Options
{
	public class PostgreOptions
	{
		public string PostgreHost { get; set; } = null!;
		public string PostgrePort { get; set; } = null!;
		public string PostgreDatabase { get; set; } = null!;
		public string PostgreUser { get; set; } = null!;
		public string PostgrePassword { get; set; } = null!;
		public string ConnectionString => $"Host={PostgreHost};Port={PostgrePort};Database={PostgreDatabase};Username={PostgreUser};Password={PostgrePassword}";
	}
}



