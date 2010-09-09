using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TokenAssist
{
    class TokenProperty
    {       
        public TokenProperty(string name, object value)
        {
            Name = Key = name;
            Value = value.ToString();
        }

        public string Name { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }

        public override string ToString()
        {
            string result = Properties.Resources.TokenPropertyTemplate;
            result = result.Replace(@"###PROP_NAME###", Name);
            result = result.Replace(@"###PROP_KEY###", Key);
            result = result.Replace(@"###PROP_VALUE###", Value);
            return result;
        }
    }
}
