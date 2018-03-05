/*
 * Created by Ranorex
 * User: wonga01
 * Date: 10/6/2017
 * Time: 16:49
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace Sage50.Types
{
	/// <summary>
	/// Description of RECEIPT_ROW.
	/// </summary>
	public class RECEIPT_ROW
	{
		public RECEIPT_ROW()
		{
		}
		
		public SALES_INVOICE Invoice = new SALES_INVOICE();
		public RECEIPT DepositReceipt = new RECEIPT();
		public string discountTaken { get; set; }
		public string Amount { get; set; }
	}
}
