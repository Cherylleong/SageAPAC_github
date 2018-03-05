/*
 * Created by Ranorex
 * User: wonga01
 * Date: 9/6/2016
 * Time: 13:42
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace Sage50.Types
{
	/// <summary>
	/// Description of INVENTORY_SETTINGS.
	/// </summary>
	public class INVENTORY_SETTINGS
	{
		public INVENTORY_SETTINGS()
		{
		}
		
		public INVENTORY_SETTINGS(COSTING_METHOD inventoryCostingMethod, Nullable<bool> allowSerialNums, PROFIT_METHOD profitMethod, SORT_METHOD sortMethod, PRICE_METHOD priceMethod, Nullable<bool> allowInventoryLevelsToGoBelowZero, List<PRICE_LIST> PriceList, Nullable<bool> UseMultipleLocations, List<LOCATION> Locations, Nullable<bool> UseCategories, List<CATEGORY> Categories, ADDITIONAL_FIELD_NAMES AdditionalFields, GL_ACCOUNT itemAssemblyCosts, GL_ACCOUNT adjustmentWriteOff) 
        {
            this.inventoryCostingMethod = inventoryCostingMethod;
            this.allowSerialNums = allowSerialNums;
		    this.profitMethod = profitMethod;
		    this.sortMethod = sortMethod;
		    this.priceMethod  = priceMethod;
            this.allowInventoryLevelsToGoBelowZero  = allowInventoryLevelsToGoBelowZero;
		    this.PriceList = PriceList;
            this.UseMultipleLocations = UseMultipleLocations;
		    this.Locations = Locations;
            this.UseCategories = UseCategories;
		    this.Categories = Categories;
		    this.AdditionalFields = AdditionalFields;
		    this.itemAssemblyCosts = itemAssemblyCosts;
            this.adjustmentWriteOff = adjustmentWriteOff;
        }

		public COSTING_METHOD inventoryCostingMethod = new COSTING_METHOD();
        public Nullable<bool> allowSerialNums { get; set; }
		public PROFIT_METHOD profitMethod = new PROFIT_METHOD();
		public SORT_METHOD sortMethod = new SORT_METHOD();
		public PRICE_METHOD priceMethod = new PRICE_METHOD();
        public Nullable<bool> allowInventoryLevelsToGoBelowZero { get; set; }
		public List<PRICE_LIST> PriceList = new List<PRICE_LIST>();
        public Nullable<bool> UseMultipleLocations { get; set; }
		public List<LOCATION> Locations = new List<LOCATION>();
        public Nullable<bool> UseCategories { get; set; }
		public List<CATEGORY> Categories = new List<CATEGORY>();
		public ADDITIONAL_FIELD_NAMES AdditionalFields = new ADDITIONAL_FIELD_NAMES();
		public GL_ACCOUNT itemAssemblyCosts = new GL_ACCOUNT();
		public GL_ACCOUNT adjustmentWriteOff = new GL_ACCOUNT();
		
	}
}
