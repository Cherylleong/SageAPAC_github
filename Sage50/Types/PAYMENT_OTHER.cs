/*
 * Created by Ranorex
 * User: wonga01
 * Date: 9/29/2017
 * Time: 14:36
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace Sage50.Types
{
	/// <summary>
	/// Description of PAYMENT_OTHER.
	/// </summary>
	public class PAYMENT_OTHER : PAYMENT
	{
		public PAYMENT_OTHER()
		{
		}
		
		public string exchangeRate {get; set;}
		public string reference {get; set;}
		public string recurringName {get; set;}
		public string recurringFrequency {get; set;}
		public List<PAY_OTHER_ROW> GridRows = new List<PAY_OTHER_ROW>();
		
	}
}
