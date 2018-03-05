/*
 * Created by Ranorex
 * User: wonga01
 * Date: 8/30/2016
 * Time: 13:51
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Sage50.Types
{
	/// <summary>
	/// Description of ITEM_PRICE.
	/// </summary>
	public class ITEM_PRICE
	{
		public ITEM_PRICE()
		{
		}
		
		 public ITEM_PRICE(string currency)
        {
            this.currency = currency;
        }

		public string currency { get; set; }
		public string priceList { get; set; }
		public string pricingMethod { get; set; }	// by Exchange Rate or Fixed Price
		public string pricePerSellingUnit { get; set; }
	}
}
