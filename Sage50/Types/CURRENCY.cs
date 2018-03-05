/*
 * Created by Ranorex
 * User: wonga01
 * Date: 9/6/2016
 * Time: 10:45
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace Sage50.Types
{
	/// <summary>
	/// Description of CURRENCY.
	/// </summary>
	public class CURRENCY
	{
		public CURRENCY()
		{
		}
		
		 public CURRENCY(Nullable<bool> allowForeignCurrency, CURRENCY_DATA HomeCurrency, List<CURRENCY_DATA> ForeignCurrencies)
        {
            this.allowForeignCurrency = allowForeignCurrency;
            this.HomeCurrency = HomeCurrency;
            this.ForeignCurrencies = ForeignCurrencies;        
        }

        public Nullable<bool> allowForeignCurrency { get; set; }
		public CURRENCY_DATA HomeCurrency = new CURRENCY_DATA();
		public List<CURRENCY_DATA> ForeignCurrencies = new List<CURRENCY_DATA>();
	}
}
