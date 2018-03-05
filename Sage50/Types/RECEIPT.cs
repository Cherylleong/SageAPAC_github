/*
 * Created by Ranorex
 * User: wonga01
 * Date: 10/6/2017
 * Time: 16:46
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace Sage50.Types
{
	/// <summary>
	/// Description of RECEIPT.
	/// </summary>
	public class RECEIPT
	{
		public RECEIPT()
		{
		}
		
		public CUSTOMER Customer = new CUSTOMER();
		public GL_ACCOUNT DepositAccount = new GL_ACCOUNT();
		public List<RECEIPT_ROW> GridRows = new List<RECEIPT_ROW>();
		public string action { get; set; }//for datafiles only
		public string paidBy { get; set; }
		public string transNumber { get; set; }
		public string transDate { get; set; }
		public string chequeNumber { get; set; }
		public string depositRefNum { get; set; }
		public string depositAmount { get; set; }
		public string exchangeRate { get; set; }
		public string comment { get; set; }
		public string padNumber { get; set; }
	}
}
