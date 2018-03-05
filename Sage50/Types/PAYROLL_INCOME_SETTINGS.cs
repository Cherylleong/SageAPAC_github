/*
 * Created by Ranorex
 * User: wonga01
 * Date: 9/6/2016
 * Time: 11:28
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace Sage50.Types
{
	/// <summary>
	/// Description of PAYROLL_INCOME_SETTINGS.
	/// </summary>
	public class PAYROLL_INCOME_SETTINGS
	{
		public PAYROLL_INCOME_SETTINGS()
		{
		}
		
		public PAYROLL_INCOME_SETTINGS(List<PAYROLL_INCOME> Incomes, Nullable<bool> trackQuebecTips)
        {
            this.Incomes = Incomes;
            this.trackQuebecTips = trackQuebecTips;
        }

		public List<PAYROLL_INCOME> Incomes = new List<PAYROLL_INCOME>();
		public Nullable<bool> trackQuebecTips { get; set; }
	}
}
