using Domain.Security;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;

namespace Services
{
    public class SecurityService : ISecurityService
    {
        public string GetHashPbkdf2(string password)
        {
            byte[] salt = new byte[] { 0x05, 0x07, 0x11, 0x20, 0x16, 0x17, 0x22 };

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: password,
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA1,
                    iterationCount: 10000,
                    numBytesRequested: 256 / 8));

            return hashed;
        }
    }
}
