using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TokenAssist
{
    class TokenProperty
    {
        public string Name = "";
        public string Key = "";
        public string Value = "";

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
