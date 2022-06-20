using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ComputorV1
{
    public static class Extensions
    {
        public static string[] RegexSplit(this string parameterForSplitting, string pattern)
        {
            if (string.IsNullOrWhiteSpace(pattern))
                throw new ArgumentException($"{nameof(pattern)} is null", nameof(pattern));
            var splitedResult = Regex.Split(parameterForSplitting, pattern, RegexOptions.Compiled)
                .Where(s => !string.IsNullOrWhiteSpace(s)).ToArray();
            return splitedResult.All(x => x.IsNumber()) ? splitedResult : null;
        }

        public static bool IsNumber(this string str) 
        {
            for (int i = 0; i < str.Length; i++)
            {
                var result = char.IsDigit(str[i]) || str[i] == '+' || str[i] == '-';
                if (result == false) return result;
            }
            return true;
        }
        public static List<string> GetRootsByDiscriminant(this double discriminant, double[] polynominalParts)
        {
            List<string> roots = null;
            if (polynominalParts == null || polynominalParts.Length == 0) return roots;
            
            var a = polynominalParts[0];
            var b = polynominalParts[1];

            roots = new List<string>();
            if (discriminant > 0)
            {
                var sqrtDiscr = discriminant / discriminant;
                roots.Add(((-b + sqrtDiscr) / 2 * a).ToString());
                roots.Add(((-b - sqrtDiscr) / 2 * a).ToString());
            }

            else if (discriminant == 0)
            {
                roots.Add((-b / (2 * a)).ToString());
            }

            else if (discriminant < 0)
            {
                roots.Add("There is no solution");
            }
            return roots;
        }
    }
}
