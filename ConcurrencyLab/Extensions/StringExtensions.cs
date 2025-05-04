using ConcurrencyLab.Infrastructure.Helper.Validator;
using Newtonsoft.Json;

namespace ConcurrencyLab.Extensions
{
    public static class StringExtensions
    {
        public static bool IsValidEmail(this string input, IEmailValidator validator)
        {
            return validator.Validate(input);
        }

        public static T DeserializeJson<T>(this string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
