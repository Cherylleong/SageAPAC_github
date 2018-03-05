/*
 * Created by Ranorex
 * User: wonda05
 * Date: 6/1/2017
 * Time: 4:20 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Threading;
using WinForms = System.Windows.Forms;

using Ranorex;
using Ranorex.Core;
using Ranorex.Core.Testing;

namespace Sage50.Scripts
{
    /// <summary>
    /// Description of UserCodeModule1.
    /// </summary>
    [TestModule("A103D595-FB72-4874-BAAC-5CCBD8039A99", ModuleType.UserCode, 1)]
    public class CheckTestCaseState : ITestModule
    {
        /// <summary>
        /// Constructs a new instance.	
        /// </summary>
        public CheckTestCaseState()
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
            
            
            String sTestCaseName = TestSuite.CurrentTestContainer.Name;
            
            
            //ITestCase iCase = TestSuite.Current.GetTestCase(sTestCaseName); // The name of your Test Case

			ITestContainer iCase = TestSuite.Current.GetTestContainer(sTestCaseName);
            
            
            
      		if(iCase.Status == Ranorex.Core.Reporting.ActivityStatus.Failed)
      		{
      			Report.Info(string.Format("TestCase1 {0} Failed!", sTestCaseName));
      			
      			System.IO.File.AppendAllText("c:\\RanorexStandAlone\\log\\log.txt",string.Format("{0} had failed.",sTestCaseName) + Environment.NewLine);
   
      		}       
            
        }
    }
}
