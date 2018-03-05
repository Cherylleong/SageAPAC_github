/*
 * Created by Ranorex
 * User: wonda05
 * Date: 6/2/2016
 * Time: 9:19 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using Sage50.Types;

namespace Sage50.Classes
{
	/// <summary>
	/// Description of Variables.
	/// </summary>
	public static class Variables
	{
		public static bool bUseDataFiles = false;
		public const string sNoTax = " - No Tax";
		
		public static bool isMultiUser = false;
		public static int iExistWaitTime = 5000;
		
		public static SETTINGS globalSettings = new SETTINGS();		
		
		public static string PrintLocation;
		public static string productVersion = "Canadian";
		public static string sLongYear;
		public static bool bAcctEd;
		public static FLAVOR SimplyFlavor;
		public static bool bHistoryMode = false;
		public static bool refreshFlag = true;
		public static bool bTimeBillingEnabled;
		
		public const string sSimplyVersionNumber = "2018";
		public static string sRegPath = String.Format(@"SOFTWARE\Sage Software\Simply Accounting\{0}\", sSimplyVersionNumber);
		public const string s64BitDirectory = @"C:\Program Files (x86)";
		public const string sProgramFilesPath = @"C:\Program Files";		
		public const string sGuid = "{CDB21C1F-4317-4A76-8711-50F9FCF30542}"; //the id used in install shield for Simply - changes every year but the same for each install
		
		public static string sInstalledDirectory = String.Format(@"{0}\Sage 50 Accountant Edition Version {1}", s64BitDirectory, sSimplyVersionNumber); // default installed path for Automation scripts
		public static string sExecutable = "Sage50Accounting.exe";
		
	}
}
