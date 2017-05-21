﻿using Microsoft.EntityFrameworkCore;
using Venture.ProfileWrite.Data.Context;
using Venture.ProfileWrite.Data.Entities;

namespace Venture.ProfileWrite.Data.Repositories
{
    public class ProfileRepository : BaseRepository<Profile>
    {
        public ProfileRepository(ProfileWriteContext context) : base(context)
        {
        }
    }
}
