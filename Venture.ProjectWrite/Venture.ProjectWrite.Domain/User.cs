﻿using System;
using Venture.Common.Data;

namespace Venture.ProjectWrite.Domain
{
    public class User : Entity
    {
        public User(Guid id)
        {
            Id = id;
        }
    }
}