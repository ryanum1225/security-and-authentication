using System.Threading.Tasks;
using Moq;
using Xunit;
using SafeVault.Services;

namespace SafeVault.Tests
{
    public class DatabaseServiceTests
    {
        [Fact]
        public async Task GetUserByCredentialsAsync_ShouldPreventSqlInjection()
        {
            // Arrange
            var connectionString = "YourTestConnectionString";
            var databaseService = new DatabaseService(connectionString);

            // Malicious SQL injection attempt
            var maliciousUsername = "admin'; DROP TABLE Users; --";
            var maliciousPassword = "password123";

            // Act
            var result = await databaseService.GetUserByUsernameAsync(maliciousUsername, maliciousPassword);

            // Assert
            Assert.Null(result); // Ensure no user is returned
        }
    }
}