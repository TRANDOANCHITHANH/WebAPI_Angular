using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.BackendServer.Data.Entities
{
	[Table("LabeInlKnowledgeBases")]
	public class LabeInlKnowledgeBase
	{
		public int KnowledBaseId { get; set; }

		[MaxLength(50)]
		[Column(TypeName = "varchar(50)")]
		public string LabelId { get; set; }
	}
}
