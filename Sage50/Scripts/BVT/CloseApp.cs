/*
 * Created by Ranorex
 * User: wonda05
 * Date: 5/31/2017
 * Time: 10:24 AM
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
using Sage50.Classes;
using Sage50.Types;
using Sage50.Shared;

namespace Sage50.Scripts
{
    /// <summary>
    /// Description of CloseApp.
    /// </summary>
    [TestModule("BB4DC7E2-AD36-483C-B844-7D859A727587", ModuleType.UserCode, 1)]
    public class CloseApp : ITestModule
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public CloseApp()
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
            
            Simply.repo.SearchTimeout = 20;
            Simply.repo.Self.PressKeys("{Alt down}{f4}{Alt up}");
            
            if(SimplyMessage.repo.OKInfo.Exists())
			{
				SimplyMessage.repo.OK.Click();
			}
            
            
            
            
        }
    }
}
