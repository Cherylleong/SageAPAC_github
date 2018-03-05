/*
 * Created by Ranorex
 * User: wonga01
 * Date: 10/13/2017
 * Time: 11:44
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
using System.Diagnostics;
using Sage50.Classes;

namespace Sage50.Classes
{
	/// <summary>
	/// Description of PayrollRunJournal.
	/// </summary>
	public class PayrollRunJournal
	{
		public static PayrollRunJournalResFolders.PayrollRunJournalAppFolder repo = PayrollRunJournalRes.Instance.PayrollRunJournal;
		
		
		public static void _SA_Invoke()
		{
			 if (Simply.isEnhancedView())
            {
                Simply.repo.Self.Activate();
                Simply.repo.EmployeeLink.Click();
                Simply.repo.PaychqRunIcon.Click();
            }
		}
		
		
		public static void _SA_MatchDefaults(PAYCHEQUE_RUN PayRunRecord)
        {
            if (!Functions.GoodData(PayRunRecord.accountPaidFrom))
            {
                PayRunRecord.accountPaidFrom.acctNumber = PayrollRunJournal.repo.PaidFrom.SelectedItemText;
            }           
            if (!Functions.GoodData(PayRunRecord.payPeriodFrequency))
            {
                PayRunRecord.payPeriodFrequency = PayrollRunJournal.repo.PayPeriodFrequency.SelectedItemText;
            }
            if (!Functions.GoodData(PayRunRecord.chequeNumber))
            {
                PayRunRecord.chequeNumber = PayrollRunJournal.repo.ChequeNumber.TextValue;
            }
            if (!Functions.GoodData(PayRunRecord.directDepositNumber))
            {
                PayRunRecord.directDepositNumber = PayrollRunJournal.repo.DirectDepositNumber.TextValue;
            }
            if (!Functions.GoodData(PayRunRecord.periodEndDate))
            {
                PayRunRecord.chequeNumber = PayrollRunJournal.repo.ChequeNumber.TextValue;
            }
            if (!Functions.GoodData(PayRunRecord.chequeDate))
            {
                PayRunRecord.chequeDate = PayrollRunJournal.repo.ChequeDate.TextValue;
            }
        }
		
		
		public static void _SA_Create(PAYCHEQUE_RUN PayRunRecord)
        {
            _SA_Create(PayRunRecord, true, false);
        }

        public static void _SA_Create(PAYCHEQUE_RUN PayRunRecord, Boolean bPost)
        {
            _SA_Create(PayRunRecord, bPost, false);
        }

        public static void _SA_Create(PAYCHEQUE_RUN PayRunRecord, Boolean bPost, Boolean bPostAll)
        {			
        	if (!PayrollRunJournal.repo.SelfInfo.Exists())
            {
                PayrollRunJournal._SA_Invoke ();
            }
			
            PayrollRunJournal._SA_MatchDefaults(PayRunRecord);
			
            // Set Account Paid From
            if (Functions.GoodData (PayRunRecord.accountPaidFrom.acctNumber))
            {
                PayrollRunJournal.repo.PaidFrom.Select(PayRunRecord.accountPaidFrom.acctNumber);
            }
			          
            if (Functions.GoodData (PayRunRecord.payPeriodFrequency))
            {
                PayrollRunJournal.repo.PayPeriodFrequency.Select(PayRunRecord.payPeriodFrequency);
            }
			
            if (Functions.GoodData (PayRunRecord.chequeNumber))
            {
                PayrollRunJournal.repo.ChequeNumber.TextValue = PayRunRecord.chequeNumber;
            }
			
            if (Functions.GoodData (PayRunRecord.directDepositNumber))
            {
                PayrollRunJournal.repo.DirectDepositNumber.TextValue = PayRunRecord.directDepositNumber;
            }
			
            if (Functions.GoodData (PayRunRecord.periodEndDate))
            {
                PayrollRunJournal.repo.PeriodEndDate.TextValue = PayRunRecord.periodEndDate;
            }
			
            if (Functions.GoodData (PayRunRecord.chequeDate))
            {
                PayrollRunJournal.repo.ChequeDate.TextValue = PayRunRecord.chequeDate;
            }
			
//            //  restore container to defaults
//            PayrollRunJournal.Instance.Window.TypeKeys ("<Alt+v>");
//            PayrollRunJournal.Instance.Window.TypeKeys ("w");
			
            if (bPost)
            {				
                if(bPostAll)
                {					                    
                    // click on first cell to move focus
                    PayrollRunJournal.repo.ChequeTable.SelectCell("Row 0", 0); // Click(MouseButton.Left, new Point (3, 33));
					
                    for (int x = 0; x < PayrollRunJournal.repo.ChequeTable.Rows.Count; x++)
                    {
						
                        // if(PayrollRunJournal.repo.ChequeTable.DataGridItem(GetDataGridItemLocator(CHEQUETABLE_LOC, 0, x)).Text.ToUpper().Contains("FALSE"))
                        if (PayrollRunJournal.repo.ChequeTable.GetCell("Row 0", x).Equals(false)) // .ToString().Contains("FALSE"))
                        {
                            PayrollRunJournal.repo.ChequeTable.PressKeys("{Space}");
                        }
                        // Move to next row
                        PayrollRunJournal.repo.ChequeTable.PressKeys("{Down}");                        
                    }
                }

                PayrollRunJournal.repo.Post.Click();
            }
        }
	}
}
