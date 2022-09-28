namespace HelloHangfire
{
	public interface IShowDate
	{
		Task Print();
	}

	public class ShowDate : IShowDate
	{
		public static List<string> Dates { get; set; } = new List<string>();

		public async Task Print()
		{
			Dates.Add($"Tarih : {DateTime.Now}");
			Console.WriteLine($"Example {DateTime.Now}");
			await Task.CompletedTask;
		}
	}
}