namespace WebAPI.ViewModels.Systems
{
	public class Pagination<T>
	{
		public List<T> Items { get; set; }
		public int TotalRecord { get; set; }
	}
}
