/*
 * Created by Ranorex
 * User: wonda05
 * Date: 8/5/2016
 * Time: 12:14 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sage50.Shared
{
	/// <summary>
	/// Description of StringFunctions.
	/// </summary>
	public class StringFunctions
	{
		static Random s_random = new Random();
		
		public static string RandStr(string sPicture)
        {
            StringBuilder returnVal = new StringBuilder(4096);
            StringBuilder repeatString = new StringBuilder();

            char pictureVal = 'A';
            bool captureLength = false;
            Int32 repeatCount = 0;
            int number = 0;
            foreach (char pictureChar in sPicture)
            {
                if ((captureLength) && (pictureChar != ')'))
                {
                    repeatString.Append(pictureChar);
                }
                else switch (pictureChar)
                    {
                        case 'A':
                            pictureVal = 'A';
                            // Include A-Z and a-z
                            // 65-90 = A-Z
                            // 97-122 = a-z
                            do
                            {
                                number = s_random.Next(65, 122);
                            } while ((number > 90) && (number < 97));
                            returnVal.Append(Convert.ToChar(number));
                            break;
                        case 'P':
                            pictureVal = 'P';
                            // Include ASCII 33 (!) to 126 (~)
                            number = s_random.Next(33, 126);
                            returnVal.Append(Convert.ToChar(number));
                            break;
                        case 'X':
                            pictureVal = 'X';
                            // Include A-Z and a-z
                            // 48-57 = 0-9
                            // 65-90 = A-Z
                            // 97-122 = a-z
                            do
                            {
                                number = s_random.Next(48, 122);
                            } while (((number > 57) && (number < 65)) || ((number > 90) && (number < 97)));
                            returnVal.Append(Convert.ToChar(number));
                            break;
                        case '9':
                            pictureVal = '9';
                            // Include 48-57 = 0-9
                            number = s_random.Next(48, 57);
                            returnVal.Append(Convert.ToChar(number));
                            break;
                        case '(':
                            if (pictureVal != '\0')
                            {
                                captureLength = true;
                            }
                            else
                                returnVal.Append(pictureChar);
                            break;
                        case ')':
                            captureLength = false;
                            if (pictureVal != '\0')
                            {
                                repeatCount = 0;
                                if (Int32.TryParse(repeatString.ToString(), out repeatCount))
                                {
                                    repeatCount -= 1;
                                    if (repeatCount > 0)
                                    {
                                        returnVal.Append(RandStr(new String(pictureVal, repeatCount)));
                                    }
                                }
                                else
                                {
                                    Assert.Fail("Invalid length string encountered for RandStr picture format");
                                }
                                repeatString.Clear();
                            }
                            else
                                returnVal.Append(pictureChar);
                            break;
                        default:
                            pictureVal = '\0';
                            returnVal.Append(pictureChar);
                            break;
                    }
            }
            return returnVal.ToString();
        }
	}
}
