/*
 * Created by Ranorex
 * User: wonga01
 * Date: 9/29/2017
 * Time: 14:45
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace Sage50.Types
{
	/// <summary>
	/// Description of PAYMENT_CREDIT_CARD.
	/// </summary>
	public class PAYMENT_CREDIT_CARD : PAYMENT
	{
		public PAYMENT_CREDIT_CARD()
		{
		}
		
		public string additionalFees {get; set;}
		public string amount {get; set;}
		
	}
}
