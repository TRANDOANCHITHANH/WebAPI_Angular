namespace WebAPI.BackendServer.Data.Entities.Interface
{
	interface IDateTracking
	{
		public DateTime CreateDate { get; set; }
		public DateTime? LastUpdateTime { get; set; }
	}
}
