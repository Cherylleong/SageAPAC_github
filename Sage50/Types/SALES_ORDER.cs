/*
 * Created by Ranorex
 * User: wonga01
 * Date: 9/1/2016
 * Time: 16:16
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Sage50.Types
{
	/// <summary>
	/// Description of SALES_ORDER.
	/// </summary>
	public class SALES_ORDER : SALES_COMMON_ORDER_INVOICE
	{
		public SALES_ORDER()
		{
		}
		
		public string depositRefNumber { get; set; }
	}
}
