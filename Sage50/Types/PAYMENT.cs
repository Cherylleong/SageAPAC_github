/*
 * Created by Ranorex
 * User: wonga01
 * Date: 9/29/2017
 * Time: 14:13
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace Sage50.Types
{
	/// <summary>
	/// Description of PAYMENT.
	/// </summary>
	public class PAYMENT
	{
		public PAYMENT()
		{
		}
		
		public string action {get; set;}
		public VENDOR Vendor = new VENDOR();
		public string paidBy {get; set;}
		public GL_ACCOUNT paidFrom = new GL_ACCOUNT();
		public string TransDate {get; set;}
		public string chequeNumber {get; set;}
		public string source {get; set;}
		public string comment {get; set;}
		public string directDepositNo {get; set;}
		
	}
}
