using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dziennik.Helpers
{
    public static class CookieEncryption
    {
        private static readonly string KEY = "OsGUyfLbk";
        private static readonly int MAGICNUMBER = 17013;

        public static int Encrypt(int id)
        {
            return id ^ MAGICNUMBER;
        }

        public static int Decrypt(int id)
        {
            return Encrypt(id);
        }

        public static string Encrypt(string role)
        {
            return VigenereCipher.Cipher(role, KEY);
        }

        public static string Decrypt(string role)
        {
            return VigenereCipher.Decipher(role, KEY);
        }

        private static class VigenereCipher
        {
            static string alphabet = "AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz";
            public static string Cipher(string text, string key)
            {
                key = expandKey(key, text.Length);
                string ciphered = "";
                for (int i = 0; i < text.Length; i++)
                {
                    int textColumn = alphabet.IndexOf(text[i]);
                    int keyRow = alphabet.IndexOf(key[i]);
                    ciphered += alphabet[(keyRow + textColumn) % alphabet.Length];
                }
                return ciphered;
            }

            public static string Decipher(string ciphered, string key)
            {
                key = expandKey(key, ciphered.Length);
                string text = "";
                for (int i = 0; i < ciphered.Length; i++)
                {
                    int textColumn = alphabet.IndexOf(ciphered[i]);
                    int keyRow = alphabet.IndexOf(key[i]);
                    text += alphabet[(textColumn - keyRow + alphabet.Length) % alphabet.Length];
                }
                return text;
            }

            private static string expandKey(string key, int length)
            {
                string expandedKey = key;
                int i = 0;
                while (expandedKey.Length < length)
                {
                    expandedKey += key[i % key.Length];
                    i++;
                }
                return expandedKey;
            }
        }
    }
}