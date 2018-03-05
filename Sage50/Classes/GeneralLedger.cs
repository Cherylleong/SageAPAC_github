/*
 * Created by Ranorex
 * User: wonda05
 * Date: 7/22/2016
 * Time: 9:06 AM
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
	/// Description of GeneralLedger.
	/// </summary>
	public static class GeneralLedger
	{

        private const string FUNCTION_ALIAS = "GL";
        private const string EXTENSION_HEADER = ".HDR";
        private const string EXTENSION_CLASS_OPTIONS = ".DT1";
        private const string EXTENSION_ADDITIONAL_INFO = ".DT2";
        private const string EXTENSION_BUDGET = ".DT3";



        public static GeneralLedgerResFolders.GeneralLedgerAppFolder repo = GeneralLedgerRes.Instance.GeneralLedger;

        public static void _SA_Invoke(Boolean bOpenLedger)
        {
            // open ledger depending on view type

            if (Simply.isEnhancedView())
            {
                Simply.repo.Self.Activate();
                Simply.repo.GeneralLink.Click();
                Simply.repo.GeneralIcon.Click();
            }
            else
            {

            }

            if (GeneralIcon.repo.SelfInfo.Exists())
            {
                if (bOpenLedger == true)
                {
                    GeneralIcon.repo.CreateNew.Click();
                    GeneralIcon.repo.Self.Close();
                }
            }
        }

        public static void _SA_Invoke()
        {
            GeneralLedger._SA_Invoke(true);
        }

        public static void _SA_Open(GL_ACCOUNT acct)
        {
            if (!GeneralLedger.repo.SelfInfo.Exists())
            {
                GeneralLedger._SA_Invoke();
            }
            GeneralLedger.repo.SelectRecord.Select(acct.name);
        }

        public static void _SA_Create(GL_ACCOUNT acct)
        {
            GeneralLedger._SA_Create(acct, true, false);
        }
        public static void _SA_Create(GL_ACCOUNT acct, bool bSave, bool bEdit)
        {
            if (!Variables.bUseDataFiles && !bEdit)
            {
                GeneralLedger._SA_MatchDefaults(acct);
            }

            if (!GeneralLedger.repo.SelfInfo.Exists())
            {
                GeneralLedger._SA_Invoke();
            }

            if (bEdit)
            {
                if (GeneralLedger.repo.SelectRecord.SelectedItemText != acct.name)
                {
                    GeneralLedger._SA_Open(acct);
                }

                if (Functions.GoodData(acct.editedAcctNumber))
                {
                    GeneralLedger.repo.AccountNumber.TextValue = acct.editedAcctNumber;

                    // handle message for account class
                    //SimplyMessage._SA_HandleMessage();

                    acct.acctNumber = acct.editedAcctNumber;
                }

                if (Functions.GoodData(acct.editedAcctName))
                {
                    GeneralLedger.repo.AccountName.TextValue = acct.editedAcctName;
                    acct.acctNumber = string.Format("{0} {1}",acct.acctNumber, acct.editedAcctName);
                    acct.acctNumber = acct.acctNumber.Trim(' ');
                }
                // print to log or results
                Ranorex.Report.Info(String.Format("Modifying GL Account {0}", acct.name));
            }
            else
            {          
                string acctNum, acctName;
                Functions.SplitAccountNumberString(acct.acctNumber, out acctNum, out acctName);
                GeneralLedger.repo.AccountNumber.TextValue = acctNum;
                GeneralLedger.repo.AccountName.TextValue = acctName;

                if (GeneralLedger.repo.LanguageBtnInfo.Exists() && Functions.GoodData(acct.acctNameFre))
                {
                    GeneralLedger.repo.LanguageBtn.Click();
                    EnterAccountName.repo.English2.TextValue = acct.acctNameFre;
                    EnterAccountName.repo.OK.Click();
                }
                Ranorex.Report.Info(String.Format("Creating GL Account {0}", acct.name));
            }

            #region Account Tab

            // Select tab 
            GeneralLedger.repo.Account.Tab.Click();

            if (GeneralLedger.repo.Self.Enabled && acct.acctType != GL_ACCT_TYPE.GL_ACCT_BLANK)
            {
                // have to create function to select correct radio, as well as add radio buttons
                switch (acct.acctType)
                {
                    case GL_ACCT_TYPE.GL_ACCT_GROUP_HEADING:
                        {
                            GeneralLedger.repo.Account.AcctGrpHeading.Select();
                            break;
                        }
                    case GL_ACCT_TYPE.GL_ACCT_SUBGROUP_ACCOUNT:
                        {
                            GeneralLedger.repo.Account.SubGrpAcct.Select();
                            break;
                        }
                    case GL_ACCT_TYPE.GL_ACCT_SUBGROUP_TOTAL:
                        {
                            GeneralLedger.repo.Account.SubGrpTotal.Select();
                            break;
                        }
                    case GL_ACCT_TYPE.GL_ACCT_GROUP_ACCOUNT:
                        {
                            GeneralLedger.repo.Account.GrpAcct.Select();
                            break;
                        }
                    case GL_ACCT_TYPE.GL_ACCT_GROUP_TOTAL:
                        {
                            GeneralLedger.repo.Account.GrpTotal.Select();
                            break;
                        }
                    default:
                        {
                            acct.acctType = GL_ACCT_TYPE.GL_ACCT_BLANK;
                            break;
                        }
                }
            }
            if (Functions.GoodData(acct.inactiveCheckBox) && acct.acctType != GL_ACCT_TYPE.GL_ACCT_SUBGROUP_TOTAL && acct.acctType != GL_ACCT_TYPE.GL_ACCT_GROUP_TOTAL && acct.acctType != GL_ACCT_TYPE.GL_ACCT_GROUP_HEADING)
            // Subgroup total, group total and group heading do not have inactive checkbox
            {
                GeneralLedger.repo.InactiveAccount.SetState(acct.inactiveCheckBox);
            }

            // Does not exists for US version
            if (Functions.GoodData(acct.gifi))
            {
                GeneralLedger.repo.Account.GifiCode.TextValue = acct.gifi;  
            }

            if (Functions.GoodData(acct.omitFromChkBox))
            {
                if (GeneralLedger.repo.Account.OmitFromInfo.Exists())
                {
                    GeneralLedger.repo.Account.OmitFrom.SetState(acct.omitFromChkBox);
                }
            }
            if (Functions.GoodData(acct.allowPrjAllocChkBox))
            {
                if (GeneralLedger.repo.Account.AllowProjectAllocationInfo.Exists())
                {
                    GeneralLedger.repo.Account.AllowProjectAllocation.SetState(acct.allowPrjAllocChkBox);
                }
            }
            if (Functions.GoodData(acct.openBalance))
            {
                if (GeneralLedger.repo.OpenBalanceInfo.Exists())
                {
                    GeneralLedger.repo.OpenBalance.TextValue = acct.openBalance;
                }
            }
            if (Functions.GoodData(acct.frgnOpenBalance))
            {
                if (GeneralLedger.repo.ForeignOpenBalanceInfo.Exists())
                {
                    GeneralLedger.repo.ForeignOpenBalance.TextValue = acct.frgnOpenBalance;
                }
            }

            #endregion

            #region Class Options
            // if (Functions.GoodData(acct.acctClass))
            if (acct.acctClass.ToString() != "0")
            {
                GeneralLedger.repo.ClassOptions.Tab.Click();
            
                // Enter data on the class options tab
                if (GeneralLedger.repo.ClassOptions.AccountClassInfo.Exists())
				{
					string acctClass = GeneralLedger._SA_GetAccountClassName(acct.acctClass);
					GeneralLedger.repo.ClassOptions.AccountClass.Select(acctClass);
					
					//SimplyMessage.repo._SA_HandleMessage(SimplyMessage.NO_LOC, SimplyMessage._MSG_PLACEHOLDERTOFIXEXISTINGMETHODCALLS_LOC);
					
					// The following fields are only availabe to certain types of accounts
					if (Functions.GoodData (acct.institution) && acct.institution != "" && (acct.acctClass == GL_ACCT_CLASS.GL_ACCT_CLASS_BANK || acct.acctClass == GL_ACCT_CLASS.GL_ACCT_CLASS_CCRE || acct.acctClass == GL_ACCT_CLASS.GL_ACCT_CLASS_CCPY))
					{
						GeneralLedger.repo.ClassOptions.Institution.Select(acct.institution);
					}
					if (Functions.GoodData (acct.branchName) && (acct.acctClass == GL_ACCT_CLASS.GL_ACCT_CLASS_BANK || acct.acctClass == GL_ACCT_CLASS.GL_ACCT_CLASS_CCRE || acct.acctClass == GL_ACCT_CLASS.GL_ACCT_CLASS_CCPY))
					{
						GeneralLedger.repo.ClassOptions.BranchName.TextValue = acct.branchName;
					}
					if (Functions.GoodData (acct.transNumber) && (acct.acctClass == GL_ACCT_CLASS.GL_ACCT_CLASS_BANK))
					{
						GeneralLedger.repo.ClassOptions.TransitNumber.TextValue = acct.transNumber;
					}
					if (Functions.GoodData (acct.bankAcctNum) && (acct.acctClass == GL_ACCT_CLASS.GL_ACCT_CLASS_BANK || acct.acctClass == GL_ACCT_CLASS.GL_ACCT_CLASS_CCRE || acct.acctClass == GL_ACCT_CLASS.GL_ACCT_CLASS_CCPY))
					{
						GeneralLedger.repo.ClassOptions.BankAccountNumber.TextValue = acct.bankAcctNum;
					}
					if (Functions.GoodData (acct.bankAcctType) && (acct.acctClass == GL_ACCT_CLASS.GL_ACCT_CLASS_BANK))
					{
						GeneralLedger.repo.ClassOptions.BankAccountType.Select(acct.bankAcctType);
					}
					if (Functions.GoodData (acct.nextDepositNo) &&  (acct.acctClass == GL_ACCT_CLASS.GL_ACCT_CLASS_BANK || acct.acctClass == GL_ACCT_CLASS.GL_ACCT_CLASS_CASH))
					{
						GeneralLedger.repo.ClassOptions.NextDepositNumber.TextValue = acct.nextDepositNo;
					}
					if (Functions.GoodData (acct.useForOnlineChkBox) && (acct.acctClass == GL_ACCT_CLASS.GL_ACCT_CLASS_BANK || acct.acctClass == GL_ACCT_CLASS.GL_ACCT_CLASS_CCRE || acct.acctClass == GL_ACCT_CLASS.GL_ACCT_CLASS_CCPY))
					{
						GeneralLedger.repo.ClassOptions.UseForOnline.SetState(acct.useForOnlineChkBox);
					}
					if (Functions.GoodData (acct.homePage) && (acct.acctClass == GL_ACCT_CLASS.GL_ACCT_CLASS_BANK || acct.acctClass == GL_ACCT_CLASS.GL_ACCT_CLASS_CCRE || acct.acctClass == GL_ACCT_CLASS.GL_ACCT_CLASS_CCPY))
					{
						GeneralLedger.repo.ClassOptions.HomePage.Focus();
						GeneralLedger.repo.ClassOptions.HomePage.TextValue = acct.homePage;
					}
					if (Functions.GoodData (acct.login) && (acct.acctClass == GL_ACCT_CLASS.GL_ACCT_CLASS_BANK || acct.acctClass == GL_ACCT_CLASS.GL_ACCT_CLASS_CCRE || acct.acctClass == GL_ACCT_CLASS.GL_ACCT_CLASS_CCPY))
					{
						GeneralLedger.repo.ClassOptions.OnlineLogin.TextValue = acct.login;
					}
					if (Functions.GoodData (acct.currencyCode) && (acct.acctClass == GL_ACCT_CLASS.GL_ACCT_CLASS_BANK || acct.acctClass == GL_ACCT_CLASS.GL_ACCT_CLASS_CASH))
					{
                        if (GeneralLedger.repo.ClassOptions.CurrencyCodeInfo.Exists())
						{
							GeneralLedger.repo.ClassOptions.CurrencyCode.Select(acct.currencyCode);
						}
					}
				}
			}
            #endregion

            #region Budget
            // ship to address tab

            if (Functions.GoodData(acct.budgetChkBox))
            {
                // Need to save the record before processing Budget info
                GeneralLedger.repo.Save.Click();
                //Functions.WUEn(GeneralLedger.repo.SelfInfo);
                // Enter data into Budget tab
                {
                    GeneralLedger.repo.Budget.Tab.Click();

                    // Set Budget check box
                    if (GeneralLedger.repo.Budget.BudgetThisAccountInfo.Exists())
                    {
                        GeneralLedger.repo.Budget.BudgetThisAccount.SetState(acct.budgetChkBox);
                    }
                }
            }
            #endregion

            #region Addtional Info

            if (Functions.GoodData(acct.additional1) || Functions.GoodData(acct.additional2) || Functions.GoodData(acct.additional3) || Functions.GoodData(acct.additional4) || Functions.GoodData(acct.additional5))
            {
            	GeneralLedger.repo.AdditionalInfo.Tab.Click();

                if (Functions.GoodData(acct.additional1))
                {
                    if (GeneralLedger.repo.AdditionalInfo.Additional1Info.Exists())
                    {
                        GeneralLedger.repo.AdditionalInfo.Additional1.Focus();
                        GeneralLedger.repo.AdditionalInfo.Additional1.TextValue = acct.additional1;
                    }
                }
                if (Functions.GoodData(acct.additional2))
                {
                    if (GeneralLedger.repo.AdditionalInfo.Additional2Info.Exists())
                    {
                        GeneralLedger.repo.AdditionalInfo.Additional2.TextValue = acct.additional2;
                    }
                }
                if (Functions.GoodData(acct.additional3))
                {
                     if (GeneralLedger.repo.AdditionalInfo.Additional3Info.Exists())
                    {
                        GeneralLedger.repo.AdditionalInfo.Additional3.TextValue = acct.additional3;
                    }
                }
                if (Functions.GoodData(acct.additional4))
                {
                    if (GeneralLedger.repo.AdditionalInfo.Additional4Info.Exists())
                    {
                        GeneralLedger.repo.AdditionalInfo.Additional4.TextValue = acct.additional4;
                    }
                }
                if (Functions.GoodData(acct.additional5))
                {
                     if (GeneralLedger.repo.AdditionalInfo.Additional5Info.Exists())
                    {
                        GeneralLedger.repo.AdditionalInfo.Additional5.TextValue = acct.additional5;
                    }
                }
            }
			
            #endregion            

            if (bSave)
            {
                GeneralLedger.repo.Save.Click();

                //if (Variables.bUseDataFiles)	// if external data files are not used
                //{
                //    if (s_desktop.Exists(SimplyMessage.YES_LOC))
                //    {
                //        SimplyMessage.repo._SA_HandleMessage(SimplyMessage.YES_LOC, SimplyMessage._MSG_PLACEHOLDERTOFIXEXISTINGMETHODCALLS_LOC);
                //    }

                //    SimplyMessage.repo._SA_HandleMessage(SimplyMessage.OK_LOC, SimplyMessage._MSG_YOUHAVEENTEREDADUPLICATEENTRY_LOC, false, true);
                //    // discard the changes
                //    GeneralLedger.repo.ClickUndoChanges();              
                //}
			}            
        }

        public static void _SA_MatchDefaults(GL_ACCOUNT acct)
        {
            //to fill in later
            //if ((!Functions.GoodData(GLRecord.acctType) || GLRecord.acctType == GL_ACCT_TYPE.GL_ACCT_BLANK))
            if (acct.acctType == 0 || acct.acctType == GL_ACCT_TYPE.GL_ACCT_BLANK)
            {
                acct.acctType = GL_ACCT_TYPE.GL_ACCT_GROUP_ACCOUNT;
            }
            if (!Functions.GoodData(acct.inactiveCheckBox))
            {
                acct.inactiveCheckBox = false;
            }
            // fields available only to subgroup and group accounts
            if ((Functions.GoodData(acct.acctType) && (acct.acctType == GL_ACCT_TYPE.GL_ACCT_SUBGROUP_ACCOUNT || acct.acctType == GL_ACCT_TYPE.GL_ACCT_GROUP_ACCOUNT)))
            {
                if (!Functions.GoodData(acct.openBalance))
                {
                    acct.openBalance = "0.00";
                }
                string acctNum = Functions.GetField(acct.acctNumber, " ", 1);
                //if ((!Functions.GoodData(GLRecord.acctClass) && Functions.IsDigit(acctNum)))
                if (acct.acctClass == 0 && Functions.IsDigit(acctNum))
                {
                    // if it's an asset account, set account class to default value "Asset"
                    if ((Convert.ToDouble(acctNum) >= Convert.ToDouble(Variables.globalSettings.GeneralSettings.Numbering.assetStartNum) && Convert.ToDouble(acctNum) <= Convert.ToDouble(Variables.globalSettings.GeneralSettings.Numbering.assetEndNum)))
                    {
                        acct.acctClass = GL_ACCT_CLASS.GL_ACCT_CLASS_ASST;
                    }
                    // if it's an equity account, set account class to default value "Equity"
                    if ((Convert.ToDouble(acctNum) >= Convert.ToDouble(Variables.globalSettings.GeneralSettings.Numbering.equityStartNum) && Convert.ToDouble(acctNum) <= Convert.ToDouble(Variables.globalSettings.GeneralSettings.Numbering.equityEndNum)))
                    {
                        acct.acctClass = GL_ACCT_CLASS.GL_ACCT_CLASS_EQUI;
                    }
                    // if it's a liability account, set account class to default value "Liability"
                    if ((Convert.ToDouble(acctNum) >= Convert.ToDouble(Variables.globalSettings.GeneralSettings.Numbering.liabilityStartNum) && Convert.ToDouble(acctNum) <= Convert.ToDouble(Variables.globalSettings.GeneralSettings.Numbering.liabilityEndNum)))
                    {
                        acct.acctClass = GL_ACCT_CLASS.GL_ACCT_CLASS_LIAB;
                    }
                    // if it's a revenue account, set account class to default value "Revenue"
                    if ((Convert.ToDouble(acctNum) >= Convert.ToDouble(Variables.globalSettings.GeneralSettings.Numbering.revStartNum) && Convert.ToDouble(acctNum) <= Convert.ToDouble(Variables.globalSettings.GeneralSettings.Numbering.revEndNum)))
                    {
                        acct.acctClass = GL_ACCT_CLASS.GL_ACCT_CLASS_REVE;
                    }
                    // if it's an expense account, set account class to default value "Expense"
                    if ((Convert.ToDouble(acctNum) >= Convert.ToDouble(Variables.globalSettings.GeneralSettings.Numbering.expStartNum) && Convert.ToDouble(acctNum) <= Convert.ToDouble(Variables.globalSettings.GeneralSettings.Numbering.expEndNum)))
                    {
                        acct.acctClass = GL_ACCT_CLASS.GL_ACCT_CLASS_EXPS;
                    }
                }
                if (Functions.GoodData(acct.acctClass))
                {
                    // fields available only to accoutns that belong to certain account classes (Bank, Cash, Credit Card Receivable, Credit Card Payable)
                    if ((!Functions.GoodData(acct.institution) || acct.institution == ""))
                    {
                        if ((acct.acctClass == GL_ACCT_CLASS.GL_ACCT_CLASS_BANK) || (acct.acctClass == GL_ACCT_CLASS.GL_ACCT_CLASS_CCRE) || (acct.acctClass == GL_ACCT_CLASS.GL_ACCT_CLASS_CCPY))
                        {
                            acct.institution = "Other";
                        }
                    }
                    // fields available only to Bank accounts
                    if (acct.acctClass == GL_ACCT_CLASS.GL_ACCT_CLASS_BANK)
                    {
                        if ((!Functions.GoodData(acct.bankAcctType) || acct.bankAcctType == ""))
                        {
                            acct.bankAcctType = "Chequing";
                        }
                        if ((!Functions.GoodData(acct.nextDepositNo) || acct.nextDepositNo == ""))
                        {
                            acct.nextDepositNo = "1";
                        }

                    }
                }
            }
            if (Variables.globalSettings.CompanySettings.CurrencySettings.allowForeignCurrency == true)
            {
                if (!Functions.GoodData(acct.currencyCode) && (acct.acctClass == GL_ACCT_CLASS.GL_ACCT_CLASS_BANK || acct.acctClass == GL_ACCT_CLASS.GL_ACCT_CLASS_CASH))
                {
                    acct.currencyCode = Variables.globalSettings.CompanySettings.CurrencySettings.HomeCurrency.currencyCode;
                }
            }
        }


        public static GL_ACCOUNT _SA_Read()
        {
            return _SA_Read(null);
        }

        public static GL_ACCOUNT _SA_Read(string sIDToRead) //  method will read all fields and store the data in a GL_ACCOUNT record
        {
            GL_ACCOUNT acct = new GL_ACCOUNT();
            if (Functions.GoodData(sIDToRead))
            {
                acct.acctNumber = sIDToRead;
                if (GeneralLedger.repo.SelectRecord.SelectedItemText != acct.acctNumber)
                {
                    GeneralLedger._SA_Open(acct);
                }
            }

            acct.acctNumber = GeneralLedger.repo.SelectRecord.Text;
            acct.editedAcctNumber = GeneralLedger.repo.AccountNumber.TextValue;
            acct.nameEdit = GeneralLedger.repo.AccountName.TextValue;

    
            GeneralLedger.repo.Account.Tab.Click();

            if (GeneralLedger.repo.Account.AcctGrpHeading.Checked)
            {
                acct.acctType = acct.acctType = GL_ACCT_TYPE.GL_ACCT_GROUP_HEADING;
            }
            else if(GeneralLedger.repo.Account.SubGrpAcct.Checked)
            {
                acct.acctType = GL_ACCT_TYPE.GL_ACCT_SUBGROUP_ACCOUNT;
            }
            else if (GeneralLedger.repo.Account.SubGrpTotal.Checked)
            {
                acct.acctType = GL_ACCT_TYPE.GL_ACCT_SUBGROUP_TOTAL;
            }
            else if (GeneralLedger.repo.Account.GrpAcct.Checked)
            {
                acct.acctType = GL_ACCT_TYPE.GL_ACCT_GROUP_ACCOUNT;
            }
            else if (GeneralLedger.repo.Account.GrpTotal.Checked)
            {
                acct.acctType = GL_ACCT_TYPE.GL_ACCT_GROUP_TOTAL;
            }
            else
            {
                acct.acctType = GL_ACCT_TYPE.GL_ACCT_BLANK;
            }


            if (GeneralLedger.repo.Account.GifiCodeInfo.Exists())
            {
                acct.gifi = GeneralLedger.repo.Account.GifiCode.TextValue;
            }
            else
            {
                acct.gifi = null;
            }
            if (GeneralLedger.repo.Account.OmitFromInfo.Exists())
            {
                acct.omitFromChkBox = GeneralLedger.repo.Account.OmitFrom.Checked;
            }
 
            if (GeneralLedger.repo.Account.AllowProjectAllocationInfo.Exists())
            {
                acct.allowPrjAllocChkBox = GeneralLedger.repo.Account.AllowProjectAllocation.Checked;
            }
   
            if (GeneralLedger.repo.OpenBalanceInfo.Exists())
            {
                acct.openBalance = GeneralLedger.repo.OpenBalance.TextValue;
            }
            else
            {
                acct.openBalance = null;
            }

            if (GeneralLedger.repo.ForeignOpenBalanceInfo.Exists())
            {
                acct.frgnOpenBalance = GeneralLedger.repo.ForeignOpenBalance.TextValue;
            }
            else
            {
                acct.frgnOpenBalance = null;
            }

            GeneralLedger.repo.ClassOptions.Tab.Click();
            // if the field presents, record the values
            if (GeneralLedger.repo.ClassOptions.AccountClassInfo.Exists() && (GeneralLedger.repo.ClassOptions.AccountClass.Enabled))
            {
                acct.acctClass = GeneralLedger._SA_GetAccountClass(GeneralLedger.repo.ClassOptions.AccountClass.Text);
            }
            else
            {
                acct.acctClass = GL_ACCT_CLASS.GL_ACCT_CLASS_BLANK;
            }
            if (GeneralLedger.repo.ClassOptions.NextDepositNumberInfo.Exists())
            {
                acct.nextDepositNo = GeneralLedger.repo.ClassOptions.NextDepositNumber.TextValue;
            }
            else
            {
                acct.nextDepositNo = null;
            }
            if (GeneralLedger.repo.ClassOptions.InstitutionInfo.Exists())
            {
                acct.institution = GeneralLedger.repo.ClassOptions.Institution.Text;
            }
            else
            {
                acct.institution = null;

            }
            if (GeneralLedger.repo.ClassOptions.BranchNameInfo.Exists())
            {
                acct.branchName = GeneralLedger.repo.ClassOptions.BranchName.TextValue;
            }
            else
            {
                acct.branchName = null;
            }
            if (GeneralLedger.repo.ClassOptions.TransitNumberInfo.Exists())
            {
                acct.transNumber = GeneralLedger.repo.ClassOptions.TransitNumber.TextValue;
            }
            else
            {
                acct.transNumber = null;
            }
            if (GeneralLedger.repo.ClassOptions.BankAccountNumberInfo.Exists())
            {
                acct.bankAcctNum = GeneralLedger.repo.ClassOptions.BankAccountNumber.TextValue;
            }
            else
            {
                acct.bankAcctNum = null;
            }
            if (GeneralLedger.repo.ClassOptions.CurrencyCodeInfo.Exists())
            {
                acct.currencyCode = GeneralLedger.repo.ClassOptions.CurrencyCode.Text;
            }
            else
            {
                acct.currencyCode = null;
            }
            if (GeneralLedger.repo.ClassOptions.BankAccountTypeInfo.Exists())
            {
                acct.bankAcctType = GeneralLedger.repo.ClassOptions.BankAccountType.Text;
            }
            else
            {
                acct.bankAcctType = null;
            }
            if (GeneralLedger.repo.ClassOptions.UseForOnlineInfo.Exists())
            {
                acct.useForOnlineChkBox = GeneralLedger.repo.ClassOptions.UseForOnline.Checked;
            }
            else
            {
                acct.useForOnlineChkBox = false;
            }
            if (GeneralLedger.repo.ClassOptions.HomePageInfo.Exists())
            {
                acct.homePage = GeneralLedger.repo.ClassOptions.HomePage.TextValue;
            }
            else
            {
                acct.homePage = null;
            }
            if (GeneralLedger.repo.ClassOptions.OnlineLoginInfo.Exists())
            {
                acct.login = GeneralLedger.repo.ClassOptions.OnlineLogin.TextValue;
            }
            else
            {
                acct.login = null;
            }

            //if (GeneralLedger.repo.Tab.FindPage("Budget") )
            //{
            GeneralLedger.repo.Budget.Tab.Click();
            if (GeneralLedger.repo.Budget.BudgetThisAccountInfo.Exists())
            {
                acct.budgetChkBox = GeneralLedger.repo.Budget.BudgetThisAccount.Checked;
            }
            else
            {
                acct.budgetChkBox = false;
            }

            //}

            GeneralLedger.repo.AdditionalInfo.Tab.Click();
            if (GeneralLedger.repo.AdditionalInfo.Additional1Info.Exists())
            {
                acct.additional1 = GeneralLedger.repo.AdditionalInfo.Additional1.TextValue;
            }
            else
            {
                acct.additional1 = null;
            }
            if (GeneralLedger.repo.AdditionalInfo.Additional2Info.Exists())
            {
                acct.additional2 = GeneralLedger.repo.AdditionalInfo.Additional2.TextValue;
            }
            else
            {
                acct.additional2 = null;
            }
            if (GeneralLedger.repo.AdditionalInfo.Additional3Info.Exists())
            {
                acct.additional3 = GeneralLedger.repo.AdditionalInfo.Additional3.TextValue;
            }
            else
            {
                acct.additional3 = null;
            }
            if (GeneralLedger.repo.AdditionalInfo.Additional4Info.Exists())
            {
                acct.additional4 = GeneralLedger.repo.AdditionalInfo.Additional4.TextValue;
            }
            else
            {
                acct.additional4 = null;
            }
            if (GeneralLedger.repo.AdditionalInfo.Additional5Info.Exists())
            {
                acct.additional5 = GeneralLedger.repo.AdditionalInfo.Additional5.TextValue;
            }
            else
            {
                acct.additional5 = null;
            }

            // Get the French Account Name if billingual is on. Otherwise, leave it blank
            if (GeneralLedger.repo.LanguageBtnInfo.Exists())
            {
                GeneralLedger.repo.LanguageBtn.Click();
                acct.acctNameFre = EnterAccountName.repo.English2.TextValue;
                EnterAccountName.repo.Cancel.Click();
            }
            else
            {
                acct.acctNameFre = null;
            }

            return acct;
        }
        
        public static void _SA_Close()
		{
        	System.Threading.Thread.Sleep(2000);
			repo.Self.Close();	
		}

        public static void DataFile_ReadFile(string sDataLocation, string fileCounter)
        {
            // Local variables
            string dataLine;	        // Stores the current field data from file
            string dataPath;	        // The name and path of the data file

            StreamReader FileHDR;	// File handle for Account tab info
            StreamReader FileDT1;	// File handle for the Class Options tab info
            StreamReader FileDT2;	// File handle for the Additional info tab info
            StreamReader FileDT3;	// File handle for the Budget tab info

            // Get the data path from file
            dataPath = sDataLocation + "GL" + fileCounter;


            List<GL_ACCOUNT> lGL = new List<GL_ACCOUNT>();

            // Open all data files, if they exist, if not then flag not to do the info for the missing file
            FileHDR = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_HEADER, "FM_READ"));
            while ((dataLine = FileHDR.ReadLine()) != null)
            {
                GL_ACCOUNT myGL = new GL_ACCOUNT();
                myGL = DataFile_setDataStructure(EXTENSION_HEADER, dataLine, myGL);

                if (File.Exists(dataPath + EXTENSION_CLASS_OPTIONS))
                {
                    using (FileDT1 = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_CLASS_OPTIONS, "FM_READ")))
                    {
                        while ((dataLine = FileDT1.ReadLine()) != null)
                        {
                            myGL = GeneralLedger.DataFile_setDataStructure(EXTENSION_CLASS_OPTIONS, dataLine, myGL);
                        }
                    }
                    //Functions.FileClose(FileDT1);
                }

                if (File.Exists(dataPath + EXTENSION_ADDITIONAL_INFO))
                {
                    using (FileDT2 = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_ADDITIONAL_INFO, "FM_READ")))
                    {
                        //dataLine = "";
                        while ((dataLine = FileDT2.ReadLine()) != null)
                        {
                            if (dataLine != "")
                            {
                                myGL = GeneralLedger.DataFile_setDataStructure(EXTENSION_ADDITIONAL_INFO, dataLine, myGL);
                            }
                        }
                    }
                    //Functions.FileClose(FileDT2);
                }

                if (File.Exists(dataPath + EXTENSION_BUDGET))
                {
                    using (FileDT3 = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_BUDGET, "FM_READ")))
                    {
                        //dataLine = "";
                        while ((dataLine = FileDT3.ReadLine()) != null)
                        {
                            if (dataLine != "")
                            {
                                myGL = GeneralLedger.DataFile_setDataStructure(EXTENSION_BUDGET, dataLine, myGL);
                            }
                        }
                    }
                    //Functions.FileClose (FileDT3);
                }

                lGL.Add(myGL);
            }


            FileHDR.Close();

            foreach (GL_ACCOUNT myGL in lGL)
            {
                // Determine the mode
                switch (myGL.action)
                {
                    case "A":
                        GeneralLedger._SA_Create(myGL);

                        break;
                    case "E":
                        GeneralLedger._SA_Create(myGL, true, true);

                        break;
                    case "D":
                        //GeneralLedger._SA_Delete(myGL);

                        break;
                    default:
                        {
                            Functions.Verify(false, true, "Action set properly");
                            //GeneralLedger.ClickUndoChanges();

                            // Handle the undo message
                            // pHandlePopupMessage(Confirmation, "Yes", SilkLogFile)
                            break;
                        }
                }
                // Close all data files
            }

            if (GeneralLedger.repo.SelfInfo.Exists())
            {
                GeneralLedger.repo.Self.Close();
            }

        }

        public static GL_ACCOUNT DataFile_setDataStructure(string extension, string dataLine, GL_ACCOUNT GLRec)
        {
            GL_ACCOUNT acct = GLRec;


            switch (extension.ToUpper())
            {
                case EXTENSION_HEADER:
                    acct.action = ConvertFunctions.TextToComma(Functions.GetField(dataLine, ",", 1));
                    //GLRecord.acctNumber = TextToComma(Functions.GetField (dataLine, ",", 2))
                    // get the account number and accout name
                    acct.acctNumber = ConvertFunctions.TextToComma(Functions.GetField(dataLine, ",", 2)) + " " + ConvertFunctions.TextToComma(Functions.GetField(dataLine, ",", 3));
                    acct.openBalance = ConvertFunctions.TextToComma(Functions.GetField(dataLine, ",", 4));
                    string S = ConvertFunctions.TextToComma(Functions.GetField(dataLine, ",", 5));

                    switch (S)
                    {
                        case "H":
                            acct.acctType = GL_ACCT_TYPE.GL_ACCT_GROUP_HEADING;
                            break;
                        case "A":
                            acct.acctType = GL_ACCT_TYPE.GL_ACCT_SUBGROUP_ACCOUNT;
                            break;
                        case "S":
                            acct.acctType = GL_ACCT_TYPE.GL_ACCT_SUBGROUP_TOTAL;
                            break;
                        case "G":
                            acct.acctType = GL_ACCT_TYPE.GL_ACCT_GROUP_ACCOUNT;
                            break;
                        case "T":
                            acct.acctType = GL_ACCT_TYPE.GL_ACCT_GROUP_TOTAL;
                            break;
                        default:
                            {
                                acct.acctType = GL_ACCT_TYPE.GL_ACCT_BLANK;
                                break;
                            }
                    }
                    acct.omitFromChkBox = ConvertFunctions.StringToBool(ConvertFunctions.TextToComma(Functions.GetField(dataLine, ",", 6)));
                    acct.allowPrjAllocChkBox = ConvertFunctions.StringToBool(ConvertFunctions.TextToComma(Functions.GetField(dataLine, ",", 7)));
                    acct.gifi = ConvertFunctions.TextToComma(Functions.GetField(dataLine, ",", 8));
                    acct.frgnOpenBalance = ConvertFunctions.TextToComma(Functions.GetField(dataLine, ",", 9));
                    acct.acctNameFre = ConvertFunctions.TextToComma(Functions.GetField(dataLine, ",", 10));
                    acct.editedAcctNumber = ConvertFunctions.TextToComma(Functions.GetField(dataLine, ",", 11));
                    acct.nameEdit = ConvertFunctions.TextToComma(Functions.GetField(dataLine, ",", 12));
                    break;
                case EXTENSION_CLASS_OPTIONS:
                    if (acct.acctNumber == (Functions.GetField(dataLine, ",", 1) + " " + Functions.GetField(dataLine, ",", 2)))
                    {
                        acct.acctClass = DataFile_getAccountClassFromAbbrev(ConvertFunctions.TextToComma(Functions.GetField(dataLine, ",", 3)));
                        acct.currencyCode = ConvertFunctions.TextToComma(Functions.GetField(dataLine, ",", 4));
                        acct.institution = ConvertFunctions.TextToComma(Functions.GetField(dataLine, ",", 5));
                        acct.branchName = ConvertFunctions.TextToComma(Functions.GetField(dataLine, ",", 6));
                        acct.transNumber = ConvertFunctions.TextToComma(Functions.GetField(dataLine, ",", 7));
                        acct.bankAcctNum = ConvertFunctions.TextToComma(Functions.GetField(dataLine, ",", 8));
                        acct.bankAcctType = ConvertFunctions.TextToComma(Functions.GetField(dataLine, ",", 9));
                        acct.nextDepositNo = ConvertFunctions.TextToComma(Functions.GetField(dataLine, ",", 10));
                        acct.useForOnlineChkBox = ConvertFunctions.StringToBool(ConvertFunctions.TextToComma(Functions.GetField(dataLine, ",", 11)));
                        acct.homePage = ConvertFunctions.TextToComma(Functions.GetField(dataLine, ",", 12));
                        acct.login = ConvertFunctions.TextToComma(Functions.GetField(dataLine, ",", 13));
                    }
                    break;
                case EXTENSION_ADDITIONAL_INFO:
                    if (acct.acctNumber == (Functions.GetField(dataLine, ",", 1) + " " + Functions.GetField(dataLine, ",", 2)))
                    {
                        acct.additional1 = ConvertFunctions.TextToComma(Functions.GetField(dataLine, ",", 3));
                        acct.additional2 = ConvertFunctions.TextToComma(Functions.GetField(dataLine, ",", 4));
                        acct.additional3 = ConvertFunctions.TextToComma(Functions.GetField(dataLine, ",", 5));
                        acct.additional4 = ConvertFunctions.TextToComma(Functions.GetField(dataLine, ",", 6));
                        acct.additional5 = ConvertFunctions.TextToComma(Functions.GetField(dataLine, ",", 7));
                    }
                    break;
                case EXTENSION_BUDGET:
                    if (acct.acctNumber == (Functions.GetField(dataLine, ",", 1) + " " + Functions.GetField(dataLine, ",", 2)))
                    {
                        acct.budgetChkBox = ConvertFunctions.StringToBool(ConvertFunctions.TextToComma(Functions.GetField(dataLine, ",", 3)));
                    }
                    break;
                default:
                    {
                        Functions.Verify(false, true, "Valid extension used");
                        break;
                    }
            }

            return acct;
        }


        public static GL_ACCT_CLASS _SA_GetAccountClass(string fullName)	// method that gets the account class enum values from full names
        {
            // Summary - Converts the full account class names to account class enum objects
            // Parameter "fullName" - The full name of account class
            // Return - The enum value of the account class
            switch (fullName.ToLower())
            {
                case "asset":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_ASST;
                case "cash":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_CASH;
                case "bank":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_BANK;
                case "credit card receivable":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_CCRE;
                case "cash equiva.Lengthts":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_CAEQ;
                case "marketable securities":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_MKSC;
                case "accounts receivable":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_AREC;
                case "other receivables":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_OREC;
                case "allowance for bad debts":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_AFBD;
                case "inventory":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_INVT;
                case "current asset":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_CURA;
                case "other current asset":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_OCRA;
                case "long term receivables":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_LTRC;
                case "other long term investments":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_OLTI;
                case "capital asset":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_CAPA;
                case "accum. amort. & depreciation":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_AADP;
                case "other non-current asset":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_ONCA;
                case "other asset":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_OTHA;
                case "liability":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_LIAB;
                case "credit card payable":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_CCPY;
                case "accounts payable":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_APAY;
                case "other payable":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_OPAY;
                case "sales tax payable":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_STPY;
                case "payroll tax payable":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_PTPY;
                case "employee deductions payable":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_EDPY;
                case "income tax payable":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_ITPY;
                case "short term debt":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_STDT;
                case "current liability":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_CURL;
                case "other current liability":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_OCRL;
                case "other non-Current liability":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_ONCL;
                case "debt":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_DEBT;
                case "deferred revenue":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_DREV;
                case "long term debt":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_LTDT;
                case "deferred income taxes":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_DINT;
                case "long term liability":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_LTLI;
                case "other liability":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_OTHL;
                case "equity":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_EQUI;
                case "owner/partner contributions":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_OPCN;
                case "owner/partner withdrawals":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_OPWD;
                case "share capital":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_SHCP;
                case "dividends":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_DIVI;
                case "retained earnings":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_RERN;
                case "current earnings":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_CERN;
                case "revenue":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_REVE;
                case "operating revenue":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_OPRE;
                case "non-operating revenue":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_NOPR;
                case "farming revenue":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_FREV;
                case "other revenue":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_OREV;
                case "gain":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_GAIN;
                case "extraordinary gain":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_EXGA;
                case "expense":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_EXPS;
                case "cost of goods sold":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_COGS;
                case "operating expense":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_OPEX;
                case "general & admin. expense":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_GAEX;
                case "amort./depreciation expense":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_ADEX;
                case "bad debt expense":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_BDEX;
                case "employee benefits":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_EMBE;
                case "payroll expense":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_PYRE;
                case "interest expense":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_INEX;
                case "non-operating expense":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_NOEX;
                case "loss":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_LOSS;
                case "extraordinary loss":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_EXLO;
                case "fixed asset":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_FIXA;
                case "income tax expense":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_ITXE;
                default:
                    {
                        return GL_ACCT_CLASS.GL_ACCT_CLASS_BLANK;
                        //Functions.VerifyFunction (false, true, "Text '" + fullName + "' is valid account class name used");
                        //break;
                    }
            }

        }

        public static string _SA_GetAccountClassName(GL_ACCT_CLASS acctClass)	// method that gets the full account class names from enum objects
        {
            // Summary - Converts the account class enum values to full names
            // Parameter "acctClassv" - The enum object of account class
            // return  - The full account class name
            switch (acctClass)
            {
                case GL_ACCT_CLASS.GL_ACCT_CLASS_ASST:
                    return "Asset";
                case GL_ACCT_CLASS.GL_ACCT_CLASS_CASH:
                    return "Cash";
                case GL_ACCT_CLASS.GL_ACCT_CLASS_BANK:
                    return "Bank";
                case GL_ACCT_CLASS.GL_ACCT_CLASS_CCRE:
                    return "Credit Card Receivable";
                case GL_ACCT_CLASS.GL_ACCT_CLASS_CAEQ:
                    return "Cash Equiva.Lengthts";
                case GL_ACCT_CLASS.GL_ACCT_CLASS_MKSC:
                    return "Marketable Securities";
                case GL_ACCT_CLASS.GL_ACCT_CLASS_AREC:
                    return "Accounts Receivable";
                case GL_ACCT_CLASS.GL_ACCT_CLASS_OREC:
                    return "Other Receivables";
                case GL_ACCT_CLASS.GL_ACCT_CLASS_AFBD:
                    return "Allowance for Bad Debts";
                case GL_ACCT_CLASS.GL_ACCT_CLASS_INVT:
                    return "Inventory";
                case GL_ACCT_CLASS.GL_ACCT_CLASS_CURA:
                    return "Current Asset";
                case GL_ACCT_CLASS.GL_ACCT_CLASS_OCRA:
                    return "Other Current Asset";
                case GL_ACCT_CLASS.GL_ACCT_CLASS_LTRC:
                    return "Long Term Receivables";
                case GL_ACCT_CLASS.GL_ACCT_CLASS_OLTI:
                    return "Other Long Term Investments";
                case GL_ACCT_CLASS.GL_ACCT_CLASS_CAPA:
                    return "Capital Asset";
                case GL_ACCT_CLASS.GL_ACCT_CLASS_AADP:
                    return "Accum. Amort. & Depreciation";
                case GL_ACCT_CLASS.GL_ACCT_CLASS_ONCA:
                    return "Other Non-Current Asset";
                case GL_ACCT_CLASS.GL_ACCT_CLASS_OTHA:
                    return "Other Asset";
                case GL_ACCT_CLASS.GL_ACCT_CLASS_LIAB:
                    return "Liability";
                case GL_ACCT_CLASS.GL_ACCT_CLASS_CCPY:
                    return "Credit Card Payable";
                case GL_ACCT_CLASS.GL_ACCT_CLASS_APAY:
                    return "Accounts Payable";
                case GL_ACCT_CLASS.GL_ACCT_CLASS_OPAY:
                    return "Other Payable";
                case GL_ACCT_CLASS.GL_ACCT_CLASS_STPY:
                    return "Sales Tax Payable";
                case GL_ACCT_CLASS.GL_ACCT_CLASS_PTPY:
                    return "Payroll Tax Payable";
                case GL_ACCT_CLASS.GL_ACCT_CLASS_EDPY:
                    return "Employee Deductions Payable";
                case GL_ACCT_CLASS.GL_ACCT_CLASS_ITPY:
                    return "Income Tax Payable";
                case GL_ACCT_CLASS.GL_ACCT_CLASS_STDT:
                    return "Short Term Debt";
                case GL_ACCT_CLASS.GL_ACCT_CLASS_CURL:
                    return "Current Liability";
                case GL_ACCT_CLASS.GL_ACCT_CLASS_OCRL:
                    return "Other Current Liability";
                case GL_ACCT_CLASS.GL_ACCT_CLASS_ONCL:
                    return "Other Non-Current Liability";
                case GL_ACCT_CLASS.GL_ACCT_CLASS_DEBT:
                    return "Debt";
                case GL_ACCT_CLASS.GL_ACCT_CLASS_DREV:
                    return "Deferred Revenue";
                case GL_ACCT_CLASS.GL_ACCT_CLASS_LTDT:
                    return "Long Term Debt";
                case GL_ACCT_CLASS.GL_ACCT_CLASS_DINT:
                    return "Deferred Income Taxes";
                case GL_ACCT_CLASS.GL_ACCT_CLASS_LTLI:
                    return "Long Term Liability";
                case GL_ACCT_CLASS.GL_ACCT_CLASS_OTHL:
                    return "Other Liability";
                case GL_ACCT_CLASS.GL_ACCT_CLASS_EQUI:
                    return "Equity";
                case GL_ACCT_CLASS.GL_ACCT_CLASS_OPCN:
                    return "Owner/Partner Contributions";
                case GL_ACCT_CLASS.GL_ACCT_CLASS_OPWD:
                    return "Owner/Partner Withdrawals";
                case GL_ACCT_CLASS.GL_ACCT_CLASS_SHCP:
                    return "Share Capital";
                case GL_ACCT_CLASS.GL_ACCT_CLASS_DIVI:
                    return "Dividends";
                case GL_ACCT_CLASS.GL_ACCT_CLASS_RERN:
                    return "Retained Earnings";
                case GL_ACCT_CLASS.GL_ACCT_CLASS_CERN:
                    return "Current Earnings";
                case GL_ACCT_CLASS.GL_ACCT_CLASS_REVE:
                    return "Revenue";
                case GL_ACCT_CLASS.GL_ACCT_CLASS_OPRE:
                    return "Operating Revenue";
                case GL_ACCT_CLASS.GL_ACCT_CLASS_NOPR:
                    return "Non-Operating Revenue";
                case GL_ACCT_CLASS.GL_ACCT_CLASS_FREV:
                    return "Farming Revenue";
                case GL_ACCT_CLASS.GL_ACCT_CLASS_OREV:
                    return "Other Revenue";
                case GL_ACCT_CLASS.GL_ACCT_CLASS_GAIN:
                    return "Gain";
                case GL_ACCT_CLASS.GL_ACCT_CLASS_EXGA:
                    return "Extraordinary Gain";
                case GL_ACCT_CLASS.GL_ACCT_CLASS_EXPS:
                    return "Expense";
                case GL_ACCT_CLASS.GL_ACCT_CLASS_COGS:
                    return "Cost of Goods Sold";
                case GL_ACCT_CLASS.GL_ACCT_CLASS_OPEX:
                    return "Operating Expense";
                case GL_ACCT_CLASS.GL_ACCT_CLASS_GAEX:
                    return "General & Admin. Expense";
                case GL_ACCT_CLASS.GL_ACCT_CLASS_ADEX:
                    return "Amort./Depreciation Expense";
                case GL_ACCT_CLASS.GL_ACCT_CLASS_BDEX:
                    return "Bad Debt Expense";
                case GL_ACCT_CLASS.GL_ACCT_CLASS_EMBE:
                    return "Employee Benefits";
                case GL_ACCT_CLASS.GL_ACCT_CLASS_PYRE:
                    return "Payroll Expense";
                case GL_ACCT_CLASS.GL_ACCT_CLASS_INEX:
                    return "Interest Expense";
                case GL_ACCT_CLASS.GL_ACCT_CLASS_NOEX:
                    return "Non-Operating Expense";
                case GL_ACCT_CLASS.GL_ACCT_CLASS_LOSS:
                    return "Loss";
                case GL_ACCT_CLASS.GL_ACCT_CLASS_EXLO:
                    return "Extraordinary Loss";
                case GL_ACCT_CLASS.GL_ACCT_CLASS_FIXA:
                    return "Fixed Asset";
                case GL_ACCT_CLASS.GL_ACCT_CLASS_ITXE:
                    return "Income Tax Expense";
                default:
                    {
                        return "Error";
                        //Functions.VerifyFunction (false, true, "valid account class enum used");
                        //break;
                    }
            }

        }

        private static GL_ACCT_CLASS DataFile_getAccountClassFromAbbrev(string abbrev)	// method that gets the account class enum object from abbrevation of account class name
        {
            // Summary - Converts the abbrevation of account class names to account class enum objects. Only used by external data files
            // Parameter "fullName" - The full name of account class
            // Return - The enum value of the account class
            switch (abbrev.ToUpper())
            {
                case "ASST":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_ASST;
                case "CASH":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_CASH;
                case "BANK":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_BANK;
                case "CCRE":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_CCRE;
                case "CAEQ":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_CAEQ;
                case "MKSC":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_MKSC;
                case "AREC":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_AREC;
                case "OREC":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_OREC;
                case "AFBD":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_AFBD;
                case "INVN":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_INVT;
                case "CURA":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_CURA;
                case "OCRA":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_OCRA;
                case "LTRC":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_LTRC;
                case "OLTI":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_OLTI;
                case "CAPA":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_CAPA;
                case "AADP":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_AADP;
                case "ONCA":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_ONCA;
                case "OTHA":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_OTHA;
                case "LIAB":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_LIAB;
                case "CCPY":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_CCPY;
                case "APAY":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_APAY;
                case "OPAY":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_OPAY;
                case "STPY":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_STPY;
                case "PTPY":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_PTPY;
                case "EDPY":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_EDPY;
                case "ITPY":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_ITPY;
                case "STDT":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_STDT;
                case "CURL":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_CURL;
                case "OCRL":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_OCRL;
                case "ONCL":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_ONCL;
                case "DEBT":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_DEBT;
                case "DREV":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_DREV;
                case "LTDT":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_LTDT;
                case "DINT":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_DINT;
                case "LTLI":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_LTLI;
                case "OTHL":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_OTHL;
                case "EQUI":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_EQUI;
                case "OPCN":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_OPCN;
                case "OPWD":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_OPWD;
                case "SHCP":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_SHCP;
                case "DIVI":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_DIVI;
                case "RERN":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_RERN;
                case "CERN":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_CERN;
                case "REVE":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_REVE;
                case "OPRE":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_OPRE;
                case "NOPR":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_NOPR;
                case "FREV":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_FREV;
                case "OREV":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_OREV;
                case "GAIN":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_GAIN;
                case "EXGA":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_EXGA;
                case "EXPS":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_EXPS;
                case "COGS":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_COGS;
                case "OPEX":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_OPEX;
                case "GAEX":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_GAEX;
                case "ADEX":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_ADEX;
                case "BDEX":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_BDEX;
                case "EMBE":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_EMBE;
                case "PYRE":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_PYRE;
                case "INEX":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_INEX;
                case "NOEX":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_NOEX;
                case "LOSS":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_LOSS;
                case "EXLO":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_EXLO;
                case "FIXA":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_FIXA;
                case "ITXE":
                    return GL_ACCT_CLASS.GL_ACCT_CLASS_ITXE;
                default:
                    {
                        Functions.Verify(false, true, "valid account class abbrevation used");
                        return GL_ACCT_CLASS.GL_ACCT_CLASS_ASST;
                    }
            }
        }

	}

    public static class GeneralIcon
    {
        public static GeneralLedgerResFolders.GeneralIconAppFolder repo = GeneralLedgerRes.Instance.GeneralIcon;
    }
    
    public static class EnterAccountName
    {
        public static GeneralLedgerResFolders.EnterAccountNameAppFolder repo = GeneralLedgerRes.Instance.EnterAccountName;
    }
    
}
