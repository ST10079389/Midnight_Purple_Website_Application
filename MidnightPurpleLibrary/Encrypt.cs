using System.Text;
using System.Security.Cryptography;
namespace MidnightPurpleLibrary
{
    public class Encrypt
    {
        public string Hash_Password(string input)
        {//used this to hash my password all my passwords are hashed to protect the users information
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(input));
                var sb = new StringBuilder(hash.Length * 2);
                //same method used to hash passwords for safety
                foreach (byte b in hash)
                {
                    sb.Append(b.ToString("X2"));
                }
                return sb.ToString();
            }
        }
    }
}
