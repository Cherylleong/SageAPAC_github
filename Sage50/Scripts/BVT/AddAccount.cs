/*
 * Created by Ranorex
 * User: wonda05
 * Date: 6/30/2017
 * Time: 3:01 PM
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
    [TestModule("0599B095-580C-4365-B6C1-E7FBFBB0A3B4", ModuleType.UserCode, 1)]
    public class AddAccount : ITestModule
    {
    	
    	
    	
    	string _varAccount = "";
    	[TestVariable("480a17b3-555e-4f8c-941a-1858fbb0b777")]
    	public string varAccount
    	{
    		get { return _varAccount; }
    		set { _varAccount = value; }
    	}
    	
    	
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public AddAccount()
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
            
            GL_ACCOUNT account = new GL_ACCOUNT();
			account.acctNumber = "571"+ StringFunctions.RandStr("9(1)") + " " + StringFunctions.RandStr("A(9)");		// the db is setup to use 4 digits accounts
		
          	GeneralLedger._SA_Create(account);
			GeneralLedger._SA_Close();
			
			this._varAccount = account.acctNumber;

        }
    }
}
