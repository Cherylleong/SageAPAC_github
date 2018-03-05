/*
 * Created by Ranorex
 * User: wonga01
 * Date: 9/6/2016
 * Time: 10:42
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Sage50.Types
{
	/// <summary>
	/// Description of FEATURES.
	/// </summary>
	public class FEATURES
	{
		public FEATURES()
		{
		}
		
		public FEATURES(Nullable<bool> ordersForVendors, Nullable<bool> quotesForVendors, Nullable<bool> ordersForCustomers, Nullable<bool> quotesForCustomers, Nullable<bool> packingSlips, Nullable<bool> projectCheckBox, Nullable<bool> Language)
        {
            this.ordersForVendors = ordersForVendors;
            this.quotesForVendors = quotesForVendors;
            this.ordersForCustomers = ordersForCustomers;
            this.quotesForCustomers = quotesForCustomers;
            this.packingSlips = packingSlips;
            this.projectCheckBox = projectCheckBox;
            this.Language = Language;
        }

		public Nullable<bool> ordersForVendors { get; set; }
		public Nullable<bool> quotesForVendors { get; set; }
		public Nullable<bool> ordersForCustomers { get; set; }
		public Nullable<bool> quotesForCustomers { get; set; }
		public Nullable<bool> packingSlips { get; set; }
		public Nullable<bool> projectCheckBox { get; set; }
		public Nullable<bool> Language { get; set; }
	}
}
