using DefaultAPI.Domain;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace DefaultAPI.Infra.CrossCutting
{
    public sealed class Cryptography
    {
        public string Hash(string password, int iterations)
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[Constants.SaltSize]);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations);
            var hash = pbkdf2.GetBytes(Constants.HashSize);
            var hashBytes = new byte[Constants.SaltSize + Constants.HashSize];
            Array.Copy(salt, 0, hashBytes, 0, Constants.SaltSize);
            Array.Copy(hash, 0, hashBytes, Constants.SaltSize, Constants.HashSize);
            var base64Hash = Convert.ToBase64String(hashBytes);
            return string.Format("$FXR$V1${0}${1}", iterations, base64Hash);
        }

        public string Hash(string password)
        {
            return Hash(password, 10000);
        }

        public bool IsHashSupported(string hashString)
        {
            return hashString.Contains("$FXR$V1$");
        }

        public bool Verify(string password, string hashedPassword)
        {
            if (!IsHashSupported(hashedPassword))
            {
                throw new NotSupportedException("The hashtype is not supported");
            }

            var splittedHashString = hashedPassword.Replace("$FXR$V1$", "").Split('$');
            var iterations = int.Parse(splittedHashString[0]);
            var base64Hash = splittedHashString[1];
            var hashBytes = Convert.FromBase64String(base64Hash);
            var salt = new byte[Constants.SaltSize];
            Array.Copy(hashBytes, 0, salt, 0, Constants.SaltSize);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations);
            byte[] hash = pbkdf2.GetBytes(Constants.HashSize);
            for (var i = 0; i < Constants.HashSize; i++)
            {
                if (hashBytes[i + Constants.SaltSize] != hash[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
