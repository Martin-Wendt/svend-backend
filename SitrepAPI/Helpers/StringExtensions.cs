using SitrepAPI.Models;
using System.Text.RegularExpressions;

namespace SitrepAPI.Helpers
{
    /// <summary>
    /// String extensions
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Remove all whitespaces in string
        /// </summary>
        /// <param name="s">string to manipulate</param>
        /// <returns>string</returns>
        public static string RemoveWhiteSpaces(this string s)
        {
            return String.Concat(s.Where(c => !Char.IsWhiteSpace(c)));
        }

        /// <summary>
        /// Seperates filterBy string into 'Property Name', 'Operation' or 'Value'
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static FilterOperation FilterBySeperation(this string s)
        {
            // regex to seperate property, operator and value
            Regex rx = new(@"(?'property'[a-zA-Z]*)(?'operator'==|>=|!=|<=)(?'value'\S*)");
            Match match = rx.Match(s);

            return new FilterOperation(match.Groups["property"].Value, match.Groups["operator"].Value, match.Groups["value"].Value);
           
        }
    }
}
