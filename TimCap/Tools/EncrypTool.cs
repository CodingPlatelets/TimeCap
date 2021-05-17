using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TimCap.Tools
{
    public class EncrypTool
    {
        public static string GenerateFakeFinger()
        {
            var random = new Random();
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var finger = new string(Enumerable.Repeat(chars, 50).Select(s => s[random.Next(chars.Length)]).ToArray());
            return finger;
        }

        public static string Sha256(string oldstring)
        {
            byte[] byteOld = Encoding.UTF8.GetBytes(oldstring);
            byte[] byteNew = SHA256.Create().ComputeHash(byteOld);
            StringBuilder sb = new();
            foreach (var b in byteNew)
            {
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }
    }
}
