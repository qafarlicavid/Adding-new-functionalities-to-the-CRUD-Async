using DemoApplication.Areas.Client.ViewModels.Authentication;
using DemoApplication.Database;
using FluentValidation;

namespace DemoApplication.Areas.Client.Validators.Register
{
    public class RegisterViewModelValidator : AbstractValidator<RegisterViewModel>
    {
        private readonly DataContext _dataContext;

        public RegisterViewModelValidator(DataContext dataContext)
        {
            _dataContext = dataContext;
            RuleFor(e=> e.Email).Must(IsEmailUnique).WithMessage("Email already taked");
        }

        private bool IsEmailUnique(string email)
        {
            return !_dataContext.Users.Any(u => u.Email == email);
        }
    }
}
