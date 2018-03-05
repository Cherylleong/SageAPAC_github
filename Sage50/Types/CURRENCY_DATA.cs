/*
 * Created by Ranorex
 * User: wonga01
 * Date: 9/6/2016
 * Time: 10:56
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace Sage50.Types
{
	/// <summary>
	/// Description of CURRENCY_DATA.
	/// </summary>
	public class CURRENCY_DATA
	{
		public CURRENCY_DATA()
		{
		}
		
		public CURRENCY_RATE_REMINDER exchangeRateReminder = new CURRENCY_RATE_REMINDER();
		public List<CURRENCY_EXCHANGE> ExchangeRates = new List<CURRENCY_EXCHANGE>();
		public string Currency { get; set; }
		public string currencyCode { get; set; }
        public string merchantAccount { get; set; }
        public string symbol { get; set; }
        public CURRENCY_SYMBOL symbolPosition = new CURRENCY_SYMBOL();
        public string thousandsSeparator { get; set; }
		public string  decimalSeparator { get; set; }
        public CURRENCY_DECIMAL decimalPlaces = new CURRENCY_DECIMAL();
        public string denomination { get; set; }

		public string roundingDifferencesAccount { get; set; }
		public Nullable<bool> exchangeDisplayReminder { get; set; }
		public string exchangeWebsite { get; set; }
	}
}
