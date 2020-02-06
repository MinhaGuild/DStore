using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DStore.Helpers
{
    public class Utils
    {
        internal static string GetNewPassword()
        {
            Random random = new Random();
            const string chars = "AaBbCcDdEefFGgHhIiJjKkLlMmNnOoPpQqReSsTtUuVvWwXxYyZz0123456789@";
            return new string(Enumerable.Repeat(chars, 12).Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
