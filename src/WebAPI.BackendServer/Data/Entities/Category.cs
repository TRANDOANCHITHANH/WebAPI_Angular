using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebAPI.BackendServer.Data.Entities.Interface;

namespace WebAPI.BackendServer.Data.Entities
{
	[Table("Category")]
	public class Category : IDateTracking
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		[Required]
		[MaxLength(200)]
		public string Name { get; set; }

		[Required]
		[MaxLength(200)]
		[Column(TypeName = "varchar(200)")]
		public string SeoAlias { get; set; }

		[MaxLength(500)]
		public string SeoDescription { get; set; }

		[Required]
		public int SortOrder { get; set; }

		public int? ParentId { get; set; }
		public int? NumberOfTickets { get; set; }

		public DateTime CreateDate { get; set; }
		public DateTime? LastUpdateTime { get; set; }
	}
}
