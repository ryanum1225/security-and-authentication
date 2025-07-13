using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using BCrypt.Net;
using SafeVault.Models;
using Azure.Identity;

namespace SafeVault.Services
{
    public class AuthService
    {
        private readonly List<User> Users = new List<User>();

        public AuthService()
        {
            Users.Add(new User
            {
                UserID = 1,
                Username = "insomniac-2212",
                PasswordHash = HashPassword("ninety-nineBottlesOfBeerOnTheWall")
            });
        }

        // Method to hash a password.
        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        // Verify a user's credentials.
        public bool Authenticate(string username, string password)
        {
            var user = Users.FirstOrDefault(u => u.Username == username);
            if (user == null)
            {
                return false;
            }

            else
            {
                return BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
            }
        }
    }
}