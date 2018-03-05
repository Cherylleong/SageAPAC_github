/*
 * Created by Ranorex
 * User: wonda05
 * Date: 5/18/2016
 * Time: 10:40 AM
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

namespace Sage50.BVT
{
    /// <summary>
    /// Description of AddVendor.
    /// </summary>
    [TestModule("B820C327-21C8-4B83-BEDC-49BEF58D4EC4", ModuleType.UserCode, 1)]
    public class AddVendor : ITestModule
    {
    	
    	string _varVendor = "";
    	[TestVariable("bb636f4c-85ee-4588-a2ae-3b7b530613a6")]
    	public string varVendor
    	{
    		get { return _varVendor; }
    		set { _varVendor = value; }
    	}
    	
    	
        /// <summary>
        /// Constructs a new repo.
        /// </summary>
        public AddVendor()
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
            
            // create record to hold vendor data            
            VENDOR vendor = new VENDOR();
            vendor.name = "vendor" + StringFunctions.RandStr("X(8)");
            
           	// call create method from class to add customer
            PayablesLedger._SA_Create(vendor);
            PayablesLedger._SA_Close();
            
            this.varVendor = vendor.name;
        }
    }
}
