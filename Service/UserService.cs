public class UserService
{
    private readonly IEmailService _emailService;

    public UserService(IEmailService emailService)
    {
        _emailService = emailService;
    }

    public void EmailAboutCreatedUser(User user)
    {
        _emailService.SendEmail(user.Email, "Welcome!", "Thank you for registering.");
    }
}
