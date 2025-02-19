using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.BackendServer.Data.Entities
{
	[Table("Labels")]
	public class Label
	{
		[MaxLength(50)]
		[Column(TypeName = "varchar(50)")]
		[Key]
		public int Id { get; set; }

		[MaxLength(50)]
		[Required]
		public string Name { get; set; }
	}
}
