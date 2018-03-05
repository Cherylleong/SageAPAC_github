/*
 * Created by Ranorex
 * User: wonga01
 * Date: 8/29/2016
 * Time: 17:56
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Sage50.Types
{
	/// <summary>
	/// Description of SALES_COMMON_INVOICE.
	/// </summary>
	public class SALES_COMMON_ORDER_INVOICE : SALES
	{
		public string paidBy { get; set; }
        public GL_ACCOUNT DepositAccount = new GL_ACCOUNT();
        public string chequeNumber { get; set; }
        public string shippedBy { get; set; }
        public string depositApplied { get; set; }	// field exists when convert from prepaid order
        public string padNumber { get; set; }
        public string project { get; set; }	// Pre+
	}
}
