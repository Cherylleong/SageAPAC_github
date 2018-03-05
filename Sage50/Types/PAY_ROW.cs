/*
 * Created by Ranorex
 * User: wonga01
 * Date: 9/29/2017
 * Time: 14:17
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace Sage50.Types
{
	/// <summary>
	/// Description of PAY_ROW.
	/// </summary>
	public class PAY_ROW
	{
		public PAY_ROW()
		{
		}
		
		public PURCHASE_INVOICE Invoice = new PURCHASE_INVOICE();
		public PAYMENT_PURCH PrePayment = new PAYMENT_PURCH();
		
		public string discountTaken {get; set;}
		public string Amount {get; set;}
		
	}
}
