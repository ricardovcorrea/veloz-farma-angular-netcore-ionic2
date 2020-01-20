using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace IhChegou.Global.Extensions.String
{
    public static class StringExtension
    {
        public static T ToObject<T>(this string value)
        {
            try
            {
                if (string.IsNullOrEmpty(value))
                    return default(T);
                return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(value);
            }
            catch (Exception)
            {
                return default(T);
            }
        }

        public static string ToUnderscore(this string value)
        {
            bool first = true;
            string newString = "";
            for (int i = 0; i < value.Length; i++)
            {
                if (char.IsUpper(value[i]))
                {
                    if (first == true)
                    {
                        first = false;
                        newString = string.Concat(newString, value[i]);
                    }
                    else
                    {
                        newString = string.Concat(newString, "_", value[i]);
                    }
                }
                else
                {
                    newString = string.Concat(newString, value[i]);
                }
            }
            return newString;
        }

        public static decimal ToDecimal(this string stringValue)
        {
            if (string.IsNullOrWhiteSpace(stringValue))
                return 0;
            stringValue = stringValue.Replace(",", ".").Replace("R", "").Replace("$", "").Replace(" ", "");
            decimal decimalValue = 0;
            if (decimal.TryParse(stringValue, NumberStyles.Any, CultureInfo.InvariantCulture, out decimalValue))
                return decimalValue;
            return 0;
        }

        public static string EscapeUnicode(this string value)
        {
            var regex = new Regex(@"\\[uU]([0-9A-F]{4})", RegexOptions.IgnoreCase);
            var input = regex.Replace(value, match => ((char)int.Parse(match.Groups[1].Value,
             NumberStyles.HexNumber)).ToString()).Replace("\"", "");
            return input;
        }

        public static string EncryptSHA256(this string password)
        {
            SHA256Managed crypt = new SHA256Managed();
            StringBuilder hash = new StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(password), 0, Encoding.UTF8.GetByteCount(password));
            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }
            return hash.ToString();
        }
    }
}