/*
 * Created by Ranorex
 * User: wonga01
 * Date: 9/6/2016
 * Time: 12:57
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Sage50.Types
{
	/// <summary>
	/// Description of PAYROLL_REMITTANCE.
	/// </summary>
	public class PAYROLL_REMITTANCE
	{
		public PAYROLL_REMITTANCE()
		{
		}
		
		public REMITTING_FREQUENCY RemitFrequency = new REMITTING_FREQUENCY();
		public string RemitName { get; set; }
		public string RemitVendor { get; set; }
		public string EndOfNextRemitPeriod { get; set; }
	}
}
