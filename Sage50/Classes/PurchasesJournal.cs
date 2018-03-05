/*
 * Created by Ranorex
 * User: wonda05
 * Date: 10/18/2016
 * Time: 10:41 AM
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

namespace Sage50.Classes
{
	/// <summary>
	/// Description of PurchasesJournal.
	/// </summary>
	public class PurchasesJournal
	{
        private const string sInvoice = "Invoice";
        private const string sOrder = "Order";
        private const string sQuote = "Quote";

        public static PurchasesJournalResFolders.PurchasesJournalAppFolder repo = PurchasesJournalRes.Instance.PurchasesJournal;


		public static void _SA_Invoke()
		{
            if (Simply.isEnhancedView())
            {
                Simply.repo.Self.Activate();
                Simply.repo.PayablesLink.Click();
                Simply.repo.PurchaseIcon.Click();
            }
		}

        public static void _SA_Open(PURCHASE TransRecord)
        {
            _SA_Open(TransRecord, true, false);
        }

        public static void _SA_Open(PURCHASE TransRecord, bool bAdjustMode)
        {
            _SA_Open(TransRecord, bAdjustMode, false);
        }

        public static void _SA_Open(PURCHASE TransRecord, bool bAdjustMode, bool bOnetimeVndr) //  can send this a Quote, Order, or Invoice
        {
            // load purchase quote, order or invoice in lookup or adjustment mod
            // bAdjustMode - flag to indicate whether to load journal entry in adjustment or lookup mode. needed for order/quote, which cannot be reversed in adjustment mode
            // bOnetimeVndr - flag to indicate wheter it's one-time vendor. needed as cannot search by vendor name field for one-time vendors
            
            if (!PurchasesJournal.repo.SelfInfo.Exists())
            {
                PurchasesJournal._SA_Invoke();
            }

            if (TransRecord.GetType() == typeof(PURCHASE_INVOICE))
            {                
                PurchasesJournal.repo.TransTypeDropDown.Select(sInvoice);

                if (bAdjustMode)
                {
                    PurchasesJournal.repo.ToolBar.Adjust.Click();
                }
                else	// lookup
                {
                    PurchasesJournal.repo.ToolBar.LookUp.Click();	// bring up the Search window in lookup mode
                }

                // wait for search window                
                if (!DotNetJournalSearch.repo.SelfInfo.Exists())
                {
                    Functions.Verify(false, true, "Purchase Journal Search window found");
                }
                
                DotNetJournalSearch._SA_SelectLookupDateRange();
                if (!bOnetimeVndr)	// search on a particular vendor
                {
                    DotNetJournalSearch.repo.Name.Select(TransRecord.Vendor.name);
                }
                else	// search for all vendors
                {
                    DotNetJournalSearch.repo.Name.Select("<Search All Vendors>");
                }
                DotNetJournalSearch.repo.InvoiceNumber.TextValue = TransRecord.transNumber;
                //System.Threading.Thread.Sleep(Functions.RandInt(2, 4));	// need during multi user mode, sometimes doesn't respond if all machines press ok at same time
                DotNetJournalSearch.repo.OK.Click();

                //SimplyMessage._SA_HandleMessage(SimplyMessage.OK, SimplyMessage._MSG_PLACEHOLDERTOFIXEXISTINGMETHODCALLS_LOC);
            }
            else
            {
                if (TransRecord.GetType() == typeof(PURCHASE_ORDER))
                {
                    PurchasesJournal.repo.TransTypeDropDown.Select(sOrder);
                }
                else	// purchase quote
                {
                    PurchasesJournal.repo.TransTypeDropDown.Select(sQuote);
                }
                PurchasesJournal.repo.OrderQuoteNo.Select(TransRecord.transNumber);
                PurchasesJournal.repo.Self.PressKeys("{Tab}"); // tab out to load the transaction
                if (bAdjustMode)	// switch to adjust mode
                {
                    PurchasesJournal.repo.ToolBar.Adjust.Click();
                }
            }
        }

        public static void _SA_MatchDefaults(PURCHASE TransRecord)
        {
            if ((!Functions.GoodData(TransRecord.shipToLocation)) && (Variables.globalSettings.InventorySettings.UseMultipleLocations == true))	// default to the first location
            {
                TransRecord.shipToLocation = Variables.globalSettings.InventorySettings.Locations[0].code;
            }
            if (Functions.GoodData(TransRecord.GridRows))	// set the default account for each grid row
            {
                for (int i = 0; i < TransRecord.GridRows.Count; i++)
                {
                    if (Functions.GoodData(TransRecord.GridRows[i]))	// if it's not a blank row
                    {
                        if (!Functions.GoodData(TransRecord.GridRows[i].Account.acctNumber))	// set account for each defail line
                        {
                            // default to the asset account if specified in the item record
                            if (Functions.GoodData(TransRecord.GridRows[i].Item) && Functions.GoodData(TransRecord.GridRows[0].Item.assetAccount.acctNumber))
                            {
                                TransRecord.GridRows[i].Account = TransRecord.GridRows[i].Item.assetAccount;
                            }
                            // or default to the expense account if specified in the vendor record
                            else if (Functions.GoodData(TransRecord.Vendor) && Functions.GoodData(TransRecord.Vendor.expenseAccount) && Functions.GoodData(TransRecord.Vendor.expenseAccount.acctNumber))
                            {
                                TransRecord.GridRows[i].Account = TransRecord.Vendor.expenseAccount;
                            }
                        }
                        if (!Functions.GoodData(TransRecord.GridRows[i].TaxCode.code) && Functions.GoodData(TransRecord.Vendor.taxCode))	// set tax code to the vendor's default
                        {
                            TransRecord.GridRows[i].TaxCode = TransRecord.Vendor.taxCode;
                        }
                    }
                }
            }
        }


        public static PURCHASE _SA_Read()
        {
            return _SA_Read(null);
        }

        public static PURCHASE _SA_Read(PURCHASE TransRecord)
        {
            return _SA_Read(TransRecord, false);
        }

        public static PURCHASE _SA_Read(PURCHASE TransRecord, bool bOneTime) //  method will read all fields and store the data in a PURCHASE record
        {
            PURCHASE Purch;

            // load it in adjustment mode first
            if (Functions.GoodData(TransRecord))	// load the transaction if specified
            {
                PurchasesJournal._SA_Open(TransRecord, true, bOneTime);                
            }

            // set focus from the settings window back to the journal window
            //PurchasesJournal.Instance.Window.SetActive();
            
            // create the correct object based on the transaction type
            string transType = PurchasesJournal.repo.TransTypeDropDown.SelectedItemText;
            if (transType.ToLower().Contains("invoice"))
            {
                Purch = new PURCHASE_INVOICE();
            }
            else if (transType.ToLower().Contains("order"))
            {
                Purch = new PURCHASE_ORDER();
            }
            else if (transType.ToLower().Contains("quote"))
            {
                Purch = new PURCHASE_QUOTE();
            }
            else
            {
                Purch = new PURCHASE_INVOICE();
                //Functions.Verify(false, true, "Valid value from transaction list");
            }

            if (Purch.GetType() != typeof(PURCHASE_QUOTE))	// i.e. order or invoice
            {
                // Set Paid by and ChequeNumbers
                ((PURCHASE_COMMON_ORDER_INVOICE)Purch).paymentMethod = PurchasesJournal.repo.PaidBy.SelectedItemText;

                if (PurchasesJournal.repo.PaidFromInfo.Exists())
                {
                    {
                        ((PURCHASE_COMMON_ORDER_INVOICE)Purch).PaidFromAccount.acctNumber = PurchasesJournal.repo.PaidFrom.SelectedItemText;
                    }
                }
                if (PurchasesJournal.repo.ChequeNumberInfo.Exists())
                {
                    {
                        ((PURCHASE_COMMON_ORDER_INVOICE)Purch).chequeNumber = PurchasesJournal.repo.ChequeNumber.TextValue;
                    }
                }
            }
            if (Purch.GetType() == typeof(PURCHASE_INVOICE))	// set style for invoice, transaction number,  & Quote Number if present
            {
                {
                    Purch.transNumber = PurchasesJournal.repo.InvoiceNumber.TextValue;

                    if (PurchasesJournal.repo.OrderQuoteNoInfo.Exists())	// doesn't exists for one time
                    {
                        ((PURCHASE_INVOICE)Purch).quoteOrderTransNumber = PurchasesJournal.repo.OrderQuoteNoText.TextValue;
                    }
                }
            }
            else	// set transaction number and shipping date for order/quote
            {
                Purch.transNumber = PurchasesJournal.repo.OrderQuoteNoText.TextValue;
                Purch.shipDate = PurchasesJournal.repo.ShipDate.TextValue;
            }

            Purch.Vendor.name = PurchasesJournal.repo.VendorNameText.TextValue;
            Purch.transDate = PurchasesJournal.repo.InvoiceDate.TextValue;
            if (PurchasesJournal.repo.ExchangeRateInfo.Exists())
            {
                Purch.exchangeRate = PurchasesJournal.repo.ExchangeRate.TextValue;
            }
            if (PurchasesJournal.repo.StoreItemsAtInfo.Exists())
            {
                if (PurchasesJournal.repo.StoreItemsAt.Enabled)
                {
                    Purch.shipToLocation = PurchasesJournal.repo.StoreItemsAt.SelectedItemText;
                }
            }

            // prepare the container
            PURCH_TABLE PT = new PURCH_TABLE();
            // InitializeTable(PT);

            //int iNumOfCols = PurchasesJournal.repo.TransContainer.ColumnCount;
            //List<string[]> containerLine = ConvertFunctions.DataGridItemsToListOfString(PurchasesJournal.Instance.TransContainer.Items, iNumOfCols);
            List<List<string>> Contents = PurchasesJournal.repo.TransContainer.GetContents();

            List<ROW> lR = new List<ROW>() { };

            //for (int x = 0; x < containerLine.Count; x++)
            foreach (List<string> currRow in Contents)
            {
                // it's a blank row if the item number, quantity received, quantity ordered, description, and amount fields are all blank
                if ((currRow[PT.iItem] == "") && (currRow[PT.iQuantity] == "") && (currRow[PT.iOrder] == "") &&
                    (currRow[PT.iDescription] == "") && (currRow[PT.iAmount] == ""))
                {
                    // do not add
                }
                else
                {
                    ROW R = new ROW();

                    // assign recordset
                    R.Item.invOrServNumber = ConvertFunctions.CommaToText(currRow[PT.iItem]);
                    R.quantityReceived = ConvertFunctions.CommaToText(currRow[PT.iQuantity]);
                    R.quantityOrdered = ConvertFunctions.CommaToText(currRow[PT.iOrder]);
                    R.quantityBackordered = ConvertFunctions.CommaToText(currRow[PT.iBackOrder]);
                    R.unit = ConvertFunctions.CommaToText(currRow[PT.iUnit]);
                    //R.Item.invOrServDescription = StrTran(containerLine[5+ iItmColID], "," ,"\comma") // wrong record field
                    R.description = ConvertFunctions.CommaToText(currRow[PT.iDescription]);
                    R.price = ConvertFunctions.CommaToText(currRow[PT.iPrice]);
                    R.TaxCode.code = ConvertFunctions.CommaToText(currRow[PT.iTax]);	// blank when no tax
                    R.amount = ConvertFunctions.CommaToText(currRow[PT.iAmount]);
                    R.Account.acctNumber = ConvertFunctions.CommaToText(currRow[PT.iAccount]);

                    // append to detail list
                    lR.Add(R);
                }

                //// only remove if more than one line is present. NC - no longer needed so commented out
                //if(containerLine.Count >iNumOfCols)
                //{
                //    // remove line already recorded
                //    for ( int i = 0; i < iNumOfCols; i++)
                //    {
                //        containerLine.RemoveAt(1);
                //    }
                //}
                //else
                //{
                //    containerLine.RemoveAt(1);
                //}				
            }

            // add rows to the record
            Purch.GridRows = lR;

            // read project allocation data
            if (Functions.GoodData(Purch.GridRows))
            {
                //PurchasesJournal.Instance.VendorName.SetFocus();    // Set focus within the journal first, or else sometimes cannot select the account field.
                for (int x = 0; x < Purch.GridRows.Count; x++)
                {
                    if (Functions.GoodData(Purch.GridRows[x].amount))	// proceed only if amount is available
                    {
                       	PurchasesJournal.repo.TransContainer.SelectCell("Account", x); // select account field                        
                        PurchasesJournal.repo.TransContainer.PressKeys("{Ctrl Shift}a");
                        
                        if (ProjectAllocationDialog.repo.SelfInfo.Exists())
                        {
                            Purch.GridRows[x].Projects = ProjectAllocationDialog._SA_GetProjectAllocationDetails();   // get dialog content
                            ProjectAllocationDialog.repo.Cancel.Click();
                        }
                    }
                }
            }

            if (PurchasesJournal.repo.PrepayRefNumberInfo.Exists())
            {
                ((PURCHASE_ORDER)Purch).prepayRefNumber = PurchasesJournal.repo.PrepayRefNumber.TextValue;
                ((PURCHASE_ORDER)Purch).prepaymentAmount = PurchasesJournal.repo.PrepaymentAmount.TextValue;
            }
            
            Purch.freightAmount = PurchasesJournal.repo.FreightAmount.TextValue;
            Purch.FreightTaxCode.code = PurchasesJournal.repo.FreightCode.TextValue;
            if (PurchasesJournal.repo.FreightTaxTotalInfo.Exists())	// db has more than 2 taxes defined
            {
                Purch.freightTaxTotal = PurchasesJournal.repo.FreightTaxTotal.TextValue;
            }
            else	// only two taxes and both shown
            {
                Purch.freightTax1 = PurchasesJournal.repo.FreightTax1.TextValue;
                // will not show if only one tax is shown
                if (PurchasesJournal.repo.FreightTax2Info.Exists())
                {
                    Purch.freightTax2 = PurchasesJournal.repo.FreightTax2.TextValue;
                }
            }

            if (PurchasesJournal.repo.TermsPercentInfo.Exists())
            {
                Purch.termsPercent = PurchasesJournal.repo.TermsPercent.TextValue;
                Purch.termsDays = PurchasesJournal.repo.TermsDay.TextValue;
                Purch.termsNetDays = PurchasesJournal.repo.TermsNetDays.TextValue;
            }
            if (PurchasesJournal.repo.EarlyDiscountInfo.Exists())
            {
                ((PURCHASE_INVOICE)Purch).earlyPaymentDiscountPercent = PurchasesJournal.repo.EarlyDiscount.TextValue;
            }
            if (Purch.GetType() == typeof(PURCHASE_INVOICE))	// set tracking
            {
                if (Functions.GoodData(Purch.shippedBy) || Functions.GoodData(Purch.trackingNumber))
                {
                    PurchasesJournal.repo.Self.PressKeys("{Ctrl}k");
                    Purch.shippedBy = TrackShipments.repo.Shipper.SelectedItemText;
                    Purch.trackingNumber = TrackShipments.repo.TrackingNumber.TextValue;
                    TrackShipments.repo.OK.Click();
                }
            }            
            return Purch;
        }

        private static void InitializeTable(PURCH_TABLE PTable)
        {
            // initializes the column indexes and retores container to default
            // item number
//            if (Simply._SA_IsItFlavor(FLAVOR.ENTERPRISE))
//            {
//                // In Enterprise edition, the first column (i.e. index = 0) is Vendor Association though it might be hidden so the item column becomes the 2nd
//                PTable.iItem = 1;
//            }
//            else
//            {
//                PTable.iItem = 0;
//            }

            PTable.iQuantity = 1; //PTable.iItem + 1;
            PTable.iOrder = 2; //PTable.iItem + 2;
            PTable.iBackOrder = 3; //PTable.iItem + 3;
            PTable.iUnit = 4; //PTable.iItem + 4;
            PTable.iDescription = 5; //PTable.iItem + 5;
            PTable.iPrice = 6; //PTable.iItem + 6;
            PTable.iTax = 7; //PTable.iItem + 7;
            PTable.iAmount = 8; //PTable.iItem + 11;
            PTable.iAccount = 12; //PTable.iItem + 12;

            Settings._SA_GetPayablesDutySettings();
            if (Variables.globalSettings.PayableSettings.trackDutyOnImportedItems == true)
            {
                PTable.iDutyPercent = 13; //PTable.iItem + 13;
                PTable.iDutyAmount = 14; //PTable.iItem + 14;
            }

            //PurchasesJournal.repo.Self.SetActive();
            PurchasesJournal.repo.Self.PressKeys("{Alt}v");
            PurchasesJournal.repo.Self.PressKeys("w");	// restore to default
            //PurchasesJournal.Instance.TransContainer.Click(1, 10, 33, true);	// activate the container NC - commented out. don't think this is necessary.
        }

        public static void _SA_Create(PURCHASE TransRecord)
        {
            _SA_Create(TransRecord, true, false, false, false);
        }

        public static void _SA_Create(PURCHASE TransRecord, bool bSave)
        {
            _SA_Create(TransRecord, bSave, false, false, false);
        }

        public static void _SA_Create(PURCHASE TransRecord, bool bSave, bool bEdit)
        {
            _SA_Create(TransRecord, bSave, bEdit, false, false);
        }

        public static void _SA_Create(PURCHASE TransRecord, bool bSave, bool bEdit, bool bRecur)
        {
            _SA_Create(TransRecord, bSave, bEdit, bRecur, false);
        }

        public static void _SA_Create(PURCHASE TransRecord, bool bSave, bool bEdit, bool bRecur, bool bOneTime)
        {
            string sType = "Invoice";	            // the type string to be used in the output message

            if (!Variables.bUseDataFiles)
            {
                PurchasesJournal._SA_MatchDefaults(TransRecord);
            }

            if (!PurchasesJournal.repo.SelfInfo.Exists())
            {
                PurchasesJournal._SA_Invoke();
            }

            // convert from previous switch statements
            if (TransRecord.GetType() == typeof(PURCHASE_QUOTE))
            {

                sType = "Quote";
                if (PurchasesJournal.repo.TransTypeDropDown.Enabled || (PurchasesJournal.repo.TransTypeDropDown.SelectedItemText != "Quote"))
                {
                    PurchasesJournal.repo.TransTypeDropDown.Select(sQuote);
                }
            }
            else if (TransRecord.GetType() == typeof(PURCHASE_ORDER))
            {
                sType = "Order";
                if (PurchasesJournal.repo.TransTypeDropDown.Enabled || (PurchasesJournal.repo.TransTypeDropDown.SelectedItemText != "Order"))
                {
                    PurchasesJournal.repo.TransTypeDropDown.Select(sOrder);
                }
            }
            else if (TransRecord.GetType() == typeof(PURCHASE_INVOICE))
            {
                if (PurchasesJournal.repo.TransTypeDropDown.Enabled || (PurchasesJournal.repo.TransTypeDropDown.SelectedItemText != "Invoice"))
                {
                    PurchasesJournal.repo.TransTypeDropDown.Select(sInvoice);
                }
            }            

            // print statements
            if (!bEdit)
            {
                if (!bRecur)
                {
                    Ranorex.Report.Info(String.Format("Creating Purchase {0} {1} ", sType, TransRecord.transNumber));
                }

            }
            else
            {
                Ranorex.Report.Info(String.Format("Adjusting Purchase {0} {1} ", sType, TransRecord.transNumber));
                PurchasesJournal._SA_Open(TransRecord);	// load the transaction for adjustment
            }

            // set vendor
            if ((!bEdit) || TransRecord.GetType() == typeof(PURCHASE_INVOICE)) // entering new or adjusting invoice
            {
                if (TransRecord.Vendor.name != PurchasesJournal.repo.VendorNameText.TextValue)
                {                    
                    PurchasesJournal.repo.VendorName.Select(TransRecord.Vendor.name);                 
                    //PurchasesJournal.repo.VendorName.PressKeys("{Tab}");	// Must press tab, otherwise selecting the Paid By field may not work
                }
            }

            //  Validate if Add on the fly is present
            if (AddOnTheFly.repo.SelfInfo.Exists())
            {
                //try
                //{
                //    Functions.Verify(true, true, "Add on fly message appears");
                //}
                //catch
                //{
                //    Functions.ExceptPrint();
                //}
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

            if (TransRecord.GetType() == typeof(PURCHASE_INVOICE))  // set invoice number
            {
                if (Functions.GoodData(TransRecord.transNumber))
                {
                    PurchasesJournal.repo.InvoiceNumber.TextValue = TransRecord.transNumber;
					PurchasesJournal.repo.InvoiceNumber.PressKeys("{Tab}");                    
                }
                else
                {
                    Ranorex.Report.Info("Invoice number is missing");
                    Functions.Verify(false, true, "Invoice Number");
                }
                if (Functions.GoodData(((PURCHASE_INVOICE)TransRecord).quoteOrderTransNumber) && (((PURCHASE_INVOICE)TransRecord).quoteOrderTransNumber != ""))
                {
                    PurchasesJournal.repo.OrderQuoteNo.Select((TransRecord as PURCHASE_INVOICE).quoteOrderTransNumber);

                    // Tab out
                    // PurchasesJournal.repo.Self.PressKeys("{tab}");
                    // do later
                    //SimplyMessage._SA_HandleMessage(SimplyMessage.YES_LOC, SimplyMessage._MSG_PLACEHOLDERTOFIXEXISTINGMETHODCALLS_LOC);
                }
            }
            else	// set order/quote number
            {
                if (Functions.GoodData(TransRecord.transNumber))
                {
                    PurchasesJournal.repo.OrderQuoteNoText.TextValue = TransRecord.transNumber;
                }
            }

            if (TransRecord.GetType() != typeof(PURCHASE_QUOTE))	// Set Paid by and ChequeNumbers
            {
                if (Functions.GoodData(((PURCHASE_COMMON_ORDER_INVOICE)TransRecord).paymentMethod))
                {
                    if (((PURCHASE_COMMON_ORDER_INVOICE)TransRecord).paymentMethod != "")
                    {
                    	PurchasesJournal.repo.PaidBy.Select((TransRecord as PURCHASE_COMMON_ORDER_INVOICE).paymentMethod);
                    }
                }                
                if (PurchasesJournal.repo.PaidFromInfo.Exists())
                {
                    if (Functions.GoodData(((PURCHASE_COMMON_ORDER_INVOICE)TransRecord).PaidFromAccount.acctNumber))
                    {
                    	PurchasesJournal.repo.PaidFrom.Select((TransRecord as PURCHASE_COMMON_ORDER_INVOICE).PaidFromAccount.acctNumber);
                    }
                }
                if (PurchasesJournal.repo.ChequeNumberInfo.Exists())
                {
                    if (Functions.GoodData(((PURCHASE_COMMON_ORDER_INVOICE)TransRecord).chequeNumber))
                    {
                        PurchasesJournal.repo.ChequeNumber.TextValue = ((PURCHASE_COMMON_ORDER_INVOICE)TransRecord).chequeNumber;
                    }
                }
            }

            if (TransRecord.GetType() == typeof(PURCHASE_INVOICE))
            {
                if (Functions.GoodData(((PURCHASE_INVOICE)TransRecord).directDepositNumber) && PurchasesJournal.repo.DirectDepositNumInfo.Exists())
                {
                    PurchasesJournal.repo.DirectDepositNum.TextValue = ((PURCHASE_INVOICE)TransRecord).directDepositNumber;
                }
            }

            if (TransRecord.GetType() != typeof(PURCHASE_INVOICE))	// Set shipping date
            {
                if (Functions.GoodData(TransRecord.shipDate))
                {
                    PurchasesJournal.repo.ShipDate.TextValue = TransRecord.shipDate;
                    // PurchasesJournal.repo.ShipDate.PressKeys("{Tab}");
                }
            }
            if (Functions.GoodData(TransRecord.transDate))
            {
                // PurchasesJournal.repo.InvoiceDate.SetFocus();	// get focus back after loading quote/order for conversion. otherwise, the following line won't work
                PurchasesJournal.repo.InvoiceDate.TextValue = TransRecord.transDate;
                // PurchasesJournal.repo.InvoiceDate.PressKeys("{Tab}");
            }
            if (Functions.GoodData(TransRecord.exchangeRate) && PurchasesJournal.repo.ExchangeRateInfo.Exists())
            {
                PurchasesJournal.repo.ExchangeRate.TextValue = TransRecord.exchangeRate;
                // PurchasesJournal.Instance.ExchangeRate.PressKeys("{Tab}");
            }
            if (Functions.GoodData(TransRecord.shipToLocation) && (TransRecord.shipToLocation != "") && (PurchasesJournal.repo.StoreItemsAtInfo.Exists()) && PurchasesJournal.repo.StoreItemsAt.Enabled)
            {
                PurchasesJournal.repo.StoreItemsAt.Select(TransRecord.shipToLocation);
            }


            // prepare the container
            PURCH_TABLE PT = new PURCH_TABLE();
            InitializeTable(PT);

//            if (bEdit)	// Maximize window if needed
//            {
//            	PurchasesJournal.repo.Self.Maximize();  // this is required other wise the selectitme is going to the wrong row
//            }

            int nLine = 1;
            //for (nLine = 0; nLine < TransRecord.GridRows.Count; nLine++)	// populate the container
            foreach(ROW currentRow in TransRecord.GridRows)
            {
            	if (TransRecord.GridRows.Count == 0 && bEdit)	// remove rest of row(s) during adjustment
                {
                    // RemoveRow(nLine, PT.iItem);
                }
                else	// enter all the fields as usual
                {
                    if (Functions.GoodData(currentRow.Item.invOrServNumber))
                    {
                        // since when converting quotes/orders to invoices, the item number field is disabled for editing
                        // we have to go to the quantity cell first then back to the item number cell as well as check if the item number cell is enabled
                        if (TransRecord.GetType() == typeof(PURCHASE_INVOICE))
                        {
                        	// For adjustment, the cursor starts on the second row, and no need to re-enter the inventory number
                        	if (bEdit)
                        	{
                        		PurchasesJournal.repo.TransContainer.PressKeys("{Up}");                        		
                        	}
                        	else
                        	{
								// Click on quantity cell first                            
								PurchasesJournal.repo.TransContainer.SelectCell("Quantity", nLine);
								PurchasesJournal.repo.TransContainer.PressKeys("{LShiftKey down}{Tab}{LShiftKey up}");
								PurchasesJournal.repo.TransContainer.PressKeys("{Delete}");
								
								// Check if item number is enabled                            
                            	if (PurchasesJournal.repo.ContainTextInfo.Exists())
                            	{      
                            		PurchasesJournal.repo.TransContainer.SelectCell("ItemNumber", nLine, currentRow.Item.invOrServNumber);                                
                                	PurchasesJournal.repo.TransContainer.PressKeys("{Tab}");
                            	}
                        	}                        	                            
                        }
                        else // go to item number cell and enter directly
                        {
                        	// to be changed
                            PurchasesJournal.repo.TransContainer.SelectCell("ItemNumber", nLine, currentRow.Item.invOrServNumber);                            
                        }
                    }

                    if (TransRecord.GetType() == typeof(PURCHASE_INVOICE))
                    {
                        if (Functions.GoodData(currentRow.quantityReceived))
                        {                            
                            PurchasesJournal.repo.TransContainer.SelectCell("Quantity", nLine, currentRow.quantityReceived);                            
                        }
                        
						// Disabled for now. For serialnumberdetails, there is a generic list created by the system
                        // enter serial number details if any                        
                        if (Functions.GoodData(((PURCHASE_INVOICE)TransRecord).SerialNumberDetails)) 
                        {
                            bool bFoundItem = false;
                            int iItmCnt;    // to be used outside the loop
                            for (iItmCnt = 0; iItmCnt < ((PURCHASE_INVOICE)TransRecord).SerialNumberDetails.Count; iItmCnt++)
                            {
                                if (((PURCHASE_INVOICE)TransRecord).SerialNumberDetails[iItmCnt].Item.invOrServNumber == currentRow.Item.invOrServNumber)
                                {
                                    bFoundItem = true;
                                }
                            }
                            if (bFoundItem)
                            {
                                PurchasesJournal.repo.TransContainer.SelectCell("Quantity", nLine);
                                PurchasesJournal.repo.TransContainer.PressKeys("{Enter}");
                                // Serial number dialog not done
//                                if (s_desktop.Exists(EditSerialNumberDotNet.EDITSERIALNUMBERDOTNET_LOC))
//                                {
//
//                                    //EditSerialNumberDotNet.repo.SerialContainer.SelectItem(0,0); // click in the first cell NC- commented out. seems redundant with the first line in the loop                                   
//                                    for (int iLine = 0; iLine < ((PURCHASE_INVOICE)TransRecord).SerialNumberDetails[iItmCnt].SerialNumbersToUse.Count; iLine++)
//                                    {
//                                        EditSerialNumberDotNet.repo.SerialContainer.SelectItem(0, iLine);
//                                        EditSerialNumberDotNet.repo.SerialContainer.PressKeys(((PURCHASE_INVOICE)TransRecord).SerialNumberDetails[iItmCnt].SerialNumbersToUse[iLine]);
//                                    }
//                                }
//                                EditSerialNumberDotNet.repo.OK.Click();
                            }
                        }
                    }


                    if (TransRecord.GetType() != typeof(PURCHASE_INVOICE)) // only if not invoice type since it's disabled for invoices
                    {
                        if (Functions.GoodData(currentRow.quantityOrdered) && currentRow.quantityOrdered != "0")
                        {
                            // have to tab into field otherwise if we select directly, it will type with existing text
                            PurchasesJournal.repo.TransContainer.SelectCell("Quantity", nLine);
                            PurchasesJournal.repo.TransContainer.PressKeys("{Tab}");
                            PurchasesJournal.repo.TransContainer.PressKeys(currentRow.quantityOrdered);	// Enter order
                        }
                    }
                    if (TransRecord.GetType() == typeof(PURCHASE_QUOTE))
                    {
                        if (Functions.GoodData(currentRow.quantityBackordered))
                        {
                            PurchasesJournal.repo.TransContainer.SelectCell("BackOrder", nLine);
                            PurchasesJournal.repo.TransContainer.PressKeys(currentRow.quantityBackordered);	// Enter back order
                        }
                    }
                    if (Functions.GoodData(currentRow.unit))
                    {
                        PurchasesJournal.repo.TransContainer.SelectCell("Unit", nLine);
                        PurchasesJournal.repo.TransContainer.PressKeys(currentRow.unit);	// Enter unit
                        PurchasesJournal.repo.TransContainer.PressKeys("{Tab}");
                    }
                    if (Functions.GoodData(currentRow.description))
                    {
                        //PurchasesJournal.repo.TransContainer.SelectItem(PT.iDescription, nLine);

                        //// we have to check if the description line has a value, if it does we have to do a delete in the field so that it doesn't append
                        //// however, if the fields is blank, it won't work properly, so only do if not blank
                        //// we have to use the xpath for the specific datagrid item in the container.  And it's using part of the locator so that we don't have two places to 
                        //// to maintain if the container tag changes it
                        //if(PurchasesJournal.repo.TransContainer.DataGridItem(String.Format("{0}({1}:{2})'", PurchasesJournal.TRANSCONTAINER_LOC.Replace("//DataGrid[", "").Replace("']", ""), PT.iDescription, nLine)).Text !="")
                        //{
                        //    PurchasesJournal.Instance.TransContainer.PressKeys("<Ctrl+Shift+Home>");  // have to delete description is duplicated
                        //    PurchasesJournal.Instance.TransContainer.TypeKeys("<Delete>");
                        //}

                        PurchasesJournal.repo.TransContainer.SelectCell("Unit", nLine);
                        PurchasesJournal.repo.TransContainer.PressKeys("{Tab}");
                        PurchasesJournal.repo.TransContainer.PressKeys(currentRow.description);	// Enter description
                    }
                    if (Functions.GoodData(currentRow.price))
                    {
                        PurchasesJournal.repo.TransContainer.SelectCell("Price", nLine);
                        PurchasesJournal.repo.Self.PressKeys(currentRow.price);

                        // NC - commented out the following debugging code. add back if necessary
                        // added debugging code as the result of  frequent failures in tests.
                        //string sCurrVal;	// value retrieved from the current cell
                        //int n = 1;
                        //while (n <= 2)
                        //{
                        //    pEnterCellValue(PurchasesJournal.Instance.TransContainer,nLine, PT.iPrice, TransRecord.GridRows[nLine].price);	// Enter price
                        //    PurchasesJournal.Instance.TransContainer.TypeKeys("<Tab>");
                        //    PurchasesJournal.Instance.TransContainer.TypeKeys("<Shift-Tab>");
                        //    sCurrVal = Functions.NoComma(PurchasesJournal.Instance.TransContainer.GetCellValue({nLine, PT.iPrice}));
                        //    if (Convert.ToDouble(sCurrVal) == Convert.ToDouble(TransRecord.GridRows[nLine].price))
                        //    {
                        //        break;
                        //    }
                        //    n++;
                        //}

                        // verify for debugging
                        //Functions.VerifyFunction(Convert.ToDouble(sCurrVal),Convert.ToDouble(TransRecord.GridRows[nLine].price), "Price is set correctly in line " + nLine + "");
                    }
                    if (Functions.GoodData(currentRow.amount))
                    {
                        if (currentRow.amount.Length <= 12)	// the amounts field only can type in 12 characters, let it calc if longer
                        {
                            PurchasesJournal.repo.TransContainer.SelectCell("Amount", nLine);
                            PurchasesJournal.repo.TransContainer.PressKeys(currentRow.amount);	// Enter amount
                        }
                        else if (TransRecord.GetType() == typeof(PURCHASE_INVOICE))
                        {
                            if (((currentRow.quantityReceived == "0") || (currentRow.quantityReceived == null)) ||
                            ((currentRow.price == "0") || (currentRow.price == null)))
                            {
                                Ranorex.Report.Info("Cannot type an amount of more than 999,999,999.99.");
                                Functions.Verify(false, true, "Amount");
                            }
                        }
                    }
                    if (Functions.GoodData(currentRow.TaxCode.code) || Functions.GoodData(currentRow.TaxCode.description))
                    {
                        // actually selecting the tax amount then tabbing back because selecint tax code column directly will bring up the selection window.
                        //PurchasesJournal.repo.TransContainer.SelectItem(PT.iTax + 1, nLine);
                        //PurchasesJournal.Instance.TransContainer.TypeKeys("<Shift+Tab>");
                        PurchasesJournal.repo.TransContainer.SelectCell("Price", nLine);  // tab forward from Price, can type in Taxcode without bringing up the selection window
                        PurchasesJournal.repo.TransContainer.PressKeys("{Tab}");

                        if (currentRow.TaxCode.description != Variables.sNoTax)	// enter the specific tax code
                        {
                            PurchasesJournal.repo.TransContainer.PressKeys(currentRow.TaxCode.code);
                        }
                        else	// set to No Tax by deleting the current tax code from the cell
                        {
                            PurchasesJournal.repo.Self.PressKeys("{Delete}");
                        }

                        // NC - commented out the following debugging code. add back if necessary
                        // verify. debugging code as the result of  frequent failures in tests
                        //PurchasesJournal.Instance.Window.TypeKeys("<Tab>");
                        //PurchasesJournal.Instance.Window.TypeKeys("<Shift-Tab>");	// have to tab out then back for the value entered to be applied
                        //// if no tax, then the field is blank
                        //if(TransRecord.GridRows[nLine].TaxCode.description == sNoTax)
                        //{
                        //    Functions.VerifyFunction(PurchasesJournal.Instance.TransContainer.GetCellValue({nLine, PT.iTax}), "", "Tax code is set correctly in line " + nLine + "");	// no tax returns as blank string
                        //}
                        //else
                        //{
                        //    VerifyFunction(PurchasesJournal.Instance.TransContainer.GetCellValue({nLine, PT.iTax}), NullToBlankString(TransRecord.GridRows[nLine].TaxCode.code), "Tax code is set correctly in line " + nLine + "");	// no tax returns as blank string
                        //}
                    }
                    // only enter if there is no inventory selected
                    if (!Functions.GoodData(currentRow.Item.invOrServNumber))
                    {
	                    if (Functions.GoodData(currentRow.Account.acctNumber))  // enter to the specific account
	                    {
	                        if (currentRow.Account.acctNumber != "")
	                        {
	                            PurchasesJournal.repo.TransContainer.SelectCell("Account", nLine);
	                            PurchasesJournal.repo.TransContainer.PressKeys(currentRow.Account.acctNumber);	// Enter account number
	                            PurchasesJournal.repo.TransContainer.PressKeys("{Tab}");
	                        }
	                    }
	                    else	// just pick a random account
	                    {
	                        PurchasesJournal.repo.TransContainer.SelectCell("Account", nLine);
	                        PurchasesJournal.repo.TransContainer.PressKeys("{Enter}");
								// SelectGLAccount not ready
	//                        // the account field maybe disabled if there's predefined account for the item selected so have to do the check here
	//                        if (s_desktop.Exists(SelectGLAccount.SELECTGLACCOUNT_LOC, 1000))
	//                        {
	//                            string acctNumber;
	//                            SelectGLAccount.repo.PickARandomAccount(out acctNumber);
	//                            currentRow.Account.acctNumber = acctNumber;    // remember the account selected
	//                            PurchasesJournal.repo.TransContainer.PressKeys("<Tab>");                            
	//                        }
	
	                    }
                    }
                    if (currentRow.Projects.Count != 0)
                    {
                        // Enter project allocation
                        // Process project allocation details if applicable
                        PurchasesJournal.repo.TransContainer.SelectCell("Account", nLine);
                        PurchasesJournal.repo.Self.PressKeys("{Ctrl Shift}a");                        
                        if (ProjectAllocationDialog.repo.SelfInfo.Exists())
                        {
                            ProjectAllocationDialog._SA_EnterProjectAllocationDetails(currentRow.Projects);
                        }
                    }
                }
            }

            // remove extra rows at the end
            if (bEdit)
            {
                //nLine++;

                //PurchasesJournal.Instance.TransContainer.SelectItem(PT.iItem, nLine); NC - no longer needed
// DataGridItem not ready
//                while (PurchasesJournal.repo.TransContainer.DataGridItem(GetDataGridItemLocator(TRANSCONTAINER_LOC, PT.iItem, nLine)).Text != "")
//                {
//
//                    RemoveRow(nLine, PT.iItem);
//
//                    //PurchasesJournal.Instance.SpecialTableField.TypeKeys("<Down>");
//                    //nLine++;
//                }
            }


            if (TransRecord.GetType() == typeof(PURCHASE_ORDER))	// set prepayment fields if specified
            {
                if (Functions.GoodData(((PURCHASE_ORDER)TransRecord).prepayRefNumber))
                {
                	PurchasesJournal.repo.PrepayRefNumber.TextValue = (TransRecord as PURCHASE_ORDER).prepayRefNumber;
                    PurchasesJournal.repo.PrepayRefNumber.PressKeys("{Tab}");
                }
                if (Functions.GoodData(((PURCHASE_ORDER)TransRecord).prepaymentAmount))
                {
                    PurchasesJournal.repo.PrepaymentAmount.TextValue = (TransRecord as PURCHASE_ORDER).prepaymentAmount;
                    PurchasesJournal.repo.PrepaymentAmount.PressKeys("{Tab}");
                }
            }
            if (Functions.GoodData(TransRecord.freightAmount))
            {
                PurchasesJournal.repo.FreightAmount.TextValue = TransRecord.freightAmount;
                PurchasesJournal.repo.FreightAmount.PressKeys("{Tab}");
            }
            if (Functions.GoodData(TransRecord.FreightTaxCode.code))
            {
                PurchasesJournal.repo.FreightCode.TextValue = TransRecord.FreightTaxCode.code;
            }
            else if (TransRecord.FreightTaxCode.description == Variables.sNoTax)
            {
            	// PurchasesJournal.repo.FreightCode.Focus();	// might not be needed in ranorex
                PurchasesJournal.repo.FreightCode.PressKeys("{Delete}");
                PurchasesJournal.repo.FreightCode.PressKeys("{Tab}");
            }

            if (PurchasesJournal.repo.TermsPercentInfo.Exists())
            {
                if (Functions.GoodData(TransRecord.termsPercent))
                {
                    PurchasesJournal.repo.TermsPercent.TextValue = TransRecord.termsPercent;
                    PurchasesJournal.repo.TermsPercent.PressKeys("{Tab}");
                }
                if (Functions.GoodData(TransRecord.termsDays))
                {
                    PurchasesJournal.repo.TermsDay.TextValue = TransRecord.termsDays;
                    PurchasesJournal.repo.TermsDay.PressKeys("{Tab}");
                }
                if (Functions.GoodData(TransRecord.termsNetDays))
                {
                    PurchasesJournal.repo.TermsNetDays.TextValue = TransRecord.termsNetDays;
                    PurchasesJournal.repo.TermsNetDays.PressKeys("{Tab}");
                }
            }
            if (PurchasesJournal.repo.EarlyDiscountInfo.Exists())
            {
                if (Functions.GoodData(((PURCHASE_INVOICE)TransRecord).earlyPaymentDiscountPercent))
                {
                    PurchasesJournal.repo.EarlyDiscount.TextValue = ((PURCHASE_INVOICE)TransRecord).earlyPaymentDiscountPercent;
                    PurchasesJournal.repo.EarlyDiscount.PressKeys("{Tab}");
                }
            }

            if (Functions.GoodData(TransRecord.shippedBy) || Functions.GoodData(TransRecord.trackingNumber))
            {                
                PurchasesJournal.repo.TrackShipments.Click();
                if (Functions.GoodData(TransRecord.shippedBy))
                {                   
                    TrackShipments.repo.Shipper.Select(TransRecord.shippedBy);
                }
                if (Functions.GoodData(TransRecord.trackingNumber))
                {                   
                    TrackShipments.repo.TrackingNumber.TextValue = TransRecord.trackingNumber;
                }
                TrackShipments.repo.OK.Click();
            }

            PurchasesJournal.repo.Self.Restore();

            if (bRecur && !bSave)
            {
            	// Recurring entry dialog not ready
//                Trace.WriteLine("Storing the recurring entry " + TransRecord.recurringName + ", " + TransRecord.recurringFrequency + "");                
//                PurchasesJournal.repo.RecurringEntry.Click();
//                StoreRecurringDialogDotNet.repo._SA_DoStoreRecurring(TransRecord.recurringName, TransRecord.recurringFrequency);
//
//                // discard the transaction
//                if (Functions.GetProgramName().ToLower() != "fastposting.cs")
//                {
//                    PurchasesJournal.repo.ClickUndoChanges(true);	// wait on the message
//                }
//                else
//                {
//                    PurchasesJournal.repo.ClickUndoChanges(false);
//                }
            }
            if (bSave)
            {
                PurchasesJournal.repo.Post.Click();

                // use data file not ready
//                if (Variables.bUseDataFiles)	// only handle the messages when using external data (i.e. audit)
//                {
//                    while (!(PurchasesJournal.repo.Window.Enabled))
//                    {
//                        SimplyMessage.repo._SA_HandleMessage(SimplyMessage.YES_LOC, SimplyMessage._MSG_AREYOUSUREYOUWANTTOREMOVEORREVERSE_LOC, false, true);
//                        SimplyMessage.repo._SA_HandleMessage(SimplyMessage.OK_LOC, SimplyMessage._MSG_ORDERHASBEENFILLEDANDREMOVED_LOC, false, true);
//                        SimplyMessage.repo._SA_HandleMessage(SimplyMessage.NO_LOC, SimplyMessage._MSG_THEINVOICENUMBERYOUENTEREDISGREATER_LOC, false, true);
//                        SimplyMessage.repo._SA_HandleMessage(SimplyMessage.YES_LOC, SimplyMessage._MSG_YOUAREABOUTTOCHANGETHEQUOTEINTOANORDER_LOC, false, true);
//                        SimplyMessage.repo._SA_HandleMessage(SimplyMessage.YES_LOC, SimplyMessage._MSG_THERECURRINGTRANSACTIONHASBEENCHANGED_LOC, false, true);
//                    }
//                }

            }
        }
        
        public static void _SA_Close()
		{
        	System.Threading.Thread.Sleep(2000);
			repo.Self.Close();
	
		}
    }
}
