using System.ComponentModel.DataAnnotations;

namespace WebAPI.ViewModels.Systems
{
	public class RoleVM
	{
		[Required]
		public string Id { get; set; }
		public string Name { get; set; }
	}
}
