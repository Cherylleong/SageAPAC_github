/*
 * Created by Ranorex
 * User: wonda05
 * Date: 10/18/2016
 * Time: 10:39 AM
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
    /// Description of PayrollLedger.
    /// </summary>
    public static class PayrollLedger
    {
        public static PayrollLedgerResFolders.PayrollLedgerAppFolder repo = PayrollLedgerRes.Instance.PayrollLedger;

        public static void _SA_Invoke(Boolean bOpenLedger)
        {
            // open ledger depending on view type

            if (Simply.isEnhancedView())
            {
                Simply.repo.Self.Activate();
                Simply.repo.EmployeeLink.Click();
                Simply.repo.EmployeeIcon.Click();
            }
            else
            {

            }

            if (EmployeeIcon.repo.SelfInfo.Exists())
            {
                if (bOpenLedger == true)
                {
                    EmployeeIcon.repo.CreateNew.Click();
                    EmployeeIcon.repo.Self.Close();                    
                }
            }
        }

        public static void _SA_Invoke()
        {
            PayrollLedger._SA_Invoke(true);
        }

        public static void _SA_Open(EMPLOYEE Emp)
        {
            if (!PayrollLedger.repo.SelfInfo.Exists())
            {
                PayrollLedger._SA_Invoke();
            }
            PayrollLedger.repo.SelectRecord.Select(Emp.name);            
        }
        
        public static void _SA_Create(EMPLOYEE Emp)
        {
            _SA_Create(Emp, true, false);
        }

        public static void _SA_Create(EMPLOYEE Emp, bool bSave, bool bEdit)
        {
            if (!(Functions.GoodData(bEdit)))
            {
                bEdit = false;
            }

            if (!Variables.bUseDataFiles && !bEdit) // if external data files are not used
            {
                PayrollLedger._SA_MatchDefaults(Emp);
            }

            if (!Functions.GoodData(bEdit))
            {
                bEdit = false;
            }
            if (!Functions.GoodData(bSave))
            {
                bSave = true;
            }
            if (!PayrollLedger.repo.SelfInfo.Exists())
            {
                PayrollLedger._SA_Invoke();
            }

            if (bEdit)
            {
                if (PayrollLedger.repo.SelectRecord.SelectedItemText != Emp.name)
                {
                    PayrollLedger._SA_Open(Emp);
                }
                if (Functions.GoodData(Emp.nameEdit))
                {
                    PayrollLedger.repo.EmployeeName.TextValue = Emp.nameEdit;
                    Emp.name = Emp.nameEdit;
                }
                Ranorex.Report.Info(String.Format("Modifying Employee {0}", Emp.name));
            }
            else
            {
                PayrollLedger.repo.CreateANewToolButton.Click();
                PayrollLedger.repo.EmployeeName.TextValue = Emp.name;
                Ranorex.Report.Info(String.Format("Creating Employee {0}", Emp.name));
            }

            PayrollLedger.repo.Personal.Tab.Click();
            if (Functions.GoodData(Emp.Address.street1))
            {
                PayrollLedger.repo.Personal.Street1.TextValue = Emp.Address.street1;
            }

            if (Functions.GoodData(Emp.Address.street2))
            {
                PayrollLedger.repo.Personal.Street2.TextValue = Emp.Address.street2;
            }

            if (Functions.GoodData(Emp.Address.city))
            {
                PayrollLedger.repo.Personal.City.TextValue = Emp.Address.city;
            }

            if (Functions.GoodData(Emp.Address.province))
            {
                PayrollLedger.repo.Personal.Province.TextValue = Emp.Address.province;
            }

            if (Functions.GoodData(Emp.Address.provinceCode))
            {
                PayrollLedger.repo.Personal.ProvinceCode.Select(Emp.Address.provinceCode);
            }

            if (Functions.GoodData(Emp.Address.postalCode))
            {
                PayrollLedger.repo.Personal.PostalCode.TextValue = Emp.Address.postalCode;
            }

            if (Functions.GoodData(Emp.Address.phone1))
            {
                PayrollLedger.repo.Personal.Phone1.TextValue = Emp.Address.phone1;
            }
            if (Functions.GoodData(Emp.Address.phone2))
            {
                PayrollLedger.repo.Personal.Phone2.TextValue = Emp.Address.phone2;
            }
            if (Functions.GoodData(Emp.languagePreference))
            {
            	PayrollLedger.repo.Personal.LanguagePreference.Select(Emp.languagePreference.ToString());
            }

            if (Functions.GoodData(Emp.sin))
            {
                PayrollLedger.repo.Personal.SIN.TextValue = Emp.sin;
            }

            if (Functions.GoodData(Emp.birthDate))
            {
                PayrollLedger.repo.Personal.BirthDate.TextValue = Emp.birthDate;
            }

            if (Functions.GoodData(Emp.roeCode))
            {
                if (Emp.roeCode != "")
                {
                    PayrollLedger.repo.Personal.ROECode.Select(Emp.roeCode);
                }
            }

            if (Functions.GoodData(Emp.hireDate))
            {
                PayrollLedger.repo.Personal.HireDate.TextValue = Emp.hireDate;
            }

            if (Functions.GoodData(Emp.terminateDate))
            {
                PayrollLedger.repo.Personal.TerminateDate.TextValue = Emp.terminateDate;
            }

            if (PayrollLedger.repo.Personal.DepartmentInfo.Exists())
            {
                if (Functions.GoodData(Emp.department))
                {
                    if (Emp.department != "")
                    {
                        PayrollLedger.repo.Personal.Department.Select(Emp.department);
                    }
                }
            }

            if (PayrollLedger.repo.Personal.JobCategoryInfo.Exists())
            {
                if (Functions.GoodData(Emp.jobCategory))
                {
                	if (Emp.jobCategory == "<None>")
                	{
                		Emp.jobCategory = "<Default Expense Group>";
                	}
                	PayrollLedger.repo.Personal.JobCategory.Select(Emp.jobCategory);
                }
            }
            if (Functions.GoodData(Emp.inactiveCheckBox))
            {
                PayrollLedger.repo.InactiveEmployee.SetState(Emp.inactiveCheckBox);
            }

            if (Functions.GoodData(Emp.Taxes))
            {
                PayrollLedger.repo.Taxes.Tab.Click();

                if (Functions.GoodData(Emp.Taxes.taxTable))
                {
                    // combo box. can also use SetText and tab
                    PayrollLedger.repo.Taxes.TaxTable.Select(Emp.Taxes.taxTable);
                }

                if (Functions.GoodData(Emp.Taxes.TaxCredits))
                {
                    for (int x = 0; x < Emp.Taxes.TaxCredits.Count; x++)
                    {
                        PayrollLedger.repo.Taxes.PersonalTaxCreditsContainer.ClickFirstCell();

                        if (Emp.Taxes.TaxCredits[x].taxCreditType == "Federal")
                        {
                            PayrollLedger.repo.Taxes.PersonalTaxCreditsContainer.MoveRight();
                        }
                        else if (Emp.Taxes.TaxCredits[x].taxCreditType == "Provincial")
                        {
                            PayrollLedger.repo.Taxes.PersonalTaxCreditsContainer.MoveRight(2);
                        }

                        if (Functions.GoodData(Emp.Taxes.TaxCredits[x].basicPersonalAmount))
                        {
                            PayrollLedger.repo.Taxes.PersonalTaxCreditsContainer.SetText(Emp.Taxes.TaxCredits[x].basicPersonalAmount);
                        }
                        PayrollLedger.repo.Taxes.PersonalTaxCreditsContainer.MoveDown();

                        if (Functions.GoodData(Emp.Taxes.TaxCredits[x].indexedAmount))
                        {
                            PayrollLedger.repo.Taxes.PersonalTaxCreditsContainer.SetText(Emp.Taxes.TaxCredits[x].indexedAmount);
                        }
                        PayrollLedger.repo.Taxes.PersonalTaxCreditsContainer.MoveDown();

                        if (Functions.GoodData(Emp.Taxes.TaxCredits[x].nonIndexedAmount))
                        {
                            PayrollLedger.repo.Taxes.PersonalTaxCreditsContainer.SetText(Emp.Taxes.TaxCredits[x].nonIndexedAmount);
                        }
                    }
                }

                if (Functions.GoodData(Emp.Taxes.additionalFederalTax))
                {
                    PayrollLedger.repo.Taxes.AdditionalFederalTax.TextValue = Emp.Taxes.additionalFederalTax;
                }
                if (Functions.GoodData(Emp.Taxes.deductionEICheckBox))
                {
                    PayrollLedger.repo.Taxes.DeductEI.SetState(Emp.Taxes.deductionEICheckBox);
                }
                if (Functions.GoodData(Emp.Taxes.deductQpipCheckBox))
                {
                    PayrollLedger.repo.Taxes.DeductQpipOrSocSec.SetState(Emp.Taxes.deductQpipCheckBox);
                }
                if (Functions.GoodData(Emp.Taxes.eiRate))
                {
                    PayrollLedger.repo.Taxes.EIRate.TextValue = Emp.Taxes.eiRate;
                }
                if (Functions.GoodData(Emp.Taxes.deductCPPQPPCheckBox))
                {
                    PayrollLedger.repo.Taxes.DeductCPPQPP.SetState(Emp.Taxes.deductCPPQPPCheckBox);
                }

                if (Emp.Taxes.HistoricalAmounts.Count != 0)
                {
                    // PayrollLedger.repo.RestoreWindow.Select();

                    //PayrollLedger.TaxContainer.TaxTable.InitializeTable()
                    List <List<string>> lsContents = PayrollLedger.repo.Taxes.TaxContainer.GetContents();
                    // Set the first cell X position for SilkTest to click (try not to click over text in the cell, so as far right in the cell as possible)
                    PayrollLedger.repo.Taxes.TaxContainer.ClickFirstCell();                    
                    PayablesLedger.repo.Taxes.TaxContainer.MoveRight();
                   
                    // for (int x = 0; x < PayrollLedger.repo.Taxes.TaxContainer.GetCount(); x++)
                    for (int x = 0; x < lsContents.Count; x++)  // see if this count is the same as taxcontainer row count
                    {
                        //PayrollLedger.TaxContainer.TaxTable.MoveToRow(PayrollLedger,x)

                        for (int y = 0; y < Emp.Taxes.HistoricalAmounts.Count; y++)
                        {
                            if (lsContents[x][0] == Emp.Taxes.HistoricalAmounts[y].Tax)
                            {
                                PayrollLedger.repo.Taxes.TaxContainer.SetText(Emp.Taxes.HistoricalAmounts[y].Amount);
                            }
                            else if (Emp.Taxes.HistoricalAmounts[y].iDataFileLine == x) // came from datafile with just a line number
                            {
                                // NC
                                //if (( PayrollLedger.repo.TableField != null) && ( PayrollLedger.repo.TableField.Enabled) && Functions.GoodData(Emp.Taxes.HistoricalAmounts[y].Amount))
                                //{                                        
                                //PayrollLedger.repo.TaxContainer.SetText (Emp.Taxes.HistoricalAmounts[y].Amount);
                                //}
                            }
                        }
                        PayrollLedger.repo.Taxes.TaxContainer.MoveDown();
                    }
                }
            }

            if (Emp.Income.IncomeData.Count != 0)
            {
                PayrollLedger.repo.Income.Tab.Click();

                if (Functions.GoodData(Emp.Income.IncomeData))
                {
                    // PayrollLedger.repo.RestoreWindow.Select();

                    List <List<string>> lsContents = PayrollLedger.repo.Income.IncomeContainer.GetContents();

                    for (int x = 0; x < lsContents.Count; x++)
                    {
                    	// ClickFirstCell() only works in the first click
                        // PayrollLedger.repo.Income.IncomeContainer.ClickFirstCell();                        
                        // PayrollLedger.repo.Income.IncomeContainer.MoveDown(x);

                        for (int y = 0; y < Emp.Income.IncomeData.Count; y++)
                        {
                            if ((lsContents[x][1] == Emp.Income.IncomeData[y].income) || (Emp.Income.IncomeData[y].iDataFileLine == x && Variables.bUseDataFiles))  // added if using data files flag as well DW
                            {
                                //are we on the line equating to the list from the datatype
                                if (Functions.GoodData(Emp.Income.IncomeData[y].Use))
                                {
                                    if (Emp.Income.IncomeData[y].Use == true)
                                    {
                                        if (lsContents[x][0] == "false")
                                        {
                                            PayrollLedger.repo.Income.IncomeContainer.Toggle();
                                        }
                                    }
                                    else
                                    {
                                        if (lsContents[x][0] == "true")
                                        {
                                            PayrollLedger.repo.Income.IncomeContainer.Toggle();
                                        }
                                    }
                                }
                                PayrollLedger.repo.Income.IncomeContainer.MoveRight();
                                if (Functions.GoodData(Emp.Income.IncomeData[y].amountPerUnit))
                                {
                                    if ((Emp.Income.IncomeData[y].amountPerUnit != "--") && (lsContents[x][3] != "--"))
                                    {
                                        PayrollLedger.repo.Income.IncomeContainer.SetText(Emp.Income.IncomeData[y].amountPerUnit);
                                        PayrollLedger.repo.Income.IncomeContainer.MoveRight();
                                    }
                                }
                                if (Functions.GoodData(Emp.Income.IncomeData[y].hoursPerPeriod))
                                {
                                    if ((Emp.Income.IncomeData[y].hoursPerPeriod != "--") && (lsContents[x][4] != "--"))
                                    {
                                        PayrollLedger.repo.Income.IncomeContainer.SetText(Emp.Income.IncomeData[y].hoursPerPeriod);
                                        PayrollLedger.repo.Income.IncomeContainer.MoveRight();
                                    }
                                }
                                if (Functions.GoodData(Emp.Income.IncomeData[y].historicalAmount))
                                {
                                    if ((Emp.Income.IncomeData[y].historicalAmount != "--") && (lsContents[x][5] != "--"))
                                    {
                                        PayrollLedger.repo.Income.IncomeContainer.SetText(Emp.Income.IncomeData[y].historicalAmount);
                                    }
                                }
                                // Move cursor back to first column
                                PayrollLedger.repo.Income.IncomeContainer.MoveLeft(3);
                            }
                        }
                        // Move cursor to next row
                        PayrollLedger.repo.Income.IncomeContainer.MoveDown();
                        
                    }
                }
               
                if (Functions.GoodData(Emp.Income.payPeriodsPerYear))
                {
                    PayrollLedger.repo.Income.PayPeriodsPerYear.Select(Emp.Income.payPeriodsPerYear);
                }
                if (Functions.GoodData(Emp.Income.retainVacationCheckBox))
                {
                    PayrollLedger.repo.Income.RetainVacation.SetState(Emp.Income.retainVacationCheckBox);
                }
                if (Functions.GoodData(Emp.Income.calVacationOnVacationCheckBox))
                {
                    PayrollLedger.repo.Income.CalculateVacationOnVacation.SetState(Emp.Income.calVacationOnVacationCheckBox);
                }
                if (Functions.GoodData(Emp.Income.retainVacationPercentage))
                {
                    PayrollLedger.repo.Income.RetainVacationPercentage.TextValue = Emp.Income.retainVacationPercentage;
                }                
               
                // Radio buttons will not exists if linked account is not set up. Available In Pro.
                // Need to use Visible method, as the button is normally hidden but still exists for Ranorex
                if (PayrollLedger.repo.Income.RecordWageExpensesIn.Visible)
                {
                    if (Emp.Income.recordInLinkedAccounts == true)
                    {
                    	PayrollLedger.repo.Income.RecordWageExpensesIn.Select();
                    }
                    else
                    {
                    	PayrollLedger.repo.Income.WageExpensesButton.Select();
                        PayrollLedger.repo.Income.WageExpensesDropDown.Select(Emp.Income.wageExpenseAccount.acctNumber);
                    }
                }
            }
            if (Emp.Deductions[0].deductions != null)
            {
                PayrollLedger.repo.Deductions.Tab.Click();
                // PayrollLedger.repo.RestoreWindow.Select();

                List <List<string>> lsContents = PayrollLedger.repo.Deductions.DeductionsContainer.GetContents();

                for (int x = 0; x < 20; x++)
                {
                    PayrollLedger.repo.Deductions.DeductionsContainer.ClickFirstCell();
                    PayrollLedger.repo.Deductions.DeductionsContainer.MoveDown(x);

                    if (Functions.GoodData(Emp.Deductions[x].amountPerPayPeriod) || Functions.GoodData(Emp.Deductions[x].Use))
                    {
                        if (Emp.Deductions[x].Use == true)
                        {
                            if (lsContents[x][0] == "false")
                            {
                                PayrollLedger.repo.Deductions.DeductionsContainer.Toggle();
                            }
                        }
                        else
                        {
                            if (Emp.Deductions != null)
                            {
                                if (lsContents[x][0] == "true")
                                {
                                    PayrollLedger.repo.Deductions.DeductionsContainer.Toggle();
                                }
                            }
                        }
                        PayrollLedger.repo.Deductions.DeductionsContainer.MoveRight();

                        if (Functions.GoodData(Emp.Deductions[x].amountPerPayPeriod))
                        {
                            PayrollLedger.repo.Deductions.DeductionsContainer.SetText(Emp.Deductions[x].amountPerPayPeriod);
                        }
                        else
                        {
                            if (Functions.GoodData(Emp.Deductions[x].percentPerPayPeriod))
                            {
                                PayrollLedger.repo.Deductions.DeductionsContainer.SetText(Emp.Deductions[x].percentPerPayPeriod);
                            }

                        }
                        PayrollLedger.repo.Deductions.DeductionsContainer.MoveRight();

                        if (Functions.GoodData(Emp.Deductions[x].historicalAmount) && (Emp.Deductions[x].historicalAmount != "--"))
                        {
                            PayrollLedger.repo.Deductions.DeductionsContainer.SetText(Emp.Deductions[x].historicalAmount);
                        }
                    }
                }
            }

            if (Functions.GoodData(Emp.Expenses))
            {
                PayrollLedger.repo.WCBOtherExp.Tab.Click(); //Select("WCB  Other Expenses");
                if (Functions.GoodData(Emp.Expenses.wcbRate))
                {
                    PayrollLedger.repo.WCBOtherExp.WCBRate.TextValue = Emp.Expenses.wcbRate;
                }
                if (Functions.GoodData(Emp.Expenses.ExpenseRows))
                {
                    // PayrollLedger.repo.RestoreWindow.Select();
                    //PayrollLedger.WCBContainer.WCBTable.InitializeTable()
                    PayrollLedger.repo.WCBOtherExp.WCBContainer.ClickFirstCell(); //Click(MouseButton.Left, new Point(22, 33));
                    PayrollLedger.repo.WCBOtherExp.WCBContainer.MoveRight();
                    for (int x = 0; x < 5; x++)
                    {
                        if (Functions.GoodData(Emp.Expenses.ExpenseRows[x]))
                        {
                            if (Functions.GoodData(Emp.Expenses.ExpenseRows[x].amountPerPeriod))
                            {
                                PayrollLedger.repo.WCBOtherExp.WCBContainer.SetText(Emp.Expenses.ExpenseRows[x].amountPerPeriod);
                            }
                            if (Functions.GoodData(Emp.Expenses.ExpenseRows[x].historicalAmount))
                            {
                                PayrollLedger.repo.WCBOtherExp.WCBContainer.MoveRight();
                                PayrollLedger.repo.WCBOtherExp.WCBContainer.SetText(Emp.Expenses.ExpenseRows[x].historicalAmount);
                            }
                            PayrollLedger.repo.WCBOtherExp.WCBContainer.MoveRight();
                        }
                        else
                        {
                            PayrollLedger.repo.WCBOtherExp.WCBContainer.MoveDown();
                        }
                    }
                }
            }

            if (Emp.Entitlements.EntitlementRows[0].entitlementName != null)
            {
                PayrollLedger.repo.Entitlements.Tab.Click();
                PayrollLedger.repo.Entitlements.HoursInWorkDay.TextValue = Emp.Entitlements.hrsInWorkDay;

                if (Functions.GoodData(Emp.Entitlements.EntitlementRows))
                {
                    // PayrollLedger.repo.RestoreWindow.Select();
                    List <List<string>> lsContents = PayrollLedger.repo.Entitlements.EntitlementsContainer.GetContents();

                    for (int x = 0; x < 5; x++)
                    {
                        if (Functions.GoodData(Emp.Entitlements.EntitlementRows[x]))
                        {
                            PayrollLedger.repo.Entitlements.EntitlementsContainer.ClickFirstCell();
                            PayrollLedger.repo.Entitlements.EntitlementsContainer.MoveDown(x);

                            PayrollLedger.repo.Entitlements.EntitlementsContainer.MoveRight();

                            if (Functions.GoodData(Emp.Entitlements.EntitlementRows[x].percentageHoursWorked))
                            {
                                PayrollLedger.repo.Entitlements.EntitlementsContainer.SetText(Emp.Entitlements.EntitlementRows[x].percentageHoursWorked);
                            }
                            PayrollLedger.repo.Entitlements.EntitlementsContainer.MoveRight();
                            if (Functions.GoodData(Emp.Entitlements.EntitlementRows[x].maximumDays))
                            {
                                PayrollLedger.repo.Entitlements.EntitlementsContainer.SetText(Emp.Entitlements.EntitlementRows[x].maximumDays);
                            }
                            PayrollLedger.repo.Entitlements.EntitlementsContainer.MoveRight();
                            if (Functions.GoodData(Emp.Entitlements.EntitlementRows[x].clearDays))
                            {
                                if (Emp.Entitlements.EntitlementRows[x].clearDays == true)
                                {
                                    if (lsContents[x][3] == "No")
                                    {
                                        PayrollLedger.repo.Entitlements.EntitlementsContainer.Toggle();
                                    }
                                }
                                else
                                {
                                    if (lsContents[x][3] == "Yes")
                                    {
                                        PayrollLedger.repo.Entitlements.EntitlementsContainer.Toggle();
                                    }
                                }
                            }
                            PayrollLedger.repo.Entitlements.EntitlementsContainer.MoveRight();

                            // Only do if in history mode - check if 6 column is there
                            if (lsContents[0][5] != "")
                            {
                                if (Functions.GoodData(Emp.Entitlements.EntitlementRows[x].historicalDays))
                                {
                                    PayrollLedger.repo.Entitlements.EntitlementsContainer.SetText(Emp.Entitlements.EntitlementRows[x].historicalDays);
                                }
                                PayrollLedger.repo.Entitlements.EntitlementsContainer.MoveRight();
                            }
                        }
                    }
                }
            }

            if (Functions.GoodData(Emp.DirectDeposits))
            {
                PayrollLedger.repo.DirectDeposit.Tab.Click();
                PayrollLedger.repo.DirectDeposit.DD.SetState(Emp.DirectDeposits.directDepositCheckBox);
                if ((Emp.DirectDeposits.directDepositCheckBox == true) && Functions.GoodData(Emp.DirectDeposits.DepositAccounts))
                {
                    List <List<string>> lsContents = PayrollLedger.repo.DirectDeposit.DirectDepositContainer.GetContents();

                    // PayrollLedger.repo.RestoreWindow.Select();
                    //PayrollLedger.DirectDepositContainer.DirectDepositTable.InitializeTable()
                    PayrollLedger.repo.DirectDeposit.DirectDepositContainer.ClickFirstCell();//Click(MouseButton.Left, new Point(22, 33));
                    for (int x = 0; x < Emp.DirectDeposits.DepositAccounts.Count; x++)
                    {
                        PayrollLedger.repo.DirectDeposit.DirectDepositContainer.SetText(Emp.DirectDeposits.DepositAccounts[x].transitNumber);
                        PayrollLedger.repo.DirectDeposit.DirectDepositContainer.MoveRight();
                        PayrollLedger.repo.DirectDeposit.DirectDepositContainer.SetText(Emp.DirectDeposits.DepositAccounts[x].bankNumber);
                        PayrollLedger.repo.DirectDeposit.DirectDepositContainer.MoveRight();
                        PayrollLedger.repo.DirectDeposit.DirectDepositContainer.SetText(Emp.DirectDeposits.DepositAccounts[x].accountNumber);
                        PayrollLedger.repo.DirectDeposit.DirectDepositContainer.MoveRight();
                        if (Functions.GoodData(Emp.DirectDeposits.DepositAccounts[x].amount))
                        {
                            PayrollLedger.repo.DirectDeposit.DirectDepositContainer.SetText(Emp.DirectDeposits.DepositAccounts[x].amount);
                        }
                        PayrollLedger.repo.DirectDeposit.DirectDepositContainer.MoveRight();
                        if (Functions.GoodData(Emp.DirectDeposits.DepositAccounts[x].percentage))
                        {
                            PayrollLedger.repo.DirectDeposit.DirectDepositContainer.SetText(Emp.DirectDeposits.DepositAccounts[x].percentage);
                        }
                        PayrollLedger.repo.DirectDeposit.DirectDepositContainer.MoveRight();

                        if (bEdit)
                        {
                            if (lsContents[x][5] == "Active")
                            {
                                if (Emp.DirectDeposits.DepositAccounts[x].ActiveStatus == false)
                                {
                                    PayrollLedger.repo.DirectDeposit.DirectDepositContainer.Toggle();
                                }
                            }
                            else
                            {
                                if (Emp.DirectDeposits.DepositAccounts[x].ActiveStatus == true)
                                {
                                    PayrollLedger.repo.DirectDeposit.DirectDepositContainer.Toggle();
                                }
                            }
                            PayrollLedger.repo.DirectDeposit.DirectDepositContainer.SetText(Emp.DirectDeposits.DepositAccounts[x].bankNumber);
                        }
                        PayrollLedger.repo.DirectDeposit.DirectDepositContainer.MoveRight();
                    }
                }
            }
            if (Functions.GoodData(Emp.memo) || Functions.GoodData(Emp.toDoDate) || Functions.GoodData(Emp.displayCheckBox))
            {
                PayrollLedger.repo.Memo.Tab.Click();
                if (Functions.GoodData(Emp.memo))
                {
                    PayrollLedger.repo.Memo.Memo.TextValue = Emp.memo;
                }
                if (Functions.GoodData(Emp.toDoDate))
                {
                    PayrollLedger.repo.Memo.ToDoDate.TextValue = Emp.toDoDate;
                }
                if (Functions.GoodData(Emp.displayCheckBox))
                {
                    PayrollLedger.repo.Memo.DisplayThisMemo.SetState(Emp.displayCheckBox);
                }
            }

            PayrollLedger.repo.AdditionalInfo.Tab.Click();
            if (Functions.GoodData(Emp.additional1))
            {                
                if (PayrollLedger.repo.AdditionalInfo.Additional1Info.Exists())
                {
                    PayrollLedger.repo.AdditionalInfo.Additional1.TextValue = Emp.additional1;                    
                }
            }
            if (Functions.GoodData(Emp.addCheckBox1))
            {                
                if (PayrollLedger.repo.AdditionalInfo.AddCheckBox1Info.Exists())
                {
                    PayrollLedger.repo.AdditionalInfo.AddCheckBox1.SetState(Emp.addCheckBox1);
                }
            }
            if (Functions.GoodData(Emp.additional2))
            {                
                if (PayrollLedger.repo.AdditionalInfo.Additional2Info.Exists())
                {
                    PayrollLedger.repo.AdditionalInfo.Additional2.TextValue = Emp.additional2;
                }
            }
            if (Functions.GoodData(Emp.addCheckBox2))
            {
                if (PayrollLedger.repo.AdditionalInfo.AddCheckBox2Info.Exists())
                {
                    PayrollLedger.repo.AdditionalInfo.AddCheckBox2.SetState(Emp.addCheckBox2);
                }
            }
            if (Functions.GoodData(Emp.additional3))
            {
                if (PayrollLedger.repo.AdditionalInfo.Additional3Info.Exists())
                {
                    PayrollLedger.repo.AdditionalInfo.Additional3.TextValue = Emp.additional3;
                }
            }
            if (Functions.GoodData(Emp.addCheckBox3))
            {
                if (PayrollLedger.repo.AdditionalInfo.AddCheckBox3Info.Exists())
                {
                    PayrollLedger.repo.AdditionalInfo.AddCheckBox3.SetState(Emp.addCheckBox3);
                }
            }
            // Additional 4 and 5 are not in 2017. Space is now used for Manulife.
            /*
            if (Functions.GoodData(Emp.additional4))
            {
                if (PayrollLedger.repo.AdditionalInfo.Additional4Info.Exists())
                {
                    PayrollLedger.repo.AdditionalInfo.Additional4.TextValue = Emp.additional4;
                }
            }
            if (Functions.GoodData(Emp.addCheckBox4))
            {
                if (PayrollLedger.repo.AdditionalInfo.AddCheckBox4Info.Exists())
                {
                    PayrollLedger.repo.AdditionalInfo.AddCheckBox4.SetState(Emp.addCheckBox4);
                }
            }
            if (Functions.GoodData(Emp.additional5))
            {
                if (PayrollLedger.repo.AdditionalInfo.Additional5Info.Exists())
                {
                    PayrollLedger.repo.AdditionalInfo.Additional5.TextValue = Emp.additional5;
                }
            }
            if (Functions.GoodData(Emp.addCheckBox5))
            {
                if (PayrollLedger.repo.AdditionalInfo.AddCheckBox5Info.Exists())
                {
                    PayrollLedger.repo.AdditionalInfo.AddCheckBox5.SetState(Emp.addCheckBox5);
                }
            }
            */

            if (Functions.GoodData(Emp.Reporting.rppDPSPRegNo) || Functions.GoodData(Emp.Reporting.pensionAdjustment) || Functions.GoodData(Emp.Reporting.t4EmpCode) ||
                Functions.GoodData(Emp.Reporting.historicalEIInsEarnings) || Functions.GoodData(Emp.Reporting.historicalPensionableEarnings) ||
                Functions.GoodData(Emp.Reporting.eiInsEarnings) || Functions.GoodData(Emp.Reporting.pensionEarnings) || Functions.GoodData(Emp.Reporting.qpipInsEarnings))
            {                
                PayrollLedger.repo.T4RL1.Tab.Click();
                if (Functions.GoodData(Emp.Reporting.eiInsEarnings))
                {
                    if (PayrollLedger.repo.T4RL1.EIInsEarningsInfo.Exists())
                    {
                        if (PayrollLedger.repo.T4RL1.EIInsEarnings.Enabled)
                        {
                            PayrollLedger.repo.T4RL1.EIInsEarnings.TextValue = Emp.Reporting.eiInsEarnings;
                        }
                    }
                }
                if (Functions.GoodData(Emp.Reporting.pensionEarnings))
                {
                    if (PayrollLedger.repo.T4RL1.PensionableEarningsInfo.Exists())
                    {                        
                        if (PayrollLedger.repo.T4RL1.PensionableEarnings.Enabled)
                        {
                            PayrollLedger.repo.T4RL1.PensionableEarnings.TextValue = Emp.Reporting.pensionEarnings;
                        }
                    }
                }
                if (Functions.GoodData(Emp.Reporting.qpipInsEarnings))
                {
                    if (PayrollLedger.repo.T4RL1.QPIPInsEarningsInfo.Exists())
                    {
                        if (PayrollLedger.repo.T4RL1.QPIPInsEarnings.Enabled)
                        {
                            PayrollLedger.repo.T4RL1.QPIPInsEarnings.TextValue = Emp.Reporting.qpipInsEarnings;
                        }
                    }
                }
                if (Functions.GoodData(Emp.Reporting.rppDPSPRegNo))
                {
                    PayrollLedger.repo.T4RL1.RPPDPSP.TextValue = Emp.Reporting.rppDPSPRegNo;
                }
                if (Functions.GoodData(Emp.Reporting.pensionAdjustment))
                {
                    PayrollLedger.repo.T4RL1.PensionAdjustment.TextValue = Emp.Reporting.pensionAdjustment;
                }
                if (Functions.GoodData(Emp.Reporting.t4EmpCode))
                {
                    if (Emp.Reporting.t4EmpCode != "")
                    {
                        PayrollLedger.repo.T4RL1.T4EmpCode.Select(Emp.Reporting.t4EmpCode);
                    }
                }
                if (PayrollLedger.repo.T4RL1.HistoricalEIInsEarningsInfo.Exists() && Functions.GoodData(Emp.Reporting.historicalEIInsEarnings))
                {
                    PayrollLedger.repo.T4RL1.HistoricalEIInsEarnings.TextValue = Emp.Reporting.historicalEIInsEarnings;
                }
                if (PayrollLedger.repo.T4RL1.HistoricalPensionableEarningsInfo.Exists() && Functions.GoodData(Emp.Reporting.historicalPensionableEarnings))
                {
                    PayrollLedger.repo.T4RL1.HistoricalPensionableEarnings.TextValue = Emp.Reporting.historicalPensionableEarnings;
                    // PayrollLedger.repo.T4RL1.HistoricalPensionableEarnings.MoveRight();
                }
                if (PayrollLedger.repo.T4RL1.HistoricalQpipEarningsInfo.Exists() && Functions.GoodData(Emp.Reporting.historicalQpipEarnings))
                {
                    if (PayrollLedger.repo.T4RL1.HistoricalQpipEarnings.Enabled)
                    {
                        PayrollLedger.repo.T4RL1.HistoricalQpipEarnings.TextValue = Emp.Reporting.historicalQpipEarnings;
                    }
                }
            }

            // Save the Record
            if (bSave)
            {
                PayrollLedger.repo.Save.Click();
            }
        }

        public static void _SA_MatchDefaults(EMPLOYEE Emp)
        {

            if (!Functions.GoodData(Emp.inactiveCheckBox))
            {
                Emp.inactiveCheckBox = false;
            }
            if (!Functions.GoodData(Emp.languagePreference))
            {
                Emp.languagePreference = LANGUAGE_PREF.English;
            }
            if (!Functions.GoodData(Emp.Address.city))
            {
                Emp.Address.city = Variables.globalSettings.CompanyInformation.Address.city;
            }
            if (!Functions.GoodData(Emp.Address.province))
            {
                //Emp.Address.province = "British Columbia"
                Emp.Address.province = Variables.globalSettings.CompanyInformation.Address.province;
            }
            if (!Functions.GoodData(Emp.Address.provinceCode))
            {
                //Emp.Address.provinceCode = "BC"
                Emp.Address.provinceCode = Variables.globalSettings.CompanyInformation.Address.provinceCode;
            }
            if (!Functions.GoodData(Emp.jobCategory))
            {
                Emp.jobCategory = "<None>";
            }
            if (!Functions.GoodData(Emp.Taxes.deductionEICheckBox))
            {
                Emp.Taxes.deductionEICheckBox = true;
            }
            if (!Functions.GoodData(Emp.Taxes.deductCPPQPPCheckBox))
            {
                Emp.Taxes.deductCPPQPPCheckBox = true;
            }
            if (!Functions.GoodData(Emp.Taxes.eiRate))
            {
                Emp.Taxes.eiRate = "1.4";
            }
            if (!Functions.GoodData(Emp.Income.retainVacationCheckBox))
            {
                Emp.Income.retainVacationCheckBox = true;
            }
            if (!Functions.GoodData(Emp.Income.recordInLinkedAccounts))
            {
                Emp.Income.recordInLinkedAccounts = true;
            }
            if (!Functions.GoodData(Emp.Entitlements.hrsInWorkDay))
            {
                Emp.Entitlements.hrsInWorkDay = "8";
            }
            if (!Functions.GoodData(Emp.displayCheckBox))
            {
                Emp.displayCheckBox = false;
            }
            if (!Functions.GoodData(Emp.DirectDeposits.directDepositCheckBox))
            {
                Emp.DirectDeposits.directDepositCheckBox = false;
            }
            if (!Functions.GoodData(Emp.addCheckBox1))
            {
                Emp.addCheckBox1 = false;
            }
            if (!Functions.GoodData(Emp.addCheckBox2))
            {
                Emp.addCheckBox2 = false;
            }
            if (!Functions.GoodData(Emp.addCheckBox3))
            {
                Emp.addCheckBox3 = false;
            }
            if (!Functions.GoodData(Emp.addCheckBox4))
            {
                Emp.addCheckBox4 = false;
            }
            if (!Functions.GoodData(Emp.addCheckBox5))
            {
                Emp.addCheckBox5 = false;
            }
        }

        public static void _SA_Delete(EMPLOYEE Emp)
        {
            if (!PayrollLedger.repo.SelfInfo.Exists())
            {
                PayrollLedger._SA_Invoke();
            }

            if (PayrollLedger.repo.SelectRecord.SelectedItemText != Emp.name)
            {
                PayrollLedger._SA_Open(Emp);
            }

            PayrollLedger.repo.Remove.Click();

            // SimplyMessage.Instance._SA_HandleMessage(SimplyMessage.REMOVE_LOC, SimplyMessage._MSG_IFYOUWILLNEEDTOPRINTAT4_LOC);
        }

        public static void _SA_Close()
        {
            repo.Self.Close();

        }

        public static EMPLOYEE _SA_Read()
        {
            return _SA_Read(null);
        }

        public static EMPLOYEE _SA_Read(string sIDToRead)
        {            
            EMPLOYEE Emp = new EMPLOYEE();

            if (!PayrollLedger.repo.SelfInfo.Exists())
            {
                PayrollLedger._SA_Invoke();
            }

            if (Functions.GoodData(sIDToRead))
            {
                Emp.name = sIDToRead;
                if (PayrollLedger.repo.SelectRecord.SelectedItemText != Emp.name)
                {
                    PayrollLedger._SA_Open(Emp);
                }
            }
            Emp.name = PayrollLedger.repo.SelectRecord.SelectedItemText;

            PayrollLedger.repo.Personal.Tab.Click();
            Emp.Address.street1 = PayrollLedger.repo.Personal.Street1.TextValue;
            Emp.Address.street2 = PayrollLedger.repo.Personal.Street2.TextValue;
            Emp.Address.city = PayrollLedger.repo.Personal.City.TextValue;
            Emp.Address.province = PayrollLedger.repo.Personal.Province.TextValue;
            Emp.Address.provinceCode = PayrollLedger.repo.Personal.ProvinceCode.SelectedItemText;
            Emp.Address.postalCode = PayrollLedger.repo.Personal.PostalCode.TextValue;
                       
            Emp.Address.phone1 = PayrollLedger.repo.Personal.Phone1.TextValue;
            Emp.Address.phone2 = PayrollLedger.repo.Personal.Phone2.TextValue;
            Emp.languagePreference = (LANGUAGE_PREF)PayrollLedger.repo.Personal.LanguagePreference.SelectedItemIndex;// PayrollLedger.repo.Personal.LanguagePreference.SelectedItemText;             
            Emp.sin = PayrollLedger.repo.Personal.SIN.TextValue;
            Emp.birthDate = PayrollLedger.repo.Personal.BirthDate.TextValue;
            Emp.roeCode = PayrollLedger.repo.Personal.ROECode.SelectedItemText;        
            
            Emp.hireDate = PayrollLedger.repo.Personal.HireDate.TextValue;
            Emp.terminateDate = PayrollLedger.repo.Personal.TerminateDate.TextValue;
            //if (PayrollLedger.Instance.Window.Exists(DEFAULTDEPARTMENT_LOC))
            if (PayrollLedger.repo.Personal.DepartmentInfo.Exists())
            {
                Emp.department = PayrollLedger.repo.Personal.Department.SelectedItemText;
            }            
            if (PayrollLedger.repo.Personal.JobCategoryInfo.Exists())
            {
                Emp.jobCategory = PayrollLedger.repo.Personal.JobCategory.SelectedItemText;
            }
            Emp.inactiveCheckBox = PayrollLedger.repo.InactiveEmployee.Checked;

            List <List<string>> lsContents;


            PayrollLedger.repo.Taxes.Tab.Click();
            Emp.Taxes.taxTable = PayrollLedger.repo.Taxes.TaxTable.SelectedItemText; //combobox

            lsContents = PayrollLedger.repo.Taxes.PersonalTaxCreditsContainer.GetContents();

            EMP_TAX_CREDIT_DATA taxCreditdata = new EMP_TAX_CREDIT_DATA();
            taxCreditdata.taxCreditType = "Federal";
            taxCreditdata.basicPersonalAmount = lsContents[0][1];
            taxCreditdata.indexedAmount = lsContents[1][1];
            taxCreditdata.nonIndexedAmount = lsContents[2][1];
            Emp.Taxes.TaxCredits.Add(taxCreditdata);

            EMP_TAX_CREDIT_DATA secondTaxCredit = new EMP_TAX_CREDIT_DATA();
            secondTaxCredit.taxCreditType = "Provincial";
            secondTaxCredit.basicPersonalAmount = lsContents[0][2];
            secondTaxCredit.indexedAmount = lsContents[1][2];
            secondTaxCredit.nonIndexedAmount = lsContents[2][2];
            Emp.Taxes.TaxCredits.Add(secondTaxCredit);

            Emp.Taxes.additionalFederalTax = PayrollLedger.repo.Taxes.AdditionalFederalTax.TextValue;
            Emp.Taxes.deductionEICheckBox = PayrollLedger.repo.Taxes.DeductEI.Checked;
            Emp.Taxes.eiRate = PayrollLedger.repo.Taxes.EIRate.TextValue;
            Emp.Taxes.deductCPPQPPCheckBox = PayrollLedger.repo.Taxes.DeductCPPQPP.Checked;            
            Emp.Taxes.deductQpipCheckBox = PayrollLedger.repo.Taxes.DeductQpipOrSocSec.Checked;            

            // PayrollLedger.repo.RestoreWindow.Select();

            List<EMP_TAX_HIST> lETH = new List<EMP_TAX_HIST>() { };
            
            foreach(List<string> currentRow in PayrollLedger.repo.Taxes.TaxContainer.GetContents())
            {
            	EMP_TAX_HIST ETH = new EMP_TAX_HIST();
            	ETH.Tax = currentRow[0];
            	ETH.Amount = currentRow[1];
            	lETH.Add(ETH);
            }

            Emp.Taxes.HistoricalAmounts = lETH;            
            

            PayrollLedger.repo.Income.Tab.Click();
            // PayrollLedger.repo.RestoreWindow.Select();

            List<EMP_INCOME_USE> lEIU = new List<EMP_INCOME_USE>() { };

            foreach (List <string> IncomeLine in PayrollLedger.repo.Income.IncomeContainer.GetContents())
            {
            	EMP_INCOME_USE EIU = new EMP_INCOME_USE();
            	
            	if (IncomeLine[0] == "true")
            	{
            		EIU.Use = true;
            	}
            	else
            	{
            		EIU.Use = false;
            	}
            	
            	EIU.income = IncomeLine[1];
            	EIU.unit = IncomeLine[2];
            	EIU.amountPerUnit = IncomeLine[3];
            	EIU.hoursPerPeriod = IncomeLine[4];
            	EIU.piecesPerPeriod = IncomeLine[5];
            	EIU.historicalAmount = IncomeLine[6];
            	lEIU.Add(EIU);
            }
            Emp.Income.IncomeData = lEIU;
            Emp.Income.payPeriodsPerYear = PayrollLedger.repo.Income.PayPeriodsPerYear.SelectedItemText;
            Emp.Income.retainVacationCheckBox = PayrollLedger.repo.Income.RetainVacation.Checked;
            Emp.Income.calVacationOnVacationCheckBox = PayrollLedger.repo.Income.CalculateVacationOnVacation.Checked;
            Emp.Income.retainVacationPercentage = PayrollLedger.repo.Income.RetainVacationPercentage.TextValue;            
                        
            if (PayrollLedger.repo.Income.RecordWageExpensesInInfo.Exists())
            {
                if (PayrollLedger.repo.Income.RecordWageExpensesIn.Checked)
                {
                    Emp.Income.recordInLinkedAccounts = true;
                }
                else
                {
                    Emp.Income.recordInLinkedAccounts = false;
                }
                Emp.Income.wageExpenseAccount.acctNumber = PayrollLedger.repo.Income.WageExpensesDropDown.SelectedItemText;
            }


            PayrollLedger.repo.Deductions.Tab.Click();
            // PayrollLedger.repo.RestoreWindow.Select();
            lsContents = PayrollLedger.repo.Deductions.DeductionsContainer.GetContents();           
            for (int x = 0; x < 20; x++)
            {
                if (lsContents[x][0] == "true")
                {
                    Emp.Deductions[x].Use = true;
                }
                else
                {
                    Emp.Deductions[x].Use = false;
                }
                Emp.Deductions[x].deductions = lsContents[x][1];
                Emp.Deductions[x].amountPerPayPeriod = lsContents[x][2];
                Emp.Deductions[x].percentPerPayPeriod = lsContents[x][3];
                Emp.Deductions[x].historicalAmount = lsContents[x][4];
            }            

            PayrollLedger.repo.WCBOtherExp.Tab.Click(); // WCB & Other Expenses
            Emp.Expenses.wcbRate = PayrollLedger.repo.WCBOtherExp.WCBRate.TextValue;
            // PayrollLedger.repo.RestoreWindow.Select();
            lsContents = PayrollLedger.repo.WCBOtherExp.WCBContainer.GetContents();
            for (int x = 0; x < 5; x++)
            {
                Emp.Expenses.ExpenseRows[x].amountPerPeriod = lsContents[x][1];
                Emp.Expenses.ExpenseRows[x].historicalAmount = lsContents[x][2];
            }


            PayrollLedger.repo.Entitlements.Tab.Click();
            Emp.Entitlements.hrsInWorkDay = PayrollLedger.repo.Entitlements.HoursInWorkDay.TextValue;
            // PayrollLedger.repo.RestoreWindow.Select();
            lsContents = PayrollLedger.repo.Entitlements.EntitlementsContainer.GetContents();
            for (int x = 0; x < 5; x++)
            {
                Emp.Entitlements.EntitlementRows[x].entitlementName = lsContents[x][0];
                Emp.Entitlements.EntitlementRows[x].percentageHoursWorked = lsContents[x][1];
                Emp.Entitlements.EntitlementRows[x].maximumDays = lsContents[x][2];
                if (lsContents[x][3] == "No")
                {
                    Emp.Entitlements.EntitlementRows[x].clearDays = false;
                }
                else
                {
                    Emp.Entitlements.EntitlementRows[x].clearDays = true;
                }
                Emp.Entitlements.EntitlementRows[x].historicalDays = lsContents[x][4];
            }


            PayrollLedger.repo.DirectDeposit.Tab.Click();
            Emp.DirectDeposits.directDepositCheckBox = PayrollLedger.repo.DirectDeposit.DD.Checked;
            if (Emp.DirectDeposits.directDepositCheckBox == true)
            {
                lsContents = PayrollLedger.repo.DirectDeposit.DirectDepositContainer.GetContents();
                // PayrollLedger.repo.RestoreWindow.Select();
                for (int x = 0; x < Emp.DirectDeposits.DepositAccounts.Count; x++)
                {
                    Emp.DirectDeposits.DepositAccounts[x].bankNumber = lsContents[x][0];
                    Emp.DirectDeposits.DepositAccounts[x].transitNumber = lsContents[x][1];
                    Emp.DirectDeposits.DepositAccounts[x].accountNumber = lsContents[x][2];
                    Emp.DirectDeposits.DepositAccounts[x].amount = lsContents[x][3];
                    Emp.DirectDeposits.DepositAccounts[x].percentage = lsContents[x][4];
                    if (lsContents[x][5] == "Active")
                    {
                        Emp.DirectDeposits.DepositAccounts[x].ActiveStatus = true;
                    }
                    else
                    {
                        Emp.DirectDeposits.DepositAccounts[x].ActiveStatus = false;
                    }
                }
            }


            PayrollLedger.repo.Memo.Tab.Click();
            Emp.memo = PayrollLedger.repo.Memo.Memo.TextValue;
            Emp.toDoDate = PayrollLedger.repo.Memo.ToDoDate.TextValue;
            Emp.displayCheckBox = PayrollLedger.repo.Memo.DisplayThisMemo.Checked;


            PayrollLedger.repo.AdditionalInfo.Tab.Click();
            if (PayrollLedger.repo.AdditionalInfo.Additional1Info.Exists())
            {
                Emp.additional1 = PayrollLedger.repo.AdditionalInfo.Additional1.TextValue;
            }
            if (PayrollLedger.repo.AdditionalInfo.AddCheckBox1Info.Exists())
            {
                Emp.addCheckBox1 = PayrollLedger.repo.AdditionalInfo.AddCheckBox1.Checked;
            }
            if (PayrollLedger.repo.AdditionalInfo.Additional2Info.Exists())
            {
                Emp.additional2 = PayrollLedger.repo.AdditionalInfo.Additional2.TextValue;
            }            
            if (PayrollLedger.repo.AdditionalInfo.AddCheckBox2Info.Exists())
            {
                Emp.addCheckBox2 = PayrollLedger.repo.AdditionalInfo.AddCheckBox2.Checked;
            }
            if (PayrollLedger.repo.AdditionalInfo.Additional3Info.Exists())
            {
                Emp.additional3 = PayrollLedger.repo.AdditionalInfo.Additional3.TextValue;
            }
            if (PayrollLedger.repo.AdditionalInfo.AddCheckBox3Info.Exists())
            {
                Emp.addCheckBox3 = PayrollLedger.repo.AdditionalInfo.AddCheckBox3.Checked;
            }
            // SA2017 only have 3 additional fields
            //if (PayrollLedger.Instance.Window.Exists(ADDITIONALINFOFIELD4_LOC))
            //{
            //    Emp.additional4 = FunctionsLib.GetFieldText(PayrollLedger.Instance.AdditionalInfoField4);
            //}
            //if (PayrollLedger.Instance.Window.Exists(ADDITIONALINFOCHECKBOX4_LOC))
            //{
            //    Emp.addCheckBox4 = ConvertFunctions.IntToBool(PayrollLedger.Instance.AdditionalInfoCheckBox4.State);
            //}
            //if (PayrollLedger.Instance.Window.Exists(ADDITIONALINFOFIELD5_LOC))
            //{
            //    Emp.additional5 = FunctionsLib.GetFieldText(PayrollLedger.Instance.AdditionalInfoField5);
            //}
            //if (PayrollLedger.Instance.Window.Exists(ADDITIONALINFOCHECKBOX5_LOC))
            //{
            //    Emp.addCheckBox5 = ConvertFunctions.IntToBool(PayrollLedger.Instance.AdditionalInfoCheckBox5.State);
            //}

            PayrollLedger.repo.T4RL1.Tab.Click(); // T4 and RL-1 Reporting
            {
                Emp.Reporting.rppDPSPRegNo = PayrollLedger.repo.T4RL1.RPPDPSP.TextValue;
                Emp.Reporting.pensionAdjustment = PayrollLedger.repo.T4RL1.PensionAdjustment.TextValue;
                Emp.Reporting.t4EmpCode = PayrollLedger.repo.T4RL1.T4EmpCode.SelectedItemText;
            }            
            if (PayrollLedger.repo.T4RL1.HistoricalEIInsEarningsInfo.Exists()) 
            {
                Emp.Reporting.historicalEIInsEarnings = PayrollLedger.repo.T4RL1.HistoricalEIInsEarnings.TextValue;
            }
           if (PayrollLedger.repo.T4RL1.HistoricalPensionableEarningsInfo.Exists())
            {
                {
                    Emp.Reporting.historicalPensionableEarnings = PayrollLedger.repo.T4RL1.HistoricalPensionableEarnings.TextValue;
                }
            }
            return Emp;
        }

        // DataFile

        public static void DataFile_ReadFile(string sDataLocation, string fileCounter)
        {            
            string EXTENSION_HEADER = ".hdr";
            string EXTENSION_TAXES = ".dt1";
            string EXTENSION_INCOME = ".dt2";
            string EXTENSION_DEDUCTIONS = ".dt3";
            string EXTENSION_WCB = ".dt4";
            string EXTENSION_ENTITLEMENTS = ".dt5";
            string EXTENSION_DIRECT_DEPOSIT = ".dt6";
            string EXTENSION_MEMO = ".dt7";
            string EXTENSION_ADDITIONAL_INFO = ".dt8";
            string EXTENSION_T4_RL1 = ".dt9";
            string EXTENSION_HISTORY_QUEBEC_TIPS = ".dt10";
            string EXTENSION_USER_DEFINED_EXPENSE = ".dt11";
            string EXTENSION_TAX_TABLE = ".cnt1";
            string EXTENSION_INCOME_TABLE = ".cnt2";
            string EXTENSION_WCB_TABLE = ".cnt3";
            string EXTENSION_ENTITLEMENTS_TABLE = ".cnt4";
            string EXTENSION_DIRECT_DEPOSIT_TABLE = ".cnt5";

            StreamReader FileHDR;	// File handle for address tab info
            StreamReader FileDT1;	// File handle for the taxes tab info
            StreamReader FileDT2;	// File handle for the income Info tab info
            StreamReader FileDT3;	// File handle for the deductions Info tab info
            StreamReader FileDT4;	// File handle for the wcb Info tab info
            StreamReader FileDT5;	// File handle for the entitlements tab info
            StreamReader FileDT6;	// File handle for the direct deposit Info tab info
            StreamReader FileDT7;	// File handle for the memo tab info
            StreamReader FileDT8;	// File handle for the additional info tab info
            StreamReader FileDT9;	// File handle for the T4 Info tab info
            StreamReader FileDT10;	// File handle for the history quebec tips tab info
            StreamReader FileDT11;	// File handle for the history quebec tips tab info

            StreamReader FileCN1;	// File handle for the tax container Info
            StreamReader FileCN2;	// File handle for the income container file
            StreamReader FileCN3;	// File handle for the wcb container Info
            StreamReader FileCN4;	// File handle for the entitlements container file
            StreamReader FileCN5;   // File handle for the direct deposit container Info		           


            string dataLine;	// Stores the current field data from file
            string dataPath;    // The name and path of the data file


            // Get the data path from file
            dataPath = sDataLocation + "EL" + fileCounter;

            List<EMPLOYEE> lEmployees = new List<EMPLOYEE>() { };

            FileHDR = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_HEADER, "FM_READ"));
            while ((dataLine = FileHDR.ReadLine()) != null)
            {
                EMPLOYEE Emp = new EMPLOYEE();

                // Must remove blank income data line that is created during the new function called when creating employee
                // DW Emp.Income.IncomeData.RemoveAt(0);

                Emp = PayrollLedger.DataFile_setDataStructure(EXTENSION_HEADER, dataLine, Emp);

                // Open DT1 file and set structure
                if (File.Exists(dataPath + EXTENSION_TAXES))
                {
                    using (FileDT1 = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_TAXES, "FM_READ")))
                    {
                        while ((dataLine = FileDT1.ReadLine()) != null)
                        {
                            Emp = PayrollLedger.DataFile_setDataStructure(EXTENSION_TAXES, dataLine, Emp);
                        }
                    }
                }

                // Open DT2 file and set structure
                if (File.Exists(dataPath + EXTENSION_INCOME))
                {
                    using (FileDT2 = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_INCOME, "FM_READ")))
                    {
                        while ((dataLine = FileDT2.ReadLine()) != null)
                        {
                            Emp = PayrollLedger.DataFile_setDataStructure(EXTENSION_INCOME, dataLine, Emp);
                        }
                    }
                }

                // Open DT3 file and set structure
                if (File.Exists(dataPath + EXTENSION_DEDUCTIONS))
                {
                    using (FileDT3 = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_DEDUCTIONS, "FM_READ")))
                    {
                        while ((dataLine = FileDT3.ReadLine()) != null)
                        {
                            Emp = PayrollLedger.DataFile_setDataStructure(EXTENSION_DEDUCTIONS, dataLine, Emp);
                        }
                    }
                }

                // Open DT4 file and set structure
                if (File.Exists(dataPath + EXTENSION_WCB))
                {
                    using (FileDT4 = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_WCB, "FM_READ")))
                    {
                        while ((dataLine = FileDT4.ReadLine()) != null)
                        {
                            Emp = PayrollLedger.DataFile_setDataStructure(EXTENSION_WCB, dataLine, Emp);
                        }
                    }
                }

                // Open DT5 file and set structure
                if (File.Exists(dataPath + EXTENSION_ENTITLEMENTS))
                {
                    using (FileDT5 = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_ENTITLEMENTS, "FM_READ")))
                    {
                        while ((dataLine = FileDT5.ReadLine()) != null)
                        {
                            Emp = PayrollLedger.DataFile_setDataStructure(EXTENSION_ENTITLEMENTS, dataLine, Emp);
                        }
                    }
                }

                // Open DT6 file and set structure
                if (File.Exists(dataPath + EXTENSION_DIRECT_DEPOSIT))
                {
                    using (FileDT6 = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_DIRECT_DEPOSIT, "FM_READ")))
                    {
                        while ((dataLine = FileDT6.ReadLine()) != null)
                        {
                            Emp = PayrollLedger.DataFile_setDataStructure(EXTENSION_DIRECT_DEPOSIT, dataLine, Emp);
                        }
                    }
                }

                // Open DT7 file and set structure
                if (File.Exists(dataPath + EXTENSION_MEMO))
                {
                    using (FileDT7 = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_MEMO, "FM_READ")))
                    {
                        while ((dataLine = FileDT7.ReadLine()) != null)
                        {
                            Emp = PayrollLedger.DataFile_setDataStructure(EXTENSION_MEMO, dataLine, Emp);
                        }
                    }
                }

                // Open DT8 file and set structure
                if (File.Exists(dataPath + EXTENSION_ADDITIONAL_INFO))
                {
                    using (FileDT8 = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_ADDITIONAL_INFO, "FM_READ")))
                    {
                        while ((dataLine = FileDT8.ReadLine()) != null)
                        {
                            Emp = PayrollLedger.DataFile_setDataStructure(EXTENSION_ADDITIONAL_INFO, dataLine, Emp);
                        }
                    }
                }

                // Open DT9 file and set structure
                if (File.Exists(dataPath + EXTENSION_T4_RL1))
                {
                    using (FileDT9 = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_T4_RL1, "FM_READ")))
                    {
                        while ((dataLine = FileDT9.ReadLine()) != null)
                        {
                            Emp = PayrollLedger.DataFile_setDataStructure(EXTENSION_T4_RL1, dataLine, Emp);
                        }
                    }
                }

                // Open DT10 file and set structure
                if (File.Exists(dataPath + EXTENSION_HISTORY_QUEBEC_TIPS))
                {
                    using (FileDT10 = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_HISTORY_QUEBEC_TIPS, "FM_READ")))
                    {
                        while ((dataLine = FileDT10.ReadLine()) != null)
                        {
                            Emp = PayrollLedger.DataFile_setDataStructure(EXTENSION_HISTORY_QUEBEC_TIPS, dataLine, Emp);
                        }
                    }
                }

                // Open DT11 file and set structure
                if (File.Exists(dataPath + EXTENSION_USER_DEFINED_EXPENSE))
                {
                    using (FileDT11 = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_USER_DEFINED_EXPENSE, "FM_READ")))
                    {
                        while ((dataLine = FileDT11.ReadLine()) != null)
                        {
                            Emp = PayrollLedger.DataFile_setDataStructure(EXTENSION_USER_DEFINED_EXPENSE, dataLine, Emp);
                        }
                    }
                }

                // Open and set tax container files
                if (File.Exists(dataPath + EXTENSION_TAX_TABLE))
                {
                    using (FileCN1 = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_TAX_TABLE, "FM_READ")))
                    {
                        while ((dataLine = FileCN1.ReadLine()) != null)
                        {
                            Emp = PayrollLedger.DataFile_setDataStructure(EXTENSION_TAX_TABLE, dataLine, Emp);
                        }
                    }
                    //histTaxFlag = TRUE                    
                }

                // Only open income container file if it exists
                if (File.Exists(dataPath + EXTENSION_INCOME_TABLE))
                {
                    using (FileCN2 = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_INCOME_TABLE, "FM_READ")))
                    {
                        while ((dataLine = FileCN2.ReadLine()) != null)
                        {
                            Emp = PayrollLedger.DataFile_setDataStructure(EXTENSION_INCOME_TABLE, dataLine, Emp);
                        }
                    }
                }

                // Open and set wcb container files
                if (File.Exists(dataPath + EXTENSION_WCB_TABLE))
                {
                    using (FileCN3 = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_WCB_TABLE, "FM_READ")))
                    {
                        while ((dataLine = FileCN3.ReadLine()) != null)
                        {
                            Emp = PayrollLedger.DataFile_setDataStructure(EXTENSION_WCB_TABLE, dataLine, Emp);
                        }
                    }
                }

                // Only open entitlements container file if it exists
                if (File.Exists(dataPath + EXTENSION_ENTITLEMENTS_TABLE))
                {
                    using (FileCN4 = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_ENTITLEMENTS_TABLE, "FM_READ")))
                    {
                        while ((dataLine = FileCN4.ReadLine()) != null)
                        {
                            Emp = PayrollLedger.DataFile_setDataStructure(EXTENSION_ENTITLEMENTS_TABLE, dataLine, Emp);
                        }
                    }
                }

                // Only open direct deposit container file if it exists
                if (File.Exists(dataPath + EXTENSION_DIRECT_DEPOSIT_TABLE))
                {
                    using (FileCN5 = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_DIRECT_DEPOSIT_TABLE, "FM_READ")))
                    {
                        while ((dataLine = FileCN5.ReadLine()) != null)
                        {
                            Emp = PayrollLedger.DataFile_setDataStructure(EXTENSION_DIRECT_DEPOSIT_TABLE, dataLine, Emp);
                        }
                    }
                }
                lEmployees.Add(Emp);

            }
            FileHDR.Close();

            foreach (EMPLOYEE Emp in lEmployees)
            {
                switch (Emp.action)
                {
                    case "A":
                        PayrollLedger._SA_Create(Emp);
                        break;
                    case "E":
                        PayrollLedger._SA_Create(Emp, true, true);
                        break;
                    case "D":
                        PayrollLedger._SA_Delete(Emp);
                        break;
                    default:
                        {
                            Functions.Verify(false, true, "Action set properly");
                            break;
                        }
                }
            }

            PayrollLedger._SA_Close();
        }

        public static EMPLOYEE DataFile_setDataStructure(string extension, string dataLine, EMPLOYEE Emp)
        {
            EMPLOYEE EmployeeRecord = Emp;
            int iLine;
            switch (extension.ToUpper())
            {
                case ".HDR":
                    EmployeeRecord.action = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 1));
                    EmployeeRecord.name = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 2));
                    EmployeeRecord.Address.street1 = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 3));
                    EmployeeRecord.Address.street2 = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 4));
                    EmployeeRecord.Address.city = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 5));
                    EmployeeRecord.Address.province = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 6));
                    EmployeeRecord.Address.postalCode = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 7));
                    EmployeeRecord.Address.phone1 = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 8));
                    EmployeeRecord.Address.phone2 = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 9));
                    EmployeeRecord.sin = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 10));
                    EmployeeRecord.birthDate = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 11));
                    EmployeeRecord.hireDate = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 12));
                    EmployeeRecord.terminateDate = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 13));
                    EmployeeRecord.roeCode = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 14));
                    EmployeeRecord.inactiveCheckBox = ConvertFunctions.StringToBool(Functions.GetField(dataLine, ",", 15));
                    EmployeeRecord.department = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 16));
                    EmployeeRecord.Address.provinceCode = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 17));
                    if (Functions.GetField(dataLine, ",", 18) == "English")
                    {
                        EmployeeRecord.languagePreference = LANGUAGE_PREF.English;
                    }
                    else if (Functions.GetField(dataLine, ",", 18) == "French")
                    {
                        EmployeeRecord.languagePreference = LANGUAGE_PREF.French;
                    }
                    else
                    {
                        Functions.Verify(false, true, "valid language preference sent");
                    }
                    EmployeeRecord.jobCategory = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 19));
                    EmployeeRecord.nameEdit = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 20));

                    //EmployeeRecord.Address.State = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 21));
                    //if (FunctionsLib.GoodData(EmployeeRecord.Address.State))	// US then, so add postal code (no zip field)
                    //{
                    //    EmployeeRecord.Address.postalCode = FunctionsLib.PrepStringsFromDataFiles(FunctionsLib.GetField(dataLine, ",", 22));
                    //}
                    //EmployeeRecord.ssn = FunctionsLib.PrepStringsFromDataFiles(FunctionsLib.GetField(dataLine, ",", 23));
                    break;
                case ".DT1":
                    if (EmployeeRecord.name == Functions.GetField(dataLine, ",", 1))
                    {
                        EmployeeRecord.Taxes.taxTable = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 2));

                        EMP_TAX_CREDIT_DATA firstTaxCredit = new EMP_TAX_CREDIT_DATA();
                        firstTaxCredit.taxCreditType = "Federal";
                        firstTaxCredit.basicPersonalAmount = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 3));
                        firstTaxCredit.indexedAmount = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 4));
                        EmployeeRecord.Taxes.TaxCredits.Add(firstTaxCredit);

                        EMP_TAX_CREDIT_DATA secondTaxCredit = new EMP_TAX_CREDIT_DATA();
                        secondTaxCredit.taxCreditType = "Provincial";
                        secondTaxCredit.basicPersonalAmount = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 5));
                        secondTaxCredit.indexedAmount = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 6));
                        EmployeeRecord.Taxes.TaxCredits.Add(secondTaxCredit);

                        EmployeeRecord.Taxes.additionalFederalTax = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 7));
                        EmployeeRecord.Taxes.deductionEICheckBox = ConvertFunctions.StringToBool(Functions.GetField(dataLine, ",", 8));
                        EmployeeRecord.Taxes.eiRate = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 9));
                        EmployeeRecord.Taxes.deductCPPQPPCheckBox = ConvertFunctions.StringToBool(Functions.GetField(dataLine, ",", 10));
                    }
                    break;
                case ".DT2":
                    if (EmployeeRecord.name == Functions.GetField(dataLine, ",", 1))
                    {
                        EmployeeRecord.Income.payPeriodsPerYear = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 2));
                        EmployeeRecord.Income.retainVacationCheckBox = ConvertFunctions.StringToBool(Functions.GetField(dataLine, ",", 3));
                        EmployeeRecord.Income.retainVacationPercentage = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 4));

                        if (Functions.GetField(dataLine, ",", 5).Trim() == "The payroll linked accounts")
                        {
                            EmployeeRecord.Income.recordInLinkedAccounts = true;
                        }
                        else
                        {
                            EmployeeRecord.Income.recordInLinkedAccounts = false;
                            EmployeeRecord.Income.wageExpenseAccount.acctNumber = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 6));
                        }
                    }
                    break;
                case ".DT3":
                    if (EmployeeRecord.name == Functions.GetField(dataLine, ",", 1))
                    {
                        EMP_DEDUCT ED = new EMP_DEDUCT();
                        iLine = Convert.ToInt32(Functions.GetField(dataLine, ",", 2)) - 1;  // minus 1 since index now starts at 0
                        if (Functions.GetField(dataLine, ",", 3) == "Yes")   // this is likely wrong, not sure what this value will be
                        {
                            ED.Use = true;
                        }
                        else
                        {
                            ED.Use = false;
                        }
                        ED.deductions = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 4));
                        ED.amountPerPayPeriod = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 5));
                        ED.percentPerPayPeriod = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 6));
                        ED.historicalAmount = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 7));
                        EmployeeRecord.Deductions[iLine] = ED;
                    }                
                    break;
                case ".DT4":
                    if (EmployeeRecord.name == Functions.GetField(dataLine, ",", 1))
                    {
                        EmployeeRecord.Expenses.wcbRate = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 2));
                    }
                    break;
                case ".DT5":
                    if (EmployeeRecord.name == Functions.GetField(dataLine, ",", 1))
                    {
                        EmployeeRecord.Entitlements.hrsInWorkDay = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 2));
                    }
                    break;
                case ".DT6":
                    if (EmployeeRecord.name == Functions.GetField(dataLine, ",", 1))
                    {
                        EmployeeRecord.DirectDeposits.directDepositCheckBox = ConvertFunctions.StringToBool(Functions.GetField(dataLine, ",", 2));
                    }
                    break;
                case ".DT7":
                    if (EmployeeRecord.name == Functions.GetField(dataLine, ",", 1))
                    {
                        EmployeeRecord.memo = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 2));
                        EmployeeRecord.toDoDate = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 3));
                        EmployeeRecord.displayCheckBox = ConvertFunctions.StringToBool(Functions.GetField(dataLine, ",", 4));
                    }
                    break;
                case ".DT8":
                    if (EmployeeRecord.name == Functions.GetField(dataLine, ",", 1))
                    {
                        EmployeeRecord.additional1 = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 2));
                        EmployeeRecord.additional2 = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 3));
                        EmployeeRecord.additional3 = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 4));
                        EmployeeRecord.additional4 = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 5));
                        EmployeeRecord.additional5 = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 6));
                        EmployeeRecord.addCheckBox1 = ConvertFunctions.StringToBool(Functions.GetField(dataLine, ",", 7));
                        EmployeeRecord.addCheckBox2 = ConvertFunctions.StringToBool(Functions.GetField(dataLine, ",", 8));
                        EmployeeRecord.addCheckBox3 = ConvertFunctions.StringToBool(Functions.GetField(dataLine, ",", 9));
                        EmployeeRecord.addCheckBox4 = ConvertFunctions.StringToBool(Functions.GetField(dataLine, ",", 10));
                        EmployeeRecord.addCheckBox5 = ConvertFunctions.StringToBool(Functions.GetField(dataLine, ",", 11));
                    }
                    break;
                case ".DT9":
                    if (EmployeeRecord.name == Functions.GetField(dataLine, ",", 1))
                    {
                        EmployeeRecord.Reporting.rppDPSPRegNo = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 2));
                        EmployeeRecord.Reporting.pensionAdjustment = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 3));
                        EmployeeRecord.Reporting.t4EmpCode = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 4));
                        EmployeeRecord.Reporting.historicalEIInsEarnings = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 5));
                        EmployeeRecord.Reporting.historicalPensionableEarnings = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 6));
                    }
                    break;
                case ".DT10":
                    if (EmployeeRecord.name == Functions.GetField(dataLine, ",", 1))
                    {
                        EmployeeRecord.Reporting.tippableSales = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 2));
                        EmployeeRecord.Reporting.tipsFromSales = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 3));
                        EmployeeRecord.Reporting.otherTips = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 4));
                        EmployeeRecord.Reporting.tipsAllocated = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 5));
                        EmployeeRecord.Reporting.fedTaxableTips = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 6));
                        EmployeeRecord.Reporting.quebecTaxableTips = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 7));
                    }
                    break;                
                case ".DT11":
                    //nothing in here is actionable, so not recording it. It is all done via the EXTENSION_WCB_TABLE
                    // if (EmployeeRecord.employeeName == FunctionsLib.GetField (dataLine, ",", 1))
                    // EMP_EXPENSE_DATA EED = New (EMP_EXPENSE_DATA)
                    // EmployeeRecord.userExpense1 = PrepStringsFromDataFiles(FunctionsLib.GetField (dataLine, ",", 2)
                    // EmployeeRecord.userExpense2 = PrepStringsFromDataFiles(FunctionsLib.GetField (dataLine, ",", 3)
                    // EmployeeRecord.userExpense3 = PrepStringsFromDataFiles(FunctionsLib.GetField (dataLine, ",", 4)
                    // EmployeeRecord.userExpense4 = PrepStringsFromDataFiles(FunctionsLib.GetField (dataLine, ",", 5)
                    // EmployeeRecord.userExpense5 = PrepStringsFromDataFiles(FunctionsLib.GetField (dataLine, ",", 6)
                    break;                
                case ".CNT1":
                    if (EmployeeRecord.name == Functions.GetField(dataLine, ",", 1))
                    {
                        EMP_TAX_HIST ETH = new EMP_TAX_HIST();
                        ETH.iDataFileLine = Convert.ToInt32(Functions.GetField(dataLine, ",", 2));
                        ETH.Amount = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 3));
                        EmployeeRecord.Taxes.HistoricalAmounts.Add(ETH);
                    }
                    break;
                case ".CNT2":
                    if (EmployeeRecord.name == Functions.GetField(dataLine, ",", 1))
                    {
                        EMP_INCOME_USE EIU = new EMP_INCOME_USE();
                        EIU.iDataFileLine = Convert.ToInt32(Functions.GetField(dataLine, ",", 2)) - 1;
                        if (Functions.GetField(dataLine, ",", 3).Trim() == "Yes")
                        {
                            EIU.Use = true;
                        }
                        else
                        {
                            EIU.Use = false;
                        }
                        EIU.income = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 4));
                        EIU.unit = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 5));
                        EIU.amountPerUnit = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 6));
                        EIU.hoursPerPeriod = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 7));
                        EIU.piecesPerPeriod = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 8));
                        EIU.historicalAmount = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 9));
                        EmployeeRecord.Income.IncomeData.Add(EIU);
                    }
                    break;
                case ".CNT3":
                    if (EmployeeRecord.name == Functions.GetField(dataLine, ",", 1))
                    {
                        EMP_EXPENSE_DATA EED = new EMP_EXPENSE_DATA();
                        iLine = Convert.ToInt32(Functions.GetField(dataLine, ",", 2)) - 1;
                        EED.amountPerPeriod = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 3));
                        EED.historicalAmount = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 4));
                        EmployeeRecord.Expenses.ExpenseRows[iLine] = EED;
                    }
                    break;
                case ".CNT4":
                    int totalItems = 0;

                    for (int x = 0; x < 5; x++)
                    {
                        if (EmployeeRecord.Entitlements.EntitlementRows[x].entitlementName != null)
                        {
                            totalItems++;
                        }
                    }

                    if (EmployeeRecord.name == Functions.GetField(dataLine, ",", 1))
                    {
                        EMP_ENTITLE_DATA EEntitlD = new EMP_ENTITLE_DATA();
                        EEntitlD.entitlementName = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 2));
                        EEntitlD.percentageHoursWorked = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 3));
                        EEntitlD.maximumDays = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 4));
                        if (Functions.GetField(dataLine, ",", 5) == "Yes")
                        {
                            EEntitlD.clearDays = true;
                        }
                        else
                        {
                            EEntitlD.clearDays = false;
                        }
                        EEntitlD.historicalDays = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 6));
                        //EmployeeRecord.Entitlements.EntitlementRows[ArraySize (Emp.Entitlements.EntitlementRows) + 1] = EEntitlD
                        //EmployeeRecord.Entitlements.EntitlementRows[totalItems + 1] = EEntitlD;
                        EmployeeRecord.Entitlements.EntitlementRows[totalItems] = EEntitlD;
                    }
                    break;
                case ".CNT5":                
                    if (EmployeeRecord.name == Functions.GetField(dataLine, ",", 1))
                    {
                        EMP_DIRECT_DEP_DATA EDDD = new EMP_DIRECT_DEP_DATA();
                        EDDD.bankNumber = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 2));
                        EDDD.transitNumber = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 3));
                        EDDD.accountNumber = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 4));
                        EDDD.amount = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 5));
                        EDDD.percentage = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 6));
                        if (Functions.GetField(dataLine, ",", 7).Trim() == "Active")
                        {
                            EDDD.ActiveStatus = true;
                        }
                        else
                        {
                            EDDD.ActiveStatus = false;
                        }
                        EmployeeRecord.DirectDeposits.DepositAccounts.Add(EDDD);
                    }
                    break;
                default:
                    {
                        break;
                    }
            }
            return (EmployeeRecord);
        }



        //public static List<string> DataFile_updateListOfStrings(string extension, EMPLOYEE EmployeeRecord)
        //{
        //    return DataFile_updateListOfStrings(extension, EmployeeRecord, 0);
        //}

        //public static List<string> DataFile_updateListOfStrings(string extension, EMPLOYEE EmployeeRecord, int iRowNum)
        //{
        //    List<string> listContents = new List<string>();

        //    switch (extension.ToUpper())
        //    {
        //        case EXTENSION_HEADER:
        //            listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(EmployeeRecord.action)));
        //            listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(EmployeeRecord.name)));
        //            listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(EmployeeRecord.Address.street1)));
        //            listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(EmployeeRecord.Address.street2)));
        //            listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(EmployeeRecord.Address.city)));
        //            listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(EmployeeRecord.Address.province)));
        //            listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(EmployeeRecord.Address.postalCode)));
        //            listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(EmployeeRecord.Address.phone1)));
        //            listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(EmployeeRecord.Address.phone2)));
        //            listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(EmployeeRecord.sin)));
        //            listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(EmployeeRecord.birthDate)));
        //            listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(EmployeeRecord.hireDate)));
        //            listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(EmployeeRecord.terminateDate)));
        //            listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(EmployeeRecord.roeCode)));
        //            listContents.Add(ConvertFunctions.BoolToString(EmployeeRecord.inactiveCheckBox));
        //            listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(EmployeeRecord.department)));
        //            listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(EmployeeRecord.Address.provinceCode)));

        //            if (EmployeeRecord.languagePreference == LANGUAGE_PREF.English)
        //            {
        //                listContents.Add("English");
        //            }
        //            else
        //            {
        //                listContents.Add("French");
        //            }

        //            //listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(EmployeeRecord.jobCategory)));
        //            //listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(EmployeeRecord.nameEdit)));
        //            //listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(EmployeeRecord.Address.State)));
        //            //listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(EmployeeRecord.Address.postalCode)));
        //            //listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(EmployeeRecord.ssn)));
        //            break;
        //        case EXTENSION_TAXES:
        //            listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(EmployeeRecord.name)));
        //            listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(EmployeeRecord.Taxes.taxTable)));

        //            listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(EmployeeRecord.Taxes.TaxCredits[0].basicPersonalAmount)));
        //            listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(EmployeeRecord.Taxes.TaxCredits[0].indexedAmount)));
        //            listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(EmployeeRecord.Taxes.TaxCredits[1].basicPersonalAmount)));
        //            listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(EmployeeRecord.Taxes.TaxCredits[1].indexedAmount)));
        //            listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(EmployeeRecord.Taxes.additionalFederalTax)));
        //            listContents.Add(ConvertFunctions.BoolToString(EmployeeRecord.Taxes.deductionEICheckBox));
        //            listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(EmployeeRecord.Taxes.eiRate)));
        //            listContents.Add(ConvertFunctions.BoolToString(EmployeeRecord.Taxes.deductCPPQPPCheckBox));

        //            //US version
        //            listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(EmployeeRecord.Taxes.federalTax)));
        //            listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(EmployeeRecord.Taxes.stateTax)));
        //            listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(EmployeeRecord.Taxes.fedAllowance)));
        //            listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(EmployeeRecord.Taxes.fedStatus)));
        //            listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(EmployeeRecord.Taxes.stateAllowance1)));
        //            listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(EmployeeRecord.Taxes.stateAllowance2)));
        //            listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(EmployeeRecord.Taxes.stateStatus)));
        //            listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(EmployeeRecord.Taxes.dependents)));
        //            listContents.Add(ConvertFunctions.BoolToString(EmployeeRecord.Taxes.SocSecCheckbox));

        //            break;
        //        case EXTENSION_INCOME:
        //            listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(EmployeeRecord.name)));
        //            listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(EmployeeRecord.Income.payPeriodsPerYear)));
        //            listContents.Add(ConvertFunctions.BoolToString(EmployeeRecord.Income.retainVacationCheckBox));
        //            listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(EmployeeRecord.Income.retainVacationPercentage)));
        //            if (EmployeeRecord.Income.recordInLinkedAccounts == true)
        //            {
        //                listContents.Add("The payroll linked accounts");
        //            }
        //            else
        //            {
        //                listContents.Add("");
        //            }
        //            listContents.Add(EmployeeRecord.Income.wageExpenseAccount.acctNumber);  // NC. changed from adding wageExpenseAccount to wageExpenseAccount.acctNumber

        //            break;
        //        case EXTENSION_DEDUCTIONS:
        //            listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(EmployeeRecord.name)));
        //            listContents.Add(Convert.ToString(iRowNum));
        //            if (EmployeeRecord.Deductions[iRowNum].Use == true)
        //            {
        //                listContents.Add("true");	// not sure if this is correct value or not
        //            }
        //            else
        //            {
        //                listContents.Add("false");	// not sure if this is correct value or not
        //            }
        //            listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(EmployeeRecord.Deductions[iRowNum].deductions)));
        //            listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(EmployeeRecord.Deductions[iRowNum].amountPerPayPeriod)));
        //            listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(EmployeeRecord.Deductions[iRowNum].percentPerPayPeriod)));
        //            listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(EmployeeRecord.Deductions[iRowNum].historicalAmount)));

        //            break;
        //        case EXTENSION_WCB:
        //            listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(EmployeeRecord.name)));
        //            listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(EmployeeRecord.Expenses.wcbRate)));
        //            break;
        //        case EXTENSION_ENTITLEMENTS:
        //            listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(EmployeeRecord.name)));
        //            listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(EmployeeRecord.Entitlements.hrsInWorkDay)));
        //            break;
        //        case EXTENSION_DIRECT_DEPOSIT:
        //            listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(EmployeeRecord.name)));
        //            listContents.Add(ConvertFunctions.BoolToString(EmployeeRecord.DirectDeposits.directDepositCheckBox));

        //            break;
        //        case EXTENSION_MEMO:
        //            listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(EmployeeRecord.name)));
        //            listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(EmployeeRecord.memo)));
        //            listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(EmployeeRecord.toDoDate)));
        //            listContents.Add(ConvertFunctions.BoolToString(EmployeeRecord.displayCheckBox));

        //            break;
        //        case EXTENSION_ADDITIONAL_INFO:
        //            listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(EmployeeRecord.name)));
        //            listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(EmployeeRecord.additional1)));
        //            listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(EmployeeRecord.additional2)));
        //            listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(EmployeeRecord.additional3)));
        //            listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(EmployeeRecord.additional4)));
        //            listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(EmployeeRecord.additional5)));
        //            listContents.Add(ConvertFunctions.BoolToString(EmployeeRecord.addCheckBox1));
        //            listContents.Add(ConvertFunctions.BoolToString(EmployeeRecord.addCheckBox2));
        //            listContents.Add(ConvertFunctions.BoolToString(EmployeeRecord.addCheckBox3));
        //            listContents.Add(ConvertFunctions.BoolToString(EmployeeRecord.addCheckBox4));
        //            listContents.Add(ConvertFunctions.BoolToString(EmployeeRecord.addCheckBox5));

        //            break;
        //        case EXTENSION_T4_RL1:
        //            listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(EmployeeRecord.name)));
        //            listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(EmployeeRecord.Reporting.rppDPSPRegNo)));
        //            listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(EmployeeRecord.Reporting.pensionAdjustment)));
        //            listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(EmployeeRecord.Reporting.t4EmpCode)));
        //            listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(EmployeeRecord.Reporting.historicalEIInsEarnings)));
        //            listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(EmployeeRecord.Reporting.historicalPensionableEarnings)));

        //            break;
        //        case EXTENSION_HISTORY_QUEBEC_TIPS:
        //            listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(EmployeeRecord.name)));
        //            listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(EmployeeRecord.Reporting.tippableSales)));
        //            listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(EmployeeRecord.Reporting.tipsFromSales)));
        //            listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(EmployeeRecord.Reporting.otherTips)));
        //            listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(EmployeeRecord.Reporting.tipsAllocated)));
        //            listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(EmployeeRecord.Reporting.fedTaxableTips)));
        //            listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(EmployeeRecord.Reporting.quebecTaxableTips)));

        //            break;
        //        case EXTENSION_USER_DEFINED_EXPENSE:
        //            listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(EmployeeRecord.name)));
        //            listContents.Add("User Exp 1");
        //            listContents.Add("User Exp 2");
        //            listContents.Add("User Exp 3");
        //            listContents.Add("User Exp 4");
        //            listContents.Add("User Exp 5");

        //            break;
        //        case EXTENSION_TAX_TABLE:
        //            listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(EmployeeRecord.name)));
        //            listContents.Add(Convert.ToString(EmployeeRecord.Taxes.HistoricalAmounts[iRowNum].iDataFileLine));
        //            listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(EmployeeRecord.Taxes.HistoricalAmounts[iRowNum].Amount)));

        //            break;
        //        case EXTENSION_INCOME_TABLE:
        //            listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(EmployeeRecord.name)));
        //            listContents.Add(Convert.ToString(EmployeeRecord.Income.IncomeData[iRowNum].iDataFileLine));
        //            listContents.Add(ConvertFunctions.BoolToString(EmployeeRecord.Income.IncomeData[iRowNum].Use));
        //            listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(EmployeeRecord.Income.IncomeData[iRowNum].income)));
        //            listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(EmployeeRecord.Income.IncomeData[iRowNum].unit)));
        //            listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(EmployeeRecord.Income.IncomeData[iRowNum].amountPerUnit)));
        //            listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(EmployeeRecord.Income.IncomeData[iRowNum].hoursPerPeriod)));
        //            listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(EmployeeRecord.Income.IncomeData[iRowNum].piecesPerPeriod)));
        //            listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(EmployeeRecord.Income.IncomeData[iRowNum].historicalAmount)));
        //            break;
        //        case EXTENSION_WCB_TABLE:
        //            listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(EmployeeRecord.name)));
        //            listContents.Add(Convert.ToString(iRowNum));
        //            listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(EmployeeRecord.Expenses.ExpenseRows[iRowNum].amountPerPeriod)));
        //            listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(EmployeeRecord.Expenses.ExpenseRows[iRowNum].historicalAmount)));
        //            break;
        //        case EXTENSION_ENTITLEMENTS_TABLE:
        //            listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(EmployeeRecord.name)));
        //            listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(EmployeeRecord.Entitlements.EntitlementRows[iRowNum].entitlementName)));
        //            listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(EmployeeRecord.Entitlements.EntitlementRows[iRowNum].percentageHoursWorked)));
        //            listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(EmployeeRecord.Entitlements.EntitlementRows[iRowNum].maximumDays)));
        //            if (EmployeeRecord.Entitlements.EntitlementRows[iRowNum].clearDays == true)
        //            {
        //                listContents.Add("Yes");
        //            }
        //            else
        //            {
        //                listContents.Add("No");
        //            }
        //            //ListAppend (listContents, EmployeeRecord.historicalDays)
        //            break;
        //        case EXTENSION_DIRECT_DEPOSIT_TABLE:
        //            listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(EmployeeRecord.name)));
        //            listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(EmployeeRecord.DirectDeposits.DepositAccounts[iRowNum].bankNumber)));
        //            listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(EmployeeRecord.DirectDeposits.DepositAccounts[iRowNum].transitNumber)));
        //            listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(EmployeeRecord.DirectDeposits.DepositAccounts[iRowNum].accountNumber)));
        //            listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(EmployeeRecord.DirectDeposits.DepositAccounts[iRowNum].amount)));
        //            listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(EmployeeRecord.DirectDeposits.DepositAccounts[iRowNum].percentage)));
        //            if (EmployeeRecord.DirectDeposits.DepositAccounts[iRowNum].ActiveStatus == true)
        //            {
        //                listContents.Add("Active");
        //            }
        //            else
        //            {
        //                listContents.Add("Inactive");
        //            }
        //            break;
        //        default:
        //            {
        //                FunctionsLib.Verify(false, true, "Valid value sent");
        //                break;
        //            }
        //    }
        //    return listContents;
        //}

    } 
    
    public static class EmployeeIcon
    {
    	public static PayrollLedgerResFolders.EmployeeIconAppFolder repo = PayrollLedgerRes.Instance.EmployeeIcon;
    }
}