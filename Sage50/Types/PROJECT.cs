/*
 * Created by Ranorex
 * User: wonga01
 * Date: 8/30/2016
 * Time: 14:59
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace Sage50.Types
{
	/// <summary>
	/// Description of PROJECT.
	/// </summary>
	public class PROJECT : LedgerData
	{
		public PROJECT()
		{
		}
		
		public PROJECT_STATUS status = new PROJECT_STATUS();
		public List<PROJECT_BUDGET> Budgets = new List<PROJECT_BUDGET>();

        //public string name { get; set; }   // NC duplicate with name field under LedgerData
		public string startDate { get; set; }
		public string endDate { get; set; }
		public string revenue { get; set; }
		public string expense { get; set; }

		public Nullable<bool> budgetCheckBox { get; set; }
	}
}
