/*
 * Created by Ranorex
 * User: wonga01
 * Date: 11/28/2017
 * Time: 10:59
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
using Sage50.Shared;
using Sage50.Types;

namespace Sage50.Scripts.BVT
{
    
	
	
	/// <summary>
    /// Description of AddGeneralJournal.
    /// </summary>
    [TestModule("A4B0C9D9-E979-41A6-A85D-61FB4965AAB3", ModuleType.UserCode, 1)]
    public class AddGeneralJournal : ITestModule
    {
		
		string _varAccount = "";
		[TestVariable("891b76a8-ccbb-4479-9676-65520f6227e1")]
		public string varAccount
		{
			get { return _varAccount; }
			set { _varAccount = value; }
		}
		
        	
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public AddGeneralJournal()
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
                        
            Settings._SA_Get_AllPayableSettings();
            
            // Setup a new account
            GL_ACCOUNT newAccount = new GL_ACCOUNT();
            
            
            if(this._varAccount == "")
            {
				newAccount.acctNumber = "590"+ StringFunctions.RandStr("9(1)") + " " + StringFunctions.RandStr("A(9)");		// the db is setup to use 4 digits accounts
			
          		GeneralLedger._SA_Create(newAccount);
				GeneralLedger._SA_Close();
            }
			else
			{
				
				newAccount.acctNumber = this._varAccount;
			}
				
			// Post an entry in GJ
			GENERAL_JOURNAL genJournal = new GENERAL_JOURNAL();
			GL_ACCOUNT account2 = new GL_ACCOUNT();
			GJ_ROW row = new GJ_ROW();
			GJ_ROW row2 = new GJ_ROW();
			
			string amount = Functions.RandCashAmount();
			
			row.Account = newAccount;
			row.debitAmt = amount;
			genJournal.GridRows.Add(row);
			
			// Using the currency account will need foreign currency setup			
			row2.Account = Variables.globalSettings.PayableSettings.CurrencyAccounts[0].BankAccount;		
			row2.creditAmt = amount;
			genJournal.GridRows.Add(row2);
			
			genJournal.source = StringFunctions.RandStr("A(8)");
			
			// to be continued
			GeneralJournal._SA_Create(genJournal);
			GeneralJournal._SA_Close();
            
			
        }
   }
}