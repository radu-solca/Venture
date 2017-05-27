namespace Venture.ProfileWrite.Data.Entities
{
    public class UserProfile : BaseEntity
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
