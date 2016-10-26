namespace Game.Utils
{
    using System;
    using System.Text.RegularExpressions;

    public sealed class TypeParse
    {
        private TypeParse()
        {
        }

        public static bool IsNumericArray(string[] strNumber)
        {
            if (strNumber == null)
            {
                return false;
            }
            if (strNumber.Length < 1)
            {
                return false;
            }
            foreach (string str in strNumber)
            {
                if (!Validate.IsNumeric(str))
                {
                    return false;
                }
            }
            return true;
        }

        public static int SafeLongToInt32(long expression)
        {
            if (expression > 0x7fffffffL)
            {
                return 0x7fffffff;
            }
            if (expression < -2147483648L)
            {
                return -2147483648;
            }
            return (int) expression;
        }

        public static bool StrToBool(object expression, bool defValue)
        {
            if (expression != null)
            {
                if (string.Compare(expression.ToString(), "true", true) == 0)
                {
                    return true;
                }
                if (string.Compare(expression.ToString(), "false", true) == 0)
                {
                    return false;
                }
            }
            return defValue;
        }

        public static float StrToFloat(object expression, float defValue)
        {
            if ((expression == null) || (expression.ToString().Length > 10))
            {
                return defValue;
            }
            float num = defValue;
            if ((expression != null) && Regex.IsMatch(expression.ToString(), @"^([-]|[0-9])[0-9]*(\.\w*)?$"))
            {
                num = Convert.ToSingle(expression);
            }
            return num;
        }

        public static int StrToInt(object expression, int defValue)
        {
            if (expression == null)
            {
                return defValue;
            }
            string input = expression.ToString();
            if (!(((input.Length > 0) && (input.Length <= 11)) && Regex.IsMatch(input, "^[-]?[0-9]*$")))
            {
                return defValue;
            }
            if (((input.Length >= 10) && ((input.Length != 10) || (input[0] != '1'))) && (((input.Length != 11) || (input[0] != '-')) || (input[1] != '1')))
            {
                return defValue;
            }
            return Convert.ToInt32(input);
        }
    }
}

