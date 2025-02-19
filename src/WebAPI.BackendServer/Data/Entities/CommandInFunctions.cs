using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.BackendServer.Data.Entities
{
	[Table("CommandInFunctions")]
	public class CommandInFunctions
	{
		[MaxLength(50)]
		[Column(TypeName = "varchar(50)")]
		[Required]
		public string CommandId { get; set; }

		[MaxLength(50)]
		[Column(TypeName = "varchar(50)")]
		[Required]
		public string FunctionId { get; set; }


	}
}
