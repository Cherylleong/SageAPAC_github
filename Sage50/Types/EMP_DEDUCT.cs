/*
 * Created by Ranorex
 * User: wonga01
 * Date: 9/6/2016
 * Time: 13:27
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Sage50.Types
{
	/// <summary>
	/// Description of EMP_DEDUCT.
	/// </summary>
	public class EMP_DEDUCT
	{
		public EMP_DEDUCT()
		{
		}
		
		public Nullable<bool> Use { get; set; }
		public string deductions { get; set; }
		public string amountPerPayPeriod { get; set; }
		public string percentPerPayPeriod { get; set; }
		public string historicalAmount { get; set; }
	}
}
