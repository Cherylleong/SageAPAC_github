/*
 * Created by Ranorex
 * User: wonga01
 * Date: 9/6/2016
 * Time: 13:13
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Sage50.Types
{
	/// <summary>
	/// Description of PAYROLL_ENTITLEMENTS.
	/// </summary>
	public class PAYROLL_ENTITLEMENTS
	{
		public PAYROLL_ENTITLEMENTS()
		{
		}
		
		public string entitlementName { get; set; }
		public string percentOfHrsWorked { get; set; }
		public string maxDays { get; set; }
		public Nullable<bool> clearDaysAtYearEnd { get; set; }
	}
}
