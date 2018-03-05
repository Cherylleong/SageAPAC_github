/*
 * Created by Ranorex
 * User: wonga01
 * Date: 7/10/2017
 * Time: 11:11
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

namespace Sage50.Scripts.BVT
{
    /// <summary>
    /// Description of EditSettings.
    /// </summary>
    [TestModule("8AEBB41C-BA1C-4908-92FD-CC5BE06A1E64", ModuleType.UserCode, 1)]
    public class EditSettings : ITestModule
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public EditSettings()
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
            
            Variables.globalSettings.CompanyInformation.Address.street2 = StringFunctions.RandStr("A(9)");
            Settings._SA_SetCompanyInformation();
            
        }
    }
}
