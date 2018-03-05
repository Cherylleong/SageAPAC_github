/*
 * Created by Ranorex
 * User: wonga01
 * Date: 8/30/2016
 * Time: 16:04
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Sage50.Types
{
	/// <summary>
	/// Description of SALES_TABLE.
	/// </summary>
	public class SALES_TABLE
	{
		public SALES_TABLE()
		{
		}
		
		public int iItem { get; set; }			// Item Number column index
		public int iQuantity { get; set; }		// Quantity column index
		public int iOrder { get; set; }			// Order column index
		public int iBackOrder { get; set; }		// Back Order column index
		public int iUnit { get; set; }			// Unit column index
		public int iDescription { get; set; }	// Item Description column index
		public int iBasePrice { get; set; }		// Base Price column index
		public int iDiscount { get; set; }		// Line Item Discount column index
		public int iPrice { get; set; }			// Price	column index
		public int iAmount { get; set; }		// Amount column index
		public int iTax { get; set; }				// Tax Code column index
		public int iAccount { get; set; }		// Account column index
		//int iProjects		// Project Allocation column index. not being used
	}
}
