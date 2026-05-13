using System.Security.Cryptography;
using System.Text;

namespace AdvertisementApp.Common.Helpers
{
    public static class PasswordHelper
    {
        public static string HashPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return string.Empty;
            }

            // SHA256 şifreleme nesnesini oluşturuyoruz
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Gelen şifreyi byte dizisine çevirip hash'liyoruz
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Hash'lenen byte dizisini okunabilir bir metne (Hexadecimal) çeviriyoruz
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                
                return builder.ToString();
            }
        }
    }
}