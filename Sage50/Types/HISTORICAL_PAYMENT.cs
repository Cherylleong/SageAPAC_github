/*
 * Created by Ranorex
 * User: wonda05
 * Date: 8/5/2016
 * Time: 11:00 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace Sage50.Types
{
	/// <summary>
	/// Description of Historical_payment.
	/// </summary>
	public class HISTORICAL_PAYMENT
	{
		public HISTORICAL_PAYMENT ()
		{}

		public List<HISTORICAL_PAYMENT_ROW> PayGridRows = new List<HISTORICAL_PAYMENT_ROW>();
		public string paymentNumber { get; set; }
		public string transDate { get; set; }
		public string exchangeRate { get; set; }
	}
}
