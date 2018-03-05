/*
 * Created by Ranorex
 * User: wonga01
 * Date: 8/30/2016
 * Time: 11:45
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace Sage50.Types
{
	/// <summary>
	/// Description of ROW.
	/// </summary>
	public class ROW : ICloneable
	{
		public ROW()
		{
		}
		
		 public virtual object Clone()
        {
            ROW r = new ROW();
            r.amount = this.amount;
            r.quantityShipped = this.quantityShipped;
            r.quantityOrdered = this.quantityOrdered;
            r.basePrice = this.basePrice;
            r.price = this.price;
            r.TaxCode = this.TaxCode;
            r.Item = this.Item;
            r.isInventoryItem = this.isInventoryItem;
            return r;
        }

		public ITEM Item = new ITEM();
		public TAX_CODE TaxCode = new TAX_CODE();
		public GL_ACCOUNT Account = new GL_ACCOUNT();
		public List<PROJECT_ALLOCATION> Projects = new List<PROJECT_ALLOCATION>();
		public Nullable<bool> isInventoryItem { get; set; }
		public string description { get; set; }
		public string quantityShipped { get; set; }
		public string quantityReceived { get; set; }
		public string quantityOrdered { get; set; }
		public string quantityBackordered { get; set; }
		public string unit { get; set; }
		public string basePrice { get; set; }
		public string discount { get; set; }
		public string price { get; set; }
		public string amount { get; set; }
		public string taxAmount { get; set; }	// for Purchase entries only
	}
}
