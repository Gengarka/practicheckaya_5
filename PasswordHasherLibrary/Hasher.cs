using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PasswordHasherLibrary
{
    public class Hasher
    {
        public string GetHash(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] sourceBytePassw = Encoding.UTF8.GetBytes(password);
                byte[] hashSourceBytePassw = sha256Hash.ComputeHash(sourceBytePassw);
                string hashPassw = BitConverter.ToString(hashSourceBytePassw).Replace("-", String.Empty);
                return hashPassw;
            }
        }
    }
}
