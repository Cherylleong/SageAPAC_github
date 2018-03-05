/*
 * Created by Ranorex
 * User: wonga01
 * Date: 10/6/2017
 * Time: 16:22
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
	/// Description of ReceiptsJournal.
	/// </summary>
	public class ReceiptsJournal
	{
		public static ReceiptsJournalResFolders.ReceiptsJournalAppFolder repo = ReceiptsJournalRes.Instance.ReceiptsJournal;
		
		
		public static void _SA_Invoke()
		{
			if (Simply.isEnhancedView())
            {
                Simply.repo.Self.Activate();
                Simply.repo.ReceivablesLink.Click();
                Simply.repo.ReceiptsIcon.Click();                
            }
		}
		
		public static void _SA_Open(RECEIPT  ReceiptRecord)
		{
			if (!ReceiptsJournal.repo.SelfInfo.Exists())
			{
				ReceiptsJournal._SA_Invoke();
			}			
			ReceiptsJournal.repo.Adjust.Click();
			
            // select date range                        
   			DialogJournalSearch._SA_SelectLookupDateRange();
			DialogJournalSearch.repo.Name.Select(ReceiptRecord.Customer.name);
			DialogJournalSearch.repo.Source.TextValue = ReceiptRecord.transNumber;
			DialogJournalSearch.repo.OK.Click();
		}
		
		public static void _SA_MatchDefaults(RECEIPT ReceiptRecord)
		{
			
			if (!Functions.GoodData(ReceiptRecord.transNumber))	// receipt number
			{
				ReceiptRecord.transNumber = Variables.globalSettings.CompanySettings.FormSettings.nextNumReceipt;
			}
			if (!Functions.GoodData(ReceiptRecord.depositRefNum))	// cusotmer deposit number
			{
				ReceiptRecord.depositRefNum = Variables.globalSettings.CompanySettings.FormSettings.nextNumCustomerDeposit;
			}
			if (!Functions.GoodData(ReceiptRecord.depositAmount))
			{
				ReceiptRecord.depositAmount = "0.00";
			}
			//GlobalSettings.CompanySettings.CurrencySettings.ForeignCurrencies.ExchangeRates.exchangeRate ReceiptRecord.Customer.currencyCode
		}
		
		
		public static void _SA_Create(RECEIPT ReceiptRecord)
		{
			_SA_Create(ReceiptRecord, true, false);
		}

		public static void _SA_Create(RECEIPT ReceiptRecord, bool bSave)
		{
			_SA_Create(ReceiptRecord, bSave, false);
		}

		public static void _SA_Create(RECEIPT ReceiptRecord, bool bSave, bool bEdit)
		{

            if (!Variables.bUseDataFiles)	// if external data files are not used
            {
                ReceiptsJournal._SA_MatchDefaults(ReceiptRecord);
            }
			
            if (!ReceiptsJournal.repo.SelfInfo.Exists())
			{
				ReceiptsJournal._SA_Invoke();
			}
			
            ReceiptsJournal.repo.EnterDeposits.Click(); // ensure enter deposits is on so objects show in screen

			if (!bEdit)
			{               
				ReceiptsJournal.repo.CustomerName.Select(ReceiptRecord.Customer.name);
				ReceiptsJournal.repo.Self.PressKeys("{Tab}");	// Must press tab, otherwise selecting the Paid By field may not work			
				Ranorex.Report.Info(String.Format("Creating receipt {0} ", ReceiptRecord.transNumber));
			}
			else
			{
				Ranorex.Report.Info(String.Format("Adjusting Receipt {0} ", ReceiptRecord.transNumber));
				ReceiptsJournal._SA_Open(ReceiptRecord);	// load transaction for adjustment
			}
			
			//  Validate if Add on the fly is present
			if (AddOnTheFly.repo.QuickAddInfo.Exists())
			{
				try
				{
					Functions.Verify(true, false, "Add on fly message appears");
				}
				catch
				{					
					Trace.WriteLine("Add on the fly message not found");
				}
				AddOnTheFly.repo.QuickAdd.Click();
			}
			
			if (Functions.GoodData(ReceiptRecord.paidBy))
			{
                // Using the select is no working properly
                // it seems to trigger event for all items in list..
                // ie. visa is last item, using select will complain PAD is not set up for customer
                ReceiptsJournal.repo.PaidBy.Select(ReceiptRecord.paidBy);                
			}
			if (Functions.GoodData (ReceiptRecord.DepositAccount.acctNumber))
			{
				ReceiptsJournal.repo.DepositTo.Select (ReceiptRecord.DepositAccount.acctNumber);
			}
			if(ReceiptsJournal.repo.ChequeNumberInfo.Exists())
			{
				if (Functions.GoodData(ReceiptRecord.chequeNumber))
				{
					ReceiptsJournal.repo.ChequeNumber.TextValue = ReceiptRecord.chequeNumber;
				}
			}
			if (Functions.GoodData (ReceiptRecord.transNumber))
			{
				ReceiptsJournal.repo.ReceiptNumber.TextValue = ReceiptRecord.transNumber;
			}
			if (Functions.GoodData (ReceiptRecord.transDate))
			{
				ReceiptsJournal.repo.ReceiptDate.TextValue = ReceiptRecord.transDate;
			}
			if (Functions.GoodData (ReceiptRecord.padNumber))
			{
				ReceiptsJournal.repo.PadNumber.TextValue = ReceiptRecord.padNumber;
			}			
		
            // handle the container
            if (ReceiptsJournal.repo.TransContainer.Enabled)	// added checking here intentionally as the container is disabled if there are no invoices available for the customer selected
            {                
                int iDepLine = 100;	// initialize to big number
                bool bDepositsInList = false;

                ReceiptsJournal.repo.TransContainer.ClickFirstCell();
                List<List<string>> lsContents = ReceiptsJournal.repo.TransContainer.GetContents();                

                for (int x = 0; x < lsContents.Count; x++)	// search for deposits line
                {
                    if(lsContents[x][1].Trim().ToUpper() == "DEPOSITS")
                    {
                        bDepositsInList = true;
                        iDepLine = x;
                        break;
                    }
                }
                int iCurrentLine = 0;
                for (int y = 0; y < ReceiptRecord.GridRows.Count; y++)
                {
                    //if (FunctionsLib.GoodData (ReceiptRecord.GridRows[y].Amount))	// this line return false when only invoice object exists and rest are null
                    //{
                        bool bFound = false;
                        int x;  // to be used outside the loop so declares here
                        for (x = 0; x < lsContents.Count; x++)
                        {
                            string sFindString;
                            if (Functions.GoodData(ReceiptRecord.GridRows[y].Invoice.transNumber))
                            {
                                sFindString = ReceiptRecord.GridRows[y].Invoice.transNumber;
                            }
                            else
                            {
                                sFindString = ReceiptRecord.GridRows[y].DepositReceipt.depositRefNum;
                                if (!bDepositsInList)
                                {
                                    break;	// error, we don't see any deposits
                                }
                            }
                            if(lsContents[x][1] == sFindString)
                            {
                                if (Functions.GoodData(ReceiptRecord.GridRows[y].DepositReceipt.depositRefNum))	// if deposit, be sure we found the deposit and not an invoice above it
                                {
                                    if (x > iDepLine)
                                    {
                                        bFound = true;
                                        break;
                                    }
                                    //else try again, you found an invoice with same number
                                }
                                else
                                {
                                    bFound = true;
                                    break;
                                }
                            }
                        }
                        if (bFound)
                        {
                            if (iCurrentLine == 0)	// see if first time in container
                            {
                                ReceiptsJournal.repo.TransContainer.MoveRight();	// get to the discount field
                                if (x > 0)	// below the first row
                                {
                                    ReceiptsJournal.repo.TransContainer.PressKeys("{Down " + Convert.ToString(x) + "}");	// get to the correct row
                                }
                            }
                            else	// else decide if we go up or down based on current location
                            {
                                if (x > iCurrentLine)
                                {
                                    ReceiptsJournal.repo.TransContainer.PressKeys("{Down " + Convert.ToString(x) + "}");	// get to the correct row
                                }
                                if (x < iCurrentLine)
                                {
                                    ReceiptsJournal.repo.TransContainer.PressKeys("{Up " + Convert.ToString(iCurrentLine-x) + "}");	// get to the correct row
                                }
                            }
                            iCurrentLine = x;
                            if (Functions.GoodData (ReceiptRecord.GridRows[y].discountTaken))
                            {
                                ReceiptsJournal.repo.TransContainer.SetText(ReceiptRecord.GridRows[y].discountTaken);
                            }
                            ReceiptsJournal.repo.TransContainer.MoveRight();	// get to the amount field
                            if (Functions.GoodData (ReceiptRecord.GridRows[y].Amount))
                            {
                                ReceiptsJournal.repo.TransContainer.SetText(ReceiptRecord.GridRows[y].Amount);
                            }
                            if(x < (iDepLine - 2))
                            {
                                ReceiptsJournal.repo.TransContainer.MoveRight();
                            }
                            iCurrentLine++;
                        }
                        else
                        {
                            Functions.Verify(false, true, "Able to find Invoice/Depositin Receipt Grid");
                        }
                    //}
                }
            }
									
			if (Functions.GoodData (ReceiptRecord.depositRefNum))
			{
				ReceiptsJournal.repo.DepositReferenceNo.TextValue = ReceiptRecord.depositRefNum;
			}
			if (Functions.GoodData (ReceiptRecord.depositAmount))
			{
				ReceiptsJournal.repo.DepositAmount.TextValue = ReceiptRecord.depositAmount;
                ReceiptsJournal.repo.DepositAmount.PressKeys("{Tab}");
			}
			if (Functions.GoodData (ReceiptRecord.comment))
			{
				ReceiptsJournal.repo.Comment.TextValue = ReceiptRecord.comment;
			}
			if(ReceiptsJournal.repo.ExchangeRate.Enabled)
			{
				if (Functions.GoodData(ReceiptRecord.exchangeRate))
				{
					ReceiptsJournal.repo.ExchangeRate.TextValue = ReceiptRecord.exchangeRate;
				}
			}
			if (bSave)
			{
				ReceiptsJournal.repo.Post.Click();
			}
		}
		
		public static void _SA_printTransaction(string sFileName)
		{
			_SA_printTransaction(sFileName, null);
		}

		public static void _SA_printTransaction(string sFileName, RECEIPT ReceiptRecord)
		{			
			if (Functions.GoodData(ReceiptRecord))
			{
				ReceiptsJournal._SA_Open(ReceiptRecord);			
				Trace.WriteLine("Receipt" + ReceiptRecord.transNumber + "");
			}
			
			ReceiptsJournal.repo.Print.Click();
			
			SimplyMessage._SA_HandleMessage(SimplyMessage.repo.Yes, SimplyMessage.sChqNumIsOutOfSequence, false, false);
						
			PrintToFileDialog.Print(sFileName);
			
			// Undo changes
			ReceiptsJournal.ClickUndoChanges();						
		}
		
		
		public static RECEIPT _SA_Read()
        {
            return _SA_Read(null);
        }

        public static RECEIPT _SA_Read(RECEIPT ReceiptRecord) //  method will read all fields and store the data in a RECEIPT record
        {
            RECEIPT Rec = new RECEIPT();

            if (Functions.GoodData(ReceiptRecord))
            {
                ReceiptsJournal._SA_Open(ReceiptRecord);
            }

            Rec.paidBy = ReceiptsJournal.repo.PaidBy.SelectedItemText;
            if (ReceiptsJournal.repo.DepositToInfo.Exists())
            {
                Rec.DepositAccount.acctNumber = ReceiptsJournal.repo.DepositTo.SelectedItemText;
            }
            if (ReceiptsJournal.repo.ChequeNumberInfo.Exists())
            {
                {
                    Rec.chequeNumber = ReceiptsJournal.repo.ChequeNumber.TextValue;
                }
            }
            Rec.Customer.name = ReceiptsJournal.repo.CustomerName.SelectedItemText;
            Rec.transDate = ReceiptsJournal.repo.ReceiptDate.TextValue;
            
            if (ReceiptsJournal.repo.ReceiptNumberInfo.Exists())
            {
                Rec.transNumber = ReceiptsJournal.repo.ReceiptNumber.TextValue;
            }
            if (ReceiptsJournal.repo.PadNumberInfo.Exists())
            {
                Rec.padNumber = ReceiptsJournal.repo.PadNumber.TextValue;
            }

            Rec.GridRows.Clear();
            List<List<string>> lsContents = ReceiptsJournal.repo.TransContainer.GetContents();	// a blank row is added at the end of the list

            if (Functions.GoodData(lsContents))
            {
                bool bDepositLineFound = false;
                for (int x = 0; x < lsContents.Count; x++)
                {
                    RECEIPT_ROW RR = new RECEIPT_ROW();
                    string sRefNum = ConvertFunctions.BlankStringToNULL(lsContents[x][1]);
                    if (Functions.GoodData(sRefNum))	// to avoid adding a blank row
                    {
                        if (sRefNum == "Deposits")
                        {
                            bDepositLineFound = true;
                        }
                        else
                        {
                            if (bDepositLineFound)	// deposit row
                            {
                                RR.DepositReceipt.depositRefNum = sRefNum;
                            }
                            else	// invoice row
                            {
                                RR.Invoice.transNumber = sRefNum;
                            }
                            RR.discountTaken = ConvertFunctions.BlankStringToNULL(lsContents[x][5]);
                            RR.Amount = ConvertFunctions.BlankStringToNULL(lsContents[x][6]);
                        }
                        Rec.GridRows.Add(RR);
                    }
                }
            }

            Rec.depositRefNum = ReceiptsJournal.repo.DepositReferenceNo.TextValue;
            Rec.depositAmount = ReceiptsJournal.repo.DepositAmount.TextValue;
            if (ReceiptsJournal.repo.ExchangeRateInfo.Exists())
            {
                Rec.exchangeRate = ReceiptsJournal.repo.ExchangeRate.TextValue;
            }
            Rec.comment = ReceiptsJournal.repo.Comment.TextValue;

            return Rec;
        }
        
        
        public static void _SA_Delete(RECEIPT ReceiptRecord)
		{			
			ReceiptsJournal._SA_Open(ReceiptRecord);	// load the receipt for deletion
			ReceiptsJournal.repo.Reverse.Click();
			SimplyMessage._SA_HandleMessage(SimplyMessage.repo.Yes, SimplyMessage.sReverseReceiptMsg);
		}
						
				
		public static void ClickUndoChanges()
		{			            
            ReceiptsJournal.repo.Undo.Click();
			
			SimplyMessage._SA_HandleMessage(SimplyMessage.repo.Yes, SimplyMessage.sDiscardJournalMsg);	// the message does not appear when in lookup mode
		}
		
		public static void _SA_Close()
		{
			ReceiptsJournal.repo.Self.Close();
		}
	}
}
