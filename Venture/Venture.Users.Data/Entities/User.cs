﻿namespace Venture.Users.Data
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string DisplayName { get; set; }
    }
}
