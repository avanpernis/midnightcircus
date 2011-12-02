using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace WindowsFormsApplication1
{
    class Token
    {
        public string Name = "";
        public string PropertyType = "4ePlayer";

        public List<TokenMacro> Macros = new List<TokenMacro>();
        public List<TokenProperty> Properties = new List<TokenProperty>();

        public void Write(string filename)
        {
            using (StreamWriter file = new StreamWriter(filename))
            {
                StringBuilder builder = new StringBuilder();

                string result = global::WindowsFormsApplication1.Properties.Resources.ContentTemplate;

                result = result.Replace(@"###TOKEN_NAME###", Name);
                result = result.Replace(@"###PROP_TYPE###", PropertyType);

                // build the property block
                builder.Clear();
                foreach (TokenProperty p in Properties)
                {
                    builder.Append(p.ToString());
                }
                result = result.Replace(@"###PROPERTY_SECTION###", builder.ToString());

                // build the macro block
                builder.Clear();
                for (int i = 0; i < Macros.Count; ++i)
                {
                    Macros[i].Index = i + 1;
                    builder.Append(Macros.ToString());
                }
                result = result.Replace(@"###MACRO_SECTION###", builder.ToString());

                file.Write(result);
            }
        }
    }
}
