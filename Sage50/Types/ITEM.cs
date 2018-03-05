/*
 * Created by Ranorex
 * User: wonga01
 * Date: 8/30/2016
 * Time: 13:48
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace Sage50.Types
{
	/// <summary>
	/// Description of ITEM.
	/// </summary>
	public class ITEM
	{
		public ITEM()
		{
		}
		
		public List<ITEM_PRICE> ItemPrices = new List<ITEM_PRICE>();
		public GL_ACCOUNT assetAccount = new GL_ACCOUNT();
		public GL_ACCOUNT revenueAccount = new GL_ACCOUNT();
		public GL_ACCOUNT cogsAccount = new GL_ACCOUNT();
		public GL_ACCOUNT varianceAccount = new GL_ACCOUNT();
		public GL_ACCOUNT expenseAccount = new GL_ACCOUNT();
		public List<ITEM_STATS> ItemStats = new List<ITEM_STATS>();
		public List<ITEM_HISTORY> ItemHistory = new List<ITEM_HISTORY>();
		public List<ITEM_QTY> ItemQuantities = new List<ITEM_QTY>();
		public List<ITEM_BUILD_ITEMS> BuildItems = new List<ITEM_BUILD_ITEMS>();
		public List<TAX_LEDGER> TaxList = new List<TAX_LEDGER>();
		public string action { get; set; }
		public Nullable<bool> InventoryType { get; set; }
		public string invOrServNumber { get; set; }
		public string invOrServDescription { get; set; }
		public Nullable<bool> inactiveCheckBox { get; set; }
		public Nullable<bool> internalServActivity { get; set; }
		public string showQuantitiesIn { get; set; }
		public string invOrServNumberEdit { get; set; }
		public string invOrServDescriptionEdit { get; set; }
		public string invOrServCategory { get; set; }			// new for 2008
		public string stockingUnitOfMeasure { get; set; }
		public Nullable<bool> sellSameAsStockUnitCheckBox { get; set; }
		public string sellUnitOfMeasure { get; set; }
		public string sellRelationship { get; set; }
		public string sellRelationshipComboBox { get; set; }
		public Nullable<bool> buySameAsStockUnitCheckBox { get; set; }
		public string buyUnitOfMeasure { get; set; }
		public string buyRelationship { get; set; }
		public string buyRelationshipComboBox { get; set; }
		public string unitOfMeasure { get; set; }		// for Service types
		public string currencyCode { get; set; }
		public string showUnits { get; set; }
		public string additional1 { get; set; }
		public string additional2 { get; set; }
		public string additional3 { get; set; }
		public string additional4 { get; set; }
		public string additional5 { get; set; }
		public Nullable<bool> addCheckBox1 { get; set; }
		public Nullable<bool> addCheckBox2 { get; set; }
		public Nullable<bool> addCheckBox3 { get; set; }
		public Nullable<bool> addCheckBox4 { get; set; }
		public Nullable<bool> addCheckBox5 { get; set; }
		public Nullable<bool> activityTimeBillCheckBox { get; set; }		// this checkbox is in the header
		//public string unitOfMeasure { get; set; }
		public Nullable<bool> unitIsRelatedToTime { get; set; }
		public string relationshipText { get; set; }
		public string relationshipComboBox { get; set; }
		public string serviceActivityIs { get; set; }
		public Nullable<bool> sometimesChargeForThisActivity { get; set; }
		public string chargesAreBasedOn { get; set; }
		public string flatFee { get; set; }
		public string defaultPayrollIncome { get; set; }
		public string longDescription { get; set; }
		public string picture { get; set; }
		public string thumbnail { get; set; }
		public string build { get; set; }
		public string additionalCosts { get; set; }
		public string recordAdditionalCostsIn { get; set; }
		public string dutyCharged { get; set; }
		public Nullable<bool> serialNumCheckBox { get; set; }
		public List <string> serialNumbers { get; set; }
	}
}
