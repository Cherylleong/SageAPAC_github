/*
 * Created by Ranorex
 * User: wonda05
 * Date: 8/5/2016
 * Time: 12:12 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace Sage50.Types
{
	/// <summary>
	/// Description of TAX.
	/// </summary>
	public class TAX
	{
		public TAX ()
		{}

		public GL_ACCOUNT acctTrackPurchases = new GL_ACCOUNT();
		public GL_ACCOUNT acctTrackSales = new GL_ACCOUNT();
		public string taxName { get; set; }
		public string taxID { get; set; }
		public Nullable<bool> exempt { get; set; }
		public Nullable<bool> taxable { get; set; }
		public Nullable<bool>  reportOnTaxes { get; set; }
        public List<String> TaxAuthoritiesToBeCharged = new List<string>();//for { get; set; }
	}
}
