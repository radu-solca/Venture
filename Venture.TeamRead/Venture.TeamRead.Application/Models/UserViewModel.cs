using System;
using Venture.TeamRead.Data.Entities;

namespace Venture.TeamRead.Application.Models
{
    public class UserViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public bool Approved { get; set; }
    }
}
