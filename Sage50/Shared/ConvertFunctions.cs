/*
 * Created by Ranorex
 * User: wonda05
 * Date: 8/4/2016
 * Time: 4:26 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Sage50.Shared
{
	/// <summary>
	/// Description of Class1.
	/// </summary>
	public static class ConvertFunctions
	{
		public static string TextToComma(string sTarget)
        {
            sTarget = sTarget.Replace("\\comma", ",");

            return sTarget;
        }
		
		/// <summary>
        /// converts "," to "\comma"
        /// </summary>
        /// <param name="sTarget">string that contains comma(s)</param>
        /// <returns>string with the text "\comma"</returns>
		public static string CommaToText(string sTarget)
        {
            sTarget = sTarget.Replace(",", "\\comma");

            return sTarget;
        }
		
		public static string BlankStringToNULL(string sTarget)
        {
            if (sTarget == "")
                sTarget = null;

            return sTarget;
        }
		
		public static bool StringToBool(string str) //  converts a string value as "false" or "true" to its corresponding boolean vaule (case insensitive)
        {
            switch (str.ToLower())
            {
                case "true":
                case "1":
                    return true;
                default:
                    return false;
            }                   
        }

        public static string NullToBlankString(string sTarget)
        {
            if (sTarget == null)
            {
                return "";
            }

            return sTarget;
        }

        public static string BoolToString(bool? value, bool bToDigit = true)
        {
            if (value == true)
            {
                if (bToDigit)
                {
                    return "1";
                }
                else
                {
                    return "true";
                }
            }
            else
            {
                if (bToDigit)
                {
                    return "0";
                }
                else
                {
                    return "false";
                }
            }
        }
    }
}
