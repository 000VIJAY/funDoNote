﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.User
{
    public class UserPostModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
