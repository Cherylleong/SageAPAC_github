/*
 * Created by Ranorex
 * User: wonga01
 * Date: 9/6/2016
 * Time: 13:15
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Sage50.Types
{
	/// <summary>
	/// Description of PAYROLL_INCOME.
	/// </summary>
	public class PAYROLL_INCOME
	{
		public PAYROLL_INCOME()
		{
		}
		
		public PAYROLL_INCOME(string incomeName, INCOME_TYPE IncomeType, string unitofMeasure)
        {
            this.incomeName = incomeName;
            this.IncomeType = IncomeType;
            this.unitofMeasure = unitofMeasure;
        
        }

        public string incomeName { get; set; }
		public INCOME_TYPE IncomeType = new INCOME_TYPE();
		public string unitofMeasure { get; set; }
	}
}
