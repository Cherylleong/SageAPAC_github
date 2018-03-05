/*
 * Created by Ranorex
 * User: wonda05
 * Date: 8/5/2016
 * Time: 12:08 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Sage50.Types
{
	/// <summary>
	/// Description of TAX_DETAIL.
	/// </summary>
	public class TAX_DETAIL
	{
		public TAX_DETAIL ()
		{}

		public TAX Tax = new TAX();
		public TAX_STATUS taxStatus = new TAX_STATUS();
		public string rate { get; set; }
		public Nullable<bool> includedInPrice { get; set; }
		public Nullable<bool> isRefundable { get; set; }
	}
}
