/*
 * Created by Ranorex
 * User: wonda05
 * Date: 10/18/2016
 * Time: 10:38 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using Sage50.Repository;
using Sage50.Shared;
using Sage50.Types;
using System.IO;

using Ranorex;

namespace Sage50.Classes
{
	/// <summary>
	/// Description of InventoryServicesLedger.
	/// </summary>
	public static class InventoryServicesLedger
	{

        static Boolean bInvDataFileSettings = false;
        
        public static InventoryServicesLedgerResFolders.InventoryServicesLedgerAppFolder repo = InventoryServicesLedgerRes.Instance.InventoryServicesLedger;

        
        public static void _SA_Invoke(Boolean bOpenLedger)
        {
            // open ledger depending on view type

            if (Simply.isEnhancedView())
            {
                Simply.repo.Self.Activate();
                Simply.repo.InventoryServicesLink.Click();
                Simply.repo.InventoryServicesIcon.Click();
            }
            else
            {

            }

            if (InventoryServicesIcon.repo.SelfInfo.Exists())
            {
                if (bOpenLedger == true)
                {
                    InventoryServicesIcon.repo.CreateNew.Click();
                    InventoryServicesIcon.repo.Self.Close();
                }
            }
        }

        public static void _SA_Invoke()
        {
            InventoryServicesLedger._SA_Invoke(true);
        }

        public static void _SA_MatchDefaults(ITEM InvServRecord)	// Need to get some values in Settings before calling this function. Expand for details
        {
            int numOfLoc = Variables.globalSettings.InventorySettings.Locations.Count;
            ITEM_HISTORY hisByLocX;
            int y;
            // Need to get Company Tax Settings and Inventory Settings (Locations & PriceLists) first

            if (!Functions.GoodData(InvServRecord.InventoryType))
            {
                InvServRecord.InventoryType = true;	// default to inventory type
            }
            if (InvServRecord.InventoryType == false)	// service type
            {
                if (!Functions.GoodData(InvServRecord.activityTimeBillCheckBox))
                {
                    InvServRecord.activityTimeBillCheckBox = false;
                }
            }
            if (!Functions.GoodData(InvServRecord.inactiveCheckBox))
            {
                InvServRecord.inactiveCheckBox = false;
            }
            if (!Functions.GoodData(InvServRecord.stockingUnitOfMeasure))
            {
                InvServRecord.stockingUnitOfMeasure = "Each";
            }
            if (InvServRecord.InventoryType == false)	// set unit of measure for service items
            {
                if (!Functions.GoodData(InvServRecord.unitOfMeasure))
                {
                    InvServRecord.unitOfMeasure = "Each";
                }
            }
            if (!Functions.GoodData(InvServRecord.showUnits))
            {
                InvServRecord.showUnits = InvServRecord.stockingUnitOfMeasure;
            }
            if (!Functions.GoodData(InvServRecord.sellSameAsStockUnitCheckBox))
            {
                InvServRecord.sellSameAsStockUnitCheckBox = true;
            }
            if (!Functions.GoodData(InvServRecord.buySameAsStockUnitCheckBox))
            {
                InvServRecord.buySameAsStockUnitCheckBox = true;
            }

            // initialize item history list based on the number of locations specified in Settings
            if (!Functions.GoodData(InvServRecord.ItemHistory))
            {
                // InvServRecord.ItemHistory[1].location = "All locations"
                // InvServRecord.ItemQuantities[1].forLocation = "All locations"
                if (Variables.globalSettings.InventorySettings.UseMultipleLocations == true)	// add more locations to the lists if there are any defined in Settings
                {
                    int x;	// counter to inerate through the location list in global settings
                    y = 1;	// counter to interate through the item quantity list

                    for (x = 0; x < numOfLoc; x++)
                    {
                        if (Variables.globalSettings.InventorySettings.Locations[x].ActiveStatus == true)
                        {
                            if (y == 1)
                            {
                                InvServRecord.ItemHistory[0].location = "All locations";
                            }
                            else
                            {
                                hisByLocX = new ITEM_HISTORY();
                                hisByLocX.location = Variables.globalSettings.InventorySettings.Locations[x].code;
                                InvServRecord.ItemHistory.Add(hisByLocX);
                            }
                            y++;	// prepare for the next item quantity
                        }
                    }
                }
            }

            // initialize item quantity list based on the number of locations specified in Settings
            y = 1;	// reset counter for item quantity list
            if (!Functions.GoodData(InvServRecord.ItemQuantities))
            {
                if (Variables.globalSettings.InventorySettings.UseMultipleLocations == true)	// add more locations to the lists if there are any defined in Settings
                {
                    for (int x = 0; x < numOfLoc; x++)
                    {
                        if (y == 1)
                        {
                            InvServRecord.ItemQuantities[0].forLocation = "All locations";
                        }
                        else
                        {
                            ITEM_QTY qtyByLocX = new ITEM_QTY();
                            //qtyByLocX.forLocation = hisByLocX.location;
                            InvServRecord.ItemQuantities.Add(qtyByLocX);
                        }
                    }
                }
            }

            InventoryServicesLedger._SA_MatchDefaultsPricesOnly(InvServRecord);

            // initialize the tax list based on the number of taxes specified in Settings
            if (!Functions.GoodData(InvServRecord.TaxList))
            {
                int numOfTaxes = Variables.globalSettings.CompanySettings.TaxSettings.Count;
                for (int x = 0; x < numOfTaxes; x++)
                {
                    // if !GoodData(InvServRecord.TaxList[x].tax)
                    if (x == 1)	// 1st list item has been created when a new item is created
                    {
                        InvServRecord.TaxList[x].tax.taxName = Variables.globalSettings.CompanySettings.TaxSettings[x].taxName;
                        InvServRecord.TaxList[x].taxExempt = "No";
                    }
                    else
                    {
                        TAX_LEDGER iTax = new TAX_LEDGER();
                        iTax.tax.taxName = Variables.globalSettings.CompanySettings.TaxSettings[x].taxName;
                        iTax.taxExempt = "No";
                        InvServRecord.TaxList.Add(iTax);
                    }
                }
            }

        }

        public static void _SA_MatchDefaultsPricesOnly(ITEM InvServRecord)	// Need to get some values in Settings before calling this function. Expand for details
        {
            bool bForeignCurrenciesCalc = false;
            int iNumOfIterationsThroughPrices = 1;
            // initialize item price list based on number of price lists specified in Settings
            if (Variables.globalSettings.InventorySettings.priceMethod == PRICE_METHOD.TAKEN)
            {
                if (Variables.globalSettings.CompanySettings.CurrencySettings.allowForeignCurrency == true)
                {
                    if (Functions.GoodData(Variables.globalSettings.CompanySettings.CurrencySettings.HomeCurrency.Currency))
                    {
                        bForeignCurrenciesCalc = true;	// don't do this till we are sure there is a home currency
                        if (Functions.GoodData(Variables.globalSettings.CompanySettings.CurrencySettings.ForeignCurrencies))
                        {
                            iNumOfIterationsThroughPrices = iNumOfIterationsThroughPrices + Variables.globalSettings.CompanySettings.CurrencySettings.ForeignCurrencies.Count;
                            List<ITEM_PRICE> lIP = new List<ITEM_PRICE>() { };

                            for (int iTimes = 0; iTimes < iNumOfIterationsThroughPrices; iTimes++)
                            {
                                for (int x = 0; x < Variables.globalSettings.InventorySettings.PriceList.Count; x++)
                                {
                                    if (Variables.globalSettings.InventorySettings.PriceList[x].ActiveStatus == true)
                                    {
                                        bool bFound = false;
                                        if (Functions.GoodData(InvServRecord.ItemPrices))
                                        {
                                            string sCurr = null;
                                            if (bForeignCurrenciesCalc)	// there will be a price for each currency
                                            {
                                                if (iTimes == 0)	// use home currency
                                                {
                                                    sCurr = Variables.globalSettings.CompanySettings.CurrencySettings.HomeCurrency.Currency.Trim();
                                                }
                                                else
                                                {
                                                    sCurr = Variables.globalSettings.CompanySettings.CurrencySettings.ForeignCurrencies[iTimes - 1].Currency.Trim();
                                                }
                                            }

                                            int y;
                                            for (y = 0; y < InvServRecord.ItemPrices.Count; y++)
                                            {
                                                if (Variables.globalSettings.InventorySettings.PriceList[x].description == InvServRecord.ItemPrices[y].priceList)
                                                {
                                                    if (bForeignCurrenciesCalc)
                                                    {
                                                        if (InvServRecord.ItemPrices[y].currency == sCurr)
                                                        {
                                                            bFound = true;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        bFound = true;
                                                    }
                                                    if (bFound)
                                                    {
                                                        break;
                                                    }
                                                }
                                            }

                                            ITEM_PRICE IP;
                                            if (bFound)
                                            {
                                                IP = InvServRecord.ItemPrices[y];
                                            }
                                            else
                                            {
                                                IP = new ITEM_PRICE();
                                                IP.priceList = Variables.globalSettings.InventorySettings.PriceList[x].description;
                                                if (bForeignCurrenciesCalc)
                                                {
                                                    IP.currency = sCurr;
                                                }
                                                else
                                                {
                                                    if (Functions.GoodData(Variables.globalSettings.CompanySettings.CurrencySettings.HomeCurrency.Currency))
                                                    {
                                                        IP.currency = Variables.globalSettings.CompanySettings.CurrencySettings.HomeCurrency.Currency;
                                                    }
                                                }
                                            }
                                            if ((IP.currency != Variables.globalSettings.CompanySettings.CurrencySettings.HomeCurrency.Currency) && !Functions.GoodData(IP.pricingMethod))
                                            {
                                                if (Functions.GoodData(IP.pricePerSellingUnit))
                                                {
                                                    IP.pricingMethod = "Fixed Price";
                                                }
                                                else
                                                {
                                                    IP.pricingMethod = "Exchange Rate";
                                                }
                                            }
                                            lIP.Add(IP);
                                        }
                                        else
                                        {
                                            //populate all the names, but no prices from global settings
                                            for (int y = 0; y < Variables.globalSettings.InventorySettings.PriceList.Count; y++)
                                            {
                                                ITEM_PRICE TempIP = new ITEM_PRICE();
                                                TempIP.priceList = Variables.globalSettings.InventorySettings.PriceList[y].description;
                                                if (bForeignCurrenciesCalc)	// there will be a price for each currency
                                                {
                                                    if (iTimes == 1)	// use home currency
                                                    {
                                                        TempIP.currency = Variables.globalSettings.CompanySettings.CurrencySettings.HomeCurrency.Currency;
                                                    }
                                                    else
                                                    {
                                                        TempIP.currency = Variables.globalSettings.CompanySettings.CurrencySettings.ForeignCurrencies[iTimes - 1].Currency;
                                                    }
                                                }
                                                lIP.Add(TempIP);
                                            }
                                            break;	// break out of originating loop
                                        }
                                    }
                                }
                            }
                            InvServRecord.ItemPrices = lIP;
                        }
                    }
                }
            }
        }

        
        public static void _SA_Delete(ITEM InvServRecord)
		{
			
//			Trace.WriteLine ("Deleting item " + InvServRecord.invOrServNumber + "");
//			
//			if (!s_desktop.Exists(INVENTORYSERVICESLEDGER_LOC, 1000))
//			{
//				InventoryServicesLedger.Instance._SA_Invoke ();
//			}
//			string sID = InvServRecord.invOrServNumber + "  " + InvServRecord.invOrServDescription;
//			if (InventoryServicesLedger.Instance.ComboBox.SelectedItem != sID)
//			{
//				InventoryServicesLedger.Instance._SA_Open (InvServRecord);
//			}
//			
//			InventoryServicesLedger.Instance.ClickRemove();
//			
//			// Press yes on message to remove record
//			SimplyMessage.Instance._SA_HandleMessage(SimplyMessage.repo.Yes, SimplyMessage._MSG_AREYOUSUREYOUWANTTODELETE_LOC, true);
//			// message to confirm the item has been deleted
//			SimplyMessage.Instance._SA_HandleMessage(SimplyMessage.OK_LOC);
		}
        
        
        
        
        
        public static void _SA_History(ITEM InvServRecord)
        {
//            if (! s_desktop.Exists(INVENTORYSERVICESLEDGER_LOC))
//            {
//                InventoryServicesLedger.Instance._SA_Invoke();
//            }
//            string sID = InvServRecord.invOrServNumber + "  " + InvServRecord.invOrServDescription;
//            if (InventoryServicesLedger.Instance.ComboBox.SelectedItem != sID)
//            {
//                InventoryServicesLedger.Instance._SA_Open(InvServRecord);
//            }
//
//            InventoryServicesLedger.Instance.Tab.Select("History");
//            if (InvServRecord.InventoryType == true)	// inventory record
//            {
//
//                // Enter all data for all locations                
//                if (FunctionsLib.GoodData(InvServRecord.ItemHistory))
//                {
//                    for (int x = 0; x < InvServRecord.ItemHistory.Count; x++)
//                    {
//                        if (FunctionsLib.GoodData(InvServRecord.ItemHistory[x].location))
//                        {
//                            InventoryServicesLedger.Instance.HistoryLocation.Select(InvServRecord.ItemHistory[x].location);	// Set Location
//                        }
//                        if (FunctionsLib.GoodData(InvServRecord.ItemHistory[x].openingQuantity))
//                        {
//                            InventoryServicesLedger.Instance.OpeningQuantity.SetText(InvServRecord.ItemHistory[x].openingQuantity);	// Set Opening Quantity
//                        }
//                        if (FunctionsLib.GoodData(InvServRecord.ItemHistory[x].openingValue))
//                        {
//                            InventoryServicesLedger.Instance.OpeningValue.SetText(InvServRecord.ItemHistory[x].openingValue);	// Set Opening Value
//                        }
//                    }
//                }
//            }
        }
        
        
        

        public static void _SA_Open(ITEM InvServRecord)
        {
            if (!InventoryServicesLedger.repo.SelfInfo.Exists())
            {
                InventoryServicesLedger._SA_Invoke();
            }
            if (Functions.GoodData(InvServRecord.invOrServDescription))
            {
                InventoryServicesLedger.repo.SelectRecord.Select(InvServRecord.invOrServNumber + "  " + InvServRecord.invOrServDescription);
            }
            else
            {
                InventoryServicesLedger.repo.SelectRecord.Text = InvServRecord.invOrServNumber;
                InventoryServicesLedger.repo.SelectRecord.PressKeys("{Tab}");
            }
        }


        public static void _SA_Create(ITEM InvServRecord)
        {
            _SA_Create(InvServRecord, true, false);
        }

        public static void _SA_Create(ITEM InvServRecord, bool bSave)
        {
            _SA_Create(InvServRecord, bSave, false);
        }

        public static void _SA_Create(ITEM InvServRecord, bool bSave, bool bEdit)
        {

            // Get the global settings that are required in match default functions
            if (!(bInvDataFileSettings))
            {
                Settings._SA_GetCompanyCurrencySettings(false);
                Settings._SA_GetInventoryOptionSettings(false);
                Settings._SA_GetInventoryPriceListSettings(true);
                if (!(Variables.bUseDataFiles))
                {
                    Settings._SA_GetCompanyTaxSettings(false);
                    Settings._SA_GetInventoryLocationsSettings(true);
                }
                bInvDataFileSettings = true;
            }

            if (Variables.bUseDataFiles)
            {
                //because of the complexity of item prices, we need to set based on global settings
                InventoryServicesLedger._SA_MatchDefaultsPricesOnly(InvServRecord);
            }
            else if (!bEdit)
            {
                InventoryServicesLedger._SA_MatchDefaults(InvServRecord);
            }

            if (!InventoryServicesLedger.repo.SelfInfo.Exists())
            {
                InventoryServicesLedger._SA_Invoke();
            }

            if (bEdit)
            {
                string tempName;
                if (Functions.GoodData(InvServRecord.invOrServDescription))
                {
                    tempName = InvServRecord.invOrServNumber + "  " + InvServRecord.invOrServDescription;
                }
                else
                {
                    tempName = InvServRecord.invOrServNumber;
                }
                if (InventoryServicesLedger.repo.SelectRecord.Text != tempName)
                {
                    InventoryServicesLedger._SA_Open(InvServRecord);
                }
                if (Functions.GoodData(InvServRecord.invOrServNumberEdit))
                {
                    InventoryServicesLedger.repo.ItemNumber.TextValue = InvServRecord.invOrServNumberEdit;
                    InvServRecord.invOrServNumber = InvServRecord.invOrServNumberEdit;
                }
                if (Functions.GoodData(InvServRecord.invOrServDescriptionEdit))
                {
                    InventoryServicesLedger.repo.ItemDescription.TextValue = InvServRecord.invOrServDescriptionEdit;
                    InvServRecord.invOrServDescription = InvServRecord.invOrServDescriptionEdit;
                }

                Ranorex.Report.Info(String.Format("Modifying Item {0}", InvServRecord.invOrServNumber));

            }
            else
            {
                InventoryServicesLedger.repo.CreateANewToolButton.Click();
                InventoryServicesLedger.repo.ItemNumber.TextValue = InvServRecord.invOrServNumber;
                
                
                if (Functions.GoodData(InvServRecord.invOrServDescription))
                {
                    InventoryServicesLedger.repo.ItemDescription.TextValue = InvServRecord.invOrServDescription;
                    
                }
            
                Ranorex.Report.Info(String.Format("Creating Item {0}", InvServRecord.invOrServNumber));
            }



            if (InventoryServicesLedger.repo.ItemCategoryInfo.Exists())	// Set Category if it exists
            {
                if (Functions.GoodData(InvServRecord.invOrServCategory) && (InvServRecord.invOrServCategory != ""))
                {
                    InventoryServicesLedger.repo.ItemCategory.Select(InvServRecord.invOrServCategory);
                }
            }
            if (InventoryServicesLedger.repo.InventoryType.Checked)	// Set Type radiolist, Only set it if the on-screen value is different from the value in the record
            {
                if (Functions.GoodData(InvServRecord.InventoryType))
                {
                    if (InvServRecord.InventoryType == false)
                    {
                    	InventoryServicesLedger.repo.ServiceType.Click();
                    }
                }
            }
            else
            {
                if (InvServRecord.InventoryType == true)
                {
                	InventoryServicesLedger.repo.InventoryType.Click();
                }
            }
            if (InvServRecord.InventoryType == false)	// Set Activity (Time & Billing) checkbox if the Type is Service (the data for this checkbox is in the DT10 file)
            {
                if (InventoryServicesLedger.repo.ActivityInfo.Exists())
                {
                    InventoryServicesLedger.repo.Activity.SetState(InvServRecord.activityTimeBillCheckBox);
                }
            }
            InventoryServicesLedger.repo.InactiveItem.SetState(InvServRecord.inactiveCheckBox);	// Set Inactive checkbox
            if (InvServRecord.InventoryType == true)	// First do the fields that affect the fields on other tabs, such as stocking unit
            {
                // Set the Stocking Unit of Measure field from the units tab (do this here because its value affects others fields, such as the Show Quantities In field that will be set immediately after this)
                InventoryServicesLedger.repo.Units.Tab.Click();
                InventoryServicesLedger.repo.Units.StockingUnitOfMeasure.TextValue = InvServRecord.stockingUnitOfMeasure;
            }
            else	// service
            {
                if (InventoryServicesLedger.repo.ActivityInfo.Exists())
                {
                    if (InvServRecord.activityTimeBillCheckBox == true && Functions.GoodData(InvServRecord.internalServActivity))
                    {
                        InventoryServicesLedger.repo.InternalServiceActivity.SetState(InvServRecord.internalServActivity);
                    }
                }
            }


            // Quantities tab
            //int x;
            if (InvServRecord.InventoryType == true)
            {
                if (Functions.GoodData(InvServRecord.ItemQuantities))
                {
                    InventoryServicesLedger.repo.Quantities.Tab.Click();

                    // Enter all data for all locations
                    for (int x = 0; x < InvServRecord.ItemQuantities.Count; x++)
                    {
                        // Set For Location
                        if (InventoryServicesLedger.repo.Quantities.ForLocationInfo.Exists() && Functions.GoodData(InvServRecord.ItemQuantities[x].forLocation))
                        {
                            InventoryServicesLedger.repo.Quantities.ForLocation.Select(InvServRecord.ItemQuantities[x].forLocation);
                        }

                        // Set Minimum Level if the field is editable
                        if ((InventoryServicesLedger.repo.Quantities.MinimumLevel.Enabled && Functions.GoodData(InvServRecord.ItemQuantities[x].minLevel)))
                        {
                            InventoryServicesLedger.repo.Quantities.MinimumLevel.TextValue = InvServRecord.ItemQuantities[x].minLevel;
                            InventoryServicesLedger.repo.Quantities.MinimumLevel.PressKeys("{Tab}");
                        }
                    }
                }
            }


            // Units tab
            InventoryServicesLedger.repo.Units.Tab.Click();
            if (InvServRecord.InventoryType == true)	// The type is Inventory
            {
                // The Stocking Unit of Measure field was already set in the processHeader() function
                if (InventoryServicesLedger.repo.Units.SellSameAsStockUnit.Enabled)	// Set the Same as Stocking Unit checkbox from the Selling Units section if it's editable
                {
                    InventoryServicesLedger.repo.Units.SellSameAsStockUnit.SetState(InvServRecord.sellSameAsStockUnitCheckBox);
                }
                if (Functions.GoodData(InvServRecord.sellUnitOfMeasure) && (InventoryServicesLedger.repo.Units.SellUnitOfMeasure.Enabled))	// Set the Unit of Measure if it's editable
                {
                    InventoryServicesLedger.repo.Units.SellUnitOfMeasure.TextValue = InvServRecord.sellUnitOfMeasure;
                    InventoryServicesLedger.repo.Units.SellUnitOfMeasure.PressKeys("{Tab}");
                }
                if (Functions.GoodData(InvServRecord.sellRelationship) && (InventoryServicesLedger.repo.Units.SellRelationshipEditBox.Enabled))	// Set the Relationship editbox if it's editable
                {
                    InventoryServicesLedger.repo.Units.SellRelationshipEditBox.TextValue = InvServRecord.sellRelationship;
                }
                if (Functions.GoodData(InvServRecord.sellRelationshipComboBox) && (InventoryServicesLedger.repo.Units.SellRelationshipComboBox.Enabled))	// Set the Relationship combobox if it's editable
                {
                    InventoryServicesLedger.repo.Units.SellRelationshipComboBox.Select(InvServRecord.sellRelationshipComboBox);
                }
                if (Functions.GoodData(InvServRecord.buySameAsStockUnitCheckBox) && (InventoryServicesLedger.repo.Units.BuySameAsStockUnit.Enabled))	// Set the Same as Stocking Unit checkbox from the Buying Units section if it's editable
                {
                    InventoryServicesLedger.repo.Units.BuySameAsStockUnit.SetState(InvServRecord.buySameAsStockUnitCheckBox);
                }
                if (Functions.GoodData(InvServRecord.buyUnitOfMeasure) && (InventoryServicesLedger.repo.Units.BuyUnitOfMeasure.Enabled))
                {
                    // Set the Unit of Measure if it's editable
                    InventoryServicesLedger.repo.Units.BuyUnitOfMeasure.TextValue = InvServRecord.buyUnitOfMeasure;
                    InventoryServicesLedger.repo.Units.BuyUnitOfMeasure.PressKeys("{Tab}");
                }
                if (Functions.GoodData(InvServRecord.buyRelationship) && (InventoryServicesLedger.repo.Units.BuyRelationshipEditBox.Enabled))
                {
                    // Set the Relationship editbox if it's editable
                    InventoryServicesLedger.repo.Units.BuyRelationshipEditBox.TextValue = InvServRecord.buyRelationship;
                }
                if (Functions.GoodData(InvServRecord.buyRelationshipComboBox) && (InventoryServicesLedger.repo.Units.BuyRelationshipComboBox.Enabled))
                {
                    // Set the Relationship combobox if it's editable
                    InventoryServicesLedger.repo.Units.BuyRelationshipComboBox.Select(InvServRecord.buyRelationshipComboBox);
                }
            }
            else	// Is a Service
            {
                if (InventoryServicesLedger.repo.ActivityInfo.Exists())	// Is Premium
                {
                    if (InvServRecord.activityTimeBillCheckBox == false)
                    {
                        // Only the Unit of Measure field is editable
                        InventoryServicesLedger.repo.Units.ServiceItemUnitOfMeasure.TextValue = InvServRecord.unitOfMeasure;
                    }
                }
                else	// Is a Service but is PRO
                {
                    InventoryServicesLedger.repo.Units.ServiceItemUnitOfMeasure.TextValue = InvServRecord.unitOfMeasure;
                }
            }
      

            // Pricing tab
            if (Functions.GoodData(InvServRecord.ItemPrices))
            {
                //const int PRICE_LIST_COLUMN = 1;
                //const int PRICING_2ND_COLUMN = 2;
                //const int PRICING_3RD_COLUMN = 3;	// This column doesn't exist when Canadian currency is chosen.
                //string homeCurrency, pricingLine, pricingMethod;

                InventoryServicesLedger.repo.Pricing.Tab.Click();

                if (InventoryServicesLedger.repo.Pricing.Currency.Visible)
                {
                    //InventoryServicesLedger.repo.Pricing.Currency.Select(Variables.GlobalSettings.CompanySettings.CurrencySettings.HomeCurrency.Currency);
                    InventoryServicesLedger.repo.Pricing.Currency.Select("Canadian Dollars");
                }

                //int iLine;
                //this.RestoreWindow.Select();


                bool bFound;            // used to see if tax data is found in container
                List<List <string>> lsContents = InventoryServicesLedger.repo.Pricing.PriceContainer.GetContents();
                List<ITEM_PRICE> lTempItemPrice = new List<ITEM_PRICE>();
                
                List<ITEM_PRICE> lOrigItemPrice = new List<ITEM_PRICE>();
                lOrigItemPrice = InvServRecord.ItemPrices;

                // loop through entire container
				
                foreach(List<string> currentRow in lsContents)
                {
                    bFound = false;
        
               		foreach(ITEM_PRICE currentItemPrice in InvServRecord.ItemPrices)
                    {
                        // see if price name in container is specified in record
                        // make sure the currency matches
                        if (currentItemPrice.currency == Variables.globalSettings.CompanySettings.CurrencySettings.HomeCurrency.Currency && currentItemPrice.priceList == currentRow[0])
                        {
                            bFound = true;
                            ITEM_PRICE priceLine = new ITEM_PRICE();
                            priceLine.priceList = currentItemPrice.priceList;
                    		priceLine.pricePerSellingUnit = currentItemPrice.pricePerSellingUnit;
                    		lTempItemPrice.Add(priceLine);
                            break;
                        }
                    }
   
					if(!bFound)
                    {
                        ITEM_PRICE priceLine = new ITEM_PRICE();
                        priceLine.priceList = currentRow[0];
                        priceLine.pricePerSellingUnit = "0.00";
                        lTempItemPrice.Add(priceLine);
                    }
                }
                InvServRecord.ItemPrices = lTempItemPrice;   // replace price list


                // Enter into container here
                for (int x = 0; x < InvServRecord.ItemPrices.Count; x++)
                {
                    // go to correct line
                    InventoryServicesLedger.repo.Pricing.PriceContainer.SetToLine(x);
                    InventoryServicesLedger.repo.Pricing.PriceContainer.MoveRight();
                    
                    // only set tax exempt if not null
                    if (Functions.GoodData(InvServRecord.ItemPrices[x].pricePerSellingUnit))
                    {
                         InventoryServicesLedger.repo.Pricing.PriceContainer.SetText(InvServRecord.ItemPrices[x].pricePerSellingUnit);
                    }
                }


                // only able to enter foreign prices if price method is Taken
                if (Variables.globalSettings.InventorySettings.priceMethod == PRICE_METHOD.TAKEN)
                {

                    for (int x = 0; x < Variables.globalSettings.CompanySettings.CurrencySettings.ForeignCurrencies.Count; x++)
                    {

                        InvServRecord.ItemPrices = lOrigItemPrice;

                        // select the currency in the combobox
                        InventoryServicesLedger.repo.Pricing.Currency.Select(Variables.globalSettings.CompanySettings.CurrencySettings.ForeignCurrencies[x].Currency);
                        List<List<string>> lsContents2 = InventoryServicesLedger.repo.Pricing.PriceContainer.GetContents();
                        lTempItemPrice = new List<ITEM_PRICE>();


                        // loop through entire container
                        
                     
                        foreach(List<string> currentRow in lsContents2)
                        {
                            bFound = false;
             
                            
                            foreach(ITEM_PRICE currentItemPrice in InvServRecord.ItemPrices)
                            {
                                // see if tax name in container is specified in record
                                // make sure the currency matches
                                if (currentItemPrice.currency == Variables.globalSettings.CompanySettings.CurrencySettings.ForeignCurrencies[x].Currency && currentItemPrice.priceList == currentItemPrice.priceList)                                {
                                    bFound = true;
                                    break;
                                }
                            }
                            // if record is specified then we add the record line to the temp container
                            // else we capture the container line and add to temp container
                            if (bFound)
                            {
                            	ITEM_PRICE priceLine = new ITEM_PRICE();
                            	priceLine.priceList = currentRow[0];
                            	priceLine.pricePerSellingUnit = currentRow[1];
                            	
                            	
                            	
                                lTempItemPrice.Add(priceLine);
                            }
                            else
                            {
                                ITEM_PRICE priceLine = new ITEM_PRICE();
                                priceLine.priceList = currentRow[0];
                                priceLine.pricePerSellingUnit = "0.00";
                                lTempItemPrice.Add(priceLine);
                            }
                        }
                        InvServRecord.ItemPrices = lTempItemPrice;   // replace tax list
// uncomment all later - no recovery in place for default values
//                        // Enter into container here
//                        for (int aa = 0; aa < InvServRecord.ItemPrices.Count; aa++)
//                        {
//                            // go to correct line
//                            InventoryServicesLedger.repo.Pricing.PriceContainer.SetToLine(aa);
//                            InventoryServicesLedger.repo.Pricing.PriceContainer.MoveRight();
//                            // only set tax exempt if not null
//
//                            if (InvServRecord.ItemPrices[aa].pricingMethod.ToUpper() != lsContents2[aa][1].ToUpper())
//                            {
//                            	InventoryServicesLedger.repo.Pricing.PriceContainer.Toggle();
//                            }
//
//                            InventoryServicesLedger.repo.Pricing.PriceContainer.MoveRight();
//
//                            if (Functions.GoodData(InvServRecord.ItemPrices[aa].pricePerSellingUnit))
//                            {
//                                InventoryServicesLedger.repo.Pricing.PriceContainer.SetText(InvServRecord.ItemPrices[aa].pricePerSellingUnit);
//                            }
//                        }
                    }
                }
            }

            // Linked tab
            InventoryServicesLedger.repo.Linked.Tab.Click();
            
            PopupWatcher invMessages = new PopupWatcher();
            invMessages.WatchAndClick(SimplyMessage.repo.SelfInfo,SimplyMessage.repo.YesInfo);
            invMessages.Start();

            if (InvServRecord.InventoryType == true)
            {
            	            	
                // Set Asset
                // if GoodData (InvServRecord.assetAccount) && GoodData (InvServRecord.assetAccount.acctNumber)
                if (Functions.GoodData(InvServRecord.assetAccount.acctNumber))
                {
                	InventoryServicesLedger.repo.Linked.InvAssetAcct.Focus();
                    InventoryServicesLedger.repo.Linked.InvAssetAcct.Text = InvServRecord.assetAccount.acctNumber;
                }
                else	// just pick a random account from the list as this field is mandatory
                {
                    InventoryServicesLedger.repo.Linked.InvAssetAcct.Focus();
                    InventoryServicesLedger.repo.Linked.InvAssetAcct.RandPick();
                    InvServRecord.assetAccount.acctNumber = InventoryServicesLedger.repo.Linked.InvAssetAcct.Text;
                }

                // Tab out to trigger the message
                InventoryServicesLedger.repo.Linked.InvAssetAcct.PressKeys("{Tab}");

//				if(SimplyMessage.repo.SelfInfo.Exists())
//				{
//                	// Press yes on message about changing accout type
//                	SimplyMessage._SA_HandleMessage(SimplyMessage.repo.Yes, SimplyMessage.repo._Msg_LinkAssetMustBeAssignedTheAccountClassInventory);
//				}


                // Set Revenue
                // if GoodData (InvServRecord.revenueAccount ) && GoodData(InvServRecord.revenueAccount.acctNumber)
                if (Functions.GoodData(InvServRecord.revenueAccount.acctNumber))
                {
                    InventoryServicesLedger.repo.Linked.InvRevenueAcct.Text = InvServRecord.revenueAccount.acctNumber;
                    // Tab out to trigger the message
                    InventoryServicesLedger.repo.Linked.InvRevenueAcct.PressKeys("{Tab}");
                    // Press yes on message about changing accout type
                    
//                    if(SimplyMessage.repo.SelfInfo.Exists())
//                    {
//                    	SimplyMessage._SA_HandleMessage(SimplyMessage.repo.Yes, SimplyMessage.repo._MSG_THEREVENUEACCOUNTYOUSELECTEDISNOTASSIGNEDTO_LOC);
//                    }
                }

                // Set C.O.G.S.
                // if GoodData (InvServRecord.cogsAccount ) && GoodData(InvServRecord.revenueAccount.acctNumber)
                if (Functions.GoodData(InvServRecord.cogsAccount.acctNumber))
                {
                    InventoryServicesLedger.repo.Linked.InvCogsAcct.Text = InvServRecord.cogsAccount.acctNumber;
                }
                else	// just pick a random account from the list as this field is mandatory
                {
                    InventoryServicesLedger.repo.Linked.InvCogsAcct.RandPick();
                    InvServRecord.cogsAccount.acctNumber = InventoryServicesLedger.repo.Linked.InvCogsAcct.Text;
                }
                // Tab out to trigger the message
                InventoryServicesLedger.repo.Linked.InvCogsAcct.PressKeys("{Tab}");
				
//                 if(SimplyMessage.repo.SelfInfo.Exists())
//                 {
//                	// Press yes on message about changing accout type
//                 	SimplyMessage._SA_HandleMessage(SimplyMessage.repo.Yes, SimplyMessage.repo._MSG_THECOGSACCOUNTISNOTASSIGNEDCOGSDOYOUWANT_LOC);
//                 }

                // Set Variance
                // if GoodData (InvServRecord.varianceAccount ) && GoodData(InvServRecord.varianceAccount.acctNumber)
                if (Functions.GoodData(InvServRecord.varianceAccount.acctNumber))
                {
                    InventoryServicesLedger.repo.Linked.InvVarianceAct.Text = InvServRecord.varianceAccount.acctNumber;
                    // Tab out to trigger the message
                    InventoryServicesLedger.repo.Linked.InvAssetAcct.PressKeys("{Tab}");
                    // Press yes on message about changing accout type
                    
//                    if(SimplyMessage.repo.SelfInfo.Exists())
//                    {
//                    	SimplyMessage._SA_HandleMessage(SimplyMessage.repo.Yes, SimplyMessage.repo._MSG_THEVARIANCEACCOUNTYOUSELECTEDISNOTASSIGNEDTO_LOC);
//                    }
                }
            }
            else	// the type is Services
            {
                // Set Revenue
                // if GoodData (InvServRecord.revenueAccount ) && GoodData(InvServRecord.revenueAccount.acctNumber)
                if (Functions.GoodData(InvServRecord.revenueAccount.acctNumber))
                {
                    InventoryServicesLedger.repo.Linked.ServRevenueAct.Focus();
                    InventoryServicesLedger.repo.Linked.ServRevenueAct.Text = InvServRecord.revenueAccount.acctNumber;
					
//                    if(SimplyMessage.repo.SelfInfo.Exists())
//                    {
//                    	// Press yes on message about changing accout type
//                    	SimplyMessage._SA_HandleMessage(SimplyMessage.repo.Yes, SimplyMessage.repo._MSG_THEREVENUEACCOUNTYOUSELECTEDISNOTASSIGNEDTO_LOC);
//                    }
                }
                else
                {
                    InventoryServicesLedger.repo.Linked.InvRevenueAcct.Focus();
                    InventoryServicesLedger.repo.Linked.InvRevenueAcct.RandPick();
                    InvServRecord.revenueAccount.acctNumber = InventoryServicesLedger.repo.Linked.InvRevenueAcct.Text;
                }
                // Tab out to trigger the message
                InventoryServicesLedger.repo.Linked.InvAssetAcct.PressKeys("{Tab}");
                
                
//                if(SimplyMessage.repo.SelfInfo.Exists())
//                {
//                	// Press yes on message about changing accout type
//                	SimplyMessage._SA_HandleMessage(SimplyMessage.repo.Yes, SimplyMessage.repo._MSG_THEREVENUEACCOUNTYOUSELECTEDISNOTASSIGNEDTO_LOC);
//                }

                // Set Expense
                // if GoodData (InvServRecord.expenseAccount ) && GoodData(InvServRecord.expenseAccount.acctNumber)
                if (Functions.GoodData(InvServRecord.expenseAccount.acctNumber))
                {
                    InventoryServicesLedger.repo.Linked.ServExpenseAct.Text = InvServRecord.expenseAccount.acctNumber;
                    // Tab out to trigger the message
                    InventoryServicesLedger.repo.Linked.InvAssetAcct.PressKeys("{Tab}");
                    
//                    if(SimplyMessage.repo.SelfInfo.Exists())
//                    {
//                    	// Press yes on message about changing accout type
//                    	SimplyMessage._SA_HandleMessage(SimplyMessage.repo.Yes, SimplyMessage.repo._MSG_THEEXPENSEACCOUNTYOUSELECTEDISNOTASSIGNEDTO_LOC);
//                    }
                }
            }
            
            invMessages.Stop();

            // Build tab
            if (InvServRecord.InventoryType == true)	// inventory only
            {
                InventoryServicesLedger.repo.Build.Tab.Click();

                if (Functions.GoodData(InvServRecord.build))
                {
                    // Set the Build field
                    InventoryServicesLedger.repo.Build.Build.TextValue = InvServRecord.build;
                    InventoryServicesLedger.repo.Build.Build.PressKeys("{Tab}");
                }

                const int ITEM_COLUMN = 0;
                const int QUANTITY_COLUMN = 1;

                // Restore containter to default
                InventoryServicesLedger.repo.Self.Activate();
                //InventoryServicesLedger.repo.RestoreWindow.Select();

                // Set the first cell X position for SilkTest to click (try not to click over text in the cell, so as far right in the cell as possible)
                InventoryServicesLedger.repo.Build.BuildContainer.ClickFirstCell();//SetFirstCellClickXPosition(63);

                if (InvServRecord.BuildItems.Count != 0)	// enter build items if any
                {
                    for (int lineIndex = 0; lineIndex < InvServRecord.BuildItems.Count; lineIndex++)
                    {

                        // Do Item field
                        InventoryServicesLedger.repo.Build.BuildContainer.MoveToField(lineIndex, ITEM_COLUMN);
                        if (Functions.GoodData(InvServRecord.BuildItems[lineIndex].item.invOrServNumber))
                        {
                            InventoryServicesLedger.repo.Build.BuildContainer.SetText(InvServRecord.BuildItems[lineIndex].item.invOrServNumber);
                        }

                        // Do Quantity field
                        InventoryServicesLedger.repo.Build.BuildContainer.PressKeys("{Tab}");
                        //InventoryServicesLedger.repo.BuildContainer.MoveToField(lineIndex, QUANTITY_COLUMN);
                        if (Functions.GoodData(InvServRecord.BuildItems[lineIndex].quantity))
                        {
                            InventoryServicesLedger.repo.Build.BuildContainer.PressKeys(InvServRecord.BuildItems[lineIndex].quantity);
                        }
                        InventoryServicesLedger.repo.Build.BuildContainer.PressKeys("{Tab}");
                    }
                }

                if (Functions.GoodData(InvServRecord.build) && (InvServRecord.build != "0"))
                {

                    // Set the Additional Costs field
                    if (Functions.GoodData(InvServRecord.additionalCosts))
                    {
                        InventoryServicesLedger.repo.Build.AdditionalCosts.TextValue = InvServRecord.additionalCosts;
                    }

                    // Set the "Record Additional Costs In" combobox
                    if (Functions.GoodData(InvServRecord.recordAdditionalCostsIn))
                    {
                        InventoryServicesLedger.repo.Build.RecordAdditionalCostsIn.Select(InvServRecord.recordAdditionalCostsIn);
                        InventoryServicesLedger.repo.Build.RecordAdditionalCostsIn.PressKeys("{Tab}");
                    }
                }
            }
            else	// service only
            {
                // time and billing tab only shows for service items
                if (InvServRecord.InventoryType == false)
                {
                    InventoryServicesLedger.repo.TimeBilling.Tab.Click();
                    if (InvServRecord.activityTimeBillCheckBox == true)	// fields are only editable when this checkbox is checked
                    {
                        if (Functions.GoodData(InvServRecord.unitOfMeasure))	// Set Unit of Measure combobox
                        {
                            if (InvServRecord.unitOfMeasure != "Minute")
                            {
                                // We type into combobox because this is how we add different units of measure
                                InventoryServicesLedger.repo.TimeBilling.UnitOfMeasure.Focus();
                                InventoryServicesLedger.repo.TimeBilling.UnitOfMeasure.Select(InvServRecord.unitOfMeasure);
                                InventoryServicesLedger.repo.TimeBilling.UnitOfMeasure.PressKeys("{Tab}");
                            }
                            else	// For some reason, when typing in "Minute", it is truncated. Therefore have to select
                            {
                                InventoryServicesLedger.repo.TimeBilling.UnitOfMeasure.Select(InvServRecord.unitOfMeasure);
                            }
                        }
                        if (Functions.GoodData(InvServRecord.unitIsRelatedToTime) && (InventoryServicesLedger.repo.TimeBilling.UnitIsRelatedToTime.Enabled))	// Set Unit Is Related To Time checkbox if it's enabled
                        {
                            InventoryServicesLedger.repo.TimeBilling.UnitIsRelatedToTime.SetState(InvServRecord.unitIsRelatedToTime);
                        }
                        if (Functions.GoodData(InvServRecord.relationshipText) && (InventoryServicesLedger.repo.TimeBilling.RelationshipEditBox.Enabled))	// Set Relationship editbox if it's editable
                        {
                            InventoryServicesLedger.repo.TimeBilling.RelationshipEditBox.TextValue = InvServRecord.relationshipText;
                        }
                        if (Functions.GoodData(InvServRecord.relationshipComboBox) && (InventoryServicesLedger.repo.TimeBilling.RelationshipComboBox.Enabled))	// Set Relationship combobox if it's editable
                        {
                            InventoryServicesLedger.repo.TimeBilling.RelationshipComboBox.Select(InvServRecord.relationshipComboBox);
                        }
                        if (Functions.GoodData(InvServRecord.serviceActivityIs) && (InventoryServicesLedger.repo.TimeBilling.ServiceActivityIs.Enabled))	// Set "The Service Activity Is" combobox if it's editable
                        {
                            InventoryServicesLedger.repo.TimeBilling.ServiceActivityIs.Select(InvServRecord.serviceActivityIs);
                        }
                        if (Functions.GoodData(InvServRecord.sometimesChargeForThisActivity) && InventoryServicesLedger.repo.TimeBilling.SomeTimesChargeForThisActivityInfo.Exists())	// Set the "Sometimes Charge for this Activity" checkbox if it exists and if it's enabled
                        {
                            if (InventoryServicesLedger.repo.TimeBilling.SomeTimesChargeForThisActivity.Enabled)
                            {
                                InventoryServicesLedger.repo.TimeBilling.SomeTimesChargeForThisActivity.SetState(InvServRecord.sometimesChargeForThisActivity);
                            }
                        }
                        if (Functions.GoodData(InvServRecord.chargesAreBasedOn) && (InventoryServicesLedger.repo.TimeBilling.ChargesAreBasedOn.Enabled))	// Set the "Charges Are Based On" combobox if it's enabled
                        {
                            if (InvServRecord.chargesAreBasedOn != "")	// set the value
                            {
                                InventoryServicesLedger.repo.TimeBilling.ChargesAreBasedOn.Select(InvServRecord.chargesAreBasedOn);
                            }
                            else	// get the value (default selection)
                            {
                                InvServRecord.chargesAreBasedOn = InventoryServicesLedger.repo.TimeBilling.ChargesAreBasedOn.Text;
                            }
                        }
                        if (Functions.GoodData(InvServRecord.flatFee) && (InventoryServicesLedger.repo.TimeBilling.FlatFee.Enabled))	// Set the Flat Fee editbox if it's enabled
                        {
                            InventoryServicesLedger.repo.TimeBilling.FlatFee.TextValue = InvServRecord.flatFee;
                        }
                        if (Functions.GoodData(InvServRecord.defaultPayrollIncome) && (InventoryServicesLedger.repo.TimeBilling.DefaultPayrollIncome.Enabled))	// Set Default Payroll Income combobox if it's enabled
                        {
                            InventoryServicesLedger.repo.TimeBilling.DefaultPayrollIncome.Select(InvServRecord.defaultPayrollIncome);
                        }
                    }
                }
            }


            // Statistics tab
            InventoryServicesLedger.repo.Statistics.Tab.Click();
            if (InvServRecord.InventoryType == true)	// inventory only
            {

                if (Functions.GoodData(InvServRecord.showUnits))
                {
                    InventoryServicesLedger.repo.Statistics.ShowUnitsSoldIn.Select(InvServRecord.showUnits);
                }
            }
            if (Functions.GoodData(InvServRecord.ItemStats))
            {
                for (int x = 0; x < InvServRecord.ItemStats.Count; x++)
                {
                    if (InventoryServicesLedger.repo.Statistics.ForLocationStatisticsInfo.Exists())	// Inventory locations feature is present only in SImply Premium and higher versions. Also doesn't exist if the item type is Service.
                    {
                        // Set the For Location
                        if (Functions.GoodData(InvServRecord.ItemStats[x].forLocation))
                        {
                            InventoryServicesLedger.repo.Statistics.ForLocationStatistics.Select(InvServRecord.ItemStats[x].forLocation);
                        }
                    }

                    // If adding a record, fill in the rest of the fields (not editable otherwise)
                    if (!bEdit)
                    {
                        //if(!s_desktop.Exists(String.Format("Window[{0}]",RECEIVABLESLEDGER_LOC),1000))


                        // Fields cannot be edited if the location is "All Locations"
                        if (Functions.GoodData(InvServRecord.ItemStats[x].forLocation) &&
                            (InvServRecord.ItemStats[x].forLocation != "All Locations") ||
                            (!InventoryServicesLedger.repo.Statistics.ForLocationStatisticsInfo.Exists()))
                        {
                            if (Functions.GoodData(InvServRecord.ItemStats[x].ytdNoOfTransactions))
                            {
                                // Set YTD No Of Transactions
                                InventoryServicesLedger.repo.Statistics.YTDNoOfTransactions.TextValue = InvServRecord.ItemStats[x].ytdNoOfTransactions;
                                InventoryServicesLedger.repo.Statistics.YTDNoOfTransactions.PressKeys("{Tab}");
                            }
                            if (Functions.GoodData(InvServRecord.ItemStats[x].ytdUnitsSold))
                            {
                                // Set YTD Units Sold
                                InventoryServicesLedger.repo.Statistics.YTDUnitsSold.TextValue = InvServRecord.ItemStats[x].ytdUnitsSold;
                                InventoryServicesLedger.repo.Statistics.YTDUnitsSold.PressKeys("{Tab}");
                            }
                            if (Functions.GoodData(InvServRecord.ItemStats[x].ytdAmountSold))
                            {
                                // Set YTD Amount Sold
                                InventoryServicesLedger.repo.Statistics.YTDAmountSold.TextValue = InvServRecord.ItemStats[x].ytdAmountSold;
                                InventoryServicesLedger.repo.Statistics.YTDAmountSold.PressKeys("{Tab}");
                            }
                            if (Functions.GoodData(InvServRecord.ItemStats[x].ytdCostOfGoodsSold))
                            {
                                // Set YTD Cost of Goods Sold
                                if (InventoryServicesLedger.repo.Statistics.YTDCostOfGoodsSoldInfo.Exists())
                                {
                                    InventoryServicesLedger.repo.Statistics.YTDCostOfGoodsSold.TextValue = InvServRecord.ItemStats[x].ytdCostOfGoodsSold;
                                    InventoryServicesLedger.repo.Statistics.YTDCostOfGoodsSold.PressKeys("{Tab}");
                                }
                            }
                            if (Functions.GoodData(InvServRecord.ItemStats[x].lastYearNoOfTransactions))
                            {
                                // Set Last Year No Of Transactions
                                InventoryServicesLedger.repo.Statistics.LastYearNoOfTransactions.TextValue = InvServRecord.ItemStats[x].lastYearNoOfTransactions;
                                InventoryServicesLedger.repo.Statistics.LastYearNoOfTransactions.PressKeys("{Tab}");
                            }
                            if (Functions.GoodData(InvServRecord.ItemStats[x].lastYearUnitsSold))
                            {
                                // Set Last Year Units Sold
                                InventoryServicesLedger.repo.Statistics.LastYearUnitsSold.TextValue = InvServRecord.ItemStats[x].lastYearUnitsSold;
                                InventoryServicesLedger.repo.Statistics.LastYearUnitsSold.PressKeys("{Tab}");
                            }
                            if (Functions.GoodData(InvServRecord.ItemStats[x].lastYearAmountSold))
                            {
                                // Set Last Year Amount Sold
                                InventoryServicesLedger.repo.Statistics.LastYearAmountSold.TextValue = InvServRecord.ItemStats[x].lastYearAmountSold;
                                InventoryServicesLedger.repo.Statistics.LastYearAmountSold.PressKeys("{Tab}");
                            }
                            if (Functions.GoodData(InvServRecord.ItemStats[x].lastYearCostOfGoodsSold))
                            {
                                // Set Last Year Cost Of Goods Sold
                                if (InventoryServicesLedger.repo.Statistics.LastYearCostOfGoodsSoldInfo.Exists())
                                {
                                    InventoryServicesLedger.repo.Statistics.LastYearCostOfGoodsSold.TextValue = InvServRecord.ItemStats[x].lastYearCostOfGoodsSold;
                                    InventoryServicesLedger.repo.Statistics.LastYearCostOfGoodsSold.PressKeys("{Tab}");
                                }
                            }
                            if (Functions.GoodData(InvServRecord.ItemStats[x].dateOfLastSale))
                            {
                                // Set Date of Last Sale
                                InventoryServicesLedger.repo.Statistics.DateOfLastSale.TextValue = InvServRecord.ItemStats[x].dateOfLastSale;
                                InventoryServicesLedger.repo.Statistics.DateOfLastSale.PressKeys("{Tab}");
                            }
                        }
                    }
                }
            }


            // Taxes tab
            if (InvServRecord.TaxList.Count != 0)
            {
                InventoryServicesLedger.repo.Taxes.Tab.Click();

                //string dataLine				;
                //string taxLine;	// holds the current tax container line
                //string taxAuth;	// the current tax authority
                //string taxExempt;	// the current tax exempt

                //int  y;
                bool bTaxFound = false;

                // Restore containter to default
                InventoryServicesLedger.repo.Self.Activate();
                //InventoryServicesLedger.repo.RestoreWindow.Select();
                
				
                List<List<string>> lsContents = InventoryServicesLedger.repo.Taxes.TaxContainer.GetContents();
                
                //InventoryServicesLedger.repo.Taxes.TaxContainer.ClickFirstCell();
                //InventoryServicesLedger.repo.Taxes.TaxContainer.PressKeys("{Tab}");

                for (int x = 0; x < InvServRecord.TaxList.Count; x++)
                {
                    bTaxFound = false;
                    for (int y = 0; y < lsContents.Count; y++)
                    {
                        if (InvServRecord.TaxList[x].tax.taxName == lsContents[x][0])
                        {
                            if (InvServRecord.TaxList[x].taxExempt != lsContents[x][1])
                            {
                            	InventoryServicesLedger.repo.Taxes.TaxContainer.Toggle();
                            }
                            bTaxFound = true;
                            break;
                        }
                    }
                    InventoryServicesLedger.repo.Taxes.TaxContainer.MoveDown();
                    if (!bTaxFound)
                    {
                        Functions.Verify(false, true, "Tax authority " + InvServRecord.TaxList[x].tax.taxName + " found");
                    }
                }

                // Set the Duty Charged On This Item if it exists
                if (InventoryServicesLedger.repo.Taxes.DutyChargedOnThisItemInfo.Exists() && Functions.GoodData(InvServRecord.dutyCharged))
                {
                    InventoryServicesLedger.repo.Taxes.DutyChargedOnThisItem.TextValue = InvServRecord.dutyCharged;
                }

            }


            // Additional Info tab
            InventoryServicesLedger.repo.AdditionalInfo.Tab.Click();
            
            if (InventoryServicesLedger.repo.AdditionalInfo.Additional1Info.Exists() && Functions.GoodData(InvServRecord.additional1))
            {
                InventoryServicesLedger.repo.AdditionalInfo.Additional1.TextValue = InvServRecord.additional1;
                InventoryServicesLedger.repo.AdditionalInfo.AddCheckBox1.SetState(InvServRecord.addCheckBox1);
            }

            if (InventoryServicesLedger.repo.AdditionalInfo.Additional2Info.Exists() && Functions.GoodData(InvServRecord.additional2))
            {
                InventoryServicesLedger.repo.AdditionalInfo.Additional2.TextValue = InvServRecord.additional2;
                InventoryServicesLedger.repo.AdditionalInfo.AddCheckBox2.SetState(InvServRecord.addCheckBox2);
            }

            if (InventoryServicesLedger.repo.AdditionalInfo.Additional3Info.Exists() && Functions.GoodData(InvServRecord.additional3))
            {
                InventoryServicesLedger.repo.AdditionalInfo.Additional3.TextValue = InvServRecord.additional3;
                InventoryServicesLedger.repo.AdditionalInfo.AddCheckBox3.SetState(InvServRecord.addCheckBox3);
            }

            if (InventoryServicesLedger.repo.AdditionalInfo.Additional4Info.Exists() && Functions.GoodData(InvServRecord.additional4))
            {
                InventoryServicesLedger.repo.AdditionalInfo.Additional4.TextValue = InvServRecord.additional4;
                InventoryServicesLedger.repo.AdditionalInfo.AddCheckBox4.SetState(InvServRecord.addCheckBox4);
            }

            if (InventoryServicesLedger.repo.AdditionalInfo.Additional5Info.Exists() && Functions.GoodData(InvServRecord.additional5))
            {
                InventoryServicesLedger.repo.AdditionalInfo.Additional5.TextValue = InvServRecord.additional5;
                InventoryServicesLedger.repo.AdditionalInfo.AddCheckBox5.SetState(InvServRecord.addCheckBox5);
            }


 

            if (Functions.GoodData(InvServRecord.longDescription) || Functions.GoodData(InvServRecord.picture) || Functions.GoodData(InvServRecord.thumbnail))
            {
                InventoryServicesLedger.repo.DetailedDesc.Tab.Click();

                if (Functions.GoodData(InvServRecord.longDescription))
                {
                    // Set Long Description
                    InventoryServicesLedger.repo.DetailedDesc.LongDescription.TextValue = InvServRecord.longDescription;
                }
                if (Functions.GoodData(InvServRecord.picture))
                {
                    // Set Picture
                    InventoryServicesLedger.repo.DetailedDesc.Picture.TextValue = InvServRecord.picture;
                }
                if (Functions.GoodData(InvServRecord.thumbnail))
                {
                    // Set Thumbnail
                    InventoryServicesLedger.repo.DetailedDesc.Thumbnail.TextValue = InvServRecord.thumbnail;
                }
            }

            if (Functions.GoodData(InvServRecord.serialNumbers))
            {
                InventoryServicesLedger.repo.SerialNumbers.Tab.Click();

                // Have to click since message pops up before checkbox can be set
                if (InventoryServicesLedger.repo.SerialNumbers.UsesSerialNumbers.Checked != InvServRecord.serialNumCheckBox)
                {
                    InventoryServicesLedger.repo.SerialNumbers.UsesSerialNumbers.Click();
                    SimplyMessage._SA_HandleMessage(SimplyMessage.repo.Yes, SimplyMessage.repo._MSG_PLACEHOLDERTOFIXEXISTINGMETHODCALLS_LOC);
                }

                // Only able to enter numbers for existing items
                if (bEdit)
                {
                    if (InvServRecord.serialNumCheckBox == true)
                    {
                        InventoryServicesLedger.repo.SerialNumbers.EditSerialNumbers.Click();	// Bring up edit serial number
                        for (int x = 0; x < InvServRecord.serialNumbers.Count; x++)
                        {
                            //EditSerialNumber.repo._SA_pEnterSerialNumberDetails(x, InvServRecord.serialNumbers[x]); NC
                        }
                        //EditSerialNumber.repo.OK.Click();	// Save changes
                        InventoryServicesLedger.repo.Self.Activate();	// Set focus back to main ledger window
                    }
                }
            }


            // Save the Record
            if (bSave)
            {
                InventoryServicesLedger.repo.Save.Click();
            }
        }

        public static void _SA_Build(ITEM InvServRecord)
        {
            _SA_Build(InvServRecord, true);
        }

        public static void _SA_Build(ITEM InvServRecord, bool bSave)
        {
            
            if (InventoryServicesLedger.repo.SelfInfo.Exists())
            {
                InventoryServicesLedger._SA_Invoke();
            }
            string sID = InvServRecord.invOrServNumber + "  " + InvServRecord.invOrServDescription;
            if (InventoryServicesLedger.repo.SelectRecord.Text != sID)
            {
                InventoryServicesLedger._SA_Open(InvServRecord);
            }


            InventoryServicesLedger.repo.Build.Tab.Click();

            if (Functions.GoodData(InvServRecord.build))
            {
                // Set the Build field
                InventoryServicesLedger.repo.Build.Build.TextValue = InvServRecord.build;
            }

            const int ITEM_COLUMN = 1;
            const int QUANTITY_COLUMN = 2;

            // Restore containter to default
            InventoryServicesLedger.repo.Self.Activate();
            //InventoryServicesLedger.Instance.RestoreWindow.Select();

            // Set the first cell X position for SilkTest to click (try not to click over text in the cell, so as far right in the cell as possible)
            InventoryServicesLedger.repo.Build.BuildContainer.ClickFirstCell();// SetFirstCellClickXPosition(63);

            for (int x = 0; x < InvServRecord.BuildItems.Count; x++)
            {

                InventoryServicesLedger.repo.Build.BuildContainer.MoveToField(x, ITEM_COLUMN);
                if (Functions.GoodData(InvServRecord.BuildItems[x].item.invOrServNumber))
                {
                    // Do Item field
                    InventoryServicesLedger.repo.Build.BuildContainer.SetText(InvServRecord.BuildItems[x].item.invOrServNumber);//TableField.SetText(InvServRecord.BuildItems[x].item.invOrServNumber);
                }

                InventoryServicesLedger.repo.Build.BuildContainer.MoveToField(x, QUANTITY_COLUMN);
                if (Functions.GoodData(InvServRecord.BuildItems[x].item.invOrServNumber))
                {
                    // Do Quantity field
                    InventoryServicesLedger.repo.Build.BuildContainer.SetText(InvServRecord.BuildItems[x].quantity);//TableField.SetText(InvServRecord.BuildItems[x].quantity);
                }
            }


            if (Functions.GoodData(InvServRecord.build) && (InvServRecord.build != "0"))
            {
                if (Functions.GoodData(InvServRecord.additionalCosts))
                {
                    // Set the Additional Costs field
                    InventoryServicesLedger.repo.Build.AdditionalCosts.TextValue = InvServRecord.additionalCosts;
                }
                if (Functions.GoodData(InvServRecord.recordAdditionalCostsIn))
                {
                    // Set the "Record Additional Costs In" combobox
                    InventoryServicesLedger.repo.Build.RecordAdditionalCostsIn.Text = InvServRecord.recordAdditionalCostsIn;
                }
            }

            if (bSave)
            {
                InventoryServicesLedger.repo.Save.Click();
            }
        }


        public static ITEM _SA_Read()
        {
            return _SA_Read(null, null);
        }

        public static ITEM _SA_Read(string sNumToRead)
        {
            return _SA_Read(sNumToRead, null);
        }

        public static ITEM _SA_Read(string sNumToRead, string sDescToRead) //  method will read all fields and store the data in a ITEM record...if sending a var, you must send both
        {

            if (Functions.GoodData(sNumToRead))
            {
                if (!Functions.GoodData(sDescToRead))
                {
                    Functions.Verify(false, true, "Need both double and Description when sending a variable to this method");
                }
            }
            if (Functions.GoodData(sDescToRead))
            {
                if (!Functions.GoodData(sNumToRead))
                {
                    Functions.Verify(false, true, "Need both double and Description when sending a variable to this method");
                }
            }

            ITEM InvServRecord = new ITEM();

            if (Functions.GoodData(sNumToRead))	// assume you have  both Number and description here
            {
                InvServRecord.invOrServNumber = sNumToRead;
                InvServRecord.invOrServDescription = sDescToRead;
                string sID = InvServRecord.invOrServNumber + "  " + InvServRecord.invOrServDescription;
                if (InventoryServicesLedger.repo.SelectRecord.Text != sID)
                {
                    InventoryServicesLedger._SA_Open(InvServRecord);
                }
            }

            InvServRecord.invOrServNumber = Functions.GetField(InventoryServicesLedger.repo.SelectRecord.Text, "  ", 1);
            InvServRecord.invOrServNumberEdit = InventoryServicesLedger.repo.ItemNumber.TextValue;
            InvServRecord.invOrServDescription = Functions.GetField(InventoryServicesLedger.repo.SelectRecord.Text, "  ", 2);
            InvServRecord.invOrServDescriptionEdit = InventoryServicesLedger.repo.ItemDescription.TextValue;
            if (InventoryServicesLedger.repo.InventoryType.Text == "Inventory")
            {
                InvServRecord.InventoryType = true;
            }
            else
            {
                InvServRecord.InventoryType = false;
            }
            InvServRecord.inactiveCheckBox = InventoryServicesLedger.repo.InactiveItem.Checked;
            if (InvServRecord.InventoryType == true)
            {
                InvServRecord.internalServActivity = false;
            }
            else	// service
            {
                if (InventoryServicesLedger.repo.InternalServiceActivityInfo.Exists())	// PRO doesn't have Activity
                {
                    InvServRecord.internalServActivity = (InventoryServicesLedger.repo.InternalServiceActivity.Checked);
                }
                else
                {
                    InvServRecord.internalServActivity = false;
                }
            }

            // Get the Show Quantities In field from the Quantities tab. This is done here because the rest of that tab is extracted differently.
            InventoryServicesLedger.repo.Quantities.Tab.Click();
            {
                InvServRecord.showQuantitiesIn = InventoryServicesLedger.repo.Quantities.ShowQuantitiesIn.Text;
            }
            if (InventoryServicesLedger.repo.ItemCategoryInfo.Exists())
            {
                InvServRecord.invOrServCategory = InventoryServicesLedger.repo.ItemCategory.Text;
            }
            if (InvServRecord.InventoryType == true)	// inventory type
            {
                InventoryServicesLedger.repo.Quantities.Tab.Click();
                ITEM_QTY ITMQ;
                if (InventoryServicesLedger.repo.Quantities.ForLocationInfo.Exists())
                {
                    InvServRecord.ItemQuantities.Clear();
                    int numberOfLocations = InventoryServicesLedger.repo.Quantities.ForLocation.Items.Count;

                    for (int x = 0; x < numberOfLocations; x++)
                    {
                        ITMQ = new ITEM_QTY();
                        InventoryServicesLedger.repo.Quantities.ForLocation.Items[x].Select(); // need to add method for this
                        ITMQ.forLocation = InventoryServicesLedger.repo.Quantities.ForLocation.Text;
                        if (ITMQ.forLocation.ToUpper() != "ALL LOCATIONS")
                        {
                            ITMQ.minLevel = InventoryServicesLedger.repo.Quantities.MinimumLevel.TextValue;
                        }
                        InvServRecord.ItemQuantities.Add(ITMQ);
                    }
                }
            }

            InventoryServicesLedger.repo.Units.Tab.Click();
            if (InvServRecord.InventoryType == true)	// inventory type
            {
                InvServRecord.stockingUnitOfMeasure = InventoryServicesLedger.repo.Units.StockingUnitOfMeasure.TextValue;

                InvServRecord.sellSameAsStockUnitCheckBox =InventoryServicesLedger.repo.Units.SellSameAsStockUnit.Checked;
                if (InvServRecord.sellSameAsStockUnitCheckBox == false)
                {
                    // If the Selling units are not the same as stocking inventory units, the following fields are active and contain data
                    InvServRecord.sellUnitOfMeasure = InventoryServicesLedger.repo.Units.SellUnitOfMeasure.TextValue;
                    InvServRecord.sellRelationship = InventoryServicesLedger.repo.Units.SellRelationshipEditBox.TextValue;
                    InvServRecord.sellRelationshipComboBox = InventoryServicesLedger.repo.Units.SellRelationshipComboBox.Text;
                }

                InvServRecord.buySameAsStockUnitCheckBox = InventoryServicesLedger.repo.Units.BuySameAsStockUnit.Checked;
                if (InvServRecord.buySameAsStockUnitCheckBox == false)
                {
                    // If the Buying units are not the same as stocking inventory units, the following fields are active and contain data
                    InvServRecord.buyUnitOfMeasure = InventoryServicesLedger.repo.Units.BuyUnitOfMeasure.TextValue;
                    InvServRecord.buyRelationship = InventoryServicesLedger.repo.Units.BuyRelationshipEditBox.TextValue;
                    InvServRecord.buyRelationshipComboBox = InventoryServicesLedger.repo.Units.BuyRelationshipComboBox.Text;
                }

            }
            else	// service
            {
                // Get the Unit of Measure field if it's active (it's inactive when the "Activity (Time & Billing)" checkbox is not checked)
                if (InventoryServicesLedger.repo.Units.ServiceItemUnitOfMeasure.Enabled)
                {
                    InvServRecord.unitOfMeasure = InventoryServicesLedger.repo.Units.ServiceItemUnitOfMeasure.TextValue;
                }
            }



            InventoryServicesLedger.repo.Pricing.Tab.Click();
            if (InventoryServicesLedger.repo.Pricing.CurrencyInfo.Exists())
            {
                InvServRecord.currencyCode = InventoryServicesLedger.repo.Pricing.Currency.Text;
            }
            // Column names are not specific because they change
            const int PRICE_LIST_COLUMN = 0;
            const int PRICING_2ND_COLUMN = 1;
            const int PRICING_3RD_COLUMN = 2;	// This column doesn't exist when Canadian currency is chosen.
            int currencyCount;	// counter for the number of currencies
            string homeCurrency = "CANADIAN DOLLARS";
      
            if (InventoryServicesLedger.repo.Pricing.CurrencyInfo.Exists())
            {
                currencyCount = InventoryServicesLedger.repo.Pricing.Currency.Items.Count;
            }
            else
            {
                currencyCount = 1;
            }
            for (int x = 0; x < currencyCount; x++)
            {
                // Select the correct currency
                if (currencyCount > 1)
                {
                	InventoryServicesLedger.repo.Pricing.Currency.Items[x].Select();  // need method for this
                }

                // Restore containter to default
                //InventoryServicesLedger.repo.View.Click();
                //InventoryServicesLedger.repo.RestoreWindow.Click();

                InventoryServicesLedger.repo.Pricing.PriceContainer.ClickFirstCell();

                ITEM_PRICE ITMP;
                InvServRecord.ItemPrices.Clear();
                foreach (List<string> pricingLine in InventoryServicesLedger.repo.Pricing.PriceContainer.GetContents())
                {
                    ITMP = new ITEM_PRICE();
                    if (currencyCount > 1)
                    {
                        ITMP.currency = InventoryServicesLedger.repo.Pricing.Currency.Text;
                    }
                    ITMP.priceList = pricingLine[PRICE_LIST_COLUMN];

                    // Get different columns depending if it's the home currency or not
                    if ((!Functions.GoodData(ITMP.currency) || ITMP.currency.ToUpper() == homeCurrency))
                    {
                        ITMP.pricePerSellingUnit = pricingLine[PRICING_2ND_COLUMN];
                    }
                    else
                    {
                        ITMP.pricingMethod = pricingLine[PRICING_2ND_COLUMN];
                        ITMP.pricePerSellingUnit = pricingLine[PRICING_3RD_COLUMN];
                    }

                    // Ensure not getting blank line
                    if (pricingLine[PRICE_LIST_COLUMN] != "")
                    {
                        InvServRecord.ItemPrices.Add(ITMP);
                    }
                }
            }



            InventoryServicesLedger.repo.Linked.Tab.Click();
            if (InvServRecord.InventoryType == true)	// inventory type
            {
                InvServRecord.assetAccount.acctNumber = InventoryServicesLedger.repo.Linked.InvAssetAcct.Text;
                InvServRecord.revenueAccount.acctNumber = InventoryServicesLedger.repo.Linked.InvRevenueAcct.Text;
                InvServRecord.cogsAccount.acctNumber = InventoryServicesLedger.repo.Linked.InvCogsAcct.Text;
                InvServRecord.varianceAccount.acctNumber = InventoryServicesLedger.repo.Linked.InvVarianceAct.Text;
            }
            else	// If it's a Service item
            {
                InvServRecord.revenueAccount.acctNumber = InventoryServicesLedger.repo.Linked.ServRevenueAct.Text;
                InvServRecord.expenseAccount.acctNumber = InventoryServicesLedger.repo.Linked.ServExpenseAct.Text;
            }



            if (InvServRecord.InventoryType == true)	// inventory type
            {
           
                InventoryServicesLedger.repo.Build.Tab.Click();
                InvServRecord.build = InventoryServicesLedger.repo.Build.Build.TextValue;


                const int ITEM_COLUMN = 1;
                const int QUANTITY_COLUMN = 4;

                // Restore containter to default
                InventoryServicesLedger.repo.Self.Activate();
                //InventoryServicesLedger.repo.View.Click();
                //InventoryServicesLedger.repo.RestoreWindow.Click();

                InventoryServicesLedger.repo.Build.BuildContainer.ClickFirstCell();

                ITEM_BUILD_ITEMS ITMB;
                InvServRecord.BuildItems.Clear();
                foreach (List<string> buildLine in InventoryServicesLedger.repo.Build.BuildContainer.GetContents())
                {
                    ITMB = new ITEM_BUILD_ITEMS();
                    ITEM tempI = new ITEM();
                    tempI.invOrServNumber = buildLine[ITEM_COLUMN];
                    ITMB.item = tempI;
                    ITMB.quantity = buildLine[QUANTITY_COLUMN];

                    InvServRecord.BuildItems.Add(ITMB);
                }
                if (InvServRecord.build != "0")
                {
                    InvServRecord.additionalCosts = InventoryServicesLedger.repo.Build.AdditionalCosts.TextValue;
                    InvServRecord.recordAdditionalCostsIn = InventoryServicesLedger.repo.Build.RecordAdditionalCostsIn.Text;
                }
                // }
            }
            else	// service item
            {
                //if (InventoryServicesLedger.Instance.Tab.FindPage("Time & Billing") != 0)
                //{
                InventoryServicesLedger.repo.TimeBilling.Tab.Click();
                InvServRecord.activityTimeBillCheckBox = InventoryServicesLedger.repo.Activity.Checked;

                // If the "Activity (Time & Billing)" checkbox is checked then the objects in this tab are editable
                if (InvServRecord.activityTimeBillCheckBox == true)
                {
                    InvServRecord.unitOfMeasure = InventoryServicesLedger.repo.TimeBilling.UnitOfMeasure.Text;
                    InvServRecord.unitIsRelatedToTime = InventoryServicesLedger.repo.TimeBilling.UnitIsRelatedToTime.Checked;
                    InvServRecord.relationshipText = InventoryServicesLedger.repo.TimeBilling.RelationshipEditBox.TextValue;
                    InvServRecord.relationshipComboBox = InventoryServicesLedger.repo.TimeBilling.RelationshipComboBox.Text;
                    InvServRecord.serviceActivityIs = InventoryServicesLedger.repo.TimeBilling.ServiceActivityIs.Text;

                    if (InventoryServicesLedger.repo.TimeBilling.SomeTimesChargeForThisActivityInfo.Exists())
                    {
                        InvServRecord.sometimesChargeForThisActivity = InventoryServicesLedger.repo.TimeBilling.SomeTimesChargeForThisActivity.Checked;
                    }
                    else
                    {
                        InvServRecord.sometimesChargeForThisActivity = false;
                    }

                    InvServRecord.chargesAreBasedOn = InventoryServicesLedger.repo.TimeBilling.ChargesAreBasedOn.Text;
                    InvServRecord.flatFee = InventoryServicesLedger.repo.TimeBilling.FlatFee.TextValue;
                    InvServRecord.defaultPayrollIncome = InventoryServicesLedger.repo.TimeBilling.DefaultPayrollIncome.Text;
                }
                else
                {
                    InvServRecord.unitIsRelatedToTime = false;
                    InvServRecord.sometimesChargeForThisActivity = false;
                }
            }


            InventoryServicesLedger.repo.Statistics.Tab.Click();
            if (InvServRecord.InventoryType == true)	// inventory type
            {
                InvServRecord.showUnits = InventoryServicesLedger.repo.Statistics.ShowUnitsSoldIn.Text;
            }
            InventoryServicesLedger.repo.Self.Activate();
            ITEM_STATS ITMS;
            InvServRecord.ItemStats.Clear();
            // Capture the location name and all info related to it
            if (InventoryServicesLedger.repo.Statistics.ForLocationStatisticsInfo.Exists())	// Inventory locations feature is present only in SImply Premium and higher versions. Also doesn't exist if the item type is Service.
            {
                for (int x = 0; x < InventoryServicesLedger.repo.Statistics.ForLocationStatistics.Items.Count; x++)
                {
                    ITMS = new ITEM_STATS();
                    // move to the next combobox item - location - at each loop iteration
                    InventoryServicesLedger.repo.Statistics.ForLocationStatistics.Items[x].Select();
                    ITMS.forLocation = InventoryServicesLedger.repo.Statistics.ForLocationStatistics.Text;
                    if (ITMS.forLocation.ToUpper() != "ALL LOCATIONS")
                    {
                        ITMS = InventoryServicesLedger._SA_ReadStats();

                    }
                    // Append the line information
                    InvServRecord.ItemStats.Add(ITMS);
                }
            }
            else
            {
                ITMS = InventoryServicesLedger._SA_ReadStats();

                // Append the line information
                InvServRecord.ItemStats.Add(ITMS);
            }



            InventoryServicesLedger.repo.Taxes.Tab.Click();
            if (InventoryServicesLedger.repo.Taxes.DutyChargedOnThisItemInfo.Exists())
            {
                InvServRecord.dutyCharged = InventoryServicesLedger.repo.Taxes.DutyChargedOnThisItem.TextValue;
            }
            const int TAX_COLUMN = 1;
            const int TAX_EXEMPT_COLUMN = 2;

            // Restore containter to default
            InventoryServicesLedger.repo.Self.Activate();
            //InventoryServicesLedger.repo.View.Cick();
            //InventoryServicesLedger.repo.RestoreWindow.Click();

            InventoryServicesLedger.repo.Taxes.TaxContainer.ClickFirstCell();

            TAX_LEDGER TA;
            InvServRecord.TaxList.Clear();
            foreach (List<string> taxLine in InventoryServicesLedger.repo.Taxes.TaxContainer.GetContents())
            {
                TA = new TAX_LEDGER();
                TA.taxID = taxLine[TAX_COLUMN];
                TA.taxExempt = taxLine[TAX_EXEMPT_COLUMN];

                InvServRecord.TaxList.Add(TA);
            }



            InventoryServicesLedger.repo.AdditionalInfo.Tab.Click();

            if (InventoryServicesLedger.repo.AdditionalInfo.Additional1Info.Exists())
            {
                InvServRecord.additional1 = InventoryServicesLedger.repo.AdditionalInfo.Additional1.TextValue;
                InvServRecord.addCheckBox1 = InventoryServicesLedger.repo.AdditionalInfo.AddCheckBox1.Checked;
            }
            else
            {
                InvServRecord.addCheckBox1 = false;
            }

            if (InventoryServicesLedger.repo.AdditionalInfo.Additional2Info.Exists())
            {
                InvServRecord.additional2 = InventoryServicesLedger.repo.AdditionalInfo.Additional2.TextValue;
                InvServRecord.addCheckBox2 = InventoryServicesLedger.repo.AdditionalInfo.AddCheckBox2.Checked;
            }
            else
            {
                InvServRecord.addCheckBox2 = false;
            }

            if (InventoryServicesLedger.repo.AdditionalInfo.Additional3Info.Exists())
            {
                InvServRecord.additional3 = InventoryServicesLedger.repo.AdditionalInfo.Additional3.TextValue;
                InvServRecord.addCheckBox3 = InventoryServicesLedger.repo.AdditionalInfo.AddCheckBox3.Checked;
            }
            else
            {
                InvServRecord.addCheckBox3 = false;
            }

            if (InventoryServicesLedger.repo.AdditionalInfo.Additional4Info.Exists())
            {
                InvServRecord.additional4 = InventoryServicesLedger.repo.AdditionalInfo.Additional4.TextValue;
                InvServRecord.addCheckBox4 = InventoryServicesLedger.repo.AdditionalInfo.AddCheckBox4.Checked;
            }
            else
            {
                InvServRecord.addCheckBox4 = false;
            }

            if (InventoryServicesLedger.repo.AdditionalInfo.Additional5Info.Exists())
            {
                InvServRecord.additional5 = InventoryServicesLedger.repo.AdditionalInfo.Additional5.TextValue;
                InvServRecord.addCheckBox5 = InventoryServicesLedger.repo.AdditionalInfo.AddCheckBox5.Checked;
            }
            else
            {
                InvServRecord.addCheckBox5 = false;
            }


            InventoryServicesLedger.repo.DetailedDesc.Tab.Click();
            InvServRecord.longDescription = InventoryServicesLedger.repo.DetailedDesc.LongDescription.TextValue;
            InvServRecord.picture = InventoryServicesLedger.repo.DetailedDesc.Picture.TextValue;
            InvServRecord.thumbnail = InventoryServicesLedger.repo.DetailedDesc.Thumbnail.TextValue;

     
            if(InventoryServicesLedger.repo.SerialNumbers.TabInfo.Exists())
            {
                InventoryServicesLedger.repo.SerialNumbers.Tab.Click();
                // Get serial checkbox value
                InvServRecord.serialNumCheckBox = InventoryServicesLedger.repo.SerialNumbers.UsesSerialNumbers.Checked;

                // if serial is on for item get info
                if (InvServRecord.serialNumCheckBox == true)
                {
                    InventoryServicesLedger.repo.SerialNumbers.EditSerialNumbers.Click();	// Bring up serial window
                    //InvServRecord.serialNumbers = EditSerialNumber.Instance._SA_pGetSerialNumberDetails (); NC

                   // EditSerialNumber.repo.Cancel.Click();	// Close serial window
                    InventoryServicesLedger.repo.Self.Activate();	// Set focus back to main ledger window
                }
            }
            
            return InvServRecord;
        }


        public static ITEM_STATS _SA_ReadStats() // check if we need to objects for disabled fields
        {
            ITEM_STATS IMTS = new ITEM_STATS();
            IMTS.forLocation = InventoryServicesLedger.repo.SelectRecord.Text;
            // All of these fields are disabled if the account has already been added, so they will have different IDs
            // Also, some fields don't show up when a Service item is selected.

  
            IMTS.ytdNoOfTransactions = InventoryServicesLedger.repo.Statistics.YTDNoOfTransactions.TextValue;
     
            IMTS.ytdUnitsSold = InventoryServicesLedger.repo.Statistics.YTDUnitsSold.TextValue;
      
            IMTS.ytdAmountSold = InventoryServicesLedger.repo.Statistics.YTDAmountSold.TextValue;
       
            if (InventoryServicesLedger.repo.Statistics.YTDCostOfGoodsSoldInfo.Exists())
            {
            	IMTS.ytdCostOfGoodsSold = InventoryServicesLedger.repo.Statistics.YTDCostOfGoodsSold.TextValue;
            }
           
            IMTS.lastYearNoOfTransactions = InventoryServicesLedger.repo.Statistics.LastYearNoOfTransactions.TextValue;
        
            IMTS.lastYearUnitsSold = InventoryServicesLedger.repo.Statistics.LastYearUnitsSold.TextValue;
            
          	IMTS.lastYearAmountSold = InventoryServicesLedger.repo.Statistics.LastYearAmountSold.TextValue;
     
			if (InventoryServicesLedger.repo.Statistics.YTDCostOfGoodsSoldInfo.Exists())
			{
                IMTS.lastYearCostOfGoodsSold = InventoryServicesLedger.repo.Statistics.LastYearCostOfGoodsSold.TextValue;
			}
    
            IMTS.dateOfLastSale = InventoryServicesLedger.repo.Statistics.DateOfLastSale.TextValue;
   

            return (IMTS);
        }


        public static void DataFile_ReadFile(string sDataLocation, string fileCounter)
        {
            StreamReader FileHDR, FileDT11, FileDT1, FileDT3, FileDT10;
            StreamReader FileDT4, FileDT5, FileDT12, FileDT6, FileDT7;
            StreamReader FileDT8, FileDT9, FileDT13, FileCNT3, FileCNT;
            StreamReader FileCNT2, FileSRL;

            // Local variables
            string dataLine;	    // Stores the current field data from file
            string dataPath;	    // The name and path of the data file            


            string FUNCTION_ALIAS = "ISL";
            string EXTENSION_HEADER = ".HDR";
            string EXTENSION_QUANTITIES = ".DT11";
            string EXTENSION_UNITS = ".DT1";
            //private const string EXTENSION_PRICING = ".dt2";    // not currently in use
            string EXTENSION_LINKED = ".DT3";
            string EXTENSION_TIME_AND_BILLING = ".DT10";
            string EXTENSION_BUILD = ".DT4";
            string EXTENSION_STATISTICS = ".DT5";
            string EXTENSION_STATISTICS_LOCATION = ".DT12";
            string EXTENSION_TAXES = ".DT6";
            string EXTENSION_ADDITIONAL_INFO = ".DT7";
            string EXTENSION_HISTORY = ".DT8";
            string EXTENSION_DETAILED_DESCRIPTION = ".DT9";
            string EXTENSION_SERIAL_NUMBER = ".DT13";
            string EXTENSION_PRICING_CONTAINER = ".CNT3";
            string EXTENSION_BUILD_CONTAINER = ".CNT";
            string EXTENSION_TAXES_CONTAINER = ".CNT2";
            string EXTENSION_SERIAL_CONTAINER = ".SRL";


            // Get the data path from file
            dataPath = sDataLocation + "ISL" + fileCounter;


            List<ITEM> lItems = new List<ITEM>() { };

            // Open all data files and set the records
            FileHDR = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_HEADER, "FM_READ"));
            // While not EOF read the header file
            while ((dataLine = FileHDR.ReadLine()) != null)
            {
                ITEM myI = new ITEM();
                //Print (dataLine)

                // Set header info
                //myI =InventoryServicesLedger.DataFile_setDataStructure(EXTENSION_HEADER, dataLine, myI)
                InventoryServicesLedger.DataFile_setDataStructure(EXTENSION_HEADER, dataLine, myI);


                if (File.Exists(dataPath + EXTENSION_QUANTITIES))
                {
                    using (FileDT11 = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_QUANTITIES, "FM_READ")))
                    {
                        while ((dataLine = FileDT11.ReadLine()) != null)
                        {
                            //myI = InventoryServicesLedger.DataFile_setDataStructure(EXTENSION_QUANTITIES,dataLine, myI)
                            InventoryServicesLedger.DataFile_setDataStructure(EXTENSION_QUANTITIES, dataLine, myI);
                        }
                    }
                    //Functions.FileClose(FileDT11);
                }

                if (File.Exists(dataPath + EXTENSION_UNITS))
                {
                    using (FileDT1 = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_UNITS, "FM_READ")))
                    {
                        while ((dataLine = FileDT1.ReadLine()) != null)
                        {
                            //myI =InventoryServicesLedger.DataFile_setDataStructure(EXTENSION_UNITS,dataLine, myI)
                            InventoryServicesLedger.DataFile_setDataStructure(EXTENSION_UNITS, dataLine, myI);
                        }
                    }
                    //Functions.FileClose(FileDT1);
                }

                if (File.Exists(dataPath + EXTENSION_LINKED))
                {
                    using (FileDT3 = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_LINKED, "FM_READ")))
                    {
                        while ((dataLine = FileDT3.ReadLine()) != null)
                        {
                            //myI =InventoryServicesLedger.DataFile_setDataStructure(EXTENSION_LINKED,dataLine, myI)
                            InventoryServicesLedger.DataFile_setDataStructure(EXTENSION_LINKED, dataLine, myI);
                        }
                    }
                    //Functions.FileClose(FileDT3);
                }

                if (File.Exists(dataPath + EXTENSION_TIME_AND_BILLING))
                {
                    using (FileDT10 = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_TIME_AND_BILLING, "FM_READ")))
                    {
                        while ((dataLine = FileDT10.ReadLine()) != null)
                        {
                            //myI =InventoryServicesLedger.DataFile_setDataStructure(EXTENSION_TIME_AND_BILLING,dataLine, myI)
                            InventoryServicesLedger.DataFile_setDataStructure(EXTENSION_TIME_AND_BILLING, dataLine, myI);
                        }
                    }
                    //Functions.FileClose(FileDT10);
                }

                if (File.Exists(dataPath + EXTENSION_BUILD))
                {
                    using (FileDT4 = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_BUILD, "FM_READ")))
                    {
                        while ((dataLine = FileDT4.ReadLine()) != null)
                        {
                            //myI =InventoryServicesLedger.DataFile_setDataStructure(EXTENSION_BUILD,dataLine, myI)
                            InventoryServicesLedger.DataFile_setDataStructure(EXTENSION_BUILD, dataLine, myI);
                        }
                    }
                    //doBuild = TRUE
                    //Functions.FileClose(FileDT4);
                }

                if (File.Exists(dataPath + EXTENSION_STATISTICS))
                {
                    using (FileDT5 = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_STATISTICS, "FM_READ")))
                    {
                        dataLine = FileDT5.ReadLine();
                        //myI =InventoryServicesLedger.DataFile_setDataStructure(EXTENSION_STATISTICS,dataLine, myI)
                        InventoryServicesLedger.DataFile_setDataStructure(EXTENSION_STATISTICS, dataLine, myI);
                    }
                    //Functions.FileClose(FileDT5);
                }

                if (File.Exists(dataPath + EXTENSION_STATISTICS_LOCATION))
                {
                    using (FileDT12 = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_STATISTICS_LOCATION, "FM_READ")))
                    {
                        while ((dataLine = FileDT12.ReadLine()) != null)
                        {
                            //myI =InventoryServicesLedger.DataFile_setDataStructure(EXTENSION_STATISTICS_LOCATION,dataLine, myI)
                            InventoryServicesLedger.DataFile_setDataStructure(EXTENSION_STATISTICS_LOCATION, dataLine, myI);
                        }
                    }
                    //Functions.FileClose(FileDT12);
                }

                if (File.Exists(dataPath + EXTENSION_TAXES))
                {
                    using (FileDT6 = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_TAXES, "FM_READ")))
                    {
                        dataLine = FileDT6.ReadLine();
                        //myI =InventoryServicesLedger.DataFile_setDataStructure(EXTENSION_TAXES,dataLine, myI)
                        InventoryServicesLedger.DataFile_setDataStructure(EXTENSION_TAXES, dataLine, myI);
                    }
                    //Functions.FileClose(FileDT6);
                }

                if (File.Exists(dataPath + EXTENSION_ADDITIONAL_INFO))
                {
                    using (FileDT7 = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_ADDITIONAL_INFO, "FM_READ")))
                    {
                        dataLine = FileDT7.ReadLine();
                        //myI =InventoryServicesLedger.DataFile_setDataStructure(EXTENSION_ADDITIONAL_INFO,dataLine, myI)
                        InventoryServicesLedger.DataFile_setDataStructure(EXTENSION_ADDITIONAL_INFO, dataLine, myI);
                    }
                    //Functions.FileClose(FileDT7);
                }

                if (File.Exists(dataPath + EXTENSION_HISTORY))
                {
                    using (FileDT8 = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_HISTORY, "FM_READ")))
                    {
                        // the data structure is set later
                        while ((dataLine = FileDT8.ReadLine()) != null)
                        {
                            //myI =InventoryServicesLedger.DataFile_setDataStructure(EXTENSION_DETAILED_DESCRIPTION,dataLine, myI)
                            InventoryServicesLedger.DataFile_setDataStructure(EXTENSION_DETAILED_DESCRIPTION, dataLine, myI);
                        }
                    }
                    //Functions.FileClose(FileDT8);
                }

                if (File.Exists(dataPath + EXTENSION_DETAILED_DESCRIPTION))
                {
                    using (FileDT9 = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_DETAILED_DESCRIPTION, "FM_READ")))
                    {
                        dataLine = FileDT9.ReadLine();
                        //myI =InventoryServicesLedger.DataFile_setDataStructure(EXTENSION_DETAILED_DESCRIPTION,dataLine, myI)
                        InventoryServicesLedger.DataFile_setDataStructure(EXTENSION_DETAILED_DESCRIPTION, dataLine, myI);
                    }
                    //Functions.FileClose(FileDT9);
                }

                if (File.Exists(dataPath + EXTENSION_SERIAL_NUMBER))
                {
                    using (FileDT13 = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_SERIAL_NUMBER, "FM_READ")))
                    {
                        dataLine = FileDT13.ReadLine();
                        //myI =InventoryServicesLedger.DataFile_setDataStructure(EXTENSION_SERIAL_NUMBER,dataLine, myI)
                        InventoryServicesLedger.DataFile_setDataStructure(EXTENSION_SERIAL_NUMBER, dataLine, myI);
                    }
                    //doSerial = TRUE
                    //Functions.FileClose(FileDT13);
                }

                if (File.Exists(dataPath + EXTENSION_PRICING_CONTAINER))
                {
                    using (FileCNT3 = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_PRICING_CONTAINER, "FM_READ")))
                    {
                        while ((dataLine = FileCNT3.ReadLine()) != null)
                        {
                            //myI =InventoryServicesLedger.DataFile_setDataStructure(EXTENSION_PRICING_CONTAINER,dataLine, myI)
                            InventoryServicesLedger.DataFile_setDataStructure(EXTENSION_PRICING_CONTAINER, dataLine, myI);
                        }
                    }
                    //Functions.FileClose(FileCNT3);
                }

                if (File.Exists(dataPath + EXTENSION_BUILD_CONTAINER))
                {
                    using (FileCNT = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_BUILD_CONTAINER, "FM_READ")))
                    {
                        while ((dataLine = FileCNT.ReadLine()) != null)
                        {
                            //myI =InventoryServicesLedger.DataFile_setDataStructure(EXTENSION_BUILD_CONTAINER,dataLine, myI)
                            InventoryServicesLedger.DataFile_setDataStructure(EXTENSION_BUILD_CONTAINER, dataLine, myI);
                        }
                    }
                    //Functions.FileClose(FileCNT);
                }

                if (File.Exists(dataPath + EXTENSION_TAXES_CONTAINER))
                {
                    using (FileCNT2 = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_TAXES_CONTAINER, "FM_READ")))
                    {
                        while ((dataLine = FileCNT2.ReadLine()) != null)
                        {
                            //myI =InventoryServicesLedger.DataFile_setDataStructure(EXTENSION_TAXES_CONTAINER,dataLine, myI)
                            InventoryServicesLedger.DataFile_setDataStructure(EXTENSION_TAXES_CONTAINER, dataLine, myI);
                        }
                    }
                    //Functions.FileClose(FileCNT2);
                }

                if (File.Exists(dataPath + EXTENSION_SERIAL_CONTAINER))
                {
                    using (FileSRL = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_SERIAL_CONTAINER, "FM_READ")))
                    {
                        while ((dataLine = FileSRL.ReadLine()) != null)
                        {
                            //myI =InventoryServicesLedger.DataFile_setDataStructure(EXTENSION_SERIAL_CONTAINER,dataLine, myI)
                            InventoryServicesLedger.DataFile_setDataStructure(EXTENSION_SERIAL_CONTAINER, dataLine, myI);
                        }
                    }
                    //doSerialContainer = TRUE
                    // Functions.FileClose(FileSRL);
                }
                lItems.Add(myI);
            }
            // Close the header file
            FileHDR.Close();

            for (int x = 0; x < lItems.Count; x++)
            {
                ITEM myI = lItems[x];

                // Determine the mode
                switch (myI.action)
                {
                    case "A":
                        InventoryServicesLedger._SA_Create(myI);

                        break;
                    case "E":
                        InventoryServicesLedger._SA_Create(myI, true, true);

                        break;
                    case "D":
                        InventoryServicesLedger._SA_Delete(myI);

                        break;
                    case "H":
                        InventoryServicesLedger._SA_History(myI);
                        break;
                    case "B":
                        InventoryServicesLedger._SA_Build(myI);
                        break;
                    default:
                        {
                            Functions.Verify(false, true, "Correct Action Value");
                            break;
                        }
                }
            }
            InventoryServicesLedger.repo.Self.Close();
        }

        public static void DataFile_setDataStructure(string extension, string dataLine, ITEM ItemRecord)
        {
        	
        	
        	const string EXTENSION_HEADER = ".HDR";
            const string EXTENSION_QUANTITIES = ".DT11";
            const string EXTENSION_UNITS = ".DT1";
            //private const string EXTENSION_PRICING = ".dt2";    // not currently in use
            const string EXTENSION_LINKED = ".DT3";
            const string EXTENSION_TIME_AND_BILLING = ".DT10";
            const string EXTENSION_BUILD = ".DT4";
            const string EXTENSION_STATISTICS = ".DT5";
            const string EXTENSION_STATISTICS_LOCATION = ".DT12";
            const string EXTENSION_TAXES = ".DT6";
            const string EXTENSION_ADDITIONAL_INFO = ".DT7";
            const string EXTENSION_HISTORY = ".DT8";
            const string EXTENSION_DETAILED_DESCRIPTION = ".DT9";
            const string EXTENSION_SERIAL_NUMBER = ".DT13";
            const string EXTENSION_PRICING_CONTAINER = ".CNT3";
            const string EXTENSION_BUILD_CONTAINER = ".CNT";
            const string EXTENSION_TAXES_CONTAINER = ".CNT2";
            const string EXTENSION_SERIAL_CONTAINER = ".SRL";
        	
            //ITEM ItemRecord = New (ITEM)

            // Summary - Assigns the information to the recordset
            // Parameter "extension" - The file extension to determine which data file
            // Parameter "dataLine" - The current data line read from file

            switch (extension.ToUpper())
            {

                case EXTENSION_HEADER:	// Header
                    ItemRecord.action = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 1));
                    string sInvType = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 2));
                    if (sInvType == "I")
                    {
                        ItemRecord.InventoryType = true;
                    }
                    else if (sInvType == "S")
                    {
                        ItemRecord.InventoryType = false;
                    }
                    else
                    {
                        Functions.Verify(false, true, "Valid Inventory type for radio list. Got value - '" + sInvType + "'");
                    }
                    ItemRecord.invOrServNumber = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 3));
                    ItemRecord.invOrServDescription = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 4));
                    ItemRecord.inactiveCheckBox = ConvertFunctions.StringToBool(Functions.GetField(dataLine, ",", 5));
                    ItemRecord.internalServActivity = ConvertFunctions.StringToBool(Functions.GetField(dataLine, ",", 6));
                    ItemRecord.showQuantitiesIn = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 7));
                    ItemRecord.invOrServNumberEdit = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 8));
                    ItemRecord.invOrServDescriptionEdit = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 9));
                    ItemRecord.invOrServCategory = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 10));
                    break;
                case EXTENSION_QUANTITIES:	// Quantities tab, starting at For Location
                    if (ItemRecord.invOrServNumber == Functions.GetField(dataLine, ",", 1))
                    {
                        ITEM_QTY IQ = new ITEM_QTY();
                        IQ.forLocation = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 3));
                        IQ.minLevel = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 4));
                        if (Functions.GoodData(ItemRecord.ItemQuantities))
                        {
                            ItemRecord.ItemQuantities.Add(IQ);
                        }
                        else
                        {
                            ItemRecord.ItemQuantities = new List<ITEM_QTY> { IQ };
                        }
                    }

                    break;
                case EXTENSION_UNITS:	// Units tab
                    if (ItemRecord.invOrServNumber == Functions.GetField(dataLine, ",", 1))
                    {
                        ItemRecord.stockingUnitOfMeasure = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 3));
                        ItemRecord.sellSameAsStockUnitCheckBox = ConvertFunctions.StringToBool(Functions.GetField(dataLine, ",", 4));
                        ItemRecord.sellUnitOfMeasure = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 5));
                        ItemRecord.sellRelationship = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 6));
                        ItemRecord.sellRelationshipComboBox = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 7));
                        ItemRecord.buySameAsStockUnitCheckBox = ConvertFunctions.StringToBool(Functions.GetField(dataLine, ",", 8));
                        ItemRecord.buyUnitOfMeasure = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 9));
                        ItemRecord.buyRelationship = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 10));
                        ItemRecord.buyRelationshipComboBox = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 11));
                        ItemRecord.unitOfMeasure = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 12));

                    }
                    // the DT2 file is not used when entering data
                    break;
                case EXTENSION_LINKED:	// Linked tab
                    if (ItemRecord.invOrServNumber == Functions.GetField(dataLine, ",", 1))
                    {
                        ItemRecord.assetAccount.acctNumber = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 3));
                        ItemRecord.revenueAccount.acctNumber = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 4));
                        ItemRecord.cogsAccount.acctNumber = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 5));
                        ItemRecord.varianceAccount.acctNumber = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 6));
                        ItemRecord.expenseAccount.acctNumber = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 7));
                    }
                    break;
                case EXTENSION_TIME_AND_BILLING:	// Time & Billing tab (has to be DT10 because that's how it is in the old scripts)
                    if (ItemRecord.invOrServNumber == Functions.GetField(dataLine, ",", 1))
                    {
                        ItemRecord.activityTimeBillCheckBox = ConvertFunctions.StringToBool(Functions.GetField(dataLine, ",", 3));
                        ItemRecord.unitOfMeasure = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 4));
                        ItemRecord.unitIsRelatedToTime = ConvertFunctions.StringToBool(Functions.GetField(dataLine, ",", 5));
                        ItemRecord.relationshipText = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 6));
                        ItemRecord.relationshipComboBox = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 7));
                        ItemRecord.serviceActivityIs = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 8));
                        ItemRecord.sometimesChargeForThisActivity = ConvertFunctions.StringToBool(Functions.GetField(dataLine, ",", 9));
                        ItemRecord.chargesAreBasedOn = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 10));
                        ItemRecord.flatFee = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 11));
                        ItemRecord.defaultPayrollIncome = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 12));
                    }
                    break;
                case EXTENSION_BUILD:	// Build tab
                    if (ItemRecord.invOrServNumber == Functions.GetField(dataLine, ",", 1))
                    {
                        ItemRecord.build = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 3));
                        ItemRecord.additionalCosts = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 4));
                        ItemRecord.recordAdditionalCostsIn = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 5));

                    }
                    break;
                case EXTENSION_STATISTICS:	// Statistics tab
                    if (ItemRecord.invOrServNumber == Functions.GetField(dataLine, ",", 1))
                    {
                        ItemRecord.showUnits = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 3));
                    }
                    break;
                case EXTENSION_STATISTICS_LOCATION:	// Statistics-location info
                    if (ItemRecord.invOrServNumber == Functions.GetField(dataLine, ",", 1))
                    {
                        ITEM_STATS IST = new ITEM_STATS();
                        IST.forLocation = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 3));
                        IST.ytdNoOfTransactions = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 4));
                        IST.ytdUnitsSold = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 5));
                        IST.ytdAmountSold = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 6));
                        IST.ytdCostOfGoodsSold = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 7));
                        IST.lastYearNoOfTransactions = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 8));
                        IST.lastYearUnitsSold = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 9));
                        IST.lastYearAmountSold = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 10));
                        IST.lastYearCostOfGoodsSold = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 11));
                        IST.dateOfLastSale = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 12));
                        if (Functions.GoodData(ItemRecord.ItemStats))
                        {
                            ItemRecord.ItemStats.Add(IST);
                        }
                        else
                        {
                            ItemRecord.ItemStats = new List<ITEM_STATS> { IST };
                        }
                    }
                    break;
                case EXTENSION_TAXES:	// Taxes tab
                    if (ItemRecord.invOrServNumber == Functions.GetField(dataLine, ",", 1))
                    {
                        ItemRecord.dutyCharged = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 3));
                    }
                    break;
                case EXTENSION_ADDITIONAL_INFO:	// Additional Info tab
                    if (ItemRecord.invOrServNumber == Functions.GetField(dataLine, ",", 1))
                    {
                        ItemRecord.additional1 = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 3));
                        ItemRecord.additional2 = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 4));
                        ItemRecord.additional3 = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 5));
                        ItemRecord.additional4 = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 6));
                        ItemRecord.additional5 = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 7));
                        ItemRecord.addCheckBox1 = ConvertFunctions.StringToBool(Functions.GetField(dataLine, ",", 8));
                        ItemRecord.addCheckBox2 = ConvertFunctions.StringToBool(Functions.GetField(dataLine, ",", 9));
                        ItemRecord.addCheckBox3 = ConvertFunctions.StringToBool(Functions.GetField(dataLine, ",", 10));
                        ItemRecord.addCheckBox4 = ConvertFunctions.StringToBool(Functions.GetField(dataLine, ",", 11));
                        ItemRecord.addCheckBox5 = ConvertFunctions.StringToBool(Functions.GetField(dataLine, ",", 12));
                    }
                    break;
                case EXTENSION_HISTORY:	// History tab
                    if (ItemRecord.invOrServNumber == Functions.GetField(dataLine, ",", 1))
                    {
                        ITEM_HISTORY ITH = new ITEM_HISTORY();
                        ITH.location = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 3));
                        ITH.openingQuantity = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 4));
                        ITH.openingValue = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 5));
                        if (Functions.GoodData(ItemRecord.ItemHistory))
                        {
                            ItemRecord.ItemHistory.Add(ITH);
                        }
                        else
                        {
                            ItemRecord.ItemHistory = new List<ITEM_HISTORY> { ITH };
                        }
                    }
                    break;
                case EXTENSION_DETAILED_DESCRIPTION:	// Detailed Description tab
                    if (ItemRecord.invOrServNumber == Functions.GetField(dataLine, ",", 1))
                    {
                        ItemRecord.longDescription = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 3));
                        ItemRecord.picture = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 4));
                        ItemRecord.thumbnail = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 5));
                    }
                    break;
                case EXTENSION_SERIAL_NUMBER:	// Serial Numbers tab
                    if (ItemRecord.invOrServNumber == Functions.GetField(dataLine, ",", 1))
                    {
                        ItemRecord.serialNumCheckBox = ConvertFunctions.StringToBool(Functions.GetField(dataLine, ",", 3));
                    }
                    break;
                case EXTENSION_PRICING_CONTAINER:	// Pricing container
                    if (ItemRecord.invOrServNumber == Functions.GetField(dataLine, ",", 1))
                    {
                        ITEM_PRICE ITP = new ITEM_PRICE();
                        ITP.currency = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 3));
                        ITP.priceList = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 4));
                        ITP.pricingMethod = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 5));
                        ITP.pricePerSellingUnit = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 6));
                        if (Functions.GoodData(ItemRecord.ItemPrices))
                        {
                            ItemRecord.ItemPrices.Add(ITP);
                        }
                        else
                        {
                            ItemRecord.ItemPrices = new List<ITEM_PRICE> { ITP };
                        }
                    }
                    break;
                case EXTENSION_BUILD_CONTAINER: // Build container
                    if (ItemRecord.invOrServNumber == Functions.GetField(dataLine, ",", 1))
                    {
                        ITEM_BUILD_ITEMS ITB = new ITEM_BUILD_ITEMS();
                        ITB.item = new ITEM();
                        ITB.item.invOrServNumber = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 3));
                        ITB.quantity = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 4));
                        if (Functions.GoodData(ItemRecord.BuildItems))
                        {
                            ItemRecord.BuildItems.Add(ITB);
                        }
                        else
                        {
                            ItemRecord.BuildItems = new List<ITEM_BUILD_ITEMS> { ITB };
                        }

                    }
                    break;
                case EXTENSION_TAXES_CONTAINER:	// Taxes container
                    if (ItemRecord.invOrServNumber == Functions.GetField(dataLine, ",", 1))
                    {
                        TAX_LEDGER ITTA = new TAX_LEDGER();
                        ITTA.tax.taxName = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 3));
                        ITTA.taxExempt = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 4));
                        if (Functions.GoodData(ItemRecord.TaxList))
                        {
                            ItemRecord.TaxList.Add(ITTA);
                        }
                        else
                        {
                            ItemRecord.TaxList = new List<TAX_LEDGER> { ITTA };
                        }
                    }
                    break;
                case ".SRL":	// Serial container
                    if (ItemRecord.invOrServNumber == Functions.GetField(dataLine, ",", 1))
                    {
                        if (Functions.GoodData(ItemRecord.serialNumbers))
                        {
                            ItemRecord.serialNumbers.Add(Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 3)));
                        }
                        else
                        {
                            ItemRecord.serialNumbers = new List<string> { Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 3)) };
                        }
                    }
                    break;
                default:
                    {
                        Functions.Verify(false, true, "Valid header type used");
                        break;
                    }
            }

            //return ItemRecord;
        }

       	public static void _SA_Close()
		{
			repo.Self.Close();
	
		}

	}
	
	
	public static class InventoryServicesIcon
	{
		public static InventoryServicesLedgerResFolders.InventoryServicesIconAppFolder repo = InventoryServicesLedgerRes.Instance.InventoryServicesIcon;
	}
	
}
