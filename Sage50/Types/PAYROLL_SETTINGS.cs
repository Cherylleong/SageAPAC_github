/*
 * Created by Ranorex
 * User: wonga01
 * Date: 9/6/2016
 * Time: 10:36
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace Sage50.Types
{
	/// <summary>
	/// Description of PAYROLL_SETTINGS.
	/// </summary>
	public class PAYROLL_SETTINGS
	{
		public PAYROLL_SETTINGS()
		{
		}
		
		 public PAYROLL_SETTINGS(PAYROLL_INCOME_SETTINGS IncomeSettings, PAYROLL_TAXES_SETTINGS TaxSettings, List<PAYROLL_DEDUCTION> Deductions, PAYROLL_ENTITLEMENT_SETTINGS EntitlementSettings, List<PAYROLL_REMITTANCE> Remittances, List<PAYROLL_JOB> JobCategories, PAYROLL_ADDITIONAL AdditionalPayrollSettings, INCOME_SPECIFIC_ACCOUNTS IncomeAccounts, INCOME_NAME[] AdditionalIncome, DEDUCTION_NAME[] AdditionalDeduction, TAXES_LINKED_ACCOUNTS TaxAccounts, USER_DEFINED_ACCOUNTS UserDefinedAccounts, Nullable<bool> UsePayrollExpenseGroups)
        {
            this.IncomeSettings = IncomeSettings;
		    this.TaxSettings = TaxSettings;
		    this.Deductions = Deductions;
		    this.EntitlementSettings = EntitlementSettings;
		    this.Remittances = Remittances;
		    this.JobCategories = JobCategories;
		    this.AdditionalPayrollSettings = AdditionalPayrollSettings;
		    this.IncomeAccounts = IncomeAccounts;
		    this.AdditionalIncome = AdditionalIncome;
		    this.AdditionalDeduction = AdditionalDeduction;
		    this.TaxAccounts = TaxAccounts;
		    this.UserDefinedAccounts = UserDefinedAccounts;
            this.UsePayrollExpenseGroups = UsePayrollExpenseGroups;

        }

		public PAYROLL_INCOME_SETTINGS IncomeSettings = new PAYROLL_INCOME_SETTINGS();
		public PAYROLL_TAXES_SETTINGS TaxSettings = new PAYROLL_TAXES_SETTINGS();
		public List<PAYROLL_DEDUCTION> Deductions = new List<PAYROLL_DEDUCTION>();
		public PAYROLL_ENTITLEMENT_SETTINGS EntitlementSettings = new PAYROLL_ENTITLEMENT_SETTINGS();
		public List<PAYROLL_REMITTANCE> Remittances = new List<PAYROLL_REMITTANCE>();
		public List<PAYROLL_JOB> JobCategories = new List<PAYROLL_JOB>();
		public PAYROLL_ADDITIONAL AdditionalPayrollSettings = new PAYROLL_ADDITIONAL();
		public INCOME_SPECIFIC_ACCOUNTS IncomeAccounts = new INCOME_SPECIFIC_ACCOUNTS();
		public INCOME_NAME[] AdditionalIncome = new INCOME_NAME[20] { new INCOME_NAME(), new INCOME_NAME(), new INCOME_NAME(), new INCOME_NAME(), new INCOME_NAME(), new INCOME_NAME(), new INCOME_NAME(), new INCOME_NAME(), new INCOME_NAME(), new INCOME_NAME(), new INCOME_NAME(), new INCOME_NAME(), new INCOME_NAME(), new INCOME_NAME(), new INCOME_NAME(), new INCOME_NAME(), new INCOME_NAME(), new INCOME_NAME(), new INCOME_NAME(), new INCOME_NAME()};
        public DEDUCTION_NAME[] AdditionalDeduction = new DEDUCTION_NAME[20] { new DEDUCTION_NAME(), new DEDUCTION_NAME(), new DEDUCTION_NAME(), new DEDUCTION_NAME(), new DEDUCTION_NAME(), new DEDUCTION_NAME(), new DEDUCTION_NAME(), new DEDUCTION_NAME(), new DEDUCTION_NAME(), new DEDUCTION_NAME(), new DEDUCTION_NAME(), new DEDUCTION_NAME(), new DEDUCTION_NAME(), new DEDUCTION_NAME(), new DEDUCTION_NAME(), new DEDUCTION_NAME(), new DEDUCTION_NAME(), new DEDUCTION_NAME(), new DEDUCTION_NAME(), new DEDUCTION_NAME(), };
		public TAXES_LINKED_ACCOUNTS TaxAccounts = new TAXES_LINKED_ACCOUNTS();
		public USER_DEFINED_ACCOUNTS UserDefinedAccounts = new USER_DEFINED_ACCOUNTS();
		public Nullable<bool> UsePayrollExpenseGroups { get; set; }
	}
}
