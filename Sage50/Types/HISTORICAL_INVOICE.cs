/*
 * Created by Ranorex
 * User: wonda05
 * Date: 8/5/2016
 * Time: 10:58 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Sage50.Types
{
	/// <summary>
	/// Description of Historical_invoice.
	/// </summary>
	public class HISTORICAL_INVOICE
	{
		public HISTORICAL_INVOICE ()
		{}

		public string invoiceNumber { get; set; }
		public string transDate { get; set; }
		public string termsPercent { get; set; }
		public string termsDays { get; set; }
		public string termsNetDays { get; set; }
		public string amount { get; set; }
		public string exchangeRate { get; set; }
		public string homeAmount { get; set; }
		public string tax { get; set; }	// from the legacy code. don't see the field though
	}
}
