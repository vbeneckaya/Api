using System;
using System.ComponentModel;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace Domain.Extensions
{
    public static class Extensions
    {

        public static string ToUpperFirstLetter(this string input)
        {
            return Char.ToUpperInvariant(input[0]) + input.Substring(1);
        }
        
        public static string ToLowerFirstLetter(this string input)
        {
            return Char.ToLowerInvariant(input[0]) + input.Substring(1);
        }
        

        public static string GetHash(this string text)
        {
            return Convert.ToBase64String(new SHA256Managed().ComputeHash(Encoding.UTF8.GetBytes(text)));
        }

        public static string GetDescription<T>(this T e) where T : IConvertible
        {
            string description = null;

            if (e is Enum)
            {
                Type type = e.GetType();
                Array values = Enum.GetValues(type);

                foreach (int val in values)
                {
                    if (val == e.ToInt32(CultureInfo.InvariantCulture))
                    {
                        var memInfo = type.GetMember(type.GetEnumName(val));
                        var descriptionAttributes = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                        if (descriptionAttributes.Length > 0)
                        {
                            // we're only getting the first description we find
                            // others will be ignored
                            description = ((DescriptionAttribute)descriptionAttributes[0]).Description;
                        }

                        break;
                    }
                }
            }

            return description;
        }
        

        public static object GetEnum<T>(this string e) where T : IConvertible
        {
            string description = null;

            Type type = typeof(T);
            Array values = Enum.GetValues(type);

            foreach (int val in values)
            {
                var memInfo = type.GetMember(type.GetEnumName(val));
                var descriptionAttributes = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (descriptionAttributes.Length > 0)
                {
                    description = ((DescriptionAttribute)descriptionAttributes[0]).Description;

                    if (e.Equals(description))
                    {
                        return Enum.Parse(type, type.GetEnumName(val));
                    }
                }
            }

            return null;
        }
        

        public static int? ToInt(this string intString)
        {
            return int.TryParse(intString, out int intValue) ? intValue : (int?)null;
        }

        public static bool? ToBool(this string boolString)
        {
            return bool.TryParse(boolString, out bool boolValue) ? boolValue : (bool?)null;
        }
    }
}
