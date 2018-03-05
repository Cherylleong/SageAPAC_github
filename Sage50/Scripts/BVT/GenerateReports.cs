/*
 * Created by Ranorex
 * User: wonga01
 * Date: 11/30/2017
 * Time: 15:00
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
    /// Description of GenerateReports.
    /// </summary>
    [TestModule("A29D8A79-AE7D-4F13-B473-ABD678564046", ModuleType.UserCode, 1)]
    public class GenerateReports : ITestModule
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public GenerateReports()
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
            
            
			// Open and closes 6 reports
			ReportsFromMenu._Rpt_Financials_BalanceSheet_Standard();
			
			ReportsFromMenu._Rpt_Financials_AllJournalEntries();
			
			ReportsFromMenu._Rpt_Receivables_CustomerSalesDetail();
			
			ReportsFromMenu._Rpt_Payables_VendorAgedDetail();
			
			if (Variables.productVersion == "Canadian")
			{
				ReportsFromMenu._Rpt_Payroll_EmployeeDetail();
			}
			
			ReportsFromMenu._Rpt_InventoryServices_TransactionDetails();
			
        }
    }
}
