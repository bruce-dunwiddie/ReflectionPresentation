using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ReflectionPresentation.Invocation;

namespace CustomValidator
{
    public class Validator : IValidator
    {
        private static readonly HashSet<char> specialCharacters = new HashSet<char>() {
            '%', 
            '$', 
            '#'
        };

        public bool IsValid(string input)
        {
            // http://stackoverflow.com/questions/22232582/regex-for-strong-password-in-asp-net

            return
                input.Length > 8 &&
                input.Any(c => char.IsLower(c)) && //Lower case 
                input.Any(c => char.IsUpper(c)) &&
                input.Any(c => char.IsDigit(c)) &&
                input.Any(c => specialCharacters.Contains(c));
        }
    }
}
