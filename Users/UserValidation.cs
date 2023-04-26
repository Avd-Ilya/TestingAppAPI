using FluentValidation;

namespace TestingAppApi.Users;

public class UserValidation : AbstractValidator<User>
{
    public UserValidation()
    {
        RuleFor(x => x.Username).NotEmpty();
        RuleFor(x => x.Password).NotEmpty();
    }
}