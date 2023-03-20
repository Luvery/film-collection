using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace FilmCollection.API.Authorization
{
    public class JwtOptions
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public double ValidForMinutes { get; set; }
        public string SecretKey { get; set; }

        public DateTime Expires => DateTime.UtcNow.AddMinutes(ValidForMinutes);
        public SymmetricSecurityKey SigningKey => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
    }
}
