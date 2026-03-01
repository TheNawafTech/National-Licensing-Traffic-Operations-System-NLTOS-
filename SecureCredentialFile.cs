using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Driving___Vehicle_License_Department__DVLD__Project
{
    internal class SecureCredentialFile
    {
        // مفتاح إضافي (اختياري) لزيادة الأمان
        private static readonly byte[] Entropy = Encoding.UTF8.GetBytes("DVLD-Login-App-Key");

        private static string GetStoragePath()
        {
            string folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "DVLD");
            Directory.CreateDirectory(folder);
            return Path.Combine(folder, "login.dat");
        }

        public static void SaveCredentials(string username, string password)
        {
            string combined = username + "|" + password;
            byte[] plain = Encoding.UTF8.GetBytes(combined);

            // تشفير البيانات
            byte[] encrypted = ProtectedData.Protect(plain, Entropy, DataProtectionScope.CurrentUser);

            File.WriteAllBytes(GetStoragePath(), encrypted);
        }

        public static (string username, string password) LoadCredentials()
        {
            string path = GetStoragePath();
            if (!File.Exists(path)) return (null, null);

            byte[] encrypted = File.ReadAllBytes(path);
            try
            {
                byte[] plain = ProtectedData.Unprotect(encrypted, Entropy, DataProtectionScope.CurrentUser);
                string combined = Encoding.UTF8.GetString(plain);
                var parts = combined.Split('|');
                if (parts.Length == 2) return (parts[0], parts[1]);
            }
            catch (CryptographicException)
            {
                // إذا الملف تالف أو غير قابل للفك
                DeleteCredentials();
            }
            return (null, null);
        }

        public static void DeleteCredentials()
        {
            string path = GetStoragePath();
            if (File.Exists(path)) File.Delete(path);
        }
    }
}
