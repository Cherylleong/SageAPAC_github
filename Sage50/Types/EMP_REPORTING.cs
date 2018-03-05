/*
 * Created by Ranorex
 * User: wonga01
 * Date: 9/6/2016
 * Time: 13:36
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Sage50.Types
{
	/// <summary>
	/// Description of EMP_REPORTING.
	/// </summary>
	public class EMP_REPORTING
	{
		public EMP_REPORTING()
		{
		}
		
		public string rppDPSPRegNo { get; set; }
		public string pensionAdjustment { get; set; }
		public string t4EmpCode { get; set; }
		public string historicalEIInsEarnings { get; set; }
		public string historicalPensionableEarnings { get; set; }
		public string historicalQpipEarnings { get; set; }
		public string tippableSales { get; set; }
		public string tipsFromSales { get; set; }
		public string otherTips { get; set; }
		public string tipsAllocated { get; set; }
		public string fedTaxableTips { get; set; }
		public string quebecTaxableTips { get; set; }
		public string eiInsEarnings { get; set; }
		public string pensionEarnings { get; set; }
		public string qpipInsEarnings { get; set; }
	}
}
