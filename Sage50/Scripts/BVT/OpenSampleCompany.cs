/*
 * Created by Ranorex
 * User: wonda05
 * Date: 5/29/2017
 * Time: 11:41 AM
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
    /// Description of OpenSampleCompany.
    /// </summary>
    [TestModule("387D42D0-89F8-4C9D-BCA2-0C9F6D8C68A6", ModuleType.UserCode, 1)]
    public class OpenSampleCompany : ITestModule
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public OpenSampleCompany()
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
            
            
	        Simply._SA_StartProgram(true);
   			//Simply._SA_StartProgram(false, false, "C:\\Users\\wonga01\\Documents\\Simply Accounting\\DATA\\test company 3.sai");
            
            
        }
    }
}
