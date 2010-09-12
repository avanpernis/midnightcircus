using System;
using System.Text.RegularExpressions;

namespace TokenAssist
{
    public static class RollUtilities
    {
        /// <summary>
        /// Retrurns the maximum possible result for the given expression (eg. 1d4+3 will return 7)
        /// </summary>
        /// <param name="expression">The expression to evaluate</param>
        /// <returns>The maximum possible result that the expression could every yield</returns>
        public static int EvaluateMaximum(string expression)
        {           
            // expand any substrings of the form #d# (eg. 1d4, d20)
            expression = Regex.Replace(expression, @"(\d*)\s*d\s*(\d+)", delegate(Match match)
            {
                int diceFaces = int.Parse(match.Groups[2].Value);
                int diceRolls = 1;

                // if value1 is empty string (eg. d20), assume 1 as the multiplier
                return match.Result(int.TryParse(match.Groups[1].Value, out diceRolls) ? (diceRolls * diceFaces).ToString() : diceFaces.ToString());               
            });

            // expand any addition or subtraction operators (eg. 2+3, 6-1)
            while (true)
            {
                Match match = Regex.Match(expression, @"(\d*)\s*([-\+])\s*(\d+)");

                if (!match.Success)
                {
                    break;
                }

                int value1 = int.Parse(match.Groups[1].Value);               
                int value2 = int.Parse(match.Groups[3].Value);
                
                int newValue;
                switch (match.Groups[2].Value)
                {
                    case "+":
                        newValue = value1 + value2;
                        break;
                    case "-":
                        newValue = value1 - value2;
                        break;
                    default:
                        throw new ArgumentException("EvaluateMaximum - Unsupported operator: " + match.Groups[2].Value);
                }

                expression = expression.Remove(match.Index, match.Length);
                expression = expression.Insert(match.Index, newValue.ToString());
            }

            int result;
            if (!int.TryParse(expression, out result))
            {
                throw new ArgumentException("EvaluateMaximum - Invalid expression supplied: " + expression);
            }

            return result;
        }
    }
}
