/*
 * Created by Ranorex
 * User: wonga01
 * Date: 9/6/2016
 * Time: 13:01
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Sage50.Types
{
	/// <summary>
	/// Description of PAYROLL_ADDITIONAL.
	/// </summary>
	public class PAYROLL_ADDITIONAL
	{
		public PAYROLL_ADDITIONAL()
		{
		}
		
		public ADDITIONAL_FIELD_NAMES AdditionalFields = new ADDITIONAL_FIELD_NAMES();
		public ADDITIONAL_FIELD_NAMES ExpenseFields = new ADDITIONAL_FIELD_NAMES();
		public ADDITIONAL_FIELD_NAMES EntitlementFields = new ADDITIONAL_FIELD_NAMES();
		public string provTax { get; set; }
		public string workersComp { get; set; }
	}
}
