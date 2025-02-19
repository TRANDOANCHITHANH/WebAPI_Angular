using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebAPI.BackendServer.Data.Entities.Interface;

namespace WebAPI.BackendServer.Data.Entities
{
	[Table("ActivityLogs")]
	public class ActivityLogs : IDateTracking
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		[MaxLength(50)]
		[Column(TypeName = "varchar(50)")]
		[Required]
		public string Action { get; set; }

		[MaxLength(50)]
		[Column(TypeName = "varchar(50)")]
		[Required]
		public string EntityName { get; set; }

		[MaxLength(50)]
		[Column(TypeName = "varchar(50)")]
		[Required]
		public string EntityId { get; set; }

		[MaxLength(50)]
		[Column(TypeName = "varchar(50)")]
		public string UserId { get; set; }

		[MaxLength(50)]
		public string Content { get; set; }
		public DateTime CreateDate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
		public DateTime? LastUpdateTime { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
	}
}
