/*
 * Created by Ranorex
 * User: wonga01
 * Date: 10/10/2017
 * Time: 14:28
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
    /// Description of AddEmployee.
    /// </summary>
    [TestModule("466DF9E1-1980-4AEE-853C-6AF8F17B8229", ModuleType.UserCode, 1)]
    public class AddEmployee : ITestModule
    {
    	
    	
    	string _varEmployee = "";
    	[TestVariable("5f030f2c-0272-4082-8f21-735bed2808f2")]
    	public string varEmployee
    	{
    		get { return _varEmployee; }
    		set { _varEmployee = value; }
    	}
    	
    	
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public AddEmployee()
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
            
            EMPLOYEE employee = new EMPLOYEE();
            employee.name = StringFunctions.RandStr("A(9)");
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
            
            PayrollLedger._SA_Create(employee);
            PayrollLedger._SA_Close();
            
            // set employee name to global var
            this.varEmployee = employee.name;
            
        }
    }
}
