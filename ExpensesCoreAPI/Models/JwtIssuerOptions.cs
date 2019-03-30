using System;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace ExpensesCoreAPI.Models
{
    public class JwtIssuerOptions
    {
        public string Issuer { get; set; }
        public string Subject { get; set; }
        public string Audience { get; set; }
        public DateTime Expiration => IssuedAt.Add(ValidFor);
        public DateTime NotBefore { get; set; } = DateTime.UtcNow;
        public TimeSpan ValidFor { get; set; } = TimeSpan.FromMinutes(20);
        public DateTime IssuedAt { get; set; } = DateTime.UtcNow;
        public SigningCredentials SigningCredentials { get; set; }

        public Func<Task<string>> JtiGenerator =>
            () => Task.FromResult(Guid.NewGuid().ToString());
    }
}
