/*
 * Created by Ranorex
 * User: wonga01
 * Date: 9/29/2017
 * Time: 14:09
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace Sage50.Types
{
	/// <summary>
	/// Description of Payment_purch.
	/// </summary>
	public class PAYMENT_PURCH : PAYMENT
	{
		public PAYMENT_PURCH()
		{
		}
		
		public string PrePayRefNumber {get; set;}
		public string PrePayAmount {get; set;}
		public string exchangeRate {get; set;}
		public List<PAY_ROW> GridRows = new List<PAY_ROW>();		
		
	}
}
