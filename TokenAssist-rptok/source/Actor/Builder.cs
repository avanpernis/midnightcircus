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
            return GetMacroButtonColor(power.Usage);
        }

        protected static ColorValue GetMacroButtonColor(Power.UsageType usage)
        {
            switch (usage)
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
            return GetMacroFontColor(power.Usage);
        }


        protected static ColorValue GetMacroFontColor(Power.UsageType usage)
        {
            switch (usage)
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
