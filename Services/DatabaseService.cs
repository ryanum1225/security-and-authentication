using BCrypt.Net;
using Dapper;
using Microsoft.Data.SqlClient;
using SafeVault.Models;

namespace SafeVault.Services
{
    public class DatabaseService
    {
        private readonly string ConnectionString;

        public DatabaseService(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            // Connect to the SQL Database.
            using var connection = new SqlConnection(ConnectionString);
            await connection.OpenAsync();

            var query = "SELECT Id, Username, PasswordHash FROM Users WHERE Username = @Username";
            return await connection.QueryFirstOrDefaultAsync<User>(query, new { Username = username });
        }

        public async Task<bool> AuthenticateUserAsync(string username, string password)
        {
            var user = await GetUserByUsernameAsync(username);
            if (user == null)
            {
                return false;
            }

            // Compare the inputted password with the stored password hash.
            return BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
        }

        public async Task<bool> RegisterUserAsync(string username, string password)
        {
            // Hash the inputted password.
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(password);

            using var connection = new SqlConnection(ConnectionString);
            await connection.OpenAsync();

            var query = "INSERT INTO Users (Username, PasswordHash) VALUES (@Username, @PasswordHash)";
            var result = await connection.ExecuteAsync(query, new { Username = username, PasswordHash = passwordHash });

            // Will return true if user was successfully registered.
            return result > 0;
        }

        internal async Task<object?> GetUserByUsernameAsync(string username, string password)
        {
            // Hash the password.
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            using var connection = new SqlConnection(ConnectionString);
            await connection.OpenAsync();

            var query = "SELECT Id, Username, PasswordHash FROM Users WHERE Username = @Username AND PasswordHash = @PasswordHash";
            return await connection.QueryFirstOrDefaultAsync<User>(
            query, new { Username = username, PasswordHash = hashedPassword });
        }
    }
}