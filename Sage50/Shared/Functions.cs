/*
 * Created by Ranorex
 * User: wonda05
 * Date: 6/2/2016
 * Time: 2:41 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Win32;
using Ranorex;
using Sage50.Types;
using Sage50.Classes;

namespace Sage50.Shared
{
	/// <summary>
	/// Description of Functions.
	/// </summary>
	/// 
	
	
	public static class Functions
	{
		public static bool GoodData(object var)
		{
//			if (var != null)
//			{
//				object o = var.GetType();
//								
//			}
			return (var != null);
		}
		
		public static string GetField(string sString, string sDelim, int iField)
        {
            string[] tmpStrArray = { sDelim };
            tmpStrArray = sString.Split(tmpStrArray, StringSplitOptions.None);
            if (iField > tmpStrArray.Length)
                return "";
            else
                return tmpStrArray[iField-1];   // zero based
        }
		
		public static int ListFind<T>(List<T> lList, T aItem)
        {
            return lList.IndexOf(aItem);
        }

        /// <summary>
        /// returns the index of a string item in string collections or returns -1 if not found
        /// this is specifically created to handle the collection List<string[]>, which is usually used to stores values from containers
        /// </summary>
        /// <typeparam name="T">generic type</typeparam>
        /// <param name="lList">collection to search in</param>
        /// <param name="aItem">item to find</param>
        /// <returns>0 based index if found. otherwise, -1</returns>
        public static int ListFind<T>(IList<T> lList, string aItem)
        {
            List<string> lsTemp = new List<string>();
            for (int x = 0; x < lList.Count; x++)
            {
                lsTemp.Add(lList[x].ToString());
            }

            return lsTemp.IndexOf(aItem);
        }
		
		public static FileStream FileOpen(string path, string mode)
        {
            FileStream fs;

            switch (mode)
            {
                case "FM_READ":
                    fs = File.Open(path, FileMode.Open, FileAccess.Read);
                    break;
                case "FM_WRITE":
                    fs = File.Open(path, FileMode.Create, FileAccess.Write);
                    break;
                case "FM_UPDATE":
                    fs = File.Open(path, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read);
                    break;
                case "FM_APPEND":
                    fs = File.Open(path, FileMode.Append, FileAccess.Write);
                    break;
                default:
                    fs = null;
                    break;
            }

            return fs;
        }
						
		public static void Verify(object aActual, object aExpected, string sDesc)
        {
            if (!aActual.Equals(aExpected))
            {
                try
                {
                    if (sDesc == null)
                    {
                        Assert.AreEqual(aExpected, aActual);
                    }
                    else
                    {
                        Assert.AreEqual(aExpected, aActual, sDesc);                  
                    }
                }
                catch (AssertFailedException e)
                {
                	Trace.WriteLine(e.Message);
                    if (sDesc == null)
                    {
             
                        Trace.WriteLine(String.Format("Verify failed - got {0} expected {1}", aActual, aExpected));
                    }
                    else
                    {
                        Trace.WriteLine(String.Format("Verify {0} failed - got {1} expected {2} ", sDesc, aActual, aExpected));
                    }
                }
            }
        }
		 
		public static string PrepStringsFromDataFiles(string sTarget)
        {
            sTarget = ConvertFunctions.TextToComma(sTarget);
            sTarget = ConvertFunctions.BlankStringToNULL(sTarget);

            return sTarget;
        }
		
		public static string RandCashAmount()
        {
            return RandCashAmount(null, null);
        }

        public static string RandCashAmount(int? iDecimalLeft)
        {
            return RandCashAmount(iDecimalLeft, null);
        }

        public static string RandCashAmount(int? iDecimalLeft, int? iDecimalRight)
        {
            // Generates a Random string with iDecimalLeft numbers the left of the decimal, and iDecimalRight places on the right
            // Guaranteed to not have a zero in the first position
            // Defaults are 2 to the left and 0 to the right
            if (iDecimalLeft == null)
            {
                iDecimalLeft = 2;
            }
            // Wait for the next random number to be genearated before using it
            System.Threading.Thread.Sleep(1000);
            
            Random rnd = new Random();
            string cashWholeN;
            
            switch(iDecimalLeft)
            {
                case 1:
                    cashWholeN = Convert.ToString(rnd.Next(1, 9));
                    break;
                case 2:
                    cashWholeN = Convert.ToString(rnd.Next(10, 99));
                    break;
                case 3:
                    cashWholeN = Convert.ToString(rnd.Next(100, 999));
                    break;
                case 4:
                    cashWholeN = Convert.ToString(rnd.Next(1000, 9999));
                    break;
                case 5:
                    cashWholeN = Convert.ToString(rnd.Next(10000, 99999));
                    break;
                default:
                    return "Invalid random cash amount input, iDecimalLeft must be between zero and five";
            }
            
            return cashWholeN + (iDecimalRight != null ? StringFunctions.RandStr(".9(" + iDecimalRight.ToString() + ")") : ".");
        }

        public static string Str(double nNum, int iWidth = 0, int iDec = 0)
        {

            //base case, if just a number is given
            if (iWidth <= 0 && iDec <= 0 || iWidth <= 0)
                return Math.Round(nNum).ToString();
            //Second case, if a number and width are given (or an invalid iDec)
            else if (iDec <= 0)
            {
                nNum = nNum = Math.Round(nNum);
                String nNumTempString = nNum.ToString();
                if (iWidth <= nNumTempString.Length)
                    return nNumTempString;
                else
                {
                    for (int i = 0; i < iWidth - nNumTempString.Length; i++)
                        nNumTempString = " " + nNumTempString;
                    return nNumTempString;
                }
            }

            //lastly, if everything is used
            else
            {
                nNum = nNum = Math.Round(nNum, iDec);
                String nNumTempString = nNum.ToString();
                int nNumLength = nNumTempString.Length;
                if (iWidth <= nNumLength)
                    return nNumTempString;
                else
                {
                    for (int i = 0; i < iWidth - nNumLength; i++)
                        nNumTempString = " " + nNumTempString;
                    return nNumTempString;
                }
            }
        }
                
        public static void SplitAccountNumberString(string sOrig, out string num, out string name)
        {
            num = sOrig.Split(' ')[0];
            name = sOrig.Substring(num.Length + 1);
        }
        
        public static bool IsDigit(string input)
        {
            if (input != null)
            {
                double output;
                return Double.TryParse(Convert.ToString(input), out output);
            }
            else
            {
                return false;
            }
        }
        
        public static string SYS_GetRegistryValue(uint iKey, string sPath, string sItem, bool bConvert = false)
        {				        	
            //Can't get a value using the iKey directly. Will query to see if it
            //is the same as any known types and use the correct HKEY qualifier
            //to find the item we want to get.
            if (iKey == RegInc.HKEY_CLASSES_ROOT)
            {
                RegistryKey rk = Registry.ClassesRoot.OpenSubKey(sPath);
                if (GoodData(rk))
                {
                	if (GoodData(rk.GetValue(sItem)))
	                {
	                	return rk.GetValue(sItem).ToString();
	                }
                }      
            }
            else if (iKey == RegInc.HKEY_CURRENT_USER)
            {
                RegistryKey rk = Registry.CurrentUser.OpenSubKey(sPath);
                if (GoodData(rk))
                {
                	if (GoodData(rk.GetValue(sItem)))
	                {
	                	return rk.GetValue(sItem).ToString();
	                }
                }
            }
            else if (iKey == RegInc.HKEY_LOCAL_MACHINE)
            {
                RegistryKey rk = Registry.LocalMachine.OpenSubKey(sPath);
                if (GoodData(rk))
                {
                	if (GoodData(rk.GetValue(sItem)))
	                {
	                	return rk.GetValue(sItem).ToString();
	                }
                }                
            }
            else if (iKey == RegInc.HKEY_USERS)
            {
                RegistryKey rk = Registry.Users.OpenSubKey(sPath);
                if (GoodData(rk))
                {
                	if (GoodData(rk.GetValue(sItem)))
	                {
	                	return rk.GetValue(sItem).ToString();
	                }
                }      
            }
            else if (iKey == RegInc.HKEY_PERFORMANCE_DATA)
            {
                RegistryKey rk = Registry.PerformanceData.OpenSubKey(sPath);
                if (GoodData(rk))
                {
                	if (GoodData(rk.GetValue(sItem)))
	                {
	                	return rk.GetValue(sItem).ToString();
	                }
                }      
            }
            else if (iKey == RegInc.HKEY_CURRENT_CONFIG)
            {
                RegistryKey rk = Registry.CurrentConfig.OpenSubKey(sPath);
                if (GoodData(rk))
                {
                	if (GoodData(rk.GetValue(sItem)))
	                {
	                	return rk.GetValue(sItem).ToString();
	                }
                }      
            }
            else if (iKey == RegInc.HKEY_DYN_DATA)
            {
                RegistryKey rk = Registry.PerformanceData.OpenSubKey(sPath);
                if (GoodData(rk))
                {
                	if (GoodData(rk.GetValue(sItem)))
	                {
	                	return rk.GetValue(sItem).ToString();
	                }
                }      
            }
            return null;       
        }        
        
        public static void LaunchAProgram(string sProgName)
        {        	
        	System.Diagnostics.Process process = new System.Diagnostics.Process();
        	System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();        
        	
        	startInfo.WindowStyle = ProcessWindowStyle.Hidden;        	
        	startInfo.UseShellExecute = false;
        	startInfo.FileName = sProgName;         
        	process.StartInfo = startInfo;
        	process.Start();        	
        }
        
        public static void InstallSage50(string sBuildPath, REGI_DATA regdata)
        {
        	InstallSage50(sBuildPath, regdata, true, false);
        }
        public static void InstallSage50(string sBuildPath, REGI_DATA regdata, bool bOpenSampleDb)
        {
        	InstallSage50(sBuildPath, regdata, bOpenSampleDb, false);
        }
        public static void InstallSage50(string sBuildPath, REGI_DATA regdata, bool bOpenSampleDb, bool bServerInstall)
        {
        	bool bInstallSuccess = false;
        	bool bRegister = true;        	
        	string sInstalledPath = "";
        	bool bNeedCleanup;
        	
        	string sCompanyName = regdata.Company;
        	string sSerial1 = regdata.SerialNum1;
        	string sSerial2 = regdata.SerialNum2;
        	string sClientID = regdata.ClientID;;
        	string sKeyCode1 = sSerial1;
        	string sKeyCode2 = "";
        	string sKeyCode3 = "";
        	string sKeyCode4 = "";
        	string sKeyCode5 = "";
        	
        	if (GoodData(regdata.ClientID) && GoodData(regdata.Keycode2) && GoodData(regdata.Keycode3) && GoodData(regdata.Keycode4) && GoodData(regdata.Keycode5))
        	{        		
	        	sKeyCode2 = regdata.Keycode2;
	        	sKeyCode3 = regdata.Keycode3;
	        	sKeyCode4 = regdata.Keycode4;
	        	sKeyCode5 = regdata.Keycode5;	        	
        	}
        	else
        	{
        		bRegister = false;
        	}
        	
        	if (!GoodData(bOpenSampleDb))
        	{
        		bOpenSampleDb = true;
        	}
        	
        	// Close Sage 50 if necessary
        	if (Simply.repo.SelfInfo.Exists())
        	{
        		Simply._SA_CloseProgram();
        		// Make sure db is released from CM service
        		System.Threading.Thread.Sleep(37000);
        	}
        	        	
        	sInstalledPath = Simply._SA_GetProgramPath();
        	bNeedCleanup = CleanupNeeded(sInstalledPath);
        	
        	bool bCleanCompleted;
        	if (bNeedCleanup)
        	{        		
        		bCleanCompleted = SimplyUninstall._SA_Uninstall();
        	}
        	else
        	{
        		bCleanCompleted = true;
        	}
        	
        	// Install Sage 50
        	bInstallSuccess = SimplyInstall._SA_Install(sBuildPath, sSerial1, sSerial2, bServerInstall);
        	
        	if (!bServerInstall && bRegister)
        	{
        		// Start sage 50
        		Simply._SA_StartSage50();
        		
        		// Register
        		Register._SA_Register(sCompanyName, sSerial1, sSerial2, sClientID, sKeyCode1, sKeyCode2, sKeyCode3, sKeyCode4, sKeyCode5);
        		
        		// Wait for Select company dialog to appear
        		while (!SelectCompany.repo.SelfInfo.Exists())
        		{
        			System.Threading.Thread.Sleep(1000);					       		
        		}        		        	
        		
        		// Open sample company or stop at Select company dialog
        		if (bOpenSampleDb)
        		{        		
        			Simply._SA_StartProgram(true);
        		}
        	}
        }
        
        public static bool CleanupNeeded()
        {
        	return CleanupNeeded(null);
        }
        public static bool CleanupNeeded(string sProgramPath)
        {
        	bool bNeedCleanup = false;
        	
        	if (!GoodData(sProgramPath))
        	{
        		sProgramPath = Variables.sInstalledDirectory;        		
        	}
        	
        	if (!sProgramPath.Trim().EndsWith(@"\"))
        	{
        		sProgramPath = sProgramPath + @"\";
        	}
        	
        	if (File.Exists(sProgramPath + Variables.sExecutable))
        	{
        		bNeedCleanup = true;
        	}
        	
        	// Server only install
        	if (File.Exists(String.Format(@"{0}\winsim\ConnectionManager\System.ComponentModel.Composition.dll", Variables.s64BitDirectory)))
        	{
        		bNeedCleanup = true;	
        	}
        	        	
        	return bNeedCleanup;
        }
		        
        public static void RemoveExistingFile(string filename)
        {
        	try
        	{
        		if (File.Exists(filename))
        		{
        			File.Delete(filename);
        		}	
        	}
        	catch (Exception ex)
        	{
        		Console.WriteLine(ex.Message);
        	}        	
        }
        
        public static bool VerifyFileExists(string sFile)
        {        	
    		if (File.Exists(sFile))
    		{
    			return true;
    		}
    		else
    		{
    			return false;
    		}        	        	
        }
        
        
//        public static string RandPick(this Ranorex.ComboBox  cmbBox)
//        {
//            Random randNum = new Random();
//            int r = randNum.Next(cmbBox.Items.Count);
//            string sPicked = cmbBox.Items[r].Text;
//            return sPicked;
//        }
        
    }
	

}
