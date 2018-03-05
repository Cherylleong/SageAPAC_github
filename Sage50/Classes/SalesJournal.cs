/*
 * Created by Ranorex
 * User: wonda05
 * Date: 5/19/2016
 * Time: 3:38 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Sage50.Repository;
using Ranorex;
using Sage50.Classes;
using Sage50.Types;
using Sage50.Shared;
using System.IO;
using System.Threading;

namespace Sage50.Classes
{
	/// <summary>
	/// Description of Class1.
	/// </summary>
	public static class SalesJournal
	{
		private const string sInvoice = "Invoice";
		private const string sOrder = "Order";
		private const string sQuote = "Quote";
						
		private const string FUNCTION_ALIAS = "SO";
		private const string EXTENSION_HEADER = ".HDR";
		private const string EXTENSION_TRANS_DETAILS = ".DT1";
		private const string EXTENSION_PROJECT = ".PRJ";
		private const string EXTENSION_SERIAL = ".SRL";
						
		public static SalesJournalResFolders.SalesJournalAppFolder repo = SalesJournalRes.Instance.SalesJournal;
		
	
		public static void _SA_Invoke()
		{
			if(Simply.isEnhancedView())
			{	
				Simply.repo.Self.Activate();
				Simply.repo.ReceivablesLink.Click();
				Simply.repo.SalesIcon.Click();
			}
			else
			{
				//
			}			
		}
		
		public static void _SA_Open(SALES transRecord)
		{			
			SalesJournal._SA_Open(transRecord, false, false);
		}
		
		public static void _SA_Open(SALES transRecord, bool bAdjustMode)
		{
			SalesJournal._SA_Open(transRecord, bAdjustMode, false);
		}
		
		public static void _SA_Open(SALES transRecord, bool bAdjustMode, bool bOneTimeCust)
		{
			// load sales quote, order or invoice in lookup or adjustment mode
			// bAdjustMode - flag that indicates whether to load journal entry in adjustment or lookup mode. needed for order/quote, which cannot be reversed in adjustment mode
			// bOneTimeCust -  flag that indicates whether to search by the customer name field. needed as cannot search on one-time customers						
			
			if (transRecord.GetType() == typeof(SALES_INVOICE))	// load an invoice entry
			{
				if (!SalesJournal.repo.SelfInfo.Exists()) //
				{
					SalesJournal._SA_Invoke();
					SalesJournal.repo.TransTypeDropDown.Select(sInvoice);
				}
				
				if (bAdjustMode)	// load in adjustment mode
				{
					SalesJournal.repo.ToolBar.Adjust.Click();	// bring up the Search window in adjusting mode
				}
				else	// load in lookup mode
				{
					SalesJournal.repo.ToolBar.LookUp.Click();	// bring up the Search window in lookup mode
				}
				                
                // wait for the Search window to show up
                if (!DotNetJournalSearch.repo.SelfInfo.Exists())
                {
                    Functions.Verify(false, true, "Sales Journal Search window found");
                }
                                                
                // set the search criteria
                DotNetJournalSearch._SA_SelectLookupDateRange();
				
                if (!(bOneTimeCust))	// search on a particular customer
				{
					DotNetJournalSearch.repo.Name.Select (transRecord.Customer.name);
				}
				else	// search for all customers
				{
					DotNetJournalSearch.repo.Name.Select("<Search All Customers>");
				}
				DotNetJournalSearch.repo.InvoiceNumber.TextValue = transRecord.transNumber;
				//sleep(Functions.RandInt(2,4));	// need during multi user mode, sometimes doesn't respond if all machines press ok at same time. cannot check multiuser status as the home window is not available
				
				DotNetJournalSearch.repo.OK.Click();
	
//				if (SelectAnInvoice.repo.SelfInfo.Exists())
//				{
//					SelectAnInvoice.repo.Select.Click();
//				}
				// comeback
				//SimplyMessage._SA_HandleMessage(SimplyMessage.OK, SimplyMessage._Msg_PlaceHolderToFixExistingMethodCalls)				
			}
			else	// load an order or quote entry
			{
				if (transRecord.GetType() == typeof(SALES_ORDER))	// set trans type to order
				{
					//if (SalesJournal.Instance.Window == null || SalesJournal.Instance.TransTypeDropDown.SelectedItem != "Order")
					if (!SalesJournal.repo.SelfInfo.Exists())
					{
						SalesJournal._SA_Invoke();
                    	SalesJournal.repo.TransTypeDropDown.Select(sOrder); // open sales journal before setting transaction type
					}
				}
				else	// set trans type to quote
				{
					//if (SalesJournal.Instance.Window == null || SalesJournal.Instance.TransTypeDropDown.SelectedItem != "Quote")
                    if (!SalesJournal.repo.SelfInfo.Exists())
					{
                    	SalesJournal.repo.TransTypeDropDown.Select(sQuote);
					}
				}
				SalesJournal.repo.Self.Activate();
				SalesJournal.repo.OrderQuoteNo.Select(transRecord.transNumber);
				SalesJournal.repo.Self.PressKeys("{Tab}");	// have to tab out to load the order/quote
				
				if (bAdjustMode)	// switch to the adjusting mode
				{
					SalesJournal.repo.ToolBar.Adjust.Click();
				}
				// else stay in the lookup mode					
			}          								

			//come back
  			//SimplyMessage.Instance._SA_HandleMessage(SimplyMessage.OK_LOC, SimplyMessage._MSG_THEORDERHASBEENREMOVEDBACKORDEREDQUANTITYWILLBEIGNORED_LOC, false, true);
		}
		
		
		public static void _SA_MatchDefaults(SALES transRecord)
        {
            if (!Functions.GoodData(transRecord.transNumber))	// set default transaction number if it's an invoice or quote (sales order doesn't have default number)
            {
                if (transRecord.GetType() == typeof(SALES_INVOICE))
                {
                    transRecord.transNumber = Variables.globalSettings.CompanySettings.FormSettings.nextNumInvoice;
                }
                else if (transRecord.GetType() == typeof(SALES_QUOTE))
                {
                    transRecord.transNumber = Variables.globalSettings.CompanySettings.FormSettings.nextNumSalesQuote;
                }
            }
            if (!Functions.GoodData(transRecord.shipFrom) && (Variables.globalSettings.InventorySettings.UseMultipleLocations == true))	// set default location according to global settings
            {
                transRecord.shipFrom = Variables.globalSettings.InventorySettings.Locations[0].code;
            }
            if (Functions.GoodData(transRecord.GridRows))	// set the default account and tax code
            {         

            	foreach(ROW currentRow in transRecord.GridRows)
                {
                    if (Functions.GoodData(currentRow))	// if it's not a blank row
                    {
                        if (!Functions.GoodData(currentRow.Account.acctNumber))	// set account for each row
                        {
                            // default to the revenue account specified in the item record
                            if (Functions.GoodData(currentRow.Item.invOrServNumber) && Functions.GoodData(currentRow.Item.revenueAccount.acctNumber))
                            {
                                currentRow.Account = currentRow.Item.revenueAccount;
                            }
                            // or default to the revenue account specified in the customer record
                            else if (Functions.GoodData(transRecord.Customer.name) && Functions.GoodData(transRecord.Customer.revenueAccount.acctNumber))
                            {
                                currentRow.Account = transRecord.Customer.revenueAccount;
                            }
                        }
                        if (!Functions.GoodData(currentRow.TaxCode.code) && Functions.GoodData(transRecord.Customer.taxCode.code))	// set tax code to the customer's default
                        {
                            currentRow.TaxCode = transRecord.Customer.taxCode;
                        }
                    }
                }
            }
        }
		
		public static void _SA_Create(SALES transRecord)
		{
			_SA_Create(transRecord, true, false, false, false);
		}
		
		public static void _SA_Create(SALES transRecord, bool bSave)
		{
			_SA_Create(transRecord, bSave, false, false, false);
		}
		
		public static void _SA_Create(SALES transRecord, bool bSave, bool bEdit)
		{
			_SA_Create(transRecord, bSave, bEdit, false, false);
		}
		
		public static void _SA_Create(SALES transRecord, bool bSave, bool bEdit, bool bRecur)
		{
			_SA_Create(transRecord, bSave, bEdit, bRecur, false);
		}
		
		public static void _SA_Create(SALES transRecord, bool bSave, bool bEdit, bool bRecur, bool bOneTime)
		{
			string sType = sInvoice;
						
			if(!Variables.bUseDataFiles)
			{
				SalesJournal._SA_MatchDefaults(transRecord);
			}
			
			if (!SalesJournal.repo.SelfInfo.Exists())
			{
				SalesJournal._SA_Invoke();
			}
            
            if (transRecord.GetType() == typeof(SALES_QUOTE))
            {
            	sType = sQuote;
            	if (SalesJournal.repo.TransTypeDropDown.Enabled && (SalesJournal.repo.TransTypeDropDown.Text != sQuote))
            	{
            		SalesJournal.repo.TransTypeDropDown.Select(sQuote);
            	}
            }
            else if (transRecord.GetType() == typeof(SALES_ORDER))
            {
            	sType = sOrder;
            	if (SalesJournal.repo.TransTypeDropDown.Enabled && (SalesJournal.repo.TransTypeDropDown.Text != sOrder))
            	{            		
					SalesJournal.repo.TransTypeDropDown.Select(sOrder);
            	}
            }
            else if (transRecord.GetType() == typeof(SALES_INVOICE))
            {
            	if (SalesJournal.repo.TransTypeDropDown.Enabled && (SalesJournal.repo.TransTypeDropDown.Text != sInvoice))
            	{
					SalesJournal.repo.TransTypeDropDown.Select(sInvoice);            	
            	}				
            }
          	
            if (!(bEdit))
            {
            	if (!(bRecur))
            	{
            		Ranorex.Report.Info(String.Format("Creating Sales {0} {1}.", sType, transRecord.transNumber));
            	}
            }
            else	// adjust
            {
            	if ((transRecord.GetType() == typeof(SALES_INVOICE) && transRecord.transNumber != SalesJournal.repo.InvoiceNumber.TextValue) || (transRecord.GetType() != typeof(SALES_INVOICE) && transRecord.transNumber != SalesJournal.repo.OrderQuoteNo.Text))
            	{
            		Ranorex.Report.Info(String.Format("Adjusting Sales {0} {1}", sType ,transRecord.transNumber));
            		// open existing invoice
            		SalesJournal._SA_Open(transRecord, bEdit);
            	}
            }

			                        
            // set customer
			if (!(bEdit) || (transRecord.GetType() == typeof(SALES_INVOICE)))	// entering new or adjusting invoice
			{
				if (transRecord.Customer.name != SalesJournal.repo.CustomerName.Text)	// adjusting customer
				{               
					SalesJournal.repo.CustomerName.Select(transRecord.Customer.name);
            
                    //SalesJournal.repo.CustomerName.PressKeys("{Tab}");	// Must press tab, otherwise selecting the Paid By field may not work
					if (bEdit)
					{
                        // come back
						//SimplyMessage.repo._SA_HandleMessage(SimplyMessage.YES_LOC, SimplyMessage._MSG_IFYOUCHANGETHECUSTOMERFORTHISINVOICE_LOC, true, true);
                        //SimplyMessage.repo._SA_HandleMessage(SimplyMessage.YES_LOC, SimplyMessage._MSG_YOUHAVEPICKEDACUSTOMERTHATUSESDIFFERENTCURRENCY, false, true);
					}
				}
			}

            //  Validate if Add on the fly is present
            if (AddOnTheFly.repo.SelfInfo.Exists())
            {
//                try
//                {
//                    Functions.Verify(true, true, "Add on fly message appears");
//                }
//                catch
//                {
//                    Functions.ExceptPrint();
//                }

				Ranorex.Report.Info("Add on fly message appears.");

                if (bOneTime)
                {
                	AddOnTheFly.repo.Continue.Select();
                }
                else
                {
                    AddOnTheFly.repo.QuickAdd.Select();
                }
                AddOnTheFly.repo.OK.Click();
            }


			if(transRecord.GetType() == typeof(SALES_INVOICE))	// set style for invoice, transaction number,  & Quote Number if present
			{
                //Set the style to standard
                SalesJournal.repo.Self.Activate();
                SalesJournal.repo.View.Click();	// View
                SalesJournal.repo.Self.PressKeys("s");	// Invoice style
                SalesJournal.repo.Self.PressKeys("s");	// Standard
				
				if (Functions.GoodData (transRecord.transNumber))
				{
					SalesJournal.repo.InvoiceNumber.TextValue = transRecord.transNumber;
                    SalesJournal.repo.InvoiceNumber.PressKeys ("{Tab}");  // must tab out or won't set properly
				}
				else
				{
					// Print("Invoice number is missing")
					// Verify(FALSE, TRUE, "Invoice Number")
                    transRecord.transNumber = SalesJournal.repo.InvoiceNumber.TextValue; // need to set invoice number for corresponding receipts
				}
                if (Functions.GoodData(((SALES_INVOICE)transRecord).quoteOrderTransNumber))
				{
                	SalesJournal.repo.OrderQuoteNo.Select((transRecord as SALES_INVOICE).quoteOrderTransNumber);
                    //SalesJournal.repo.OrderQuoteNo.PressKeys("{Tab}");  // Tab out
					
					// Press yes on message if it exists. need to find out the message content
					// come back
					//SimplyMessage.repo._SA_HandleMessage(SimplyMessage.YES_LOC, SimplyMessage._MSG_PLACEHOLDERTOFIXEXISTINGMETHODCALLS_LOC);				
				}
			}
			else	// set order/quote number
			{
				if (Functions.GoodData (transRecord.transNumber))
				{
					SalesJournal.repo.OrderQuoteNo.Select(transRecord.transNumber);
                    //SalesJournal.repo.OrderQuoteNo.PressKeys("{Tab}");
				}
				else
				{				
					Ranorex.Report.Info("Order/Quote double is missing.");
					//Functions.Verify(false, true, "Order/Quote Number");
					Validate.Fail();
				}
			}
			
			if (transRecord.GetType() != typeof(SALES_QUOTE))	// Set Paid by and chequeNumbers. This step needs to be done after loading the order/quote number if it's a conversion
			{
                if (Functions.GoodData(((SALES_COMMON_ORDER_INVOICE)transRecord).paidBy))
				{
					SalesJournal.repo.PaidBy.Select ((transRecord as SALES_COMMON_ORDER_INVOICE).paidBy);  
				}
                
				if(SalesJournal.repo.DepositToInfo.Exists())
				{
                    if (Functions.GoodData(((SALES_COMMON_ORDER_INVOICE)transRecord).DepositAccount.acctNumber))
					{
                        SalesJournal.repo.DepositTo.Select((transRecord as SALES_COMMON_ORDER_INVOICE).DepositAccount.acctNumber);
					}
				}
                if (SalesJournal.repo.ChequeNumberInfo.Exists())
				{
                    if (Functions.GoodData(((SALES_COMMON_ORDER_INVOICE)transRecord).chequeNumber))
					{
                        SalesJournal.repo.ChequeNumber.TextValue = ((SALES_COMMON_ORDER_INVOICE)transRecord).chequeNumber;
					}
				}
                if (Functions.GoodData(((SALES_COMMON_ORDER_INVOICE)transRecord).padNumber) && SalesJournal.repo.PadNumberInfo.Exists())
				{
                    SalesJournal.repo.PadNumber.TextValue = ((SALES_COMMON_ORDER_INVOICE)transRecord).padNumber;
				}
				
			}
			
            // must use typekeys for dates otherwise tab out will clear the fields
            // the tab is required in winXP or the date will not be registered in the field
			if (Functions.GoodData (transRecord.shipDate))
			{
                SalesJournal.repo.ShipDate.TextValue = transRecord.shipDate;
                SalesJournal.repo.ShipDate.PressKeys("{Tab}");
			}
			if (Functions.GoodData (transRecord.transDate))
			{
                SalesJournal.repo.InvoiceDate.TextValue = transRecord.transDate;
                SalesJournal.repo.InvoiceDate.PressKeys("{Tab}");
			}
			if (Functions.GoodData (transRecord.SalesPerson.name))
			{
				SalesJournal.repo.SoldBy.Select (transRecord.SalesPerson.name);
			}
			if(Functions.GoodData (transRecord.exchangeRate) && SalesJournal.repo.ExchangeRateInfo.Exists())
			{
                //SalesJournal.repo.ExchangeRate.SetFocus();  // need to set focus to exchange rate before typing in new rate. this will trigger the exchange rate has been changed message and will save new rate to database.
                SalesJournal.repo.ExchangeRate.TextValue = transRecord.exchangeRate;
                SalesJournal.repo.ExchangeRate.PressKeys("{Tab}");	// tab out to trigger the message below
				
                // handle the message on change of exchange rate if it presents
                
                // come back
                //SimplyMessage.repo._SA_HandleMessage(SimplyMessage.YES_LOC, SimplyMessage._MSG_THEEXCHANGERATEHASBEENCHANGED_LOC);

			}
            if (Functions.GoodData(transRecord.shipFrom) && SalesJournal.repo.ItemsStoredAtInfo.Exists())
			{
				if(SalesJournal.repo.ItemsStoredAt.Enabled)
				{
					SalesJournal.repo.ItemsStoredAt.Select (transRecord.shipFrom);
				}
			}
			if (transRecord.GetType() != typeof(SALES_QUOTE))
			{
                if (Functions.GoodData(((SALES_COMMON_ORDER_INVOICE)transRecord).project) && SalesJournal.repo.ProjectInfo.Exists())
				{
                    SalesJournal.repo.Project.Select(((SALES_COMMON_ORDER_INVOICE)transRecord).project);
				}
			}                        
                          
            SALES_TABLE ST = new SALES_TABLE();
                        
            if(bEdit)
            {
            	//Max window if needed
            }
            
            // fill out grid
            //int nLine;
            //for (int nLine = 1; nLine <= transRecord.GridRows.Count; nLine++)
            int nLine = 1;	
            foreach(ROW currentRow in transRecord.GridRows)
            {
            	
            	if(transRecord.GridRows.Count == 0 && bEdit)
            	{
            		//RemoveRow(nLine);
            	}
            	else
            	{
            		if(Functions.GoodData(currentRow.Item.invOrServNumber))
            		{
            			if(transRecord.GetType()== typeof(SALES_INVOICE))
            			{
            				SalesJournal.repo.TransContainer.SelectCell("Quantity", nLine);
            				SalesJournal.repo.TransContainer.Focus();
            				SalesJournal.repo.TransContainer.PressKeys("{LShiftKey down}{Tab}{LShiftKey up}");
            				
            				
            				if(SalesJournal.repo.ContainTextInfo.Exists())
            				{
            					//SalesJournal.repo.TransContainer.SelectCell("ItemNumber", nLine,currentRow.Item.invOrServNumber);
            					SalesJournal.repo.ContainText.PressKeys(currentRow.Item.invOrServNumber);
            				}
            			}
            			else
            			{
            				SalesJournal.repo.TransContainer.SelectCell("ItemNumber", nLine);
            				
            				if(bEdit)
            				{
            					SalesJournal.repo.TransContainer.PressKeys("{Shift+Home}");
                                SalesJournal.repo.TransContainer.PressKeys("{Delete}");
            				}
            				SalesJournal.repo.TransContainer.SelectCell("ItemNumber", nLine,currentRow.Item.invOrServNumber);	// Enter item number
                            SalesJournal.repo.TransContainer.PressKeys("{Tab}");		
            			}
            			
// come back            			
//            			if(SalesJournal.repoSelectInventory.SelfInfo.Exists() && currentRow.isInventoryItem == false)
//            			{
//            				SalesJournal.repoSelectInventory.Self.Activate();
//            				SalesJournal.repoSelectInventory.Self.Close();
//            			}
            		}
            		if(transRecord.GetType() == typeof(SALES_INVOICE))
            		{
            			if(Functions.GoodData(currentRow.quantityShipped))
            			{
            				SalesJournal.repo.TransContainer.SelectCell("ItemNumber", nLine);             				
             				SalesJournal.repo.TransContainer.PressKeys("{Tab}");
             				SalesJournal.repo.TransContainer.PressKeys("{Delete}");	// delete does not remove existing text
            				
            				SalesJournal.repo.TransContainer.SelectCell("Quantity", nLine, currentRow.quantityShipped);            				
            			}
            			
            			if (((SALES_INVOICE)transRecord).SerialNumberDetails.Count > 0)
            			{
            				bool bFoundItem = false;
            				int iItemCnt;  //to be used later outside the loop
            				
            				for(iItemCnt = 0; iItemCnt < ((SALES_INVOICE)transRecord).SerialNumberDetails.Count; iItemCnt++)
            				{
            					if(((SALES_INVOICE)transRecord).SerialNumberDetails[iItemCnt].Item.invOrServNumber == currentRow.Item.invOrServNumber)
            					{
            						bFoundItem = true;
            					}
            				}
            				if(bFoundItem)
            				{
            					// enter serial number stuff here later	
            				}
            			}
            		}
            		
            		// where paste is from
            		if (transRecord.GetType() == typeof(SALES_ORDER) || transRecord.GetType() == typeof(SALES_QUOTE))	// Enter quantity ordered if specified for either orders or quotes
                    {
                        if (Functions.GoodData(currentRow.quantityOrdered) && currentRow.quantityOrdered != "0")
                        {
                            SalesJournal.repo.TransContainer.SelectCell("Quantity", nLine);
                            SalesJournal.repo.TransContainer.PressKeys("{Tab}");
                            SalesJournal.repo.TransContainer.SelectCell("Order", nLine, currentRow.quantityOrdered);	// Enter order
                        }
                    }
                    if (transRecord.GetType() != typeof(SALES_QUOTE))	// Enter quantity backordered if specified for orders only
                    {
                        if (Functions.GoodData(currentRow.quantityBackordered))
                        {
                            SalesJournal.repo.TransContainer.SelectCell("BackOrder", nLine,currentRow.quantityBackordered);
                            //SalesJournal.repo.ContainText.PressKeys(currentRow.quantityBackordered);	// Enter back order
                        }
                    }
                    if (Functions.GoodData (currentRow.unit))
                    {
                        SalesJournal.repo.TransContainer.SelectCell("Unit", nLine, currentRow.unit);
                        //SalesJournal.repo.ContainText.PressKeys(currentRow.unit);	// Enter unit
                    }
                    if (Functions.GoodData (currentRow.description))
                    {
                        SalesJournal.repo.TransContainer.SelectCell("ItemDescription", nLine, currentRow.description);
                        //SalesJournal.repo.ContainText.PressKeys(currentRow.description);	// Enter description
                    }
                    if (Functions.GoodData(currentRow.basePrice) && Functions.GoodData(ST.iBasePrice))
                    {
                        SalesJournal.repo.TransContainer.SelectCell("BasePrice", nLine, currentRow.basePrice );
                        //SalesJournal.repo.ContainText.PressKeys(currentRow.basePrice);	// Enter base price 
                    }
                    if (Functions.GoodData(currentRow.discount) && Functions.GoodData(ST.iDiscount))
                    {
                        SalesJournal.repo.TransContainer.SelectCell("Discount", nLine, currentRow.discount);
                        //SalesJournal.repo.ContainText.PressKeys(currentRow.discount);	// Enter line item discount
                    }
                    if (Functions.GoodData(currentRow.price))	// added debugging code as the result of  frequent failures in tests
                    {
                        SalesJournal.repo.TransContainer.SelectCell("Price", nLine, currentRow.price);
                        // Workaround for default clicking on the left side of a cell
                        if (SelectPrice.repo.SelfInfo.Exists())
                        {
                        	SelectPrice._Cancel();
                        }
                        
                        //SalesJournal.repo.ContainText.PressKeys(currentRow.price);
                    }
                    if (Functions.GoodData (currentRow.amount))
                    {
                        if(currentRow.amount.Length <= 12)	// the amounts field only can time in 11 characters, let it calc if longer
                        {
                        	SalesJournal.repo.TransContainer.SelectCell("Amount", nLine, currentRow.amount);
                            //SalesJournal.repo.ContainText.PressKeys(currentRow.amount);	// Enter amount //amount has halved bcoz quantity has halved, but script still enters full amount.  GW
                        }
                        else if (transRecord.GetType() == typeof(SALES_INVOICE))
                        {
                            if (currentRow.quantityShipped == "0" || transRecord.GridRows[nLine] == null)
                            {
                                //Trace.WriteLine ("Cannot type an amount of more than 999,999,999.99.");
                                //FunctionsVerify(false, true, "Amount");								
                            }
                        }
                    }
                    if (Functions.GoodData (currentRow.TaxCode.code))	// added debugging code as the result of  frequent failures in tests
                    {
                        // debugging code added due to tax code wasn't get set properly sometimes. can try up to two times and log error if still not correct.	
                        // have to go to amount field first then tab, since selecting tax code will bring up dialog
                        // will have to add dialog if want to select tax cell
                        SalesJournal.repo.TransContainer.SelectCell("Amount", nLine);
                        SalesJournal.repo.TransContainer.PressKeys("{Tab}");
                       
                        // set tax code as specified
                        if (currentRow.TaxCode.description != Variables.sNoTax)	// Enter tax code
                        { 
                            SalesJournal.repo.TransContainer.SelectCell("Tax",nLine, currentRow.TaxCode.code);
                        }
                        else	// set to no tax by deleting the current one if any
                        {      
                            SalesJournal.repo.TransContainer.PressKeys("{Delete}");
                        }
						
                        // DW add back in if necessary
                        //// verify for debugging purpose
                        //SalesJournal.repo.TransContainer.PressKeys("<Tab>");	// tab out
                        //SalesJournal.repo.TransContainer.PressKeys("<Shift-Tab>");	// then back
						
                        //// if no tax, then the field is blank
                        //if(currentRow.TaxCode.description == sNoTax)
                        //{
                        //    FunctionsVerifyFunction(SalesJournal.repo.GetCellValue({nLine, ST.iTax}), "", "Tax code is set correctly in line " + nLine + "");	// no tax returns as blank string
                        //}
                        //else
                        //{
                        //    VerifyFunction(SalesJournal.repo.GetCellValue({nLine, ST.iTax}), NullToBlankString(currentRow.TaxCode.code), "Tax Code is set correctly in line " + nLine + "");	// no tax shows as blank string
                        //}
                    }
                    if (Functions.GoodData (currentRow.Account.acctNumber))
                    {
                        if (currentRow.Account.acctNumber != "")
                        {
                            SalesJournal.repo.TransContainer.SelectCell("Account", nLine, currentRow.Account.acctNumber);
                            //SalesJournal.repo.ContainText.PressKeys(currentRow.Account.acctNumber);	// Enter account number
                            SalesJournal.repo.TransContainer.PressKeys("{Tab}");	
                        }
                    }
                    else	// just pick a random account from the popup cnotainer. commented out since this should be done in the test case intead
                    {                    	                	
                        // only set account if blank in container
                        if(SalesJournal.repo.TransContainer.GetCell("Account", nLine) =="(null)" && Functions.GoodData(currentRow.Item.invOrServNumber))
                        {
                        	SalesJournal.repo.TransContainer.SelectCell("Account", nLine);
                        	
                        	if(!SelectAccountDialog.repo.SelectInfo.Exists())
                        	{
                        		SalesJournal.repo.Self.PressKeys("{Enter}");
                        	}
                        	                        	                        	
                        	SelectAccountDialog.repo.DotNetAccountName.RandPick();
                        	SelectAccountDialog.repo.Select.Click();
                        }
                    }
                    if (currentRow.Projects.Count != 0)
                    {
                        // Enter project allocation
                        // Process project allocation details if applicable
                        SalesJournal.repo.TransContainer.SelectCell("Account", nLine);
                        SalesJournal.repo.Self.PressKeys("{Ctrl Shift}a");
                        if (ProjectAllocationDialog.repo.SelfInfo.Exists())
                        {
                            ProjectAllocationDialog._SA_EnterProjectAllocationDetails(currentRow.Projects);
                        }						
                    }
                }
            	nLine++;
            }

            //come back
            // remove extra rows at the end
//            if (bEdit)
//            {
//                //nLine++;
//                //SalesJournal.repo.TransContainer.SelectCell(1, nLine);				
//                while (SalesJournal.repo.TransContainer.DataGridItem(GetDataGridItemLocator(TRANSCONTAINER_LOC, 0, nLine)).Text != "")
//                {
//					
//                    RemoveRow(nLine);
//					
//                    // SalesJournal.TransContainer.SetFocusCell({nLine,1})	// back to item number field
//                    //SalesJournal.repo.TransContainer.PressKeys("<Down>");
//                    //Line++;
//                }
//            }
//            	
            		
            // maybe not be needed		
//			if (bEdit)
//            {
//                SalesJournal.Instance.Window.Restore();
//            }

            if (transRecord.GetType() == typeof(SALES_ORDER) && Functions.GoodData((transRecord as SALES_ORDER).depositRefNumber) &&
                ((transRecord as SALES_COMMON_ORDER_INVOICE).paidBy != "Pay Later"))	// prepaid orders
            {
                SalesJournal.repo.DepositRefNumber.TextValue = (transRecord as SALES_ORDER).depositRefNumber;
                //SalesJournal.repo.DepositRefNumber.TypeKeys("<Tab>");
            }
            if (transRecord.GetType() != typeof(SALES_QUOTE) && Functions.GoodData((transRecord as SALES_COMMON_ORDER_INVOICE).depositApplied) && 
                SalesJournal.repo.DepositAppliedInfo.Exists())	// include invoices converted from prepaid orders, which can be changed to pay later
            {
				//SalesJournal.repo.DepositApplied.Focus();
                SalesJournal.repo.DepositApplied.TextValue = (transRecord as SALES_COMMON_ORDER_INVOICE).depositApplied;
            }

            if (Functions.GoodData(transRecord.freightAmount))
            {
                SalesJournal.repo.FreightAmount.TextValue = transRecord.freightAmount;
                SalesJournal.repo.FreightAmount.PressKeys("{Tab}");
            }

            if (Functions.GoodData(transRecord.FreightTaxCode.code))
            {
                SalesJournal.repo.FreightCode.TextValue = transRecord.FreightTaxCode.code;
                SalesJournal.repo.FreightCode.PressKeys("{Tab}");
                
            }
            else if (transRecord.FreightTaxCode.description == Variables.sNoTax)
            {
                //SalesJournal.repo.FreightCode.SetFocus();
                SalesJournal.repo.FreightCode.PressKeys("{Delete}");
                SalesJournal.repo.FreightCode.PressKeys("{Tab}");
            }

            if (Functions.GoodData(transRecord.freightTaxTotal))	// db has more than 2 taxes defined
            {
                SalesJournal.repo.FreightTaxTotal.TextValue = transRecord.freightTaxTotal;
            }
            else	// only two taxes and both shown
            {
                if (Functions.GoodData(transRecord.freightTax1))
                {
                    SalesJournal.repo.FreightTax1.TextValue = transRecord.freightTax1;
                }
                if (Functions.GoodData(transRecord.freightTax2))
                {
                    SalesJournal.repo.FreightTax2.TextValue = transRecord.freightTax2;
                }
            }
            if (SalesJournal.repo.TermsPercentInfo.Exists())
            {
                if (Functions.GoodData(transRecord.termsPercent))
                {
                    SalesJournal.repo.TermsPercent.TextValue = transRecord.termsPercent;
                }
                if (Functions.GoodData(transRecord.termsDays))
                {
                    SalesJournal.repo.TermsDay.TextValue = transRecord.termsDays;
                }
                if (Functions.GoodData(transRecord.termsNetDays))
                {
                    SalesJournal.repo.TermsNetDays.TextValue = transRecord.termsNetDays;
                }
            }
            
            if (SalesJournal.repo.EarlyDiscountPercentInfo.Exists())
            {
                if (Functions.GoodData((transRecord as SALES_INVOICE).earlyPaymentDiscountPercent))
                {
                    SalesJournal.repo.EarlyDiscountPercent.TextValue = (transRecord as SALES_INVOICE).earlyPaymentDiscountPercent;
                    SalesJournal.repo.EarlyDiscountPercent.PressKeys("{Tab}");
                }
                if (Functions.GoodData((transRecord as SALES_INVOICE).earlyPaymentDiscount))
                {
                    SalesJournal.repo.EarlyDiscountAmount.TextValue = (transRecord as SALES_INVOICE).earlyPaymentDiscount;
                    SalesJournal.repo.EarlyDiscountAmount.PressKeys("{Tab}");
                }
            }
            if (Functions.GoodData(transRecord.message))
            {
                SalesJournal.repo.Comments.TextValue = transRecord.message;
            }
            if(transRecord.GetType() == typeof(SALES_INVOICE))	// set tracking
            {
                if (Functions.GoodData ((transRecord as SALES_INVOICE).shippedBy) || Functions.GoodData ((transRecord as SALES_INVOICE).trackingNumber))
                {
                	SalesJournal.repo.ToolBar.TrackShipments.Click();
                    if (Functions.GoodData ((transRecord as SALES_INVOICE).shippedBy))
                    {
                        TrackShipments.repo.Shipper.Select((transRecord as SALES_INVOICE).shippedBy);
                       
                    }
                    if (Functions.GoodData ((transRecord as SALES_INVOICE).trackingNumber))
                    {
                        TrackShipments.repo.TrackingNumber.TextValue = (transRecord as SALES_INVOICE).trackingNumber;
                        TrackShipments.repo.TrackingNumber.PressKeys("{Tab}");
                    }
                    TrackShipments.repo.OK.Click();
                }
            }

            if ((bRecur && !bSave))	// store recurring
            {
//                // come back
//                SalesJournal.repo.Window.TypeKeys ("<Ctrl+t>");
//                StoreRecurringDialogDotNet.repo._SA_DoStoreRecurring(transRecord.recurringName, transRecord.recurringFrequency);
//                // discard the transaction
//                SalesJournal.repo.ClickUndoChanges();
            }
            if (bSave)
            {
                SalesJournal.repo.Post.Click();

// come back                
//                if (Variables.bUseDataFiles)	// only handle the messages when using external data (i.e. audit)
//                {
//                    SimplyMessage.repo._SA_HandleMessage(SimplyMessage.YES_LOC, SimplyMessage._MSG_AREYOUSUREYOUWANTTOREMOVEORREVERSE_LOC, false, true);
//                    SimplyMessage.repo._SA_HandleMessage(SimplyMessage.OK_LOC, SimplyMessage._MSG_ORDERHASBEENFILLEDANDREMOVED_LOC, false, true);
//                    SimplyMessage.repo._SA_HandleMessage(SimplyMessage.NO_LOC, SimplyMessage._MSG_THEINVOICENUMBERYOUENTEREDISGREATER_LOC, false, true);
//                    SimplyMessage.repo._SA_HandleMessage(SimplyMessage.YES_LOC, SimplyMessage._MSG_YOUAREABOUTTOCHANGETHEQUOTEINTOANORDER_LOC, false, true);
//                    SimplyMessage.repo._SA_HandleMessage(SimplyMessage.YES_LOC, SimplyMessage._MSG_THERECURRINGTRANSACTIONHASBEENCHANGED_LOC, false, true);
//                }
            }      		
            			
		}
		
		
		
		public static SALES _SA_Read()
		{
			return _SA_Read(null);
		}
		
		public static SALES _SA_Read(SALES transRecord)
		{
			return _SA_Read(transRecord, false);
		}
		
		public static SALES _SA_Read(SALES transRecord, bool bOneTime)
		{
			SALES sales;
			
			if(Functions.GoodData(transRecord))
			{
				SalesJournal._SA_Open(transRecord, false, bOneTime);
			}
			
			string transType = SalesJournal.repo.TransTypeDropDown.Text;
			
			
			if (transType.ToLower().Contains("invoice"))
            {
                sales = new SALES_INVOICE();
            }
            else if (transType.ToLower().Contains("order"))
            {
                sales = new SALES_ORDER();
            }
            else if (transType.ToLower().Contains("quote"))
            {
                sales = new SALES_QUOTE();
            }
            else
            {
                sales = new SALES_INVOICE();
                //Functions.Verify(false, true, "Valid value from transaction list");
            }
			
            if (sales.GetType() != typeof(SALES_QUOTE))	// Set Paid by and chequeNumbers
            {
                ((SALES_COMMON_ORDER_INVOICE)sales).paidBy = SalesJournal.repo.PaidBy.Text;
                
                if(SalesJournal.repo.DepositToInfo.Exists())
                {
                    ((SALES_COMMON_ORDER_INVOICE)sales).DepositAccount.acctNumber = SalesJournal.repo.DepositTo.Text;
                }
                
                if(SalesJournal.repo.ChequeNumberInfo.Exists())
                {
                    ((SALES_COMMON_ORDER_INVOICE)sales).chequeNumber = SalesJournal.repo.ChequeNumber.TextValue;
                }
                if(SalesJournal.repo.DepositRefNumberInfo.Exists())
                {
                    ((SALES_ORDER)sales).depositRefNumber = SalesJournal.repo.DepositRefNumber.TextValue;
                }
                if(SalesJournal.repo.DepositAppliedInfo.Exists())
                {
                    ((SALES_COMMON_ORDER_INVOICE)sales).depositApplied = SalesJournal.repo.DepositApplied.TextValue;
                }
            }

            if(sales.GetType() == typeof(SALES_INVOICE))	// set style for invoice, transaction number,  & Quote Number if present
            {
                {
                    sales.transNumber = SalesJournal.repo.InvoiceNumber.TextValue;
                    if (SalesJournal.repo.OrderQuoteNoInfo.Exists())	// field not exist for one-time customers
                    {
                        ((SALES_INVOICE)sales).quoteOrderTransNumber = SalesJournal.repo.OrderQuoteNo.Text;
                    }
                }
            }
            else	// set transaction number
            {
                sales.transNumber = SalesJournal.repo.OrderQuoteNo.Text;
            }
			
            sales.Customer.name = SalesJournal.repo.CustomerName.Text;	// the customer field is disabled on Quote/Order window in adjust mode
            sales.shipDate = SalesJournal.repo.ShipDate.TextValue;
            sales.transDate = SalesJournal.repo.InvoiceDate.TextValue;
            sales.SalesPerson.name = SalesJournal.repo.SoldBy.Text;
            if (SalesJournal.repo.ExchangeRateInfo.Exists())
            {
                sales.exchangeRate = SalesJournal.repo.ExchangeRate.TextValue;
            }
            if (SalesJournal.repo.ItemsStoredAtInfo.Exists())
            {
                sales.shipFrom = SalesJournal.repo.ItemsStoredAt.Text;
                //Agent.SetOption (OPT_VERIFY_ENABLED, TRUE)
            }
            if(SalesJournal.repo.ProjectInfo.Exists())	// only avail in Ent DB. Since we can only check for flavor but not the version of DB, have to check on the object directly
            {         
                ((SALES_COMMON_ORDER_INVOICE)sales).project = SalesJournal.repo.Project.Text;          
            }
			
			// start of grid here
			List<List<string>> contents = SalesJournal.repo.TransContainer.GetContents();
						
			SALES_TABLE st = new SALES_TABLE();
			InitializeTable(st);
			
			List<ROW> lR = new List<ROW>() {};
						
			foreach (List<string> currentRow in contents)	
			{				
				// if not blank then
				// right now looks for a blank string where it's one space - may change this later
				if(currentRow[st.iItem] == " ")
				{
					// do not add
				}
				else
				{							
					ROW r = new ROW();
					
					r.Item.invOrServNumber = ConvertFunctions.CommaToText(currentRow[st.iItem]);
					r.quantityShipped = ConvertFunctions.CommaToText(currentRow[st.iQuantity]);
					r.quantityOrdered = ConvertFunctions.CommaToText(currentRow[st.iOrder]);
					r.quantityBackordered = ConvertFunctions.CommaToText(currentRow[st.iBackOrder]);
					r.unit = ConvertFunctions.CommaToText(currentRow[st.iUnit]);
					r.description = ConvertFunctions.CommaToText(currentRow[st.iDescription]);
					if(Functions.GoodData(st.iBasePrice))
					{
						r.basePrice = ConvertFunctions.CommaToText(currentRow[st.iBasePrice]);
					}
					if(Functions.GoodData(st.iDiscount))
					{
						r.discount = ConvertFunctions.CommaToText(currentRow[st.iDiscount]);						
					}
					r.price = ConvertFunctions.CommaToText(currentRow[st.iPrice]);					
					r.amount = ConvertFunctions.CommaToText(currentRow[st.iAmount]);
					r.TaxCode.code = ConvertFunctions.CommaToText(currentRow[st.iTax]);
					r.Account.acctNumber = ConvertFunctions.CommaToText(currentRow[st.iAccount]);
										
					lR.Add(r);			
				}
			}
			
			sales.GridRows = lR;
												
			// read project allocation data
			if (Functions.GoodData (sales.GridRows))	
			{
				for (int x = 0; x < sales.GridRows.Count; x++)
				{
					if (Functions.GoodData(sales.GridRows[x].amount))	// proceed only if amount is available
					{
						SalesJournal.repo.TransContainer.SelectCell("Account", x);
						SalesJournal.repo.Self.PressKeys("{Ctrl Shift}a");
						
						if (ProjectAllocationDialog.repo.SelfInfo.Exists())
						{							
							sales.GridRows[x].Projects = ProjectAllocationDialog._SA_GetProjectAllocationDetails();
							
							ProjectAllocationDialog.repo.Cancel.Click();
						}
					}
				}
			}
												
			sales.freightAmount = SalesJournal.repo.FreightAmount.TextValue;
			sales.FreightTaxCode.code = SalesJournal.repo.FreightCode.TextValue;
			// get freight tax amount
			if(SalesJournal.repo.FreightTaxTotalInfo.Exists())	// db has more than 2 taxes defined
			{
				sales.freightTaxTotal = SalesJournal.repo.FreightTaxTotal.TextValue;
			}
			else	// only two taxes and both shown
			{
				sales.freightTax1 = SalesJournal.repo.FreightTax1.TextValue;
				// will not show if only one tax is shown
				if(SalesJournal.repo.FreightTax2Info.Exists())
				{
					sales.freightTax2 = SalesJournal.repo.FreightTax2.TextValue;
				}
			}
				
			if(SalesJournal.repo.TermsPercentInfo.Exists())
			{
				sales.termsPercent = SalesJournal.repo.TermsPercent.TextValue;
				sales.termsDays = SalesJournal.repo.TermsDay.TextValue;
				sales.termsNetDays = SalesJournal.repo.TermsNetDays.TextValue;
			}
			if(SalesJournal.repo.EarlyDiscountPercentInfo.Exists())  // invoice only fields
			{
				((SALES_INVOICE)sales).earlyPaymentDiscountPercent = SalesJournal.repo.EarlyDiscountPercent.TextValue;
				((SALES_INVOICE)sales).earlyPaymentDiscount = SalesJournal.repo.EarlyDiscountAmount.TextValue;
			}
			sales.message = SalesJournal.repo.Comments.TextValue;
			if(sales.GetType() == typeof(SALES_INVOICE))	// get tracking info
			{					
				//SalesJournal.repo.Window.TypeKeys ("<Ctrl+k>");
						
				SalesJournal.repo.ToolBar.TrackShipments.Click();
						
				((SALES_INVOICE)sales).shippedBy = TrackShipments.repo.Shipper.Text;
				((SALES_INVOICE)sales).trackingNumber = TrackShipments.repo.TrackingNumber.TextValue;
				TrackShipments.repo.OK.Click();					
			}			
			
			return sales;
		}
		
		public static void _SA_PrintToFile(string sFileName)
		{
			SalesJournal.repo.Self.Activate();
			// Click to print, press keys clicking save instead..
			// SalesJournal.repo.Self.PressKeys("{ControlKey down}{p}{ControlKey up}");
			SalesJournal.repo.ToolBar.Print.Click();
			
			// Handle company logo message
			if ((SimplyMessage.repo.SelfInfo.Exists()) && SimplyMessage.repo.Self.Visible)
			{
				if (SimplyMessage.repo.MessageText.TextValue.Contains(SimplyMessage.sInvoiceLogoMsg))
				{
					SimplyMessage.repo.No.Click();
				}
			}
			// At the moment us pdfCreator with auto-save location feature
			
			// Handle company logo message
//			if (CommonObjects.repoSaveTheFileAs.SelfInfo.Exists())
//			{
//				CommonObjects._SaveFileAs(sFileName);
//			}
//			else 
//			{
//				if ((SimplyMessage.repo.SelfInfo.Exists()))
//				{
//					if (SimplyMessage.repo.MessageText.TextValue.Contains(SimplyMessage.sInvoiceLogoMsg))
//					{
//						SimplyMessage.repo.No.Click();
//					}
//				}
				
				// Save as dialog
				// CommonObjects._SaveFileAs(sFileName);				
//			 }									
		}
		

		
		public static void UndoChanges()
		{
			SalesJournal.UndoChanges(false);
		}
		public static void UndoChanges(bool bWaitForMsg)			
		{
			SalesJournal.repo.Self.Activate();			
			// SalesJournal.repo.Self.PressKeys("{ControlKey down}{z}{ControlKey up}");
			SalesJournal.repo.ToolBar.UndoTransaction.Click();
			
			if (bWaitForMsg)
			{
				int x = 0;
				while (!SimplyMessage.repo.SelfInfo.Exists())
				{
					x++;
					Thread.Sleep(1000);
					if (x > 20)
					{
						Console.WriteLine("Did not find message.");
						break;
					}
				}
			}
			
			while (!SimplyMessage.repo.SelfInfo.Exists())
			{
				Thread.Sleep(500);			
			}
			
			// This message will always appear
			SimplyMessage._SA_HandleMessage(SimplyMessage.repo.Yes, SimplyMessage.sDiscardJournalMsg);
			
		}
		
		public static void _SA_Close()
		{
			System.Threading.Thread.Sleep(2000);
			repo.Self.Close();
		}
		
		
		
//		public static void _SA_SetTransType(SALES trans)
//		{
//			if(!SalesJournal.repo.SelfInfo.Exists())
//			{
//				SalesJournal._SA_Invoke();
//			}
//			
//			// have to click drop down box
//			SalesJournal.repo.TransTypeButton.Click();
//			
//			int index = 0;
//        	        	
//        	IList<ListItem> lTemp = SalesJournal.repo.TransTypeCombo.Items;
//        	
//        	foreach(ListItem currentItem in lTemp)
//        	{
//        		if(currentItem.Text == transType)
//        		{
//        			break;
//        		}
//        		index++;
//        	}
//        	        	
//        	SalesJournal.repo.TransTypeCombo.Items[index].Click();    	
        	
//		}
		
		
		public static void _SA_printTransaction(string sFileName)
		{
	
		}
		
		public static void _SA_RecallRecurring(SALES transRecord)
        {
            // recall and post a recurring entry
            if (Functions.GoodData(transRecord.recurringName))
            {
                Ranorex.Report.Info(String.Format("Recalling the recurring entry {0}", transRecord.recurringName));

                if (!SalesJournal.repo.SelfInfo.Exists())
                {
                    SalesJournal._SA_Invoke();
                }

                SalesJournal.repo.ToolBar.RecallRecurring.Click();
                RecallRecurring._SA_SelectEntryToRecall(transRecord.recurringName);
                SalesJournal._SA_Create(transRecord);
            }
            else	// log the error
            {
                Functions.Verify(false, true, "recurring name found");
            }
        }
		
		
		
		public static void DataFile_ReadFile(string sDataLocation, string fileCounter)
        {					
			string dataLine;	// Stores the current field data from file
            string dataPath;	// The name and path of the data file
	
            StreamReader FileHDR;	// File handle for header info
            StreamReader FileDT1;	// File handle for the transaction details Info
            StreamReader FilePRJ;	// File handle for the project allocations
            StreamReader FileSRL;	// File handle for the serial numbers
			
            // Get the data path from file
            dataPath = sDataLocation + "SO" + fileCounter;
			
            List<SALES>  lSales = new List<SALES>() {};
            // Only open header file
            FileHDR = new StreamReader(Functions.FileOpen (dataPath  + EXTENSION_HEADER, "FM_READ"));
            while ((dataLine = FileHDR.ReadLine()) != null)
            {
                SALES ST = null;
                ST = SalesJournal.DataFile_setDataStructure(EXTENSION_HEADER, dataLine, ST);
				
                // Only open transaction detail file if it exists
                if (File.Exists (dataPath  + EXTENSION_TRANS_DETAILS))
                {
                    using (FileDT1 = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_TRANS_DETAILS, "FM_READ")))
                    {
                        while ((dataLine = FileDT1.ReadLine()) != null)
                        {
                            if (dataLine != "")
                            {
                                ST = SalesJournal.DataFile_setDataStructure(EXTENSION_TRANS_DETAILS, dataLine, ST);
                            }
                        }
                    }                   
                }
				
                // Only open project file if it exists
                if (File.Exists (dataPath  + EXTENSION_PROJECT))
                {
                    using (FilePRJ = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_PROJECT, "FM_READ")))
                    {
                        while ((dataLine = FilePRJ.ReadLine()) !=  null)
                        {
                            ST = SalesJournal.DataFile_setDataStructure(EXTENSION_PROJECT, dataLine, ST);
                        }
                    }                    
                }
				
                // Only open serial file if it exists
                if (File.Exists (dataPath  + EXTENSION_SERIAL))
                {
                    using (FileSRL = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_SERIAL, "FM_READ")))
                    {
                        while ((dataLine = FileSRL.ReadLine()) != null)
                        {
                            ST = SalesJournal.DataFile_setDataStructure(EXTENSION_SERIAL, dataLine, ST);
                        }
                    }                    
                }
                lSales.Add(ST);
            }
            FileHDR.Close();
						            
            for (int x = 0; x < lSales.Count; x++)
            {
                SALES STrans = lSales[x];
                    
                // Determine the type
                switch (STrans.action)
                {
                	case "CREATE":
                    	SalesJournal._SA_Create (STrans);
                        break;
                    case "ADJUST":
                        SalesJournal._SA_Create (STrans, true, true);
                        break;
                    case "STORE_RECURRING":
                        SalesJournal._SA_Create (STrans, false, false, true);
                        break;
                    case "RECALL_RECURRING":
                        _SA_RecallRecurring(STrans);
                        break;
                    case "PRINT":
                        SalesJournal._SA_Open (STrans);
                        SalesJournal.repo.ToolBar.Adjust.Click();
                        SalesJournal._SA_printTransaction (Variables.PrintLocation + "SO.mdi");
                        break;
                    default:
                    {
                    	Functions.Verify(false, true, "Valid value for action");
                        break;
                    }
                }
            }
			
        }

        public static SALES DataFile_setDataStructure(string extension,string dataLine,SALES SalesTrans)
        {
            //this function assumes that the HDR lines will  be sent first to get the type of transaction established
            string sType = null;  // assigned later
            SALES T = null;     // assigned later

            //int x;
            bool bFound;
            if (Functions.GoodData (SalesTrans))
            {
                //we already know what kind of transaction it is
                T = SalesTrans;
            }
            else
            {
                switch (extension.ToUpper())
                {
                    case EXTENSION_HEADER:
                        sType  = Functions.GetField (dataLine, ",", 1);
                        break;
                    default:
                    {
                        Functions.Verify(false, true, "Set header line first");
                        break;
                    }
                }
                switch (sType)
                {
                    case "Q":                        
                        T = new SALES_QUOTE();
                        break;
                    case "O" :                        
                        T = new SALES_ORDER();
                        break;
                    case "I" :                        
                        T = new SALES_INVOICE();
                        break;
                }
            }
			
            ITEM ITemp;
            GL_ACCOUNT GLTemp;
            switch (extension.ToUpper())
            {
                case EXTENSION_HEADER:
                    CUSTOMER Cust = new CUSTOMER();
                    Cust.name = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 4));
                    T.Customer = Cust;
                    if  (T.GetType() != typeof(SALES_QUOTE))
                    {
                        string spaidBy = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 2));                                
                        switch(spaidBy)
                        {
                            case "PL":
                            case null:	// In data file for orders, it's left as blank
                                ((SALES_COMMON_ORDER_INVOICE)T).paidBy =  "Pay Later";
                                break;
                            case "CA":
                                ((SALES_COMMON_ORDER_INVOICE)T).paidBy = "Cash";
                                break;
                            case "CH":	 // CDN
                                ((SALES_COMMON_ORDER_INVOICE)T).paidBy = "Cheque";
                                break;
                            case "CHECK":// US
                                ((SALES_COMMON_ORDER_INVOICE)T).paidBy = "Check";
                                break;
                            default:
                            {
                                ((SALES_COMMON_ORDER_INVOICE)T).paidBy = spaidBy;
                                break;
                            }
                        }
                        if ((((SALES_COMMON_ORDER_INVOICE)T).paidBy == "Cash" || ((SALES_COMMON_ORDER_INVOICE)T).paidBy == "Cheque" || ((SALES_COMMON_ORDER_INVOICE)T).paidBy == "Check"))	// set bank account/cheque number if paid by cash/cheque
                        {
                            GLTemp = new GL_ACCOUNT();
                            GLTemp.acctNumber = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 3));
                            ((SALES_COMMON_ORDER_INVOICE)T).DepositAccount = GLTemp;
                            ((SALES_COMMON_ORDER_INVOICE)T).chequeNumber = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 16));
                        }
                    }
                    if (T.GetType() == typeof(SALES_INVOICE))
                    {
                        if ((((SALES_INVOICE)T).paidBy != "'" && ((SALES_INVOICE)T).paidBy != "Pay Later"))	// set the early payment fields if paid by cash/chque/credit card
                        {
                            ((SALES_INVOICE)T).earlyPaymentDiscountPercent = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 25));
                        }
						
                        if (Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 15)) != null)	// if there is a good value in here, then we need a quote or order number (for converting to invoice)
                        {
                            ((SALES_INVOICE)T).quoteOrderTransNumber = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 15));
                        }
                        T.transNumber = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 17));
                        ((SALES_INVOICE)T).earlyPaymentDiscountPercent = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 25));
                        ((SALES_INVOICE)T).shippedBy = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 28));
                        ((SALES_INVOICE)T).trackingNumber = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 29));
                    }
                    else	// for quotes or orders
                    {
                        T.transNumber = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 15));
                    }
                    T.transDate = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 19));
                    T.shipDate = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 18));
					
                    ADDRESS A1 = new ADDRESS();
                    A1.street1 = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 5));
                    A1.street2 = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 6));
                    A1.city = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 7));
                    A1.postalCode = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 8));
                    T.Address = A1;
					
                    A1 = new ADDRESS();
                    A1.contact = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 9));
                    A1.street1 =  Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",",10));
                    A1.street2 = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",",11));
                    A1.city = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 12));
                    A1.province = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 13));
                    A1.postalCode = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 14));
                    T.ShipToAddress = A1;
					
					
                    T.freightAmount = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 20));
                    TAX_CODE TC = new TAX_CODE();
                    TC.code = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 21));
                    T.FreightTaxCode = TC;
                    if(T.FreightTaxCode.code == Variables.sNoTax)	// should be tax description not tax code
                    {
                        T.FreightTaxCode.description = Variables.sNoTax;
                        T.FreightTaxCode.code = null;
                    }
                    T.termsPercent = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 22));
                    T.termsDays = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 23));
                    T.termsNetDays = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 24));
                    T.exchangeRate = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 26));
                    T.shipFrom = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 33));
                    T.recurringName = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 31));
                    T.recurringFrequency = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 32));
                    T.message = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 35));
                    EMPLOYEE Emp = new EMPLOYEE();
                    Emp.name = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 27));
                    T.action = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 30));
                    break;
                case EXTENSION_TRANS_DETAILS:                           
                    switch (T.GetType().Name)
                    {
                        case "SALES_QUOTE":
                            sType =  "Q";
                            break;
                        case "SALES_ORDER":
                            sType =  "O";
                            break;
                        case "SALES_INVOICE":                        
                            sType =  "I";
                            break;
                        default:
                            {
                                Functions.Verify(false, true, "Valid data type for a sales entry: " + T.GetType().Name);
                                break;
                            }
                    }
					
                    if ((Functions.GetField (dataLine, ",", 1)  == sType) && (Functions.GetField (dataLine, ",", 2) == T.Customer.name) && 
                        (Functions.GetField (dataLine, ",", 3) == T.transNumber))
                    {
                        ROW R = new ROW();
                        ITemp = new ITEM();
                        ITemp.invOrServNumber = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 4));
                        R.Item = ITemp;
                        R.quantityShipped = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 5));
                        R.quantityOrdered = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 6));
                        R.quantityBackordered = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 7));
                        R.unit = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 8));
                        R.description = Functions.GetField (dataLine, ",", 9);
                        R.price = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 10));
                        R.amount = ConvertFunctions.TextToComma(Functions.GetField(dataLine, ",", 11));
                        TAX_CODE TX = new TAX_CODE();
                        TX.code = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 12));
                        R.TaxCode = TX;
                        if(R.TaxCode.code == Variables.sNoTax)	// should be tax description not tax code
                        {
                            R.TaxCode.description = Variables.sNoTax;
                            R.TaxCode.code = null;
                        }
                        GLTemp = new GL_ACCOUNT();
                        GLTemp.acctNumber = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 13));
                        R.Account = GLTemp;
                        List<ROW>  lRows = T.GridRows;	// have to do this nonsense as silk doesn't like the append to the T list
                        lRows.Add(R);
                        T.GridRows = lRows;
                    }
                    break;
                case EXTENSION_PROJECT:                            
                    switch (T.GetType().Name)
                    {
                        case "SALES_QUOTE":
                            sType =  "Q";
                            break;
                        case "SALES_ORDER":
                            sType =  "O";
                            break;
                        case "SALES_INVOICE":
                            sType =  "I";
                            break;
                        default:
                            {
                                Functions.Verify(false, true, "Valid data type for a sales entry: " + T.GetType().Name);
                                break;
                            }
                    }
					
                    if ((Functions.GetField (dataLine, ",", 1)  == sType) && (Functions.GetField (dataLine, ",", 2) == T.Customer.name) && 
                        (Functions.GetField (dataLine, ",", 3) == T.transNumber))
                    {                        
                        PROJECT_ALLOCATION PA = new PROJECT_ALLOCATION();
                        PROJECT P = new PROJECT();
                        P.name = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 5));
                        PA.Project =  P;
                        PA.Amount = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 6));
                        PA.Percent = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 7));
                        bFound = false;
                        int x;  // to be used later outside the loop
                        for (x = 0; x < T.GridRows.Count; x++)
                        {
                            if (T.GridRows[x].Item.invOrServNumber == Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 4)))	// look for the item in the list of rows
                            {
                                bFound = true;
                                break;
                            }
                        }
                        if (bFound)	// you found the line, so now add the project allocation
                        {
                            List<ROW>  lR = T.GridRows;	// have to do this nonsense as silk doesn't like the append to the T list
                            lR[x].Projects.Add(PA);
                            T.GridRows = lR;
                        }
                        else
                        {
                            Functions.Verify(false, true, "Row found for project allocation");
                        }
                    }
                    break;
                case EXTENSION_SERIAL:                            
                    switch (T.GetType().Name)
                    {
                        case "SALES_QUOTE":
                            sType =  "Q";
                            break;
                        case "SALES_ORDER":
                            sType =  "O";
                            break;
                        case "SALES_INVOICE":
                            sType =  "I";
                            break;
                        default:
                            {
                                Functions.Verify(false, true, "Valid data type for a sales entry: " + T.GetType().Name);
                                break;
                            }
                    }
					
                    if ((Functions.GetField (dataLine, ",", 1)  == sType) && (Functions.GetField (dataLine, ",", 2) == T.Customer.name) 
                        && (Functions.GetField (dataLine, ",", 3) == T.transNumber))
                    {
                        List<TRANS_SERIAL>  lTS;
                        TRANS_SERIAL TS = new TRANS_SERIAL();
                        ITemp = new ITEM();
                        ITemp.invOrServNumber = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 4));
                        TS.Item = ITemp;
                        TS.SerialNumbersToUse.Add(Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 5)));
                        bFound = false;
                        if (Functions.GoodData (((SALES_INVOICE)T).SerialNumberDetails))	// if there is already serial details setup, see if you need to add to an existing line or create a new line
                        {
                            lTS = ((SALES_INVOICE)T).SerialNumberDetails;	// have to do this as Silk doesn't like the list append on the T list

                            for (int x = 0; x < ((SALES_INVOICE)T).SerialNumberDetails.Count; x++)
                            {
                                if (lTS[x].Item.invOrServNumber == TS.Item.invOrServNumber)	// if we find the item, it is already in the list...just add the serial numbers
                                {
									
                                    lTS[x].SerialNumbersToUse.AddRange(TS.SerialNumbersToUse);
                                    ((SALES_INVOICE)T).SerialNumberDetails = lTS;
                                    bFound = true;
                                    break;
                                }
                            }
                            if (!(bFound))	// if we didn't find the item in the current list, then we need to add this row
                            {
                                lTS.Add(TS);
                                ((SALES_INVOICE)T).SerialNumberDetails = lTS;
                            }
                        }
                        else
                        {
                            ((SALES_INVOICE)T).SerialNumberDetails = new List<TRANS_SERIAL>(){TS};
                        }
                    }
                    break;
                default:
                {
                    Functions.Verify(false, true, "Correct file types sent");
                    break;
                }
            }
			
            return (T);
        }


		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		private static void InitializeTable(SALES_TABLE STable)
        {            
            STable.iItem = 0;
            STable.iQuantity = 1;
            STable.iOrder = 2;
            STable.iBackOrder = 3;
            STable.iUnit = 4;
            STable.iDescription = 5;

            //SalesJournal.repo.Self.PressKeys("{LMenu down}{Vkey}{LMenu up}");
            //SalesJournal.repo.Self.PressKeys("w");	// restore to default
            SalesJournal.repo.View.Click();
            SalesJournal.repo.RestoreWindow.Click();

            // the rest columns depends on if the line item discount is turned on or off
            if (SalesJournal.repo.TransContainer.ColumnCount() == 13)	// has line item discount columns
            {
                STable.iBasePrice = 6;
                STable.iDiscount = 7;
                STable.iPrice = 8;
                STable.iAmount = 9;
                STable.iTax = 10;
                STable.iAccount = 11;
            }
            else
            {
                STable.iPrice = 6;
                STable.iAmount = 7;
                STable.iTax = 8;
                STable.iAccount = 9;
            }
        }
	}
	
	// Child windows and dialogs
	public static class SelectInventory
	{
		public static SalesJournalResFolders.SelectInventoryAppFolder repo = SalesJournalRes.Instance.SelectInventory;
	}
	
	public static class TrackShipments
	{
		public static SalesJournalResFolders.TrackShipmentsAppFolder repo = SalesJournalRes.Instance.TrackShipments;
	}
	
	public static class AddOnTheFly
	{
		public static SalesJournalResFolders.AddOnTheFlyAppFolder repo = SalesJournalRes.Instance.AddOnTheFly;
	}		
	
	public static class RecallRecurring
	{
		public static SalesJournalResFolders.RecallRecurringAppFolder repo = SalesJournalRes.Instance.RecallRecurring;
		
		
		public static void _SA_SelectEntryToRecall(string name)
		{
			// selects a recurring entry to recall and handle the discard message if presents
			
			RecallRecurring.repo.Self.Activate();
			RecallRecurring.repo.ListOfRecurringEntries.SelectListItem(name);
			RecallRecurring.repo.Select.Click();
			
			//come back
			//SimplyMessage.Instance._SA_HandleMessage(SimplyMessage.YES_LOC, SimplyMessage._MSG_AREYOUSUREYOUWANTTODISCARD_LOC, false, true);
		}		
	}
	
	public static class SelectPrice
	{
		public static SalesJournalResFolders.SelectPriceAppFolder repo = SalesJournalRes.Instance.SelectPrice;
		
		public static void _Cancel()
		{
			SelectPrice.repo.Self.Activate();
			SelectPrice.repo.Cancel.Click();
		}		
	}
	
	public static class SelectAnInvoice
	{
		public static SalesJournalResFolders.SelectAnInvoiceAppFolder repo = SalesJournalRes.Instance.SelectAnInvoice;
		
	}
	
	
}