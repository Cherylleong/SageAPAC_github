/*
 * Created by Ranorex
 * User: wonda05
 * Date: 6/14/2017
 * Time: 8:21 AM
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
    /// Description of UserCodeModule1.
    /// </summary>
    [TestModule("FB5D49CA-64C5-4599-A208-9C79FDD2005A", ModuleType.UserCode, 1)]
    public class AddCustomer : ITestModule
    {
    	
    	
    	string _varCustomer = "";
    	[TestVariable("C1C14D10-28FB-4DA0-A221-9178BEF04259")]
    	public string varCustomer
    	{
    		get { return _varCustomer; }
    		set { _varCustomer = value; }
    	}
    	
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public AddCustomer()
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
            
            // create record to hold customer data            
            CUSTOMER cus = new CUSTOMER();
            cus.name = "cust" + StringFunctions.RandStr("X(8)");
            
           	// call create method from class to add customer
            ReceivablesLedger._SA_Create(cus);
            ReceivablesLedger._SA_Close();
            
            // set customer name to global var
            this._varCustomer = cus.name;
            
        }
    }
}
