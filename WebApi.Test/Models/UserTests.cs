using WebApi.Models;

namespace WebApi.Test.Models
{
    public class UserTests
    {
        [Fact]
        public void ShouldReturnTrueIfPasswordsAreEquals()
        {
            var password = "123456";
            var passwordHash = "123456";
            var result = User.IsCorrectPassoword(passwordHash, password);

            Assert.True(result);
        }

        [Fact]
        public void ShouldReturnFalseIfPasswordsAreNotEquals()
        {
            var password = "123456";
            var passwordHash = "abcdef";
            var result = User.IsCorrectPassoword(passwordHash, password);

            Assert.False(result);
        }
    }
}
