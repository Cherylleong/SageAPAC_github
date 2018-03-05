/*
 * Created by Ranorex
 * User: wonga01
 * Date: 9/6/2016
 * Time: 12:43
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Sage50.Types
{
	/// <summary>
	/// Description of PAYROLL_DEDUCTION.
	/// </summary>
	public class PAYROLL_DEDUCTION
	{
		public PAYROLL_DEDUCTION()
		{
		}
		
		public DEDUCT_TYPE DeductBy = new DEDUCT_TYPE();
		public string Deduction { get; set; }
	}
}
