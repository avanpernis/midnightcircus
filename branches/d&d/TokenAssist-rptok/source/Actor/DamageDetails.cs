namespace TokenAssist
{
    ////////////////////////////////////////////////////////////////////////////
    // Hold details about damage modifiers (eg. immunities, resistances, or
    // vulnerabilities)
    ////////////////////////////////////////////////////////////////////////////
    public class DamageDetails
    {
        public DamageDetails(string type)
        {
            Type = type.ToLower();
            Amount = 0;
            Details = string.Empty;
        }

        public DamageDetails(string type, int amount, string details)
            : this(type)
        {
            Amount = amount;
            Details = details;
        }

        public string Type { get; set; }
        public int Amount { get; set; }
        public string Details { get; set; }

        public override string ToString()
        {
            string result = string.Empty;

            if (Amount != 0)
            {
                result = Amount.ToString() + " ";
            }

            result += Type;

            if (!string.IsNullOrEmpty(Details))
            {
                result += " " + Details;
            }

            return result;
        }
    }
}