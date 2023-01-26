using Dapper.Contrib.Extensions;
using WebApi.Models.Request;

namespace WebApi.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }

        public static bool IsCorrectPassoword(string passwordHash, string password)
        {
            // Implementar lógica para tratamento de criptografia de senha
            return passwordHash.Equals(password);
        }
    }
}
