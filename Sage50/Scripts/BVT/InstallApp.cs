/*
 * Created by Ranorex
 * User: wonda05
 * Date: 6/14/2017
 * Time: 11:18 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Threading;
using System.IO;
using WinForms = System.Windows.Forms;

using Ranorex;
using Ranorex.Core;
using Ranorex.Core.Testing;
using Sage50.Classes;
using Sage50.Types;
using Sage50.Shared;

namespace Sage50.Scripts.BVT
{
	
	
    /// <summary>
    /// Description of UserCodeModule1.
    /// </summary>
    [TestModule("1C895F1E-6E61-46F2-BBA7-A4B42E75E4F0", ModuleType.UserCode, 1)]
    public class InstallApp : ITestModule
    {
    	
    	
    	
    	
    	string _sBuildnumber = "\"\"";
    	//string _sBuildnumber = @"""1025151""";
    	[TestVariable("f5a583f8-4d79-44c6-80f4-446cdb3d44b1")]
    	public string sBuildnumber
    	{
    		get { return _sBuildnumber; }
    		set { _sBuildnumber = value; }
    	}
    	
    	
    	
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public InstallApp()
        {
            // Do not delete - a parameterless constructor is required!
        }

        /// <summary>
        /// Performs the playback of actions in this module.
        /// </summary>
        /// <remarks>You should not call this method directly, instead pass the module
        /// instance to the <see cref="TestModuleRunner.Run(ITestModule)"/> method
        /// that will in turn invoke this method.</remarks>
        void ITestModule.Run()
        {
            Mouse.DefaultMoveTime = 300;
            Keyboard.DefaultKeyPressTime = 100;
            Delay.SpeedFactor = 1.0;
            
            
            // string sInstallPath = string.Format(@"{0}\{1}\",Environment.GetEnvironmentVariable("SA_InstallPath", EnvironmentVariableTarget.User), sBuildnumber.Replace("\"",""));
            // Format the string with or without \ depends on c:\temp\installinfo.csv
            string sInstallPath = string.Format(@"{0}{1}",Environment.GetEnvironmentVariable("SA_InstallPath", EnvironmentVariableTarget.User), sBuildnumber.Replace("\"",""));
			// vars for registration
			string sSerialNumOne = "16ae5u2";
			string sSerialNumTwo = "9999985";
			string sClientId = "3099099985";
			string sKeyTwo = "LB81";
			string sKeyThree = "GXNT";
			string sKeyFour = "SRFR";
			string sKeyFive = "BRVF";
			string sCompanyName = "SageAuto";
			            	
			//16AE5U2-LB81-GXNT-SRFR-BRVF
            if(Simply.repo.SelfInfo.Exists())
            {
            	Simply._SA_CloseProgram();
            }
            
            // check if Sage 50 is installed
            if(File.Exists(Simply._SA_GetProgramPath() + Variables.sExecutable))
            {
            	SimplyUninstall._SA_Uninstall();
            }
            
            
            // do the install
            SimplyInstall._SA_Install(sInstallPath,"16AE5U2","9999985");
            
            // activate Sage50
            Simply._SA_StartSage50();
            while (!Register.repo.Self.Visible)
            {
            	Thread.Sleep(1000);
            }
            
            Register._SA_Register(sCompanyName, sSerialNumOne, sSerialNumTwo, sClientId, sSerialNumOne, sKeyTwo, sKeyThree, sKeyFour, sKeyFive);
            
                        
        }
    }
}
