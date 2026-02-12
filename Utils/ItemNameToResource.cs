using System.Collections.Generic;

namespace ACTQoL.Utils
{
    internal class ItemNameToResource
    {
        public static Dictionary<string, string> ItemToResource = new() { { "barbedhook", "hook" }, { "oldworldwhorl", "whorl" }, { "heartkelpsprout", "heartkelp" }, { "bloodstarlimb", "bloodstar" }, { "stainlessrelic", "key" } };

        public static string GetResourceName(string itemName)
        {
            string lowerName = itemName.ToLower();
            if (ItemToResource.ContainsKey(lowerName))
            {
                return ItemToResource[lowerName];
            }
            if (lowerName.Contains("stowaway"))
            {
                return "stowaways";
            }
            if (lowerName.Contains("claw"))
            {
                return "junk";
            }
            if (lowerName.Contains("costume"))
            {
                return "costume";
            }
            return "junk";
        }
    }
}
