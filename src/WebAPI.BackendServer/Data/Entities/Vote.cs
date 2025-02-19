using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebAPI.BackendServer.Data.Entities.Interface;

namespace WebAPI.BackendServer.Data.Entities
{
	[Table("Votes")]
	public class Vote : IDateTracking
	{
		public int KnowledBaseId { get; set; }

		[MaxLength(50)]
		[Column(TypeName = "varchar(50)")]
		public string UserId { get; set; }

		public DateTime CreateDate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
		public DateTime? LastUpdateTime { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
	}
}
