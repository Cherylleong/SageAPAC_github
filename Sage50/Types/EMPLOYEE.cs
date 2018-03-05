/*
 * Created by Ranorex
 * User: wonga01
 * Date: 8/30/2016
 * Time: 15:01
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Sage50.Types
{
	/// <summary>
	/// Description of EMPLOYEE.
	/// </summary>
	public class EMPLOYEE : Person
	{
		public EMPLOYEE()
		{
		}
		
		public LANGUAGE_PREF languagePreference = new LANGUAGE_PREF();
		
		public EMP_TAX Taxes = new EMP_TAX();
		public EMP_INCOME Income = new EMP_INCOME();
        public EMP_DEDUCT[] Deductions = new EMP_DEDUCT[20] { new EMP_DEDUCT(), new EMP_DEDUCT(), new EMP_DEDUCT(), new EMP_DEDUCT(), new EMP_DEDUCT(), new EMP_DEDUCT(), new EMP_DEDUCT(), new EMP_DEDUCT(), new EMP_DEDUCT(), new EMP_DEDUCT(), new EMP_DEDUCT(), new EMP_DEDUCT(), new EMP_DEDUCT(), new EMP_DEDUCT(), new EMP_DEDUCT(), new EMP_DEDUCT(), new EMP_DEDUCT(), new EMP_DEDUCT(), new EMP_DEDUCT(), new EMP_DEDUCT()};
		public EMP_EXPENSE Expenses = new EMP_EXPENSE();
		public EMP_ENTITLE Entitlements = new EMP_ENTITLE();
		public EMP_DIRECT_DEP DirectDeposits = new EMP_DIRECT_DEP();
		public EMP_REPORTING Reporting = new EMP_REPORTING();

        public string employeeName { get; set; }
		public string sin { get; set; }
		public string birthDate { get; set; }
		public string hireDate { get; set; }
		public string terminateDate { get; set; }
		public string roeCode { get; set; }

		
		public string jobCategory { get; set; }
		public string ssn { get; set; }
		
	}
}
