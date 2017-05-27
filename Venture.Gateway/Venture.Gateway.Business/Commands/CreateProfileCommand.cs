namespace Venture.Gateway.Business.Commands
{
    public class CreateProfileCommand : ICommand
    {
        public CreateProfileCommand(string email, string firstName, string lastName)
        {
            Email = email;
            FirstName = firstName;
            LastName = lastName;
        }

        public string Email { get; }
        public string FirstName { get; }
        public string LastName { get; }
    }
}
