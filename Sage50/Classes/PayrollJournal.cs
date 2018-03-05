/*
 * Created by Ranorex
 * User: wonga01
 * Date: 10/11/2017
 * Time: 15:54
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
	/// Description of PayrollJournal.
	/// </summary>
	public class PayrollJournal
	{			
		public static PayrollJournalResFolders.PayrollJournalAppFolder repo = PayrollJournalRes.Instance.PayrollJournal;
		public static PayrollJournalResFolders.ProjAllocByHourAppFolder projByHourRepo = PayrollJournalRes.Instance.ProjAllocByHour;		
		
		
		public static void _SA_Invoke()
        {
            _SA_Invoke(true);
        }
        public static void _SA_Invoke(bool bSetActive)
        {
        	if (Simply.isEnhancedView())
        	{
        		Simply.repo.Self.Activate();
                Simply.repo.EmployeeLink.Click();
        		Simply.repo.PaychequeIcon.Click();
        	}

            if (bSetActive) // set the journal window active after closing any messages or enter a payroll id
            {
//                //Handle new HR message
//                //SimplyMessage._SA_HandleMessage(SimplyMessage.DoNotShowAgainButton, SimplyMessage._Msg_SimplyAccountingHRManager, FALSE, TRUE)
//                // message tags changed, child of simply now, so have to do this way
//                if (s_desktop.Exists(HRManager.HRMANAGER_LOC))
//                {
//                    HRManager.Instance.DoNotShowMeAgain.Click();
//                }
//
//                // this might be a temporary fix for new payroll feature
//                if (s_desktop.Exists(PayrollExpiry.PAYROLLEXPIRY_LOC))
//                {
//                    PayrollExpiry.Instance.OK.Click();
//                }
//
//                // Enter payroll id if necessary
//                if (s_desktop.Exists(UnlockPayroll2.UNLOCKPAYROLL2_LOC))
//                {
//                    UnlockPayroll2.Instance.EnterPayrollID.Click();
//                    EnterPayrollID2.Instance.ClientID.SetText("1234599850");
//                    EnterPayrollID2.Instance.PayrollRadio.Select("Use this Payroll ID");
//                    EnterPayrollID2.Instance.PayrollID.SetText("PK00VNB73C");
//                    EnterPayrollID2.Instance.OK.Click();
//                    System.Threading.Thread.Sleep(2000);
//                    // temp code till proper key codes are generated
//                    if (s_desktop.Exists(PayrollExpiry.PAYROLLEXPIRY_LOC))
//                    {
//                        PayrollExpiry.Instance.OK.Click();
//                        EnterPayrollID2.Instance.Cancel.Click();
//                        UnlockPayroll2.Instance.Close.Click();
//
//                    }
//                    else
//                    {
//                        // SimplyMessage._SA_HandleMessage(SimplyMessage.OK)	// not working
//                        if (ThankYouForSubscribing2.Instance.Window != null)
//                        {
//                            ThankYouForSubscribing2.Instance.OK.Click();	// message changed to parent of unlock
//                        }
//                    }
//                }

//                //Handle new HR message
//                //SimplyMessage._SA_HandleMessage(SimplyMessage.DoNotShowAgainButton, SimplyMessage._Msg_SimplyAccountingHRManager, FALSE, TRUE)
//                // message tags changed, child of simply now, so have to do this way
//                if (s_desktop.Exists(HRManager.HRMANAGER_LOC))
//                {
//                    HRManager.Instance.DoNotShowMeAgain.Click();
//                }

				PayrollJournal.repo.Self.Activate();
            }
        }
        
        
        public static void _SA_Close()
        {
//        	if (s_desktop.Exists(PayrollExpiry.PAYROLLEXPIRY_LOC))	// handle payroll expiry message
//            {
//                PayrollExpiry.Instance.OK.Click();
//            }
			PayrollJournal.repo.Self.Close();	
		
        }
        
        
        public static void _SA_MatchDefaults(PAYCHEQUE PayRecord)
        {
            if (!Functions.GoodData(PayRecord.directDeposit))
            {
                PayRecord.directDeposit = false;
            }
            if (!Functions.GoodData(PayRecord.paychequeNumber))
            {
                PayRecord.paychequeNumber = PayrollJournal.repo.PaychequeNumber.TextValue;
            }
        }
        
        
        public static void _SA_Open(PAYCHEQUE PayRecord)
        {
        	if (!PayrollJournal.repo.SelfInfo.Exists())
            {
                PayrollJournal._SA_Invoke();
            }

            // select the lookup date range            
            PayrollJournal.repo.Adjust.Click();            
            DotNetJournalSearch._SA_SelectLookupDateRange();

            // select the employee
            if (Functions.GoodData(PayRecord.employee.name))
            {
                DotNetJournalSearch.repo.Name.Select(PayRecord.employee.name);
            }

            // enter the paycheque number
            if (Functions.GoodData(PayRecord.paychequeNumber))
            {
                DotNetJournalSearch.repo.Source.TextValue = PayRecord.paychequeNumber;
            }

            DotNetJournalSearch.repo.OK.Click();
        }
        
        
        public static void _SA_Create(PAYCHEQUE PayRecord)
        {
            _SA_Create(PayRecord, true, false, false);
        }

        public static void _SA_Create(PAYCHEQUE PayRecord, bool bSave)
        {
            _SA_Create(PayRecord, bSave, false, false);
        }

        public static void _SA_Create(PAYCHEQUE PayRecord, bool bSave, bool bEdit)
        {
            _SA_Create(PayRecord, bSave, bEdit, false);
        }

        public static void _SA_Create(PAYCHEQUE PayRecord, bool bSave, bool bEdit, bool bRecur)
        {
            bool bCheckGlobalProject = false;

            if (!PayrollJournal.repo.SelfInfo.Exists())
            {
                PayrollJournal._SA_Invoke();
            }

            // Can't edit employee name when adjusting
            if (!bEdit)
            {
                PayrollJournal.repo.EmployeeName.Select(PayRecord.employee.name);	// change to settext so it handles partial name
                PayrollJournal.repo.Self.PressKeys("{Tab}");
            }

            if (!Variables.bUseDataFiles)	// if external data files are not used
            {
                PayrollJournal._SA_MatchDefaults(PayRecord);
            }
            else	// if currently using auto tax calculation, switch to manual
            {
                PayrollJournal.repo.ManualEnterTax.Click();                
            }

            // Print statements
            if (!bEdit)
            {
                if (bRecur)
                {                   	
                   	Ranorex.Report.Info(String.Format("Storing recurring paycheque entry {0} {1}", PayRecord.recurringName, PayRecord.recurringFrequency));
                }
                else
                {
                    Ranorex.Report.Info(String.Format("Creating Paycheque {0}", PayRecord.paychequeNumber));
                }
            }
            else
            {
                Ranorex.Report.Info(String.Format("Adjusting Paycheque {0}", PayRecord.paychequeNumber));
            }

            // Set Account Paid From
            if (Functions.GoodData(PayRecord.accountPaidFrom.acctNumber))
            {
                PayrollJournal.repo.AccountPaidFrom.Select(PayRecord.accountPaidFrom.acctNumber);
            }

            if (Functions.GoodData(PayRecord.paychequeNumber))
            {
                PayrollJournal.repo.PaychequeNumber.TextValue = PayRecord.paychequeNumber;
            }
            if (Functions.GoodData(PayRecord.paychequeDate))
            {
                PayrollJournal.repo.PaychequeDate.TextValue = PayRecord.paychequeDate;
                PayrollJournal.repo.PaychequeDate.PressKeys("{Tab}");
            }

            // set this before match defaults for it changes the default deposit number
            PayrollJournal.repo.DirectDeposit.SetState(PayRecord.directDeposit);

//            if (Variables.productVersion != "Canadian") // i.e. US
//            {
//                if (Functions.GoodData(PayRecord.periodStartDate))
//                {
//                    PayrollJournal.Instance.PeriodStart.SetText(PayRecord.periodStartDate);	// US only
//                    PayrollJournal.Instance.PeriodStart.TypeKeys("<Tab>");
//                }
//            }
            if (Functions.GoodData(PayRecord.periodEndingDate))
            {
                PayrollJournal.repo.PeriodEnd.TextValue = PayRecord.periodEndingDate;
                PayrollJournal.repo.PeriodEnd.PressKeys("{Tab}");
            }

            PayrollJournal.repo.Income.Tab.Click();

//            // restore window
//            PayrollJournal.Instance.Window.TypeKeys("<ALT+V>");
//            PayrollJournal.Instance.Window.TypeKeys("w");

            // Income Tab
            if (PayRecord.Earnings.Count != 0)                
            {                
                // List<string[]> lsEarnings = ConvertFunctions.DataGridItemsToListOfString(PayrollJournal.Instance.EarningIncomeGrid.Items, PayrollJournal.Instance.EarningIncomeGrid.ColumnCount);
                List<List<string>> lsEarnings = PayrollJournal.repo.Income.EarningIncomeGrid.GetContents();
                                
                if (!(Variables.bUseDataFiles))
                {
                    //make a list of all earnings in correct order                    
                    List<PAYCHEQUE_EARNINGS> lTempEarnings = new List<PAYCHEQUE_EARNINGS>(){};

                    bool bFound;
                    for (int x = 0; x < lsEarnings.Count; x++)	// go through all the rows in the table
                    {
                        bFound = false;
                        int y; // to be used outside the loop
                        for (y = 0; y < PayRecord.Earnings.Count; y++)
                        {
                            if (PayRecord.Earnings[y].earningName == lsEarnings[x][0])
                            {
                                bFound = true;
                                break;
                            }
                        }
                        if (bFound)	// if payrecord has that earning listed, then move that into the  temp list
                        {
                            lTempEarnings.Add(PayRecord.Earnings[y]);
                        }
                        else	// we will just put in a place holder for that earning to the list
                        {
                            PAYCHEQUE_EARNINGS PE = new PAYCHEQUE_EARNINGS();
                            PE.earningName = lsEarnings[x][0];
                            lTempEarnings.Add(PE);
                        }
                    }
                    PayRecord.Earnings = lTempEarnings;
                }

                //at this point we have a list of earnings that is complete, either from the data files or from code
                // enter data into container here
                for (int x = 0; x < lsEarnings.Count; x++)	// number of rows available
                {
                    if (x >= PayRecord.Earnings.Count)
                    {
                        break;
                    }

                    if (Functions.GoodData (PayRecord.Earnings[x].hours) && PayRecord.Earnings[x].hours != "")
                    {
                    	PayrollJournal.repo.Income.EarningIncomeGrid.SelectCell("Hours", x); // .SelectItem(1, x);
                        PayrollJournal.repo.Income.EarningIncomeGrid.PressKeys(PayRecord.Earnings[x].hours); 
                    }
                    if (Functions.GoodData (PayRecord.Earnings[x].pieces) && PayRecord.Earnings[x].pieces != "")
                    {
                        PayrollJournal.repo.Income.EarningIncomeGrid.SelectCell("Pieces", x);
                        PayrollJournal.repo.Income.EarningIncomeGrid.PressKeys(PayRecord.Earnings[x].pieces);	// Enter pieces					
                    }
                    if (Functions.GoodData (PayRecord.Earnings[x].Amount) && PayRecord.Earnings[x].Amount != "")
                    {
                        PayrollJournal.repo.Income.EarningIncomeGrid.SelectCell("This Period", x);
                        PayrollJournal.repo.Income.EarningIncomeGrid.PressKeys(PayRecord.Earnings[x].Amount);	// Enter amount (this period
                    }
                }
            }
			
            // Other Incomes             
            if (PayRecord.Incomes.Count != 0)           
            {
                // List<string[]> lsOtherIncomes = ConvertFunctions.DataGridItemsToListOfString(PayrollJournal.Instance.OtherIncomeGrid.Items, PayrollJournal.Instance.OtherIncomeGrid.ColumnCount);
                List<List<string>> lsOtherIncomes = PayrollJournal.repo.Income.OtherIncomeGrid.GetContents();
                
                //make a list of all income in correct order
                List<PAYCHEQUE_OTHER_INCOMES>  lTempIncomes = new List<PAYCHEQUE_OTHER_INCOMES>() {};
                for (int x = 0; x < lsOtherIncomes.Count; x++)	// go through all the rows in the table
                {
                    bool bFound = false;
                    int y;  // to be used outside the loop
                    for (y = 0; y < PayRecord.Incomes.Count; y++)
                    {
                        if (PayRecord.Incomes[y].incomeName == lsOtherIncomes[x][0])
                        {
                            bFound = true;
                            break;
                        }
                    }
                    if (bFound)	// if payrecord has that income listed, then move that into the  temp list
                    {
                        lTempIncomes.Add(PayRecord.Incomes[y]);
                    }
                    else	// we will just put in a place holder for that income to the list
                    {
                        PAYCHEQUE_OTHER_INCOMES POI = new PAYCHEQUE_OTHER_INCOMES();
                        POI.incomeName = lsOtherIncomes[x][0];
                        lTempIncomes.Add(POI);
                    }
                }
                PayRecord.Incomes = lTempIncomes;
				
                //at this point we have a list of income that is complete, either from the data files or from code
                for (int x = 0; x < lTempIncomes.Count; x++)	// number of rows available
                {
                    if (x > PayRecord.Incomes.Count)
                    {
                        break;
                    }

                    if (Functions.GoodData(PayRecord.Incomes[x].Amount) && PayRecord.Incomes[x].Amount != "")
                    {
                        PayrollJournal.repo.Income.OtherIncomeGrid.SelectCell("This Period", x);
                        PayrollJournal.repo.Income.OtherIncomeGrid.PressKeys(PayRecord.Incomes[x].Amount);
                    }
                }
            }
						            
            // Project
            if ( PayRecord.Projects.Count != 0)
            {
                // get global settings if haven't already
                if(!bCheckGlobalProject)
                {
                    Settings._SA_Get_AllProjectSettings();
                    PayrollJournal.repo.Self.Activate();
                }
				
                PayrollJournal.repo.AllocateToProjects.Click();

                if (ProjectAllocationDialog.repo.SelfInfo.Exists())
                {
                    ProjectAllocationDialog._SA_EnterProjectAllocationDetails(PayRecord.Projects);
                }
            }

            if (PayRecord.Vacations.Count != 0)
            {
                PayrollJournal.repo.Vacation.Tab.Click();

                //figure out what the rows are in the  table
                // List<string[]> lsVacation = ConvertFunctions.DataGridItemsToListOfString(PayrollJournal.Instance.VacationGrid.Items, PayrollJournal.Instance.VacationGrid.ColumnCount);
                List<List<string>> lsVacation = PayrollJournal.repo.Vacation.VacationGrid.GetContents();
				
                List<string> lsVacationNames = new List<string>() { };

                for (int x = 0; x < lsVacation.Count; x++)
                {
                    lsVacationNames.Add(lsVacation[x][0]);
                }

                if (!Variables.bUseDataFiles)	// we need to match the amount with deduction name
                {
                    //make a list of all earnings in correct order
                    List<PAYCHEQUE_VACATION> lTempVacations = new List<PAYCHEQUE_VACATION>() { };
                    for (int x = 0; x < lsVacationNames.Count; x++)	// go through all the rows in the table
                    {
                        bool bFound = false;
                        int y;  // to be used outside the loop
                        for (y = 0; y < PayRecord.Vacations.Count; y++)
                        {
                            if (PayRecord.Vacations[y].vacationType == lsVacationNames[x])
                            {
                                bFound = true;
                                break;
                            }
                        }
                        if (bFound)	// if payrecord has that deduction listed, then move that into the  temp list
                        {
                            lTempVacations.Add(PayRecord.Vacations[y]);
                        }
                        else	// we will just put in a place holder for that deduction to the list
                        {
                            PAYCHEQUE_VACATION PV = new PAYCHEQUE_VACATION();
                            PV.vacationType = lsVacationNames[x];
                            lTempVacations.Add(PV);
                        }
                    }
                    PayRecord.Vacations = lTempVacations;
                }
                // else we don't care so we will just enter the deduction amount in the same sequence as they were listed in the data files
                // so it saves us time on adding the names, which can be customized by the way, back to the data files

                //at this point we have a list of deduction that is complete, either from the data files or from code
                for (int x = 0; x < lsVacationNames.Count; x++)	// number of rows available
                {
                    if (x >= PayRecord.Vacations.Count)
                    {
                        break;
                    }

                    if (Functions.GoodData(PayRecord.Vacations[x].hours) && PayRecord.Vacations[x].hours != "" && PayRecord.Vacations[x].hours != "0")
                    {
                        PayrollJournal.repo.Vacation.VacationGrid.SelectCell("Hours", x);
                        PayrollJournal.repo.Vacation.VacationGrid.PressKeys(PayRecord.Vacations[x].hours);	// Enter hours
                    }
           
                    if (Functions.GoodData(PayRecord.Vacations[x].amount) && PayRecord.Vacations[x].amount != "" && PayRecord.Vacations[x].amount != "0")
                    {
                        PayrollJournal.repo.Vacation.VacationGrid.SelectCell("Amount", x);
                        PayrollJournal.repo.Vacation.VacationGrid.PressKeys(PayRecord.Vacations[x].amount);	// Enter hours
                    }
                }
            }
			
            if (PayRecord.Deductions.Count != 0)            
            {
                PayrollJournal.repo.Deductions.Tab.Click();
				
                //figure out what the rows are in the table
                // List<string[]> lsDeductions = ConvertFunctions.DataGridItemsToListOfString(PayrollJournal.Instance.DeductionGrid.Items, PayrollJournal.Instance.DeductionGrid.ColumnCount);
                List<List<string>> lsDeductions = PayrollJournal.repo.Deductions.DeductionGrid.GetContents();
				              				
                if (!Variables.bUseDataFiles)	// we need to match the amount with deduction name
                {
                    //make a list of all earnings in correct order
                    List<PAYCHEQUE_DEDUCTIONS>  lTempDeductions = new List<PAYCHEQUE_DEDUCTIONS>(){};
                    for (int x = 0; x < lsDeductions.Count; x++)	// go through all the rows in the table
                    {
                        bool bFound = false;
                        int y;  // to be used outside the loop
                        for (y = 0; y < PayRecord.Deductions.Count; y++)
                        {
                            if (PayRecord.Deductions[y].deductionName == lsDeductions[x][0])
                            {
                                bFound = true;
                                break;
                            }
                        }
                        if (bFound)	// if payrecord has that deduction listed, then move that into the  temp list
                        {
                            lTempDeductions.Add(PayRecord.Deductions[y]);
                        }
                        else	// we will just put in a place holder for that deduction to the list
                        {
                            PAYCHEQUE_DEDUCTIONS PD = new PAYCHEQUE_DEDUCTIONS();
                            PD.deductionName = lsDeductions[x][0];
                            lTempDeductions.Add(PD);
                        }
                    }
                    PayRecord.Deductions = lTempDeductions;
                }
                // else we don't care so we will just enter the deduction amount in the same sequence as they were listed in the data files
                // so it saves us time on adding the names, which can be customized by the way, back to the data files
				
                //at this point we have a list of deduction that is complete, either from the data files or from code
                for (int x = 0; x < lsDeductions.Count; x++)	// number of rows available
                {
                    if (x > PayRecord.Deductions.Count)
                    {
                        break;
                    }
					
                    if(Functions.GoodData(PayRecord.Deductions[x].Amount) && PayRecord.Deductions[x].Amount != "" && PayRecord.Deductions[x].Amount != "0")
                    {
                        PayrollJournal.repo.Deductions.DeductionGrid.SelectCell("This Period",x);
                        PayrollJournal.repo.Deductions.DeductionGrid.PressKeys(PayRecord.Deductions[x].Amount);	// Enter hours
                    }
                }			
            }
			
            // Taxes Tab
            if (PayRecord.Taxes.Count != 0)          
            {				                
                PayrollJournal.repo.ManualEnterTax.Click();	// turn on the manual calculation so we can enter amounts next
                PayrollJournal.repo.Taxes.Tab.Click();
				
                //figure out what the rows are in the  table
                // List<string[]> lsTaxes = ConvertFunctions.DataGridItemsToListOfString(PayrollJournal.Instance.TaxGrid.Items, PayrollJournal.Instance.TaxGrid.ColumnCount);
                List<List<string>> lsTaxes = PayrollJournal.repo.Taxes.TaxesGrid.GetContents();
				                               				
                //make a list of all earnings in correct order
                List<PAYCHEQUE_TAXES>  lTempTaxes= new List<PAYCHEQUE_TAXES>(){};

                for (int x = 0; x < lsTaxes.Count; x++)	// go through all the rows in the table
                {
                    bool bFound = false;
                    int y;  // to be used outside the loop
                    for (y = 0; y < PayRecord.Taxes.Count; y++)
                    {
                        if (PayRecord.Taxes[y].taxName == lsTaxes[x][0])
                        {
                            bFound = true;
                            break;
                        }
                    }
                    if (bFound)	// if payrecord has that deduction listed, then move that into the  temp list
                    {
                        lTempTaxes.Add(PayRecord.Taxes[y]);
                    }
                    else	// we will just put in a place holder for that deduction to the list
                    {
                        PAYCHEQUE_TAXES PT = new PAYCHEQUE_TAXES();
                        PT.taxName = lsTaxes[x][0];
                        lTempTaxes.Add(PT);
                    }
                }
                PayRecord.Taxes = lTempTaxes;
				
                //at this point we have a list of taxes that is complete, either from the data files or from code
                for (int x = 0; x < lsTaxes.Count; x++)	// number of rows available
                {
                    if (x > PayRecord.Taxes.Count)
                    {
                        break;
                    }
                    //click in first column for each row or focus is lost
					
                    if (Functions.GoodData(PayRecord.Taxes[x].amount) && PayRecord.Taxes[x].amount != "" && PayRecord.Taxes[x].amount != "0")
                    {
                        PayrollJournal.repo.Taxes.TaxesGrid.SelectCell("This Period", x);
                        PayrollJournal.repo.Taxes.TaxesGrid.PressKeys(PayRecord.Taxes[x].amount);
                    }
                }
            }
			
            // User Defined Expenses Tab. Tab may not exists so does the check first
            // if(PayrollJournal.Instance.TabCtrl.ItemCount == 6) // 6 if all tabs are there
            if (PayrollJournal.repo.UserDefinedExpenses.Tab.Visible)
            {
                if (Functions.GoodData(PayRecord.UserDefExpenses) && PayRecord.UserDefExpenses.Count != 0)                
                {
                    PayrollJournal.repo.UserDefinedExpenses.Tab.Click();
					
                    //figure out what the rows are in the  table
                    // List<string[]> lsExpenses = ConvertFunctions.DataGridItemsToListOfString(PayrollJournal.Instance.ExpenseGrid.Items, PayrollJournal.Instance.ExpenseGrid.ColumnCount);                     					
                    List<List<string>> lsExpenses = PayrollJournal.repo.UserDefinedExpenses.ExpensesGrid.GetContents();
                    
                    if (!Variables.bUseDataFiles)
                    {
                        //make a list of all earnings in correct order
                        List<PAYCHEQUE_USER_DEFINED_EXPENSES>  lTempUserDefExps= new List<PAYCHEQUE_USER_DEFINED_EXPENSES>() {};

                        for (int x = 0; x < lsExpenses.Count; x++)	// go through all the rows in the table
                        {
                            bool bFound = false;
                            int y;  // to be used outside the loop
                            for (y = 0; y < PayRecord.UserDefExpenses.Count; y++)
                            {
                                if (PayRecord.UserDefExpenses[y].expName == lsExpenses[x][0])
                                {
                                    bFound = true;
                                    break;
                                }
                            }
                            if (bFound)	// if payrecord has that deduction listed, then move that into the  temp list
                            {
                                lTempUserDefExps.Add(PayRecord.UserDefExpenses[y]);
                            }
                            else	// we will just put in a place holder for that deduction to the list
                            {
                                PAYCHEQUE_USER_DEFINED_EXPENSES PDE = new PAYCHEQUE_USER_DEFINED_EXPENSES();
                                PDE.expName = lsExpenses[x][0];
                                lTempUserDefExps.Add(PDE);
                            }
                        }
                        PayRecord.UserDefExpenses = lTempUserDefExps;
						
                    }
                    //at this point we have a list of taxes that is complete, either from the data files or from code
                    for (int x = 0; x < lsExpenses.Count; x++)	// number of rows available
                    {
                        if (x > PayRecord.UserDefExpenses.Count)
                        {
                            break;
                        }
						
                        if (Functions.GoodData (PayRecord.UserDefExpenses[x].amount) && PayRecord.UserDefExpenses[x].amount != "" && PayRecord.UserDefExpenses[x].amount != "0")
                        {
                            PayrollJournal.repo.UserDefinedExpenses.ExpensesGrid.SelectCell("This Period", x);
                            PayrollJournal.repo.UserDefinedExpenses.ExpensesGrid.PressKeys(PayRecord.UserDefExpenses[x].amount);	// Enter hours
                        }
                    }
                }
            }
			
            // Entitlements Tab
            if (Functions.GoodData (PayRecord.hrsWorkedInPayPeriod) || PayRecord.Entitlements.Count != 0) // or use PayRecord.Entitlements.Count != 0 instead as GoodData is not working
            {
            	PayrollJournal.repo.Entitlements.Tab.Click();
				
                if (Functions.GoodData (PayRecord.hrsWorkedInPayPeriod))
                {
                    PayrollJournal.repo.Entitlements.HoursWorkedThisPeriod.TextValue = PayRecord.hrsWorkedInPayPeriod;
                    PayrollJournal.repo.Entitlements.HoursWorkedThisPeriod.PressKeys("{Tab}");
                }
				
                if (PayRecord.Entitlements.Count != 0)
                {
                    //figure out what the rows are in the  table
                    // List<string[]> lsEntitlements = ConvertFunctions.DataGridItemsToListOfString(PayrollJournal.Instance.EntitlementGrid.Items, PayrollJournal.Instance.EntitlementGrid.ColumnCount);
                    List<List<string>> lsEntitlements = PayrollJournal.repo.Entitlements.EntitlementGrid.GetContents();
					                                       
                    if (!Variables.bUseDataFiles)
                    {
                        //make a list of all earnings in correct order
                        List<PAYCHEQUE_ENTITLEMENTS>  lTempEntitlements= new List<PAYCHEQUE_ENTITLEMENTS>(){};

                        for (int x = 0; x < lsEntitlements.Count; x++)	// go through all the rows in the table
                        {
                            bool bFound = false;
                            int y;  // to be used outside the loop
                            for (y = 0; y < PayRecord.Entitlements.Count; y++)
                            {
                                if (PayRecord.Entitlements[y].dayName == lsEntitlements[x][0])
                                {
                                    bFound = true;
                                    break;
                                }
                            }
                            if (bFound)	// if payrecord has that deduction listed, then move that into the  temp list
                            {
                                lTempEntitlements.Add(PayRecord.Entitlements[y]);
                            }
                            else	// we will just put in a place holder for that deduction to the list
                            {
                                PAYCHEQUE_ENTITLEMENTS PET = new PAYCHEQUE_ENTITLEMENTS();
                                PET.dayName = lsEntitlements[x][0];
                                lTempEntitlements.Add(PET);
                            }
                        }
                        PayRecord.Entitlements = lTempEntitlements;
                    }
                    //at this point we have a list of taxes that is complete, either from the data files or from code
                    for (int x = 0; x < lsEntitlements.Count; x++)	// number of rows available
                    {
                        if (x > PayRecord.Entitlements.Count)
                        {
                            break;
                        }
						
                        if (Functions.GoodData (PayRecord.Entitlements[x].daysEarned) && PayRecord.Entitlements[x].daysEarned != "" && PayRecord.Entitlements[x].daysEarned != "0")
                        {
                        	PayrollJournal.repo.Entitlements.EntitlementGrid.SelectCell("Days Earned", x);
                            PayrollJournal.repo.Entitlements.EntitlementGrid.PressKeys("{Tab}");
                            PayrollJournal.repo.Entitlements.EntitlementGrid.PressKeys("{Shift down}{Tab}{Shift up}");
                            PayrollJournal.repo.Entitlements.EntitlementGrid.PressKeys(PayRecord.Entitlements[x].daysEarned);
                        }
						
                        if (Functions.GoodData (PayRecord.Entitlements[x].daysTaken) && PayRecord.Entitlements[x].daysTaken != "" && PayRecord.Entitlements[x].daysTaken != "0")
                        {
                            //PayrollJournal.repo.Entitlements.EntitlementGrid.PressKeys("{Tab}");
                            PayrollJournal.repo.Entitlements.EntitlementGrid.SelectCell("Days Taken", x);
                            PayrollJournal.repo.Entitlements.EntitlementGrid.PressKeys(PayRecord.Entitlements[x].daysTaken);
                        }
                    }
                }
            }
			
            // Tips Tab
            if (PayRecord.Tips.Count != 0)            
            {
            	PayrollJournal.repo.QuebecTips.Tab.Click();
				
                //figure out what the rows are in the  table
                // List<string[]> lsTips = ConvertFunctions.DataGridItemsToListOfString(PayrollJournal.Instance.QuebecTipsGrid.Items, PayrollJournal.Instance.QuebecTipsGrid.ColumnCount);
                List<List<string>> lsTips = PayrollJournal.repo.QuebecTips.QuebecTipsGrid.GetContents();
              				
                if (!(Variables.bUseDataFiles))
                {
                    //make a list of all earnings in correct order
                    List<PAYCHEQUE_TIPS>  lTempTips = new List<PAYCHEQUE_TIPS>(){};

                    for (int x = 0; x < lsTips.Count; x++)	// go through all the rows in the table
                    {
                        bool bFound = false;
                        int y;  // to be used outside the loop
                        for (y = 0; y < PayRecord.Tips.Count; y++)
                        {
                            if (PayRecord.Tips[y].tipName == lsTips[x][0])
                            {
                                bFound = true;
                                break;
                            }
                        }
                        if (bFound)	// if payrecord has that deduction listed, then move that into the  temp list
                        {
                            lTempTips.Add(PayRecord.Tips[y]);
                        }
                        else	// we will just put in a place holder for that deduction to the list
                        {
                            PAYCHEQUE_TIPS PTip = new PAYCHEQUE_TIPS();
                            PTip.tipName = lsTips[x][0];
                            lTempTips.Add(PTip);
                        }
                    }
                    PayRecord.Tips = lTempTips;
					
                }
                //at this point we have a list of deduction that is complete, either from the data files or from code
                for (int x = 0; x < lsTips.Count; x++)	// number of rows available
                {
                    if (x > PayRecord.Tips.Count)
                    {
                        break;
                    }

                    if (Functions.GoodData (PayRecord.Tips[x].amount) && PayRecord.Tips[x].amount != "" && PayRecord.Tips[x].amount != "0")
                    {
                        PayrollJournal.repo.QuebecTips.QuebecTipsGrid.SelectCell("This Period", x);
                        PayrollJournal.repo.QuebecTips.QuebecTipsGrid.PressKeys(PayRecord.Tips[x].amount);	// Enter amount                        
                    }
                }
            }

            // To be done later
//            if (bRecur)	// store recurring
//            {
//                PayrollJournal.Instance.Window.TypeKeys("<Ctrl+t>");
//                StoreRecurringDialogDotNet.Instance._SA_DoStoreRecurring(PayRecord.recurringName, PayRecord.recurringFrequency);
//
//                // discard the transaction
//                PayrollJournal.ClickUndoChanges();
//            }
            if (bSave)
            {
                PayrollJournal.repo.Post.Click();

                if (Variables.bUseDataFiles)	// only handle the messages when execute using external data
                {
                    while (!PayrollJournal.repo.Self.Enabled)
                    {
                        SimplyMessage._SA_HandleMessage(SimplyMessage.repo.Yes, SimplyMessage.sNonPositiveAmountMsg, false, true);
                        SimplyMessage._SA_HandleMessage(SimplyMessage.repo.Yes, SimplyMessage.sNonValidFormulaMsg, false, true);
                        SimplyMessage._SA_HandleMessage(SimplyMessage.repo.Yes, SimplyMessage.sNoRecordedHoursMsg, false, true);
                    }
                }

//                // this might be a temporary fix for new payroll feature
//                if (s_desktop.Exists(PayrollExpiry.PAYROLLEXPIRY_LOC))
//                {
//                    PayrollExpiry.Instance.OK.Click();
//                }
            }
        }
        
        
        public static void _SA_printTransaction(string sFileName)
        {
            // bring up the print-to-file dialog
            PayrollJournal.repo.Print.Click();

            // handle messages
            if (SimplyMessage.repo.SelfInfo.Exists() && SimplyMessage.repo.MessageText.TextValue == SimplyMessage.sChangesMadeToPaychqMsg) // appears if paycheque is loaded in adjustment mode
            {
                SimplyMessage._SA_HandleMessage(SimplyMessage.repo.OK, SimplyMessage.sChangesMadeToPaychqMsg, true, true);
                PayrollJournal.repo.Print.Click();	// try to bring up the print dialog again
            }
            
            SimplyMessage._SA_HandleMessage(SimplyMessage.repo.Yes, SimplyMessage.sPaychqNumOutOfSequenceMsg, false, true);

            
            if (PrintToFileDialog.repo.SelfInfo.Exists())
            {
                PrintToFileDialog.Print(sFileName);
            }
            else	// log the error
            {
                Functions.Verify(false, true, "Print-to-File dialog is found");
            }

            // Undo changes
            PayrollJournal.ClickUndoChanges();
        }
        
        
        public static void ClickUndoChanges()
        {
        	ClickUndoChanges(false);
        }
        public static void ClickUndoChanges(bool bWaitForMsg)
        {
        	PayrollJournal.repo.Undo.Click();
        	
        	// need to indicate the wait explicitly as the message does not always appear. Should put in hard wait instead.
			if (bWaitForMsg)
			{
//				// wait for message to show
//				while(!s_desktop.Exists(SimplyMessage.SIMPLYMESSAGE_LOC))
//                {
//                }
			}
			
        	SimplyMessage._SA_HandleMessage(SimplyMessage.repo.Yes, SimplyMessage.sDiscardJournalMsg);
        }
        
        // To be done later
//         public static void _SA_RecallRecurring(PAYCHEQUE PayRecord)
//        {            
//            if (Functions.GoodData(PayRecord.recurringName))
//            {
//                Trace.WriteLine("Recalling the recurring entry " + PayRecord.recurringName + "");
//
//                if (!PayrollJournal.repo.SelfInfo.Exists())
//                {
//                    PayrollJournal._SA_Invoke();
//                }
//
//                PayrollJournal.repo.Recall.Click();
//                RecallRecurringDialogDotNet.Instance._SA_SelectEntryToRecall(PayRecord.recurringName);
//                PayrollJournal._SA_Create(PayRecord);
//            }
//            else	// log the error
//            {
//                Functions.Verify(false, true, "recurring name found");
//            }
//        }

		public static void _SA_Delete(PAYCHEQUE PayRecord)
        {
			Ranorex.Report.Info(String.Format("Deleting Paycheque {0} ", PayRecord.paychequeNumber));
			
            PayrollJournal._SA_Open(PayRecord);
            PayrollJournal.repo.Reverse.Click();
        }
		
		
		public static PAYCHEQUE _SA_Read()
        {
            return _SA_Read(null);
        }

        public static PAYCHEQUE _SA_Read(string sNumToRead) //  method will read all fields and store the data in a PAYCHEQUE record
        {
            PAYCHEQUE PC = new PAYCHEQUE();
			
            if (Functions.GoodData (sNumToRead))
            {
                PC.paychequeNumber =  sNumToRead;
                if (PayrollJournal.repo.PaychequeNumber.TextValue != PC.paychequeNumber)
                {
                    PayrollJournal._SA_Open (PC);
                }
            }
			
            PC.employee.name = PayrollJournal.repo.EmployeeName.SelectedItemText;
            PC.accountPaidFrom.acctNumber = PayrollJournal.repo.AccountPaidFrom.SelectedItemText;
            PC.paychequeDate = PayrollJournal.repo.PaychequeDate.TextValue;
            PC.paychequeNumber = PayrollJournal.repo.PaychequeNumber.TextValue;
            PC.directDeposit = PayrollJournal.repo.DirectDeposit.Checked; //ConvertFunctions.IntToBool(PayrollJournal.Instance.DirectDeposit.State) ;
			
//            if(Variables.productVersion != "Canadian")
//            {
//                PC.periodStartDate = FunctionsLib.GetFieldText (PayrollJournal.Instance.PeriodStart);	// US only
//            }
            PC.periodEndingDate = PayrollJournal.repo.PeriodEnd.TextValue;
			
            // Income tab
            PayrollJournal.repo.Income.Tab.Click();
            // List<string[]> lsEarnings = ConvertFunctions.DataGridItemsToListOfString(PayrollJournal.Instance.EarningIncomeGrid.Items, PayrollJournal.Instance.EarningIncomeGrid.ColumnCount);
            List<List<string>> lsEarnings = PayrollJournal.repo.Income.EarningIncomeGrid.GetContents();
            
            List<PAYCHEQUE_EARNINGS>  lPE = new List<PAYCHEQUE_EARNINGS>() {};            
            //int iNumOfCols = 6;
			
            // earnings container
            //while (lsContents.Count >= iNumOfCols)
            for (int x = 0; x < lsEarnings.Count; x++)
            {
                PAYCHEQUE_EARNINGS PE = new PAYCHEQUE_EARNINGS();
                PE.earningName = lsEarnings[x][0];
                PE.hours = lsEarnings[x][1];
                PE.pieces = lsEarnings[x][2];
                PE.Amount = lsEarnings[x][4];
                lPE.Add(PE);
				
            }
            PC.Earnings = lPE;
			
            // Other Incomes container
            // List<string[]> lsOther = ConvertFunctions.DataGridItemsToListOfString(PayrollJournal.Instance.OtherIncomeGrid.Items, PayrollJournal.Instance.OtherIncomeGrid.ColumnCount);
            List<List<string>> lsOther = PayrollJournal.repo.Income.OtherIncomeGrid.GetContents();
            
            List<PAYCHEQUE_OTHER_INCOMES>  lPO = new List<PAYCHEQUE_OTHER_INCOMES>() {};
			
            //iNumOfCols = 3;
            //while (lsContents.Count >= iNumOfCols)
            for (int x = 0; x < lsOther.Count; x++)
            {
                PAYCHEQUE_OTHER_INCOMES PO = new PAYCHEQUE_OTHER_INCOMES();
                PO.incomeName = lsOther[x][0];
                PO.Amount = lsOther[x][1];
                lPO.Add(PO);                
				
            }
            PC.Incomes = lPO;
			
            // Vacations tab
            PayrollJournal.repo.Vacation.Tab.Click();
            // List<string[]> lsVacations = ConvertFunctions.DataGridItemsToListOfString(PayrollJournal.Instance.VacationGrid.Items, PayrollJournal.Instance.VacationGrid.ColumnCount);
            List<List<string>> lsVacations = PayrollJournal.repo.Vacation.VacationGrid.GetContents();
            
            List<PAYCHEQUE_VACATION> lPV = new List<PAYCHEQUE_VACATION>() { };

            for (int x = 0; x < lsVacations.Count; x++)
            {
                PAYCHEQUE_VACATION PV = new PAYCHEQUE_VACATION();
                PV.vacationType = lsVacations[x][0];
                PV.hours = lsVacations[x][1];
                PV.amount = lsVacations[x][2];
                PV.ytdAmt = lsVacations[x][4];
                lPV.Add(PV);
            }
            PC.Vacations = lPV;

            // Deductions tab
            PayrollJournal.repo.Deductions.Tab.Click();
            // List<string[]> lsDeductions = ConvertFunctions.DataGridItemsToListOfString(PayrollJournal.Instance.DeductionGrid.Items, PayrollJournal.Instance.DeductionGrid.ColumnCount);
            List<List<string>> lsDeductions = PayrollJournal.repo.Deductions.DeductionGrid.GetContents();
            
            List<PAYCHEQUE_DEDUCTIONS>  lPD = new List<PAYCHEQUE_DEDUCTIONS>(){};
			
            //iNumOfCols = 3;
            //while (lsContents.Count >= iNumOfCols)
            for (int x = 0; x < lsDeductions.Count; x++)
            {
                PAYCHEQUE_DEDUCTIONS PD = new PAYCHEQUE_DEDUCTIONS();
                PD.deductionName = lsDeductions[x][0];
                PD.Amount = lsDeductions[x][1];
                lPD.Add(PD);
				
                //// removed already processed fields. NC - no longer needed
                //// only remove if more than one line is present
                //if(lsContents.Count > iNumOfCols )
                //{
                //    // remove line already recorded
                //    for ( int x = 1; x <= iNumOfCols; x++)
                //    {
                //        lsContents.RemoveAt(1);
                //    }
                //}
                //else
                //{
                //    lsContents.RemoveAt(1);
                //}				
            }
            PC.Deductions = lPD;
			
            // Taxes tab
            PayrollJournal.repo.Taxes.Tab.Click();
            // List<string[]> lsTaxes = ConvertFunctions.DataGridItemsToListOfString(PayrollJournal.Instance.TaxGrid.Items, PayrollJournal.Instance.TaxGrid.ColumnCount);
            List<List<string>> lsTaxes = PayrollJournal.repo.Taxes.TaxesGrid.GetContents();
            List<PAYCHEQUE_TAXES>  lPT = new List<PAYCHEQUE_TAXES>() {};
			
            //iNumOfCols = 3;
            //while (lsContents.Count >= iNumOfCols)
            for (int x = 0; x < lsTaxes.Count; x++)
            {
                PAYCHEQUE_TAXES PT = new PAYCHEQUE_TAXES();
                PT.taxName = lsTaxes[x][0];
                PT.amount = lsTaxes[x][1];
                lPT.Add(PT);
				
                //// removed already processed fields. NC - no longer needed
                //// only remove if more than one line is present
                //if(lsContents.Count > iNumOfCols )
                //{
                //    // remove line already recorded
                //    for ( int x = 1; x <= iNumOfCols; x++)
                //    {
                //        lsContents.RemoveAt(1);
                //    }
                //}
                //else
                //{
                //    lsContents.RemoveAt(1);
                //}				
            }
            PC.Taxes = lPT;
			
            // User Defined Expenses tab
            PayrollJournal.repo.UserDefinedExpenses.Tab.Click();
            // List<string[]> lsExpenses = ConvertFunctions.DataGridItemsToListOfString(PayrollJournal.Instance.ExpenseGrid.Items, PayrollJournal.Instance.ExpenseGrid.ColumnCount);
            List<List<string>> lsExpenses = PayrollJournal.repo.UserDefinedExpenses.ExpensesGrid.GetContents();
            List<PAYCHEQUE_USER_DEFINED_EXPENSES>  lPUD = new List<PAYCHEQUE_USER_DEFINED_EXPENSES>() {};
			
            //iNumOfCols = 3;
            //while (lsContents.Count >= iNumOfCols)
            for (int x = 0; x < lsExpenses.Count; x++)
            {
                PAYCHEQUE_USER_DEFINED_EXPENSES PUD = new PAYCHEQUE_USER_DEFINED_EXPENSES();
                PUD.expName = lsExpenses[x][0];
                PUD.amount = lsExpenses[x][1];
                lPUD.Add(PUD);
				
                //// removed already processed fields
                //// only remove if more than one line is present. NC - no longer needed
                //if(lsContents.Count > iNumOfCols )
                //{
                //    // remove line already recorded
                //    for ( int x = 1; x <= iNumOfCols; x++)
                //    {
                //        lsContents.RemoveAt(1);
                //    }
                //}
                //else
                //{
                //    lsContents.RemoveAt(1);
                //}				
            }
            PC.UserDefExpenses = lPUD;
						
            // Entitlements tab
            PayrollJournal.repo.Entitlements.Tab.Click();
            PC.hrsWorkedInPayPeriod = PayrollJournal.repo.Entitlements.HoursWorkedThisPeriod.TextValue;
			
            // List<string[]> lsEntitlements = ConvertFunctions.DataGridItemsToListOfString(PayrollJournal.Instance.EntitlementGrid.Items, PayrollJournal.Instance.EntitlementGrid.ColumnCount);
            List<List<string>> lsEntitlements = PayrollJournal.repo.Entitlements.EntitlementGrid.GetContents();
            List<PAYCHEQUE_ENTITLEMENTS>  lPET = new List<PAYCHEQUE_ENTITLEMENTS>() {};
			
            //iNumOfCols = 4;
            //while (lsContents.Count >= iNumOfCols)
            for (int x = 0; x < lsEntitlements.Count; x++)
            {
                PAYCHEQUE_ENTITLEMENTS PET = new PAYCHEQUE_ENTITLEMENTS();
                PET.dayName = lsEntitlements[x][0];
                PET.daysEarned = lsEntitlements[x][1];
                PET.daysTaken = lsEntitlements[x][2];
                lPET.Add(PET);
				
            }
            PC.Entitlements = lPET;
			
            // Quebec Tips tab 
            try // use try, catch so that script will continue when Quebec tips tab is not found
            {
            	PayrollJournal.repo.QuebecTips.Tab.Click();
            	if (PayrollJournal.repo.QuebecTips.QuebecTipsGridInfo.Exists())
                {
                    // List<string[]> lsTips = ConvertFunctions.DataGridItemsToListOfString(PayrollJournal.Instance.QuebecTipsGrid.Items, PayrollJournal.Instance.QuebecTipsGrid.ColumnCount);
                    List<List<string>> lsTips = PayrollJournal.repo.QuebecTips.QuebecTipsGrid.GetContents();
                    List<PAYCHEQUE_TIPS> lPTips = new List<PAYCHEQUE_TIPS>() { };

                    //while (lsContents.Count >= iNumOfCols)
                    for (int x = 0; x < lsTips.Count; x++)
                    {
                        PAYCHEQUE_TIPS PTips = new PAYCHEQUE_TIPS();
                        PTips.tipName = lsTips[x][0];
                        PTips.amount = lsTips[x][1];
                        lPTips.Add(PTips);
                    }
                    PC.Tips = lPTips;
                }
            }
            catch
            {
                Ranorex.Report.Info(String.Format("Continue after Quebec tips tab is not found"));
            }
			
            // bring up project allocation
            if (Convert.ToDouble(PayrollJournal.repo.NetPay.TextValue) > 0) // only click on project allocation button if netpay is greater than zero
            {
            	PayrollJournal.repo.AllocateToProjects.Click();
            }
            
            if (ProjectAllocationDialog.repo.SelfInfo.Exists())
            {
                List<PROJECT_ALLOCATION>  PA = new List<PROJECT_ALLOCATION>() {};
                List<List<string>> lsAllocations = ProjectAllocationDialog.repo.DataGrid.GetContents(); 
                
                // Enter first field is not blank
                if((lsAllocations[0][0]) != "")
                {
                    //while ((lsContents.Count >= 4))lsContents[0].Trim(' ') != "" && lsContents.Count >= 4))
                    for (int x = 0; x < lsAllocations.Count; x++)
                    {
                        {
                            PROJECT_ALLOCATION TempProj = new PROJECT_ALLOCATION();
                            // assign recordset
                            TempProj.Project.name = lsAllocations[x][0];
                            TempProj.Amount = lsAllocations[x][1];
                            TempProj.Percent = lsAllocations[x][2];
                            PA.Add(TempProj);
                        }
                    }
                }
				
                PC.Projects = PA;
                ProjectAllocationDialog.repo.Cancel.Click();
            }

            return PC;
        }
        
        
        
	}
}
