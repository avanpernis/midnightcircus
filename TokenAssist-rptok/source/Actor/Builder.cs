using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TokenAssist
{
    public class Builder
    {
        protected static ColorValue GetMacroButtonColor(Power power)
        {
            switch (power.Usage)
            {
                case Power.UsageType.AtWill:
                    return Color.green;
                case Power.UsageType.Encounter:
                case Power.UsageType.Recharge:
                    return Color.red;
                case Power.UsageType.Daily:
                    return Color.black;
                default:
                    return Color.white;
            }
        }

        protected static ColorValue GetMacroFontColor(Power power)
        {
            switch (power.Usage)
            {
                default:
                case Power.UsageType.AtWill:
                    return Color.black;
                case Power.UsageType.Encounter:
                case Power.UsageType.Daily:
                case Power.UsageType.Recharge:
                    return Color.white;
            }
        }
    }
}
