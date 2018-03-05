/*
 * Created by Ranorex
 * User: wonga01
 * Date: 9/29/2017
 * Time: 14:47
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace Sage50.Types
{
	/// <summary>
	/// Description of PAYMENT_REMIT.
	/// </summary>
	public class PAYMENT_REMIT : PAYMENT
	{
		public PAYMENT_REMIT()
		{
		}
		
		
		public List<PAY_REMIT_ROW> GridRows = new List<PAY_REMIT_ROW>();
		public string reference {get; set;}
		public string periodEnding {get; set;}
		public string remitFrequency {get; set;}
		
	}
}
