using FluentValidation;

namespace WebAPI.ViewModels.Systems
{
	public class UserCreateRequestValidator : AbstractValidator<UserCreateRequest>
	{
		public UserCreateRequestValidator()
		{
			RuleFor(x => x.UserName).NotEmpty().WithMessage("Username is required");
			RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required").Matches(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").WithMessage("Email format is required");
			RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required");
			RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("PhoneNumber is required");
			RuleFor(x => x.FirstName).NotEmpty().WithMessage("FirstName is required").MaximumLength(50).WithMessage("First name can not over 50 characters");
			RuleFor(x => x.LastName).NotEmpty().WithMessage("LastName is required").MaximumLength(50).WithMessage("Last name can not over 50 characters");
		}
	}
}
