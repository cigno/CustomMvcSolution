using System;
using System.Security.Cryptography;
using System.Text;

namespace CustomMvcSolution.Helpers.Generic
{
    public class PasswordHelper
    {
        public static string CreateSalt(int size = 32)
        {
            //Generate a cryptographic random number.
            var rng = new RNGCryptoServiceProvider();
            var buff = new byte[size];
            rng.GetBytes(buff);

            // Return a Base64 string representation of the random number.
            return Convert.ToBase64String(buff);
        }

        public static string CreatePasswordHash(string pwd, string salt)
        {
            string saltAndPwd = String.Concat(pwd, salt);
            byte[] data = Encoding.ASCII.GetBytes(saltAndPwd);
            var sha1 = new SHA1Managed();

            byte[] dataHash = sha1.ComputeHash(data);

            var result = new StringBuilder();

            foreach (byte b in dataHash)
            {
                result.Append(string.Format("{0:X}", b));
            }

            return result.ToString();
        }
    }
}
