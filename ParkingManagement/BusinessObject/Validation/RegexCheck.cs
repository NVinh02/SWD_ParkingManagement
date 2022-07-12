using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BusinessObject.Validation
{
    public static class RegexCheck
    {
        public static string CapitalizeFirstLetterOfEveryWord = @"^(\b[A-Z]{1}\w*\s*)+$"; //Work --> Example: Abc Abc, C C, Kek Kek Kek | Failed to catch: AA AA, BBB C, CCCC and so on
        public static string VehicleBikePlateNumber = @"^[0-9]{2}[-][A-Z]{1}[0-9]{1}[\s]{1}[0-9]{4,5}$";//Good --> Example: 59-Z1 12345, 59-Z1 1234
        public static string VehicleCarPlateNumber = @"^[0-9]{2}[A-Z]{1}[-][0-9]{5}$"; //Good --> Example: 30V-12345

        public static bool CheckStringMatchWithRegex(string s, string regex)
        {
            Regex rg = new Regex(regex);
            if (rg.IsMatch(s) == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
