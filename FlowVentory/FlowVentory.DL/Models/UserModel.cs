﻿namespace Flowventory.DL.Models
{
    public class User
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }  // Store hashed password
        public string Email { get; set; }
    }
}