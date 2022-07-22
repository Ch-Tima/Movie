using System;
using System.Security.Cryptography;
using System.Text;

namespace Movie.Helpers
{
    public class HashPasswordHelpers
    {
        public static string HashPassword(string password)
        {
            try
            {
                using (var sha = SHA256.Create())
                {
                    var hashBytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
                    var hash = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
                    return hash;
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
    }
}
