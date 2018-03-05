/*
 * Created by Ranorex
 * User: wonga01
 * Date: 10/12/2017
 * Time: 15:41
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
    /// Description of AddPayCheque.
    /// </summary>
    [TestModule("0FE029FD-66C9-4144-BA5C-6429E1CB6FF1", ModuleType.UserCode, 1)]
    public class AddPayCheque : ITestModule
    {
    	
    	string _varEmployee = "";
    	[TestVariable("7ae633ef-f5aa-4f2d-aab4-3ce0a2428841")]
    	public string varEmployee
    	{
    		get { return _varEmployee; }
    		set { _varEmployee = value; }
    	}
    	
    	
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public AddPayCheque()
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
            
            // Setup Employee
            EMPLOYEE employee = new EMPLOYEE();
     
            employee.birthDate = "01/01/78";
            employee.Taxes.taxTable = "British Columbia";
            employee.Income.payPeriodsPerYear = "26";
            
            EMP_INCOME_USE empIncomeUse = new EMP_INCOME_USE();
            empIncomeUse.income = "Regular";            
            empIncomeUse.amountPerUnit = Functions.RandCashAmount(2);
            // need a pause to generate second random number
            System.Threading.Thread.Sleep(1000);
            empIncomeUse.hoursPerPeriod = Functions.RandCashAmount(2);                           
            employee.Income.IncomeData.Add(empIncomeUse);                        
            
            if(this.varEmployee == "")
            {
            	employee.name = StringFunctions.RandStr("A(9)");
            	PayrollLedger._SA_Create(employee);
            	PayrollLedger._SA_Close();
            }
            else
            {
            	employee.name = this.varEmployee;
            }
            
            // Create paycheque
            PAYCHEQUE paychq = new PAYCHEQUE();
			paychq.employee = employee;
			PayrollJournal._SA_Create(paychq);
			if (SimplyMessage.repo.SelfInfo.Exists(Variables.iExistWaitTime) && SimplyMessage.repo.Self.Visible)
			{
				SimplyMessage._SA_HandleMessage(SimplyMessage.repo.Yes);
			}
			// Wait post to complete before closing journal
			System.Threading.Thread.Sleep(2000);
			PayrollJournal._SA_Close();
            
        }
    }
}
