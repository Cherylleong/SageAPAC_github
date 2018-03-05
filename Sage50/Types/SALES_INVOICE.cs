/*
 * Created by Ranorex
 * User: wonga01
 * Date: 8/29/2016
 * Time: 17:59
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace Sage50.Types
{
	/// <summary>
	/// Description of SALES_INVOICE.
	/// </summary>
	public class SALES_INVOICE : SALES_COMMON_ORDER_INVOICE
	{
		public SALES_INVOICE()
		{
		}
		
		public string quoteOrderTransNumber { get; set; }
        public string earlyPaymentDiscount { get; set; }
        public string earlyPaymentDiscountPercent { get; set; }        
        public string trackingNumber { get; set; }
        public List<TRANS_SERIAL> SerialNumberDetails = new List<TRANS_SERIAL>();	
	}
}
