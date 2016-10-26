namespace Game.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Text.RegularExpressions;

    public class MessageRender
    {
        private Dictionary<string, string> replaceVariables = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        private const string variableBegin = @"\{";
        private const string variableEnd = @"\}";

        public MessageRender()
        {
            this.RegisterVariable("datetime", TextUtility.FormatDateTime(DateTime.Now.ToString(), 4));
        }

        public void RegisterVariable(string varName, string value)
        {
            if (varName != null)
            {
                if (this.replaceVariables.ContainsKey(varName))
                {
                    this.replaceVariables[varName] = value;
                }
                else
                {
                    this.replaceVariables.Add(varName, value);
                }
            }
        }

        public string Render(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }
            foreach (KeyValuePair<string, string> pair in this.replaceVariables)
            {
                if (!(string.IsNullOrEmpty(pair.Key) || (text.IndexOf(pair.Key, StringComparison.OrdinalIgnoreCase) == -1)))
                {
                    text = Regex.Replace(text, string.Format("{0}{1}{2}", @"\{", pair.Key, @"\}"), (pair.Value == null) ? "" : pair.Value, RegexOptions.IgnoreCase);
                }
            }
            return text;
        }

        public string this[string key]
        {
            get
            {
                return this.replaceVariables[key];
            }
            set
            {
                this.RegisterVariable(key, value);
            }
        }
    }
}

