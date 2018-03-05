/*
 * Created by Ranorex
 * User: wonga01
 * Date: 10/11/2017
 * Time: 16:26
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace Sage50.Types
{
	/// <summary>
	/// Description of PAYCHEQUE.
	/// </summary>
	public class PAYCHEQUE
	{
		public PAYCHEQUE()
		{
		}
		
		public GL_ACCOUNT accountPaidFrom = new GL_ACCOUNT();
		public EMPLOYEE employee = new EMPLOYEE();
		public List<PAYCHEQUE_EARNINGS> Earnings = new List<PAYCHEQUE_EARNINGS>();
		public List<PAYCHEQUE_OTHER_INCOMES> Incomes = new List<PAYCHEQUE_OTHER_INCOMES>();
        public List<PAYCHEQUE_VACATION> Vacations = new List<PAYCHEQUE_VACATION>();
		public List<PAYCHEQUE_DEDUCTIONS> Deductions = new List<PAYCHEQUE_DEDUCTIONS>();
		public List<PAYCHEQUE_TAXES> Taxes = new List<PAYCHEQUE_TAXES>();
		public List<PAYCHEQUE_USER_DEFINED_EXPENSES> UserDefExpenses = new List<PAYCHEQUE_USER_DEFINED_EXPENSES>();
		public List<PAYCHEQUE_ENTITLEMENTS> Entitlements = new List<PAYCHEQUE_ENTITLEMENTS>();
		public List<PAYCHEQUE_TIPS> Tips = new List<PAYCHEQUE_TIPS>();
		public List<PROJECT_ALLOCATION> Projects = new List<PROJECT_ALLOCATION>();
		public string action { get; set; }//for datafiles only
		public string paychequeNumber { get; set; }
		public string paychequeDate { get; set; }
		public string periodEndingDate { get; set; }
		public string periodStartDate { get; set; }
		public Nullable<bool> directDeposit { get; set; }
		public string recurringName { get; set; }
		public string recurringFrequency { get; set; }
		public string hrsWorkedInPayPeriod { get; set; }		
	}
}
