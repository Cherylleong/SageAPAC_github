/*
 * Created by Ranorex
 * User: wonga01
 * Date: 9/29/2017
 * Time: 13:59
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
	/// Description of PurchasePayment.
	/// </summary>
	public class PaymentsJournal
	{
		public static PaymentsJournalResFolders.PaymentsJournalAppFolder repo = PaymentsJournalRes.Instance.PaymentsJournal;
		
		
		public static void _SA_Invoke()
		{
			if (Simply.isEnhancedView())
            {
                Simply.repo.Self.Activate();
                Simply.repo.PayablesLink.Click();
                Simply.repo.PaymentIcon.Click();                
            }
		}
		
		
		public static void ClickUndoChanges()
		{
			PaymentsJournal.repo.UndoCurrentTransaction.Click();
			// SimplyMessage.Instance._SA_HandleMessage(SimplyMessage.YES_LOC, SimplyMessage._MSG_AREYOUSUREYOUWANTTODISCARD_LOC);	// the message does not appear when in lookup mode
		}
		
		
		public static void _SA_MatchDefaultsPayment(PAYMENT_PURCH PaymentRecord)
		{
			// need to fill this in
		}

		public static void _SA_MatchDefaultsPayment_CreditCard(PAYMENT_CREDIT_CARD PaymentRecord)
		{
			//need to fill this in
		}

		public static void _SA_MatchDefaultsPayment_Other(PAYMENT_OTHER PaymentRecord)
		{
			//need to fill this in
		}

		public static void _SA_MatchDefaultsPayment_Remit(PAYMENT_REMIT PaymentRecord)
		{
			//need to fill this in
			if (!Functions.GoodData(PaymentRecord.chequeNumber))
			{
				PaymentRecord.chequeNumber = PaymentsJournal.repo.ChequeNo.TextValue;
			}
			if (!Functions.GoodData(PaymentRecord.TransDate))
			{
				PaymentRecord.TransDate = PaymentsJournal.repo.PaymentDate.TextValue;
			}
			if (!Functions.GoodData(PaymentRecord.periodEnding))
			{
				PaymentRecord.periodEnding = PaymentsJournal.repo.EndDate.TextValue;
			}
		}
		
		
		public static void _SA_CreatePayment(PAYMENT_PURCH PaymentRecord)
		{
			_SA_CreatePayment(PaymentRecord, true, false);
		}

		public static void _SA_CreatePayment(PAYMENT_PURCH PaymentRecord, bool bSave)
		{
			_SA_CreatePayment(PaymentRecord, bSave, false);
		}

		public static void _SA_CreatePayment(PAYMENT_PURCH PaymentRecord, bool bSave, bool bEdit)
		{
			
			if (!PaymentsJournal.repo.SelfInfo.Exists())
			{
				PaymentsJournal._SA_Invoke();
			}
			
			// after invoke because checks for cheque number on screan
			if (! Variables.bUseDataFiles)
			{
				PaymentsJournal._SA_MatchDefaultsPayment(PaymentRecord);
			}
			
			string source;	// to be used in print statements
			if (Functions.GoodData(PaymentRecord.chequeNumber))	// paid by cheque
			{
				source = PaymentRecord.chequeNumber;
			}
			else
			{
				source = PaymentRecord.source;
			}
			
			if (!(bEdit))
			{
				Ranorex.Report.Info(String.Format("Creating payment {0} ", source));
			}
			else
			{
				Ranorex.Report.Info(String.Format("Adjusting payment {0} ", source));
			}
			
			PaymentsJournal.repo.Transaction.Select("Pay Purchase Invoices");
			// Ensure prepayments button is depressed
			if (!PaymentsJournal.repo.PrepaymentAmountInfo.Exists())
			{
				PaymentsJournal.repo.EnterPrepayments.Click();
			}
			
			Common_HeaderSetup(PaymentRecord, bEdit);
			
			if (PaymentsJournal.repo.TransContainer.Enabled)	// need to check if the container is enabled before entering data into it
			{
                PaymentsJournal.repo.TransContainer.ClickFirstCell();
				List<List<string>> lsContents = PaymentsJournal.repo.TransContainer.GetContents();
				
				int iPrePayLine = 100;	// intialize to big number
				bool bPrePaysInList = false;
				for (int x = 0; x < lsContents.Count; x++)	// search for prepayment line
				{
                    if(lsContents[x][1].Trim().ToUpper() == "PREPAYMENTS")
					{
						bPrePaysInList = true;
						iPrePayLine = x;
						break;
					}
				}
				int iCurrentLine = 0;
				for (int y = 0; y < PaymentRecord.GridRows.Count; y++)
				{
					if(Functions.GoodData(PaymentRecord.GridRows[y].Invoice.transNumber))
					{	
						bool bFound = false;

                        int x;  // needs to be referenced outside the loop so declares here
						for (x = 0; x < lsContents.Count; x++)
						{
                            string sFindString;
							if (Functions.GoodData (PaymentRecord.GridRows[y].Invoice.transNumber))
							{
								sFindString = PaymentRecord.GridRows[y].Invoice.transNumber;
							}
							else
							{
								sFindString = PaymentRecord.GridRows[y].PrePayment.PrePayRefNumber;
								if (!bPrePaysInList)
								{
									break;	// error, we don't see any prepayments
								}
							}
                            if (lsContents[x][1] == sFindString)
							{
								if (Functions.GoodData (PaymentRecord.GridRows[y].PrePayment.PrePayRefNumber))	// if prepayment, be sure we found the prepayment and not an invoice above it
								{
									if (x > iPrePayLine)
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
								PaymentsJournal.repo.TransContainer.MoveRight();	// get to the discount field
                                PaymentsJournal.repo.TransContainer.PressKeys("{Delete}");   // clear in case not line working with
								if (x > 0)
								{
                                    PaymentsJournal.repo.TransContainer.PressKeys("{Down " + Convert.ToString(x) + "}");	// get to the correct row
								}
							}
							else	// else decide if we go up or down based on current location
							{
								if (x > iCurrentLine)
								{
                                    PaymentsJournal.repo.TransContainer.PressKeys("{Down " + Convert.ToString(x) + "}");	// get to the correct row
								}
								if (x < iCurrentLine)
								{
                                    PaymentsJournal.repo.TransContainer.PressKeys("{Up " + Convert.ToString(iCurrentLine - x) + "}");	// get to the correct row
								}
							}
							iCurrentLine = x;
							if (Functions.GoodData(PaymentRecord.GridRows[y].discountTaken))
							{
								PaymentsJournal.repo.TransContainer.SetText(PaymentRecord.GridRows[y].discountTaken);
							}
                            PaymentsJournal.repo.TransContainer.MoveRight();	// get to the amount field
							if (Functions.GoodData (PaymentRecord.GridRows[y].Amount))
							{
								PaymentsJournal.repo.TransContainer.SetText(PaymentRecord.GridRows[y].Amount);
							}
							if(x < (iPrePayLine - 2))
							{
                                PaymentsJournal.repo.TransContainer.MoveRight();
							}
							iCurrentLine++;
						}
						else
						{
							Functions.Verify(false, true, "Able to find Invoice/prepayment double in payment Grid");
						}
					}					
				}
			}
			
			if ((Functions.GoodData(PaymentRecord.PrePayRefNumber)) || (Functions.GoodData(PaymentRecord.PrePayAmount)))
			{
				if (PaymentsJournal.repo.PrepaymentAmountInfo.Exists())
				{
					PaymentsJournal.repo.EnterPrepayments.Click();
				}
				if (Functions.GoodData(PaymentRecord.PrePayRefNumber))
				{
					PaymentsJournal.repo.PrepaymentReferenceNo.TextValue = PaymentRecord.PrePayRefNumber;
				}
				if (Functions.GoodData(PaymentRecord.PrePayAmount))
				{
					PaymentsJournal.repo.PrepaymentAmount.TextValue = PaymentRecord.PrePayAmount;
                    PaymentsJournal.repo.PrepaymentAmount.PressKeys("{Tab}");
				}
			}
			if (PaymentsJournal.repo.ExchangeRate.Enabled) 
			{
				if (Functions.GoodData(PaymentRecord.exchangeRate))
				{
					PaymentsJournal.repo.ExchangeRate.TextValue = PaymentRecord.exchangeRate;
				}
			}
			
			if (bSave)
			{
				PaymentsJournal.repo.Post.Click();
			}
		}

		
		public static void _SA_CreatePayment_CreditCard(PAYMENT_CREDIT_CARD PaymentRecord)
		{
			_SA_CreatePayment_CreditCard(PaymentRecord, true);
		}

		public static void _SA_CreatePayment_CreditCard(PAYMENT_CREDIT_CARD PaymentRecord, bool bSave)
		{
			
			string source;	// to be used in print statements
			if (Functions.GoodData(PaymentRecord.chequeNumber))	// paid by cheque
			{
				source = PaymentRecord.chequeNumber;
			}
			else
			{
				source = PaymentRecord.source;
			}
			
			Ranorex.Report.Info(String.Format("Creating credit card payment {0} ", source));
			
			if (!PaymentsJournal.repo.SelfInfo.Exists())
			{
				PaymentsJournal._SA_Invoke();
			}
			
			PaymentsJournal.repo.Transaction.Select ("Pay Credit Card Bill");
			
			Common_HeaderSetup (PaymentRecord, false);	// can't adjust credit card payments
			
			if (Functions.GoodData(PaymentRecord.additionalFees))
			{
				PaymentsJournal.repo.AdditionalFeesAndInterest.TextValue = PaymentRecord.additionalFees;
			}
			if (Functions.GoodData(PaymentRecord.amount))
			{
				PaymentsJournal.repo.PaymentAmount.TextValue = PaymentRecord.amount;
                // PaymentsJournal.repo.PaymentAmount.PressKeys("{Tab}");
			}
			
			
			if (bSave)
			{
				PaymentsJournal.repo.Post.Click();
			}
		}
		
		public static void _SA_CreatePayment_Other(PAYMENT_OTHER PaymentRecord)
		{
			_SA_CreatePayment_Other(PaymentRecord, true, false, false);
		}
		public static void _SA_CreatePayment_Other(PAYMENT_OTHER PaymentRecord, bool bSave)
		{
			_SA_CreatePayment_Other(PaymentRecord, bSave, false, false);
		}

		public static void _SA_CreatePayment_Other(PAYMENT_OTHER PaymentRecord, bool bSave, bool bEdit)
		{
			_SA_CreatePayment_Other(PaymentRecord, bSave, bEdit, false);
		}

		public static void _SA_CreatePayment_Other(PAYMENT_OTHER PaymentRecord, bool bSave, bool bEdit, bool bRecur)
		{
			
			
			bool bCheckGlobalProject = false;
			
			if (!PaymentsJournal.repo.SelfInfo.Exists())
			{
				PaymentsJournal._SA_Invoke();
			}
			
			string source;	// to be used in print statements
			if (Functions.GoodData(PaymentRecord.chequeNumber))	// paid by cheque
			{
				source = PaymentRecord.chequeNumber;
			}
			else
			{
				source = PaymentRecord.source;
			}
			
			if (!bEdit)
			{
				if (!bRecur)
				{
					Ranorex.Report.Info(String.Format("Creating Other Payment {0} ", source));
				}
                //else is recurring entry and the message will be printed later.
			}
			else
			{
				Ranorex.Report.Info(String.Format("Adjusting Other Payment {0} ", source));
			}
			
			PaymentsJournal.repo.Transaction.Select("Make Other Payment");
			
			Common_HeaderSetup(PaymentRecord, bEdit);
			
			if (Functions.GoodData(PaymentRecord.reference))
			{
				PaymentsJournal.repo.Reference.TextValue = PaymentRecord.reference;
			}

            PaymentsJournal.repo.TransContainer.ClickFirstCell();
			PaymentsJournal.repo.TransContainer.PressKeys("{Tab}");	// go to the Description field first
			PaymentsJournal.repo.TransContainer.PressKeys("{LShiftKey down}{Tab}{LShiftKey up}");	// then come back to the Account field to activate the cell
            

			for (int x = 0; x < PaymentRecord.GridRows.Count; x++)
			{
				if (Functions.GoodData (PaymentRecord.GridRows[x]))
				{
					if (Functions.GoodData(PaymentRecord.GridRows[x].account.acctNumber))
					{
						PaymentsJournal.repo.TransContainer.SetText(PaymentRecord.GridRows[x].account.acctNumber);
                        PaymentsJournal.repo.TransContainer.MoveRight();	// tab to the Description field
					}
					else	// pick a random account
					{                     
						PaymentsJournal.repo.TransContainer.PressKeys("<Enter>");	// press enter to bring up the select GL account window												                                                
                        PaymentRecord.GridRows[x].account.acctNumber = SelectAccountDialog.repo.AccountName.RandPick(true);                        
						// the focus is set to descirption field automatically so no need to tab again
					}
					if (Functions.GoodData(PaymentRecord.GridRows[x].description))
					{
                        PaymentsJournal.repo.TransContainer.SetText(PaymentRecord.GridRows[x].description);
					}
                    PaymentsJournal.repo.TransContainer.MoveRight();
                    PaymentsJournal.repo.TransContainer.SetText(PaymentRecord.GridRows[x].amount);
                    PaymentsJournal.repo.TransContainer.MoveRight();
					if (Functions.GoodData(PaymentRecord.GridRows[x].taxCode.code))
					{
                        PaymentsJournal.repo.TransContainer.SetText(PaymentRecord.GridRows[x].taxCode.code);
					}
					
                    if (PaymentRecord.GridRows[x].Projects.Count != 0)	// enter project allocation if provided
                    {

                        // get global settings if haven't alreayd
                        if (!bCheckGlobalProject)
                        {
                            Settings._SA_Get_AllProjectSettings();
                            // PaymentsJournal.repo.Window.SetActive();
                            bCheckGlobalProject = true;
                        }
                        
                        PaymentsJournal.repo.AllocateToProjects.Click();
                        if (ProjectAllocationDialog.repo.SelfInfo.Exists())
                        {                            
                            ProjectAllocationDialog._SA_EnterProjectAllocationDetails(PaymentRecord.GridRows[x].Projects);
                        }
                    }
                    
                    PaymentsJournal.repo.TransContainer.PressKeys("{Tab}");	// move to the next row
				}
			}
			
			// Recurring entry not ready
//			if (bRecur && !bSave)	// store recurring
//			{
//				Trace.WriteLine("Storing the recurring entry " + PaymentRecord.recurringName + ", " + PaymentRecord.recurringFrequency + "");
//				PaymentsJournal.repo.PressKeys("{Ctrl+t}");
//				StoreRecurringDialog.Instance._SA_DoStoreRecurring(PaymentRecord.recurringName, PaymentRecord.recurringFrequency);
//				// discard the transaction
//				PaymentsJournal.ClickUndoChanges();
//			}

            if (bSave)
            {
                PaymentsJournal.repo.Post.Click();
            }
		}
		
		
		public static void _SA_CreatePayment_Remit(PAYMENT_REMIT PaymentRecord)
        {
            _SA_CreatePayment_Remit(PaymentRecord, true, false);
        }

        public static void _SA_CreatePayment_Remit(PAYMENT_REMIT PaymentRecord, bool bSave)
        {
            _SA_CreatePayment_Remit(PaymentRecord, bSave, false);
        }

        public static void _SA_CreatePayment_Remit(PAYMENT_REMIT PaymentRecord, bool bSave, bool bEdit)
        {
        	if (!PaymentsJournal.repo.SelfInfo.Exists())
            {
                PaymentsJournal._SA_Invoke();
            }
			
            if (!(bEdit))
            {
                Ranorex.Report.Info(String.Format("Creating Remittance {0} ", PaymentRecord.chequeNumber));
            }
            else
            {
                Ranorex.Report.Info(String.Format("Adjusting Remittance {0} ", PaymentRecord.chequeNumber));
            }
			
            PaymentsJournal.repo.Transaction.Select("Pay Remittance");
            PaymentsJournal._SA_MatchDefaultsPayment_Remit(PaymentRecord);
			
            Common_HeaderSetup(PaymentRecord, bEdit);
            if (Functions.GoodData (PaymentRecord.reference))
            {
                PaymentsJournal.repo.Reference.TextValue = PaymentRecord.reference;
            }
            if (Functions.GoodData (PaymentRecord.periodEnding) && PaymentsJournal.repo.EndDate.Enabled)
            {
                PaymentsJournal.repo.EndDate.TextValue = PaymentRecord.periodEnding;
                // PaymentsJournal.repo.EndDate.TypeKeys("<Tab>");
            }
			
            if ((Functions.GoodData(PaymentRecord.remitFrequency) && PaymentsJournal.repo.FrequencyButton.Enabled))
            {
                PaymentsJournal.repo.FrequencyButton.Click();
                SelectRemittingFrequency.repo.Frequency.SelectListItem(PaymentRecord.remitFrequency);
                SelectRemittingFrequency.repo.Select.Click();
            }
			
            if (PaymentsJournal.repo.TransContainer.Enabled)
            {
                // get the contents in the container
                List<List<string>>  lsContents = PaymentsJournal.repo.TransContainer.GetContents();
				
                //PaymentsJournal.Instance.TransContainer.ClickFirstCell();
                //PaymentsJournal.Instance.TransContainer.MoveRight();
                
                if (PaymentRecord.GridRows.Count != 0)
                {
                    for (int x = 0; x < lsContents.Count; x++)
                    {
                        PaymentsJournal.repo.TransContainer.ClickFirstCell();
                        PaymentsJournal.repo.TransContainer.MoveRight();
                        PaymentsJournal.repo.TransContainer.PressKeys("{Down " + x + "}");

                        for (int y = 0; y < PaymentRecord.GridRows.Count; y++)
                        {
                            if (Functions.GoodData (PaymentRecord.GridRows[y].remitName))
                            {
                                if (lsContents[x][0] == PaymentRecord.GridRows[y].remitName)	// enter data for the matching payroll liability
                                {
                                    if (Functions.GoodData(PaymentRecord.GridRows[y].adjustAccount))
                                    {
                                        PaymentsJournal.repo.TransContainer.PressKeys(PaymentRecord.GridRows[y].adjustAccount);
                                    }
                                    PaymentsJournal.repo.TransContainer.MoveRight();
                                    if (Functions.GoodData(PaymentRecord.GridRows[y].adjustment))
                                    {
                                        PaymentsJournal.repo.TransContainer.PressKeys(PaymentRecord.GridRows[y].adjustment);
                                    }
                                    PaymentsJournal.repo.TransContainer.MoveRight();
                                    PaymentsJournal.repo.TransContainer.PressKeys(PaymentRecord.GridRows[y].amount);
                                    PaymentsJournal.repo.TransContainer.MoveRight();
                                    break;	// go to the outter loop
                                }
                            }
                        }
                    }
                }
            }
			
            if (bSave)
            {
                PaymentsJournal.repo.Post.Click();
            }
        }
        
        
        public PAYMENT_PURCH _SA_ReadPayment()
		{
			return _SA_ReadPayment(null);
		}

		public PAYMENT_PURCH _SA_ReadPayment(PAYMENT_PURCH PaymentToRead) //  method will read all fields and store the data in a PAYMENT record
		{
			PAYMENT_PURCH P = new PAYMENT_PURCH();
			
			if (Functions.GoodData(PaymentToRead))	// else we assume you are on the correct payment
			{
				PaymentsJournal._SA_Open(PaymentToRead);
			}

            P.Vendor.name = PaymentsJournal.repo.VendorName.TextValue;
            P.paidBy = PaymentsJournal.repo.PaidBy.SelectedItem.ToString();
            if (PaymentsJournal.repo.PaidFromInfo.Exists())
			{
				P.paidFrom.acctNumber = PaymentsJournal.repo.PaidFrom.SelectedItemText;
			}
            if (PaymentsJournal.repo.ChequeNoInfo.Exists())
			{
				P.chequeNumber = PaymentsJournal.repo.ChequeNo.TextValue;
			}
			else if (PaymentsJournal.repo.DirectDepositNoInfo.Exists())
			{
				P.directDepositNo = PaymentsJournal.repo.DirectDepositNo.TextValue;
			}
			else
			{
				P.source = PaymentsJournal.repo.Source.TextValue;
			}
			P.TransDate = PaymentsJournal.repo.PaymentDate.TextValue;
			P.comment = PaymentsJournal.repo.Comment.TextValue;
			
			if (PaymentsJournal.repo.PrepaymentReferenceNoInfo.Exists())
			{
				P.PrePayRefNumber = PaymentsJournal.repo.PrepaymentReferenceNo.TextValue;
			}
			if (PaymentsJournal.repo.PrepaymentAmountInfo.Exists())
			{
				P.PrePayAmount = PaymentsJournal.repo.PrepaymentAmount.TextValue;
			}
			if (PaymentsJournal.repo.ExchangeRateInfo.Exists())
			{
				P.exchangeRate = PaymentsJournal.repo.ExchangeRate.TextValue;
			}
			
			List<List<string>>  lsContents = PaymentsJournal.repo.TransContainer.GetContents();
			if (lsContents.Count > 0)
			{
				bool bPrepayment = false;

                for (int x = 0; x < lsContents.Count; x++)
				{
					PAY_ROW PR = new PAY_ROW();
					if (lsContents[x][1].ToUpper().Trim() == "PREPAYMENTS")
					{
						bPrepayment =  true;	// only set this true once...from here out it is true
					}
					else
					{
						if (bPrepayment)
						{
							PAYMENT_PURCH preP = new PAYMENT_PURCH();
                            preP.PrePayRefNumber = lsContents[x][1];
							PR.PrePayment = preP;
						}
						else
						{
                            PR.Invoice.transNumber = lsContents[x][1];
						}
                        PR.discountTaken = lsContents[x][5];
                        PR.Amount = lsContents[x][6];
						P.GridRows.Add(PR);
					}
				}
			}
			return P;
		}
		
		
		public static PAYMENT_OTHER _SA_ReadPayment_Other()
        {
            return _SA_ReadPayment_Other(null);
        }

        public static PAYMENT_OTHER _SA_ReadPayment_Other(PAYMENT_OTHER PaymentToRead) //  method will read all fields and store the data in a PAYMENT_OTHER record
        {
            PAYMENT_OTHER P = new PAYMENT_OTHER();
			
            if (Functions.GoodData (PaymentToRead))
            {
                PaymentsJournal._SA_Open(PaymentToRead);
            }
			
            P.Vendor.name = PaymentsJournal.repo.VendorName.TextValue;
            P.paidBy = PaymentsJournal.repo.PaidBy.SelectedItem.Text;
            
            if (PaymentsJournal.repo.PaidFromInfo.Exists())
            {
                P.paidFrom.acctNumber = PaymentsJournal.repo.PaidFrom.SelectedItemText;
            }
            if (PaymentsJournal.repo.ChequeNoInfo.Exists())
            {
                P.chequeNumber = PaymentsJournal.repo.ChequeNo.TextValue;
            }
            else
            {
                P.source = PaymentsJournal.repo.Source.TextValue;
            }
            P.TransDate = PaymentsJournal.repo.PaymentDate.TextValue;
            P.comment = PaymentsJournal.repo.Comment.TextValue;
            P.reference = PaymentsJournal.repo.Reference.TextValue;
			
            List<List<string>>  lsContents = PaymentsJournal.repo.TransContainer.GetContents();
            if (lsContents.Count > 0)
            {
                for (int x = 0; x < lsContents.Count; x++)
                {
                    PAY_OTHER_ROW PR = new PAY_OTHER_ROW();
                    PR.account.acctNumber = lsContents[x][0];
                    PR.description = lsContents[x][1];
                    PR.amount = lsContents[x][2];
                    PR.taxCode.code = lsContents[x][3];

                    // read project allocation info
                    if (lsContents[x][4] == "true")
                    {
                        // bring up the allocation dialog
                        PaymentsJournal.repo.TransContainer.ClickFirstCell(); //Click ();
                        PaymentsJournal.repo.TransContainer.PressKeys("{Up " + lsContents.Count + "}");	// get to top of grid
                        PaymentsJournal.repo.TransContainer.PressKeys("{Down " + (x - 1) + "}");	// get to top of grid
                        PaymentsJournal.repo.Self.PressKeys("{Ctrl+Shift+A}");

                        // read allocation details (shouldn't we use the common function here?)
//                        if (s_desktop.Exists(ProjectAllocationDialog.PROJECTALLOCATIONDIALOG_LOC))
//                        {
//                            //List<PROJECT_ALLOCATION> PA = new List<PROJECT_ALLOCATION>() { };
//
//                            //List<string[]> containerLine = ProjectAllocationDialog.Instance.AllocationContainer.GetContents().ToList();
//                            //// Enter first field if not blank
//                            //if (containerLine[0][0] != "")
//                            //{
//                            //    while ((containerLine[0][0].Trim() != "") && (containerLine.Count >= 4))
//                            //    {
//                            //        PROJECT_ALLOCATION TempProj = new PROJECT_ALLOCATION();
//                            //        // assign recordset
//                            //        TempProj.Project.name = ConvertFunctions.CommaToText(containerLine[x][0]);
//                            //        TempProj.Amount = ConvertFunctions.CommaToText(containerLine[x][1]);
//                            //        TempProj.Percent = ConvertFunctions.CommaToText(containerLine[x][2]);
//                            //        PA.Add(TempProj);
//
//                            //        //int i;
//                            //        if (containerLine.Count > 4)
//                            //        {
//                            //            for (int i = 1; i < 4; i++)
//                            //            {
//                            //                containerLine.RemoveAt(1);
//                            //            }
//                            //        }
//                            //        else
//                            //        {
//                            //            containerLine.RemoveAt(1);
//                            //        }
//                            //    }
//                            //}
//                            //P.GridRows[x].Projects = PA;
//
//                            P.GridRows[x].Projects = ProjectAllocationDialog.Instance._SA_GetProjectAllocationDetails();
//                            ProjectAllocationDialog.Instance.Cancel.Click();
//                        }
                    }
                    P.GridRows.Add(PR);
                }
            }
            			
            return P;			
        }
        
        
        public static PAYMENT_REMIT _SA_ReadPayment_Remit()
        {
            return _SA_ReadPayment_Remit(null);
        }

        public static PAYMENT_REMIT _SA_ReadPayment_Remit(PAYMENT_REMIT PaymentToRead) //  method will read all fields and store the data in a PAYMENT_REMIT record
        {
            PAYMENT_REMIT P = new PAYMENT_REMIT();

            if (Functions.GoodData(PaymentToRead))
            {
                PaymentsJournal._SA_Open(PaymentToRead);
            }
        
            P.Vendor.name = PaymentsJournal.repo.VendorName.TextValue;
            if (PaymentsJournal.repo.PaidFromInfo.Exists())
            {
                P.paidFrom.acctNumber = PaymentsJournal.repo.PaidFrom.SelectedItemText;
            }
            P.chequeNumber = PaymentsJournal.repo.ChequeNo.TextValue;
            P.TransDate = PaymentsJournal.repo.PaymentDate.TextValue;
            P.comment = PaymentsJournal.repo.Comment.TextValue;
            P.reference = PaymentsJournal.repo.Reference.TextValue;

            PaymentsJournal.repo.FrequencyButton.Click();
            P.remitFrequency = SelectRemittingFrequency.repo.Frequency.SelectedItemText;
            SelectRemittingFrequency.repo.Cancel.Click();

            List<List<string>> lsContents = PaymentsJournal.repo.TransContainer.GetContents();
            if (lsContents.Count > 0)
            {
                int x;
                for (x = 0; x < lsContents.Count; x++)
                {
                    PAY_REMIT_ROW PR = new PAY_REMIT_ROW();
                    PR.remitName = ConvertFunctions.BlankStringToNULL(lsContents[x][0]);
                    //if (PR.remitName == "")
                    //{
                    //    PR.remitName = null;
                    //}
                    PR.amountOwing = ConvertFunctions.BlankStringToNULL(lsContents[x][1]);
                    //if (PR.amountOwing == "")
                    //{
                    //    PR.amountOwing = null;
                    //}
                    PR.adjustAccount = ConvertFunctions.BlankStringToNULL(lsContents[x][2]);
                    //if (PR.adjustAccount == "")
                    //{
                    //    PR.adjustAccount = null;
                    //}
                    PR.adjustment = ConvertFunctions.BlankStringToNULL(lsContents[x][3]);
                    //if (PR.adjustment == "")
                    //{
                    //    PR.adjustment = null;
                    //}
                    PR.amount = ConvertFunctions.BlankStringToNULL(lsContents[x][4]);
                    //if (PR.amount == "")
                    //{
                    //    PR.amount = null;
                    //}
                    P.GridRows.Add(PR);
                }
            }
            return (P);
        }
        
        
        public static PAYMENT_CREDIT_CARD _SA_ReadPayment_CreditCard()	// method will read all fields (cannot pull up existing though, so just unsaved transactions) and store the data in a PAYMENT_CREDIT_CARD record
		{
			PAYMENT_CREDIT_CARD P = new PAYMENT_CREDIT_CARD();
			
			P.Vendor.name = PaymentsJournal.repo.VendorName.TextValue;
			P.paidBy = PaymentsJournal.repo.PaidBy.SelectedItemText;
			if (PaymentsJournal.repo.PaidFromInfo.Exists())
			{
				P.paidFrom.acctNumber = PaymentsJournal.repo.PaidFrom.SelectedItemText;
			}
			if (PaymentsJournal.repo.ChequeNoInfo.Exists())
			{
				P.chequeNumber = PaymentsJournal.repo.ChequeNo.TextValue;
			}
			else
			{
				P.source = PaymentsJournal.repo.Source.TextValue;
			}
			P.TransDate = PaymentsJournal.repo.PaymentDate.TextValue;
			P.comment = PaymentsJournal.repo.Comment.TextValue;
			P.additionalFees = PaymentsJournal.repo.AdditionalFeesAndInterest.TextValue;
			P.amount = PaymentsJournal.repo.PaymentAmount.TextValue;
			
			return P;
		}
        
		// Recurring entries not done yet
//        public static void _SA_RecallPayment_Other(PAYMENT_OTHER PaymentRecord)	// recall recurring entry. other payment only
//        {
//            // recall and post a recurring entry
//
//            if (Functions.GoodData(PaymentRecord.recurringName))
//            {
//                Trace.WriteLine("Recalling the recurring entry " + PaymentRecord.recurringName + "");
//
//                if (!PaymentsJournal.repo.SelfInfo.Exists())
//                {
//                    PaymentsJournal._SA_Invoke();
//                }
//
//                PaymentsJournal.repo.Transaction.Select("Make Other Payment");
//
//                PaymentsJournal.repo.Self.PressKeys("{Ctrl+r}");	// invoke Recall Recurring dialog
//                RecallRecurringDialog._SA_SelectEntryToRecall(PaymentRecord.recurringName);
//                PaymentsJournal._SA_CreatePayment_Other(PaymentRecord);
//            }
//            else	// log the error
//            {
//                Functions.Verify(false, true, "recurring name found");
//            }
//        }
        
        
		public static void _SA_Open(PAYMENT Payment)	// for common code amongst all the payment types
		{
			
			if (!PaymentsJournal.repo.SelfInfo.Exists())
			{
				PaymentsJournal._SA_Invoke();
			}

            if (Payment.GetType() == typeof(PAYMENT_REMIT))
			{
				PaymentsJournal.repo.Transaction.Select("Pay Remittance");
			}
			else if (Payment.GetType() == typeof(PAYMENT_PURCH))
			{
				PaymentsJournal.repo.Transaction.Select("Pay Purchase Invoices");
			}
			else if (Payment.GetType() == typeof(PAYMENT_OTHER))
			{
				PaymentsJournal.repo.Transaction.Select("Make Other Payment");
			}
            else if (Payment.GetType() == typeof(PAYMENT_CREDIT_CARD))
            {
                Ranorex.Report.Info("Credit card payment can not be looked up");
                return;
            }
            else
            {
                Ranorex.Report.Info("Invalid payment type");
                return;
            }
			
			PaymentsJournal.repo.SearchDialog.Click();
			DialogJournalSearch._SA_SelectLookupDateRange();
			DialogJournalSearch.repo.Name.Select(Payment.Vendor.name);
			
			if (Functions.GoodData(Payment.chequeNumber))	// enter cheque number
			{
				DialogJournalSearch.repo.Source.TextValue = Payment.chequeNumber;
			}
			else if (Functions.GoodData(Payment.directDepositNo))	// enter source number
			{
                DialogJournalSearch.repo.Source.TextValue = Payment.directDepositNo;
			}
			
			else	// enter source number
			{
                DialogJournalSearch.repo.Source.TextValue = Payment.source;
			}
						
			DialogJournalSearch.repo.OK.Click();
			
		}
		
		
		public static void _SA_Delete(PAYMENT Payment)
		{
			// does one-step reversal by clicking on the reverse toolstrip button
			// the param has to be the type of PAYMENT, PAYMENT_OTHER, PAYMENT_CREDIT_CARD, or PAYMENT_REMIT
			string source;
			
			if (Functions.GoodData(Payment.chequeNumber))
			{
				source = Payment.chequeNumber;
			}
			else
			{
				source = Payment.source;
			}
			
			Ranorex.Report.Info(String.Format("Deleting the payment record {0} ", source));
			
			PaymentsJournal._SA_Open(Payment);	// load the transaction for deletion
			
			PaymentsJournal.repo.Reverse.Click();
			
			// handle the conformation message
			SimplyMessage._SA_HandleMessage(SimplyMessage.repo.Yes, SimplyMessage.sReversePaymentMsg, true);
		}
		
		
		public static void _SA_printTransaction(string sFileName)
        {
            _SA_printTransaction(sFileName, null);
        }

        public static void _SA_printTransaction(string sFileName, PAYMENT PaymentRecord)
        {
            // payment paid by cheque only

            if (Functions.GoodData(PaymentRecord))
            {
                if (PaymentRecord.GetType() != typeof(PAYMENT_CREDIT_CARD))
                {
                    PaymentsJournal._SA_Open(PaymentRecord);
                }
                Ranorex.Report.Info(String.Format("Printing payment {0} ", PaymentRecord.chequeNumber));
            }

            PaymentsJournal.repo.Print.Click();

            SimplyMessage._SA_HandleMessage(SimplyMessage.repo.Yes, SimplyMessage.sChqNumIsOutOfSequence);

            PrintToFileDialog.Print(sFileName);
			
            // Undo changes
            PaymentsJournal.ClickUndoChanges();

            SimplyMessage._SA_HandleMessage(SimplyMessage.repo.Yes, SimplyMessage.sDiscardJournalMsg);
        }
		
        
		public static void Common_HeaderSetup(PAYMENT PaymentRecord,bool bEdit)
		{
			if (bEdit)
			{
				PaymentsJournal._SA_Open(PaymentRecord);
			}
			else
			{
				// Might not be needed any longer
//				if (PaymentRecord.GetType() == typeof(PAYMENT_CREDIT_CARD))	// can only select a credit card on the pay credit card bill window
//				{
//					PaymentsJournal.repo.VendorName2.Select(PaymentRecord.Vendor.name);
//				}
//				else
//				{
//					PaymentsJournal.repo.VendorName.TextValue = PaymentRecord.Vendor.name;															
//					//PaymentsJournal.repo.VendorName.PressKeys("{Tab}");	// Must press tab, otherwise selecting the Paid By field may not work
//				}
				// open drop down to trigger outstanding invoices in grid
				PaymentsJournal.repo.VendorName2.Select(PaymentRecord.Vendor.name);
			}
			
			//  Validate if Add on the fly is present
			if (AddOnTheFly.repo.QuickAddInfo.Exists())
			{
				try
				{
					Functions.Verify (true, false, "Add on fly message appears");
				}
				catch
				{
					Ranorex.Report.Info("Add on the fly message not found");
				}
				AddOnTheFly.repo.QuickAdd.Click();
			}
			
			if (Functions.GoodData (PaymentRecord.paidBy) && (PaymentsJournal.repo.PaidBy.Enabled))
			{
                //Agent.SetOption(Options.PlaybackMode, ReplayMode.HighLevel);
                PaymentsJournal.repo.PaidBy.Select(PaymentRecord.paidBy);
                //PaymentsJournal.Instance.Window.TypeKeys("<Tab>");

                //// temporary workaround since selecting the combobox does not properly trigger the event
                //if (PaymentRecord.paidBy != "Cheque")
                //{
                //    if (PaymentsJournal.Instance.PaidBy.SelectedIndex != PaymentsJournal.Instance.PaidBy.ItemCount - 1)
                //    {
                //        PaymentsJournal.Instance.PaidBy.PressKeys("<Down>");
                //        PaymentsJournal.Instance.PaidBy.PressKeys("<UP>");
                //    }
                //    else
                //    {
                //        PaymentsJournal.Instance.PaidBy.PressKeys("<UP>");
                //        PaymentsJournal.Instance.PaidBy.PressKeys("<Down>");
                //    }
                //}
                //Agent.SetOption(Options.PlaybackMode, ReplayMode.LowLevel);
                
			}
			if (Functions.GoodData (PaymentRecord.paidFrom.acctNumber))
			{
				PaymentsJournal.repo.PaidFrom.Select (PaymentRecord.paidFrom.acctNumber );
			}
			if (PaymentsJournal.repo.ChequeNoInfo.Exists())
			{
				if (Functions.GoodData(PaymentRecord.chequeNumber))
				{
					PaymentsJournal.repo.ChequeNo.TextValue = PaymentRecord.chequeNumber;
				}
				// else use the default cheque number
			}
			else if(PaymentsJournal.repo.DirectDepositNoInfo.Exists())
			{
				if(Functions.GoodData(PaymentRecord.directDepositNo))
				{
					PaymentsJournal.repo.DirectDepositNo.TextValue = PaymentRecord.directDepositNo;
				}
			}
			else
			{
				PaymentsJournal.repo.Source.TextValue = PaymentRecord.source;
			}
			if (Functions.GoodData (PaymentRecord.TransDate))
			{
				PaymentsJournal.repo.PaymentDate.TextValue = PaymentRecord.TransDate;
			}
			if (Functions.GoodData (PaymentRecord.comment))
			{
				PaymentsJournal.repo.Comment.TextValue = PaymentRecord.comment;
			}
		}
		
		public static void _SA_Close()
		{
			repo.Self.Close();
	
		}
	}
	
	// Child windows
//	public static class AddOnTheFly
//	{
//		public static PaymentsJournalResFolders.AddOnTheFlyAppFolder repo = PaymentsJournalRes.Instance.AddOnTheFly;
//	}
	
	public static class SelectRemittingFrequency
	{
		public static PaymentsJournalResFolders.SelectRemittingFrequencyAppFolder repo = PaymentsJournalRes.Instance.SelectRemittingFrequency;
	}
}
