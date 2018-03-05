/*
 * Created by Ranorex
 * User: wonda05
 * Date: 8/5/2016
 * Time: 11:06 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Sage50.Types
{
	/// <summary>
	/// Description of Historical_payment_row.
	/// </summary>
	public class HISTORICAL_PAYMENT_ROW
	{
		public HISTORICAL_PAYMENT_ROW ()
		{}

		public string invoiceNumber { get; set; }
		public string discountTaken { get; set; }
		public string amountPaid { get; set; }
	}
}
