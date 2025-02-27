using FluentValidation;

namespace WebAPI.ViewModels.Systems
{
	public class RoleVMValidator : AbstractValidator<RoleVM>
	{
		public RoleVMValidator()
		{
			RuleFor(x => x.Id).NotEmpty().MaximumLength(50).WithMessage("Role id cannot limit 50 chracters");
			RuleFor(x => x.Name).NotEmpty();
		}
	}
}
