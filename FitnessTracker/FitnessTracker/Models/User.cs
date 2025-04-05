﻿using System;
using System.Collections.Generic;
using System.Text;

namespace FitnessTracker.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        // Optional profile info
        public string FullName { get; set; }
        public int? Age { get; set; }
        public float? Weight { get; set; }
        public float? Height { get; set; }
    }

}
