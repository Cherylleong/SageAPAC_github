/*
 * Created by Ranorex
 * User: wonga01
 * Date: 9/6/2016
 * Time: 13:25
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace Sage50.Types
{
	/// <summary>
	/// Description of EMP_INCOME.
	/// </summary>
	public class EMP_INCOME
	{
		public EMP_INCOME()
		{
		}
		
		public GL_ACCOUNT wageExpenseAccount = new GL_ACCOUNT();
		public List<EMP_INCOME_USE> IncomeData = new List<EMP_INCOME_USE>();
		public string payPeriodsPerYear { get; set; }
		public Nullable<bool> retainVacationCheckBox { get; set; }
		public Nullable<bool> calVacationOnVacationCheckBox { get; set; }
		public string retainVacationPercentage { get; set; }
		public Nullable<bool> recordInLinkedAccounts { get; set; }
	}
}
