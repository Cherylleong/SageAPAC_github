/*
 * Created by Ranorex
 * User: wonda05
 * Date: 7/25/2016
 * Time: 9:52 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
// using System.Windows.Forms;
using System.Diagnostics;
using Sage50.Types;
using Sage50.Shared;
using Sage50.Classes;
using Sage50.Repository;
using Ranorex;
using System.Data;
using System.IO;

namespace Sage50.Classes
{
	/// <summary>
	/// Description of Settings.
	/// </summary>
	public class Settings
	{
		public static SettingsResFolders.SettingsAppFolder repo = SettingsRes.Instance.Settings;		
		
		private static bool bPro = false;	// default is false
		
		public static void _SA_Invoke()
		{
			Simply.repo.Self.Activate();
			Simply.repo.SetupMenu.Click();
			Simply.repo.SettingsMenuItem.Click();
			//System.Threading.Thread.Sleep(5000);						
		}
		

		// Get Infomation
		
		// Company Settings Methods
		public static void _SA_Get_AllCompanySettings()
        {
            _SA_Get_AllCompanySettings(true);
        }
        public static void _SA_Get_AllCompanySettings(bool bCloseWin)
        {        	            
            Settings._SA_GetCompanyInformation(false);
            Settings._SA_GetCompanySystemSettings(false);
            Settings._SA_GetCompanyBackupSettings(false);
            Settings._SA_GetCompanyFeatureSettings(false);
            Settings._SA_GetCompanyCreditCardSettings(false);
            Settings._SA_GetCompanyTaxSettings(false);
            Settings._SA_GetCompanyCurrencySettings(false);
            Settings._SA_GetCompanyFormsSettings(false);
            Settings._SA_GetCompanyEmailSettings(false);
            Settings._SA_GetCompanyDateSettings(false);
            Settings._SA_GetCompanyShipperSettings(false);
            Settings._SA_GetCompanyNameSettings(false);

            if (bCloseWin)
            {
                Settings.repo.Cancel.Click();
            }
        }
		
		public static void _SA_GetCompanyInformation()
        {
            _SA_GetCompanyInformation(true);
        }
        public static void _SA_GetCompanyInformation(bool bCloseWin)
        {
        	// tmp
        	// Simply.repo.Self.Activate();
        	// bool b = repo.SelfInfo.Exists(Variables.iExistWaitTime);	// Before reboot, return true even though the dialog is not open. After reboot, return false as expected.
        	// bool b1 = repo.Self.Visible; // Befor reboot, return false. After reboot, this throws exception
        	
        	CheckSettingsDialog();        	
        	
        	Variables.globalSettings.CompanyInformation = new COMPANY_INFO();
        	
        	Settings.repo.SettingsTree.CompInformation.Select();
        	Variables.globalSettings.CompanyInformation.companyName = Settings.repo.CompanyName.TextValue;
        	Variables.globalSettings.CompanyInformation.Address = new ADDRESS();
        	Variables.globalSettings.CompanyInformation.Address.street1 = Settings.repo.Address1.TextValue;
        	Variables.globalSettings.CompanyInformation.Address.street2 = Settings.repo.Address2.TextValue;
        	Variables.globalSettings.CompanyInformation.Address.city = Settings.repo.City.TextValue;
        	Variables.globalSettings.CompanyInformation.Address.provinceCode = Settings.repo.ProvinceCode.SelectedItemText;
        	Variables.globalSettings.CompanyInformation.Address.province = Settings.repo.Province.TextValue;
        	Variables.globalSettings.CompanyInformation.Address.postalCode = Settings.repo.PostalCode.TextValue;
        	Variables.globalSettings.CompanyInformation.Address.country = Settings.repo.Country.TextValue;
        	Variables.globalSettings.CompanyInformation.Address.phone1 = Settings.repo.Phone1.TextValue;
        	Variables.globalSettings.CompanyInformation.Address.phone2 = Settings.repo.Phone2.TextValue;
        	Variables.globalSettings.CompanyInformation.Address.fax = Settings.repo.Fax.TextValue;
        	Variables.globalSettings.CompanyInformation.businessNum = Settings.repo.BusinessNumber.TextValue;
        	Variables.globalSettings.CompanyInformation.companyType = Settings.repo.BusinessType.SelectedItemText;
        				
        	Variables.globalSettings.CompanyInformation.fiscalStart = Settings.repo.FiscalStart.TextValue;
        	Variables.globalSettings.CompanyInformation.fiscalEnd = Settings.repo.FiscalEnd.TextValue;
        	Variables.globalSettings.CompanyInformation.earliestTransaction = Settings.repo.EarliestTransaction.TextValue;
        	Variables.globalSettings.CompanyInformation.sessionDate = Settings.repo.SessionDate.TextValue;
        	Variables.globalSettings.CompanyInformation.latestTransaction = Settings.repo.LatestTransaction.TextValue;        	
        	
        	if (Functions.GoodData(Variables.globalSettings.CompanySettings.LogoLocation))
        	{
        		Settings.repo.SettingsTree.CompanyLogo.Select();
				Variables.globalSettings.CompanySettings.LogoLocation = Settings.repo.CompanyLogoLoc.TextValue;
        	}

        	if (bCloseWin)
        	{
        		Settings.repo.Cancel.Click();
        		// SelfInfo.Exists() not working properly..
        		// while (Settings.repo.SelfInfo.Exists()){}
        	}
        	
        }
        
        public static void _SA_GetCompanySystemSettings()
        {
            _SA_GetCompanySystemSettings(true);
        }
        public static void _SA_GetCompanySystemSettings(bool bCloseWin)
        {
        	CheckSettingsDialog();        	
        	
        	Variables.globalSettings.CompanySettings.SystemSettings = new SYSTEM();
        	
        	Settings.repo.SettingsTree.CompanySystem.Select();
        	
        	Variables.globalSettings.CompanySettings.SystemSettings.useCashBasisAccounting = Settings.repo.UseCashBasisAccounting.Checked;
        	        	
        	if (Variables.globalSettings.CompanySettings.SystemSettings.useCashBasisAccounting == true)
        	{
        		Variables.globalSettings.CompanySettings.SystemSettings.cashBasisDate = Settings.repo.CashAccountingDate.TextValue;
        	}
        	Variables.globalSettings.CompanySettings.SystemSettings.useChequeNo = Settings.repo.UseChequeNoAsTheSourceCode.Checked;
        	Variables.globalSettings.CompanySettings.SystemSettings.doNotAllowTransactionsDatedBefore = Settings.repo.DoNotAllowTransactionsDatedBefore.Checked;
        	
        	if (Variables.globalSettings.CompanySettings.SystemSettings.doNotAllowTransactionsDatedBefore == true)
        	{
        		Variables.globalSettings.CompanySettings.SystemSettings.lockingDate = Settings.repo.LockingDate.TextValue;
        	}
			Variables.globalSettings.CompanySettings.SystemSettings.allowTransactionsInTheFuture = Settings.repo.AllowFutureTransactions.Checked;
			Variables.globalSettings.CompanySettings.SystemSettings.warnIfTransactionsAre = Settings.repo.WarnIfTransactionsAreInTheFuture.Checked;
			if (Variables.globalSettings.CompanySettings.SystemSettings.warnIfTransactionsAre == true)
			{
				Variables.globalSettings.CompanySettings.SystemSettings.daysInTheFuture = Settings.repo.DaysInTheFuture.TextValue;
			}
			Variables.globalSettings.CompanySettings.SystemSettings.warnIfAccountsAreNotBalanced = Settings.repo.WarnIfAccountsAreNotBalanced.Checked;
									
			if (bCloseWin)
			{
				Settings.repo.Cancel.Click();
			}

        }
        
        public static void _SA_GetCompanyBackupSettings()
        {
            _SA_GetCompanyBackupSettings(true);
        }
        public static void _SA_GetCompanyBackupSettings(bool bCloseWin)
        {
        	CheckSettingsDialog();
        	
        	Settings.repo.SettingsTree.CompanyBackup.Select();
        	  
        	Variables.globalSettings.CompanySettings.BackupSettings.displayReminderOnSession =  Settings.repo.DisplayBackupReminderWhenSessionDateChange.Checked;
        	if (Variables.globalSettings.CompanySettings.BackupSettings.displayReminderOnSession == true)
        	{
        		Variables.globalSettings.CompanySettings.BackupSettings.reminderFrequency = Settings.repo.BackupReminderFrequency.SelectedItemText;
        	}
        	Variables.globalSettings.CompanySettings.BackupSettings.displayReminderWhenClosing = Settings.repo.DisplayABackupReminderAtClosing.Checked;
        	        	
        	if (bCloseWin)
        	{
        		Settings.repo.Cancel.Click();
        	}
        	
        }
        
        public static void _SA_GetCompanyFeatureSettings()
        {
            _SA_GetCompanyFeatureSettings(true);
        }
        public static void _SA_GetCompanyFeatureSettings(bool bCloseWin)
        {   
        	CheckSettingsDialog();        	
        	
        	Settings.repo.SettingsTree.CompanyFeatures.Select();
        	Variables.globalSettings.CompanySettings.FeatureSettings.ordersForVendors = Settings.repo.OrdersForVendors.Checked;
        	Variables.globalSettings.CompanySettings.FeatureSettings.quotesForVendors = Settings.repo.QuotesForVendors.Checked;
        	Variables.globalSettings.CompanySettings.FeatureSettings.ordersForCustomers = Settings.repo.OrdersForCustomers.Checked;
        	Variables.globalSettings.CompanySettings.FeatureSettings.quotesForCustomers = Settings.repo.QuotesForCustomers.Checked;
        	Variables.globalSettings.CompanySettings.FeatureSettings.packingSlips = Settings.repo.PackingSlipsForCustomers.Checked;
        	Variables.globalSettings.CompanySettings.FeatureSettings.projectCheckBox = Settings.repo.Project.Checked;
        	Variables.globalSettings.CompanySettings.FeatureSettings.Language = Settings.repo.DoesBusinessInBillingual.Checked;
        	        	
        	if (bCloseWin)
        	{
        		Settings.repo.Cancel.Click();
        	}
        }
        
        public static void _SA_GetCompanyCreditCardSettings()
        {
            _SA_GetCompanyCreditCardSettings(true);
        }
        public static void _SA_GetCompanyCreditCardSettings(bool bCloseWin)
		{
        	CheckSettingsDialog();
        	
        	int USED_NAME_COLUMN = 0;
        	int USED_PAYABLE_COLUMN = 1;
        	int USED_EXPENSE_COLUMN = 2;
        	
        	Settings.repo.SettingsTree.CompanyCreditCardUsed.Select();
        	
        	// Get credit cards used
        	List <CREDIT_CARD_USED> lCreditCardUsed = new List<CREDIT_CARD_USED>() {};
        	
        	foreach (List <string> creditCardLine in Settings.repo.CCUsedContainer.GetContents())
        	{
        		CREDIT_CARD_USED CCardUsed = new CREDIT_CARD_USED();
        		
        		if (creditCardLine[USED_NAME_COLUMN] != "")
        		{
        			CCardUsed.CardName = ConvertFunctions.BlankStringToNULL(creditCardLine[USED_NAME_COLUMN]);
        			CCardUsed.PayableAccount.acctNumber = ConvertFunctions.BlankStringToNULL(creditCardLine[USED_PAYABLE_COLUMN]);
        			CCardUsed.ExpenseAccount.acctNumber = ConvertFunctions.BlankStringToNULL(creditCardLine[USED_EXPENSE_COLUMN]);
        			
        			lCreditCardUsed.Add(CCardUsed);
        		}
        		else
        		{
        			// exit loop once a blank line is found
        			break;
        		}
        	}
        	Variables.globalSettings.CompanySettings.CreditCardSettings.CardsUsed = lCreditCardUsed;
			
        	
			// Get credit cards accepted
			Settings.repo.SettingsTree.CompanyCreditCardAccepted.Select();
			
			int ACCEPT_NAME_COLUMN = 0;
			int ACCEPT_DISCOUNT_COLUMN = 1;
			int ACCEPT_EXPENSE_COLUMN = 2;
			int ACCEPT_ASSET_COLUMN = 3;
			
			List <CREDIT_CARD_ACCEPTED> lCreditCardAccept = new List<CREDIT_CARD_ACCEPTED>() {};
			
			foreach (List <string> creditCardLine in Settings.repo.CCAcceptedContainer.GetContents())
			{
				CREDIT_CARD_ACCEPTED CCardAccepted = new CREDIT_CARD_ACCEPTED();
				
				if (creditCardLine[ACCEPT_NAME_COLUMN] != "")
				{
					CCardAccepted.CardName = ConvertFunctions.BlankStringToNULL(creditCardLine[ACCEPT_NAME_COLUMN]);
					CCardAccepted.Discount = ConvertFunctions.BlankStringToNULL(creditCardLine[ACCEPT_DISCOUNT_COLUMN]);
					CCardAccepted.ExpenseAccount.acctNumber = ConvertFunctions.BlankStringToNULL(creditCardLine[ACCEPT_EXPENSE_COLUMN]);
					CCardAccepted.AssetAccount.acctNumber = ConvertFunctions.BlankStringToNULL(creditCardLine[ACCEPT_ASSET_COLUMN]);
					
					lCreditCardAccept.Add(CCardAccepted);
				}
				else
				{
					// exit loop once a blank line is found
					break;
				}
			}
			
			Variables.globalSettings.CompanySettings.CreditCardSettings.CardsAccepted = lCreditCardAccept;
			
			if (bCloseWin)
			{
				Settings.repo.Cancel.Click();
			}		
		}
        
        public static void _SA_GetCompanyTaxSettings()
        {
            _SA_GetCompanyTaxSettings(true);
        }
        public static void _SA_GetCompanyTaxSettings(bool bCloseWin)
        {   
        	CheckSettingsDialog();
        	
        	Variables.globalSettings.CompanySettings.TaxSettings = Settings._SA_ReadAllTaxes();
        	Variables.globalSettings.CompanySettings.TaxCodes = Settings._SA_ReadAllTaxCodes();
			
			if (bCloseWin)
			{
				Settings.repo.Cancel.Click();
			}
        }
        
        // Tax Methods
        public static List<TAX> _SA_ReadAllTaxes()
        {
        	List <TAX> LT = new List<TAX>();
        	
        	CheckTaxesDialog();
        	
        	List <List <string>> lsTaxContents = Settings.repo.SalesTaxContainer.GetContents();
        	
        	foreach (List <string> lsRow in lsTaxContents)
        	{
        		TAX T = new TAX();
        		
        		// handle blank lines
        		if (lsRow[0] != "")
        		{
        			T.taxName = lsRow[0];
        			T.taxID = lsRow[1];
        			if (lsRow[2] == "No")
        			{
        				T.exempt = false;
        			}
        			else
        			{
        				T.exempt = true;
        			}
        			if (lsRow[3] == "No")
        			{
        				T.taxable = false;
        			}
        			else
        			{
        				T.taxable = true;
        			}
        			T.acctTrackPurchases.acctNumber = lsRow[4];
        			T.acctTrackSales.acctNumber = lsRow[5];
        			if (lsRow[6] == "No")
        			{
        				T.reportOnTaxes = false;
        			}
        			else
        			{
        				T.reportOnTaxes = true;	
        			}
        			LT.Add(T);
        		}
        	}
			
			// Taxable tax list
			Settings.repo.SalesTaxTable.SetToLine(0);
//			Settings.repo.SalesTaxTable.MoveRight(3);
			
			for (int x = 0; x < lsTaxContents.Count; x++)
			{
				// Open the tax list if there is more than one tax
				if (lsTaxContents.Count > 1)
				{
					Settings.repo.SalesTaxTable.PressKeys("{Enter}");
					
					List <List <string>> lsTTContents = TaxableTaxList.repo.TaxableTaxListContainer.GetContents();
					
					foreach (List <string> lsRow in lsTTContents)
					{
						if (lsRow[1] == "true")
						{
							LT[x].TaxAuthoritiesToBeCharged.Add(lsRow[0]);
						}
					}
					TaxableTaxList.repo.Cancel.Click();
				}
			}
			return LT;
        }
        
         public static List<TAX_CODE> _SA_ReadAllTaxCodes()
        {
         	
         	// Works only if the default value No Tax is on the first line. Not the case in sample database.
         	List <TAX_CODE> LTC = new List<TAX_CODE>();
         	string useIn;
         	
         	CheckTaxCodesDialog();
         	
         	List <List <string>> lsTaxcodeContents = Settings.repo.SalesTaxCodeContainer.GetContents();
         	
         	foreach (List <string> lsRow in lsTaxcodeContents)
         	{
         		TAX_CODE TC = new TAX_CODE();
         		
         		// Handle blank lines
         		if (lsRow[0] != "" || lsRow[1] != "")
         		{
         			TC.code = lsRow[0];
         			TC.description = lsRow[1];
         			
         			// If the company use both English and French, there will be an extra column
         			if (lsRow[2] != "")
         			{
         				useIn = lsRow[2];
         			}
         			else
         			{
         				useIn = lsRow[3];
         			}
         			
         			switch (useIn)
         			{
         				case "All journals":
         					TC.useIn = TAX_USED_IN.TAX_ALL_JOURNALS;
         					break;
         				case "Sales":
         					TC.useIn = TAX_USED_IN.TAX_SALES;
         					break;
         				case "Purchases":
         					TC.useIn = TAX_USED_IN.TAX_PURCHASES;
         					break;
         				default:		
         				{
         					Functions.Verify(false, true, "Valid value from Use In field");
         					break;
         				}
         			}
         			LTC.Add(TC);
         		}
         	}			
			
          	// Get tax code details
          	Settings.repo.SalesTaxCodeContainer.ClickFirstCell();
          	
          	for (int x = 0; x < lsTaxcodeContents.Count; x++)
          	{
          		Settings.repo.SalesTaxCodeTable.SetText("{Enter}");
          		
          		if (TaxCodeDetails.repo.TaxCodeDetailsContainerInfo.Exists(Variables.iExistWaitTime))
          		{
          			List <List<string>> lsTCDContents = TaxCodeDetails.repo.TaxCodeDetailsContainer.GetContents();
          			
          			foreach (List <string> lsRow in lsTCDContents)
          			{
          				if (lsRow[0] != "")
          				{
          					TAX_DETAIL TD = new TAX_DETAIL();
          					
          					TD.Tax.taxName = lsRow[0];
          					
          					switch (lsRow[1])
          					{
          						case "Taxable":
          							TD.taxStatus = TAX_STATUS.TAX_STATUS_TAXABLE;
          							break;
          						case "Non-taxable":
          							TD.taxStatus = TAX_STATUS.TAX_STATUS_NON_TAXABLE;
          							break;
          						case "Exempt":
          							TD.taxStatus = TAX_STATUS.TAX_STATUS_EXEMPT;
          							break;
          						default:
  								{
      								Functions.Verify(false, true, "Valid value received for Tax status field");
      								break;
      							}
          					}
          					
          					TD.rate = lsRow[2];
          					
          					if (lsRow[3] == "No")
          					{
          						TD.includedInPrice = false;
          					}
          					else if (lsRow[3] == "Yes")
          					{
          						TD.includedInPrice = true;
          					}
          					else
          					{
          						TD.includedInPrice = null;
          					}
          					
          					if (lsRow[4] == "No")
          					{
          						TD.isRefundable = false;
          					}
          					else if (lsRow[4] == "Yes")
          					{
          						TD.isRefundable = true;
          					}
          					else
          					{
          						TD.isRefundable = null;
          					}
          					
          					LTC[x].TaxDetails.Add(TD);
          				}
          			}
          			
          			TaxCodeDetails.repo.Cancel.Click();
          		}
          		
          		Settings.repo.SalesTaxCodeContainer.MoveDown();
          	}
          	
          	return LTC;	
        }
        
        // Tax Methods 
        public static void _SA_TaxesInvoke()
        {
        	CheckSettingsDialog();
        	
         	Settings.repo.SettingsTree.CompanySalesTaxesTaxes.Select();
        }
         
        public static void _SA_TaxCodesInvoke()
        {
        	CheckSettingsDialog();
        	
        	Settings.repo.SettingsTree.CompanySalesTaxesTaxCodes.Select();
        }

        public static void _SA_GetCompanyCurrencySettings()
        {
            _SA_GetCompanyCurrencySettings(true);
        }
        public static void _SA_GetCompanyCurrencySettings(bool bClickOK)
        {	
        	CheckSettingsDialog();
        	    
        	Settings.repo.SettingsTree.CompanyCurrency.Select();
        	// Home currency
			Variables.globalSettings.CompanySettings.CurrencySettings.allowForeignCurrency = Settings.repo.AllowForeignCurrency.Checked;
			Variables.globalSettings.CompanySettings.CurrencySettings.HomeCurrency.Currency = Settings.repo.HomeCurrency.Text;
			Variables.globalSettings.CompanySettings.CurrencySettings.HomeCurrency.currencyCode = Settings.repo.CurrencyCode.TextValue;
			Variables.globalSettings.CompanySettings.CurrencySettings.HomeCurrency.decimalPlaces = (CURRENCY_DECIMAL)Settings.repo.DecimalPlaces.SelectedItemIndex;
			Variables.globalSettings.CompanySettings.CurrencySettings.HomeCurrency.denomination = Settings.repo.Denomination.SelectedItemText;
			Variables.globalSettings.CompanySettings.CurrencySettings.HomeCurrency.thousandsSeparator = Settings.repo.ThousandsSeparator.TextValue;
			Variables.globalSettings.CompanySettings.CurrencySettings.HomeCurrency.symbol = Settings.repo.Symbol.TextValue;
			Variables.globalSettings.CompanySettings.CurrencySettings.HomeCurrency.decimalSeparator = Settings.repo.DecimalSeparator.TextValue;
			Variables.globalSettings.CompanySettings.CurrencySettings.HomeCurrency.symbolPosition = (CURRENCY_SYMBOL)Settings.repo.SymbolPosition.SelectedItemIndex;
			
			if (Variables.globalSettings.CompanySettings.CurrencySettings.allowForeignCurrency == true)
			{
				Variables.globalSettings.CompanySettings.CurrencySettings.HomeCurrency.roundingDifferencesAccount = Settings.repo.DifferencesAccount.SelectedItemText;
				
				List<CURRENCY_DATA> lFC = new List<CURRENCY_DATA>() {};
				
				// Premium or Quantum
				if (Settings.repo.CurrencyContainerInfo.Exists(Variables.iExistWaitTime))
				{
																			
					List<List <string>> lsCurContents = Settings.repo.CurrencyContainer.GetContents();
					
					foreach (List <string> lsRow in lsCurContents)
					{
						if (lsRow[0] != "")
						{
							CURRENCY_DATA CD = new CURRENCY_DATA();
							CD.Currency = lsRow[0];
							CD.currencyCode = lsRow[1];
							CD.merchantAccount = lsRow[2];
							CD.symbol = lsRow[3];
							if (lsRow[4] == "Leading")
							{
								CD.symbolPosition = CURRENCY_SYMBOL.CURRENCY_LEADING;
							}
							else
							{
								CD.symbolPosition = CURRENCY_SYMBOL.CURRENCY_TRAILING;
							}
							CD.thousandsSeparator = lsRow[5];
							CD.decimalSeparator = lsRow[6];
							switch (lsRow[7])
							{
								case "0":
									CD.decimalPlaces = CURRENCY_DECIMAL.CURRENCY_DECIMAL_0;
									break;
								case "1":
									CD.decimalPlaces = CURRENCY_DECIMAL.CURRENCY_DECIMAL_1;
									break;
								case "2":
									CD.decimalPlaces = CURRENCY_DECIMAL.CURRENCY_DECIMAL_2;
									break;
							}
							lFC.Add(CD);
						}
					}
				}
				// Pro
				else
				{
					bPro = true;
					
					CURRENCY_DATA CDPro = new CURRENCY_DATA() {};
					CDPro.Currency = repo.SingleForeignCurrency.SelectedItemText;
					CDPro.currencyCode =  repo.SingleCurrencyCode.TextValue;										
					CDPro.symbol = repo.SingleSymbol.TextValue;
					if (repo.SingleSymbolPosition.SelectedItemText == "Leading")
					{
						CDPro.symbolPosition = CURRENCY_SYMBOL.CURRENCY_LEADING;
					}
					else
					{
						CDPro.symbolPosition = CURRENCY_SYMBOL.CURRENCY_TRAILING;
					}
					CDPro.thousandsSeparator = repo.SingleThousandsSeparator.TextValue;
					CDPro.decimalSeparator = repo.SingleDecimalSeparator.TextValue;
					switch (repo.SingleDecimalPlaces.SelectedItemIndex)
					{
						case 0:
							CDPro.decimalPlaces = CURRENCY_DECIMAL.CURRENCY_DECIMAL_0;
							break;
						case 1:
							CDPro.decimalPlaces = CURRENCY_DECIMAL.CURRENCY_DECIMAL_1;
							break;
						case 2:
							CDPro.decimalPlaces = CURRENCY_DECIMAL.CURRENCY_DECIMAL_2;
							break;
					}
					lFC.Add(CDPro);
				}
				
				// Exchange rate dialog
				Settings.repo.ExchangeRateBtn.Click();
				
				foreach (CURRENCY_DATA CD in lFC)
				{
					if (!bPro)
					{
						ExchangeRate.repo.Currency.Select(CD.Currency);	
					}					
					CD.exchangeDisplayReminder = ExchangeRate.repo.DisplayReminder.Checked;
					CD.exchangeRateReminder = (CURRENCY_RATE_REMINDER)ExchangeRate.repo.CurrencyRateReminder.SelectedItemIndex;
					CD.exchangeWebsite = ExchangeRate.repo.CurrencyWebsite.TextValue;
					
					List<CURRENCY_EXCHANGE> lCE = new List<CURRENCY_EXCHANGE>() {};
					List <List<string>> lsExContents = ExchangeRate.repo.ExchangeRateContainer.GetContents();
					
					foreach (List <string> lsRow in lsExContents)
					{
						CURRENCY_EXCHANGE CE = new CURRENCY_EXCHANGE();
						CE.exchangeDate = lsRow[0];
						CE.exchangeRate = lsRow[1];
						lCE.Add(CE);
					}
					CD.ExchangeRates = lCE;
				}
				
				ExchangeRate.repo.Cancel.Click();
				
				Variables.globalSettings.CompanySettings.CurrencySettings.ForeignCurrencies = lFC;							
			}								
			
			if (bClickOK)
			{
				Settings.repo.OK.Click();
			}		
        }
        
        public static void _SA_GetCompanyFormsSettings()
        {
        	_SA_GetCompanyFormsSettings(true);
        }
        public static void _SA_GetCompanyFormsSettings(bool BCloseWin)
        {
        	CheckSettingsDialog();
        	
        	Settings.repo.SettingsTree.CompanyForms.Select();
        	// Next form number column
        	Variables.globalSettings.CompanySettings.FormSettings.nextNumInvoice = Settings.repo.NextNumInvoices.TextValue;
        	Variables.globalSettings.CompanySettings.FormSettings.nextNumSalesQuote = Settings.repo.NextNumSalesQuotes.TextValue;
        	Variables.globalSettings.CompanySettings.FormSettings.nextNumReceipt = Settings.repo.NextNumReceipts.TextValue;
        	Variables.globalSettings.CompanySettings.FormSettings.nextNumCustomerDeposit = Settings.repo.NextNumCustomerDeposits.TextValue;
        	Variables.globalSettings.CompanySettings.FormSettings.nextNumPurchaseOrder = Settings.repo.NextNumPurchaseOrders.TextValue;			        	
        	if (repo.TimeSlipElementInfo.Exists(Variables.iExistWaitTime))
        	{
        		Variables.globalSettings.CompanySettings.FormSettings.nextNumTimeSlip = Settings.repo.NextNumTimeSlips.TextValue;
        	}
        	Variables.globalSettings.CompanySettings.FormSettings.nextNumDirectDeposit = Settings.repo.NextNumDDEmployee.TextValue;
        	
        	// Verify number sequence colum
        	Variables.globalSettings.CompanySettings.FormSettings.verifyNumSeqInvoice = Settings.repo.VerifyInvoices.Checked;
        	Variables.globalSettings.CompanySettings.FormSettings.verifyNumSeqSalesQuote = Settings.repo.VerifySalesQuotes.Checked;
        	Variables.globalSettings.CompanySettings.FormSettings.verifyNumSeqReceipt = Settings.repo.VerifyReceipts.Checked;
        	Variables.globalSettings.CompanySettings.FormSettings.verifyNumSeqCustomerDeposit = Settings.repo.VerifyCustomerDeposits.Checked;
        	Variables.globalSettings.CompanySettings.FormSettings.verifyNumSeqPurchaseOrder = Settings.repo.VerifyPurchaseOrders.Checked;
        	Variables.globalSettings.CompanySettings.FormSettings.verifyNumSeqCheque = Settings.repo.VerifyCheques.Checked;
        	if (repo.TimeSlipElementInfo.Exists(Variables.iExistWaitTime))
        	{
        		Variables.globalSettings.CompanySettings.FormSettings.verifyNumSeqTimeSlips = Settings.repo.VerifyTimeSlips.Checked;
        	}
        	Variables.globalSettings.CompanySettings.FormSettings.verifyNumSeqDepositSlips = Settings.repo.VerifyDepositSlips.Checked;
        	
        	// Confirm printing email column
        	Variables.globalSettings.CompanySettings.FormSettings.confirmPrintEmailInvoice = Settings.repo.ConfirmPrintEmailInvoice.Checked;
			Variables.globalSettings.CompanySettings.FormSettings.confirmPrintEmailSalesQuote = Settings.repo.ConfirmPrintEmailSalesQuotes.Checked;
			Variables.globalSettings.CompanySettings.FormSettings.confirmPrintEmailSalesOrder = Settings.repo.ConfirmPrintEmailSalesOrders.Checked;
			Variables.globalSettings.CompanySettings.FormSettings.confirmPrintEmailReceipt = Settings.repo.ConfirmPrintEmailReceipts.Checked;
			Variables.globalSettings.CompanySettings.FormSettings.confirmPrintEmailPurchaseOrder = Settings.repo.ConfirmPrintEmailPurchaseOrders.Checked;
			Variables.globalSettings.CompanySettings.FormSettings.confirmPrintEmailChequesOrder = Settings.repo.ConfirmPrintEmailCheques.Checked;
			if (repo.TimeSlipElementInfo.Exists(Variables.iExistWaitTime))
			{
				Variables.globalSettings.CompanySettings.FormSettings.confirmPrintEmailTimeSlips = Settings.repo.ConfirmPrintEmailTimeSlips.Checked;
			}
			Variables.globalSettings.CompanySettings.FormSettings.confirmPrintEmailDepositSlips = Settings.repo.ConfirmPrintEmailDepositSlips.Checked;
			
			// Print company address on column
			Variables.globalSettings.CompanySettings.FormSettings.printCompAddressInvoice = Settings.repo.PrintCompanyAddressInvoices.Checked;
			Variables.globalSettings.CompanySettings.FormSettings.printCompAddressSalesQuotes = Settings.repo.PrintCompanyAddressSalesQuotes.Checked;
			Variables.globalSettings.CompanySettings.FormSettings.printCompAddressSalesOrders = Settings.repo.PrintCompanyAddressSalesOrders.Checked;
			Variables.globalSettings.CompanySettings.FormSettings.printCompAddressReceipt = Settings.repo.PrintCompanyAddressReceipts.Checked;
			Variables.globalSettings.CompanySettings.FormSettings.printCompAddressStatements = Settings.repo.PrintCompanyAddressStatements.Checked;
			Variables.globalSettings.CompanySettings.FormSettings.printCompAddressPurchaseOrders = Settings.repo.PrintCompanyAddressPurchaseOrders.Checked;
			Variables.globalSettings.CompanySettings.FormSettings.printCompAddressCheque = Settings.repo.PrintCompanyAddressCheques.Checked;
			
			// Print in batches column
			Variables.globalSettings.CompanySettings.FormSettings.printInBatchesInvoice = Settings.repo.PrintInBatchesInvoices.Checked;
			Variables.globalSettings.CompanySettings.FormSettings.printInBatchesPackingSlip = Settings.repo.PrintInBatchesPackingSlips.Checked;
			Variables.globalSettings.CompanySettings.FormSettings.printInBatchesSalesQuotes = Settings.repo.PrintInBatchesSalesQuotes.Checked;
			Variables.globalSettings.CompanySettings.FormSettings.printInBatchesSalesOrders = Settings.repo.PrintInBatchesSalesOrders.Checked;
			Variables.globalSettings.CompanySettings.FormSettings.printInBatchesReceipt = Settings.repo.PrintInBatchesReceipts.Checked;
			Variables.globalSettings.CompanySettings.FormSettings.printInBatchesPurchaseOrders = Settings.repo.PrintInBatchesPurchaseOrders.Checked;
			Variables.globalSettings.CompanySettings.FormSettings.printInBatchesCheque = Settings.repo.PrintInBatchesCheques.Checked;
			Variables.globalSettings.CompanySettings.FormSettings.printInBatchesDepositSlip = false;
			
			// Check for duplicates column
			Variables.globalSettings.CompanySettings.FormSettings.checkForDupInvoices = Settings.repo.CheckForDupInvoices.Checked;
			Variables.globalSettings.CompanySettings.FormSettings.checkForDupReceipts = Settings.repo.CheckForDupReceipts.Checked;
						
			if (BCloseWin)
			{
				repo.Cancel.Click();
			}			
        }
        
        public static void _SA_GetCompanyEmailSettings()
        {
        	_SA_GetCompanyEmailSettings(true);
        }
        
        public static void _SA_GetCompanyEmailSettings(bool bCloseWin)
        {
        	CheckSettingsDialog();
        	
        	Settings.repo.SettingsTree.CompanyEmail.Select();
        	
        	List <EMAIL_MESSAGES> lEMS = new List<EMAIL_MESSAGES>() {};
        	
        	for (int x=0; x < Settings.repo.EmailForms.Items.Count; x++)
        	{
        		EMAIL_MESSAGES EMS = new EMAIL_MESSAGES();
        		Settings.repo.EmailForms.Items[x].Select();
        		switch (Settings.repo.EmailForms.SelectedItemText)
        		{
        			case "Invoice":
        				EMS.Form = EMAIL_FORM_TYPE.FORM_INVOICES;
        				break;
        			case "Purchase Orders":
        				EMS.Form = EMAIL_FORM_TYPE.FORM_PURCHASE_ORDERS;
        				break;
        			case "Sales Orders":
        				EMS.Form = EMAIL_FORM_TYPE.FORM_SALES_ORDERS;
        				break;
        			case "Sales Quotes":
        				EMS.Form = EMAIL_FORM_TYPE.FORM_SALES_QUOTES;
        				break;
        			case "Receipts":
        				EMS.Form = EMAIL_FORM_TYPE.FORM_RECEIPTS;
        				break;
        			case "Statements":
        				EMS.Form = EMAIL_FORM_TYPE.FORM_STATEMENTS;
        				break;
        			case "Purchase Quote Confirmations":
        				EMS.Form = EMAIL_FORM_TYPE.FORM_PURCHASE_QUOTE_CONFIRMATIONS;
        				break;
        			case "Purchase Invoice Confirmations":
        				EMS.Form = EMAIL_FORM_TYPE.FORM_PURCHASE_INVOICE_CONFIRMATIONS;
        				break;
        			case "Direct Deposit Payment Stub":
        				EMS.Form = EMAIL_FORM_TYPE.FORM_DIRECT_DEPOSIT_PAYMENT_STUB;
        				break;
        			case "Direct Deposit Payroll Stub":
        				EMS.Form = EMAIL_FORM_TYPE.FORM_DIRECT_DEPOSIT_PAYROLL_STUB;
        				break;
        			default:
    				{
    					Functions.Verify(false, true, "Email form value known");
    					break;        					
    				}        				        					        				
        		}
        		if (Variables.isMultiUser)
        		{
        			EMS.Message = Settings.repo.SettingsText.EmailMessageMult.TextValue;        			
        		}
        		else
        		{
        			EMS.Message = Settings.repo.EmailMessage.TextValue;
        		}
        		lEMS.Add(EMS);
        	}
        	Variables.globalSettings.CompanySettings.EmailSettings.Messages = lEMS;
        	
        	// Feature has been removed?
        	// Variables.GlobalSettings.CompanySettings.EmailSettings.AttachmentFormat
        	
        	if (bCloseWin)
        	{
        		Settings.repo.Cancel.Click();
        	}
        }
        
        public static void _SA_GetCompanyDateSettings()
        {
        	_SA_GetCompanyDateSettings(true);
        }
        
        public static void _SA_GetCompanyDateSettings(bool bCloseWin)
        {
        	CheckSettingsDialog();
        	
        	Settings.repo.SettingsTree.CompanyDateFormat.Select();
        	
        	if (Settings.repo.OnScreenUseShort.Checked)
        	{
        		Variables.globalSettings.CompanySettings.DateSettings.onScreenUse = DATE_SHORT_LONG.DATE_SHORT;
        	}
        	else
        	{
        		Variables.globalSettings.CompanySettings.DateSettings.onScreenUse = DATE_SHORT_LONG.DATE_LONG;
        	}
        	
        	if (Settings.repo.InReportsUseShort.Checked)
        	{
        		Variables.globalSettings.CompanySettings.DateSettings.inReportsUse = DATE_SHORT_LONG.DATE_SHORT;
        	}
        	else
        	{
        		Variables.globalSettings.CompanySettings.DateSettings.inReportsUse = DATE_SHORT_LONG.DATE_LONG;
        	}
        	
        	Variables.globalSettings.CompanySettings.DateSettings.WeekBeginsOn = (DATE_WEEK)Settings.repo.WeekBegins.SelectedItemIndex;
        	
        	if (bCloseWin)
        	{
        		Settings.repo.Cancel.Click();
        	}
        	
        }
        
        public static void _SA_GetCompanyShipperSettings()
        {
        	_SA_GetCompanyShipperSettings(true);
        }
        
        public static void _SA_GetCompanyShipperSettings(bool bCloseWin)
        {
        	CheckSettingsDialog();
        	
        	Settings.repo.SettingsTree.CompanyShippers.Select();
        	Variables.globalSettings.CompanySettings.ShipperSettings.TrackShipments = Settings.repo.TrackShipments.Checked;
        	
        	if (Variables.globalSettings.CompanySettings.ShipperSettings.TrackShipments == true)
        	{
        		List<SHIP_SERVICES> lSS = new List<SHIP_SERVICES>() {};
        		
        		for (int x = 0; x < 24; x += 2)
        		{
        			int iStartingElement = 1102;	// starting element number for shippers
        			string sShipper = (iStartingElement + x).ToString();
        			string sTrackingSite = (iStartingElement + x + 1).ToString();
        			
        			SHIP_SERVICES SS = new SHIP_SERVICES();
        			        			                			        		
        			SS.Shipper = Settings.repo.Shippers.GetFieldText(sShipper);
        			SS.TrackingSite = Settings.repo.Shippers.GetFieldText(sTrackingSite);        			
        			
        			lSS.Add(SS);															
        		}
        		Variables.globalSettings.CompanySettings.ShipperSettings.ShipServices = lSS;
        	}
        	
        	if (bCloseWin)
        	{
        		Settings.repo.Cancel.Click();
        	}
        	
        }
        
        public static void _SA_GetCompanyNameSettings()
        {
        	_SA_GetCompanyNameSettings(true);
        }
        
        public static void _SA_GetCompanyNameSettings(bool bCloseWin)
        {
        	CheckSettingsDialog();
        	
        	Settings.repo.SettingsTree.CompanyNames.Select();
        	
        	Variables.globalSettings.CompanySettings.additionalInformationDate = Settings.repo.AdditionalInformationDate.TextValue;
        	Variables.globalSettings.CompanySettings.additionalInformationField = Settings.repo.AdditionalInformationField.TextValue;
        	
        	if (bCloseWin)
        	{
        		Settings.repo.Cancel.Click();
        	}
        }
        
        // General Settings Methods
        public static void _SA_Get_AllGeneralSettings()
        {
        	_SA_Get_AllGeneralSettings(true);
        }
        
        public static void _SA_Get_AllGeneralSettings(bool bCloseWin)
        {
        	CheckSettingsDialog();
        	
        	Variables.globalSettings.GeneralSettings = new GENERAL_SETTINGS();
        	Settings._SA_GetGeneralBudgetSettings(false);
        	Settings._SA_GetGeneralNumberingSettings(false);
        	Settings._SA_GetGeneralDepartmentSettings(false);
        	Settings._SA_GetGeneralNameAndLinkedAccountSettings(false);
        	
        	if (bCloseWin)
        	{
        		Settings.repo.Cancel.Click();
        	}
        }
        
        
        public static void _SA_GetGeneralBudgetSettings()
        {
        	_SA_GetGeneralBudgetSettings(true);
        }
        
        public static void _SA_GetGeneralBudgetSettings(bool bCloseWin)
        {
        	CheckSettingsDialog();
        	
        	Settings.repo.SettingsTree.GeneralBudget.Select();
        	
        	Variables.globalSettings.GeneralSettings.budgetRevAndExAccts = Settings.repo.BudgetRevenueAndExpenseAccounts.Checked;
        	if (Variables.globalSettings.GeneralSettings.budgetRevAndExAccts == true)
        	{
        		Variables.globalSettings.GeneralSettings.budgetFrequency = (BUDGET_FREQUENCY)Settings.repo.BudgetFrequency_LAPayUserExp1.SelectedItemIndex;        		
        	}
        	
        	if (bCloseWin)
        	{
        		Settings.repo.Cancel.Click();
        	}
        	
        }
        
        public static void _SA_GetGeneralNumberingSettings()
        {
        	_SA_GetGeneralNumberingSettings(true);
        }
        
        public static void _SA_GetGeneralNumberingSettings(bool bCloseWin)
        {
        	CheckSettingsDialog();
        	
        	Settings.repo.SettingsTree.GeneralNumbering.Select();
        	Variables.globalSettings.GeneralSettings.Numbering.showAcctNumInTransactions = Settings.repo.ShowAcctNumInTransactions.Checked;
        	Variables.globalSettings.GeneralSettings.Numbering.showAcctNumInReports = Settings.repo.ShowAcctNumInReports.Checked;
        	Variables.globalSettings.GeneralSettings.Numbering.numOfDigitsInAcctNum = Settings.repo.numOfDigitsInAcctNum.SelectedItemText;
        	
        	if (Functions.GoodData(Variables.globalSettings.GeneralSettings.Numbering))
        	{
        		List <List<string>> lsNumContents = Settings.repo.AccountNumContainer.GetContents();
        		
        		Variables.globalSettings.GeneralSettings.Numbering.assetStartNum = lsNumContents[0][1];
        		Variables.globalSettings.GeneralSettings.Numbering.assetEndNum = lsNumContents[0][2];
        		Variables.globalSettings.GeneralSettings.Numbering.liabilityStartNum = lsNumContents[1][1];
        		Variables.globalSettings.GeneralSettings.Numbering.liabilityEndNum = lsNumContents[1][2];
        		Variables.globalSettings.GeneralSettings.Numbering.equityStartNum = lsNumContents[2][1];
        		Variables.globalSettings.GeneralSettings.Numbering.equityEndNum = lsNumContents[2][2];
        		Variables.globalSettings.GeneralSettings.Numbering.revStartNum = lsNumContents[3][1];
        		Variables.globalSettings.GeneralSettings.Numbering.revEndNum = lsNumContents[3][2];
        		Variables.globalSettings.GeneralSettings.Numbering.expStartNum = lsNumContents[4][1];
        		Variables.globalSettings.GeneralSettings.Numbering.expEndNum = lsNumContents[4][2];
        	}
        	
        	if (bCloseWin)
        	{
        		Settings.repo.Cancel.Click();
        	}
        }
        
        public static void _SA_GetGeneralDepartmentSettings()
        {
        	_SA_GetGeneralDepartmentSettings(true);
        }
        
        public static void _SA_GetGeneralDepartmentSettings(bool bCloseWin)
        {
        	CheckSettingsDialog();
        	
        	Settings.repo.SettingsTree.GeneralDepartments.Select();
        	
        	Variables.globalSettings.GeneralSettings.UseDepartmentalAcounting = Settings.repo.UseDepartmentalAccounting.Checked;
        	List <DEPT_ACCT> lDA = new List<DEPT_ACCT>() {};
        	
        	int CODE_COLUMN = 0;
        	int DESCRIPTION_COLUMN = 1;
        	int STATUS_COLUMN = 2;
        	
        	List <List <string>> lDP = Settings.repo.DepartmentsContainer.GetContents();
        	
        	foreach (List <string> lsRow in lDP)
        	{
        		DEPT_ACCT DA = new DEPT_ACCT();
        		
        		if (lsRow[CODE_COLUMN] != "")
        		{
        			DA.code = ConvertFunctions.BlankStringToNULL(lsRow[CODE_COLUMN]);
        			DA.description = ConvertFunctions.BlankStringToNULL(lsRow[DESCRIPTION_COLUMN]);
        			if (lsRow[STATUS_COLUMN] == "ACTIVE")
        			{
        				DA.ActiveStatus = true;
        			}
        			else
        			{
        				DA.ActiveStatus = false;
        			}
        			
        			lDA.Add(DA);
        		}
        		else
        		{
        			// exit loop when a blank line is found
        			break;
        		}
        		
        	}
        	Variables.globalSettings.GeneralSettings.DepartmentalAccounting = lDA;
        	
        	if (bCloseWin)
        	{
        		Settings.repo.Cancel.Click();
        	}        	        	
        }
        
        public static void _SA_GetGeneralNameAndLinkedAccountSettings()
        {
        	_SA_GetGeneralNameAndLinkedAccountSettings(true);
        }
        
        public static void _SA_GetGeneralNameAndLinkedAccountSettings(bool bCloseWin)
        {
        	CheckSettingsDialog();
        	
        	Settings.repo.SettingsTree.GeneralNames.Select();
        	
        	Variables.globalSettings.GeneralSettings.AdditionalFields.Field1 = Settings.repo.AdditionalInfo1.TextValue;
        	Variables.globalSettings.GeneralSettings.AdditionalFields.Field2 = Settings.repo.AdditionalInfo2.TextValue;
        	Variables.globalSettings.GeneralSettings.AdditionalFields.Field3 = Settings.repo.AdditionalInfo3.TextValue;
        	Variables.globalSettings.GeneralSettings.AdditionalFields.Field4 = Settings.repo.AdditionalInfo4.TextValue;
        	Variables.globalSettings.GeneralSettings.AdditionalFields.Field5 = Settings.repo.AdditionalInfo5.TextValue;
        	
        	Settings.repo.SettingsTree.GeneralLinkedAccounts.Select();
        	
        	Variables.globalSettings.GeneralSettings.RetainedEarnings = new GL_ACCOUNT();
        	
        	Variables.globalSettings.GeneralSettings.RetainedEarnings.acctNumber = Settings.repo.RetainedEarnings.SelectedItemText;
        	Variables.globalSettings.GeneralSettings.RecordRetainedEarningsBalance = true;
        	
        	if (bCloseWin)
        	{
        		Settings.repo.Cancel.Click();
        	}			        
        }
        
        // Payable Settings Methods        
        public static void _SA_Get_AllPayableSettings()
        {
        	_SA_Get_AllPayableSettings(true);
        }
        
        public static void _SA_Get_AllPayableSettings(bool bCloseWin)
        {
        	CheckSettingsDialog();
        	
        	Variables.globalSettings.PayableSettings = new PAYABLE_SETTINGS();
        	
        	Settings._SA_GetPayablesAddressSettings(false);
        	Settings._SA_GetPayablesOptionsSettings(false);
        	Settings._SA_GetPayablesDutySettings(false);
        	Settings._SA_GetPayablesGeneralNameSettings(false);
        	Settings._SA_GetPayablesLinkedAcctSettings(false);
			
			if (bCloseWin)
        	{
        		Settings.repo.Cancel.Click();
        	}        	
        }
        
        public static void _SA_GetPayablesAddressSettings()
        {
        	_SA_GetPayablesAddressSettings(true);
        }
        
        public static void _SA_GetPayablesAddressSettings(bool bCloseWin)
        {
        	CheckSettingsDialog();
        	
        	Settings.repo.SettingsTree.PayablesAddress.Select();
        	
        	Variables.globalSettings.PayableSettings.City = Settings.repo.VendorCity.TextValue;
        	Variables.globalSettings.PayableSettings.Province = Settings.repo.VendorProvince.TextValue;
        	Variables.globalSettings.PayableSettings.Country = Settings.repo.VendorCountry.TextValue;
        	
        	if (bCloseWin)
        	{
        		Settings.repo.Cancel.Click();
        	}
        }
        
        public static void _SA_GetPayablesOptionsSettings()
        {
        	_SA_GetPayablesOptionsSettings(true);
        }
        
        public static void _SA_GetPayablesOptionsSettings(bool bCloseWin)
        {
        	CheckSettingsDialog();       	
        	
        	Settings.repo.SettingsTree.PayablesOptions.Select();
        	
        	Variables.globalSettings.PayableSettings.agingPeriod1 = Settings.repo.AgingPeriodVendor1.TextValue;
        	Variables.globalSettings.PayableSettings.agingPeriod2 = Settings.repo.AgingPeriodVendor2.TextValue;
        	Variables.globalSettings.PayableSettings.agingPeriod3 = Settings.repo.AgingPeriodVendor3.TextValue;
        	Variables.globalSettings.PayableSettings.calculateDiscountsBeforeTax = Settings.repo.CalculateDiscountsBeforeTaxVendor.Checked;
        	
        	if (bCloseWin)
        	{
        		Settings.repo.Cancel.Click();
        	}
        }
        
        public static void _SA_GetPayablesDutySettings()
        {
        	_SA_GetPayablesDutySettings(true);
        }
		
        public static void _SA_GetPayablesDutySettings(bool bCloseWin)
        {
        	CheckSettingsDialog();
        
        	Settings.repo.SettingsTree.PayablesDuty.Select();
        	
        	Variables.globalSettings.PayableSettings.trackDutyOnImportedItems = Settings.repo.TrackDutyOnImportedItems.Checked;
        	if (Variables.globalSettings.PayableSettings.trackDutyOnImportedItems == true)
        	{
        		Variables.globalSettings.PayableSettings.importDutyAcct.acctNumber = Settings.repo.ImportDutyAccount.SelectedItemText;
        	}
        	
        	if (bCloseWin)
        	{
        		Settings.repo.Cancel.Click();
        	}
        }
        
        public static void _SA_GetPayablesGeneralNameSettings()
        {
        	_SA_GetPayablesGeneralNameSettings(true);
        }
        
        public static void _SA_GetPayablesGeneralNameSettings(bool bCloseWin)
        {
        	CheckSettingsDialog();
        	
        	Settings.repo.SettingsTree.PayablesNames.Select();
        	
        	Variables.globalSettings.PayableSettings.AdditionalFields.Field1 = Settings.repo.AdditionalInfoVendor1.TextValue;
			Variables.globalSettings.PayableSettings.AdditionalFields.Field2 = Settings.repo.AdditionalInfoVendor2.TextValue;
			Variables.globalSettings.PayableSettings.AdditionalFields.Field3 = Settings.repo.AdditionalInfoVendor3.TextValue;
			Variables.globalSettings.PayableSettings.AdditionalFields.Field4 = Settings.repo.AdditionalInfoVendor4.TextValue;
			Variables.globalSettings.PayableSettings.AdditionalFields.Field5 = Settings.repo.AdditionalInfoVendor5.TextValue;
			
			if (bCloseWin)
			{
				Settings.repo.Cancel.Click();
			}
        }
        
        public static void _SA_GetPayablesLinkedAcctSettings()
        {
        	_SA_GetPayablesLinkedAcctSettings(true);
        }
        
        public static void _SA_GetPayablesLinkedAcctSettings(bool bCloseWin)
        {     
        	CheckSettingsDialog();
        	
        	Settings.repo.SettingsTree.PayablesLinkedAccounts.Select();
        	
        	// Single currency
        	if (Settings.repo.PrincipalBankAcctVendor.Visible)
        	{        		
        		Variables.globalSettings.PayableSettings.principalBankAcct.acctNumber = Settings.repo.PrincipalBankAcctVendor.SelectedItemText;	
        	}
        	// Multiple currencies
        	else 
        	{
        		List <List <string>> lSBContents = Settings.repo.BankAccountsVendor.GetContents();
        		Variables.globalSettings.PayableSettings.principalBankAcct.acctNumber = lSBContents[0][1];
        		
        		Variables.globalSettings.PayableSettings.CurrencyAccounts.Clear();
        		
        		foreach (List <string> lsRow in lSBContents)
        		{
        			if (lsRow != null)
        			{
        				CURRENCY_ACCOUNT CA = new CURRENCY_ACCOUNT();
        				CA.Currency = lsRow[0];
        				CA.BankAccount.acctNumber = lsRow[1];        				
        				Variables.globalSettings.PayableSettings.CurrencyAccounts.Add(CA);
        			}
        		}        		
        	}
        	
        	Variables.globalSettings.PayableSettings.apAcct.acctNumber = Settings.repo.AccountsPayable.SelectedItemText;
        	Variables.globalSettings.PayableSettings.freightAcct.acctNumber = Settings.repo.FreightExpense.SelectedItemText;
        	Variables.globalSettings.PayableSettings.earlyPayDiscountAcct.acctNumber = Settings.repo.EarlyPaymentVendor.SelectedItemText;
        	Variables.globalSettings.PayableSettings.prepaymentAcct.acctNumber = Settings.repo.PrePaymentsVendor.SelectedItemText;        	
        	
        	if (bCloseWin)
        	{
        		Settings.repo.Cancel.Click();
        	}
        }
        
        
        // Receivable Settings Methods
        public static void _SA_Get_AllReceivablesSettings()
        {
        	_SA_Get_AllReceivablesSettings(true);
        }
        
        public static void _SA_Get_AllReceivablesSettings(bool bCloseWin)
        {
        	CheckSettingsDialog();
        	
        	Variables.globalSettings.ReceivableSettings = new RECEIVABLE_SETTINGS();
        	Settings._SA_GetReceivablesOptionsAndDiscountSettings(false);
        	Settings._SA_GetReceivablesGeneralNameSettings(false);
        	Settings._SA_GetReceivablesCommentSettings(false);
        	Settings._SA_GetReceivablesLinkedAcctSettings(false);
        	
        	if (bCloseWin)
        	{
        		Settings.repo.Cancel.Click();
        	}
        }
        
        public static void _SA_GetReceivablesOptionsAndDiscountSettings()
        {
        	_SA_GetReceivablesOptionsAndDiscountSettings(true);
        }
        
        public static void _SA_GetReceivablesOptionsAndDiscountSettings(bool bCloseWin)
        {
        	CheckSettingsDialog();
        	
        	Settings.repo.SettingsTree.ReceivablesOptions.Select();
        	
        	Variables.globalSettings.ReceivableSettings.agingPeriod1 = Settings.repo.AgingPeriodCust1.TextValue;
        	Variables.globalSettings.ReceivableSettings.agingPeriod2 = Settings.repo.AgingPeriodCust2.TextValue;
        	Variables.globalSettings.ReceivableSettings.agingPeriod3 = Settings.repo.AgingPeriodCust3.TextValue;
        	Variables.globalSettings.ReceivableSettings.interestCharges = Settings.repo.InterestChargesCust.Checked;
        	Variables.globalSettings.ReceivableSettings.interestPercent = Settings.repo.InterestPercentCust.TextValue;
        	Variables.globalSettings.ReceivableSettings.interestDays = Settings.repo.InterestDaysCust.TextValue;
        	Variables.globalSettings.ReceivableSettings.statementDays = Settings.repo.StatementDaysCust.TextValue;
        	Variables.globalSettings.ReceivableSettings.taxCodeForNewCustomers.code = Settings.repo.TaxCodeForNewCustomers.SelectedItemText;
        	if (Variables.globalSettings.ReceivableSettings.taxCodeForNewCustomers.code == Variables.sNoTax)
        	{
        		Variables.globalSettings.ReceivableSettings.taxCodeForNewCustomers = null;
        	}
        	Variables.globalSettings.ReceivableSettings.printSalesperson = Settings.repo.PrintSalespersonCust.Checked;
        	
        	Settings.repo.SettingsTree.ReceivablesDiscount.Select();
        	
        	Variables.globalSettings.ReceivableSettings.discountPercent = Settings.repo.DiscountPercentCust.TextValue;
        	Variables.globalSettings.ReceivableSettings.discountDays = Settings.repo.DiscountPeriodCust.TextValue;
        	Variables.globalSettings.ReceivableSettings.netDays = Settings.repo.NetDaysCust.TextValue;
        	Variables.globalSettings.ReceivableSettings.calculateEarlyPaymentDiscountsB4Tax = Settings.repo.CalculateEarlyPaymentDiscountsB4Tax.Checked;
        	if (Settings.repo.CalculateLineItemDiscount.Visible)
        	{
        		Variables.globalSettings.ReceivableSettings.calculateLineItemDiscounts = Settings.repo.CalculateLineItemDiscount.Checked;
        	}
        	
        	Nullable <bool> b = Variables.globalSettings.ReceivableSettings.calculateLineItemDiscounts;
        	if (bCloseWin)
        	{
        		Settings.repo.Cancel.Click();
        	}
        }
        
        public static void _SA_GetReceivablesGeneralNameSettings()
        {
        	_SA_GetReceivablesGeneralNameSettings(true);
        }
        
        public static void _SA_GetReceivablesGeneralNameSettings(bool bCloseWin)
        {
        	CheckSettingsDialog();        	
        	
        	Settings.repo.SettingsTree.ReceivablesNames.Select();
        	
        	Variables.globalSettings.ReceivableSettings.AdditionalFields.Field1 = Settings.repo.AdditionalInfoCust1.TextValue;
        	Variables.globalSettings.ReceivableSettings.AdditionalFields.Field2 = Settings.repo.AdditionalInfoCust2.TextValue;
        	Variables.globalSettings.ReceivableSettings.AdditionalFields.Field3 = Settings.repo.AdditionalInfoCust3.TextValue;
        	Variables.globalSettings.ReceivableSettings.AdditionalFields.Field4 = Settings.repo.AdditionalInfoCust4.TextValue;
        	Variables.globalSettings.ReceivableSettings.AdditionalFields.Field5 = Settings.repo.AdditionalInfoCust5.TextValue;
        	
        	if (bCloseWin)
        	{
        		Settings.repo.Cancel.Click();
        	}
        }
        
        public static void _SA_GetReceivablesCommentSettings()
        {
        	_SA_GetReceivablesCommentSettings(true);
        }
        
        public static void _SA_GetReceivablesCommentSettings(bool bCloseWin)
        {
        	CheckSettingsDialog();
        	
        	Settings.repo.SettingsTree.ReceivablesComments.Select();
        	
        	Variables.globalSettings.ReceivableSettings.salesInvoiceComment = Settings.repo.SalesInvoiceComment.TextValue;
        	Variables.globalSettings.ReceivableSettings.salesOrderComment = Settings.repo.SalesOrderComment.TextValue;
        	Variables.globalSettings.ReceivableSettings.salesQuoteComment = Settings.repo.SalesQuoteComment.TextValue;
        	
        	if (bCloseWin)
        	{
        		Settings.repo.Cancel.Click();
        	}
        }
        
        public static void _SA_GetReceivablesLinkedAcctSettings()
        {
        	_SA_GetReceivablesLinkedAcctSettings(true);
        }
        
        public static void _SA_GetReceivablesLinkedAcctSettings(bool bCloseWin)
        {
        	CheckSettingsDialog();
        	
        	Settings.repo.SettingsTree.ReceivablesLinkedAccounts.Select();
        	
        	if (Settings.repo.PrincipleBankAcctCust.Visible)
        	{
        		Variables.globalSettings.ReceivableSettings.principalBankAcct.acctNumber = Settings.repo.PrincipleBankAcctCust.SelectedItemText;
        	}
        	else // Multi currencies
        	{
        		List <List<string>> lscContents = Settings.repo.BankAccountsCust.GetContents();
        		Variables.globalSettings.ReceivableSettings.principalBankAcct.acctNumber = ConvertFunctions.BlankStringToNULL(lscContents[0][1]);
        		
        		foreach (List <string> lsRow in lscContents)
        		{
        			CURRENCY_ACCOUNT CA = new CURRENCY_ACCOUNT();
        			
        			CA.Currency = ConvertFunctions.BlankStringToNULL(lsRow[0]);
        			CA.BankAccount.acctNumber = ConvertFunctions.BlankStringToNULL(lsRow[1]);
        			
        			if (CA.Currency != "")
        			{
        				Variables.globalSettings.ReceivableSettings.CurrencyAccounts.Add(CA);
        			}                                                
        		}
        	}
        	
        	Variables.globalSettings.ReceivableSettings.arAcct.acctNumber = Settings.repo.AccountReceivable.SelectedItemText;
        	Variables.globalSettings.ReceivableSettings.freightAcct.acctNumber = Settings.repo.FreightRevenue.SelectedItemText;
        	Variables.globalSettings.ReceivableSettings.earlyPayDiscountAcct.acctNumber = Settings.repo.EarlyPmtSalesDiscount.SelectedItemText;
        	Variables.globalSettings.ReceivableSettings.depositsAcct.acctNumber = Settings.repo.Deposits.SelectedItemText;

        	if (bCloseWin)
        	{
        		Settings.repo.Cancel.Click();
        	}
        }
        
        // Payroll Settings Methods
        public static void _SA_GetAllPayrollSettings()	// changed from _SA_GetPayrollSettings
        {
        	_SA_GetAllPayrollSettings(true);
        }
        
        public static void _SA_GetAllPayrollSettings(bool bCloseWin)
        {
        	CheckSettingsDialog();
        	
        	Variables.globalSettings.PayrollSettings = new PAYROLL_SETTINGS();
        	
        	Settings._SA_GetPayrollIncomeSettings(false);
        	Settings._SA_GetPayrollDeductionSettings(false);
        	Settings._SA_GetPayrollTaxesSettings(false);
        	Settings._SA_GetPayrollEntitlementSettings(false);
        	Settings._SA_GetPayrollRemittanceSettings(false);
        	Settings._SA_GetPayrollJobSettings(false); // not in silktest project
        	Settings._SA_GetPayrollIncomeDeductionNameSettings(false);
        	Settings._SA_GetPayrollAdditionalNameSettings(false);
        	Settings._SA_GetPayrollLinkedIncomeSettings(false);
        	Settings._SA_GetPayrollLinkedDeductionSettings(false);
        	Settings._SA_GetPayrollLinkedTaxesSettings(false);
        	Settings._SA_GetPayrollLinkedUserDefinedSettings(false);
        	Variables.globalSettings.PayrollSettings.UsePayrollExpenseGroups = true;	// no declarations yet
        	
        	if (bCloseWin)
        	{
        		Settings.repo.Cancel.Click();
        	}
        }
        
        public static void _SA_GetPayrollIncomeSettings()
        {
        	_SA_GetPayrollIncomeSettings(true);
        }
        
        public static void _SA_GetPayrollIncomeSettings(bool bCloseWin)
        {
        	CheckSettingsDialog();        	

        	Settings.repo.SettingsTree.PayrollIncomes.Select();
       
        	Variables.globalSettings.PayrollSettings.IncomeSettings.trackQuebecTips = Settings.repo.TrackQuebecTips.Checked;
        	
        	Settings.repo.PayrollIncomesContainer.ClickFirstCell();
        	List <List<string>> lsIncomeContents = Settings.repo.PayrollIncomesContainer.GetContents();
        	
        	List <PAYROLL_INCOME> lPI = new List<PAYROLL_INCOME>() {};
        	
        	for (int x = 6; x < lsIncomeContents.Count; x++) // Starts from row 7, Regular which is semi modifiable
        	{
        		if (Functions.GoodData(lsIncomeContents[x][0]) && lsIncomeContents[x][0] != "")
        		{
        			PAYROLL_INCOME PI = new PAYROLL_INCOME();
        			
        			PI.incomeName = ConvertFunctions.BlankStringToNULL(lsIncomeContents[x][0]);
        			string sType = ConvertFunctions.BlankStringToNULL(lsIncomeContents[x][1]);
        			switch (sType)
        			{
        				case "Income":
        					PI.IncomeType = INCOME_TYPE.INCOME_TYPE_INCOME;
        					break;
        				case "Benefit":
        					PI.IncomeType = INCOME_TYPE.INCOME_TYPE_BENEFIT;
        					break;
        				case "Reimbursement":
        					PI.IncomeType = INCOME_TYPE.INCOME_TYPE_REIMBURSEMENT;
        					break;
        				case "Hourly Rate":
        					PI.IncomeType = INCOME_TYPE.INCOME_TYPE_HOURLY_RATE;
        					break;
        				case "Piece Rate":
        					PI.IncomeType = INCOME_TYPE.INCOME_TYPE_PIECE_RATE;
        					break;
        				case "Differential Rate":
        					PI.IncomeType = INCOME_TYPE.INCOME_TYPE_DIFFERENTIAL_RATE;
        					break;
        			}
        			PI.unitofMeasure = ConvertFunctions.BlankStringToNULL(lsIncomeContents[x][2]);
        			lPI.Add(PI);
        		}
        	}
        	
        	Variables.globalSettings.PayrollSettings.IncomeSettings.Incomes = lPI;
        	
        	if (bCloseWin)
        	{
        		Settings.repo.Cancel.Click();
        	}
        }
        
        public static void _SA_GetPayrollDeductionSettings()
        {
        	_SA_GetPayrollDeductionSettings(true);
        }
        
        public static void _SA_GetPayrollDeductionSettings(bool bCloseWin)
        {
        	CheckSettingsDialog();
        	
        	List <PAYROLL_DEDUCTION> lPD = new List<PAYROLL_DEDUCTION>() {};
        	
        	Settings.repo.SettingsTree.PayrollDeductions.Select();        	        	
        	
        	foreach (List <string> lsRow in Settings.repo.PayrollDeductionsContainer.GetContents())
        	{
        		PAYROLL_DEDUCTION PD = new PAYROLL_DEDUCTION();
        		
        		if (lsRow[0] != "")
        		{
        			PD.Deduction = lsRow[0].Trim();
        			if (lsRow[1].Trim() == "Amount")
        			{
        				PD.DeductBy = DEDUCT_TYPE.DEDUCT_AMOUNT;
        			}
        			else if (lsRow[1].Trim().Contains("Percent"))
	        		{
	        			PD.DeductBy = DEDUCT_TYPE.DEDUCT_PERCENT;
	        		}
	        		else
	        		{
	        			Functions.Verify(false, true, "Valid value from Deductions table, Deduct By column");
	        		}
	        		lPD.Add(PD);
        		}
        	}
        	Variables.globalSettings.PayrollSettings.Deductions = lPD;
        	
        	if (bCloseWin)
        	{
        		Settings.repo.Cancel.Click();
        	}
        }
        
        public static void _SA_GetPayrollTaxesSettings()
        {
        	_SA_GetPayrollTaxesSettings(true);
        }
        
        public static void _SA_GetPayrollTaxesSettings(bool bCloseWin)
        {
        	CheckSettingsDialog();
        	
        	Variables.globalSettings.PayrollSettings.TaxSettings = new PAYROLL_TAXES_SETTINGS();
        	
        	Settings.repo.SettingsTree.PayrollTaxes.Select();
        	
        	Variables.globalSettings.PayrollSettings.TaxSettings.eiFactor = Settings.repo.EIFactor.TextValue;
        	Variables.globalSettings.PayrollSettings.TaxSettings.wcbRate = Settings.repo.WCBRate.TextValue;
        	Variables.globalSettings.PayrollSettings.TaxSettings.ehtFactor = Settings.repo.EHTFactor.TextValue;
        	Variables.globalSettings.PayrollSettings.TaxSettings.qhsfFactor = Settings.repo.QHSFFactor.TextValue;
        	
        	if (bCloseWin)
        	{
        		Settings.repo.Cancel.Click();
        	}
        }
        
        public static void _SA_GetPayrollEntitlementSettings()
        {
        	_SA_GetPayrollEntitlementSettings(true);
        }
        
        public static void _SA_GetPayrollEntitlementSettings(bool bCloseWin)
        {
        	CheckSettingsDialog();
        	
        	Settings.repo.SettingsTree.PayrollEntitlements.Select();
        	
        	Variables.globalSettings.PayrollSettings.EntitlementSettings.numOfHrsInTheWorkDay = Settings.repo.NumOfHoursInTheWorkDay.TextValue;
        	
        	List <PAYROLL_ENTITLEMENTS> PYE = new List<PAYROLL_ENTITLEMENTS>() {};        	
        	
        	foreach (List <string> lsRow in Settings.repo.PayrollEntitlementsContainer.GetContents())
        	{
        		PAYROLL_ENTITLEMENTS PE = new PAYROLL_ENTITLEMENTS();
        		if (PE.entitlementName == "<undefined>")
        		{
        			PE.percentOfHrsWorked = "0.00";
        			PE.maxDays = "0.00";
        			PE.clearDaysAtYearEnd = false;
        		}
        		PE.entitlementName = ConvertFunctions.BlankStringToNULL(lsRow[0]);
        		PE.percentOfHrsWorked = ConvertFunctions.BlankStringToNULL(lsRow[1]);
        		PE.maxDays = ConvertFunctions.BlankStringToNULL(lsRow[2]);
        		string sClear = ConvertFunctions.BlankStringToNULL(lsRow[3]);
        		if (sClear == "No")
        		{
        			PE.clearDaysAtYearEnd = false;
        		}
        		else
        		{
        			PE.clearDaysAtYearEnd = true;
        		}
        		if (PE.entitlementName == "")
        		{
        			break;
        		}
        		PYE.Add(PE);
        	}
        	Variables.globalSettings.PayrollSettings.EntitlementSettings.Entitlements = PYE;
        	
        	if (bCloseWin)
        	{
        		Settings.repo.Cancel.Click();
        	}
        }
        
        public static void _SA_GetPayrollRemittanceSettings()
        {
        	_SA_GetPayrollRemittanceSettings(true);
        }
        
        public static void _SA_GetPayrollRemittanceSettings(bool bCloseWin)
        {
        	CheckSettingsDialog();
        	
        	Settings.repo.SettingsTree.PayrollRemittance.Select();
        	
        	List <PAYROLL_REMITTANCE> lPR = new List<PAYROLL_REMITTANCE>() {};        	     
        	
        	foreach (List <string> lsRow in Settings.repo.PayrollRemittanceContainer.GetContents())
        	{
        		PAYROLL_REMITTANCE PR = new PAYROLL_REMITTANCE();
        		PR.RemitName = lsRow[0].Trim();
        		PR.RemitVendor = lsRow[1].Trim();
        		
        		string sFrequency = lsRow[2].Trim();
        		switch (sFrequency)
        		{
        			case "None":
        				PR.RemitFrequency = REMITTING_FREQUENCY.REMIT_NONE;
        				break;
        			case "Every Seven Days":
        				PR.RemitFrequency = REMITTING_FREQUENCY.REMIT_EVERY_7;
        				break;
        			case "Weekly":
        				PR.RemitFrequency = REMITTING_FREQUENCY.REMIT_WEEKLY;
        				break;
        			case "Twice Monthly":
        				PR.RemitFrequency = REMITTING_FREQUENCY.REMIT_TWICE_MONTHLY;
        				break;
        			case "Monthly":
        				PR.RemitFrequency = REMITTING_FREQUENCY.REMIT_MONTHLY;
        				break;
        			case "Quarterly":
        				PR.RemitFrequency = REMITTING_FREQUENCY.REMIT_QUARTERLY;
        				break;
        			case "Annually":
        				PR.RemitFrequency = REMITTING_FREQUENCY.REMIT_ANNUALLY;
        				break;
        		}
        		PR.EndOfNextRemitPeriod = lsRow[3].Trim();
        		lPR.Add(PR);
        	}
        	Variables.globalSettings.PayrollSettings.Remittances = lPR;
        	
        	if (bCloseWin)
        	{
        		Settings.repo.Cancel.Click();
        	}
        }
        
        public static void _SA_GetPayrollJobSettings()
        {
        	_SA_GetPayrollJobSettings(true);
        }
        
        public static void _SA_GetPayrollJobSettings(bool bCloseWin)
        {
        	CheckSettingsDialog();
        	
        	Variables.globalSettings.PayrollSettings.JobCategories.Clear();
        	Settings.repo.SettingsTree.PayrollJobCategories.Select();        	        	
        	
        	foreach (List <string> lsRow in Settings.repo.PayrollJobCategoriesContainer.GetContents())
        	{
        		PAYROLL_JOB PJ = new PAYROLL_JOB();
        		PJ.Category = lsRow[0];
        		PJ.SubmitTimeSlips = ConvertFunctions.StringToBool(lsRow[1]);
        		PJ.AreSalespersons = ConvertFunctions.StringToBool(lsRow[2]);
        		
        		if (lsRow[3] == "Active")
        		{
        			PJ.ActiveStatus = true;
        		}
        		else
        		{
        			PJ.ActiveStatus = false;
        		}
        		
        		Variables.globalSettings.PayrollSettings.JobCategories.Add(PJ);
        	}
        	
        	// Job categories assigned
        	if (Functions.GoodData(Variables.globalSettings.PayrollSettings.JobCategories))
        	{
        		for (int x = 0; x < Variables.globalSettings.PayrollSettings.JobCategories.Count; x++)
        		{
        			if (Variables.globalSettings.PayrollSettings.JobCategories[x].Category != "<None>")
        			{
        				Variables.globalSettings.PayrollSettings.JobCategories[x].EmployeesAssigned.Clear();
        				Settings.repo.AssignJobCategories.Click();
        				        		
        				// Select Job category after verifying it is available the combobox        				        				        			        				
        				if (Functions.ListFind(JobCategoryInformation.repo.JobCategorySelection.Items, Variables.globalSettings.PayrollSettings.JobCategories[x].Category) != -1)
        				{
        					JobCategoryInformation.repo.JobCategorySelection.Select(Variables.globalSettings.PayrollSettings.JobCategories[x].Category);
        				}    						        				
        				else // Category not found
        				{        				  
        					// Inactive categories will not show up in combobox
							if (Variables.globalSettings.PayrollSettings.JobCategories[x].ActiveStatus == true)
							{
								Functions.Verify(false, true, "Able to find the category");
							}
        				}        				
        				        				
        				if (Functions.GoodData(JobCategoryInformation.repo.EmployeesInJobCat.Items))
        				{
        					foreach (ListItem aItem in JobCategoryInformation.repo.EmployeesInJobCat.Items)
        					{
        						EMPLOYEE Emp = new EMPLOYEE();
        						Emp.name = ConvertFunctions.BlankStringToNULL(aItem.Text);
        						if (Emp.name != null)
        						{
        							Variables.globalSettings.PayrollSettings.JobCategories[x].EmployeesAssigned.Add(Emp);
        						}
        					}
        				}
        				JobCategoryInformation.repo.Cancel.Click();
        			}
        		}
        	}
        	
        	if (bCloseWin)
        	{
        		Settings.repo.Cancel.Click();
        	}
        }
        
        public static void _SA_GetPayrollIncomeDeductionNameSettings()
        {
        	_SA_GetPayrollIncomeDeductionNameSettings(true);
        }
        
        public static void _SA_GetPayrollIncomeDeductionNameSettings(bool bCloseWin)
        {
        	CheckSettingsDialog();
        	
        	Settings.repo.SettingsTree.PayrollNamesIncomesDeductions.Select();
        	
        	int loopCount = 0;
        	int fixedIncome = 9;         	
        	INCOME_NAME[] lIN = new INCOME_NAME[20];
        	int INCOME_DEDUCTIONS_NAMES_COLUMN = 1;
        	
        	foreach (List <string> incomeLine in Settings.repo.IncomeNamesContainer.GetContents())
        	{
        		INCOME_NAME IN = new INCOME_NAME();
        		
        		// Skip the first 9 lines
        		if (loopCount++ > fixedIncome)
        		{
        			// handle blank line
        			if (incomeLine[0] != "")
        			{
        				IN.Income = "Income " + (loopCount - fixedIncome-1) + "";
        				IN.Name = ConvertFunctions.BlankStringToNULL(incomeLine[INCOME_DEDUCTIONS_NAMES_COLUMN]);
        				                                             
        				lIN[loopCount - fixedIncome - 2] = IN;
        			}
        		}
        	}
        	Variables.globalSettings.PayrollSettings.AdditionalIncome = lIN;
        	
        	DEDUCTION_NAME[] lDN = new DEDUCTION_NAME[20];
        	
        	int x = 0;
        	foreach (List <string> deductionLine in Settings.repo.DeductionNamesContainer.GetContents())
        	{
        		DEDUCTION_NAME DN = new DEDUCTION_NAME();
        		
        		// handle blank line
        		if (deductionLine[0] != "")
        		{
        			DN.Deduction = "Deduction " + (x + 1) + "";
        			DN.Name = ConvertFunctions.BlankStringToNULL(deductionLine[INCOME_DEDUCTIONS_NAMES_COLUMN]);
        			
        			lDN[x] = DN;
        		}
        		x++;
        	}
        	Variables.globalSettings.PayrollSettings.AdditionalDeduction = lDN;
        		
        	if (bCloseWin)
        	{
        		Settings.repo.Cancel.Click();
        	}
        }
        
        public static void _SA_GetPayrollAdditionalNameSettings()
        {
        	_SA_GetPayrollAdditionalNameSettings(true);
        }
        
        public static void _SA_GetPayrollAdditionalNameSettings(bool bCloseWin)
        {
        	CheckSettingsDialog();
        	
        	Settings.repo.SettingsTree.PayrollNamesAdditionalPayroll.Select();
        	
        	Variables.globalSettings.PayrollSettings.AdditionalPayrollSettings.AdditionalFields.Field1 = Settings.repo.AdditionalPayrollAddInfo1.TextValue;
        	Variables.globalSettings.PayrollSettings.AdditionalPayrollSettings.AdditionalFields.Field2 = Settings.repo.AdditionalPayrollAddInfo2.TextValue;
        	Variables.globalSettings.PayrollSettings.AdditionalPayrollSettings.AdditionalFields.Field3 = Settings.repo.AdditionalPayrollAddInfo3.TextValue;
        	Variables.globalSettings.PayrollSettings.AdditionalPayrollSettings.AdditionalFields.Field4 = Settings.repo.AdditionalPayrollAddInfo4.TextValue;
        	Variables.globalSettings.PayrollSettings.AdditionalPayrollSettings.AdditionalFields.Field5 = Settings.repo.AdditionalPayrollAddInfo5.TextValue;
        	
        	Variables.globalSettings.PayrollSettings.AdditionalPayrollSettings.ExpenseFields.Field1 = Settings.repo.AdditionalPayrollUDExp1.TextValue;	
        	Variables.globalSettings.PayrollSettings.AdditionalPayrollSettings.ExpenseFields.Field2 = Settings.repo.AdditionalPayrollUDExp2.TextValue;
        	Variables.globalSettings.PayrollSettings.AdditionalPayrollSettings.ExpenseFields.Field3 = Settings.repo.AdditionalPayrollUDExp3.TextValue;
        	Variables.globalSettings.PayrollSettings.AdditionalPayrollSettings.ExpenseFields.Field4 = Settings.repo.AdditionalPayrollUDExp4.TextValue;
        	Variables.globalSettings.PayrollSettings.AdditionalPayrollSettings.ExpenseFields.Field5 = Settings.repo.AdditionalPayrollUDExp5.TextValue;
        	
        	Variables.globalSettings.PayrollSettings.AdditionalPayrollSettings.EntitlementFields.Field1 = Settings.repo.AdditionalPayrollEntitle1.TextValue;
        	Variables.globalSettings.PayrollSettings.AdditionalPayrollSettings.EntitlementFields.Field2 = Settings.repo.AdditionalPayrollEntitle2.TextValue;
        	Variables.globalSettings.PayrollSettings.AdditionalPayrollSettings.EntitlementFields.Field3 = Settings.repo.AdditionalPayrollEntitle3.TextValue;
        	Variables.globalSettings.PayrollSettings.AdditionalPayrollSettings.EntitlementFields.Field4 = Settings.repo.AdditionalPayrollEntitle4.TextValue;
        	Variables.globalSettings.PayrollSettings.AdditionalPayrollSettings.EntitlementFields.Field5 = Settings.repo.AdditionalPayrollEntitle5.TextValue;
        	
        	if (Settings.repo.AdditionalPayrollProvTaxInfo.Exists(Variables.iExistWaitTime))
        	{
        		Variables.globalSettings.PayrollSettings.AdditionalPayrollSettings.provTax = Settings.repo.AdditionalPayrollProvTax.TextValue;
        	}
        	
        	if (Settings.repo.AdditionalPayrollWorkersCompInfo.Exists(Variables.iExistWaitTime))
        	{
        		Variables.globalSettings.PayrollSettings.AdditionalPayrollSettings.workersComp = Settings.repo.AdditionalPayrollWorkersComp.SelectedItemText;        		
        	}
        	
        	if (bCloseWin)
        	{
        		Settings.repo.Cancel.Click();
        	}
        }
        
        public static void _SA_GetPayrollLinkedIncomeSettings()
        {
        	_SA_GetPayrollLinkedIncomeSettings(true);
        }
        
        public static void _SA_GetPayrollLinkedIncomeSettings(bool bCloseWin)
        {
        	CheckSettingsDialog();
        	
        	Settings.repo.SettingsTree.PayrollLinkedAcctsIncomes.Select();
        	
        	Variables.globalSettings.PayrollSettings.IncomeAccounts.principalBank.acctNumber = Settings.repo.LAPrincipleBank.SelectedItemText;
			Variables.globalSettings.PayrollSettings.IncomeAccounts.vacation.acctNumber = Settings.repo.LAVacOwned.SelectedItemText;
			Variables.globalSettings.PayrollSettings.IncomeAccounts.advances.acctNumber = Settings.repo.LAAdvances.SelectedItemText;
			
			Settings.repo.LAIncomesContainer.ClickFirstCell();
			Settings.repo.LAIncomesContainer.MoveRight();
			
			Variables.globalSettings.PayrollSettings.IncomeAccounts.vacationEarnedLinkedAccount.acctNumber = Settings.repo.LAIncomesContainerTextField.TextValue;			
			Settings.repo.LAIncomesContainer.MoveDown();
			Variables.globalSettings.PayrollSettings.IncomeAccounts.regularWageLinkedAccount.acctNumber = Settings.repo.LAIncomesContainerTextField.TextValue;
			Settings.repo.LAIncomesContainer.MoveDown();
			Variables.globalSettings.PayrollSettings.IncomeAccounts.ot1LinkedAccount.acctNumber = Settings.repo.LAIncomesContainerTextField.TextValue;
			Settings.repo.LAIncomesContainer.MoveDown();
			Variables.globalSettings.PayrollSettings.IncomeAccounts.ot2LinkedAccount.acctNumber = Settings.repo.LAIncomesContainerTextField.TextValue;
			Settings.repo.LAIncomesContainer.MoveDown();
			
			// other incomes								
        	int counter = 0;
        	foreach(List <string> LinkAcctLine in Settings.repo.LAIncomesContainer.GetContents())
        	{        		
        		// Ignore the first four lines
        		if(counter > 3 && LinkAcctLine[1] != "")
        		{
        			Variables.globalSettings.PayrollSettings.AdditionalIncome[counter-4].LinkedAccount.acctNumber = LinkAcctLine[1];
        		}
        		counter++;
        	}
			
			if (bCloseWin)
			{
				Settings.repo.Cancel.Click();
			}
        }
        
        public static void _SA_GetPayrollLinkedDeductionSettings()
        {
        	_SA_GetPayrollLinkedDeductionSettings(true);
        }
        
        public static void _SA_GetPayrollLinkedDeductionSettings(bool bCloseWin)
        {
        	CheckSettingsDialog();
        	
        	Settings.repo.SettingsTree.PayrollLinkedAcctsDeductions.Select();
        	
        	Settings.repo.LADeductionsContainer.ClickFirstCell();
        	Settings.repo.LADeductionsContainer.MoveRight();
        	
        	for (int x = 0; x < 20; x++)
        	{
        		Variables.globalSettings.PayrollSettings.AdditionalDeduction[x].LinkedAccount.acctNumber = Settings.repo.LADeductionsContainerTextField.TextValue;
        		Settings.repo.LADeductionsContainer.MoveDown();
        	}
        	
        	if (bCloseWin)
        	{
        		Settings.repo.Cancel.Click();
        	}
        }
        
        public static void _SA_GetPayrollLinkedTaxesSettings()
        {
        	_SA_GetPayrollLinkedTaxesSettings(true);
        }
        
        public static void _SA_GetPayrollLinkedTaxesSettings(bool bCloseWin)
        {
        	CheckSettingsDialog();
        	
        	Settings.repo.SettingsTree.PayrollLinkedAcctsTaxes.Select();
        	
        	// Payables
        	Variables.globalSettings.PayrollSettings.TaxAccounts.payEI.acctNumber = Settings.repo.LAPayEI.SelectedItemText;
        	Variables.globalSettings.PayrollSettings.TaxAccounts.payCPP.acctNumber = Settings.repo.LAPayCPP.SelectedItemText;
        	Variables.globalSettings.PayrollSettings.TaxAccounts.payTax.acctNumber = Settings.repo.LAPayTax.SelectedItemText;
        	Variables.globalSettings.PayrollSettings.TaxAccounts.payWCB.acctNumber = Settings.repo.LAPayWCB.SelectedItemText;
        	Variables.globalSettings.PayrollSettings.TaxAccounts.payEHT.acctNumber = Settings.repo.LAPayEHT.SelectedItemText;
        	Variables.globalSettings.PayrollSettings.TaxAccounts.payQueTax.acctNumber = Settings.repo.LAPayQueTax.SelectedItemText;
        	Variables.globalSettings.PayrollSettings.TaxAccounts.payQPP.acctNumber = Settings.repo.LAPayQPP.SelectedItemText;
        	Variables.globalSettings.PayrollSettings.TaxAccounts.payQHSF.acctNumber = Settings.repo.LAPayQHSF.SelectedItemText;
        	Variables.globalSettings.PayrollSettings.TaxAccounts.payQPIP.acctNumber = Settings.repo.LAPayQPIP.SelectedItemText;
        	
        	// Expenses
        	Variables.globalSettings.PayrollSettings.TaxAccounts.exEI.acctNumber = Settings.repo.LAExEI.SelectedItemText;
        	Variables.globalSettings.PayrollSettings.TaxAccounts.exCPP.acctNumber = Settings.repo.LAExCPP.SelectedItemText;
        	Variables.globalSettings.PayrollSettings.TaxAccounts.exWCB.acctNumber = Settings.repo.LAExWCB.SelectedItemText;
        	Variables.globalSettings.PayrollSettings.TaxAccounts.exEHT.acctNumber = Settings.repo.LAExEHT.SelectedItemText;
        	Variables.globalSettings.PayrollSettings.TaxAccounts.exQPP.acctNumber = Settings.repo.LAExQPP.SelectedItemText;
        	Variables.globalSettings.PayrollSettings.TaxAccounts.exQHSF.acctNumber = Settings.repo.LAExQHSF.SelectedItemText;
        	Variables.globalSettings.PayrollSettings.TaxAccounts.exQPIP.acctNumber = Settings.repo.LAExQPIP.SelectedItemText;
        	
        	if (bCloseWin)
        	{
        		Settings._SA_Invoke();
        	}        		
        }
        
        public static void _SA_GetPayrollLinkedUserDefinedSettings()
        {
        	_SA_GetPayrollLinkedUserDefinedSettings(true);
        }
        
        public static void _SA_GetPayrollLinkedUserDefinedSettings(bool bCloseWin)
        {
        	CheckSettingsDialog();
        	
        	Settings.repo.SettingsTree.PayrollLinkedAcctsUDExpenses.Select();
        	
        	// Payables
        	Variables.globalSettings.PayrollSettings.UserDefinedAccounts.payable1Acct.acctNumber = Settings.repo.BudgetFrequency_LAPayUserExp1.SelectedItemText;
        	Variables.globalSettings.PayrollSettings.UserDefinedAccounts.payable2Acct.acctNumber = Settings.repo.LAPayUserExp2.SelectedItemText;
        	Variables.globalSettings.PayrollSettings.UserDefinedAccounts.payable3Acct.acctNumber = Settings.repo.LAPayUserExp3.SelectedItemText;
        	Variables.globalSettings.PayrollSettings.UserDefinedAccounts.payable4Acct.acctNumber = Settings.repo.LAPayUserExp4.SelectedItemText;
        	Variables.globalSettings.PayrollSettings.UserDefinedAccounts.payable5Acct.acctNumber = Settings.repo.LAPayUserExp5.SelectedItemText;
        	
        	// Expenses
        	Variables.globalSettings.PayrollSettings.UserDefinedAccounts.expense1Acct.acctNumber = Settings.repo.LAExUserExp1.SelectedItemText;
        	Variables.globalSettings.PayrollSettings.UserDefinedAccounts.expense2Acct.acctNumber = Settings.repo.LAExUserExp2.SelectedItemText;
        	Variables.globalSettings.PayrollSettings.UserDefinedAccounts.expense3Acct.acctNumber = Settings.repo.LAExUserExp3.SelectedItemText;
        	Variables.globalSettings.PayrollSettings.UserDefinedAccounts.expense4Acct.acctNumber = Settings.repo.LAExUserExp4.SelectedItemText;        	
        	
        	if (Settings.repo.LAExUserExp5Info.Exists(Variables.iExistWaitTime))
        	{
        		Variables.globalSettings.PayrollSettings.UserDefinedAccounts.expense5Acct.acctNumber = Settings.repo.LAExUserExp5.SelectedItemText;
        	}
        	
        	if (bCloseWin)
        	{
        		Settings.repo.Cancel.Click();
        	}        	
        }
        
                                     
        // Inventory Methods
        public static void _SA_Get_AllInventorySettings()
        {
        	_SA_Get_AllInventorySettings(true);
        }
        
        public static void _SA_Get_AllInventorySettings(bool bCloseWin)
        {
        	CheckSettingsDialog();
        	
        	Variables.globalSettings.InventorySettings = new INVENTORY_SETTINGS();
        	
        	Settings._SA_GetInventoryOptionSettings(false);
        	Settings._SA_GetInventoryPriceListSettings(false);
        	Settings._SA_GetInventoryLocationsSettings(false);
        	Settings._SA_GetInventoryCategoriesSettings(false);
        	Settings._SA_GetInventoryNameAndLinkedAcctSettings(false);
        	
        	if (bCloseWin)
        	{
        		Settings.repo.Cancel.Click();
        	}
        }
        
        public static void _SA_GetInventoryOptionSettings()
        {
        	_SA_GetInventoryOptionSettings(true);
        }
        
        public static void _SA_GetInventoryOptionSettings(bool bCloseWin)
        {
        	CheckSettingsDialog();
        	
        	Settings.repo.SettingsTree.InventoryOptions.Select();
        	
        	if (Settings.repo.InventoryCostingMethodAverageCost.Checked)
        	{
        		Variables.globalSettings.InventorySettings.inventoryCostingMethod = COSTING_METHOD.AVERAGE_COST;
        	}
        	else
        	{
        		Variables.globalSettings.InventorySettings.inventoryCostingMethod = COSTING_METHOD.FIFO_COST;
        		Variables.globalSettings.InventorySettings.allowSerialNums = Settings.repo.AllowSerialNumbersForInventory.Checked;
        	}
        	
        	if (Settings.repo.ProfitMethodMarkup.Checked)
        	{
        		Variables.globalSettings.InventorySettings.profitMethod = PROFIT_METHOD.MARKUP;
        	}
        	else
        	{
        		Variables.globalSettings.InventorySettings.profitMethod = PROFIT_METHOD.MARGIN;
        	}
        	
        	if (Settings.repo.SortMethodNumber.Checked)
        	{
        		Variables.globalSettings.InventorySettings.sortMethod = SORT_METHOD.SORT_NUMBER;
        	}
        	else
        	{
        		Variables.globalSettings.InventorySettings.sortMethod = SORT_METHOD.SORT_DESCRIPTION;        			
        	}
        	
        	if (Settings.repo.PricingMethodExchange.Visible)
        	{
        		if (Settings.repo.PricingMethodExchange.Checked)
        		{
        			Variables.globalSettings.InventorySettings.priceMethod = PRICE_METHOD.CALCULATED;
        		}
        		else
        		{
        			Variables.globalSettings.InventorySettings.priceMethod = PRICE_METHOD.TAKEN;
        		}        		
        	}
        	Variables.globalSettings.InventorySettings.allowInventoryLevelsToGoBelowZero = Settings.repo.AllowInventoryLevelsToGoBelowZero.Checked;        	
        	
        	if (bCloseWin)
        	{
        		Settings.repo.Cancel.Click();
        	}
        }
        
        public static void _SA_GetInventoryPriceListSettings()
        {
        	_SA_GetInventoryPriceListSettings(true);
        }
        
        public static void _SA_GetInventoryPriceListSettings(bool bCloseWin)
        {
        	CheckSettingsDialog();
        	
        	Settings.repo.SettingsTree.InventoryPriceList.Select();
        	
        	List <PRICE_LIST> lPL = new List<PRICE_LIST>() {};        	
        	
        	foreach (List <string> lsRow in Settings.repo.PriceListContainer.GetContents())
        	{
        		PRICE_LIST PL = new PRICE_LIST();
        		        		
    			PL.description = lsRow[0];
    			if (lsRow[1] == "Active")
    			{
    				PL.ActiveStatus = true;
    			}
    			else
    			{
    				PL.ActiveStatus = false;
    			}
    			lPL.Add(PL);        		
        	}
        	Variables.globalSettings.InventorySettings.PriceList = lPL;
        	
        	if (bCloseWin)
        	{
        		Settings.repo.Cancel.Click();
        	}
        }
        
        public static void _SA_GetInventoryLocationsSettings()
        {
        	_SA_GetInventoryLocationsSettings(true);
        }
        
        public static void _SA_GetInventoryLocationsSettings(bool bCloseWin)
        {
        	CheckSettingsDialog();
        	
        	Settings.repo.SettingsTree.InventoryLocations.Select();
        	
        	Variables.globalSettings.InventorySettings.UseMultipleLocations = Settings.repo.UseMultipleLocations.Checked;
        	
        	if (Variables.globalSettings.InventorySettings.UseMultipleLocations == true)
        	{
        		List <LOCATION> lLoc = new List<LOCATION>() {};
        		
        		foreach (List <string> lsRow in Settings.repo.LocationsContainer.GetContents())
        		{
        			LOCATION loc = new LOCATION();
        			loc.code = lsRow[0];
        			loc.description = lsRow[1];
        			if (lsRow[2] == "Active")
        			{
        				loc.ActiveStatus = true;
        			}
        			else
        			{
        				loc.ActiveStatus = false;
        			}
        			if (loc.code == "")
        			{
        				break;
        			}
        			lLoc.Add(loc);
        		}
        		Variables.globalSettings.InventorySettings.Locations = lLoc;        		
        	}
        	else
        	{
        		Variables.globalSettings.InventorySettings.Locations.Clear();
        	}
        	
        	if (bCloseWin)
        	{
        		Settings.repo.Cancel.Click();
        	}
        }
        
        public static void _SA_GetInventoryCategoriesSettings()
        {
        	_SA_GetInventoryCategoriesSettings(true);
        }
        
        public static void _SA_GetInventoryCategoriesSettings(bool bCloseWin)
        {
        	CheckSettingsDialog();
        	
        	Settings.repo.SettingsTree.InventoryCategories.Select();
        	
        	Variables.globalSettings.InventorySettings.UseCategories = Settings.repo.UseCategoriesForInventory.Checked;
        	
        	if (Variables.globalSettings.InventorySettings.UseCategories == true)
        	{
	        	List <string> lsTempList = new List<string>();
	        	List<CATEGORY> lC = new List<CATEGORY>() {};
	        	        
	        	foreach (List <string> lsRow in Settings.repo.CategoriesContainer.GetContents())
	        	{
	        		CATEGORY kat = new CATEGORY();
	        		kat.name = lsRow[0];
	        		lC.Add(kat);
	        	}        	
        	        
	        	// Assign items dialog
	        	if (Settings.repo.AssignItems.Enabled)
	        	{
	        		Settings.repo.AssignItems.Click();
	        		foreach (CATEGORY Cat in lC)
	        		{
	        			Cat.items.Clear();
	        			CategoryInformation.repo.Category.Select(Cat.name);	        				        			
	        			
	        			for (int y = 0; y < CategoryInformation.repo.ItemsInCategory.Items.Count; y++)
	        			{
	        				ITEM I = new ITEM();
	        				string[] tempLine = CategoryInformation.repo.ItemsInCategory.Items[y].Text.Split('\t');
	        				
	        				I.invOrServNumber = tempLine[0];	        					
	        				I.invOrServDescription = tempLine[1];
	        				
	        				Cat.items.Add(I);	        				
	        			}
	        		}
	        	}
	        	CategoryInformation.repo.Cancel.Click();
	        	
	        	Variables.globalSettings.InventorySettings.Categories = lC;	        	
        	}
        	
        	if (bCloseWin)
        	{
        		Settings.repo.Cancel.Click();
        	}
        }
        
        public static void _SA_GetInventoryNameAndLinkedAcctSettings()
        {
        	_SA_GetInventoryNameAndLinkedAcctSettings(true);
        }
        
        public static void _SA_GetInventoryNameAndLinkedAcctSettings(bool bCloseWin)
        {
        	CheckSettingsDialog();
        	
        	Settings.repo.SettingsTree.InventoryNames.Select();
        	Variables.globalSettings.InventorySettings.AdditionalFields.Field1 = Settings.repo.AdditionalInfoInv1.TextValue;
        	Variables.globalSettings.InventorySettings.AdditionalFields.Field2 = Settings.repo.AdditionalInfoInv2.TextValue;
        	Variables.globalSettings.InventorySettings.AdditionalFields.Field3 = Settings.repo.AdditionalInfoInv3.TextValue;
        	Variables.globalSettings.InventorySettings.AdditionalFields.Field4 = Settings.repo.AdditionalInfoInv4.TextValue;
        	Variables.globalSettings.InventorySettings.AdditionalFields.Field5 = Settings.repo.AdditionalInfoInv5.TextValue;
        	
        	Settings.repo.SettingsTree.InventoryLinkedAccounts.Select();
        	Variables.globalSettings.InventorySettings.itemAssemblyCosts.acctNumber = Settings.repo.ItemAssemblyCosts.SelectedItemText;
        	Variables.globalSettings.InventorySettings.adjustmentWriteOff.acctNumber = Settings.repo.AdjustmentWriteOff.SelectedItemText;
        	
        	if (bCloseWin)
        	{
        		Settings.repo.Cancel.Click();
        	}
        }
        
        // Project Settings Methods
        public static void _SA_Get_AllProjectSettings()
        {
        	_SA_Get_AllProjectSettings(true);        	
        }
        
        public static void _SA_Get_AllProjectSettings(bool bCloseWin)
        {
        	CheckSettingsDialog();
        	
        	Variables.globalSettings.ProjectSettings = new PROJECT_SETTINGS();
        	Settings.repo.SettingsTree.ProjectBudget.Select();
        	Variables.globalSettings.ProjectSettings.budgetProjects = Settings.repo.BudgetProjects.Checked;
        	if (Variables.globalSettings.ProjectSettings.budgetProjects == true)
        	{
        		Variables.globalSettings.ProjectSettings.budgetPeriodFrequency = (BUDGET_FREQUENCY)Settings.repo.BudgetProjectFrequency.SelectedItemIndex;
        	}
        	
        	Settings.repo.SettingsTree.ProjectAllocation.Select();
        	// Payroll transactions
        	if (Settings.repo.ProjAlloPayrollByAmount.Checked)
        	{
        		Variables.globalSettings.ProjectSettings.payrollAllocationMethod = ALLOCATE_PAYROLL.ALLOCATE_AMOUNT;
        	}
        	else if (Settings.repo.ProjAlloPayrollByPercent.Checked)
        	{
        		Variables.globalSettings.ProjectSettings.payrollAllocationMethod = ALLOCATE_PAYROLL.ALLOCATE_PERCENT;        		
        	}
        	else
        	{
        		Variables.globalSettings.ProjectSettings.payrollAllocationMethod = ALLOCATE_PAYROLL.ALLOCATE_HOURS;
        	}
        	// Other transactions
        	if (Settings.repo.ProjAlloOtherAmount.Checked)
        	{
        		Variables.globalSettings.ProjectSettings.otherAllocationMethod = ALLOCATE_TRANSACTIONS.ALLOCATE_TRANS_AMOUNT;
        	}
        	else
        	{
        		Variables.globalSettings.ProjectSettings.otherAllocationMethod = ALLOCATE_TRANSACTIONS.ALLOCATE_TRANS_PERCENT;
        	}
        	Variables.globalSettings.ProjectSettings.warnIfAllocationIsNotComplete = Settings.repo.WarnIfAllocationIsNotComplete.Checked;
        	Variables.globalSettings.ProjectSettings.allowAccessToAllocateFieldUsingTab = Settings.repo.AllowAccessToAllocateFieldUsingTab.Checked;
        	
        	if (bCloseWin)
        	{
        		Settings.repo.Cancel.Click();
        	}
        }
                                              
        
        // Set Information
        
        public static void _SA_SetToGenericValues()
        {
        	Variables.globalSettings = new SETTINGS();
        	Settings._SA_GetCompanyInformation();
        	Variables.globalSettings.CompanySettings.SystemSettings = new SYSTEM(false, null, null, true, false, null, true, true, "1", false);
        	Variables.globalSettings.CompanySettings.FeatureSettings = new FEATURES(false, false, false, false, true, true, false);
        	Variables.globalSettings.CompanySettings.BackupSettings = new BACKUP(true, "Daily", true);
        	Variables.globalSettings.CompanySettings.DateSettings = new DATE_FORMAT("MM dd yyyy", "/", "MMM dd, yyyy", DATE_SHORT_LONG.DATE_SHORT, DATE_SHORT_LONG.DATE_SHORT, DATE_WEEK.WEEK_SUNDAY);
        	Variables.globalSettings.CompanySettings.FormSettings = new FORMS(null, null, null, null, null, null, null, true, true, true, true, true, true, true, true, true, false, false, false, false, false, false, false, false, false, true, true, true, true, true, true, true, true, false, false, false, false, false, false, false, false, false, false, false);    // form numbers should not be set to 1, only works for first testcase and new company
        	List <EMAIL_MESSAGES> emailList = new List<EMAIL_MESSAGES>();
        	emailList.Add(new EMAIL_MESSAGES(EMAIL_FORM_TYPE.FORM_INVOICES, "If you are unable to view the attached invoice, please "));
            emailList.Add(new EMAIL_MESSAGES(EMAIL_FORM_TYPE.FORM_PURCHASE_ORDERS, "If you are unable to view the attached purchase order, "));
            emailList.Add(new EMAIL_MESSAGES(EMAIL_FORM_TYPE.FORM_SALES_ORDERS, "If you are unable to view the attached sales order "));
            emailList.Add(new EMAIL_MESSAGES(EMAIL_FORM_TYPE.FORM_SALES_QUOTES, "If you are unable to view the attached sales quote, "));
            emailList.Add(new EMAIL_MESSAGES(EMAIL_FORM_TYPE.FORM_RECEIPTS, "If you are unable to view the attached receipt, please "));
            emailList.Add(new EMAIL_MESSAGES(EMAIL_FORM_TYPE.FORM_STATEMENTS, "If you are unable to view the attached statement, "));
            emailList.Add(new EMAIL_MESSAGES(EMAIL_FORM_TYPE.FORM_PURCHASE_QUOTE_CONFIRMATIONS, "Your quote has been received. Thank you."));
            emailList.Add(new EMAIL_MESSAGES(EMAIL_FORM_TYPE.FORM_PURCHASE_INVOICE_CONFIRMATIONS, "Your invoice has been received. Thank you."));
            emailList.Add(new EMAIL_MESSAGES(EMAIL_FORM_TYPE.FORM_DIRECT_DEPOSIT_PAYMENT_STUB, "If you are unable to view the attached direct deposit "));
        	Variables.globalSettings.CompanySettings.EmailSettings = new EMAIL(emailList, EMAIL_ATTACH.EMAIL_PDF);
            Variables.globalSettings.CompanySettings.additionalInformationDate = "Additional Date";
            Variables.globalSettings.CompanySettings.additionalInformationField = "Additional Field";
            Variables.globalSettings.CompanySettings.CurrencySettings = new CURRENCY(false, new CURRENCY_DATA(), new List<CURRENCY_DATA>());
            Variables.globalSettings.GeneralSettings = new GENERAL_SETTINGS(false, BUDGET_FREQUENCY.BUDGET_ANNUAL, new GENERAL_NUMBERING(true, true, "4", "1000", "1999", "2000", "2999", "3000", "3999", "4000", "4999", "5000", "5999"), false, new List<DEPT_ACCT>{}, new ADDITIONAL_FIELD_NAMES(), new GL_ACCOUNT(), false);
            Variables.globalSettings.PayableSettings = new PAYABLE_SETTINGS(null, null, null, "30", "60", "90", false, false, new GL_ACCOUNT(), new ADDITIONAL_FIELD_NAMES(), new GL_ACCOUNT(), new GL_ACCOUNT(), new GL_ACCOUNT(), new GL_ACCOUNT(), new GL_ACCOUNT(), new List<CURRENCY_ACCOUNT> {});
            Variables.globalSettings.ReceivableSettings = new RECEIVABLE_SETTINGS(null, null, null, "30", "60", "90", false, "0.0", "90", new TAX_CODE(null, "No Tax", TAX_USED_IN.TAX_ALL_JOURNALS, null), false, "31", null, null, null, false, true, null, null, null, new ADDITIONAL_FIELD_NAMES(), new GL_ACCOUNT(), new GL_ACCOUNT(), new GL_ACCOUNT(), new GL_ACCOUNT(), new GL_ACCOUNT(), new List<CURRENCY_ACCOUNT> {});
            
             // needs to be done?
            //Variables.GlobalSettings.ReceivableSettings.arAcct = {NULL, "12000 Accounts Receivable", NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL}
            //Variables.GlobalSettings.ReceivableSettings.freightAcct = {NULL, "44100 Freight Revenue", NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL}
            //Variables.GlobalSettings.ReceivableSettings.earlyPayDiscountAcct = {NULL, "44200 Sales Discounts", NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL}
            //Variables.GlobalSettings.ReceivableSettings.depositsAcct = {NULL, "24600 Prepaid Sales/Deposits", NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL}
            //Variables.GlobalSettings.ReceivableSettings.CurrencyAccounts = {{NULL, {NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL}}}
			
            List<PRICE_LIST> priceList = new List<PRICE_LIST>();
            priceList.Add(new PRICE_LIST("Regular", true));
            priceList.Add(new PRICE_LIST("Preferred", true));
            priceList.Add(new PRICE_LIST("Web Price", true));
            Variables.globalSettings.InventorySettings = new INVENTORY_SETTINGS(COSTING_METHOD.AVERAGE_COST, false, PROFIT_METHOD.MARGIN, SORT_METHOD.SORT_NUMBER, PRICE_METHOD.CALCULATED, false, priceList, false, new List<LOCATION>{}, false, new List<CATEGORY>{}, new ADDITIONAL_FIELD_NAMES(), new GL_ACCOUNT(), new GL_ACCOUNT());
            Variables.globalSettings.ProjectSettings = new PROJECT_SETTINGS(false, BUDGET_FREQUENCY.BUDGET_ANNUAL, ALLOCATE_PAYROLL.ALLOCATE_HOURS, ALLOCATE_TRANSACTIONS.ALLOCATE_TRANS_PERCENT, false, false, "Project", new ADDITIONAL_FIELD_NAMES());
			
            List<PAYROLL_INCOME> payrollIncomeList = new List<PAYROLL_INCOME>();
            payrollIncomeList.Add(new PAYROLL_INCOME("Salary", INCOME_TYPE.INCOME_TYPE_INCOME, null));
            payrollIncomeList.Add(new PAYROLL_INCOME("Commission", INCOME_TYPE.INCOME_TYPE_INCOME, null));
            payrollIncomeList.Add(new PAYROLL_INCOME("Income 3", INCOME_TYPE.INCOME_TYPE_INCOME, null));
            payrollIncomeList.Add(new PAYROLL_INCOME("Income 4", INCOME_TYPE.INCOME_TYPE_INCOME, null));
            payrollIncomeList.Add(new PAYROLL_INCOME("Income 5", INCOME_TYPE.INCOME_TYPE_INCOME, null));
            payrollIncomeList.Add(new PAYROLL_INCOME("Income 6", INCOME_TYPE.INCOME_TYPE_INCOME, null));
            payrollIncomeList.Add(new PAYROLL_INCOME("Income 7", INCOME_TYPE.INCOME_TYPE_INCOME, null));
            payrollIncomeList.Add(new PAYROLL_INCOME("Income 8", INCOME_TYPE.INCOME_TYPE_INCOME, null));
            payrollIncomeList.Add(new PAYROLL_INCOME("Income 9", INCOME_TYPE.INCOME_TYPE_INCOME, null));
            payrollIncomeList.Add(new PAYROLL_INCOME("Income 10", INCOME_TYPE.INCOME_TYPE_INCOME, null));
            payrollIncomeList.Add(new PAYROLL_INCOME("Income 11", INCOME_TYPE.INCOME_TYPE_INCOME, null));
            payrollIncomeList.Add(new PAYROLL_INCOME("Income 12", INCOME_TYPE.INCOME_TYPE_INCOME, null));
            payrollIncomeList.Add(new PAYROLL_INCOME("Income 13", INCOME_TYPE.INCOME_TYPE_INCOME, null));
            payrollIncomeList.Add(new PAYROLL_INCOME("Income 14", INCOME_TYPE.INCOME_TYPE_INCOME, null));
            payrollIncomeList.Add(new PAYROLL_INCOME("Income 15", INCOME_TYPE.INCOME_TYPE_INCOME, null));
            payrollIncomeList.Add(new PAYROLL_INCOME("Income 16", INCOME_TYPE.INCOME_TYPE_INCOME, null));
            payrollIncomeList.Add(new PAYROLL_INCOME("Income 17", INCOME_TYPE.INCOME_TYPE_INCOME, null));
            payrollIncomeList.Add(new PAYROLL_INCOME("Income 18", INCOME_TYPE.INCOME_TYPE_INCOME, null));
            payrollIncomeList.Add(new PAYROLL_INCOME("Income 19", INCOME_TYPE.INCOME_TYPE_INCOME, null));
            payrollIncomeList.Add(new PAYROLL_INCOME("Income 20", INCOME_TYPE.INCOME_TYPE_INCOME, null));

            INCOME_NAME[] additionalIncomeArray = { new INCOME_NAME(), new INCOME_NAME(), new INCOME_NAME(), new INCOME_NAME(), new INCOME_NAME(), new INCOME_NAME(), new INCOME_NAME(), new INCOME_NAME(), new INCOME_NAME(), new INCOME_NAME(), new INCOME_NAME(), new INCOME_NAME(), new INCOME_NAME(), new INCOME_NAME(), new INCOME_NAME(), new INCOME_NAME(), new INCOME_NAME(), new INCOME_NAME(), new INCOME_NAME(), new INCOME_NAME()};
            DEDUCTION_NAME[] addtionalDedArray = { new DEDUCTION_NAME(), new DEDUCTION_NAME(), new DEDUCTION_NAME(), new DEDUCTION_NAME(), new DEDUCTION_NAME(), new DEDUCTION_NAME(), new DEDUCTION_NAME(), new DEDUCTION_NAME(), new DEDUCTION_NAME(), new DEDUCTION_NAME(), new DEDUCTION_NAME(), new DEDUCTION_NAME(), new DEDUCTION_NAME(), new DEDUCTION_NAME(), new DEDUCTION_NAME(), new DEDUCTION_NAME(), new DEDUCTION_NAME(), new DEDUCTION_NAME(), new DEDUCTION_NAME(), new DEDUCTION_NAME()};
            
            //Variables.GlobalSettings.PayrollSettings.IncomeSettings = new PAYROLL_INCOME_SETTINGS(payrollIncomeList, false);
            //Variables.GlobalSettings.PayrollSettings.TaxSettings = {"1.4", "0.0", "0.0", "0.0", NULL, NULL, NULL}
            //Variables.GlobalSettings.PayrollSettings.EntitlementSettings = {"8.00", {{"Sick Days" , "4.61" , "15.00 ", FALSE}, {"Comp. Days ", "0.00 ", "10.00 ", FALSE}, {"Special Days ", "0.00 ", "3.00 ", FALSE}, {"undefined ", "0.00 ", "0.00 ", FALSE}, {"undefined ", "0.00 ", "0.00 ", FALSE}}}
            //Variables.GlobalSettings.PayrollSettings.Remittance = {}
            //Variables.GlobalSettings.PayrollSettings.AdditionalPayrollSettings = {"Tax (Que)", "WCB", {"Emergency Contact", "Contact Phone #", "Other Information", NULL, NULL}, {"RRSP", "Un. Pension", "Medical", "Donation", NULL}, {"Sick Days", "Comp. Days", "Special Days", NULL, NULL}}            

            Variables.globalSettings.PayrollSettings = new PAYROLL_SETTINGS(new PAYROLL_INCOME_SETTINGS(payrollIncomeList, false), new PAYROLL_TAXES_SETTINGS(), new List<PAYROLL_DEDUCTION> { }, new PAYROLL_ENTITLEMENT_SETTINGS(), new List<PAYROLL_REMITTANCE> { }, new List<PAYROLL_JOB> { }, new PAYROLL_ADDITIONAL(), new INCOME_SPECIFIC_ACCOUNTS(), additionalIncomeArray, addtionalDedArray, new TAXES_LINKED_ACCOUNTS(), new USER_DEFINED_ACCOUNTS(), true);
            Variables.globalSettings.PayrollSettings.UsePayrollExpenseGroups = true;
        }
        
        // CompanySettings method
        public static void _SA_Set_AllCompanySettings()
        {
            _SA_Set_AllCompanySettings(true);
        }
        public static void _SA_Set_AllCompanySettings(bool bClickOK)
        {                     
            Settings._SA_SetCompanyInformation(false);
            Settings._SA_SetCompanySystemSettings(false);
            Settings._SA_SetCompanyBackupSettings(false);
            Settings._SA_SetCompanyFeatureSettings(false);
            Settings._SA_SetCompanyCreditCardSettings(false);
            Settings._SA_SetCompanySalesTaxSettings(false);
            Settings._SA_SetCompanyCurrencySettings(false);
            Settings._SA_SetCompanyFormsSettings(false);
            Settings._SA_SetCompanyDateSettings(false);
            Settings._SA_SetCompanyShipperSettings(false);
            Settings._SA_SetCompanyNameSettings(false);

            if (bClickOK)
            {
            	repo.OK.Click();
            }
        }
        
        
        public static void _SA_SetCompanyInformation()
        {
            _SA_SetCompanyInformation(true);
        }
        public static void _SA_SetCompanyInformation(bool bClickOK)
        {
        	CheckSettingsDialog();
        	
        	Settings.repo.SettingsTree.CompInformation.Select();
        	if (Functions.GoodData(Variables.globalSettings.CompanyInformation.companyName))
        	{
        		Settings.repo.CompanyName.TextValue = Variables.globalSettings.CompanyInformation.companyName;
        	}
        	if (Functions.GoodData(Variables.globalSettings.CompanyInformation.Address.street1))
        	{
        		Settings.repo.Address1.TextValue = Variables.globalSettings.CompanyInformation.Address.street1;
        	}
        	if (Functions.GoodData(Variables.globalSettings.CompanyInformation.Address.street2))
        	{
        		Settings.repo.Address2.TextValue = Variables.globalSettings.CompanyInformation.Address.street2;
        	}
        	if (Functions.GoodData(Variables.globalSettings.CompanyInformation.Address.city))
        	{
        		Settings.repo.City.TextValue = Variables.globalSettings.CompanyInformation.Address.city;
        	}
        	if (Functions.GoodData(Variables.globalSettings.CompanyInformation.Address.provinceCode))
        	{        		        	
        		Settings.repo.ProvinceCombo.Select(Variables.globalSettings.CompanyInformation.Address.provinceCode);
        	}
        	if (Functions.GoodData(Variables.globalSettings.CompanyInformation.Address.province))
        	{
        		Settings.repo.Province.TextValue = Variables.globalSettings.CompanyInformation.Address.province;
        	}
        	if (Functions.GoodData(Variables.globalSettings.CompanyInformation.Address.postalCode))
        	{
        		Settings.repo.PostalCode.TextValue = Variables.globalSettings.CompanyInformation.Address.postalCode;
        	}
        	if (Functions.GoodData(Variables.globalSettings.CompanyInformation.Address.country))
        	{
        		Settings.repo.Country.TextValue = Variables.globalSettings.CompanyInformation.Address.country;
        	}
        	if (Functions.GoodData(Variables.globalSettings.CompanyInformation.Address.phone1))
        	{
        		Settings.repo.Phone1.TextValue = Variables.globalSettings.CompanyInformation.Address.phone1;
        	}
        	if (Functions.GoodData(Variables.globalSettings.CompanyInformation.Address.phone2))
        	{
        		Settings.repo.Phone2.TextValue = Variables.globalSettings.CompanyInformation.Address.phone2;
        	}
        	if (Functions.GoodData(Variables.globalSettings.CompanyInformation.Address.fax))
        	{
        		Settings.repo.Fax.TextValue = Variables.globalSettings.CompanyInformation.Address.fax;
        	}
        	if (Functions.GoodData(Variables.globalSettings.CompanyInformation.businessNum))
        	{
        		Settings.repo.BusinessNumber.TextValue = Variables.globalSettings.CompanyInformation.businessNum;
        	}
        	if (Functions.GoodData(Variables.globalSettings.CompanyInformation.companyType))
        	{
        		Settings.repo.BusinessTypeCombo.Select(Variables.globalSettings.CompanyInformation.companyType);
        	}
        	if (Settings.repo.FiscalStart.Enabled && Functions.GoodData(Variables.globalSettings.CompanyInformation.fiscalStart))
        	{
        		Settings.repo.FiscalStart.TextValue = Variables.globalSettings.CompanyInformation.fiscalStart;        		
        	}
        	if (Settings.repo.FiscalEnd.Enabled && Functions.GoodData(Variables.globalSettings.CompanyInformation.fiscalEnd))
        	{
        		Settings.repo.FiscalEnd.TextValue = Variables.globalSettings.CompanyInformation.fiscalEnd;
        	}        	
        	if (Settings.repo.EarliestTransaction.Enabled && Functions.GoodData(Variables.globalSettings.CompanyInformation.earliestTransaction))
        	{
        		Settings.repo.EarliestTransaction.TextValue = Variables.globalSettings.CompanyInformation.earliestTransaction;
        	}
        	if (Functions.GoodData(Variables.globalSettings.CompanySettings.LogoLocation))
        	{
        		Settings.repo.SettingsTree.CompanyLogo.Select();
        		Settings.repo.CompanyLogoLoc.TextValue = Variables.globalSettings.CompanySettings.LogoLocation;
        	}
        	
        	if (bClickOK)
        	{
        		Settings.repo.OK.Click();
        	}
        }
        
        public static void _SA_SetCompanySystemSettings()
        {
            _SA_SetCompanySystemSettings(true);
        }
        public static void _SA_SetCompanySystemSettings(bool bClickOK)
        {            
        	CheckSettingsDialog();
        	
        	Settings.repo.SettingsTree.CompanySystem.Select();
        	Settings.repo.UseCashBasisAccounting.SetState(Variables.globalSettings.CompanySettings.SystemSettings.useCashBasisAccounting);
        	if (Variables.globalSettings.CompanySettings.SystemSettings.useCashBasisAccounting == true)
        	{
        		Settings.repo.CashAccountingDate.TextValue = Variables.globalSettings.CompanySettings.SystemSettings.cashBasisDate;
        	}
        	Settings.repo.UseChequeNoAsTheSourceCode.SetState(Variables.globalSettings.CompanySettings.SystemSettings.useChequeNo);
        	if (Variables.globalSettings.CompanySettings.SystemSettings.doNotAllowTransactionsDatedBefore == true)
        	{
        		Settings.repo.DoNotAllowTransactionsDatedBefore.Check();
        		Settings.repo.LockingDate.TextValue = Variables.globalSettings.CompanySettings.SystemSettings.lockingDate;
        	}	
        	Settings.repo.AllowFutureTransactions.SetState(Variables.globalSettings.CompanySettings.SystemSettings.allowTransactionsInTheFuture);
        	
        	if (Variables.globalSettings.CompanySettings.SystemSettings.allowTransactionsInTheFuture == true)
        	{
        		Settings.repo.WarnIfTransactionsAreInTheFuture.SetState(Variables.globalSettings.CompanySettings.SystemSettings.warnIfTransactionsAre);
        	}
        	if (Variables.globalSettings.CompanySettings.SystemSettings.warnIfTransactionsAre == true)
        	{
        		Settings.repo.DaysInTheFuture.TextValue = Variables.globalSettings.CompanySettings.SystemSettings.daysInTheFuture;                                          
        	}
        	
        	if (Validate.Exists(repo.WarnIfAccountsAreNotBalancedInfo.ToString(), 1000, "'{0}'", false))
        	{
        		Settings.repo.WarnIfAccountsAreNotBalanced.SetState(Variables.globalSettings.CompanySettings.SystemSettings.warnIfAccountsAreNotBalanced);
        	}
        	
        	if (bClickOK)
        	{
        		Settings.repo.OK.Click();
        	}
        }
        
        public static void _SA_SetCompanyBackupSettings()
        {
            _SA_SetCompanyBackupSettings(true);
        }
        public static void _SA_SetCompanyBackupSettings(bool bClickOK)
        {           
        	CheckSettingsDialog();
        	
        	Settings.repo.SettingsTree.CompanyBackup.Select();
        	
        	Settings.repo.DisplayBackupReminderWhenSessionDateChange.SetState(Variables.globalSettings.CompanySettings.BackupSettings.displayReminderOnSession);
        	if (Variables.globalSettings.CompanySettings.BackupSettings.displayReminderOnSession == true)
        	{
        		Settings.repo.BackupReminderFrequency.Select(Variables.globalSettings.CompanySettings.BackupSettings.reminderFrequency);
        	}
        	Settings.repo.DisplayABackupReminderAtClosing.SetState(Variables.globalSettings.CompanySettings.BackupSettings.displayReminderWhenClosing);
        	
        	if (bClickOK)
        	{
        		Settings.repo.OK.Click();
        	}
        }
        
        public static void _SA_SetCompanyFeatureSettings()
        {
            _SA_SetCompanyFeatureSettings(true);
        }
        public static void _SA_SetCompanyFeatureSettings(bool bClickOK)
        {
        	CheckSettingsDialog();
        	
        	Settings.repo.SettingsTree.CompanyFeatures.Select();
        	Settings.repo.OrdersForVendors.SetState(Variables.globalSettings.CompanySettings.FeatureSettings.ordersForVendors);
        	Settings.repo.QuotesForVendors.SetState(Variables.globalSettings.CompanySettings.FeatureSettings.quotesForVendors);
        	Settings.repo.OrdersForCustomers.SetState(Variables.globalSettings.CompanySettings.FeatureSettings.ordersForCustomers);
        	Settings.repo.QuotesForCustomers.SetState(Variables.globalSettings.CompanySettings.FeatureSettings.quotesForCustomers);
        	Settings.repo.Project.SetState(Variables.globalSettings.CompanySettings.FeatureSettings.projectCheckBox);
        	
        	if (bClickOK)
        	{
        		Settings.repo.OK.Click();
        	}
        }
        
        public static void _SA_SetCompanyCreditCardSettings()
        {
            _SA_SetCompanyCreditCardSettings(true);
        }
        public static void _SA_SetCompanyCreditCardSettings(bool bClickOK)
        {
        	CheckSettingsDialog();
        	
        	// Credit cards used
        	Settings.repo.SettingsTree.CompanyCreditCardUsed.Select();
        	
        	if (Functions.GoodData(Variables.globalSettings.CompanySettings.CreditCardSettings.CardsUsed))
        	{
        		for (int x =0; x < Variables.globalSettings.CompanySettings.CreditCardSettings.CardsUsed.Count; x++)
        		{        			
        			Settings.repo.CCUsedTable.SetToLine(x);
        			Settings.repo.CCUsedTable.SetText(Variables.globalSettings.CompanySettings.CreditCardSettings.CardsUsed[x].CardName);
        			Settings.repo.CCUsedTable.MoveRight();
        			Settings.repo.CCUsedTable.SetText(Variables.globalSettings.CompanySettings.CreditCardSettings.CardsUsed[x].PayableAccount.acctNumber);
        			Settings.repo.CCUsedTable.MoveRight();
        			Settings.repo.CCUsedTable.SetText(Variables.globalSettings.CompanySettings.CreditCardSettings.CardsUsed[x].ExpenseAccount.acctNumber);
        			Settings.repo.CCUsedTable.MoveRight();
        		}
        	}
        	
        	// Credit cards accepted
        	Settings.repo.SettingsTree.CompanyCreditCardAccepted.Select();
        	
        	if (Functions.GoodData(Variables.globalSettings.CompanySettings.CreditCardSettings.CardsAccepted))
        	{
        		for (int x=0; x < Variables.globalSettings.CompanySettings.CreditCardSettings.CardsAccepted.Count; x++)
        		{
        			Settings.repo.CCAcceptedTable.SetToLine(x);
        			Settings.repo.CCAcceptedTable.SetText(Variables.globalSettings.CompanySettings.CreditCardSettings.CardsAccepted[x].CardName);
        			Settings.repo.CCAcceptedTable.MoveRight();
        			Settings.repo.CCAcceptedTable.MoveRight();
        			Settings.repo.CCAcceptedTable.SetText(Variables.globalSettings.CompanySettings.CreditCardSettings.CardsAccepted[x].AssetAccount.acctNumber);
        			Settings.repo.CCAcceptedTable.MoveRight();
        			Settings.repo.CCAcceptedTable.SetText(Variables.globalSettings.CompanySettings.CreditCardSettings.CardsAccepted[x].Discount);
        			Settings.repo.CCAcceptedTable.MoveRight();
        			Settings.repo.CCAcceptedTable.SetText(Variables.globalSettings.CompanySettings.CreditCardSettings.CardsAccepted[x].ExpenseAccount.acctNumber);
        			Settings.repo.CCAcceptedTable.MoveRight();
        		}
        	}
        	    
        	if (bClickOK)
        	{
        		Settings.repo.OK.Click();
        	}
        }
        
        public static void _SA_SetCompanySalesTaxSettings()
        {
            _SA_SetCompanySalesTaxSettings(true);
        }
        public static void _SA_SetCompanySalesTaxSettings(bool bClickOK)
        {
        	CheckSettingsDialog();
        	
            Settings.repo.SettingsTree.CompanySalesTaxesTaxes.Select();            
            //for each TaxRecord in GlobalSettings.CompanySettings.TaxSettings
            Settings._SA_SetupTax(Variables.globalSettings.CompanySettings.TaxSettings, false);

            Settings.repo.SettingsTree.CompanySalesTaxesTaxCodes.Select();
            // for each TaxCodeRecord in GlobalSettings.CompanySettings.TaxCodes
            Settings._SA_SetupTaxCode(Variables.globalSettings.CompanySettings.TaxCodes, false);
            
            if (bClickOK)
            {
            	Settings.repo.OK.Click();
            }
        }
        
        public static void _SA_SetupTax(List<TAX> Taxes)
        {
            _SA_SetupTax(Taxes, true);
        }
        public static void _SA_SetupTax(List<TAX> Taxes, bool bSave)
		{
        	CheckTaxesDialog();
        	
			// Move to the first column of the current row
			Settings.repo.SalesTaxContainer.ClickFirstCell();
			
			for (int x=0; x < Taxes.Count; x++)
			{
				Settings.repo.SalesTaxContainer.SetText(Taxes[x].taxName);
				Settings.repo.SalesTaxContainer.MoveRight();
				
				List <List <string>> lsTaxContents = Settings.repo.SalesTaxContainer.GetContents();
				
				if (Functions.GoodData(Taxes[x].taxID))
				{
					Settings.repo.SalesTaxContainer.SetText(Taxes[x].taxID);
				}
				
				// Exempt column
				Settings.repo.SalesTaxContainer.MoveRight();
				
				if (Taxes[x].exempt == true)
				{
					if (lsTaxContents[x][2] == "No")
					{
						Settings.repo.SalesTaxContainer.Toggle();
					}
				}
				else
				{
					if (lsTaxContents[x][2] == "Yes")
					{
						Settings.repo.SalesTaxContainer.Toggle();
					}
				}
				// Taxable column
				Settings.repo.SalesTaxContainer.MoveRight();
				
				if (Taxes[x].taxable == true)
				{
					if (lsTaxContents[x][3] == "No")
					{
						Settings.repo.SalesTaxContainer.Toggle();
					}
				}
				else
				{
					if (lsTaxContents[x][3] == "Yes")
					{
						Settings.repo.SalesTaxContainer.Toggle();
					}
				}
				// Open Taxable tax list window
				Settings.repo.SalesTaxContainer.PressKeys("{Enter}");
				
				if (TaxableTaxList.repo.TaxableTaxListContainerInfo.Exists(Variables.iExistWaitTime))
				{
					TaxableTaxList.repo.TaxableTaxListContainer.ClickFirstCell();
					TaxableTaxList.repo.TaxableTaxListContainer.MoveRight();					
					
					foreach (List <string> lsRow in TaxableTaxList.repo.TaxableTaxListContainer.GetContents())
					{
						if (Taxes[x].TaxAuthoritiesToBeCharged.Count != 0 && lsRow[0].Length != 0)
						{
							if (Functions.ListFind(Taxes[x].TaxAuthoritiesToBeCharged, lsRow[0]) != -1)
							    {
									if (lsRow[1] == "false")
									{
										TaxableTaxList.repo.TaxableTaxListContainer.Toggle();
									}
							    }
						}
						TaxableTaxList.repo.TaxableTaxListContainer.MoveRight();
					}
					TaxableTaxList.repo.OK.Click();
				}
				else
				{
					// Simply message, click ok
				}
				
				// Account to Track Purchases column
				Settings.repo.SalesTaxContainer.MoveRight();
				
				if (Taxes[x].exempt == false)
				{
					if (Functions.GoodData(Taxes[x].acctTrackPurchases.acctNumber))
					{
						Settings.repo.SalesTaxContainer.SetText(Taxes[x].acctTrackPurchases.acctNumber);
					}
					Settings.repo.SalesTaxContainer.MoveRight();
				}
				
				Settings.repo.SalesTaxContainer.SetText(Taxes[x].acctTrackSales.acctNumber);
				
				// Report on Taxes column
				Settings.repo.SalesTaxContainer.MoveRight();
				if (Taxes[x].reportOnTaxes == true)
				{
					if (lsTaxContents[x][6] == "No")
					{
						Settings.repo.SalesTaxContainer.Toggle();
					}				
				}
				else
				{
					if (lsTaxContents[x][7] == "Yes")
					{
						Settings.repo.SalesTaxContainer.Toggle();
					}
				}
				Settings.repo.SalesTaxContainer.MoveRight();
			}
			
			if (bSave)
			{
				Settings.repo.OK.Click();
			}			
		}
        
        public static void _SA_SetupTaxCode(List<TAX_CODE> TaxCodes)
        {
            _SA_SetupTaxCode(TaxCodes, true);
        }
        public static void _SA_SetupTaxCode(List<TAX_CODE> TaxCodes, bool bSave)
		{
        	CheckTaxCodesDialog();
        	
        	repo.SalesTaxCodeContainer.ClickFirstCell();
        	// bypass the first non-modifiable tax code (i.e. No Tax)
        	repo.SalesTaxCodeContainer.MoveDown();
        	        	
        	for (int x=0; x < TaxCodes.Count; x++)
        	{
        		if (TaxCodes[x].code.Trim() != "")
        		{
        			Settings.repo.SalesTaxCodeContainer.SetText(TaxCodes[x].code);
        			
        			// tax details dialog
        			if (Functions.GoodData(TaxCodes[x].TaxDetails))
        			{
        				Settings.repo.SalesTaxCodeContainer.SetText("{Enter}");
        				
        				TaxCodeDetails.repo.TaxCodeDetailsContainer.ClickFirstCell();
        				
        				for (int y=0; y < TaxCodes[x].TaxDetails.Count; y++)
        				{
        					TaxCodeDetails.repo.TaxCodeDetailsContainer.SetText(TaxCodes[x].TaxDetails[y].Tax.taxName);
        					TaxCodeDetails.repo.TaxCodeDetailsContainer.MoveRight();
        					
							List <List <string>> lsTaxDetails = TaxCodeDetails.repo.TaxCodeDetailsContainer.GetContents();
							
							string sTaxStatus = null;
							switch (TaxCodes[x].TaxDetails[y].taxStatus)
							{
								case TAX_STATUS.TAX_STATUS_TAXABLE:
									sTaxStatus = "Taxable";
									break;
								case TAX_STATUS.TAX_STATUS_NON_TAXABLE:
									sTaxStatus = "Non-taxable";
									break;
								case TAX_STATUS.TAX_STATUS_EXEMPT:
									sTaxStatus = "Exempt";
									break;
							}
							TaxCodeDetails.repo.TaxCodeDetailsContainer.SetText(sTaxStatus);
							TaxCodeDetails.repo.TaxCodeDetailsContainer.MoveRight();
							
							if (TaxCodes[x].TaxDetails[y].taxStatus == TAX_STATUS.TAX_STATUS_TAXABLE)
							{
								TaxCodeDetails.repo.TaxCodeDetailsContainer.SetText(TaxCodes[x].TaxDetails[y].rate);
								
								TaxCodeDetails.repo.TaxCodeDetailsContainer.MoveRight();
								
								if (TaxCodes[x].TaxDetails[y].includedInPrice == true)
								{
									if (lsTaxDetails[y][3] == "No")
									{
										TaxCodeDetails.repo.TaxCodeDetailsContainer.Toggle();
									}
								}
								else
								{
									if (lsTaxDetails[y][3] == "Yes")
									{
										TaxCodeDetails.repo.TaxCodeDetailsContainer.Toggle();
									}
								}
								
								TaxCodeDetails.repo.TaxCodeDetailsContainer.MoveRight();
								if (TaxCodes[x].TaxDetails[y].isRefundable == true)
								{
									if (lsTaxDetails[y][4] == "No")
									{
										TaxCodeDetails.repo.TaxCodeDetailsContainer.Toggle();
									}
								}
								else
								{
									if (lsTaxDetails[y][4] == "Yes")
									{
										TaxCodeDetails.repo.TaxCodeDetailsContainer.Toggle();
									}
								}
							
								TaxCodeDetails.repo.TaxCodeDetailsContainer.MoveRight();
							}							
        				}
        			}
        			TaxCodeDetails.repo.OK.Click();
        			
        			// back to taxcode grid
        			repo.SalesTaxCodeContainer.MoveRight();
        			if (Functions.GoodData(TaxCodes[x].description))
        			{
        				repo.SalesTaxCodeContainer.SetText(TaxCodes[x].description);
        			}
        			
        			repo.SalesTaxCodeContainer.MoveRight(2);
        			
        			string sUsedIn;
        			switch(TaxCodes[x].useIn)
        			{
        				case TAX_USED_IN.TAX_ALL_JOURNALS:
        					sUsedIn = "All journals";
        					break;
        				case TAX_USED_IN.TAX_SALES:
        					sUsedIn = "Sales";
        					break;
        				case TAX_USED_IN.TAX_PURCHASES:
        					sUsedIn = "Purchases";
        					break;
        				default:
    					{
    						sUsedIn = "All journals";
    						break;
    					}
        			}
        			repo.SalesTaxCodeContainer.SetText(sUsedIn);
        			repo.SalesTaxCodeContainer.MoveRight();
        		}
        	}
        	
        	if (bSave)
        	{
        		repo.OK.Click();
        	}
		}
        
        public static void _SA_SetCompanyCurrencySettings()
        {
            _SA_SetCompanyCurrencySettings(true);
        }
        public static void _SA_SetCompanyCurrencySettings(bool bClickOK)
        {
        	CheckSettingsDialog();
        	
        	Settings.repo.SettingsTree.CompanyCurrency.Select();
        	// Check method does not enable exchange difference account combo box
        	if (Variables.globalSettings.CompanySettings.CurrencySettings.allowForeignCurrency == true && !Settings.repo.AllowForeignCurrency.Checked)
        	{
        		Settings.repo.AllowForeignCurrency.Click();	
        	}
        	

            // set home currency
            if (Functions.GoodData(Variables.globalSettings.CompanySettings.CurrencySettings.HomeCurrency.Currency))
            {
            	Settings.repo.HomeCurrency.Select(Variables.globalSettings.CompanySettings.CurrencySettings.HomeCurrency.Currency);
            }
            if (Functions.GoodData(Variables.globalSettings.CompanySettings.CurrencySettings.HomeCurrency.currencyCode))
            {
            	Settings.repo.CurrencyCode.TextValue = Variables.globalSettings.CompanySettings.CurrencySettings.HomeCurrency.currencyCode;
            }
            if (Functions.GoodData(Variables.globalSettings.CompanySettings.CurrencySettings.HomeCurrency.thousandsSeparator))
            {
            	Settings.repo.ThousandsSeparator.TextValue = Variables.globalSettings.CompanySettings.CurrencySettings.HomeCurrency.thousandsSeparator;
            }
            if (Functions.GoodData(Variables.globalSettings.CompanySettings.CurrencySettings.HomeCurrency.symbol))
            {
            	Settings.repo.Symbol.TextValue = Variables.globalSettings.CompanySettings.CurrencySettings.HomeCurrency.symbol;
            }
            if (Functions.GoodData(Variables.globalSettings.CompanySettings.CurrencySettings.HomeCurrency.decimalSeparator))
            {
            	Settings.repo.DecimalSeparator.TextValue = Variables.globalSettings.CompanySettings.CurrencySettings.HomeCurrency.decimalSeparator;
            }
            if (Functions.GoodData(Variables.globalSettings.CompanySettings.CurrencySettings.HomeCurrency.symbolPosition))
            {
            	Settings.repo.SymbolPosition.Items[(int)Variables.globalSettings.CompanySettings.CurrencySettings.HomeCurrency.symbolPosition].Select();
            }
            if (Functions.GoodData(Variables.globalSettings.CompanySettings.CurrencySettings.HomeCurrency.decimalPlaces))
            {
            	Settings.repo.DecimalPlaces.Items[(int)Variables.globalSettings.CompanySettings.CurrencySettings.HomeCurrency.decimalPlaces].Select();            	
            }
            if (Functions.GoodData(Variables.globalSettings.CompanySettings.CurrencySettings.HomeCurrency.denomination))
            {
            	Settings.repo.Denomination.Select(Variables.globalSettings.CompanySettings.CurrencySettings.HomeCurrency.denomination);
            }
            // set foreign currencies if any
            if (Variables.globalSettings.CompanySettings.CurrencySettings.allowForeignCurrency == true)
            {
            	// Pro, Premium, Quantum                       
            	Settings.repo.DifferencesAccount.Select(Variables.globalSettings.CompanySettings.CurrencySettings.HomeCurrency.roundingDifferencesAccount);
            	
            	// Premium and Quantum
            	if (repo.CurrencyContainerInfo.Exists(Variables.iExistWaitTime))
            	{
            		if (Functions.GoodData(Variables.globalSettings.CompanySettings.CurrencySettings.ForeignCurrencies))
            		{
            			while (Settings.repo.CurrencyContainer.GetContents().Count != 0)
            			{
            				// remove existing foreign currency row using Remove Line in Edit menu
            				repo.Self.PressKeys("{Alt}er");
            				
            				// confirm to remove existing foreign currency
            				SimplyMessage.repo.Yes.Click();            				
            			}
            			
            			// Number of currencies
            			for (int x=0; x < Variables.globalSettings.CompanySettings.CurrencySettings.ForeignCurrencies.Count; x++)
            			{
            				repo.CurrencyContainer.SetToLine(x);
            				
            				if (Functions.GoodData(Variables.globalSettings.CompanySettings.CurrencySettings.ForeignCurrencies[x].Currency))
            				{
            					repo.CurrencyContainer.SetText(Variables.globalSettings.CompanySettings.CurrencySettings.ForeignCurrencies[x].Currency);
            				}
            				repo.CurrencyContainer.MoveRight();
            				
            				if (Functions.GoodData(Variables.globalSettings.CompanySettings.CurrencySettings.ForeignCurrencies[x].currencyCode))
            				{
            					repo.CurrencyContainer.SetText(Variables.globalSettings.CompanySettings.CurrencySettings.ForeignCurrencies[x].currencyCode);
            				}
            				repo.CurrencyContainer.MoveRight();
            				
            				// add merchant account here
            				repo.CurrencyContainer.MoveRight();
            				
            				// add currency symbol here
            				repo.CurrencyContainer.MoveRight();
            					
            				if (Functions.GoodData(Variables.globalSettings.CompanySettings.CurrencySettings.ForeignCurrencies[x].symbolPosition))
            				{
            					string sSymbolPosition;
            					if (Variables.globalSettings.CompanySettings.CurrencySettings.ForeignCurrencies[x].symbolPosition == CURRENCY_SYMBOL.CURRENCY_LEADING)
            					{
            						sSymbolPosition = "Leading";
            					}
            					else
            					{
            						sSymbolPosition = "Trailing";
            					}
            					repo.CurrencyContainer.SetText(sSymbolPosition);
            				}
            				repo.CurrencyContainer.MoveRight();
            				
            				if (Functions.GoodData(Variables.globalSettings.CompanySettings.CurrencySettings.ForeignCurrencies[x].thousandsSeparator))
            				{
            					repo.CurrencyContainer.SetText(Variables.globalSettings.CompanySettings.CurrencySettings.ForeignCurrencies[x].thousandsSeparator);
            				}
            				repo.CurrencyContainer.MoveRight();
            				
            				if (Functions.GoodData(Variables.globalSettings.CompanySettings.CurrencySettings.ForeignCurrencies[x].decimalSeparator))
            				{
            					repo.CurrencyContainer.SetText(Variables.globalSettings.CompanySettings.CurrencySettings.ForeignCurrencies[x].decimalSeparator);
            				}
            				repo.CurrencyContainer.MoveRight();
            				
            				if (Functions.GoodData(Variables.globalSettings.CompanySettings.CurrencySettings.ForeignCurrencies[x].decimalPlaces))
            				{
            					string sDecimal;
            					switch (Variables.globalSettings.CompanySettings.CurrencySettings.ForeignCurrencies[x].decimalPlaces)
            					{
            						case CURRENCY_DECIMAL.CURRENCY_DECIMAL_0:
            							sDecimal = "0";
            							break;
            						case CURRENCY_DECIMAL.CURRENCY_DECIMAL_1:
            							sDecimal = "1";
            							break;
            						case CURRENCY_DECIMAL.CURRENCY_DECIMAL_2:
            							sDecimal = "2";
            							break;
            						default:
            							sDecimal = "0";
            							break;
            					}
            					repo.CurrencyContainer.SetText(sDecimal);
            				}
            				repo.CurrencyContainer.MoveRight();
            				
            				if (Functions.GoodData(Variables.globalSettings.CompanySettings.CurrencySettings.ForeignCurrencies[x].denomination))
            				{
            					repo.CurrencyContainer.SetText(Variables.globalSettings.CompanySettings.CurrencySettings.ForeignCurrencies[x].denomination);
            				}
            				repo.CurrencyContainer.MoveRight();
            				

            			}
            		}
            	}
            	else	// Pro
            	{
            		// Set bPro
            		bPro = true;
            		
            		if (Functions.GoodData(Variables.globalSettings.CompanySettings.CurrencySettings.ForeignCurrencies[0].Currency))
            		{
            			repo.SingleForeignCurrency.Select(Variables.globalSettings.CompanySettings.CurrencySettings.ForeignCurrencies[0].Currency);
            		}
            		
            		if (Functions.GoodData(Variables.globalSettings.CompanySettings.CurrencySettings.ForeignCurrencies[0].currencyCode))
            		{
            			repo.SingleCurrencyCode.TextValue = Variables.globalSettings.CompanySettings.CurrencySettings.ForeignCurrencies[0].currencyCode;
            		}
            		
            		if (Functions.GoodData(Variables.globalSettings.CompanySettings.CurrencySettings.ForeignCurrencies[0].thousandsSeparator))
            		{
            			repo.SingleThousandsSeparator.TextValue = Variables.globalSettings.CompanySettings.CurrencySettings.ForeignCurrencies[0].thousandsSeparator;
            		}
            		
            		if (Functions.GoodData(Variables.globalSettings.CompanySettings.CurrencySettings.ForeignCurrencies[0].symbol))
            		{
            			repo.SingleSymbol.TextValue = Variables.globalSettings.CompanySettings.CurrencySettings.ForeignCurrencies[0].symbol;
            		}
            		
            		if (Functions.GoodData(Variables.globalSettings.CompanySettings.CurrencySettings.ForeignCurrencies[0].decimalSeparator))
            		{
            			repo.SingleDecimalSeparator.TextValue = Variables.globalSettings.CompanySettings.CurrencySettings.ForeignCurrencies[0].decimalSeparator;
            		}
            		
            		if (Functions.GoodData(Variables.globalSettings.CompanySettings.CurrencySettings.ForeignCurrencies[0].symbolPosition))
    				{
    					string sSingleSymbolPos;
    					if (Variables.globalSettings.CompanySettings.CurrencySettings.ForeignCurrencies[0].symbolPosition == CURRENCY_SYMBOL.CURRENCY_LEADING)
    					{
    						sSingleSymbolPos = "Leading";
    					}
    					else
    					{
    						sSingleSymbolPos = "Trailing";
    					}    				
    					repo.SingleSymbolPosition.Select(sSingleSymbolPos);
    				}
            		    
            		if (Functions.GoodData(Variables.globalSettings.CompanySettings.CurrencySettings.ForeignCurrencies[0].decimalPlaces))
    				{
    					string sDecimal;
    					switch (Variables.globalSettings.CompanySettings.CurrencySettings.ForeignCurrencies[0].decimalPlaces)
    					{
    						case CURRENCY_DECIMAL.CURRENCY_DECIMAL_0:
    							sDecimal = "0";
    							break;
    						case CURRENCY_DECIMAL.CURRENCY_DECIMAL_1:
    							sDecimal = "1";
    							break;
    						case CURRENCY_DECIMAL.CURRENCY_DECIMAL_2:
    							sDecimal = "2";
    							break;
    						default:
    							sDecimal = "0";
    							break;
    					}
    					repo.SingleDecimalPlaces.Select(sDecimal);
            		}
            		
            		// TBD
//            		if (Functions.GoodData(Variables.GlobalSettings.CompanySettings.CurrencySettings.ForeignCurrencies[0].denomination))
//            		{
//            			// Pro uses a combo box with preset denominations
//            		}
            		
            	}
            	
	            // Exchange rates dialog - common dialog
	            for (int x = 0; x < Variables.globalSettings.CompanySettings.CurrencySettings.ForeignCurrencies.Count; x++)
	            {
					if (Variables.globalSettings.CompanySettings.CurrencySettings.ForeignCurrencies[x].ExchangeRates.Count != 0)
					{
						repo.ExchangeRateBtn.Click();
						
						// Sage 50 message about changing existing foreign currencies
						if (SimplyMessage.repo.SelfInfo.Exists(Variables.iExistWaitTime) && SimplyMessage.repo.Self.Visible)
						{
							SimplyMessage.repo.OK.Click();
						}
						
            			
						if (!bPro)
						{
							ExchangeRate.repo.Currency.Select(Variables.globalSettings.CompanySettings.CurrencySettings.ForeignCurrencies[x].Currency);	
						}
						ExchangeRate.repo.DisplayReminder.SetState(Variables.globalSettings.CompanySettings.CurrencySettings.ForeignCurrencies[x].exchangeDisplayReminder);
						ExchangeRate.repo.CurrencyRateReminder.Items[(int)Variables.globalSettings.CompanySettings.CurrencySettings.ForeignCurrencies[x].exchangeRateReminder].Select();
						
						if (Functions.GoodData(Variables.globalSettings.CompanySettings.CurrencySettings.ForeignCurrencies[x].exchangeWebsite))
						{
							ExchangeRate.repo.CurrencyWebsite.TextValue = Variables.globalSettings.CompanySettings.CurrencySettings.ForeignCurrencies[x].exchangeWebsite;
						}
						
						if (Variables.globalSettings.CompanySettings.CurrencySettings.ForeignCurrencies[x].ExchangeRates.Count != 0)
						{
							// remove existing rows
							while (ExchangeRate.repo.ExchangeRateContainer.GetContents().Count != 0)
							{
								ExchangeRate.repo.Self.PressKeys("{alt}er");
							}
							
							for (int y=0; y < Variables.globalSettings.CompanySettings.CurrencySettings.ForeignCurrencies[x].ExchangeRates.Count; y++)
							{
								ExchangeRate.repo.ExchangeRateContainer.SetToLine(x);
								// use container element
								ExchangeRate.repo.ExchangeRateElement.SetText(Variables.globalSettings.CompanySettings.CurrencySettings.ForeignCurrencies[x].ExchangeRates[y].exchangeDate);								
								ExchangeRate.repo.ExchangeRateElement.MoveRight();
								
								ExchangeRate.repo.ExchangeRateElement.SetText(Variables.globalSettings.CompanySettings.CurrencySettings.ForeignCurrencies[x].ExchangeRates[y].exchangeRate);
								ExchangeRate.repo.ExchangeRateElement.MoveRight();
							}
							ExchangeRate.repo.OK.Click();
						}
					}
	            }
            }
			
            if (bClickOK)
            {
            	repo.OK.Click();
            }
            
        }
        
        public static void _SA_SetCompanyFormsSettings()
        {
        	_SA_SetCompanyFormsSettings(true);
        }
        
        public static void _SA_SetCompanyFormsSettings(bool bClickOK)
        {
        	CheckSettingsDialog();
        	
        	Settings.repo.SettingsTree.CompanyForms.Select();
        	
        	// Next form number column
        	Settings.repo.NextNumInvoices.TextValue = Variables.globalSettings.CompanySettings.FormSettings.nextNumInvoice;
        	Settings.repo.NextNumSalesQuotes.TextValue = Variables.globalSettings.CompanySettings.FormSettings.nextNumSalesQuote;
        	Settings.repo.NextNumReceipts.TextValue = Variables.globalSettings.CompanySettings.FormSettings.nextNumReceipt;
        	Settings.repo.NextNumCustomerDeposits.TextValue = Variables.globalSettings.CompanySettings.FormSettings.nextNumCustomerDeposit;
        	Settings.repo.NextNumPurchaseOrders.TextValue = Variables.globalSettings.CompanySettings.FormSettings.nextNumPurchaseOrder;
        	if (Settings.repo.TimeSlipElementInfo.Exists(Variables.iExistWaitTime))
        	{
        		if (Functions.GoodData(Variables.globalSettings.CompanySettings.FormSettings.nextNumTimeSlip))
        		{
        			Settings.repo.NextNumTimeSlips.TextValue = Variables.globalSettings.CompanySettings.FormSettings.nextNumTimeSlip;
        		}
        	}        
        	Settings.repo.NextNumDDEmployee.TextValue = Variables.globalSettings.CompanySettings.FormSettings.nextNumDirectDeposit;
        	
        	// Verify number sequence column
        	Settings.repo.VerifyInvoices.SetState(Variables.globalSettings.CompanySettings.FormSettings.verifyNumSeqInvoice);
        	Settings.repo.VerifySalesQuotes.SetState(Variables.globalSettings.CompanySettings.FormSettings.verifyNumSeqSalesQuote);
        	Settings.repo.VerifyReceipts.SetState(Variables.globalSettings.CompanySettings.FormSettings.verifyNumSeqReceipt);
        	Settings.repo.VerifyCustomerDeposits.SetState(Variables.globalSettings.CompanySettings.FormSettings.verifyNumSeqCustomerDeposit);
        	Settings.repo.VerifyPurchaseOrders.SetState(Variables.globalSettings.CompanySettings.FormSettings.verifyNumSeqPurchaseOrder);
        	if (Settings.repo.TimeSlipElementInfo.Exists(Variables.iExistWaitTime) && Settings.repo.VerifyTimeSlips.Enabled)
        	{
        		Settings.repo.VerifyTimeSlips.SetState(Variables.globalSettings.CompanySettings.FormSettings.verifyNumSeqTimeSlips);
        	}
        	Settings.repo.VerifyCheques.SetState(Variables.globalSettings.CompanySettings.FormSettings.verifyNumSeqCheque);
        	Settings.repo.VerifyDepositSlips.SetState(Variables.globalSettings.CompanySettings.FormSettings.verifyNumSeqDepositSlips);
        	
        	// Confirm printing e-mail column
        	Settings.repo.ConfirmPrintEmailInvoice.SetState(Variables.globalSettings.CompanySettings.FormSettings.confirmPrintEmailInvoice);
        	Settings.repo.ConfirmPrintEmailSalesQuotes.SetState(Variables.globalSettings.CompanySettings.FormSettings.confirmPrintEmailSalesQuote);
        	Settings.repo.ConfirmPrintEmailSalesOrders.SetState(Variables.globalSettings.CompanySettings.FormSettings.confirmPrintEmailSalesOrder);
        	Settings.repo.ConfirmPrintEmailReceipts.SetState(Variables.globalSettings.CompanySettings.FormSettings.confirmPrintEmailReceipt);
        	Settings.repo.ConfirmPrintEmailPurchaseOrders.SetState(Variables.globalSettings.CompanySettings.FormSettings.confirmPrintEmailPurchaseOrder);
        	Settings.repo.ConfirmPrintEmailCheques.SetState(Variables.globalSettings.CompanySettings.FormSettings.confirmPrintEmailChequesOrder);        	
        	if (Settings.repo.TimeSlipElementInfo.Exists(Variables.iExistWaitTime))
        	{
        		Settings.repo.ConfirmPrintEmailTimeSlips.SetState(Variables.globalSettings.CompanySettings.FormSettings.confirmPrintEmailTimeSlips);
        	}
        	Settings.repo.ConfirmPrintEmailDepositSlips.SetState(Variables.globalSettings.CompanySettings.FormSettings.confirmPrintEmailDepositSlips);
        	
        	// Print company address on column
        	Settings.repo.PrintCompanyAddressInvoices.SetState(Variables.globalSettings.CompanySettings.FormSettings.printCompAddressInvoice);
        	Settings.repo.PrintCompanyAddressSalesQuotes.SetState(Variables.globalSettings.CompanySettings.FormSettings.printCompAddressSalesQuotes);
        	Settings.repo.PrintCompanyAddressSalesOrders.SetState(Variables.globalSettings.CompanySettings.FormSettings.printCompAddressSalesOrders);
        	Settings.repo.PrintCompanyAddressReceipts.SetState(Variables.globalSettings.CompanySettings.FormSettings.printCompAddressStatements);
        	Settings.repo.PrintCompanyAddressPurchaseOrders.SetState(Variables.globalSettings.CompanySettings.FormSettings.printCompAddressPurchaseOrders);
        	Settings.repo.PrintCompanyAddressCheques.SetState(Variables.globalSettings.CompanySettings.FormSettings.printCompAddressCheque);
        	
        	// Print in batches column
        	Settings.repo.PrintInBatchesInvoices.SetState(Variables.globalSettings.CompanySettings.FormSettings.printInBatchesInvoice);
        	Settings.repo.PrintInBatchesPackingSlips.SetState(Variables.globalSettings.CompanySettings.FormSettings.printInBatchesPackingSlip);
        	Settings.repo.PrintInBatchesSalesQuotes.SetState(Variables.globalSettings.CompanySettings.FormSettings.printInBatchesSalesQuotes);
        	Settings.repo.PrintInBatchesSalesOrders.SetState(Variables.globalSettings.CompanySettings.FormSettings.printInBatchesSalesOrders);
        	Settings.repo.PrintInBatchesReceipts.SetState(Variables.globalSettings.CompanySettings.FormSettings.printInBatchesReceipt);
        	Settings.repo.PrintInBatchesPurchaseOrders.SetState(Variables.globalSettings.CompanySettings.FormSettings.printInBatchesPurchaseOrders);
        	Settings.repo.PrintInBatchesCheques.SetState(Variables.globalSettings.CompanySettings.FormSettings.printInBatchesCheque);
        	
        	// Check for duplicates column
        	Settings.repo.CheckForDupInvoices.SetState(Variables.globalSettings.CompanySettings.FormSettings.checkForDupInvoices);
        	Settings.repo.CheckForDupReceipts.SetState(Variables.globalSettings.CompanySettings.FormSettings.checkForDupReceipts);
        	
        	if (bClickOK)
        	{
        		Settings.repo.OK.Click();
        	}
        	                                           
        }
        
        public static void _SA_SetCompanyEmailSettings()
        {
        	_SA_SetCompanyEmailSettings(true);
        }
        
        public static void _SA_SetCompanyEmailSettings(bool bClickOK)
        {
        	CheckSettingsDialog();
        	
        	Settings.repo.SettingsTree.CompanyEmail.Select();
        	
        	// Attachment format feature has been removed
        	
        	for (int x = 0; x < Variables.globalSettings.CompanySettings.EmailSettings.Messages.Count; x++)
        	{
        		Settings.repo.EmailForms.Items[x].Select();
        		Settings.repo.EmailMessage.TextValue = Variables.globalSettings.CompanySettings.EmailSettings.Messages[x].Message;
        	}
        	
        	if (bClickOK)
        	{
        		Settings.repo.OK.Click();
        	}
        }
        
        public static void _SA_SetCompanyDateSettings()
        {
        	_SA_SetCompanyDateSettings(true);
        }
        
        public static void _SA_SetCompanyDateSettings(bool bClickOK)
        {
        	CheckSettingsDialog();
        	
        	Settings.repo.SettingsTree.CompanyDateFormat.Select();
        	
        	Settings.repo.ShortDateFormat.Select(Variables.globalSettings.CompanySettings.DateSettings.shortDateFormat);
        	Settings.repo.ShortDateSeparator.Select(Variables.globalSettings.CompanySettings.DateSettings.shortDateSeparator);
        	Settings.repo.LongDateFormat.Select(Variables.globalSettings.CompanySettings.DateSettings.longDateFormat);
        	
        	if (Variables.globalSettings.CompanySettings.DateSettings.onScreenUse == DATE_SHORT_LONG.DATE_SHORT)
        	{
        		Settings.repo.OnScreenUseShort.Click();
        	}
        	else
        	{
        		Settings.repo.OnScreenUseLong.Click();
        	}
        	
        	if (Variables.globalSettings.CompanySettings.DateSettings.inReportsUse == DATE_SHORT_LONG.DATE_SHORT)
        	{
        		Settings.repo.InReportsUseShort.Click();
        	}
        	else
        	{
        		Settings.repo.InReportsUseLong.Click();
        	}
        	
        	if (bClickOK)
        	{
        		Settings.repo.OK.Click();
        	}
        }
        
        public static void _SA_SetCompanyShipperSettings()
        {
        	_SA_SetCompanyShipperSettings(true);
        }
        
        public static void _SA_SetCompanyShipperSettings(bool bClickOk)
        {
        	CheckSettingsDialog();
        	
        	Settings.repo.SettingsTree.CompanyShippers.Select();
        	
        	Settings.repo.TrackShipments.SetState(Variables.globalSettings.CompanySettings.ShipperSettings.TrackShipments);
        	if (Variables.globalSettings.CompanySettings.ShipperSettings.TrackShipments == true)
        	{        		        		
        		int startElement = 1102;	// First shipper field element number
				int y = 0;        		
        		
        		for (int x = 0; x < Variables.globalSettings.CompanySettings.ShipperSettings.ShipServices.Count; x++)
        		{
        			
    				string sShipper = (startElement + y).ToString();
    				string sTrackingSite = (startElement + y + 1).ToString();
    				
        			if (Functions.GoodData(Variables.globalSettings.CompanySettings.ShipperSettings.ShipServices[x].Shipper))
        			{
        				Settings.repo.Shippers.SetFieldText(sShipper, Variables.globalSettings.CompanySettings.ShipperSettings.ShipServices[x].Shipper);
        			}
        			if (Functions.GoodData(Variables.globalSettings.CompanySettings.ShipperSettings.ShipServices[x].TrackingSite))
        			{
        				Settings.repo.Shippers.SetFieldText(sTrackingSite, Variables.globalSettings.CompanySettings.ShipperSettings.ShipServices[x].TrackingSite);
        			}
        			// increment element number by 2
        			y += 2;
        		}
        		
        	}
        	
        	if (bClickOk)
        	{
        		Settings.repo.OK.Click();
        	}
        	
        }
        
        // Company Settings Methods
        public static void _SA_SetCompanyNameSettings()
        {
        	_SA_SetCompanyNameSettings(true);
        }
        
        public static void _SA_SetCompanyNameSettings(bool bClickOK)
        {
        	CheckSettingsDialog();
        	
        	Settings.repo.SettingsTree.CompanyNames.Select();
        	
        	Settings.repo.AdditionalInformationDate.TextValue = Variables.globalSettings.CompanySettings.additionalInformationDate;
        	Settings.repo.AdditionalInformationField.TextValue = Variables.globalSettings.CompanySettings.additionalInformationField;
        	
        	if (bClickOK)
        	{
        		Settings.repo.OK.Click();
        	}
        }
        
        // General Settings methods
        public static void _SA_Set_AllGeneralSettings()
        {
        	_SA_Set_AllGeneralSettings(true);
        }
        
        public static void _SA_Set_AllGeneralSettings(bool bClickOK)
        {
        	CheckSettingsDialog();
        	
        	Settings._SA_SetGeneralBudgetSettings(false);
        	Settings._SA_SetGeneralNumberingSettings(false);
        	Settings._SA_SetGeneralDepartmentSettings(false);
        	Settings._SA_SetGeneralNameAndLinkedAccountSettings(false);
        }
        
        public static void _SA_SetGeneralBudgetSettings()
        {
        	_SA_SetGeneralBudgetSettings(true);
        }
        
        public static void _SA_SetGeneralBudgetSettings(bool bClickOK)
        {
        	CheckSettingsDialog();
        	
        	Settings.repo.SettingsTree.GeneralBudget.Select();
        	
        	Settings.repo.BudgetRevenueAndExpenseAccounts.SetState(Variables.globalSettings.GeneralSettings.budgetRevAndExAccts);
        	if (Variables.globalSettings.GeneralSettings.budgetRevAndExAccts == true && Functions.GoodData(Variables.globalSettings.GeneralSettings.budgetFrequency))
        	{
        		Settings.repo.BudgetFrequency_LAPayUserExp1.Items[(int)Variables.globalSettings.GeneralSettings.budgetFrequency].Select();        		
        	}
        	    
        	if (bClickOK)
        	{
        		Settings.repo.OK.Click();
        		if (SimplyMessage.repo.SelfInfo.Exists(Variables.iExistWaitTime) && SimplyMessage.repo.Self.Visible)
        		{
        			SimplyMessage.repo.Yes.Click();	
        		}        		
        	}
        	
        }
        
        public static void _SA_SetGeneralNumberingSettings()
        {
        	_SA_SetGeneralNumberingSettings(true);
        }
        
        public static void _SA_SetGeneralNumberingSettings(bool bClickOK)
        {
        	CheckSettingsDialog();
        	
        	Settings.repo.SettingsTree.GeneralNumbering.Select();
        	if (Settings.repo.ShowAcctNumInTransactions.Checked)
        	{
        		Settings.repo.ShowAcctNumInTransactions.SetState(Variables.globalSettings.GeneralSettings.Numbering.showAcctNumInTransactions);
        	}
        	if (Settings.repo.ShowAcctNumInReports.Checked)
        	{
        		Settings.repo.ShowAcctNumInReports.SetState(Variables.globalSettings.GeneralSettings.Numbering.showAcctNumInReports);
        	}
        	
        	Settings.repo.numOfDigitsInAcctNum.Select(Variables.globalSettings.GeneralSettings.Numbering.numOfDigitsInAcctNum);
        	
        	if (Functions.GoodData(Variables.globalSettings.GeneralSettings.Numbering))
        	{
	        	Settings.repo.AccountNumContainer.ClickFirstCell();       	
	        	Settings.repo.AccountNumContainer.MoveRight();
	        	
	        	Settings.repo.AccountNumContainer.SetText(Variables.globalSettings.GeneralSettings.Numbering.assetStartNum);
	        	Settings.repo.AccountNumContainer.MoveRight();
	        	Settings.repo.AccountNumContainer.SetText(Variables.globalSettings.GeneralSettings.Numbering.assetEndNum);
	        	Settings.repo.AccountNumContainer.MoveRight();
	        	Settings.repo.AccountNumContainer.SetText(Variables.globalSettings.GeneralSettings.Numbering.liabilityStartNum);
	        	Settings.repo.AccountNumContainer.MoveRight();
	        	Settings.repo.AccountNumContainer.SetText(Variables.globalSettings.GeneralSettings.Numbering.liabilityEndNum);
	        	Settings.repo.AccountNumContainer.MoveRight();
	        	Settings.repo.AccountNumContainer.SetText(Variables.globalSettings.GeneralSettings.Numbering.equityStartNum);
	        	Settings.repo.AccountNumContainer.MoveRight();
	        	Settings.repo.AccountNumContainer.SetText(Variables.globalSettings.GeneralSettings.Numbering.equityEndNum);
	        	Settings.repo.AccountNumContainer.MoveRight();
	        	Settings.repo.AccountNumContainer.SetText(Variables.globalSettings.GeneralSettings.Numbering.revStartNum);
	        	Settings.repo.AccountNumContainer.MoveRight();
	        	Settings.repo.AccountNumContainer.SetText(Variables.globalSettings.GeneralSettings.Numbering.revEndNum);
	        	Settings.repo.AccountNumContainer.MoveRight();
	        	Settings.repo.AccountNumContainer.SetText(Variables.globalSettings.GeneralSettings.Numbering.expStartNum);
	        	Settings.repo.AccountNumContainer.MoveRight();
	        	Settings.repo.AccountNumContainer.SetText(Variables.globalSettings.GeneralSettings.Numbering.expEndNum);
        	}
        	
        	if (bClickOK)
        	{
        		Settings.repo.OK.Click();
        	}
        }
        
        // Only for create departments without existing departments
        public static void _SA_SetGeneralDepartmentSettings()
        {
        	_SA_SetGeneralDepartmentSettings(true);
        }
        
        public static void _SA_SetGeneralDepartmentSettings(bool bClickOK)
        {
        	CheckSettingsDialog();
        	
        	Settings.repo.SettingsTree.GeneralDepartments.Select();
        	
        	if(Functions.GoodData(Variables.globalSettings.GeneralSettings.DepartmentalAccounting))
        	{
        		Settings.repo.UseDepartmentalAccounting.Click();
        		SimplyMessage.repo.OK.Click();
        		
        		for (int i = 0; i < Variables.globalSettings.GeneralSettings.DepartmentalAccounting.Count; i++)
        		{
        			Settings.repo.DepartmentsContainer.SetToLine(i);
        			
        			// use container element to stop pop up messages
        			Settings.repo.DepartmentsContainerElement.SetText(Variables.globalSettings.GeneralSettings.DepartmentalAccounting[i].code);
        			Settings.repo.DepartmentsContainerElement.MoveRight();
        			Settings.repo.DepartmentsContainerElement.SetText(Variables.globalSettings.GeneralSettings.DepartmentalAccounting[i].description);
        			Settings.repo.DepartmentsContainerElement.MoveRight();
        			
        			if (Variables.globalSettings.GeneralSettings.DepartmentalAccounting[i].ActiveStatus == false)
        			{
        				Settings.repo.DepartmentsContainer.Toggle();
        				// SimplyMessage.repo.OK.Click();
        			}
        		}
        	}
        	
        	if (bClickOK)
        	{
        		Settings.repo.OK.Click();
        	}
        }
        
        public static void _SA_SetGeneralNameAndLinkedAccountSettings()
        {
        	_SA_SetGeneralNameAndLinkedAccountSettings(true);
        }
        
        public static void _SA_SetGeneralNameAndLinkedAccountSettings(bool bClickOK)
        {
        	CheckSettingsDialog();
        	
        	if (Functions.GoodData(Variables.globalSettings.GeneralSettings.AdditionalFields.Field1) ||
        	    Functions.GoodData(Variables.globalSettings.GeneralSettings.AdditionalFields.Field2) ||
        	    Functions.GoodData(Variables.globalSettings.GeneralSettings.AdditionalFields.Field3) ||
        	    Functions.GoodData(Variables.globalSettings.GeneralSettings.AdditionalFields.Field4) ||
        	    Functions.GoodData(Variables.globalSettings.GeneralSettings.AdditionalFields.Field5))
        	{
        		Settings.repo.SettingsTree.GeneralNames.Select();
        		
        		if (Functions.GoodData(Variables.globalSettings.GeneralSettings.AdditionalFields.Field1))
        		{
        			Settings.repo.AdditionalInfo1.TextValue = Variables.globalSettings.GeneralSettings.AdditionalFields.Field1;
        		}
        		if (Functions.GoodData(Variables.globalSettings.GeneralSettings.AdditionalFields.Field2))
        		{
        			Settings.repo.AdditionalInfo2.TextValue = Variables.globalSettings.GeneralSettings.AdditionalFields.Field2;
        		}
        		if (Functions.GoodData(Variables.globalSettings.GeneralSettings.AdditionalFields.Field3))
        		{
        			Settings.repo.AdditionalInfo3.TextValue = Variables.globalSettings.GeneralSettings.AdditionalFields.Field3;
        		}
        		if (Functions.GoodData(Variables.globalSettings.GeneralSettings.AdditionalFields.Field4))
        		{
        			Settings.repo.AdditionalInfo4.TextValue = Variables.globalSettings.GeneralSettings.AdditionalFields.Field4;
        		}
        		if (Functions.GoodData(Variables.globalSettings.GeneralSettings.AdditionalFields.Field5))
        		{
        			Settings.repo.AdditionalInfo5.TextValue = Variables.globalSettings.GeneralSettings.AdditionalFields.Field5;
        		}        		        		            		    	        		    
        	}
        	
        	if (Functions.GoodData(Variables.globalSettings.GeneralSettings.RetainedEarnings))
        	{
        		Settings.repo.SettingsTree.GeneralLinkedAccounts.Select();        	
        		Settings.repo.RetainedEarningsText.TextValue = Variables.globalSettings.GeneralSettings.RetainedEarnings.acctNumber;        		
        	}
        	
        	if (bClickOK)
        	{
        		Settings.repo.OK.Click();
        		if (SimplyMessage.repo.SelfInfo.Exists(Variables.iExistWaitTime) && SimplyMessage.repo.Self.Visible)
        		{
        			SimplyMessage.repo.Yes.Click();
        		}
        	}
        }
        
        // Payable Settings Methods
        public static void _SA_Set_AllPayableSettings()
        {
        	_SA_Set_AllPayableSettings(true);
        }
        
        public static void _SA_Set_AllPayableSettings(bool bClickOK)
        {
        	CheckSettingsDialog();
        	
        	// Address
        	if (Functions.GoodData(Variables.globalSettings.PayableSettings.City) ||
        	    Functions.GoodData(Variables.globalSettings.PayableSettings.Province) ||
        	    Functions.GoodData(Variables.globalSettings.PayableSettings.Country))
        	{
        		Settings.repo.SettingsTree.PayablesAddress.Select();
        		
        		if (Functions.GoodData(Variables.globalSettings.PayableSettings.City))
        		{
        			Settings.repo.VendorCity.TextValue = Variables.globalSettings.PayableSettings.City;
        		}
        		if (Functions.GoodData(Variables.globalSettings.PayableSettings.Province))
        		{
        			Settings.repo.VendorProvince.TextValue = Variables.globalSettings.PayableSettings.Province;
        		}
        		if (Functions.GoodData(Variables.globalSettings.PayableSettings.Country))
        		{
        			Settings.repo.VendorCountry.TextValue = Variables.globalSettings.PayableSettings.Country;
        		}
        	}
        	
        	// Options
        	if (Functions.GoodData(Variables.globalSettings.PayableSettings.agingPeriod1) ||
        	    Functions.GoodData(Variables.globalSettings.PayableSettings.agingPeriod2) ||
        	    Functions.GoodData(Variables.globalSettings.PayableSettings.agingPeriod3) ||
        	    Functions.GoodData(Variables.globalSettings.PayableSettings.calculateDiscountsBeforeTax))
        	{
        		Settings.repo.SettingsTree.PayablesOptions.Select();
        		
        		if (Functions.GoodData(Variables.globalSettings.PayableSettings.agingPeriod1))
        		{
        			Settings.repo.AgingPeriodVendor1.TextValue = Variables.globalSettings.PayableSettings.agingPeriod1;
        		}
        		if (Functions.GoodData(Variables.globalSettings.PayableSettings.agingPeriod2))
        		{
        			Settings.repo.AgingPeriodVendor2.TextValue = Variables.globalSettings.PayableSettings.agingPeriod2;
        		}
        		if (Functions.GoodData(Variables.globalSettings.PayableSettings.agingPeriod3))
        		{
        			Settings.repo.AgingPeriodVendor3.TextValue = Variables.globalSettings.PayableSettings.agingPeriod3;
        		}
        		if (Functions.GoodData(Variables.globalSettings.PayableSettings.calculateDiscountsBeforeTax))
        		{
        			Settings.repo.CalculateDiscountsBeforeTaxVendor.SetState(Variables.globalSettings.PayableSettings.calculateDiscountsBeforeTax);        			
        		}        		    
        	}
        	
        	// Duty
        	if (Functions.GoodData(Variables.globalSettings.PayableSettings.trackDutyOnImportedItems) ||
        	    Functions.GoodData(Variables.globalSettings.PayableSettings.importDutyAcct))
        	{
        		Settings.repo.SettingsTree.PayablesDuty.Select();
        		
        		// SetState method do not enable import duty account
        		if (Variables.globalSettings.PayableSettings.trackDutyOnImportedItems == true && !Settings.repo.TrackDutyOnImportedItems.Checked)
        		{
        			Settings.repo.TrackDutyOnImportedItems.Click();
        		}
        		else if (Variables.globalSettings.PayableSettings.trackDutyOnImportedItems == false && Settings.repo.TrackDutyOnImportedItems.Checked)
        		{
        			Settings.repo.TrackDutyOnImportedItems.Click();
        		}
        		
        		if (Variables.globalSettings.PayableSettings.trackDutyOnImportedItems == true)
        		{
        			Settings.repo.ImportDutyAcctText.PressKeys(Variables.globalSettings.PayableSettings.importDutyAcct.acctNumber);
        			Settings.repo.ImportDutyAcctText.PressKeys("{Tab}");
        		}
        	}
        	
        	// Names
        	if (Functions.GoodData(Variables.globalSettings.PayableSettings.AdditionalFields.Field1) ||
        	    Functions.GoodData(Variables.globalSettings.PayableSettings.AdditionalFields.Field2) ||
        	    Functions.GoodData(Variables.globalSettings.PayableSettings.AdditionalFields.Field3) ||
        	    Functions.GoodData(Variables.globalSettings.PayableSettings.AdditionalFields.Field4) ||
        	    Functions.GoodData(Variables.globalSettings.PayableSettings.AdditionalFields.Field5))
        	{
        		Settings.repo.SettingsTree.PayablesNames.Select();
        		
        		if (Functions.GoodData(Variables.globalSettings.PayableSettings.AdditionalFields.Field1))
        		{
        			Settings.repo.AdditionalInfoVendor1.TextValue = Variables.globalSettings.PayableSettings.AdditionalFields.Field1;
        		}
        		if (Functions.GoodData(Variables.globalSettings.PayableSettings.AdditionalFields.Field2))
        		{
        			Settings.repo.AdditionalInfoVendor2.TextValue = Variables.globalSettings.PayableSettings.AdditionalFields.Field2;
        		}
        		if (Functions.GoodData(Variables.globalSettings.PayableSettings.AdditionalFields.Field3))
        		{
        			Settings.repo.AdditionalInfoVendor3.TextValue = Variables.globalSettings.PayableSettings.AdditionalFields.Field3;
        		}
        		if (Functions.GoodData(Variables.globalSettings.PayableSettings.AdditionalFields.Field4))
        		{
        			Settings.repo.AdditionalInfoVendor4.TextValue = Variables.globalSettings.PayableSettings.AdditionalFields.Field4;
        		}
        		if (Functions.GoodData(Variables.globalSettings.PayableSettings.AdditionalFields.Field5))
        		{
        			Settings.repo.AdditionalInfoVendor5.TextValue = Variables.globalSettings.PayableSettings.AdditionalFields.Field5;
        		}
        	}
        	
        	// Linked Accounts
        	if (Functions.GoodData(Variables.globalSettings.PayableSettings.principalBankAcct) ||
        	    Functions.GoodData(Variables.globalSettings.PayableSettings.apAcct) ||
        	    Functions.GoodData(Variables.globalSettings.PayableSettings.freightAcct) ||
        	    Functions.GoodData(Variables.globalSettings.PayableSettings.earlyPayDiscountAcct) ||
        	    Functions.GoodData(Variables.globalSettings.PayableSettings.prepaymentAcct))
        	{
        		Settings.repo.SettingsTree.PayablesLinkedAccounts.Select();
        		
        		if (Functions.GoodData(Variables.globalSettings.PayableSettings.principalBankAcct) && Settings.repo.PrincipalBankAcctVendor.Visible)
        		{
        			Settings.repo.PrincipalBankAcctVendor.Select(Variables.globalSettings.PayableSettings.principalBankAcct.acctNumber);
        		}
        		if (Functions.GoodData(Variables.globalSettings.PayableSettings.apAcct.acctNumber))
        		{
        			Settings.repo.AccountsPayable.Select(Variables.globalSettings.PayableSettings.apAcct.acctNumber);
        		}
        		if (Functions.GoodData(Variables.globalSettings.PayableSettings.freightAcct.acctNumber))
        		{
        			Settings.repo.FreightExpense.Select(Variables.globalSettings.PayableSettings.freightAcct.acctNumber);
        		}
        		if (Functions.GoodData(Variables.globalSettings.PayableSettings.earlyPayDiscountAcct.acctNumber))
        		{
        			Settings.repo.EarlyPaymentVendor.Select(Variables.globalSettings.PayableSettings.earlyPayDiscountAcct.acctNumber);
        		}
        		if (Functions.GoodData(Variables.globalSettings.PayableSettings.prepaymentAcct.acctNumber))
        		{
        			Settings.repo.PrePaymentsVendor.Select(Variables.globalSettings.PayableSettings.prepaymentAcct.acctNumber);
        		}
        		if (Variables.globalSettings.PayableSettings.CurrencyAccounts.Count != 0)
        		{        			
        			for (int x = 0; x < Variables.globalSettings.PayableSettings.CurrencyAccounts.Count; x++)
        			{        				
        				Settings.repo.BankAccountsVendor.SetToLine(x);
        				
        				foreach (List <string> lsRow in Settings.repo.BankAccountsVendor.GetContents())
        				{
        					if (Variables.globalSettings.PayableSettings.CurrencyAccounts[x].Currency == lsRow[0])
        					{
        						Settings.repo.BankAccountsVendor.MoveRight();
        						Settings.repo.BankAccountsVendor.SetText(Variables.globalSettings.PayableSettings.CurrencyAccounts[x].BankAccount.acctNumber);
        						break;
        					}
        				}
        			}
        		}
        	}
        	
        	if (bClickOK)
        	{
        		Settings.repo.OK.Click();
        	}
        }
        
        // Receivables Methods
        public static void _SA_Set_AllReceivablesSettings()
        {
        	_SA_Set_AllReceivablesSettings(true);
        }
        
        public static void _SA_Set_AllReceivablesSettings(bool bClickOK)
        {
        	CheckSettingsDialog();
        	
        	Settings._SA_SetReceivablesOptionsAndDiscountSettings(false);
        	Settings._SA_SetReceivablesGeneralNameSettings(false);
        	Settings._SA_SetReceivablesCommentSettings(false);
        	Settings._SA_SetReceivablesLinkedAcctSettings(false);
        }
        
        public static void _SA_SetReceivablesOptionsAndDiscountSettings()
        {
        	_SA_SetReceivablesOptionsAndDiscountSettings(true);
        }
        
        public static void _SA_SetReceivablesOptionsAndDiscountSettings(bool bClickOK)
        {
        	CheckSettingsDialog();
        	
        	Settings.repo.Self.Activate();
        	Settings.repo.SettingsTree.ReceivablesOptions.Select();
        	
        	Settings.repo.AgingPeriodCust1.TextValue = Variables.globalSettings.ReceivableSettings.agingPeriod1;
        	Settings.repo.AgingPeriodCust2.TextValue = Variables.globalSettings.ReceivableSettings.agingPeriod2;
        	Settings.repo.AgingPeriodCust3.TextValue = Variables.globalSettings.ReceivableSettings.agingPeriod3;
        	// Use Click method
        	if (Variables.globalSettings.ReceivableSettings.interestCharges == true && !Settings.repo.InterestChargesCust.Checked)
        	{
        		Settings.repo.InterestChargesCust.Click();
        	}
        	if (Variables.globalSettings.ReceivableSettings.interestCharges == false && Settings.repo.InterestChargesCust.Checked)
        	{
        		Settings.repo.InterestChargesCust.Click();
        	}
        	if (Variables.globalSettings.ReceivableSettings.interestCharges == true)
        	{
        		Settings.repo.InterestPercentCust.TextValue = Variables.globalSettings.ReceivableSettings.interestPercent;
        		Settings.repo.InterestDaysCust.TextValue = Variables.globalSettings.ReceivableSettings.interestDays;
        	}
        	Settings.repo.StatementDaysCust.TextValue = Variables.globalSettings.ReceivableSettings.statementDays;
        	if (Functions.GoodData(Variables.globalSettings.ReceivableSettings.taxCodeForNewCustomers.code))
        	{
        		if (Variables.globalSettings.ReceivableSettings.taxCodeForNewCustomers.code != "No Tax")
        		{
        			Settings.repo.TaxCodeForNewCustomers.Select(Variables.globalSettings.ReceivableSettings.taxCodeForNewCustomers.code);
        		}
        	}
        	else
        	{
        		Settings.repo.TaxCodeForNewCustomers.Select(" - No Tax");
        	}
        	Settings.repo.PrintSalespersonCust.SetState(Variables.globalSettings.ReceivableSettings.printSalesperson);
        	
        	
        	Settings.repo.SettingsTree.ReceivablesDiscount.Select();
        	
        	if (Functions.GoodData(Variables.globalSettings.ReceivableSettings.discountPercent))
        	{
        		Settings.repo.DiscountPercentCust.TextValue = Variables.globalSettings.ReceivableSettings.discountPercent;
        	}
        	if (Functions.GoodData(Variables.globalSettings.ReceivableSettings.discountDays))
        	{
        		Settings.repo.DiscountPeriodCust.TextValue = Variables.globalSettings.ReceivableSettings.discountDays;
        	}
        	if (Functions.GoodData(Variables.globalSettings.ReceivableSettings.netDays))
        	{
        		Settings.repo.NetDaysCust.TextValue = Variables.globalSettings.ReceivableSettings.netDays;
        	}
        	
        	Settings.repo.CalculateEarlyPaymentDiscountsB4Tax.SetState(Variables.globalSettings.ReceivableSettings.calculateEarlyPaymentDiscountsB4Tax);
        	if (Settings.repo.CalculateLineItemDiscount.Visible)
        	{
        		Settings.repo.CalculateLineItemDiscount.SetState(Variables.globalSettings.ReceivableSettings.calculateLineItemDiscounts);
        		if (SimplyMessage.repo.SelfInfo.Exists(Variables.iExistWaitTime) && SimplyMessage.repo.Self.Visible)
        		{
        			SimplyMessage.repo.Yes.Click();
        		}
				//SimplyMessage._ClickMessageDialogYes();
				
        	}
        	
        	if (bClickOK)
        	{
        		Settings.repo.OK.Click();
        		// while (Settings.repo.SelfInfo.Exists()){}
        	}
        }
        
        public static void _SA_SetReceivablesGeneralNameSettings()
        {
        	_SA_SetReceivablesGeneralNameSettings(true);
        }
        
        public static void _SA_SetReceivablesGeneralNameSettings(bool bClickOK)
        {
        	CheckSettingsDialog();
        	
        	if (Functions.GoodData(Variables.globalSettings.ReceivableSettings.AdditionalFields.Field1) ||
        	    Functions.GoodData(Variables.globalSettings.ReceivableSettings.AdditionalFields.Field2) ||
        	    Functions.GoodData(Variables.globalSettings.ReceivableSettings.AdditionalFields.Field3) ||
        	    Functions.GoodData(Variables.globalSettings.ReceivableSettings.AdditionalFields.Field4) ||
        	    Functions.GoodData(Variables.globalSettings.ReceivableSettings.AdditionalFields.Field5))
        	{
        		Settings.repo.SettingsTree.ReceivablesNames.Select();
        		
        		if (Functions.GoodData(Variables.globalSettings.ReceivableSettings.AdditionalFields.Field1))
        		{
        			Settings.repo.AdditionalInfoCust1.TextValue = Variables.globalSettings.ReceivableSettings.AdditionalFields.Field1;
        		}
        		if (Functions.GoodData(Variables.globalSettings.ReceivableSettings.AdditionalFields.Field2))
        		{
        			Settings.repo.AdditionalInfoCust2.TextValue = Variables.globalSettings.ReceivableSettings.AdditionalFields.Field2;
        		}
        		if (Functions.GoodData(Variables.globalSettings.ReceivableSettings.AdditionalFields.Field3))
        		{
        			Settings.repo.AdditionalInfoCust3.TextValue = Variables.globalSettings.ReceivableSettings.AdditionalFields.Field3;
        		}
        		if (Functions.GoodData(Variables.globalSettings.ReceivableSettings.AdditionalFields.Field4))
        		{
        			Settings.repo.AdditionalInfoCust4.TextValue = Variables.globalSettings.ReceivableSettings.AdditionalFields.Field4;
        		}
        		if (Functions.GoodData(Variables.globalSettings.ReceivableSettings.AdditionalFields.Field5))
        		{
        			Settings.repo.AdditionalInfoCust5.TextValue = Variables.globalSettings.ReceivableSettings.AdditionalFields.Field5;
        		}
        	}
        	
        	if (bClickOK)
        	{
        		Settings.repo.OK.Click();
        	}
        }
        
        public static void _SA_SetReceivablesCommentSettings()
        {
        	_SA_SetReceivablesCommentSettings(true);
        }
        
        public static void _SA_SetReceivablesCommentSettings(bool bClickOK)
        {
        	CheckSettingsDialog();
        	
        	if (Functions.GoodData(Variables.globalSettings.ReceivableSettings.salesInvoiceComment) ||
        	    Functions.GoodData(Variables.globalSettings.ReceivableSettings.salesOrderComment) ||
        	    Functions.GoodData(Variables.globalSettings.ReceivableSettings.salesQuoteComment))
        	{	        			        	
	        	Settings.repo.SettingsTree.ReceivablesComments.Select();
	        	
	        	Settings.repo.SalesInvoiceComment.TextValue = Variables.globalSettings.ReceivableSettings.salesInvoiceComment;
	        	Settings.repo.SalesOrderComment.TextValue = Variables.globalSettings.ReceivableSettings.salesOrderComment;
	        	Settings.repo.SalesQuoteComment.TextValue = Variables.globalSettings.ReceivableSettings.salesQuoteComment;
        	}
        	
        	if (bClickOK)
        	{
        		Settings.repo.OK.Click();
        	}        	
        }
        
        public static void _SA_SetReceivablesLinkedAcctSettings()
        {
        	_SA_SetReceivablesLinkedAcctSettings(true);
        }
        
        public static void _SA_SetReceivablesLinkedAcctSettings(bool bClickOK)
        {
        	CheckSettingsDialog();
        	
        	if (Functions.GoodData(Variables.globalSettings.ReceivableSettings.principalBankAcct) ||
        		Functions.GoodData(Variables.globalSettings.ReceivableSettings.arAcct) ||
        		Functions.GoodData(Variables.globalSettings.ReceivableSettings.freightAcct) ||
        		Functions.GoodData(Variables.globalSettings.ReceivableSettings.earlyPayDiscountAcct) ||
        		Functions.GoodData(Variables.globalSettings.ReceivableSettings.depositsAcct) ||
        		Functions.GoodData(Variables.globalSettings.ReceivableSettings.CurrencyAccounts))
        	{
        		Settings.repo.SettingsTree.ReceivablesLinkedAccounts.Select();
        		
        		if (Functions.GoodData(Variables.globalSettings.ReceivableSettings.principalBankAcct.acctNumber) && 
        		    Settings.repo.PrincipleBankAcctCust.Visible)
        		{
        			Settings.repo.PrincipleBankAcctCust.Select(Variables.globalSettings.ReceivableSettings.principalBankAcct.acctNumber);
        		}
        		if (Functions.GoodData(Variables.globalSettings.ReceivableSettings.arAcct.acctNumber))
        		{        			
        			Settings.repo.AccountReceivable.Select(Variables.globalSettings.ReceivableSettings.arAcct.acctNumber);
        		}
        		if (Functions.GoodData(Variables.globalSettings.ReceivableSettings.freightAcct.acctNumber))
        		{
        			Settings.repo.FreightRevenue.Select(Variables.globalSettings.ReceivableSettings.freightAcct.acctNumber);
        		}
        		if (Functions.GoodData(Variables.globalSettings.ReceivableSettings.earlyPayDiscountAcct.acctNumber))
        		{
        			Settings.repo.EarlyPmtSalesDiscount.Select(Variables.globalSettings.ReceivableSettings.earlyPayDiscountAcct.acctNumber);
        		}
        		if (Functions.GoodData(Variables.globalSettings.ReceivableSettings.depositsAcct.acctNumber))
        		{
        			Settings.repo.Deposits.Select(Variables.globalSettings.ReceivableSettings.depositsAcct.acctNumber);
        		}
        		if (Functions.GoodData(Variables.globalSettings.ReceivableSettings.CurrencyAccounts) &&
        		    Settings.repo.BankAccountsCustInfo.Exists(Variables.iExistWaitTime))
        		{        			        			
        			for (int x = 0; x < Variables.globalSettings.ReceivableSettings.CurrencyAccounts.Count; x++)
        			{
        				Settings.repo.BankAccountsCust.SetToLine(x);
        				
        				foreach (List <string> lsRow in Settings.repo.BankAccountsCust.GetContents())
        				{
        					if (Variables.globalSettings.ReceivableSettings.CurrencyAccounts[x].Currency == lsRow[0])
        					{
        						Settings.repo.BankAccountsCust.MoveRight();
        						Settings.repo.BankAccountsCust.SetText(Variables.globalSettings.ReceivableSettings.CurrencyAccounts[x].BankAccount.acctNumber);
        						break;
        					}
        				}
        			}
        		}        		        		            		
        	}
        	
        	if (bClickOK)
        	{
        		Settings.repo.OK.Click();
        	}
        }
        
        // Payroll Settings Methods        
        public static void _SA_Set_AllPayrollSettings()
        {
        	_SA_Set_AllPayrollSettings(true);
        }
        
        public static void _SA_Set_AllPayrollSettings(bool bClickOK)
        {
        	CheckSettingsDialog();
        	
        	Settings._SA_SetPayrollIncomeSettings(false);
        	Settings._SA_SetPayrollDeductionSettings(false);
        	Settings._SA_SetPayrollTaxesSettings(false);
        	Settings._SA_SetPayrollEntitlementSettings(false);
        	Settings._SA_SetPayrollRemittanceSettings(false);
        	Settings._SA_SetPayrollJobSettings(false);	// not in Silktest project
        	Settings._SA_SetPayrollIncomeDeductionNameSettings(false);
        	Settings._SA_SetPayrollAdditionalNameSettings(false);
        	Settings._SA_SetPayrollLinkedIncomeSettings(false);
        	Settings._SA_SetPayrollLinkedDeductionSettings(false);
        	Settings._SA_SetPayrollLinkedTaxesSettings(false);
        	Settings._SA_SetPayrollLinkedUserDefinedSettings(false);
        	
        	if (bClickOK)
        	{
        		Settings.repo.OK.Click();
        	}
        }
        
        public static void _SA_SetPayrollIncomeSettings()
        {
        	_SA_SetPayrollIncomeSettings(true);
        }
        
        public static void _SA_SetPayrollIncomeSettings(bool bClickOK)
        {
        	CheckSettingsDialog();
        	
        	Settings.repo.SettingsTree.PayrollIncomes.Select();
        	
        	if (Settings.repo.TrackQuebecTips.Enabled)
        	{
        		Settings.repo.TrackQuebecTips.SetState(Variables.globalSettings.PayrollSettings.IncomeSettings.trackQuebecTips);
        	}
        	
        	for (int x = 0; x < Variables.globalSettings.PayrollSettings.IncomeSettings.Incomes.Count; x++)
        	{
        		// Settings.repo.PayrollIncomesContainer.SetToLine(x);	// original code
        		Settings.repo.PayrollIncomesContainer.SetToLine(x + 9);	// Customizable incomes starts at Salary
        		
        		if (Functions.GoodData(Variables.globalSettings.PayrollSettings.IncomeSettings.Incomes[x].incomeName))
        		{
        			Settings.repo.PayrollIncomesContainer.SetText(Variables.globalSettings.PayrollSettings.IncomeSettings.Incomes[x].incomeName);        			
        		}
        		Settings.repo.PayrollIncomesContainer.MoveRight();
        		
        		string sType = "";
        		
        		switch(Variables.globalSettings.PayrollSettings.IncomeSettings.Incomes[x].IncomeType)
        		{
        			case INCOME_TYPE.INCOME_TYPE_INCOME:
        				sType = "Income";
        				break;
        			case INCOME_TYPE.INCOME_TYPE_BENEFIT:
        				sType = "Benefit";
        				break;
        			case INCOME_TYPE.INCOME_TYPE_REIMBURSEMENT:
        				sType = "Reimbursement";
        				break;
        			case INCOME_TYPE.INCOME_TYPE_HOURLY_RATE:
        				sType = "Hourly Rate";
        				break;
        			case INCOME_TYPE.INCOME_TYPE_PIECE_RATE:
        				sType = "Piece Rate";
        				break;
        			case INCOME_TYPE.INCOME_TYPE_DIFFERENTIAL_RATE:
        				sType = "Differential Rate";
        				break;
        		}
        		Settings.repo.PayrollIncomesContainer.SetText(sType);
        	}
        	
        	if (bClickOK)
        	{
        		Settings.repo.OK.Click();
        	}
        }
        
        public static void _SA_SetPayrollDeductionSettings()
        {
        	_SA_SetPayrollDeductionSettings(true);
        }
        
        public static void _SA_SetPayrollDeductionSettings(bool bClickOK)
        {
        	CheckSettingsDialog();
        	
        	if (Functions.GoodData(Variables.globalSettings.PayrollSettings.Deductions))
        	{
        		Settings.repo.SettingsTree.PayrollDeductions.Select();
        		
        		for (int x = 0; x < Variables.globalSettings.PayrollSettings.Deductions.Count; x++)
        		{
        			Settings.repo.PayrollDeductionsContainer.SetToLine(x);
        			
        			Settings.repo.PayrollDeductionsContainer.SetText(Variables.globalSettings.PayrollSettings.Deductions[x].Deduction);
        			Settings.repo.PayrollDeductionsContainer.MoveRight();
        			
        			string sDed = "";
        			switch(Variables.globalSettings.PayrollSettings.Deductions[x].DeductBy)
        			{
        				case DEDUCT_TYPE.DEDUCT_AMOUNT:
        					sDed = "Amount";
							break;
						case DEDUCT_TYPE.DEDUCT_PERCENT:
							sDed = "Percent of Gross";
							break;
        			}
        			Settings.repo.PayrollDeductionsContainer.SetText(sDed);
        		}        		
        	}
        	
        	if (bClickOK)
        	{
        		Settings.repo.OK.Click();
        	}
        }
        
        public static void _SA_SetPayrollTaxesSettings()
        {
        	_SA_SetPayrollTaxesSettings(true);
        }
        
        public static void _SA_SetPayrollTaxesSettings(bool bClickOK)
        {
        	CheckSettingsDialog();
        	
        	Settings.repo.SettingsTree.PayrollTaxes.Select();
        	if (Functions.GoodData(Variables.globalSettings.PayrollSettings.TaxSettings.eiFactor))
        	{
        		Settings.repo.EIFactor.TextValue = Variables.globalSettings.PayrollSettings.TaxSettings.eiFactor;
        	}
        	if (Functions.GoodData(Variables.globalSettings.PayrollSettings.TaxSettings.wcbRate))
        	{
        		Settings.repo.WCBRate.TextValue = Variables.globalSettings.PayrollSettings.TaxSettings.wcbRate;
        	}
        	if (Functions.GoodData(Variables.globalSettings.PayrollSettings.TaxSettings.ehtFactor))
        	{
        		Settings.repo.EHTFactor.TextValue = Variables.globalSettings.PayrollSettings.TaxSettings.ehtFactor;
        	}
        	if (Functions.GoodData(Variables.globalSettings.PayrollSettings.TaxSettings.qhsfFactor))
        	{
        		Settings.repo.QHSFFactor.TextValue = Variables.globalSettings.PayrollSettings.TaxSettings.qhsfFactor;
        	}
        	
        	if (bClickOK)
        	{
        		Settings.repo.OK.Click();
        	}
        }
        
        public static void _SA_SetPayrollEntitlementSettings()
        {
        	_SA_SetPayrollEntitlementSettings(true);
        }
        
        public static void _SA_SetPayrollEntitlementSettings(bool bClickOK)
        {
        	CheckSettingsDialog();
        	
        	Settings.repo.SettingsTree.PayrollEntitlements.Select();
        	
        	if (Functions.GoodData(Variables.globalSettings.PayrollSettings.EntitlementSettings.numOfHrsInTheWorkDay))
        	{
        		Settings.repo.NumOfHoursInTheWorkDay.TextValue = Variables.globalSettings.PayrollSettings.EntitlementSettings.numOfHrsInTheWorkDay;
        	}
        	
        	if (Functions.GoodData(Variables.globalSettings.PayrollSettings.EntitlementSettings.Entitlements))
        	{        		
        		Settings.repo.PayrollEntitlementsContainer.Click("27;35");	// click first cell requires custom coordinates for this container
        		
        		List <List <string>> lsContents = Settings.repo.PayrollEntitlementsContainer.GetContents();
        		
        		for (int x = 0; x < Variables.globalSettings.PayrollSettings.EntitlementSettings.Entitlements.Count; x++)
        		{
        			Settings.repo.PayrollEntitlementsContainer.SetText(Variables.globalSettings.PayrollSettings.EntitlementSettings.Entitlements[x].entitlementName);
        			if (Variables.globalSettings.PayrollSettings.EntitlementSettings.Entitlements[x].entitlementName == "<undefined>")
        			{
        				Settings.repo.PayrollEntitlementsContainer.MoveDown();
        			}
        			else
        			{
        				Settings.repo.PayrollEntitlementsContainer.MoveRight();
        				Settings.repo.PayrollEntitlementsContainer.SetText(Variables.globalSettings.PayrollSettings.EntitlementSettings.Entitlements[x].percentOfHrsWorked);
        				Settings.repo.PayrollEntitlementsContainer.MoveRight();
        				Settings.repo.PayrollEntitlementsContainer.SetText(Variables.globalSettings.PayrollSettings.EntitlementSettings.Entitlements[x].maxDays);
        				Settings.repo.PayrollEntitlementsContainer.MoveRight();
        				
        				string sClear = lsContents[x][3];
        				
        				if (Variables.globalSettings.PayrollSettings.EntitlementSettings.Entitlements[x].clearDaysAtYearEnd == true)
        				{
        					if (sClear == "No")
        					{
        						Settings.repo.PayrollEntitlementsContainer.Toggle();
        					}
        				}
        				else
        				{
        					if (sClear == "Yes")
        					{
        						Settings.repo.PayrollEntitlementsContainer.Toggle();
        					}
        				}
        				Settings.repo.PayrollEntitlementsContainer.MoveRight();
        			}
        		}
        	}
        	
        	if (bClickOK)
        	{
        		Settings.repo.OK.Click();
        	}
        }
        
        public static void _SA_SetPayrollRemittanceSettings()
        {
        	_SA_SetPayrollRemittanceSettings(true);
        }
        
        public static void _SA_SetPayrollRemittanceSettings(bool bClickOK)
        {
        	CheckSettingsDialog();
        	
        	if (Functions.GoodData(Variables.globalSettings.PayrollSettings.Remittances))
        	{
        		Settings.repo.SettingsTree.PayrollRemittance.Select();
        		
        		// Move to Remittance vendor column? required here?
        		Settings.repo.PayrollRemittanceContainer.ClickFirstCell();	// or needs to change coordinates?
        		Settings.repo.PayrollRemittanceContainer.MoveRight();
        		
        		for (int x = 0; x < Variables.globalSettings.PayrollSettings.Remittances.Count; x++)
        		{
        			Settings.repo.PayrollRemittanceContainer.SetToLine(x);
        			Settings.repo.PayrollRemittanceContainer.MoveRight();
        			
        			if (Variables.globalSettings.PayrollSettings.Remittances[x].RemitVendor.Trim() != "")
        			{
        				Settings.repo.PayrollRemittanceContainer.SetText(Variables.globalSettings.PayrollSettings.Remittances[x].RemitVendor);
        				Settings.repo.PayrollRemittanceContainer.MoveRight();
        				
        				if (Functions.GoodData(Variables.globalSettings.PayrollSettings.Remittances[x].RemitFrequency))
        				{
        					string sFrequency = "";
        					
        					switch (Variables.globalSettings.PayrollSettings.Remittances[x].RemitFrequency)
        					{
        						case REMITTING_FREQUENCY.REMIT_NONE:
        							sFrequency = "None";
        							break;
        						case REMITTING_FREQUENCY.REMIT_EVERY_7:
        							sFrequency = "Every Seven Days";
        							break;
        						case REMITTING_FREQUENCY.REMIT_WEEKLY:
        							sFrequency = "Weekly";
        							break;
        						case REMITTING_FREQUENCY.REMIT_TWICE_MONTHLY:
        							sFrequency = "Twice Monthly";
        							break;
        						case REMITTING_FREQUENCY.REMIT_MONTHLY:
        							sFrequency = "Monthly";
        							break;
        						case REMITTING_FREQUENCY.REMIT_QUARTERLY:
        							sFrequency = "Quarterly";
        							break;
        						case REMITTING_FREQUENCY.REMIT_ANNUALLY:
        							sFrequency = "Annually";
        							break;        							
        					}
        					Settings.repo.PayrollRemittanceContainer.SetText(sFrequency);
        					Settings.repo.PayrollRemittanceContainer.MoveRight();
        				}
						
						// SimplyMessage might pop up
						
						if (Variables.globalSettings.PayrollSettings.Remittances[x].RemitFrequency != REMITTING_FREQUENCY.REMIT_NONE)
						{
							if (Functions.GoodData(Variables.globalSettings.PayrollSettings.Remittances[x].EndOfNextRemitPeriod))
							{
								Settings.repo.PayrollRemittanceContainer.SetText(Variables.globalSettings.PayrollSettings.Remittances[x].EndOfNextRemitPeriod);
							}
							
							Settings.repo.PayrollRemittanceContainer.MoveRight();
							// Simply message might pop up
						}
        			}        			        			
        		}
        	}
        	
        	if (bClickOK)
        	{
        		Settings.repo.OK.Click();
        	}
        }
        
        public static void _SA_SetPayrollJobSettings()
        {
        	_SA_SetPayrollJobSettings(true);
        }
        
        public static void _SA_SetPayrollJobSettings(bool bClickOK)
        {
        	CheckSettingsDialog();
        	
        	if (Functions.GoodData(Variables.globalSettings.PayrollSettings.JobCategories))
        	{
        		Settings.repo.SettingsTree.PayrollJobCategories.Select();
        		
        		List <List <string>> lsjContents = Settings.repo.PayrollJobCategoriesContainer.GetContents();
        		
        		Settings.repo.PayrollJobCategoriesContainer.ClickFirstCell();	// try not click on the text in the cell, click far right in the cell
        		Settings.repo.PayrollRemittanceContainer.MoveDown();
        		
        		for (int x = 0; x < Variables.globalSettings.PayrollSettings.JobCategories.Count; x++)
        		{
        			Settings.repo.PayrollJobCategoriesContainer.SetText(Variables.globalSettings.PayrollSettings.JobCategories[x].Category);
        			Settings.repo.PayrollJobCategoriesContainer.MoveRight();
        			
        			if (Variables.globalSettings.PayrollSettings.JobCategories[x].SubmitTimeSlips == true)
        			{
        				if (lsjContents[x][1] == "false")
        				{
        					Settings.repo.PayrollJobCategoriesContainer.Toggle();
        				}
        			}
        			else
        			{
        				if (lsjContents[x][1] == "true")
        				{
        					Settings.repo.PayrollJobCategoriesContainer.Toggle();
        				}
        			}
        			Settings.repo.PayrollJobCategoriesContainer.MoveRight();
        			
        			if (Variables.globalSettings.PayrollSettings.JobCategories[x].AreSalespersons == true)
        			{
        				if (lsjContents[x][2] == "false")
        				{
        					Settings.repo.PayrollJobCategoriesContainer.Toggle();
        				}
        			}
        			else
        			{
        				if (lsjContents[x][2] == "true")
        				{
        					Settings.repo.PayrollJobCategoriesContainer.Toggle();
        				}
        			}
        			Settings.repo.PayrollJobCategoriesContainer.MoveRight();
        			
        			if (Variables.globalSettings.PayrollSettings.JobCategories[x].ActiveStatus == true)
        			{
        				if (lsjContents[x][3] == "Inactive")
        				{
        					Settings.repo.PayrollJobCategoriesContainer.Toggle();
        				}        				
        			}
        			else
        			{
        				if (lsjContents[x][3] == "Active")
        				{
        					Settings.repo.PayrollJobCategoriesContainer.Toggle();
        				}
        			}
        			Settings.repo.PayrollJobCategoriesContainer.MoveRight();
        			
        		}
        		
        		// assign job categories except the first one row
        		for (int x = 1; x < Variables.globalSettings.PayrollSettings.JobCategories.Count; x++)
        		{
        			if (Variables.globalSettings.PayrollSettings.JobCategories[x].EmployeesAssigned.Count != 0 && Variables.globalSettings.PayrollSettings.JobCategories[x].ActiveStatus == true)
        			{
        				Settings.repo.AssignJobCategories.Click();
        				        				
        				JobCategoryInformation.repo.JobCategorySelection.Select(Variables.globalSettings.PayrollSettings.JobCategories[x].Category);
        				JobCategoryInformation.repo.RemoveAll.Click();
        				
        				for (int y = 0; y < Variables.globalSettings.PayrollSettings.JobCategories[x].EmployeesAssigned.Count; y++)
        				{
							JobCategoryInformation.repo.EmployeesNotInJobCat.SelectListItem(Variables.globalSettings.PayrollSettings.JobCategories[x].EmployeesAssigned[y].name);
							JobCategoryInformation.repo.Select.Click();
        				}
        				JobCategoryInformation.repo.OK.Click();
        			}
        		}        		
        	}
        	
        	if (bClickOK)
        	{
        		Settings.repo.OK.Click();
        	}
        }
        
        public static void _SA_SetPayrollIncomeDeductionNameSettings()
        {
        	_SA_SetPayrollIncomeDeductionNameSettings(true);
        }
        
        public static void _SA_SetPayrollIncomeDeductionNameSettings(bool bClickOK)
        {
        	CheckSettingsDialog();
        	
        	if (Functions.GoodData(Variables.globalSettings.PayrollSettings.AdditionalIncome) || Functions.GoodData(Variables.globalSettings.PayrollSettings.AdditionalDeduction))
        	{
        		Settings.repo.SettingsTree.PayrollNamesIncomesDeductions.Select(); 
			
				if (Variables.globalSettings.PayrollSettings.AdditionalIncome[0].Name != null)
	        	{
	        		Settings.repo.IncomeNamesContainer.SetToLine(9);	// First editable row, might need to adjust testcase input data 
	        		Settings.repo.IncomeNamesContainer.MoveRight();
	        		
	        		for (int x = 0; x < Variables.globalSettings.PayrollSettings.AdditionalIncome.Length; x++)
	        		{
	        			if (Variables.globalSettings.PayrollSettings.AdditionalIncome[x].Name != null)
	        			{
	        				Settings.repo.IncomeNamesContainer.SetText(Variables.globalSettings.PayrollSettings.AdditionalIncome[x].Name);
	        				Settings.repo.IncomeNamesContainer.MoveDown();
	        			}
	        		}
	        	}
				
				if (Variables.globalSettings.PayrollSettings.AdditionalDeduction[0].Name != null)
				{
					Settings.repo.DeductionNamesContainer.ClickFirstCell();
					Settings.repo.DeductionNamesContainer.MoveRight();
					
					for (int x = 0; x < Variables.globalSettings.PayrollSettings.AdditionalDeduction.Length; x++)
					{
						if (Variables.globalSettings.PayrollSettings.AdditionalDeduction[x].Name != null)
						{
							Settings.repo.DeductionNamesContainer.SetText(Variables.globalSettings.PayrollSettings.AdditionalDeduction[x].Name);
							Settings.repo.DeductionNamesContainer.MoveDown();
						}
					}
				}
        	}
        	
        	if (bClickOK)
        	{
        		Settings.repo.OK.Click();
        	}
        }
        
        public static void _SA_SetPayrollAdditionalNameSettings()
        {
        	_SA_SetPayrollAdditionalNameSettings(true);
        }
        
        public static void _SA_SetPayrollAdditionalNameSettings(bool bClickOK)
        {
        	CheckSettingsDialog();
        	
        	Settings.repo.SettingsTree.PayrollNamesAdditionalPayroll.Select();
        	
        	if (Functions.GoodData(Variables.globalSettings.PayrollSettings.AdditionalPayrollSettings.AdditionalFields.Field1))
        	{
        		Settings.repo.AdditionalPayrollAddInfo1.TextValue = Variables.globalSettings.PayrollSettings.AdditionalPayrollSettings.AdditionalFields.Field1;
        	}
        	if (Functions.GoodData(Variables.globalSettings.PayrollSettings.AdditionalPayrollSettings.AdditionalFields.Field2))
        	{
        		Settings.repo.AdditionalPayrollAddInfo2.TextValue = Variables.globalSettings.PayrollSettings.AdditionalPayrollSettings.AdditionalFields.Field2;        			
        	}
        	if (Functions.GoodData(Variables.globalSettings.PayrollSettings.AdditionalPayrollSettings.AdditionalFields.Field3))
        	{
        		Settings.repo.AdditionalPayrollAddInfo3.TextValue = Variables.globalSettings.PayrollSettings.AdditionalPayrollSettings.AdditionalFields.Field3;
        	}
        	if (Functions.GoodData(Variables.globalSettings.PayrollSettings.AdditionalPayrollSettings.AdditionalFields.Field4))
        	{
        		Settings.repo.AdditionalPayrollAddInfo4.TextValue = Variables.globalSettings.PayrollSettings.AdditionalPayrollSettings.AdditionalFields.Field4;
        	}
        	if (Functions.GoodData(Variables.globalSettings.PayrollSettings.AdditionalPayrollSettings.AdditionalFields.Field5))
        	{
        		Settings.repo.AdditionalPayrollAddInfo5.TextValue = Variables.globalSettings.PayrollSettings.AdditionalPayrollSettings.AdditionalFields.Field5;        			
        	}
        	
        	if (Functions.GoodData(Variables.globalSettings.PayrollSettings.AdditionalPayrollSettings.ExpenseFields.Field1))
        	{
        		Settings.repo.AdditionalPayrollUDExp1.TextValue = Variables.globalSettings.PayrollSettings.AdditionalPayrollSettings.ExpenseFields.Field1;
        	}
        	if (Functions.GoodData(Variables.globalSettings.PayrollSettings.AdditionalPayrollSettings.ExpenseFields.Field2))
        	{
        		Settings.repo.AdditionalPayrollUDExp2.TextValue = Variables.globalSettings.PayrollSettings.AdditionalPayrollSettings.ExpenseFields.Field2;
        	}
        	if (Functions.GoodData(Variables.globalSettings.PayrollSettings.AdditionalPayrollSettings.ExpenseFields.Field3))
        	{
        		Settings.repo.AdditionalPayrollUDExp3.TextValue = Variables.globalSettings.PayrollSettings.AdditionalPayrollSettings.ExpenseFields.Field3;
        	}
        	if (Functions.GoodData(Variables.globalSettings.PayrollSettings.AdditionalPayrollSettings.ExpenseFields.Field4))
        	{
        		Settings.repo.AdditionalPayrollUDExp4.TextValue = Variables.globalSettings.PayrollSettings.AdditionalPayrollSettings.ExpenseFields.Field4;
        	}
        	if (Functions.GoodData(Variables.globalSettings.PayrollSettings.AdditionalPayrollSettings.ExpenseFields.Field5))
        	{
        		Settings.repo.AdditionalPayrollUDExp5.TextValue = Variables.globalSettings.PayrollSettings.AdditionalPayrollSettings.ExpenseFields.Field5;
        	}
        	
        	if (Functions.GoodData(Variables.globalSettings.PayrollSettings.AdditionalPayrollSettings.EntitlementFields.Field1))
        	{
        		Settings.repo.AdditionalPayrollEntitle1.TextValue = Variables.globalSettings.PayrollSettings.AdditionalPayrollSettings.EntitlementFields.Field1;
        	}
        	if (Functions.GoodData(Variables.globalSettings.PayrollSettings.AdditionalPayrollSettings.EntitlementFields.Field2))
        	{
        		Settings.repo.AdditionalPayrollEntitle2.TextValue = Variables.globalSettings.PayrollSettings.AdditionalPayrollSettings.EntitlementFields.Field2;
        	}
        	if (Functions.GoodData(Variables.globalSettings.PayrollSettings.AdditionalPayrollSettings.EntitlementFields.Field3))
        	{
        		Settings.repo.AdditionalPayrollEntitle3.TextValue = Variables.globalSettings.PayrollSettings.AdditionalPayrollSettings.EntitlementFields.Field3;
        	}
        	if (Functions.GoodData(Variables.globalSettings.PayrollSettings.AdditionalPayrollSettings.EntitlementFields.Field4))
        	{
        		Settings.repo.AdditionalPayrollEntitle4.TextValue = Variables.globalSettings.PayrollSettings.AdditionalPayrollSettings.EntitlementFields.Field4;
        	}
        	if (Functions.GoodData(Variables.globalSettings.PayrollSettings.AdditionalPayrollSettings.EntitlementFields.Field5))
        	{
        		Settings.repo.AdditionalPayrollEntitle5.TextValue = Variables.globalSettings.PayrollSettings.AdditionalPayrollSettings.EntitlementFields.Field5;
        	}
        	
        	if (Functions.GoodData(Variables.globalSettings.PayrollSettings.AdditionalPayrollSettings.provTax))
        	{
        		if (Settings.repo.AdditionalPayrollProvTaxInfo.Exists(Variables.iExistWaitTime))
        		{
        			Settings.repo.AdditionalPayrollProvTax.TextValue = Variables.globalSettings.PayrollSettings.AdditionalPayrollSettings.provTax;
        		}
        	}
        	
        	if (Functions.GoodData(Variables.globalSettings.PayrollSettings.AdditionalPayrollSettings.workersComp))
        	{
        		if (Settings.repo.AdditionalPayrollWorkersCompInfo.Exists(Variables.iExistWaitTime))
        		{
        			Settings.repo.AdditionalPayrollWorkersComp.Select(Variables.globalSettings.PayrollSettings.AdditionalPayrollSettings.workersComp);
        		}
        	}
        	
        	if (bClickOK)
        	{
        		Settings._SA_Invoke();
        	}
        }
        
        public static void _SA_SetPayrollLinkedIncomeSettings()
        {
        	_SA_SetPayrollLinkedIncomeSettings(true);
        }
        
        public static void _SA_SetPayrollLinkedIncomeSettings(bool bClickOK)
        {
        	CheckSettingsDialog();
        	
        	Settings.repo.SettingsTree.PayrollLinkedAcctsIncomes.Select();
        	
        	if (Functions.GoodData(Variables.globalSettings.PayrollSettings.IncomeAccounts.principalBank.acctNumber))
        	{
        		Settings.repo.LAPrincipleBank.Select(Variables.globalSettings.PayrollSettings.IncomeAccounts.principalBank.acctNumber);
        	}
        	if (Functions.GoodData(Variables.globalSettings.PayrollSettings.IncomeAccounts.vacation.acctNumber))
        	{
        		Settings.repo.LAVacOwned.Select(Variables.globalSettings.PayrollSettings.IncomeAccounts.vacation.acctNumber);
        	}
        	if (Functions.GoodData(Variables.globalSettings.PayrollSettings.IncomeAccounts.advances.acctNumber))
        	{
        		Settings.repo.LAAdvances.Select(Variables.globalSettings.PayrollSettings.IncomeAccounts.advances.acctNumber);
        	}
        	
        	if (Functions.GoodData(Variables.globalSettings.PayrollSettings.AdditionalIncome) ||
        	    Functions.GoodData(Variables.globalSettings.PayrollSettings.IncomeAccounts.vacationEarnedLinkedAccount.acctNumber) ||
        	    Functions.GoodData(Variables.globalSettings.PayrollSettings.IncomeAccounts.regularWageLinkedAccount.acctNumber) ||
        	    Functions.GoodData(Variables.globalSettings.PayrollSettings.IncomeAccounts.ot1LinkedAccount.acctNumber) ||
        	    Functions.GoodData(Variables.globalSettings.PayrollSettings.IncomeAccounts.ot2LinkedAccount.acctNumber))
        	{
        		Settings.repo.LAIncomesContainer.ClickFirstCell();
        		Settings.repo.LAIncomesContainer.MoveRight();
        		
        		if (Functions.GoodData(Variables.globalSettings.PayrollSettings.IncomeAccounts.vacationEarnedLinkedAccount.acctNumber))
        		{
        			Settings.repo.LAIncomesContainer.SetText(Variables.globalSettings.PayrollSettings.IncomeAccounts.vacationEarnedLinkedAccount.acctNumber);
        		}
        		Settings.repo.LAIncomesContainer.MoveDown();
        		if (Functions.GoodData(Variables.globalSettings.PayrollSettings.IncomeAccounts.regularWageLinkedAccount.acctNumber))
        		{
        			Settings.repo.LAIncomesContainer.SetText(Variables.globalSettings.PayrollSettings.IncomeAccounts.regularWageLinkedAccount.acctNumber);
        		}
        		Settings.repo.LAIncomesContainer.MoveDown();
        		if (Functions.GoodData(Variables.globalSettings.PayrollSettings.IncomeAccounts.ot1LinkedAccount.acctNumber))
        		{
        			Settings.repo.LAIncomesContainer.SetText(Variables.globalSettings.PayrollSettings.IncomeAccounts.ot1LinkedAccount.acctNumber);
        		}
        		Settings.repo.LAIncomesContainer.MoveDown();
        		if (Functions.GoodData(Variables.globalSettings.PayrollSettings.IncomeAccounts.ot2LinkedAccount.acctNumber))
        		{
        			Settings.repo.LAIncomesContainer.SetText(Variables.globalSettings.PayrollSettings.IncomeAccounts.ot2LinkedAccount.acctNumber);
        		}
        		Settings.repo.LAIncomesContainer.MoveDown();
        		
        		if (Functions.GoodData(Variables.globalSettings.PayrollSettings.AdditionalIncome))
        		{
        			for (int x = 0; x < Variables.globalSettings.PayrollSettings.AdditionalIncome.Length; x++)
        			{
        				if (Functions.GoodData(Variables.globalSettings.PayrollSettings.AdditionalIncome[x].LinkedAccount.acctNumber))
        				{
        					Settings.repo.LAIncomesContainer.SetText(Variables.globalSettings.PayrollSettings.AdditionalIncome[x].LinkedAccount.acctNumber);
        				}
        				Settings.repo.LAIncomesContainer.MoveDown();
        			}
        		}        		
        	}
        	
        	if (bClickOK)
        	{
        		Settings.repo.OK.Click();
        	}
        }
        
        public static void _SA_SetPayrollLinkedDeductionSettings()
        {
        	_SA_SetPayrollLinkedDeductionSettings(true);
        }
        
        public static void _SA_SetPayrollLinkedDeductionSettings(bool bClickOK)
        {
        	CheckSettingsDialog();
        	
        	if (Functions.GoodData(Variables.globalSettings.PayrollSettings.AdditionalDeduction))
        	{
        		Settings.repo.SettingsTree.PayrollLinkedAcctsDeductions.Select();
        		
        		Settings.repo.LADeductionsContainer.ClickFirstCell();
        		Settings.repo.LADeductionsContainer.MoveRight();
        		
        		for (int x = 0; x < Variables.globalSettings.PayrollSettings.AdditionalDeduction.Length; x++)
        		{
        			if (Functions.GoodData(Variables.globalSettings.PayrollSettings.AdditionalDeduction[x].LinkedAccount.acctNumber))
        			{
        				Settings.repo.LADeductionsContainer.SetText(Variables.globalSettings.PayrollSettings.AdditionalDeduction[x].LinkedAccount.acctNumber);
        			}
        			Settings.repo.LADeductionsContainer.MoveDown();
        		}
        	}
        	
        	if (bClickOK)
        	{
        		Settings.repo.OK.Click();
        	}
        }
        
        public static void _SA_SetPayrollLinkedTaxesSettings()
        {
        	_SA_SetPayrollLinkedTaxesSettings(true);
        }
        
        public static void _SA_SetPayrollLinkedTaxesSettings(bool bClickOK)
        {
        	CheckSettingsDialog();
        	
        	Settings.repo.SettingsTree.PayrollLinkedAcctsTaxes.Select();
        	
        	// Payables
        	if (Functions.GoodData(Variables.globalSettings.PayrollSettings.TaxAccounts.payEI))
        	{
        		Settings.repo.LAPayEI.Select(Variables.globalSettings.PayrollSettings.TaxAccounts.payEI.acctNumber);
        	}
        	if (Functions.GoodData(Variables.globalSettings.PayrollSettings.TaxAccounts.payCPP))
        	{
        		Settings.repo.LAPayCPP.Select(Variables.globalSettings.PayrollSettings.TaxAccounts.payCPP.acctNumber);
        	}
        	if (Functions.GoodData(Variables.globalSettings.PayrollSettings.TaxAccounts.payTax))
        	{
        		Settings.repo.LAPayTax.Select(Variables.globalSettings.PayrollSettings.TaxAccounts.payTax.acctNumber);
        	}
        	if (Functions.GoodData(Variables.globalSettings.PayrollSettings.TaxAccounts.payWCB))
        	{
        		Settings.repo.LAPayWCB.Select(Variables.globalSettings.PayrollSettings.TaxAccounts.payWCB.acctNumber);
        	}
        	if (Functions.GoodData(Variables.globalSettings.PayrollSettings.TaxAccounts.payEHT))
        	{
        		Settings.repo.LAPayEHT.Select(Variables.globalSettings.PayrollSettings.TaxAccounts.payEHT.acctNumber);
        	}
        	if (Functions.GoodData(Variables.globalSettings.PayrollSettings.TaxAccounts.payQueTax))
        	{
        		Settings.repo.LAPayQueTax.Select(Variables.globalSettings.PayrollSettings.TaxAccounts.payQueTax.acctNumber);
        	}
        	if (Functions.GoodData(Variables.globalSettings.PayrollSettings.TaxAccounts.payQPP))
        	{
        		Settings.repo.LAPayQPP.Select(Variables.globalSettings.PayrollSettings.TaxAccounts.payQPP.acctNumber);
        	}
        	if (Functions.GoodData(Variables.globalSettings.PayrollSettings.TaxAccounts.payQHSF))
        	{
        		Settings.repo.LAPayQHSF.Select(Variables.globalSettings.PayrollSettings.TaxAccounts.payQHSF.acctNumber);
        	}
        	if (Functions.GoodData(Variables.globalSettings.PayrollSettings.TaxAccounts.payQPIP))
        	{
        		Settings.repo.LAPayQPIP.Select(Variables.globalSettings.PayrollSettings.TaxAccounts.payQPIP.acctNumber);
        	}
        	
        	// Expenses
        	if (Functions.GoodData(Variables.globalSettings.PayrollSettings.TaxAccounts.exEI))
        	{
        		Settings.repo.LAExEI.Select(Variables.globalSettings.PayrollSettings.TaxAccounts.exEI.acctNumber);
        	}
        	if (Functions.GoodData(Variables.globalSettings.PayrollSettings.TaxAccounts.exCPP))
        	{
        		Settings.repo.LAExCPP.Select(Variables.globalSettings.PayrollSettings.TaxAccounts.exCPP.acctNumber);
        	}
        	if (Functions.GoodData(Variables.globalSettings.PayrollSettings.TaxAccounts.exWCB))
        	{
        		Settings.repo.LAExWCB.Select(Variables.globalSettings.PayrollSettings.TaxAccounts.exWCB.acctNumber);
        	}
        	if (Functions.GoodData(Variables.globalSettings.PayrollSettings.TaxAccounts.exEHT))
        	{
        		Settings.repo.LAExEHT.Select(Variables.globalSettings.PayrollSettings.TaxAccounts.exEHT.acctNumber);
        	}
        	if (Functions.GoodData(Variables.globalSettings.PayrollSettings.TaxAccounts.exQPP))
        	{
        		Settings.repo.LAExQPP.Select(Variables.globalSettings.PayrollSettings.TaxAccounts.exQPP.acctNumber);
        	}
        	if (Functions.GoodData(Variables.globalSettings.PayrollSettings.TaxAccounts.exQHSF))
        	{
        		Settings.repo.LAExQHSF.Select(Variables.globalSettings.PayrollSettings.TaxAccounts.exQHSF.acctNumber);
        	}
        	if (Functions.GoodData(Variables.globalSettings.PayrollSettings.TaxAccounts.exQPIP))
        	{
        		Settings.repo.LAExQPIP.Select(Variables.globalSettings.PayrollSettings.TaxAccounts.exQPIP.acctNumber);
        	}
        	
        	if (bClickOK)
        	{
        		Settings.repo.OK.Click();
        	}        	
        }
        
        public static void _SA_SetPayrollLinkedUserDefinedSettings()
        {
        	_SA_SetPayrollLinkedUserDefinedSettings(true);
        }
        
        public static void _SA_SetPayrollLinkedUserDefinedSettings(bool bClickOK)
        {
        	CheckSettingsDialog();
        	
        	if (Functions.GoodData(Variables.globalSettings.PayrollSettings.UserDefinedAccounts))
        	{
        		Settings.repo.SettingsTree.PayrollLinkedAcctsUDExpenses.Select();
        			
        		// Payables
        		if (Functions.GoodData(Variables.globalSettings.PayrollSettings.UserDefinedAccounts.payable1Acct))
        		{
        			Settings.repo.BudgetFrequency_LAPayUserExp1.Select(Variables.globalSettings.PayrollSettings.UserDefinedAccounts.payable1Acct.acctNumber);
        		}
        		if (Functions.GoodData(Variables.globalSettings.PayrollSettings.UserDefinedAccounts.payable2Acct))
        		{
        			Settings.repo.LAPayUserExp2.Select(Variables.globalSettings.PayrollSettings.UserDefinedAccounts.payable2Acct.acctNumber);
        		}
        		if (Functions.GoodData(Variables.globalSettings.PayrollSettings.UserDefinedAccounts.payable3Acct))
        		{
        			Settings.repo.LAPayUserExp3.Select(Variables.globalSettings.PayrollSettings.UserDefinedAccounts.payable3Acct.acctNumber);
        		}
        		if (Functions.GoodData(Variables.globalSettings.PayrollSettings.UserDefinedAccounts.payable4Acct))
        		{
        			Settings.repo.LAPayUserExp4.Select(Variables.globalSettings.PayrollSettings.UserDefinedAccounts.payable4Acct.acctNumber);
        		}
        		if (Functions.GoodData(Variables.globalSettings.PayrollSettings.UserDefinedAccounts.payable5Acct))
        		{
        			Settings.repo.LAPayUserExp5.Select(Variables.globalSettings.PayrollSettings.UserDefinedAccounts.payable5Acct.acctNumber);
        		}
        		
        		// Expenses
        		if (Functions.GoodData(Variables.globalSettings.PayrollSettings.UserDefinedAccounts.expense1Acct))
        		{
        			Settings.repo.LAExUserExp1.Select(Variables.globalSettings.PayrollSettings.UserDefinedAccounts.expense1Acct.acctNumber);
        		}
        		if (Functions.GoodData(Variables.globalSettings.PayrollSettings.UserDefinedAccounts.expense2Acct))
        		{
        			Settings.repo.LAExUserExp2.Select(Variables.globalSettings.PayrollSettings.UserDefinedAccounts.expense2Acct.acctNumber);
        		}
        		if (Functions.GoodData(Variables.globalSettings.PayrollSettings.UserDefinedAccounts.expense3Acct))
        		{
        			Settings.repo.LAExUserExp3.Select(Variables.globalSettings.PayrollSettings.UserDefinedAccounts.expense3Acct.acctNumber);
        		}
        		if (Functions.GoodData(Variables.globalSettings.PayrollSettings.UserDefinedAccounts.expense4Acct))
        		{
        			Settings.repo.LAExUserExp4.Select(Variables.globalSettings.PayrollSettings.UserDefinedAccounts.expense4Acct.acctNumber);
        		}
        		if (Functions.GoodData(Variables.globalSettings.PayrollSettings.UserDefinedAccounts.expense5Acct))
        		{
        			Settings.repo.LAExUserExp5.Select(Variables.globalSettings.PayrollSettings.UserDefinedAccounts.expense5Acct.acctNumber);
        		}        		
        	}
        	
        	if (bClickOK)
        	{
        		Settings.repo.OK.Click();
        	}
        }
        
        // Inventory Settings Methods	
        public static void _SA_Set_AllInventorySettings()
        {
        	_SA_Set_AllInventorySettings(true);
        }
        
        public static void _SA_Set_AllInventorySettings(bool bClickOK)
        {
        	CheckSettingsDialog();
        	
        	Settings._SA_SetInventoryOptionSettings(false);
        	Settings._SA_SetInventoryPriceListSettings(false);
        	Settings._SA_SetInventoryLocationsSettings(false);        	
        	Settings._SA_SetInventoryCategoriesSettings(false);
        	Settings._SA_SetInventoryNameAndLinkedAcctSettings(false);
        	
        	if (bClickOK)
        	{
        		Settings.repo.OK.Click();
        	}
        }
        
        public static void _SA_SetInventoryOptionSettings()
        {
        	_SA_SetInventoryOptionSettings(true);
        }
        
        public static void _SA_SetInventoryOptionSettings(bool bClickOK)
        {
        	CheckSettingsDialog();
        	
        	Settings.repo.SettingsTree.InventoryOptions.Select();
        	
        	switch ((int) Variables.globalSettings.InventorySettings.inventoryCostingMethod)
        	{
        		case 1:
        			Settings.repo.InventoryCostingMethodAverageCost.Select();
        			break;
        		case 2:
        			Settings.repo.InventoryCostingMethodFifo.Select();
        			break;
        	}
        	
        	switch ((int) Variables.globalSettings.InventorySettings.profitMethod)
        	{
        		case 1:
        			Settings.repo.ProfitMethodMarkup.Select();
        			break;
        		case 2:
        			Settings.repo.ProfitMethodMargin.Select();    
					break;        			
        	}
        	
        	switch ((int) Variables.globalSettings.InventorySettings.sortMethod)
        	{
        		case 1:
        			Settings.repo.SortMethodNumber.Select();
        			break;
        		case 2:
        			Settings.repo.SortMethodDescription.Select();
        			break;
        	}
 			
        	if (Settings.repo.AllowInventoryLevelsToGoBelowZero.Enabled)
        	{
        		Settings.repo.AllowInventoryLevelsToGoBelowZero.SetState(Variables.globalSettings.InventorySettings.allowInventoryLevelsToGoBelowZero);
        	}
        	if (Settings.repo.AllowSerialNumbersForInventoryInfo.Exists(Variables.iExistWaitTime))
        	{
        		if (Variables.globalSettings.InventorySettings.inventoryCostingMethod == COSTING_METHOD.FIFO_COST && 
        		    Functions.GoodData(Variables.globalSettings.InventorySettings.allowSerialNums) &&
        		    Settings.repo.AllowSerialNumbersForInventory.Enabled)
        		{
        			Settings.repo.AllowSerialNumbersForInventory.SetState(Variables.globalSettings.InventorySettings.allowSerialNums);
        		}        		        		        		                       
        	}
        	
        	if (bClickOK)
        	{
        		Settings.repo.OK.Click();
        		// while (Settings.repo.SelfInfo.Exists()){}
        		
        	}        	
        }
        
        public static void _SA_SetInventoryPriceListSettings()
        {
        	_SA_SetInventoryPriceListSettings(true);
        }
        
        public static void _SA_SetInventoryPriceListSettings(bool bClickOK)
        {
        	CheckSettingsDialog();
        	
        	if (Functions.GoodData(Variables.globalSettings.InventorySettings.PriceList))
        	{
        		Settings.repo.SettingsTree.InventoryPriceList.Select();
        		
        		Settings.repo.PriceListContainer.ClickFirstCell();
        		
        		int DESCRIPTION_COLUMN = 0;
        		int STATUS_COLUMN = 1;
        		
        		for (int x = 0; x < Variables.globalSettings.InventorySettings.PriceList.Count; x++)
        		{
        			if (Variables.globalSettings.InventorySettings.PriceList[x].description != "")
        			{
        				Settings.repo.PriceListContainer.MoveToField(x, DESCRIPTION_COLUMN);
        				// investigate later to move insert line into Settings res
        				Settings.repo.Edit.Click();
        				Sage50Process.repo.InsertLine.Click();
        				
        				Settings.repo.PriceListContainer.SetText(Variables.globalSettings.InventorySettings.PriceList[x].description);
        				Settings.repo.PriceListContainer.MoveToField(x, STATUS_COLUMN);
        				
        				if (Variables.globalSettings.InventorySettings.PriceList[x].ActiveStatus == false)
        				{        					
        					Settings.repo.PriceListContainer.Toggle();
        				}        		
        			}
        		}        		
        	}
        	
        	if (bClickOK)
        	{
        		Settings.repo.OK.Click();
        	}
        }
        
        public static void _SA_SetInventoryLocationsSettings()
        {
        	_SA_SetInventoryLocationsSettings(true);
        }
        
        public static void _SA_SetInventoryLocationsSettings(bool bClickOK)
        {
        	CheckSettingsDialog();
        	
        	Settings.repo.SettingsTree.InventoryLocations.Select();
        	
        	for (int x = 0; x < Variables.globalSettings.InventorySettings.Locations.Count; x++)
        	{
        		Settings.repo.LocationsContainer.SetToLine(x);
        		
        		Settings.repo.LocationsContainer.SetText(Variables.globalSettings.InventorySettings.Locations[x].code);
        		Settings.repo.LocationsContainer.MoveRight();
        		Settings.repo.LocationsContainer.SetText(Variables.globalSettings.InventorySettings.Locations[x].description);
        		Settings.repo.LocationsContainer.MoveRight();
        		
        		if (Variables.globalSettings.InventorySettings.Locations[x].ActiveStatus == false)
        		{
        			Settings.repo.LocationsContainer.Toggle();
        		}
        	}
        	
        	if (bClickOK)
        	{
        		Settings.repo.OK.Click();
        	}
        }
        
        public static void _SA_SetInventoryCategoriesSettings()
        {
        	_SA_SetInventoryCategoriesSettings(true, false, null);
        }
        
        public static void _SA_SetInventoryCategoriesSettings(bool bClickOK)
        {
        	_SA_SetInventoryCategoriesSettings(bClickOK, false, null);
        }
        
        public static void _SA_SetInventoryCategoriesSettings(bool bClickOK, Boolean bAddSingleCategory)
        {
        	_SA_SetInventoryCategoriesSettings(bClickOK, bAddSingleCategory, null);
        }
        
        public static void _SA_SetInventoryCategoriesSettings(bool bClickOK, Boolean bAddSingleCategory, CATEGORY CatToAdd)
        {
        	CheckSettingsDialog();
        	
        	Settings.repo.SettingsTree.InventoryCategories.Select();        	        	        	        	        
        	        	
        	if (Settings.repo.UseCategoriesForInventory.Enabled)
        	{
        		Settings.repo.UseCategoriesForInventory.Click();
        	}
        	        	
        	List <List<string>> lsContents = Settings.repo.CategoriesContainer.GetContents();        	
        	
        	// Clean up the categories list first, is it still necessary?
        	
        	if (bAddSingleCategory)
        	{
        		if (CatToAdd.name == "<uncategorized>")
        		{
        			// do nothing
        		}
        		else
        		{
        			if (Functions.ListFind(lsContents, CatToAdd.name) == -1) // new category. -1 equals category not found
        			{
        				Settings.repo.CategoriesContainer.ClickFirstCell();
        				Settings.repo.CategoriesContainer.MoveDown(lsContents.Count);
        				Settings.repo.CategoriesContainer.SetText(CatToAdd.name);
        				
        				if (Functions.GoodData(CatToAdd.items))
        				{
        					Settings.repo.AssignItems.Click();
        					        					
        					
        					for (int y = 0; y < CatToAdd.items.Count; y++)
        					{	
								List <string> lsItems = new List<string>();        						
								// Trim Items to only have item number
        						for (int z = 0; z < CategoryInformation.repo.ItemsNotInCategory.Items.Count; z++)
        						{        							        							
        							string[] tempLine = CategoryInformation.repo.ItemsInCategory.Items[z].Text.Split('\t');	        				        							
        							lsItems[z]	= tempLine[0];
        						}
        						
        						int iItem = Functions.ListFind(lsItems, CatToAdd.items[y].invOrServNumber);
        						if (iItem == -1)
        						{
        							Functions.Verify(false, true, "Item found in list");
        						}
        						else
        						{									        							        																        					
        							CategoryInformation.repo.ItemsNotInCategory.SelectListItem(CatToAdd.items[y].invOrServNumber);
        							CategoryInformation.repo.Select.Click();
        						}
        					}
        					CategoryInformation.repo.OK.Click();
        				}        				
        			}
        			else	// the category is already there, re-add the associated items if exists
        			{
        				Settings.repo.CategoriesContainer.ClickFirstCell();
        				Settings.repo.CategoriesContainer.SetToLine(Functions.ListFind(lsContents, CatToAdd.name) -1);
        				Settings.repo.AssignItems.Click();
        				
        				CategoryInformation.repo.RemoveAll.Click();
        				if (Functions.GoodData(CatToAdd.items))
        				{
        					for (int y = 0; y < CatToAdd.items.Count; y++)
        					{        
								List <string> lsItems = new List<string>();        						
								for (int z = 0; z < CategoryInformation.repo.ItemsNotInCategory.Items.Count; z++)
        						{        								
        							string[] tmpLine = CategoryInformation.repo.ItemsNotInCategory.Items[z].Text.Split('\t');
        							lsItems[z] = tmpLine[0];
        						}
								
        						if (Functions.ListFind(lsItems, CatToAdd.items[y].invOrServNumber) == -1)
        						{
        							Functions.Verify(false, true, "Item found in list");
        						}
        						else
        						{        							        						
        							CategoryInformation.repo.ItemsNotInCategory.SelectListItem(CatToAdd.items[y].invOrServNumber);
        							CategoryInformation.repo.Select.Click();
        						}
        					}
        					CategoryInformation.repo.OK.Click();
        				}
        			}
        		}
        	}
        	else // multiple categories
        	{
        		for (int x = 0; x < Variables.globalSettings.InventorySettings.Categories.Count; x++)
        		{
        			if (Variables.globalSettings.InventorySettings.Categories[x].name == "<Uncategorized>")
        			{
        				// do nothing        				
        			}
        			else
        			{
        				if (Functions.ListFind(lsContents, Variables.globalSettings.InventorySettings.Categories[x].name) == -1)	// new category
        				{
        					Settings.repo.CategoriesContainer.ClickFirstCell();
        					Settings.repo.CategoriesContainer.MoveDown(lsContents.Count);
        					Settings.repo.CategoriesContainer.SetText(Variables.globalSettings.InventorySettings.Categories[x].name);
        					if (Functions.GoodData(Variables.globalSettings.InventorySettings.Categories[x].items))
        					{
        						Settings.repo.AssignItems.Click();
        						
        						for (int y = 0; y < Variables.globalSettings.InventorySettings.Categories[x].items.Count; y++)
        						{
        							List <string> lsItems = new List<string>();
        							for (int z = 0; z < CategoryInformation.repo.ItemsNotInCategory.Items.Count; z++)
        							{
        								string[] tmpLine = CategoryInformation.repo.ItemsNotInCategory.Items[z].Text.Split('\t');
        								lsItems[z] = tmpLine[0];
        							}
        							
        							if (Functions.ListFind(lsItems, Variables.globalSettings.InventorySettings.Categories[x].items[y].invOrServNumber) == -1)
        							{
        								Functions.Verify(false, true, "Item found in list");
        							}
        							else
        							{
        								CategoryInformation.repo.ItemsNotInCategory.SelectListItem(Variables.globalSettings.InventorySettings.Categories[x].items[y].invOrServNumber);
        								CategoryInformation.repo.Select.Click();
        							}
        						}
        						CategoryInformation.repo.OK.Click();
        					}
        				}
        				else	// existing categories, verify items
        				{
        					Settings.repo.CategoriesContainer.ClickFirstCell();
        					Settings.repo.CategoriesContainer.SetToLine(Functions.ListFind(lsContents, Variables.globalSettings.InventorySettings.Categories[x].name) -1);
        					Settings.repo.AssignItems.Click();
        					
        					CategoryInformation.repo.RemoveAll.Click();
        					for (int y = 0; y < Variables.globalSettings.InventorySettings.Categories[x].items.Count; y++)
        					{
        						List <string> lsItems = new List<string>();
        						for (int z = 0; z < CategoryInformation.repo.ItemsNotInCategory.Items.Count; z++)
        						{
        							string[] tmpLine = CategoryInformation.repo.ItemsNotInCategory.Items[z].Text.Split('\t');
        							lsItems[z] = tmpLine[0];
        						}
        						        						
        						if (Functions.ListFind(lsItems, Variables.globalSettings.InventorySettings.Categories[x].items[y].invOrServNumber) == -1)
        						{
        							Functions.Verify(false, true, "Item found in list");
        						}
        						else
        						{
        							CategoryInformation.repo.ItemsNotInCategory.SelectListItem(Variables.globalSettings.InventorySettings.Categories[x].items[y].invOrServNumber);
        							CategoryInformation.repo.Select.Click();
        						}
        					}
        					CategoryInformation.repo.OK.Click();
        				}
        			}
        		}
        	}
        	
        	if (bClickOK)
        	{
        		Settings.repo.OK.Click();
        		// while (Settings.repo.SelfInfo.Exists()){}
        	}
        }
        
        public static void _SA_SetInventoryNameAndLinkedAcctSettings()
        {
        	_SA_SetInventoryNameAndLinkedAcctSettings(true);
        }
        
        public static void _SA_SetInventoryNameAndLinkedAcctSettings(bool bClickOK)
        {
        	CheckSettingsDialog();
        	
        	if (Functions.GoodData(Variables.globalSettings.InventorySettings.AdditionalFields.Field1) ||
        	    Functions.GoodData(Variables.globalSettings.InventorySettings.AdditionalFields.Field2) ||
        	    Functions.GoodData(Variables.globalSettings.InventorySettings.AdditionalFields.Field3) ||
        	    Functions.GoodData(Variables.globalSettings.InventorySettings.AdditionalFields.Field4) ||
        	    Functions.GoodData(Variables.globalSettings.InventorySettings.AdditionalFields.Field5))
        	{
        		Settings.repo.SettingsTree.InventoryNames.Select();
        		if (Functions.GoodData(Variables.globalSettings.InventorySettings.AdditionalFields.Field1))
        		{
        			Settings.repo.AdditionalInfoInv1.TextValue = Variables.globalSettings.InventorySettings.AdditionalFields.Field1;        			
        		}
        		if (Functions.GoodData(Variables.globalSettings.InventorySettings.AdditionalFields.Field2))
        		{
        			Settings.repo.AdditionalInfoInv2.TextValue = Variables.globalSettings.InventorySettings.AdditionalFields.Field2;        			
        		}
        		if (Functions.GoodData(Variables.globalSettings.InventorySettings.AdditionalFields.Field3))
        		{
        			Settings.repo.AdditionalInfoInv3.TextValue = Variables.globalSettings.InventorySettings.AdditionalFields.Field3;        			
        		}
        		if (Functions.GoodData(Variables.globalSettings.InventorySettings.AdditionalFields.Field4))
        		{
        			Settings.repo.AdditionalInfoInv4.TextValue = Variables.globalSettings.InventorySettings.AdditionalFields.Field4;
        		}
        		if (Functions.GoodData(Variables.globalSettings.InventorySettings.AdditionalFields.Field5))
        		{
        			Settings.repo.AdditionalInfoInv5.TextValue = Variables.globalSettings.InventorySettings.AdditionalFields.Field5;
        		}
        	}
        	
        	if (Functions.GoodData(Variables.globalSettings.InventorySettings.itemAssemblyCosts.acctNumber) ||
        	    Functions.GoodData(Variables.globalSettings.InventorySettings.adjustmentWriteOff.acctNumber))
        	{
        		Settings.repo.SettingsTree.InventoryLinkedAccounts.Select();
        		Settings.repo.ItemAssemblyCosts.Select(Variables.globalSettings.InventorySettings.itemAssemblyCosts.acctNumber);
        		Settings.repo.AdjustmentWriteOff.Select(Variables.globalSettings.InventorySettings.adjustmentWriteOff.acctNumber);
        	}
        	
        	if (bClickOK)
        	{
        		Settings.repo.OK.Click();
        	}
        }
        
        // Project Settings Methods
        public static void _SA_Set_AllProjectSettings()
        {
        	_SA_Set_AllProjectSettings(true);        	
        }
        
        public static void _SA_Set_AllProjectSettings(bool bClickOK)
        {
        	CheckSettingsDialog();
        	
        	if (Functions.GoodData(Variables.globalSettings.ProjectSettings.budgetProjects) ||
        	    Functions.GoodData(Variables.globalSettings.ProjectSettings.budgetPeriodFrequency))
        	{
        		Settings.repo.SettingsTree.ProjectBudget.Select();
        		Settings.repo.BudgetProjects.SetState(Variables.globalSettings.ProjectSettings.budgetProjects);
        		if (Variables.globalSettings.ProjectSettings.budgetProjects == true)
        		{
        			Settings.repo.BudgetProjectFrequency.Items[(int)Variables.globalSettings.ProjectSettings.budgetPeriodFrequency].Select();
        		}
        	}
        	
        	if (Functions.GoodData(Variables.globalSettings.ProjectSettings.payrollAllocationMethod) ||
        	    Functions.GoodData(Variables.globalSettings.ProjectSettings.otherAllocationMethod) ||
        	    Functions.GoodData(Variables.globalSettings.ProjectSettings.warnIfAllocationIsNotComplete) ||
        	    Functions.GoodData(Variables.globalSettings.ProjectSettings.allowAccessToAllocateFieldUsingTab))
        	{
        		Settings.repo.SettingsTree.ProjectAllocation.Select();
        		if (Variables.globalSettings.ProjectSettings.payrollAllocationMethod == ALLOCATE_PAYROLL.ALLOCATE_AMOUNT)
        		{
        			Settings.repo.ProjAlloPayrollByAmount.Select();
        		}
        		else if (Variables.globalSettings.ProjectSettings.payrollAllocationMethod == ALLOCATE_PAYROLL.ALLOCATE_PERCENT)
        		{
        			Settings.repo.ProjAlloPayrollByPercent.Select();
        		}
        		else
        		{
        			Settings.repo.ProjAlloPayrollByHours.Select();
        		}
        		
        		if (Variables.globalSettings.ProjectSettings.otherAllocationMethod == ALLOCATE_TRANSACTIONS.ALLOCATE_TRANS_AMOUNT)
        		{
        			Settings.repo.ProjAlloOtherAmount.Select();
        		}
        		else
        		{
        			Settings.repo.ProjAlloOtherPercent.Select();
        		}
        		
        		Settings.repo.WarnIfAllocationIsNotComplete.SetState(Variables.globalSettings.ProjectSettings.warnIfAllocationIsNotComplete);
        		Settings.repo.AllowAccessToAllocateFieldUsingTab.SetState(Variables.globalSettings.ProjectSettings.allowAccessToAllocateFieldUsingTab);        		
        	}
        	
        	if (Functions.GoodData(Variables.globalSettings.ProjectSettings.AdditionalFields.Field1) ||
        	    Functions.GoodData(Variables.globalSettings.ProjectSettings.AdditionalFields.Field2) ||
        	    Functions.GoodData(Variables.globalSettings.ProjectSettings.AdditionalFields.Field3) ||
        	    Functions.GoodData(Variables.globalSettings.ProjectSettings.AdditionalFields.Field4) ||
        	    Functions.GoodData(Variables.globalSettings.ProjectSettings.AdditionalFields.Field5))
        	{
        		Settings.repo.SettingsTree.ProjectNames.Select();
        		
        		if (Functions.GoodData(Variables.globalSettings.ProjectSettings.AdditionalFields.Field1))
        		{
        			Settings.repo.AdditionalInfoProj1.TextValue = Variables.globalSettings.ProjectSettings.AdditionalFields.Field1;        			
        		}
        		if (Functions.GoodData(Variables.globalSettings.ProjectSettings.AdditionalFields.Field2))
        		{
        			Settings.repo.AdditionalInfoProj2.TextValue = Variables.globalSettings.ProjectSettings.AdditionalFields.Field2;
        		}
        		if (Functions.GoodData(Variables.globalSettings.ProjectSettings.AdditionalFields.Field3))
        		{
        			Settings.repo.AdditionalInfoProj3.TextValue = Variables.globalSettings.ProjectSettings.AdditionalFields.Field3;
        		}
        		if (Functions.GoodData(Variables.globalSettings.ProjectSettings.AdditionalFields.Field4))
        		{
        			Settings.repo.AdditionalInfoProj4.TextValue = Variables.globalSettings.ProjectSettings.AdditionalFields.Field4;
        		}
        		if (Functions.GoodData(Variables.globalSettings.ProjectSettings.AdditionalFields.Field5))
        		{
        			Settings.repo.AdditionalInfoProj5.TextValue = Variables.globalSettings.ProjectSettings.AdditionalFields.Field5;
        		}
        			
        	}
        	
        	if (bClickOK)
        	{
        		Settings.repo.OK.Click();
        	}
        }
        
        private static void CheckSettingsDialog()
        {
        	try
        	{           		
        		if (!repo.SelfInfo.Exists(Variables.iExistWaitTime))
        		{
        			Settings._SA_Invoke();
        		}
        		else if (!repo.Self.Visible)
        		{
        			Settings._SA_Invoke();
        		}
        		else
        		{        		
        			string sExistsbool = repo.SelfInfo.Exists().ToString();
        			Ranorex.Report.Info("Is Settings Exists: " + sExistsbool);
        			Ranorex.Report.Info("Is Settings visible: " + repo.Self.Visible.ToString());
        		}
        	}
        	catch
        	{
        		Exception ex = new Exception();
        		Ranorex.Report.Info(ex.Message.ToString());
        	}
        }
                        
        private static void CheckTaxesDialog()
        {
        	try
        	{
	        	if (!repo.SelfInfo.Exists(Variables.iExistWaitTime))
	        	{
	        		Settings._SA_TaxesInvoke();
	        	}
	        	else if (!repo.Self.Visible)
	        	{
	        		Settings._SA_TaxesInvoke();
	        	}	        		        	
	        	else if (!repo.SalesTaxContainerInfo.Exists(Variables.iExistWaitTime))
	        	{
	        		Settings._SA_TaxesInvoke();
	        	}
	        	else if (!repo.SalesTaxCodeContainer.Visible)
	        	{
	        		Settings._SA_TaxesInvoke();
	        	}
	        	else
	        	{
	        		Ranorex.Report.Info("Settings Exists: " + repo.SelfInfo.Exists().ToString());
        			Ranorex.Report.Info("Settings visible: " + repo.Self.Visible.ToString());
	        	}	        		        	
        	}
        	catch
        	{
        		Exception ex = new Exception();
        		Ranorex.Report.Info(ex.Message.ToString());
        	}
        }
        
        private static void CheckTaxCodesDialog()
        {
        	try
        	{
        		if (!repo.SelfInfo.Exists(Variables.iExistWaitTime))
	        	{
	        		Settings._SA_TaxCodesInvoke();
	        	}
        		else if (!repo.Self.Visible)
        		{
        			Settings._SA_TaxCodesInvoke();
        		}
	        	else if (!repo.SalesTaxCodeContainerInfo.Exists(Variables.iExistWaitTime))
	        	{
	        		Settings._SA_TaxCodesInvoke();
	        	}
	        	else if (!repo.SalesTaxCodeContainer.Visible)
	        	{
	        		Settings._SA_TaxCodesInvoke();
	        	}
	        	else
	        	{	        		
	        		Ranorex.Report.Info("Settings Exists: " + repo.SelfInfo.Exists().ToString());
        			Ranorex.Report.Info("Settings visible: " + repo.Self.Visible.ToString());
	        	}
        	}
        	catch
        	{
        		Exception ex = new Exception();
        		Ranorex.Report.Info(ex.Message.ToString());
        	}
        }
        
        // DataFile Methods
        // Incomplete, writefile methods are not converted
        public static void DataFile_Taxes_ReadFile(string sDataLocation, string fileCounter)
        {
        	string EXTENSION_TAXES_CONTAINER = ".hdr";
        	string EXTENSION_TAX_CODES_CONTAINER = ".dt1";
        	string EXTENSION_TAXABLE_TAX_LIST_CONTAINER = ".dt2";
        	string EXTENSION_TAX_CODE_DETAILS_CONTAINER = ".dt3";
        	
        	StreamReader FileHDR;	// File handle for Taxes container
        	StreamReader FileDT1;	// File handle for Tax Codes container
        	StreamReader FileDT2;	// File handle for Taxable Tax List container
        	StreamReader FileDT3;	// File handle for Tax Code Details container
        	
        	string dataLine;	// Current field data from file
        	string dataPath;	// Name and path of the data file
        	
        	List<TAX> LT = new List<TAX>();
        	List<TAX_CODE> LTC = new List<TAX_CODE>();
        	TAX TaxRecord;
        	TAX_CODE TaxCodeRecord;
        	
        	// Get data path from file
        	dataPath = sDataLocation + "Tax" + fileCounter;
        	
        	// Open all data files and set the records
        	if (File.Exists(dataPath + EXTENSION_TAXES_CONTAINER))
        	{
        		using (FileHDR = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_TAXES_CONTAINER, "FM_READ")))
        		{
        			while ((dataLine = FileHDR.ReadLine()) != null)
        			{
        				TaxRecord = new TAX();
        				TaxRecord.taxName = "temp";	// if the record is blank, it will fail in the setstructures because of gooddata
        				TaxRecord = (TAX) Settings.DataFile_Taxes_setDataStructure(EXTENSION_TAXES_CONTAINER, dataLine, TaxRecord);
        				LT.Add(TaxRecord);
        			}
        		}
        	}
        	
        	if (File.Exists(dataPath + EXTENSION_TAX_CODES_CONTAINER))
        	{
        		using (FileDT1 = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_TAX_CODES_CONTAINER, "FM_READ")))
        		{
        			while ((dataLine = FileDT1.ReadLine()) != null)
        			{
        				TaxCodeRecord = new TAX_CODE();
        				TaxCodeRecord.description = "temp";	// if the record is blank, it will fail in the setstructures
        				TaxCodeRecord = (TAX_CODE) Settings.DataFile_Taxes_setDataStructure(EXTENSION_TAX_CODES_CONTAINER, dataLine, null, TaxCodeRecord);
        				LTC.Add(TaxCodeRecord);
        			}
        		}
        	}
        	
        	if (File.Exists(dataPath + EXTENSION_TAXABLE_TAX_LIST_CONTAINER))
        	{
        		using (FileDT2 = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_TAXABLE_TAX_LIST_CONTAINER, "FM_READ")))
        		{
        			while ((dataLine = FileDT2.ReadLine()) != null)
        			{
        				LT = (List<TAX>) Settings.DataFile_Taxes_setDataStructure(EXTENSION_TAXABLE_TAX_LIST_CONTAINER, dataLine, null, null, LT);
        			}
        		}
        	}
        	
        	if (File.Exists(dataPath + EXTENSION_TAX_CODE_DETAILS_CONTAINER))
            {
                using (FileDT3 = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_TAX_CODE_DETAILS_CONTAINER, "FM_READ")))
                {
                    while((dataLine = FileDT3.ReadLine()) != null)
                    {
                        LTC = (List<TAX_CODE>)Settings.DataFile_Taxes_setDataStructure(EXTENSION_TAX_CODE_DETAILS_CONTAINER, dataLine, null, null, LT, LTC);
                    }
                }
            }

            // for each TaxRecord in LT
            Settings._SA_SetupTax(LT);
            // for each TaxRecord in LTC
            Settings._SA_SetupTaxCode(LTC);
        }
        
        public static void DataFile_CompanyInfo_ReadFile(string sDataLocation, string fileCounter)
        {
            string EXTENSION_COMPANY_INFO = ".hdr";
            StreamReader FileHDR;

            // local var
            string dataLine;    // stores the current field data from file
            string dataPath;    // the name and path of the data file

            // Get the data path from file
            dataPath = sDataLocation + "CMPINFO" + fileCounter;

            Variables.globalSettings.CompanyInformation = new COMPANY_INFO();

            // Open the data file and set the record
            if (File.Exists(dataPath + EXTENSION_COMPANY_INFO))
            {
                FileHDR = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_COMPANY_INFO, "FM_READ"));
                dataLine = FileHDR.ReadLine();
                //Settings
            }
        }

       public static void DataFile_CreditCard_ReadFile(string sDataLocation, string fileCounter)
       {
            //string FUNCTION_ALIAS = "CRDT";
            string EXTENSION_CC_USED = ".dt1";
            string EXTENSION_CC_ACCEPTED = ".hdr";
            StreamReader FileDT1;	// File handle for CC - Used  tab info
            StreamReader FileHDR;	// File handle for CC - Accepted  tab info

            // Local variables
            string dataLine;	// Stores the current field data from file
            string dataPath;    // The name and path of the data file

            // Get the data path from file
            dataPath = sDataLocation + "CRDT" + fileCounter;

            Variables.globalSettings.CompanySettings.CreditCardSettings = new CREDIT_CARD_SETTINGS();

            // remove the blank credit card lines
            //Variables.GlobalSettings.CompanySettings.CreditCardSettings.CardsUsed.RemoveAt(1);
            //Variables.GlobalSettings.CompanySettings.CreditCardSettings.CardsAccepted.RemoveAt(1);

            // Open all data files
            if (File.Exists(dataPath + EXTENSION_CC_USED))
            {
                FileDT1 = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_CC_USED, "FM_READ"));
                while ((dataLine = FileDT1.ReadLine()) != null)
                {
                    Settings.DataFile_CreditCard_setDataStructure(EXTENSION_CC_USED, dataLine);                    
                }
                FileDT1.Close();
            }
            if (File.Exists(dataPath + EXTENSION_CC_ACCEPTED))
            {
                FileHDR = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_CC_ACCEPTED, "FM_READ"));
                while ((dataLine = FileHDR.ReadLine()) != null)
                {
                    Settings.DataFile_CreditCard_setDataStructure(EXTENSION_CC_ACCEPTED, dataLine);
                }
                FileHDR.Close();
            }

            Settings._SA_SetCompanyCreditCardSettings();
        }

        public static void DataFile_Currency_ReadFile(string sDataLocation, string fileCounter)
        {
            //string FUNCTION_ALIAS = "Currency";
            string EXTENSION_HEADER = ".hdr";
            string EXTENSION_EXCHANGE_RATE = ".dt1";
            string EXTENSION_CURRENCY_CONTAINER = ".cnt";
            string EXTENSION_EXCHANGE_RATE_CONTAINER = ".cnt2";
            StreamReader FileHDR;   // File handle for header
            StreamReader FileDT1;   // File handle for exchange rate window
            StreamReader FileCNT;   // File handle for currencies container
            StreamReader FileCNT2;  // File handle for exchange rates container

            // Local variables
            string dataLine;    // Stores the current field data from file
            string dataPath;    // The name and path of the data file

            // Get the data path from file
            dataPath = sDataLocation + "Currency" + fileCounter;

            Variables.globalSettings.CompanySettings.CurrencySettings = new CURRENCY();

            // Open all data files
            if (File.Exists(dataPath + EXTENSION_HEADER))
            {
                FileHDR = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_HEADER, "FM_READ"));
                dataLine = FileHDR.ReadLine();
                Settings.DataFile_Currency_setDataStructure(EXTENSION_HEADER, dataLine);
                FileHDR.Close();
            }

            if (File.Exists(dataPath + EXTENSION_CURRENCY_CONTAINER))   // read all foreign currencies info here
            {
                FileCNT = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_CURRENCY_CONTAINER, "FM_READ"));
                while ((dataLine = FileCNT.ReadLine()) != null)
                {
                    Settings.DataFile_Currency_setDataStructure(EXTENSION_CURRENCY_CONTAINER, dataLine);
                }
                FileCNT.Close();
            }

            //once the home and foreign currencies are read, we can then do the exchanges
            if (Functions.GoodData(Variables.globalSettings.CompanySettings.CurrencySettings.ForeignCurrencies))
            {
                if (File.Exists(dataPath + EXTENSION_EXCHANGE_RATE))
                {
                    FileDT1 = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_EXCHANGE_RATE, "FM_READ"));

                    for (int x = 0; x < Variables.globalSettings.CompanySettings.CurrencySettings.ForeignCurrencies.Count; x++)
                    {
                        while ((dataLine = FileDT1.ReadLine()) != null)
                        {
                            Settings.DataFile_Currency_setDataStructure(EXTENSION_EXCHANGE_RATE, dataLine);
                        }
                    }
                    FileDT1.Close();
                }

                if (File.Exists(dataPath + EXTENSION_EXCHANGE_RATE_CONTAINER))
                {
                    FileCNT2 = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_EXCHANGE_RATE_CONTAINER, "FM_READ"));
                    for (int x = 0; x < Variables.globalSettings.CompanySettings.CurrencySettings.ForeignCurrencies.Count; x++)
                    {
                        while ((dataLine = FileCNT2.ReadLine()) != null)
                        {
                            Settings.DataFile_Currency_setDataStructure(EXTENSION_EXCHANGE_RATE_CONTAINER, dataLine);
                        }
                    }
                    FileCNT2.Close();
                }
            }
            Settings._SA_SetCompanyCurrencySettings();
        }

        public static void DataFile_Department_ReadFile(string sDataLocation, string fileCounter)
        {
            //string FUNCTION_ALIAS = "Dpt";
            string EXTENSION_DEPARTMENTS = ".hdr";
            StreamReader FileHDR;	// File handle for Project tab info

            // Local variables
            string dataLine;	// Stores the current field data from file
            string dataPath;    // The name and path of the data file

            // Get the data path from file
            dataPath = sDataLocation + "DPT" + fileCounter;

            //GlobalSettings.GeneralSettings.DepartmentalAccounting = New (DEPT_ACCT)

            // Open all data files
            if (File.Exists(dataPath + EXTENSION_DEPARTMENTS))
            {
                FileHDR = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_DEPARTMENTS, "FM_READ"));
                while ((dataLine = FileHDR.ReadLine()) != null)
                {
                    Settings.DataFile_Department_setDataStructure(EXTENSION_DEPARTMENTS, dataLine);
                }
                FileHDR.Close();
            }
            Settings._SA_SetGeneralDepartmentSettings();
        }

        public static void DataFile_Location_ReadFile(string sDataLocation, string fileCounter)
        {
            //string FUNCTION_ALIAS = "locations";
            string EXTENSION_LOCATIONS_CONTAINER = ".hdr";
            StreamReader FileHDR;	// File handle for Project tab info

            // Local variables
            string dataLine;	// Stores the current field data from file
            string dataPath;    // The name and path of the data file

            // Get the data path from file
            dataPath = sDataLocation + "Locations" + fileCounter;

            //GlobalSettings.InventorySettings.Locations = New (LOCATION)

            // Open all data files
            if (File.Exists(dataPath + EXTENSION_LOCATIONS_CONTAINER))
            {
                FileHDR = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_LOCATIONS_CONTAINER, "FM_READ"));
                while ((dataLine = FileHDR.ReadLine()) != null)
                {
                    Settings.DataFile_Location_setDataStructure(EXTENSION_LOCATIONS_CONTAINER, dataLine);
                }
                FileHDR.Close();
            }
            Settings._SA_SetInventoryLocationsSettings();
        }

        public static void DataFile_Names_ReadFile(string sDataLocation, string fileCounter)
        {
            //string FUNCTION_ALIAS = "Names";
            string EXTENSION_GENERAL_NAMES = ".hdr";
            string EXTENSION_PAYABLES_NAMES = ".hd2";
            string EXTENSION_RECEIVABLES_NAMES = ".hd3";
            string EXTENSION_PAYROLL_NAMES = ".hd4";
            string EXTENSION_PAYROLL_INCOME_NAMES = ".hd5";
            string EXTENSION_PAYROLL_USERDEF_ENTITLE_NAMES = ".hd6";
            string EXTENSION_INVENTORY_NAMES = ".hd7";
            string EXTENSION_PROJECT_NAMES = ".hd8";
            string EXTENSION_COMPANY_NAMES = ".hd9";

            StreamReader FileHDR;	// File handle for Project tab info
            StreamReader FileHD2;
            StreamReader FileHD3;
            StreamReader FileHD4;
            StreamReader FileHD5;
            StreamReader FileHD6;
            StreamReader FileHD7;
            StreamReader FileHD8;
            StreamReader FileHD9;

            // Local variables
            string dataLine;	// Stores the current field data from file
            string dataPath;	// The name and path of the data file

            // Intialize flags
            bool DoGeneralNames = false;
            bool DoPayablesNames = false;
            bool DoReceivablesNames = false;
            bool DoAdditionalPayrollNames = false;
            bool DoPayrollIncomesDeductionsNames = false;
            bool DoInventoryNames = false;
            bool DoProjectNames = false;
            bool DoCompanyNames = false;

            // Get the data path from file
            dataPath = sDataLocation + "Names" + fileCounter;

            // Open the data file and set the record
            if (File.Exists(dataPath + EXTENSION_GENERAL_NAMES))
            {
                Variables.globalSettings.GeneralSettings.AdditionalFields = new ADDITIONAL_FIELD_NAMES();
                FileHDR = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_GENERAL_NAMES, "FM_READ"));
                DoGeneralNames = true;
                dataLine = FileHDR.ReadLine();
                Settings.DataFile_Name_setDataStructure(EXTENSION_GENERAL_NAMES, dataLine);
                FileHDR.Close();
            }
            if (File.Exists(dataPath + EXTENSION_PAYABLES_NAMES))
            {
                Variables.globalSettings.PayableSettings.AdditionalFields = new ADDITIONAL_FIELD_NAMES();
                FileHD2 = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_PAYABLES_NAMES, "FM_READ"));
                DoPayablesNames = true;
                dataLine = FileHD2.ReadLine();
                Settings.DataFile_Name_setDataStructure(EXTENSION_PAYABLES_NAMES, dataLine);
                FileHD2.Close();
            }
            if (File.Exists(dataPath + EXTENSION_RECEIVABLES_NAMES))
            {
                Variables.globalSettings.ReceivableSettings.AdditionalFields = new ADDITIONAL_FIELD_NAMES();
                FileHD3 = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_RECEIVABLES_NAMES, "FM_READ"));
                DoReceivablesNames = true;
                dataLine = FileHD3.ReadLine();
                Settings.DataFile_Name_setDataStructure(EXTENSION_RECEIVABLES_NAMES, dataLine);
                FileHD3.Close();
            }
            if (File.Exists(dataPath + EXTENSION_PAYROLL_NAMES))
            {
                Variables.globalSettings.PayrollSettings.AdditionalPayrollSettings.AdditionalFields = new ADDITIONAL_FIELD_NAMES();
                FileHD4 = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_PAYROLL_NAMES, "FM_READ"));
                DoAdditionalPayrollNames = true;
                dataLine = FileHD4.ReadLine();
                Settings.DataFile_Name_setDataStructure(EXTENSION_PAYROLL_NAMES, dataLine);
                FileHD4.Close();
            }
            if (File.Exists(dataPath + EXTENSION_PAYROLL_INCOME_NAMES))
            {
                Variables.globalSettings.PayrollSettings.AdditionalPayrollSettings.provTax = null;
                Variables.globalSettings.PayrollSettings.AdditionalPayrollSettings.workersComp = null;
                //Variables.GlobalSettings.PayrollSettings.AdditionalDeduction = new DEDUCTION_NAME[];  // initialized in the data type already
                //Variables.GlobalSettings.PayrollSettings.AdditionalIncome = new INCOME_NAME[];
                FileHD5 = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_PAYROLL_INCOME_NAMES, "FM_READ"));
                DoPayrollIncomesDeductionsNames = true;
                dataLine = FileHD5.ReadLine();
                Settings.DataFile_Name_setDataStructure(EXTENSION_PAYROLL_INCOME_NAMES, dataLine);
                FileHD5.Close();
            }
            if (File.Exists(dataPath + EXTENSION_PAYROLL_USERDEF_ENTITLE_NAMES))
            {
                Variables.globalSettings.PayrollSettings.AdditionalPayrollSettings.ExpenseFields = new ADDITIONAL_FIELD_NAMES();
                Variables.globalSettings.PayrollSettings.AdditionalPayrollSettings.EntitlementFields = new ADDITIONAL_FIELD_NAMES();
                FileHD6 = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_PAYROLL_USERDEF_ENTITLE_NAMES, "FM_READ"));
                DoAdditionalPayrollNames = true;
                dataLine = FileHD6.ReadLine();
                Settings.DataFile_Name_setDataStructure(EXTENSION_PAYROLL_USERDEF_ENTITLE_NAMES, dataLine);
                FileHD6.Close();
            }
            if (File.Exists(dataPath + EXTENSION_INVENTORY_NAMES))
            {
                Variables.globalSettings.InventorySettings.AdditionalFields = new ADDITIONAL_FIELD_NAMES();
                FileHD7 = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_INVENTORY_NAMES, "FM_READ"));
                DoInventoryNames = true;
                dataLine = FileHD7.ReadLine();
                Settings.DataFile_Name_setDataStructure(EXTENSION_INVENTORY_NAMES, dataLine);
                FileHD7.Close();
            }
            if (File.Exists(dataPath + EXTENSION_PROJECT_NAMES))
            {
                Variables.globalSettings.ProjectSettings.ProjectTitle = null;
                Variables.globalSettings.ProjectSettings.AdditionalFields = new ADDITIONAL_FIELD_NAMES();
                FileHD8 = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_PROJECT_NAMES, "FM_READ"));
                DoProjectNames = true;
                dataLine = FileHD8.ReadLine();
                Settings.DataFile_Name_setDataStructure(EXTENSION_PROJECT_NAMES, dataLine);
                FileHD8.Close();
            }
            if (File.Exists(dataPath + EXTENSION_COMPANY_NAMES))
            {
                Variables.globalSettings.CompanySettings.additionalInformationDate = null;
                Variables.globalSettings.CompanySettings.additionalInformationField = null;
                FileHD9 = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_COMPANY_NAMES, "FM_READ"));
                DoCompanyNames = true;
                dataLine = FileHD9.ReadLine();
                Settings.DataFile_Name_setDataStructure(EXTENSION_COMPANY_NAMES, dataLine);
                FileHD9.Close();
            }

            if (DoCompanyNames)
            {
                Settings._SA_SetCompanyNameSettings();
            }
            if (DoGeneralNames)
            {
                Settings._SA_SetGeneralNameAndLinkedAccountSettings(false);
            }
            if (DoReceivablesNames)
            {
                Settings._SA_SetReceivablesGeneralNameSettings(false);
            }
            if (DoAdditionalPayrollNames)
            {
                Settings._SA_SetPayrollAdditionalNameSettings(false);
            }
            if (DoPayrollIncomesDeductionsNames)
            {
                Settings._SA_SetPayrollIncomeDeductionNameSettings(false);
                Settings._SA_SetPayrollAdditionalNameSettings(false);	// have to do this also to get the prov Tax and workers comp
            }
            if (DoPayablesNames)
            {
                Settings._SA_Set_AllPayableSettings(false);
            }
            if (DoInventoryNames)
            {
                Settings._SA_SetInventoryNameAndLinkedAcctSettings(false);
            }
            if (DoProjectNames)
            {
                Settings._SA_Set_AllProjectSettings(false);
            }

            Settings.repo.OK.Click();            
        }

        public static void DataFile_Shipper_ReadFile(string sDataLocation, string fileCounter)
        {
            //string FUNCTION_ALIAS = "Shippers";
            string EXTENSION_COMPANY_SHIPPERS = ".hdr";
            StreamReader FileHDR;	// File handle for Project tab info

            // Local variables
            string dataLine;	        // Stores the current field data from file
            string dataPath;            // The name and path of the data file


            // Get the data path from file
            dataPath = sDataLocation + "SHIPPERS" + fileCounter;

            Variables.globalSettings.CompanySettings.ShipperSettings = new TRACK_SHIPMENTS();

            // Open all data files
            if (File.Exists(dataPath + EXTENSION_COMPANY_SHIPPERS))
            {
                FileHDR = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_COMPANY_SHIPPERS, "FM_READ"));
                dataLine = FileHDR.ReadLine();
                Settings.DataFile_Shipper_setDataStructure(dataLine);
                FileHDR.Close();
            }

            Settings._SA_SetCompanyShipperSettings();
        }

        public static void DataFile_PriceList_ReadFile(string sDataLocation, string fileCounter)
        {
            //string FUNCTION_ALIAS = "PrcLst";
            string EXTENSION_INVENTORY_PRICELIST = ".hdr";
            StreamReader FileHDR;	// File handle for Project tab info

            // Local variables
            string dataLine;	        // Stores the current field data from file
            string dataPath;	        // The name and path of the data file

            // Get the data path from file
            dataPath = sDataLocation + "PrcLst" + fileCounter;

            Variables.globalSettings.InventorySettings.PriceList.Clear();	// make empty set so that we can append to it. New will give us a value in position1

            // Open all data files
            if (File.Exists(dataPath + EXTENSION_INVENTORY_PRICELIST))
            {
                FileHDR = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_INVENTORY_PRICELIST, "FM_READ"));
                while ((dataLine = FileHDR.ReadLine()) != null)
                {
                    Settings.DataFile_PriceList_setDataStructure(dataLine);
                }
                FileHDR.Close();
            }

            Settings._SA_SetInventoryPriceListSettings();
        }

        public static void DataFile_APLinkAccounts_ReadFile(string sDataLocation, string fileCounter)
        {
            //string FUNCTION_ALIAS = "lnkAccts";
            string EXTENSION_HEADER = ".hd2";
            string EXTENSION_BANK_ACCOUNTS_CONTAINER = ".cnt";

            StreamReader FileHD2;	// File handle for header
            StreamReader FileCNT;	// File handle for bank accounts container

            // Local variables
            string dataLine;	// Stores the current field data from file
            string dataPath;    // The name and path of the data file

            // Get the data path from file
            dataPath = sDataLocation + "lnkAccts" + fileCounter;

            // NC - no longer needed as been done with declarations
            //Variables.GlobalSettings.PayableSettings.apAcct = new GL_ACCOUNT();
            //Variables.GlobalSettings.PayableSettings.freightAcct = new GL_ACCOUNT();
            //Variables.GlobalSettings.PayableSettings.earlyPayDiscountAcct = new GL_ACCOUNT();
            //Variables.GlobalSettings.PayableSettings.prepaymentAcct = new GL_ACCOUNT();
            //Variables.GlobalSettings.PayableSettings.CurrencyAccounts.Clear();

            // Open all data files
            if (File.Exists(dataPath + EXTENSION_HEADER))
            {
                FileHD2 = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_HEADER, "FM_READ"));
                dataLine = FileHD2.ReadLine();
                Settings.DataFile_APLinkAccounts_setDataStructure(EXTENSION_HEADER, dataLine);
                FileHD2.Close();
            }
            if (File.Exists(dataPath + EXTENSION_BANK_ACCOUNTS_CONTAINER))
            {
                FileCNT = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_BANK_ACCOUNTS_CONTAINER, "FM_READ"));
                while ((dataLine = FileCNT.ReadLine()) != null)
                {
                    Settings.DataFile_APLinkAccounts_setDataStructure(EXTENSION_BANK_ACCOUNTS_CONTAINER, dataLine);
                }
                FileCNT.Close();
            }

            Settings._SA_Set_AllPayableSettings();
        }

        public void DataFile_ARLinkAccounts_ReadFile(string sDataLocation, string fileCounter)
        {
            //string FUNCTION_ALIAS = "lnkAccts";
            string EXTENSION_HEADER = ".hd3";
            string EXTENSION_BANK_ACCOUNTS_CONTAINER = ".cnt2";

            StreamReader FileHD3;	// File handle for header
            StreamReader FileCNT2;	// File handle for bank accounts container

            // Local variables
            string dataLine;	    // Stores the current field data from file
            string dataPath;        // The name and path of the data file

            // Get the data path from file
            dataPath = sDataLocation + "lnkAccts" + fileCounter;

            // NC - no longer needed as been done with declarations
            //Variables.GlobalSettings.ReceivableSettings.arAcct = new GL_ACCOUNT();
            //Variables.GlobalSettings.ReceivableSettings.freightAcct = new GL_ACCOUNT();
            //Variables.GlobalSettings.ReceivableSettings.earlyPayDiscountAcct = new GL_ACCOUNT();
            //Variables.GlobalSettings.ReceivableSettings.depositsAcct = new GL_ACCOUNT();
            //Variables.GlobalSettings.ReceivableSettings.CurrencyAccounts.Clear();

            // Open all data files
            if (File.Exists(dataPath + EXTENSION_HEADER))
            {
                FileHD3 = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_HEADER, "FM_READ"));
                dataLine = FileHD3.ReadLine();
                Settings.DataFile_ARLinkAccounts_setDataStructure(EXTENSION_HEADER, dataLine);
                FileHD3.Close();
            }
            if (File.Exists(dataPath + EXTENSION_BANK_ACCOUNTS_CONTAINER))
            {
                FileCNT2 = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_BANK_ACCOUNTS_CONTAINER, "FM_READ"));
                while ((dataLine = FileCNT2.ReadLine()) != null)
                {
                    Settings.DataFile_ARLinkAccounts_setDataStructure(EXTENSION_BANK_ACCOUNTS_CONTAINER, dataLine);
                }
                FileCNT2.Close();
            }

            Settings._SA_SetReceivablesLinkedAcctSettings();
        }

        public static void DataFile_GLLinkAccounts_ReadFile(string sDataLocation, string fileCounter)
        {
            //string FUNCTION_ALIAS = "lnkAccts";
            string EXTENSION_HEADER = ".hdr";

            StreamReader FileHDR;	// File handle for header

            // Local variables
            string dataLine;        // Stores the current field data from file
            string dataPath;        // The name and path of the data file

            // Get the data path from file
            dataPath = sDataLocation + "lnkAccts" + fileCounter;

            Variables.globalSettings.GeneralSettings.RetainedEarnings = new GL_ACCOUNT();

            // Open all data files
            if (File.Exists(dataPath + EXTENSION_HEADER))
            {
                FileHDR = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_HEADER, "FM_READ"));
                dataLine = FileHDR.ReadLine();
                Settings.DataFile_GLLinkAccounts_setDataStructure(EXTENSION_HEADER, dataLine);
                FileHDR.Close();
            }

            Settings._SA_SetGeneralNameAndLinkedAccountSettings();
        }

        public void DataFile_InvLinkAccounts_ReadFile(string sDataLocation, string fileCounter)
        {
            //string FUNCTION_ALIAS = "lnkAccts";
            string EXTENSION_HEADER = ".hd8";

            StreamReader FileHD8;	// File handle for header

            // Local variables
            string dataLine;	// Stores the current field data from file
            string dataPath;    // The name and path of the data file


            // Get the data path from file
            dataPath = sDataLocation + "lnkAccts" + fileCounter;

            Variables.globalSettings.InventorySettings.itemAssemblyCosts = new GL_ACCOUNT();
            Variables.globalSettings.InventorySettings.adjustmentWriteOff = new GL_ACCOUNT();

            // Open all data files
            if (File.Exists(dataPath + EXTENSION_HEADER))
            {
                FileHD8 = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_HEADER, "FM_READ"));
                dataLine = FileHD8.ReadLine();
                Settings.DataFile_InvLinkAccounts_setDataStructure(EXTENSION_HEADER, dataLine);
                FileHD8.Close();
            }

            Settings._SA_SetInventoryNameAndLinkedAcctSettings();
        }

        public static void DataFile_PayrollLinkAccounts_ReadFile(string sDataLocation, string fileCounter)
        {
            //string FUNCTION_ALIAS = "lnkAccts";
            string EXTENSION_INCOMES = ".hd4";
            string EXTENSION_DEDUCTIONS = ".hd5";
            string EXTENSION_TAXES = ".hd6";
            string EXTENSION_USER_DEFINED_EXPENSES = ".hd7";

            StreamReader FileHD4;	// File handle for incomes page
            StreamReader FileHD5;	// File handle for deductions page
            StreamReader FileHD6;	// File handle for taxes page
            StreamReader FileHD7;	// File handle for user-defined expenses page

            // Local variables
            string dataLine;	    // Stores the current field data from file
            string dataPath;        // The name and path of the data file

            // Get the data path from file
            dataPath = sDataLocation + "lnkAccts" + fileCounter;

            // NC - no longer needed as been done with declarations
            //Variables.GlobalSettings.PayrollSettings.IncomeAccounts = new INCOME_SPECIFIC_ACCOUNTS();            
            //for (int x = 0; x < 20; x++)
            //{
            //    Variables.GlobalSettings.PayrollSettings.AdditionalIncome[x].LinkedAccount = new GL_ACCOUNT();
            //    Variables.GlobalSettings.PayrollSettings.AdditionalDeduction[x].LinkedAccount = new GL_ACCOUNT();
            //}            
            //Variables.GlobalSettings.PayrollSettings.TaxAccounts = new TAXES_LINKED_ACCOUNTS();
            //Variables.GlobalSettings.PayrollSettings.UserDefinedAccounts = new USER_DEFINED_ACCOUNTS();

            // Open all data files
            if (File.Exists(dataPath + EXTENSION_INCOMES))
            {
                FileHD4 = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_INCOMES, "FM_READ"));
                dataLine = FileHD4.ReadLine();
                Settings.DataFile_PayrollLinkAccounts_setDataStructure(EXTENSION_INCOMES, dataLine);
                FileHD4.Close();
            }
            if (File.Exists(dataPath + EXTENSION_DEDUCTIONS))
            {
                FileHD5 = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_DEDUCTIONS, "FM_READ"));
                dataLine = FileHD5.ReadLine();
                Settings.DataFile_PayrollLinkAccounts_setDataStructure(EXTENSION_DEDUCTIONS, dataLine);
                FileHD5.Close();
            }
            if (File.Exists(dataPath + EXTENSION_TAXES))
            {
                FileHD6 = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_TAXES, "FM_READ"));
                dataLine = FileHD6.ReadLine();
                Settings.DataFile_PayrollLinkAccounts_setDataStructure(EXTENSION_TAXES, dataLine);
                FileHD6.Close();
            }
            if (File.Exists(dataPath + EXTENSION_USER_DEFINED_EXPENSES))
            {
                FileHD7 = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_USER_DEFINED_EXPENSES, "FM_READ"));
                dataLine = FileHD7.ReadLine();
                Settings.DataFile_PayrollLinkAccounts_setDataStructure(EXTENSION_USER_DEFINED_EXPENSES, dataLine);
                FileHD7.Close();
            }

            Settings._SA_SetPayrollLinkedIncomeSettings(false);
            Settings._SA_SetPayrollLinkedDeductionSettings(false);
            Settings._SA_SetPayrollLinkedTaxesSettings(false);
            Settings._SA_SetPayrollLinkedUserDefinedSettings(true);
        }

        public static void DataFile_PayrollJob_ReadFile(string sDataLocation, string fileCounter)
        {
            //string FUNCTION_ALIAS = "JobCat";
            string EXTENSION_CATEGORIES = ".hdr";
            string EXTENSION_CATEGORIES_ASSIGNMENTS = ".dt1";
            StreamReader FileHDR;	// File handle for Project tab info
            StreamReader FileDT1;

            // Local variables
            string dataLine;	    // Stores the current field data from file
            string dataPath;        // The name and path of the data file


            // Get the data path from file
            dataPath = sDataLocation + "JOBCAT" + fileCounter;

            Variables.globalSettings.PayrollSettings.JobCategories.Clear();

            // Open all data files
            if (File.Exists(dataPath + EXTENSION_CATEGORIES))
            {
                FileHDR = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_CATEGORIES, "FM_READ"));
                while ((dataLine = FileHDR.ReadLine()) != null)
                {
                    Settings.DataFile_PayrollJob_setDataStructure(EXTENSION_CATEGORIES, dataLine);
                }
                FileHDR.Close();
            }

            if (File.Exists(dataPath + EXTENSION_CATEGORIES_ASSIGNMENTS))
            {
                FileDT1 = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_CATEGORIES_ASSIGNMENTS, "FM_READ"));
                while ((dataLine = FileDT1.ReadLine()) != null)
                {
                    Settings.DataFile_PayrollJob_setDataStructure(EXTENSION_CATEGORIES_ASSIGNMENTS, dataLine);
                }
                FileDT1.Close();
            }

            Settings._SA_SetPayrollJobSettings();
        }

        public void DataFile_MainSettings_ReadFile(string sDataLocation, string fileCounter)
        {
            DataFile_MainSettings_ReadFile(sDataLocation, fileCounter, true);
        }

        public void DataFile_MainSettings_ReadFile(string sDataLocation, string fileCounter, bool bSetSettings)
        {
            //string FUNCTION_ALIAS = "settings";
            string EXTENSION_COMPANY_SYSTEM = ".hdr";
            string EXTENSION_COMPANY_FEATURES = ".hd2";
            string EXTENSION_COMPANY_FORMS = ".hd3";
            string EXTENSION_COMPANY_EMAIL = ".hd4";
            string EXTENSION_COMPANY_DATES = ".hd5";
            string EXTENSION_GENERAL_BUDGET = ".hd6";
            string EXTENSION_GENERAL_NUMBERING = ".hd7";
            string EXTENSION_PAYABLES_OPTIONS = ".hd8";
            string EXTENSION_PAYABLES_DUTY = ".hd9";
            string EXTENSION_RECEIVABLES_OPTIONS = ".hd11";
            string EXTENSION_RECEIVABLES_COMMENTS = ".hd12";
            string EXTENSION_PAYROLL_INCOMES = ".hd13";
            string EXTENSION_PAYROLL_DEDUCTIONS = ".hd14";
            string EXTENSION_PAYROLL_ENTITLEMENTS = ".hd15";
            string EXTENSION_PAYROLL_REMITTANCE = ".hd16";
            string EXTENSION_INVSERV_OPTIONS = ".hd17";
            string EXTENSION_PROJECT_BUDGET = ".hd18";
            string EXTENSION_PROJECT_ALLOCATION = ".hd19";

            StreamReader FileHDR;
            StreamReader FileHD2;
            StreamReader FileHD3;
            StreamReader FileHD4;
            StreamReader FileHD5;
            StreamReader FileHD6;
            StreamReader FileHD7;
            StreamReader FileHD8;
            StreamReader FileHD9;
            StreamReader FileHD11;
            StreamReader FileHD12;
            StreamReader FileHD13;
            StreamReader FileHD14;
            StreamReader FileHD15;
            StreamReader FileHD16;
            StreamReader FileHD17;
            StreamReader FileHD18;
            StreamReader FileHD19;

            // Local variables
            string dataLine;	// Stores the current field data from file
            string dataPath;    // The name and path of the data file

            // Get the data path from file
            dataPath = sDataLocation + "settings" + fileCounter;

            // Open all data files
            if (File.Exists(dataPath + EXTENSION_COMPANY_SYSTEM))
            {
                FileHDR = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_COMPANY_SYSTEM, "FM_READ"));
                dataLine = FileHDR.ReadLine();
                Settings.DataFile_MainSettings_setDataStructure(EXTENSION_COMPANY_SYSTEM, dataLine);
                FileHDR.Close();
            }
            if (File.Exists(dataPath + EXTENSION_COMPANY_FEATURES))
            {
                FileHD2 = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_COMPANY_FEATURES, "FM_READ"));
                dataLine = FileHD2.ReadLine();
                Settings.DataFile_MainSettings_setDataStructure(EXTENSION_COMPANY_FEATURES, dataLine);
                FileHD2.Close();
            }
            if (File.Exists(dataPath + EXTENSION_COMPANY_FORMS))
            {
                FileHD3 = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_COMPANY_FORMS, "FM_READ"));
                dataLine = FileHD3.ReadLine();
                Settings.DataFile_MainSettings_setDataStructure(EXTENSION_COMPANY_FORMS, dataLine);
                FileHD3.Close();
            }
            if (File.Exists(dataPath + EXTENSION_COMPANY_EMAIL))
            {
                FileHD4 = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_COMPANY_EMAIL, "FM_READ"));
                dataLine = FileHD4.ReadLine();
                Settings.DataFile_MainSettings_setDataStructure(EXTENSION_COMPANY_EMAIL, dataLine);
                FileHD4.Close();
            }
            if (File.Exists(dataPath + EXTENSION_COMPANY_DATES))
            {
                FileHD5 = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_COMPANY_DATES, "FM_READ"));
                dataLine = FileHD5.ReadLine();
                Settings.DataFile_MainSettings_setDataStructure(EXTENSION_COMPANY_DATES, dataLine);
                FileHD5.Close();
            }
            if (File.Exists(dataPath + EXTENSION_GENERAL_BUDGET))
            {
                FileHD6 = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_GENERAL_BUDGET, "FM_READ"));
                dataLine = FileHD6.ReadLine();
                Settings.DataFile_MainSettings_setDataStructure(EXTENSION_GENERAL_BUDGET, dataLine);
                FileHD6.Close();
            }
            if (File.Exists(dataPath + EXTENSION_GENERAL_NUMBERING))
            {
                FileHD7 = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_GENERAL_NUMBERING, "FM_READ"));
                dataLine = FileHD7.ReadLine();
                Settings.DataFile_MainSettings_setDataStructure(EXTENSION_GENERAL_NUMBERING, dataLine);
                FileHD7.Close();
            }
            if (File.Exists(dataPath + EXTENSION_PAYABLES_OPTIONS))
            {
                FileHD8 = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_PAYABLES_OPTIONS, "FM_READ"));
                dataLine = FileHD8.ReadLine();
                Settings.DataFile_MainSettings_setDataStructure(EXTENSION_PAYABLES_OPTIONS, dataLine);
                FileHD8.Close();
            }
            if (File.Exists(dataPath + EXTENSION_PAYABLES_DUTY))
            {
                FileHD9 = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_PAYABLES_DUTY, "FM_READ"));
                dataLine = FileHD9.ReadLine();
                Settings.DataFile_MainSettings_setDataStructure(EXTENSION_PAYABLES_DUTY, dataLine);
                FileHD9.Close();
            }
            if (File.Exists(dataPath + EXTENSION_RECEIVABLES_OPTIONS))
            {
                FileHD11 = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_RECEIVABLES_OPTIONS, "FM_READ"));
                dataLine = FileHD11.ReadLine();
                Settings.DataFile_MainSettings_setDataStructure(EXTENSION_RECEIVABLES_OPTIONS, dataLine);
                FileHD11.Close();
            }
            if (File.Exists(dataPath + EXTENSION_RECEIVABLES_COMMENTS))
            {
                FileHD12 = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_RECEIVABLES_COMMENTS, "FM_READ"));
                dataLine = FileHD12.ReadLine();
                Settings.DataFile_MainSettings_setDataStructure(EXTENSION_RECEIVABLES_COMMENTS, dataLine);
                FileHD12.Close();
            }
            if (File.Exists(dataPath + EXTENSION_PAYROLL_INCOMES))
            {
                FileHD13 = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_PAYROLL_INCOMES, "FM_READ"));
                dataLine = FileHD13.ReadLine();
                Settings.DataFile_MainSettings_setDataStructure(EXTENSION_PAYROLL_INCOMES, dataLine);
                FileHD13.Close();
            }
            if (File.Exists(dataPath + EXTENSION_PAYROLL_DEDUCTIONS))
            {
                FileHD14 = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_PAYROLL_DEDUCTIONS, "FM_READ"));
                dataLine = FileHD14.ReadLine();
                Settings.DataFile_MainSettings_setDataStructure(EXTENSION_PAYROLL_DEDUCTIONS, dataLine);
                FileHD14.Close();
            }
            if (File.Exists(dataPath + EXTENSION_PAYROLL_ENTITLEMENTS))
            {
                FileHD15 = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_PAYROLL_ENTITLEMENTS, "FM_READ"));
                dataLine = FileHD15.ReadLine();
                Settings.DataFile_MainSettings_setDataStructure(EXTENSION_PAYROLL_ENTITLEMENTS, dataLine);
                FileHD15.Close();
            }
            if (File.Exists(dataPath + EXTENSION_PAYROLL_REMITTANCE))
            {
                FileHD16 = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_PAYROLL_REMITTANCE, "FM_READ"));
                dataLine = FileHD16.ReadLine();
                Settings.DataFile_MainSettings_setDataStructure(EXTENSION_PAYROLL_REMITTANCE, dataLine);
                FileHD16.Close();
            }
            if (File.Exists(dataPath + EXTENSION_INVSERV_OPTIONS))
            {
                FileHD17 = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_INVSERV_OPTIONS, "FM_READ"));
                dataLine = FileHD17.ReadLine();
                Settings.DataFile_MainSettings_setDataStructure(EXTENSION_INVSERV_OPTIONS, dataLine);
                FileHD17.Close();
            }
            if (File.Exists(dataPath + EXTENSION_PROJECT_BUDGET))
            {
                FileHD18 = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_PROJECT_BUDGET, "FM_READ"));
                dataLine = FileHD18.ReadLine();
                Settings.DataFile_MainSettings_setDataStructure(EXTENSION_PROJECT_BUDGET, dataLine);
                FileHD18.Close();
            }
            if (File.Exists(dataPath + EXTENSION_PROJECT_ALLOCATION))
            {
                FileHD19 = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_PROJECT_ALLOCATION, "FM_READ"));
                dataLine = FileHD19.ReadLine();
                Settings.DataFile_MainSettings_setDataStructure(EXTENSION_PROJECT_ALLOCATION, dataLine);
                FileHD19.Close();
            }

            if (bSetSettings)
            {
                Settings._SA_SetCompanySystemSettings(false);
                Settings._SA_SetCompanyBackupSettings(false);
                Settings._SA_SetCompanyFeatureSettings(false);
                Settings._SA_SetCompanyFormsSettings(false);
                Settings._SA_SetCompanyEmailSettings(false);
                Settings._SA_SetCompanyDateSettings(false);
                Settings._SA_SetGeneralBudgetSettings(false);
                Settings._SA_SetGeneralNumberingSettings();
                Settings._SA_Set_AllPayableSettings(false);
                Settings._SA_SetReceivablesOptionsAndDiscountSettings(false);               
                Settings._SA_SetReceivablesCommentSettings(false);
                Settings._SA_SetPayrollDeductionSettings(false);
                Settings._SA_SetPayrollTaxesSettings(false);
                Settings._SA_SetPayrollEntitlementSettings(false);
                Settings._SA_SetPayrollRemittanceSettings(false);
                Settings._SA_SetInventoryOptionSettings(false);
                Settings._SA_Set_AllProjectSettings(true);
            }
        }       


        public static void InitializeMainSettings()
        {
            Variables.globalSettings.CompanySettings.SystemSettings = new SYSTEM();
            Variables.globalSettings.CompanySettings.BackupSettings = new BACKUP();
            Variables.globalSettings.CompanySettings.FeatureSettings = new FEATURES();
            Variables.globalSettings.CompanySettings.FormSettings = new FORMS();
            Variables.globalSettings.CompanySettings.EmailSettings = new EMAIL();
            Variables.globalSettings.CompanySettings.DateSettings = new DATE_FORMAT();
            Variables.globalSettings.GeneralSettings.budgetRevAndExAccts = null;
            //Variables.GlobalSettings.GeneralSettings.budgetFrequency = null;
            Variables.globalSettings.GeneralSettings.Numbering = new GENERAL_NUMBERING();
            Variables.globalSettings.PayableSettings.agingPeriod1= null;
            Variables.globalSettings.PayableSettings.agingPeriod2= null;
            Variables.globalSettings.PayableSettings.agingPeriod3= null;
            Variables.globalSettings.PayableSettings.calculateDiscountsBeforeTax= null;
            Variables.globalSettings.PayableSettings.trackDutyOnImportedItems= null;
            Variables.globalSettings.PayableSettings.importDutyAcct = new GL_ACCOUNT();
            Variables.globalSettings.ReceivableSettings.agingPeriod1 = null;
            Variables.globalSettings.ReceivableSettings.agingPeriod2 = null;
            Variables.globalSettings.ReceivableSettings.agingPeriod3 = null;
            Variables.globalSettings.ReceivableSettings.interestCharges = null;
            Variables.globalSettings.ReceivableSettings.interestPercent = null;
            Variables.globalSettings.ReceivableSettings.interestDays = null;
            Variables.globalSettings.ReceivableSettings.taxCodeForNewCustomers = new TAX_CODE();
            Variables.globalSettings.ReceivableSettings.printSalesperson = null;
            Variables.globalSettings.ReceivableSettings.salesInvoiceComment = null;
            Variables.globalSettings.ReceivableSettings.salesOrderComment = null;
            Variables.globalSettings.ReceivableSettings.salesQuoteComment = null;
            Variables.globalSettings.PayrollSettings.IncomeSettings = new PAYROLL_INCOME_SETTINGS();
            Variables.globalSettings.PayrollSettings.Deductions = new List<PAYROLL_DEDUCTION>();
            Variables.globalSettings.PayrollSettings.TaxSettings = new PAYROLL_TAXES_SETTINGS();
            Variables.globalSettings.PayrollSettings.EntitlementSettings = new PAYROLL_ENTITLEMENT_SETTINGS();
            Variables.globalSettings.PayrollSettings.Remittances = new List<PAYROLL_REMITTANCE>();
            //Variables.GlobalSettings.InventorySettings.inventoryCostingMethod = null;
            //Variables.GlobalSettings.InventorySettings.profitMethod = null;
            //Variables.GlobalSettings.InventorySettings.sortMethod = null;
            Variables.globalSettings.InventorySettings.allowInventoryLevelsToGoBelowZero = null;
            Variables.globalSettings.ProjectSettings.budgetProjects = null;
            //Variables.GlobalSettings.ProjectSettings.budgetPeriodFrequency = null;
            //Variables.GlobalSettings.ProjectSettings.payrollAllocationMethod = null;
            //Variables.GlobalSettings.ProjectSettings.otherAllocationMethod = null;
            Variables.globalSettings.ProjectSettings.warnIfAllocationIsNotComplete = null;
            Variables.globalSettings.ProjectSettings.allowAccessToAllocateFieldUsingTab = null;			
        }

		public static object DataFile_Taxes_setDataStructure(string extension, string dataLine)
		{	
			return DataFile_Taxes_setDataStructure(extension, dataLine, null, null, null, null);
		}
		
		public static object DataFile_Taxes_setDataStructure(string extension, string dataLine, TAX Tax)
		{
			return DataFile_Taxes_setDataStructure(extension, dataLine, Tax, null, null, null);
		}
		
		public static object DataFile_Taxes_setDataStructure(string extension, string dataLine, TAX Tax, TAX_CODE TaxCode)
		{
			return DataFile_Taxes_setDataStructure(extension, dataLine, Tax, TaxCode, null, null);
		}
		
		public static object DataFile_Taxes_setDataStructure(string extension, string dataLine, TAX Tax, TAX_CODE TaxCode, List<TAX> Taxes)
		{
			return DataFile_Taxes_setDataStructure(extension, dataLine, Tax, TaxCode, Taxes, null);
		}
		
		public static object DataFile_Taxes_setDataStructure(string extension, string dataLine, TAX Tax, TAX_CODE TaxCode, List<TAX> Taxes, List<TAX_CODE> TaxCodes)
		{
			object T = null;
			GL_ACCOUNT TempGL;
			
			if (Functions.GoodData(Tax))
			{
				T = Tax;
			}
			else if (Functions.GoodData(TaxCode))
			{
				T = TaxCode;
			}
			else if (Functions.GoodData(Taxes) && !Functions.GoodData(TaxCodes))
			{
				T = Taxes;
			}
			else if (Functions.GoodData(TaxCodes) && Functions.GoodData(Taxes))
			{
				T = TaxCodes;
			}
			else
			{
				Functions.Verify(false, true, "Correct values sent to function");
			}
			
			bool bFound = false;
			string sTax;
			int x;	// counter outside loop
			
			string taxObjectType;
			bool isList = false;
			// Note, GetType do not return List with the type
			if (T.GetType().Name.Contains("List"))
			{
				isList = true;
				taxObjectType = T.GetType().GetGenericArguments()[0].Name;
			}
			else
			{
				taxObjectType = T.GetType().Name;
			}
			
			switch (taxObjectType)
			{
				case "TAX": // .HDR
					if (isList)
					{
						sTax = Functions.GetField(dataLine, ",", 1);
						for (x = 0; x < Taxes.Count; x++)
						{
							if (sTax == Taxes[x].taxName)
							{
								bFound = true;
								break;
							}
						}
						
						if (bFound)
						{
							Taxes[x].TaxAuthoritiesToBeCharged.Add(Functions.GetField(dataLine, ",", 3));																				
						}
						else
						{
							Functions.Verify(false, true, "Tax found in List");
						}
						T = Taxes;											
					}
					else
					{
						((TAX)T).taxName = Functions.GetField(dataLine, ",", 1);
						((TAX)T).taxID = Functions.GetField(dataLine, ";", 2);
						if (Functions.GetField(dataLine, ",", 3) == "Yes")
						{
							((TAX)T).exempt = true;
						}
						else
						{
							((TAX)T).exempt = false;
						}
						if (Functions.GetField(dataLine, ",", 4) == "Yes")
						{
							((TAX)T).taxable = true;
						}
						else
						{
							((TAX)T).taxable = false;
						}
						TempGL = new GL_ACCOUNT();
						TempGL.acctNumber = Functions.GetField(dataLine, ",", 5);
						((TAX)T).acctTrackPurchases = TempGL;
						TempGL = new GL_ACCOUNT();
						TempGL.acctNumber = Functions.GetField(dataLine, ",", 6);
						((TAX)T).acctTrackSales = TempGL;
						if (Functions.GetField(dataLine, ",", 7) == "Yes")
						{
							((TAX)T).reportOnTaxes = true;
						}
						else
						{
							((TAX)T).reportOnTaxes = false;
						}
					}
					break;					
				case "TAX_CODE":	// .DT1
					int iTaxCodeLine = 1;
					if (isList)
					{
						bFound = false;
						
						string sTaxCode = Functions.GetField(dataLine, ",", 1);
						for (x = 0; x < TaxCodes.Count; x++)
						{
							if (TaxCodes[x].code == sTaxCode)
							{
								bFound = true;
								break;
							}							
						}
						if (bFound)
						{
							iTaxCodeLine = x;
						}
						else
						{
							Functions.Verify(false, true, "Valid Tax Code located");
						}
						
						bFound = false;
						TAX_DETAIL TD = new TAX_DETAIL();
						sTax = Functions.GetField(dataLine, ",", 3);
						for (x = 0; x < Taxes.Count; x++)
						{
							if (sTax == Taxes[x].taxName)
							{
								bFound = true;
								break;
							}						
						}
						if (bFound)
						{
							TD.Tax = Taxes[x];
						}
						else
						{
							Functions.Verify(false, true, "Tax found in list");
						}
						
						string sTaxStatus = Functions.GetField(dataLine, ",", 4);
						switch (sTaxStatus)
						{
							case "Taxable":
								TD.taxStatus = TAX_STATUS.TAX_STATUS_TAXABLE;
								break;
							case "Non-taxable":
								TD.taxStatus = TAX_STATUS.TAX_STATUS_NON_TAXABLE;
								break;
							case "Exempt":
								TD.taxStatus = TAX_STATUS.TAX_STATUS_EXEMPT;
								break;
							default:
								{
									Functions.Verify(false, true, "Valid value for Tax status");
									break;
								}
						}
						TD.rate = Functions.GetField(dataLine, ",", 5);
						if (Functions.GetField(dataLine, ",", 6) == "Yes")
						{
							TD.includedInPrice = true;
						}
						else
						{
							TD.includedInPrice = false;
						}
						if (Functions.GetField(dataLine, ",", 7) == "Yes")
						{
							TD.isRefundable = true;
						}
						else
						{
							TD.isRefundable = false;
						}
						
						TaxCodes[iTaxCodeLine].TaxDetails.Add(TD);
						T = TaxCodes;
					}
					else
					{
						((TAX_CODE)T).code = Functions.GetField(dataLine, ",", 1);
						((TAX_CODE)T).description = Functions.GetField(dataLine, ",", 2);
						
						switch(Functions.GetField(dataLine, ",", 3))
						{
							case "All journals":
								((TAX_CODE)T).useIn = TAX_USED_IN.TAX_ALL_JOURNALS;
								break;
							case "Sales":
								((TAX_CODE)T).useIn = TAX_USED_IN.TAX_SALES;
								break;
							case "Purchases":
								((TAX_CODE)T).useIn = TAX_USED_IN.TAX_PURCHASES;
								break;								
						}
					}
					break;
			}
			return T;
		}
        
        public static void DataFile_CompanyInfo_setDataStructure(string extension, string dataLine)
        {
            switch(extension.ToUpper())
            {
                case ".HDR":
                    Variables.globalSettings.CompanyInformation.companyName = Functions.GetField(dataLine, ",", 1);
                    Variables.globalSettings.CompanyInformation.Address.street1 = Functions.GetField(dataLine, ",", 2);
                    Variables.globalSettings.CompanyInformation.Address.street2 = Functions.GetField(dataLine, ",", 3);
                    Variables.globalSettings.CompanyInformation.Address.city = Functions.GetField(dataLine, ",", 4);
                    Variables.globalSettings.CompanyInformation.Address.province =Functions.GetField(dataLine, ",", 5);
                    Variables.globalSettings.CompanyInformation.Address.postalCode = Functions.GetField(dataLine, ",", 6);
                    Variables.globalSettings.CompanyInformation.Address.country = Functions.GetField(dataLine, ",", 7);
                    Variables.globalSettings.CompanyInformation.Address.phone1 = Functions.GetField(dataLine, ",", 8);
                    Variables.globalSettings.CompanyInformation.Address.phone2 = Functions.GetField(dataLine, ",", 9);
                    Variables.globalSettings.CompanyInformation.Address.fax = Functions.GetField(dataLine, ",", 10);
                    Variables.globalSettings.CompanyInformation.businessNum = Functions.GetField(dataLine, ",", 11);
                    Variables.globalSettings.CompanyInformation.companyType = Functions.GetField(dataLine, ",", 12);
                    Variables.globalSettings.CompanyInformation.Address.provinceCode = Functions.GetField(dataLine, ",", 13);
                    Variables.globalSettings.CompanyInformation.fiscalStart = Functions.GetField(dataLine, ",", 14);
                    Variables.globalSettings.CompanyInformation.fiscalEnd = Functions.GetField(dataLine, ",", 15);
                    Variables.globalSettings.CompanyInformation.earliestTransaction = Functions.GetField(dataLine, ",", 16);
                    Variables.globalSettings.CompanySettings.LogoLocation = Functions.GetField(dataLine, ",", 17);
                    Variables.globalSettings.CompanyInformation.federalID = Functions.GetField(dataLine, ",", 18);
                    Variables.globalSettings.CompanyInformation.stateID = Functions.GetField(dataLine, ",", 19);
                    break;
                default:
                    {
                        Functions.Verify(false, true, "Valid value sent");
                        break;
                    }
            }
        }
        
        public static void DataFile_CreditCard_setDataStructure(string extension, string dataLine)
        {
            switch (extension.ToUpper())
            {
                case ".DT1":
                    CREDIT_CARD_USED CCU = new CREDIT_CARD_USED();
                    CCU.CardName = Functions.GetField(dataLine, ",", 1);
                    CCU.PayableAccount.acctNumber = Functions.GetField(dataLine, ",", 2);
                    CCU.ExpenseAccount.acctNumber = Functions.GetField(dataLine, ",", 3);
                    Variables.globalSettings.CompanySettings.CreditCardSettings.CardsUsed.Add(CCU);
                    break;
                case ".HDR":
                    CREDIT_CARD_ACCEPTED CCA = new CREDIT_CARD_ACCEPTED();
                    CCA.CardName = Functions.GetField(dataLine, ",", 1);
                    CCA.Discount = Functions.GetField(dataLine, ",", 2);
                    CCA.ExpenseAccount.acctNumber = Functions.GetField(dataLine, ",", 3);
                    CCA.AssetAccount.acctNumber = Functions.GetField(dataLine, ",", 4);
                    Variables.globalSettings.CompanySettings.CreditCardSettings.CardsAccepted.Add(CCA);
                    break;
                default:
                    {
                        Functions.Verify(false, true, "Valid file type");
                        break;
                    }
            }
        }

        public static void DataFile_Currency_setDataStructure(string extension, string dataLine)
        {
            switch(extension.ToUpper())
            {
                case ".HDR": // home currency
                    Variables.globalSettings.CompanySettings.CurrencySettings.allowForeignCurrency = ConvertFunctions.StringToBool(Functions.GetField(dataLine, ",", 1));
                    Variables.globalSettings.CompanySettings.CurrencySettings.HomeCurrency.Currency = Functions.GetField(dataLine, ",", 2);
                    Variables.globalSettings.CompanySettings.CurrencySettings.HomeCurrency.currencyCode = Functions.GetField(dataLine, ",", 3);
                    Variables.globalSettings.CompanySettings.CurrencySettings.HomeCurrency.thousandsSeparator = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 4));
                    Variables.globalSettings.CompanySettings.CurrencySettings.HomeCurrency.symbol = Functions.GetField(dataLine, ",", 5);
                    Variables.globalSettings.CompanySettings.CurrencySettings.HomeCurrency.decimalSeparator = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 6));
                    if (Functions.GetField(dataLine, ",", 7) == "Leading")
                    {
                        Variables.globalSettings.CompanySettings.CurrencySettings.HomeCurrency.symbolPosition = CURRENCY_SYMBOL.CURRENCY_LEADING;
                    }
                    else
                    {
                        Variables.globalSettings.CompanySettings.CurrencySettings.HomeCurrency.symbolPosition = CURRENCY_SYMBOL.CURRENCY_TRAILING;
                    }
                    Variables.globalSettings.CompanySettings.CurrencySettings.HomeCurrency.decimalPlaces = (CURRENCY_DECIMAL)(int)(Convert.ToDouble(Functions.GetField(dataLine, ",", 8)));
                    Variables.globalSettings.CompanySettings.CurrencySettings.HomeCurrency.denomination = Functions.GetField(dataLine, ",", 9);
                    Variables.globalSettings.CompanySettings.CurrencySettings.HomeCurrency.roundingDifferencesAccount = Functions.GetField(dataLine, ",", 10);
                    break;
                case ".CNT": // list of foreign currencies
                    CURRENCY_DATA CD = new CURRENCY_DATA();
                    CD.Currency = Functions.GetField(dataLine, ",", 1);
                    CD.currencyCode = Functions.GetField(dataLine, ",", 2);
                    CD.symbol = Functions.GetField(dataLine, ",", 3);
                    if (Functions.GetField(dataLine, ",", 4) == "Leading")
                    {
                        CD.symbolPosition = CURRENCY_SYMBOL.CURRENCY_LEADING;
                    }
                    else
                    {
                        CD.symbolPosition = CURRENCY_SYMBOL.CURRENCY_TRAILING;
                    }
                    CD.thousandsSeparator = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 5));
                    CD.decimalSeparator = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 6));
                    CD.decimalPlaces = (CURRENCY_DECIMAL)(int)(Convert.ToDouble(Functions.GetField(dataLine, ",", 7)) + 1);
                    CD.denomination = Functions.GetField(dataLine, ",", 8);
                    Variables.globalSettings.CompanySettings.CurrencySettings.ForeignCurrencies.Add(CD);
                    break;
                case ".DT1": // exchange settings per foreign currency
                    foreach (CURRENCY_DATA cd in Variables.globalSettings.CompanySettings.CurrencySettings.ForeignCurrencies)
                    {
                        if (cd.Currency == Functions.GetField(dataLine, ",", 1))
                        {
                            cd.exchangeDisplayReminder = ConvertFunctions.StringToBool(Functions.GetField(dataLine, ",", 2));
                            switch(Functions.GetField(dataLine, ",", 3))
                            {
                                case "One Day":
                                    cd.exchangeRateReminder = CURRENCY_RATE_REMINDER.RATE_REMINDER_ONE_DAY;
                                    break;
                                case "One Week":
                                    cd.exchangeRateReminder = CURRENCY_RATE_REMINDER.RATE_REMINDER_ONE_WEEK;
                                    break;
                                case "One Month":
                                    cd.exchangeRateReminder = CURRENCY_RATE_REMINDER.RATE_REMINDER_ONE_MONTH;
                                    break;
                                case "Six Months":
                                    cd.exchangeRateReminder = CURRENCY_RATE_REMINDER.RATE_REMINDER_SIX_MONTHS;
                                    break;
                                case "One Year":
                                    cd.exchangeRateReminder = CURRENCY_RATE_REMINDER.RATE_REMINDER_ONE_YEAR;
                                    break;
                                default:
                                {
                                    Functions.Verify(false, true, "Valid exchange rate reminder valuew");
                                    break;
                                }                                           
                            }
                            cd.exchangeWebsite = Functions.GetField(dataLine, ",", 4);
                        }
                    }
                    break;
                case ".CNT2": // rates for the exchange container
                    CURRENCY_EXCHANGE CE = new CURRENCY_EXCHANGE();
                    CE.exchangeDate = Functions.GetField(dataLine, ",", 2);
                    CE.exchangeRate = Functions.GetField(dataLine, ",", 3);
                    foreach (CURRENCY_DATA cdr in Variables.globalSettings.CompanySettings.CurrencySettings.ForeignCurrencies)
                    {
                        if (cdr.Currency == Functions.GetField(dataLine, ",", 1))
                        {
                            cdr.ExchangeRates.Add(CE);
                        }
                    }
                    break;
                default:
                {
                    break;
                }    
            }
        }

        public static void DataFile_Department_setDataStructure(string extension, string dataLine)
        {
            DataFile_Department_setDataStructure(extension, dataLine, 0);
        }

        public static void DataFile_Department_setDataStructure(string extension, string dataLine, int iLine)
        {
            int CODE_COLUMN = 1;
            int DESCRIPTION_COLUMN = 2;
            int STATUS_COLUMN = 3;

            switch(extension.ToUpper())
            {
                case ".HDR":
                    DEPT_ACCT DA = new DEPT_ACCT();
                    DA.code = Functions.GetField(dataLine, ",", CODE_COLUMN);
                    DA.description = Functions.GetField(dataLine, ",", DESCRIPTION_COLUMN);
                    if (Functions.GetField(dataLine, ",", STATUS_COLUMN) == "Active")
                    {
                        DA.ActiveStatus = true;
                    }
                    else
                    {
                        DA.ActiveStatus = false;
                    }
                    Variables.globalSettings.GeneralSettings.DepartmentalAccounting.Add(DA);
                    break;
                default:
                    {
                        Functions.Verify(false, true, "Valid extension type sent");
                        break;
                    }
            }
        }

        public static void DataFile_Location_setDataStructure(string extension, string dataLine)
        {
            DataFile_Location_setDataStructure(extension, dataLine, 0);
        }

        public static void DataFile_Location_setDataStructure(string extension, string dataLine, int iLine)
        {
            //int CODE_COLUMN = 1;
            //int DESCRIPTION_COLUMN = 2;
            //int STATUS_COLUMN = 3;

            switch(extension.ToUpper())
            {
                case ".HDR":
                    LOCATION loc = new LOCATION();
                    loc.code = Functions.GetField(dataLine, ",", 1);
                    loc.description = Functions.GetField(dataLine, ",", 2);
                    if (Functions.GetField(dataLine, ",", 3) == "Active")
                    {
                        loc.ActiveStatus = true;
                    }
                    else
                    {
                        loc.ActiveStatus = false;
                    }
                    Variables.globalSettings.InventorySettings.Locations.Add(loc);
                    break;
                default:
                    {
                        Functions.Verify(false, true, "Valid extension type sent");
                        break;
                    }
            }
        }

        public static void DataFile_Name_setDataStructure(string extension, string dataLine)
        {
            DataFile_Name_setDataStructure(extension, dataLine, 0);
        }

        public static void DataFile_Name_setDataStructure(string extension, string dataLine, int iLine)
        {
            switch(extension.ToUpper())
            {
                case ".HDR":
                    Variables.globalSettings.GeneralSettings.AdditionalFields.Field1 = Functions.GetField(dataLine, ",", 1);
                    Variables.globalSettings.GeneralSettings.AdditionalFields.Field2 = Functions.GetField(dataLine, ",", 2);
                    Variables.globalSettings.GeneralSettings.AdditionalFields.Field3 = Functions.GetField(dataLine, ",", 3);
                    Variables.globalSettings.GeneralSettings.AdditionalFields.Field4 = Functions.GetField(dataLine, ",", 4);
                    Variables.globalSettings.GeneralSettings.AdditionalFields.Field5 = Functions.GetField(dataLine, ",", 5);
                    break;
                case ".HD2":
                    Variables.globalSettings.PayableSettings.AdditionalFields.Field1 = Functions.GetField(dataLine, ",", 1);
                    Variables.globalSettings.PayableSettings.AdditionalFields.Field2 = Functions.GetField(dataLine, ",", 2);
                    Variables.globalSettings.PayableSettings.AdditionalFields.Field3 = Functions.GetField(dataLine, ",", 3);
                    Variables.globalSettings.PayableSettings.AdditionalFields.Field4 = Functions.GetField(dataLine, ",", 4);
                    Variables.globalSettings.PayableSettings.AdditionalFields.Field5 = Functions.GetField(dataLine, ",", 5);
                    break;
                case ".HD3":
                    Variables.globalSettings.ReceivableSettings.AdditionalFields.Field1 = Functions.GetField(dataLine, ",", 1);
                    Variables.globalSettings.ReceivableSettings.AdditionalFields.Field2 = Functions.GetField(dataLine, ",", 2);
                    Variables.globalSettings.ReceivableSettings.AdditionalFields.Field3 = Functions.GetField(dataLine, ",", 3);
                    Variables.globalSettings.ReceivableSettings.AdditionalFields.Field4 = Functions.GetField(dataLine, ",", 4);
                    Variables.globalSettings.ReceivableSettings.AdditionalFields.Field5 = Functions.GetField(dataLine, ",", 5);
                    break;
                case ".HD4":
                    Variables.globalSettings.PayrollSettings.AdditionalPayrollSettings.AdditionalFields.Field1 = Functions.GetField(dataLine, ",", 1);
                    Variables.globalSettings.PayrollSettings.AdditionalPayrollSettings.AdditionalFields.Field2 = Functions.GetField(dataLine, ",", 2);
                    Variables.globalSettings.PayrollSettings.AdditionalPayrollSettings.AdditionalFields.Field3 = Functions.GetField(dataLine, ",", 3);
                    Variables.globalSettings.PayrollSettings.AdditionalPayrollSettings.AdditionalFields.Field4 = Functions.GetField(dataLine, ",", 4);
                    Variables.globalSettings.PayrollSettings.AdditionalPayrollSettings.AdditionalFields.Field5 = Functions.GetField(dataLine, ",", 5);
                    break;
                case ".HD5":
                    Variables.globalSettings.PayrollSettings.AdditionalPayrollSettings.provTax = Functions.GetField(dataLine, ",", 1);
                    Variables.globalSettings.PayrollSettings.AdditionalPayrollSettings.workersComp = Functions.GetField(dataLine, ",", 2);

                    int INCOMES_TABLE_START_FIELD = 3;
                    int INCOMES_TABLE_END_FIELD = 22;
                    int DEDUCTIONS_TABLE_START_FIELD = 23;
                    int DEDUCTIONS_TABLE_END_FIELD = 42;

                    INCOME_NAME IN = new INCOME_NAME();
                    int iNum;
                    for (int i = INCOMES_TABLE_START_FIELD; i <= INCOMES_TABLE_END_FIELD; i++)
                    {
                        IN.Name = Functions.GetField(dataLine, ",", i);
                        iNum = i - INCOMES_TABLE_START_FIELD + 1;
                        IN.Income = "Income " + iNum + "";
                        IN.LinkedAccount = new GL_ACCOUNT();
                        Variables.globalSettings.PayrollSettings.AdditionalIncome[iNum - 1] = IN;
                    }

                    DEDUCTION_NAME DN = new DEDUCTION_NAME();
                    for (int i = DEDUCTIONS_TABLE_START_FIELD; i <= DEDUCTIONS_TABLE_END_FIELD; i++)
                    {
                        DN.Name = Functions.GetField(dataLine, ",", 1);
                        iNum = i - DEDUCTIONS_TABLE_START_FIELD + 1;
                        DN.Deduction = "Deduction " + iNum + "";
                        DN.LinkedAccount = new GL_ACCOUNT();
                        Variables.globalSettings.PayrollSettings.AdditionalDeduction[iNum - 1] = DN;
                    }
                    break;
                case ".HD6":
                    Variables.globalSettings.PayrollSettings.AdditionalPayrollSettings.ExpenseFields.Field1 = Functions.GetField(dataLine, ",", 1);
                    Variables.globalSettings.PayrollSettings.AdditionalPayrollSettings.ExpenseFields.Field2 = Functions.GetField(dataLine, ",", 2);
                    Variables.globalSettings.PayrollSettings.AdditionalPayrollSettings.ExpenseFields.Field3 = Functions.GetField(dataLine, ",", 3);
                    Variables.globalSettings.PayrollSettings.AdditionalPayrollSettings.ExpenseFields.Field4 = Functions.GetField(dataLine, ",", 4);
                    Variables.globalSettings.PayrollSettings.AdditionalPayrollSettings.ExpenseFields.Field5 = Functions.GetField(dataLine, ",", 5);
                    Variables.globalSettings.PayrollSettings.AdditionalPayrollSettings.EntitlementFields.Field1 = Functions.GetField(dataLine, ",", 6);
                    Variables.globalSettings.PayrollSettings.AdditionalPayrollSettings.EntitlementFields.Field2 = Functions.GetField(dataLine, ",", 7);
                    Variables.globalSettings.PayrollSettings.AdditionalPayrollSettings.EntitlementFields.Field3 = Functions.GetField(dataLine, ",", 8);
                    Variables.globalSettings.PayrollSettings.AdditionalPayrollSettings.EntitlementFields.Field4 = Functions.GetField(dataLine, ",", 9);
                    Variables.globalSettings.PayrollSettings.AdditionalPayrollSettings.EntitlementFields.Field5 = Functions.GetField(dataLine, ",", 10);
                    break;
                case ".HD7": 
                    Variables.globalSettings.InventorySettings.AdditionalFields.Field1 = Functions.GetField(dataLine, ",", 1);
                    Variables.globalSettings.InventorySettings.AdditionalFields.Field2 = Functions.GetField(dataLine, ",", 2);
                    Variables.globalSettings.InventorySettings.AdditionalFields.Field3 = Functions.GetField(dataLine, ",", 3);
                    Variables.globalSettings.InventorySettings.AdditionalFields.Field4 = Functions.GetField(dataLine, ",", 4);
                    Variables.globalSettings.InventorySettings.AdditionalFields.Field5 = Functions.GetField(dataLine, ",", 5);
                    break;
                case ".HD8":
                    Variables.globalSettings.ProjectSettings.ProjectTitle = Functions.GetField(dataLine, ",", 1);
                    Variables.globalSettings.ProjectSettings.AdditionalFields.Field1 = Functions.GetField(dataLine, ",", 2);
                    Variables.globalSettings.ProjectSettings.AdditionalFields.Field2 = Functions.GetField(dataLine, ",", 3);
                    Variables.globalSettings.ProjectSettings.AdditionalFields.Field3 = Functions.GetField(dataLine, ",", 4);
                    Variables.globalSettings.ProjectSettings.AdditionalFields.Field4 = Functions.GetField(dataLine, ",", 5);
                    Variables.globalSettings.ProjectSettings.AdditionalFields.Field5 = Functions.GetField(dataLine, ",", 6);
                    break;
                case ".HD9":
                    Variables.globalSettings.CompanySettings.additionalInformationDate = Functions.GetField(dataLine, ",", 1);
                    Variables.globalSettings.CompanySettings.additionalInformationField = Functions.GetField(dataLine, ",", 2);
                    break;
                default:
                    {
                        Functions.Verify(false, true, "Valid extension type sent");
                        break;
                    }
            }
        }

        public static void DataFile_Shipper_setDataStructure(string dataLine)
        {
            Variables.globalSettings.CompanySettings.ShipperSettings.TrackShipments = ConvertFunctions.StringToBool(Functions.GetField(dataLine, ",", 1));
            Variables.globalSettings.CompanySettings.ShipperSettings.ShipServices.Clear();

            int iIndex = 2;
            for (int x = 0; x < 12; x++)
            {
                SHIP_SERVICES SS = new SHIP_SERVICES();
                SS.Shipper = Functions.GetField(dataLine, ",", iIndex);
                SS.TrackingSite = Functions.GetField(dataLine, ",", ++iIndex);
                Variables.globalSettings.CompanySettings.ShipperSettings.ShipServices.Add(SS);
                iIndex++;
            }
        }

        public static void DataFile_PriceList_setDataStructure(string dataLine)
        {
            PRICE_LIST PL = new PRICE_LIST();
            PL.description = Functions.GetField(dataLine, ",", 1);
            if (Functions.GetField(dataLine, ",", 2) == "Active")
            {
                PL.ActiveStatus = true;
            }
            else
            {
                PL.ActiveStatus = false;
            }
            Variables.globalSettings.InventorySettings.PriceList.Add(PL);
        }

        public static void DataFile_APLinkAccounts_setDataStructure(string extension, string dataLine)
        {
            switch (extension.ToUpper())
            {
                case ".HD2":
                    Variables.globalSettings.PayableSettings.principalBankAcct.acctNumber = Functions.GetField(dataLine, ",", 1);
                    Variables.globalSettings.PayableSettings.apAcct.acctNumber = Functions.GetField(dataLine, ",", 2);
                    Variables.globalSettings.PayableSettings.freightAcct.acctNumber = Functions.GetField(dataLine, ",", 3);
                    Variables.globalSettings.PayableSettings.earlyPayDiscountAcct.acctNumber = Functions.GetField(dataLine, ",", 4);
                    Variables.globalSettings.PayableSettings.prepaymentAcct.acctNumber = Functions.GetField(dataLine, ",", 5);
                    break;
                case ".CNT":
                    CURRENCY_ACCOUNT CA = new CURRENCY_ACCOUNT();
                    CA.Currency = Functions.GetField(dataLine, ",", 1);
                    CA.BankAccount.acctNumber = Functions.GetField(dataLine, ",", 2);
                    Variables.globalSettings.PayableSettings.CurrencyAccounts.Add(CA);
                    break;
                default:
                    {
                        Functions.Verify(false, true, "Valid extension type sent");
                        break;
                    }
            }
        }

        public static void DataFile_ARLinkAccounts_setDataStructure(string extension, string dataLine)
        {
            switch (extension.ToUpper())
            {
                case ".HD3":
                    Variables.globalSettings.ReceivableSettings.principalBankAcct.acctNumber = Functions.GetField(dataLine, ",", 1);
                    Variables.globalSettings.ReceivableSettings.arAcct.acctNumber = Functions.GetField(dataLine, ",", 2);
                    Variables.globalSettings.ReceivableSettings.freightAcct.acctNumber = Functions.GetField(dataLine, ",", 3);
                    Variables.globalSettings.ReceivableSettings.earlyPayDiscountAcct.acctNumber = Functions.GetField(dataLine, ",", 4);
                    Variables.globalSettings.ReceivableSettings.depositsAcct.acctNumber = Functions.GetField(dataLine, ",", 5);
                    break;
                case ".CNT2":
                    CURRENCY_ACCOUNT CA = new CURRENCY_ACCOUNT();
                    CA.Currency = Functions.GetField(dataLine, ",", 1);
                    CA.BankAccount.acctNumber = Functions.GetField(dataLine, ",", 2);
                    Variables.globalSettings.ReceivableSettings.CurrencyAccounts.Add(CA);
                    break;
                default:
                    {
                        Functions.Verify(false, true, "Valid extension type sent");
                        break;
                    }
            }
        }

        public static void DataFile_GLLinkAccounts_setDataStructure(string extension, string dataLine)
        {
            switch (extension.ToUpper())
            {
                case ".HDR":
                    Variables.globalSettings.GeneralSettings.RetainedEarnings.acctNumber = Functions.GetField(dataLine, ",", 1);
                    break;
                default:
                    {
                        Functions.Verify(false, true, "Valid extension type sent");
                        break;
                    }
            }
        }

        public static void DataFile_InvLinkAccounts_setDataStructure(string extension, string dataLine)
        {
            switch (extension.ToUpper())
            {
                case ".HD8":
                    Variables.globalSettings.InventorySettings.itemAssemblyCosts.acctNumber = Functions.GetField(dataLine, ",", 1);
                    Variables.globalSettings.InventorySettings.adjustmentWriteOff.acctNumber = Functions.GetField(dataLine, ",", 2);
                    break;
                default:
                    {
                        Functions.Verify(false, true, "Valid extension type sent");
                        break;
                    }
            }
        }

        public static void DataFile_PayrollLinkAccounts_setDataStructure(string extension, string dataLine)
        {
            switch (extension.ToUpper())
            {
                case ".HD4":
                    Variables.globalSettings.PayrollSettings.IncomeAccounts.principalBank.acctNumber = Functions.GetField(dataLine, ",", 1);
                    Variables.globalSettings.PayrollSettings.IncomeAccounts.vacation.acctNumber = Functions.GetField(dataLine, ",", 2);
                    Variables.globalSettings.PayrollSettings.IncomeAccounts.advances.acctNumber = Functions.GetField(dataLine, ",", 3);
                    Variables.globalSettings.PayrollSettings.IncomeAccounts.vacationEarnedLinkedAccount.acctNumber = Functions.GetField(dataLine, ",", 4);
                    Variables.globalSettings.PayrollSettings.IncomeAccounts.regularWageLinkedAccount.acctNumber = Functions.GetField(dataLine, ",", 5);
                    Variables.globalSettings.PayrollSettings.IncomeAccounts.ot1LinkedAccount.acctNumber = Functions.GetField(dataLine, ",", 6);
                    Variables.globalSettings.PayrollSettings.IncomeAccounts.ot2LinkedAccount.acctNumber = Functions.GetField(dataLine, ",", 7);
                    int y = 8;
                    for (int x = 0; x < 20; x++)
                    {
                        Variables.globalSettings.PayrollSettings.AdditionalIncome[x].LinkedAccount.acctNumber = Functions.GetField(dataLine, ",", y);
                        y++;
                    }
                    break;
                case ".HD5":
                    for (int x = 0; x < 20; x++)
                    {
                        Variables.globalSettings.PayrollSettings.AdditionalDeduction[x].LinkedAccount.acctNumber = Functions.GetField(dataLine, ",", x + 1);
                    }
                    break;
                case ".HD6":
                    // Canadian
                    Variables.globalSettings.PayrollSettings.TaxAccounts.payEI.acctNumber = Functions.GetField(dataLine, ",", 1);
                    Variables.globalSettings.PayrollSettings.TaxAccounts.payCPP.acctNumber = Functions.GetField(dataLine, ",", 2);
                    Variables.globalSettings.PayrollSettings.TaxAccounts.payTax.acctNumber = Functions.GetField(dataLine, ",", 3);
                    Variables.globalSettings.PayrollSettings.TaxAccounts.payWCB.acctNumber = Functions.GetField(dataLine, ",", 4);
                    Variables.globalSettings.PayrollSettings.TaxAccounts.payEHT.acctNumber = Functions.GetField(dataLine, ",", 5);
                    Variables.globalSettings.PayrollSettings.TaxAccounts.payQueTax.acctNumber = Functions.GetField(dataLine, ",", 6);
                    Variables.globalSettings.PayrollSettings.TaxAccounts.payQPP.acctNumber = Functions.GetField(dataLine, ",", 7);
                    Variables.globalSettings.PayrollSettings.TaxAccounts.payQHSF.acctNumber = Functions.GetField(dataLine, ",", 8);
                    Variables.globalSettings.PayrollSettings.TaxAccounts.exEI.acctNumber = Functions.GetField(dataLine, ",", 9);
                    Variables.globalSettings.PayrollSettings.TaxAccounts.exCPP.acctNumber = Functions.GetField(dataLine, ",", 10);
                    Variables.globalSettings.PayrollSettings.TaxAccounts.exWCB.acctNumber = Functions.GetField(dataLine, ",", 11);
                    Variables.globalSettings.PayrollSettings.TaxAccounts.exEHT.acctNumber = Functions.GetField(dataLine, ",", 12);
                    Variables.globalSettings.PayrollSettings.TaxAccounts.exQPP.acctNumber = Functions.GetField(dataLine, ",", 13);
                    Variables.globalSettings.PayrollSettings.TaxAccounts.exQHSF.acctNumber = Functions.GetField(dataLine, ",", 14);
                    Variables.globalSettings.PayrollSettings.TaxAccounts.payQPIP.acctNumber = Functions.GetField(dataLine, ",", 15);
                    Variables.globalSettings.PayrollSettings.TaxAccounts.exQPIP.acctNumber = Functions.GetField(dataLine, ",", 16);

                    //US
                    Variables.globalSettings.PayrollSettings.TaxAccounts.payFIT.acctNumber = Functions.GetField(dataLine, ",", 17);
                    Variables.globalSettings.PayrollSettings.TaxAccounts.paySIT.acctNumber = Functions.GetField(dataLine, ",", 18);
                    Variables.globalSettings.PayrollSettings.TaxAccounts.payMedTax.acctNumber = Functions.GetField(dataLine, ",", 19);
                    Variables.globalSettings.PayrollSettings.TaxAccounts.paySSTax.acctNumber = Functions.GetField(dataLine, ",", 20);
                    Variables.globalSettings.PayrollSettings.TaxAccounts.payFUTA.acctNumber = Functions.GetField(dataLine, ",", 21);
                    Variables.globalSettings.PayrollSettings.TaxAccounts.paySUTA.acctNumber = Functions.GetField(dataLine, ",", 22);
                    Variables.globalSettings.PayrollSettings.TaxAccounts.paySDI.acctNumber = Functions.GetField(dataLine, ",", 23);
                    Variables.globalSettings.PayrollSettings.TaxAccounts.payLocalTax.acctNumber = Functions.GetField(dataLine, ",", 24);
                    Variables.globalSettings.PayrollSettings.TaxAccounts.exMedTax.acctNumber = Functions.GetField(dataLine, ",", 25);
                    Variables.globalSettings.PayrollSettings.TaxAccounts.exSSTax.acctNumber = Functions.GetField(dataLine, ",", 26);
                    Variables.globalSettings.PayrollSettings.TaxAccounts.exFUTA.acctNumber = Functions.GetField(dataLine, ",", 27);
                    Variables.globalSettings.PayrollSettings.TaxAccounts.exSUTA.acctNumber = Functions.GetField(dataLine, ",", 28);
                    Variables.globalSettings.PayrollSettings.TaxAccounts.exSDI.acctNumber = Functions.GetField(dataLine, ",", 29);
                    break;
                case ".HD7":
                    Variables.globalSettings.PayrollSettings.UserDefinedAccounts.payable1Acct.acctNumber = Functions.GetField(dataLine, ",", 1);
                    Variables.globalSettings.PayrollSettings.UserDefinedAccounts.payable2Acct.acctNumber = Functions.GetField(dataLine, ",", 2);
                    Variables.globalSettings.PayrollSettings.UserDefinedAccounts.payable3Acct.acctNumber = Functions.GetField(dataLine, ",", 3);
                    Variables.globalSettings.PayrollSettings.UserDefinedAccounts.payable4Acct.acctNumber = Functions.GetField(dataLine, ",", 4);
                    Variables.globalSettings.PayrollSettings.UserDefinedAccounts.payable5Acct.acctNumber = Functions.GetField(dataLine, ",", 5);
                    Variables.globalSettings.PayrollSettings.UserDefinedAccounts.expense1Acct.acctNumber = Functions.GetField(dataLine, ",", 6);
                    Variables.globalSettings.PayrollSettings.UserDefinedAccounts.expense2Acct.acctNumber = Functions.GetField(dataLine, ",", 7);
                    Variables.globalSettings.PayrollSettings.UserDefinedAccounts.expense3Acct.acctNumber = Functions.GetField(dataLine, ",", 8);
                    Variables.globalSettings.PayrollSettings.UserDefinedAccounts.expense4Acct.acctNumber = Functions.GetField(dataLine, ",", 9);
                    Variables.globalSettings.PayrollSettings.UserDefinedAccounts.expense5Acct.acctNumber = Functions.GetField(dataLine, ",", 10);
                    break;
                default:
                    {
                        Functions.Verify(false, true, "Valid extension type sent");
                        break;
                    }
            }
        }

        public static void DataFile_PayrollJob_setDataStructure(string extension, string dataLine)
        {
            switch (extension.ToUpper())
            {
                case ".HDR":
                    PAYROLL_JOB PJ = new PAYROLL_JOB();
                    PJ.Category = (Functions.GetField(dataLine, ",", 1));
                    PJ.SubmitTimeSlips = ConvertFunctions.StringToBool(Functions.GetField(dataLine, ",", 2));
                    PJ.AreSalespersons = ConvertFunctions.StringToBool(Functions.GetField(dataLine, ",", 3));
                    if (Functions.GetField(dataLine, ",", 4) == "Active")
                    {
                        PJ.ActiveStatus = true;
                    }
                    else
                    {
                        PJ.ActiveStatus = false;
                    }
                    Variables.globalSettings.PayrollSettings.JobCategories.Add(PJ);
                    break;
                case ".DT1":
                    // fix later
                    // int x
                    // for x = 1 to ListCount (GlobalSettings.PayrollSettings.JobCategories)//make sure all jobs have no emps assigned
                    // GlobalSettings.PayrollSettings.JobCategories[x].EmployeesAssigned = {}
                    // INT i = 0
                    //
                    //
                    // while ( GetField (dataLine, ",", ++i) != "" )
                    // EMPLOYEE emp = New (EMPLOYEE)
                    // emp.employeeName =  (GetField (dataLine, ",", i))
                    // for x = 1 to ListCount (GlobalSettings.PayrollSettings.JobCategories)//assign the emps to all the jobs
                    // ListAppend (GlobalSettings.PayrollSettings.JobCategories[x].EmployeesAssigned, emp)
                    break;
                default:
                    {
                        Functions.Verify(false, true, "Valid extension type sent");
                        break;
                    }
            }
        }

        public static void DataFile_MainSettings_setDataStructure(string extension, string dataLine)
        {
            switch (extension.ToUpper())
            {
                case ".HDR":    // Company - System page and Company - Backup page
                    Variables.globalSettings.CompanySettings.SystemSettings.useCashBasisAccounting = ConvertFunctions.StringToBool(Functions.GetField(dataLine, ",", 1));
                    Variables.globalSettings.CompanySettings.SystemSettings.cashBasisDate = Functions.GetField(dataLine, ",", 2);
                    Variables.globalSettings.CompanySettings.SystemSettings.storeInvoiceLookupDetails = ConvertFunctions.StringToBool(Functions.GetField(dataLine, ",", 3));
                    Variables.globalSettings.CompanySettings.SystemSettings.useChequeNo = ConvertFunctions.StringToBool(Functions.GetField(dataLine, ",", 4));
                    Variables.globalSettings.CompanySettings.SystemSettings.doNotAllowTransactionsDatedBefore = ConvertFunctions.StringToBool(Functions.GetField(dataLine, ",", 5));
                    Variables.globalSettings.CompanySettings.SystemSettings.lockingDate = Functions.GetField(dataLine, ",", 6);
                    Variables.globalSettings.CompanySettings.SystemSettings.allowTransactionsInTheFuture = ConvertFunctions.StringToBool(Functions.GetField(dataLine, ",", 7));
                    Variables.globalSettings.CompanySettings.SystemSettings.warnIfTransactionsAre = ConvertFunctions.StringToBool(Functions.GetField(dataLine, ",", 8));
                    Variables.globalSettings.CompanySettings.SystemSettings.daysInTheFuture = Functions.GetField(dataLine, ",", 9);
                    Variables.globalSettings.CompanySettings.SystemSettings.warnIfAccountsAreNotBalanced = ConvertFunctions.StringToBool(Functions.GetField(dataLine, ",", 10));
                    Variables.globalSettings.CompanySettings.BackupSettings.displayReminderOnSession = ConvertFunctions.StringToBool(Functions.GetField(dataLine, ",", 11));
                    Variables.globalSettings.CompanySettings.BackupSettings.reminderFrequency = Functions.GetField(dataLine, ",", 12);
                    Variables.globalSettings.CompanySettings.BackupSettings.displayReminderWhenClosing = ConvertFunctions.StringToBool(Functions.GetField(dataLine, ",", 13));
                    break;
                case ".HD2":    // Company - Features page
                    Variables.globalSettings.CompanySettings.FeatureSettings.ordersForVendors = ConvertFunctions.StringToBool(Functions.GetField(dataLine, ",", 1));
                    Variables.globalSettings.CompanySettings.FeatureSettings.quotesForVendors = ConvertFunctions.StringToBool(Functions.GetField(dataLine, ",", 2));
                    Variables.globalSettings.CompanySettings.FeatureSettings.ordersForCustomers = ConvertFunctions.StringToBool(Functions.GetField(dataLine, ",", 3));
                    Variables.globalSettings.CompanySettings.FeatureSettings.quotesForCustomers = ConvertFunctions.StringToBool(Functions.GetField(dataLine, ",", 4));
                    Variables.globalSettings.CompanySettings.FeatureSettings.projectCheckBox = ConvertFunctions.StringToBool(Functions.GetField(dataLine, ",", 5));
                    break;
                case ".HD3":    // Company - Forms page
                    Variables.globalSettings.CompanySettings.FormSettings.nextNumInvoice = Functions.GetField(dataLine, ",", 1);
                    Variables.globalSettings.CompanySettings.FormSettings.nextNumSalesQuote = Functions.GetField(dataLine, ",", 2);
                    Variables.globalSettings.CompanySettings.FormSettings.nextNumReceipt = Functions.GetField(dataLine, ",", 3);
                    Variables.globalSettings.CompanySettings.FormSettings.nextNumCustomerDeposit = Functions.GetField(dataLine, ",", 4);
                    Variables.globalSettings.CompanySettings.FormSettings.nextNumPurchaseOrder = Functions.GetField(dataLine, ",", 5);
                    Variables.globalSettings.CompanySettings.FormSettings.nextNumDirectDeposit = Functions.GetField(dataLine, ",", 6);
                    Variables.globalSettings.CompanySettings.FormSettings.nextNumTimeSlip = Functions.GetField(dataLine, ",", 7);

                    Variables.globalSettings.CompanySettings.FormSettings.verifyNumSeqInvoice = ConvertFunctions.StringToBool(Functions.GetField(dataLine, ",", 8));
                    Variables.globalSettings.CompanySettings.FormSettings.verifyNumSeqSalesQuote = ConvertFunctions.StringToBool(Functions.GetField(dataLine, ",", 9));
                    Variables.globalSettings.CompanySettings.FormSettings.verifyNumSeqReceipt = ConvertFunctions.StringToBool(Functions.GetField(dataLine, ",", 10));
                    Variables.globalSettings.CompanySettings.FormSettings.verifyNumSeqCustomerDeposit = ConvertFunctions.StringToBool(Functions.GetField(dataLine, ",", 11));
                    Variables.globalSettings.CompanySettings.FormSettings.verifyNumSeqPurchaseOrder = ConvertFunctions.StringToBool(Functions.GetField(dataLine, ",", 12));
                    Variables.globalSettings.CompanySettings.FormSettings.verifyNumSeqCheque = ConvertFunctions.StringToBool(Functions.GetField(dataLine, ",", 13));
                    Variables.globalSettings.CompanySettings.FormSettings.verifyNumSeqDirectDeposit = ConvertFunctions.StringToBool(Functions.GetField(dataLine, ",", 14));
                    Variables.globalSettings.CompanySettings.FormSettings.verifyNumSeqTimeSlips = ConvertFunctions.StringToBool(Functions.GetField(dataLine, ",", 15));
                    Variables.globalSettings.CompanySettings.FormSettings.verifyNumSeqDepositSlips = ConvertFunctions.StringToBool(Functions.GetField(dataLine, ",", 16));

                    Variables.globalSettings.CompanySettings.FormSettings.confirmPrintEmailInvoice = ConvertFunctions.StringToBool(Functions.GetField(dataLine, ",", 17));
                    Variables.globalSettings.CompanySettings.FormSettings.confirmPrintEmailSalesQuote = ConvertFunctions.StringToBool(Functions.GetField(dataLine, ",", 18));
                    Variables.globalSettings.CompanySettings.FormSettings.confirmPrintEmailSalesOrder = ConvertFunctions.StringToBool(Functions.GetField(dataLine, ",", 19));
                    Variables.globalSettings.CompanySettings.FormSettings.confirmPrintEmailReceipt = ConvertFunctions.StringToBool(Functions.GetField(dataLine, ",", 20));
                    Variables.globalSettings.CompanySettings.FormSettings.confirmPrintEmailPurchaseOrder = ConvertFunctions.StringToBool(Functions.GetField(dataLine, ",", 21));
                    Variables.globalSettings.CompanySettings.FormSettings.confirmPrintEmailChequesOrder = ConvertFunctions.StringToBool(Functions.GetField(dataLine, ",", 22));
                    Variables.globalSettings.CompanySettings.FormSettings.confirmPrintEmailDirectDeposit = ConvertFunctions.StringToBool(Functions.GetField(dataLine, ",", 23));
                    Variables.globalSettings.CompanySettings.FormSettings.confirmPrintEmailTimeSlips = ConvertFunctions.StringToBool(Functions.GetField(dataLine, ",", 24));
                    Variables.globalSettings.CompanySettings.FormSettings.confirmPrintEmailDepositSlips = ConvertFunctions.StringToBool(Functions.GetField(dataLine, ",", 25));

                    Variables.globalSettings.CompanySettings.FormSettings.printCompAddressInvoice = ConvertFunctions.StringToBool(Functions.GetField(dataLine, ",", 26));
                    Variables.globalSettings.CompanySettings.FormSettings.printCompAddressSalesQuotes = ConvertFunctions.StringToBool(Functions.GetField(dataLine, ",", 27));
                    Variables.globalSettings.CompanySettings.FormSettings.printCompAddressSalesOrders = ConvertFunctions.StringToBool(Functions.GetField(dataLine, ",", 28));
                    Variables.globalSettings.CompanySettings.FormSettings.printCompAddressReceipt = ConvertFunctions.StringToBool(Functions.GetField(dataLine, ",", 29));
                    Variables.globalSettings.CompanySettings.FormSettings.printCompAddressStatements = ConvertFunctions.StringToBool(Functions.GetField(dataLine, ",", 30));
                    Variables.globalSettings.CompanySettings.FormSettings.printCompAddressPurchaseOrders = ConvertFunctions.StringToBool(Functions.GetField(dataLine, ",", 31));
                    Variables.globalSettings.CompanySettings.FormSettings.printCompAddressCheque = ConvertFunctions.StringToBool(Functions.GetField(dataLine, ",", 32));
                    Variables.globalSettings.CompanySettings.FormSettings.printCompAddressDirectDeposit = ConvertFunctions.StringToBool(Functions.GetField(dataLine, ",", 33));

                    Variables.globalSettings.CompanySettings.FormSettings.printInBatchesInvoice = ConvertFunctions.StringToBool(Functions.GetField(dataLine, ",", 34));
                    Variables.globalSettings.CompanySettings.FormSettings.printInBatchesSalesQuotes = ConvertFunctions.StringToBool(Functions.GetField(dataLine, ",", 35));
                    Variables.globalSettings.CompanySettings.FormSettings.printInBatchesSalesOrders = ConvertFunctions.StringToBool(Functions.GetField(dataLine, ",", 36));
                    Variables.globalSettings.CompanySettings.FormSettings.printInBatchesReceipt = ConvertFunctions.StringToBool(Functions.GetField(dataLine, ",", 37));
                    Variables.globalSettings.CompanySettings.FormSettings.printInBatchesPurchaseOrders = ConvertFunctions.StringToBool(Functions.GetField(dataLine, ",", 38));
                    Variables.globalSettings.CompanySettings.FormSettings.printInBatchesCheque = ConvertFunctions.StringToBool(Functions.GetField(dataLine, ",", 39));
                    Variables.globalSettings.CompanySettings.FormSettings.printInBatchesDirectDeposit = ConvertFunctions.StringToBool(Functions.GetField(dataLine, ",", 40));
                    Variables.globalSettings.CompanySettings.FormSettings.printInBatchesDepositSlip = ConvertFunctions.StringToBool(Functions.GetField(dataLine, ",", 41));
                    Variables.globalSettings.CompanySettings.FormSettings.printInBatchesPackingSlip = ConvertFunctions.StringToBool(Functions.GetField(dataLine, ",", 42));

                    Variables.globalSettings.CompanySettings.FormSettings.checkForDupInvoices = ConvertFunctions.StringToBool(Functions.GetField(dataLine, ",", 42));
                    Variables.globalSettings.CompanySettings.FormSettings.checkForDupReceipts = ConvertFunctions.StringToBool(Functions.GetField(dataLine, ",", 43));
                    break;
                case ".HD4":    // Company - E-mail page
                    if (Functions.GetField(dataLine, ",", 1).Contains("PDF"))
                    {
                        Variables.globalSettings.CompanySettings.EmailSettings.AttachmentFormat = EMAIL_ATTACH.EMAIL_PDF;
                    }
                    else
                    {
                        Variables.globalSettings.CompanySettings.EmailSettings.AttachmentFormat = EMAIL_ATTACH.EMAIL_RTF;
                    }
                    Variables.globalSettings.CompanySettings.EmailSettings.Messages.Clear();
                    
                    int iField = 2;
                    for (int x = 0; x < 8; x++)
                    {
                        EMAIL_MESSAGES EM = new EMAIL_MESSAGES();

                        switch (Functions.GetField(dataLine, ",", iField))
                        {
                            case "Invoices":
                                EM.Form = EMAIL_FORM_TYPE.FORM_INVOICES;
                                break;
                            case "Purchase Orders":
                                EM.Form = EMAIL_FORM_TYPE.FORM_PURCHASE_ORDERS;
                                break;
                            case "Sales Orders":
                                EM.Form = EMAIL_FORM_TYPE.FORM_SALES_ORDERS;
                                break;
                            case "Sales Quotes":
                                EM.Form = EMAIL_FORM_TYPE.FORM_SALES_QUOTES;
                                break;
                            case "Receipts":
                                EM.Form = EMAIL_FORM_TYPE.FORM_RECEIPTS;
                                break;
                            case "Statements":
                                EM.Form = EMAIL_FORM_TYPE.FORM_STATEMENTS;
                                break;
                            case "Purchase Quote Confirmations":
                                EM.Form = EMAIL_FORM_TYPE.FORM_PURCHASE_QUOTE_CONFIRMATIONS;
                                break;
                            case "Purchase Invoice Confirmations":
                                EM.Form = EMAIL_FORM_TYPE.FORM_PURCHASE_INVOICE_CONFIRMATIONS;
                                break;
                            case "Direct Deposit Stub":
                                EM.Form = EMAIL_FORM_TYPE.FORM_DIRECT_DEPOSIT_PAYMENT_STUB;
                                break;
                            default:
                                {
                                    Functions.Verify(false, true, "Correct value for email message");
                                    break;
                                }
                        }
                        EM.Message = Functions.GetField(dataLine, ",", iField++);
                        Variables.globalSettings.CompanySettings.EmailSettings.Messages.Add(EM);
                        iField++;
                    }
                    break;
                case ".HD5":    // Company - Dates page
                    Variables.globalSettings.CompanySettings.DateSettings.shortDateFormat = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 1));
                    Variables.globalSettings.CompanySettings.DateSettings.shortDateSeparator = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 2));
                    Variables.globalSettings.CompanySettings.DateSettings.longDateFormat = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 3));
                    if (Functions.GetField(dataLine, ",", 4) == "Short Dates")
                    {
                        Variables.globalSettings.CompanySettings.DateSettings.onScreenUse = DATE_SHORT_LONG.DATE_SHORT;
                    }
                    else
                    {
                        Variables.globalSettings.CompanySettings.DateSettings.onScreenUse = DATE_SHORT_LONG.DATE_LONG;
                    }
                    if (Functions.GetField(dataLine, ",", 5) == "Short Dates")
                    {
                        Variables.globalSettings.CompanySettings.DateSettings.inReportsUse = DATE_SHORT_LONG.DATE_SHORT;
                    }
                    else
                    {
                        Variables.globalSettings.CompanySettings.DateSettings.inReportsUse = DATE_SHORT_LONG.DATE_LONG;
                    }
                    break;
                case ".HD6":    // General - Budget page
                    Variables.globalSettings.GeneralSettings.budgetRevAndExAccts = ConvertFunctions.StringToBool(Functions.GetField(dataLine, ",", 1));

                    // only want to get the frequency if budget is on
                    if (Variables.globalSettings.GeneralSettings.budgetRevAndExAccts == true)
                    {
                        switch (Functions.GetField(dataLine, ",", 2))
                        {
                            case "Annual":
                                Variables.globalSettings.GeneralSettings.budgetFrequency = BUDGET_FREQUENCY.BUDGET_ANNUAL;
                                break;
                            case "Semi-annual":
                                Variables.globalSettings.GeneralSettings.budgetFrequency = BUDGET_FREQUENCY.BUDGET_SEMI_ANNUAL;
                                break;
                            case "Quarterly":
                                Variables.globalSettings.GeneralSettings.budgetFrequency = BUDGET_FREQUENCY.BUDGET_QUARTERLY;
                                break;
                            case "Bi-monthly":
                                Variables.globalSettings.GeneralSettings.budgetFrequency = BUDGET_FREQUENCY.BUDGET_BIMONTHLY;
                                break;
                            case "Monthly":
                                Variables.globalSettings.GeneralSettings.budgetFrequency = BUDGET_FREQUENCY.BUDGET_MONTHLY;
                                break;
                            case "13-period":
                                Variables.globalSettings.GeneralSettings.budgetFrequency = BUDGET_FREQUENCY.BUDGET_PERIOD;
                                break;
                            default:
                                {
                                    Functions.Verify(false, true, "Correct value sent");
                                    break;
                                }
                        }
                    }
                    break;
                case ".HD7":    // General - Numbering page
                    Variables.globalSettings.GeneralSettings.Numbering.showAcctNumInTransactions = ConvertFunctions.StringToBool(Functions.GetField(dataLine, ",", 1));
                    Variables.globalSettings.GeneralSettings.Numbering.showAcctNumInReports = ConvertFunctions.StringToBool(Functions.GetField(dataLine, ",", 2));
                    Variables.globalSettings.GeneralSettings.Numbering.numOfDigitsInAcctNum = Functions.GetField(dataLine, ",", 3);
                    Variables.globalSettings.GeneralSettings.Numbering.assetStartNum = Functions.GetField(dataLine, ",", 4);
                    Variables.globalSettings.GeneralSettings.Numbering.assetEndNum = Functions.GetField(dataLine, ",", 5);
                    Variables.globalSettings.GeneralSettings.Numbering.liabilityStartNum = Functions.GetField(dataLine, ",", 6);
                    Variables.globalSettings.GeneralSettings.Numbering.liabilityEndNum = Functions.GetField(dataLine, ",", 7);
                    Variables.globalSettings.GeneralSettings.Numbering.equityStartNum = Functions.GetField(dataLine, ",", 8);
                    Variables.globalSettings.GeneralSettings.Numbering.equityEndNum = Functions.GetField(dataLine, ",", 9);
                    Variables.globalSettings.GeneralSettings.Numbering.revStartNum = Functions.GetField(dataLine, ",", 10);
                    Variables.globalSettings.GeneralSettings.Numbering.revEndNum = Functions.GetField(dataLine, ",", 11);
                    Variables.globalSettings.GeneralSettings.Numbering.expStartNum = Functions.GetField(dataLine, ",", 12);
                    Variables.globalSettings.GeneralSettings.Numbering.expEndNum = Functions.GetField(dataLine, ",", 13);
                    break;
                case ".HD8":    // Payables - Options
                    Variables.globalSettings.PayableSettings.agingPeriod1 = Functions.GetField(dataLine, ",", 1);
                    Variables.globalSettings.PayableSettings.agingPeriod2 = Functions.GetField(dataLine, ",", 2);
                    Variables.globalSettings.PayableSettings.agingPeriod3 = Functions.GetField(dataLine, ",", 3);
                    Variables.globalSettings.PayableSettings.calculateDiscountsBeforeTax = ConvertFunctions.StringToBool(Functions.GetField(dataLine, ",", 4));
                    break;
                case ".HD9":    // Payables - Duty
                    Variables.globalSettings.PayableSettings.trackDutyOnImportedItems = ConvertFunctions.StringToBool(Functions.GetField(dataLine, ",", 1));
                    Variables.globalSettings.PayableSettings.importDutyAcct.acctNumber = Functions.GetField(dataLine, ",", 2);
                    break;
                case ".HD11":   // Receivables - Options and Discounts
                    Variables.globalSettings.ReceivableSettings.agingPeriod1 = Functions.GetField(dataLine, ",", 1);
                    Variables.globalSettings.ReceivableSettings.agingPeriod2 = Functions.GetField(dataLine, ",", 2);
                    Variables.globalSettings.ReceivableSettings.agingPeriod3 = Functions.GetField(dataLine, ",", 3);
                    Variables.globalSettings.ReceivableSettings.interestCharges = ConvertFunctions.StringToBool(Functions.GetField(dataLine, ",", 4));
                    Variables.globalSettings.ReceivableSettings.interestPercent = Functions.GetField(dataLine, ",", 5);
                    Variables.globalSettings.ReceivableSettings.interestDays = Functions.GetField(dataLine, ",", 6);
                    Variables.globalSettings.ReceivableSettings.statementDays = Functions.GetField(dataLine, ",", 7);
                    Variables.globalSettings.ReceivableSettings.discountPercent = Functions.GetField(dataLine, ",", 8);
                    Variables.globalSettings.ReceivableSettings.discountDays = Functions.GetField(dataLine, ",", 9);
                    Variables.globalSettings.ReceivableSettings.netDays = Functions.GetField(dataLine, ",", 10);
                    Variables.globalSettings.ReceivableSettings.calculateLineItemDiscounts = ConvertFunctions.StringToBool(Functions.GetField(dataLine, ",", 11));
                    Variables.globalSettings.ReceivableSettings.taxCodeForNewCustomers.code = Functions.GetField(dataLine, ",", 12);
                    Variables.globalSettings.ReceivableSettings.printSalesperson = ConvertFunctions.StringToBool(Functions.GetField(dataLine, ",", 13));
                    break;
                case ".HD12":   // Receivables - Comments
                    Variables.globalSettings.ReceivableSettings.salesInvoiceComment = Functions.GetField(dataLine, ",", 1);
                    Variables.globalSettings.ReceivableSettings.salesOrderComment = Functions.GetField(dataLine, ",", 2);
                    Variables.globalSettings.ReceivableSettings.salesQuoteComment = Functions.GetField(dataLine, ",", 3);
                    break;
                case ".HD13":   // Payroll - Income and Taxes
                                //CDN only
                    Variables.globalSettings.PayrollSettings.TaxSettings.eiFactor = Functions.GetField(dataLine, ",", 1);
                    Variables.globalSettings.PayrollSettings.TaxSettings.wcbRate = Functions.GetField(dataLine, ",", 2);
                    Variables.globalSettings.PayrollSettings.TaxSettings.ehtFactor = Functions.GetField(dataLine, ",", 3);
                    Variables.globalSettings.PayrollSettings.TaxSettings.qhsfFactor = Functions.GetField(dataLine, ",", 4);

                    Variables.globalSettings.PayrollSettings.IncomeSettings.trackQuebecTips = ConvertFunctions.StringToBool(Functions.GetField(dataLine, ",", 5));
                    Variables.globalSettings.PayrollSettings.IncomeSettings.Incomes.Clear();
                    int iLine = 6;
                    for (int x = 1; x <= 23; x++)
                    {
                        PAYROLL_INCOME PI = new PAYROLL_INCOME();

                        switch (Functions.GetField(dataLine, ",", iLine))
                        {
                            case "Income":
                                PI.IncomeType = INCOME_TYPE.INCOME_TYPE_INCOME;
                                break;
                            case "Benefit":
                                PI.IncomeType = INCOME_TYPE.INCOME_TYPE_BENEFIT;
                                break;
                            case "Reimbursement":
                                PI.IncomeType = INCOME_TYPE.INCOME_TYPE_REIMBURSEMENT;
                                break;
                            case "Hourly Rate":
                                PI.IncomeType = INCOME_TYPE.INCOME_TYPE_HOURLY_RATE;
                                break;
                            case "Piece Rate":
                                PI.IncomeType = INCOME_TYPE.INCOME_TYPE_PIECE_RATE;
                                break;
                            case "Differential Rate":
                                PI.IncomeType = INCOME_TYPE.INCOME_TYPE_DIFFERENTIAL_RATE;
                                break;
                            default:
                                {
                                    Functions.Verify(false, true, "Correct income type located");
                                    break;
                                }
                        }
                        PI.unitofMeasure = Functions.GetField(dataLine, ",", iLine++);
                        Variables.globalSettings.PayrollSettings.IncomeSettings.Incomes.Add(PI);
                        iLine++;
                    }

                    //US only
                    Variables.globalSettings.PayrollSettings.TaxSettings.sdiRate = Functions.GetField(dataLine, ",", 52);
                    Variables.globalSettings.PayrollSettings.TaxSettings.sutaRate = Functions.GetField(dataLine, ",", 53);
                    Variables.globalSettings.PayrollSettings.TaxSettings.futaRate = Functions.GetField(dataLine, ",", 54);
                    break;
                case ".HD14":   // Payroll - Deductions
                    Variables.globalSettings.PayrollSettings.Deductions.Clear();

                    iLine = 1;
                    for (int x = 1; x <= 20; x++)
                    {
                        PAYROLL_DEDUCTION PD = new PAYROLL_DEDUCTION();
                        PD.Deduction = "Deduction " + x + "";
                        if (Functions.GetField(dataLine, ",", iLine++) == "Amount")
                        {
                            PD.DeductBy = DEDUCT_TYPE.DEDUCT_AMOUNT;
                        }
                        else
                        {
                            PD.DeductBy = DEDUCT_TYPE.DEDUCT_PERCENT;
                        }
                        Variables.globalSettings.PayrollSettings.Deductions.Add(PD);
                    }
                    break;
                case ".HD15":   // Payroll - Entitlements
                    Variables.globalSettings.PayrollSettings.EntitlementSettings.numOfHrsInTheWorkDay = Functions.GetField(dataLine, ",", 1);
                    Variables.globalSettings.PayrollSettings.EntitlementSettings.Entitlements.Clear();

                    iLine = 2;
                    for (int x = 1; x <= 5; x++)
                    {
                        PAYROLL_ENTITLEMENTS PE = new PAYROLL_ENTITLEMENTS();
                        PE.entitlementName = Functions.GetField(dataLine, ",", iLine);
                        PE.percentOfHrsWorked = Functions.GetField(dataLine, ",", iLine++);
                        PE.maxDays = Functions.GetField(dataLine, ",", iLine++);
                        if (Functions.GetField(dataLine, ",", iLine++) == "Yes")
                        {
                            PE.clearDaysAtYearEnd = true;
                        }
                        else
                        {
                            PE.clearDaysAtYearEnd = false;
                        }
                        Variables.globalSettings.PayrollSettings.EntitlementSettings.Entitlements.Add(PE);
                        iLine++;
                    }
                    break;
                case ".HD16":	// Payroll - Remittance

                    PAYROLL_REMITTANCE remit = new PAYROLL_REMITTANCE();
                    remit.RemitName = Functions.GetField(dataLine, ",", 1);
                    remit.RemitVendor = Functions .GetField(dataLine, ",", 2);
                    Variables.globalSettings.PayrollSettings.Remittances.Add(remit);


                    iLine = 3;
                    while ((Functions.GetField(dataLine, ",", iLine)) != "")
                    {
                        PAYROLL_REMITTANCE PR = new PAYROLL_REMITTANCE();
                        PR.RemitName = Functions.GetField(dataLine, ",", iLine);
                        PR.RemitVendor = Functions.GetField(dataLine, ",", ++iLine);
                        Variables.globalSettings.PayrollSettings.Remittances.Add(PR);
                        iLine++;
                    }
                    break;
                case ".HD17":   // Inventory & Services - Options
                    if (Functions.GetField(dataLine, ",", 1) == "Markup")
                    {
                        Variables.globalSettings.InventorySettings.profitMethod = PROFIT_METHOD.MARKUP;
                    }
                    else
                    {
                        Variables.globalSettings.InventorySettings.profitMethod = PROFIT_METHOD.MARGIN;
                    }
                    if (Functions .GetField(dataLine, ",", 2) == "Number")
                    {
                        Variables.globalSettings.InventorySettings.sortMethod = SORT_METHOD.SORT_NUMBER;
                    }
                    else
                    {
                        Variables. globalSettings.InventorySettings.sortMethod = SORT_METHOD.SORT_DESCRIPTION;
                    }
                    if (Functions.GetField(dataLine, ",", 3).Contains("Calculated"))
                    {
                        Variables.globalSettings.InventorySettings.priceMethod = PRICE_METHOD.CALCULATED;
                    }
                    else
                    {
                        Variables.globalSettings.InventorySettings.priceMethod = PRICE_METHOD.TAKEN;
                    }
                    Variables.globalSettings.InventorySettings.allowInventoryLevelsToGoBelowZero = ConvertFunctions.StringToBool(Functions.GetField(dataLine, ",", 4));
                    if (Functions.GetField(dataLine, ",", 5) == "FIFO")
                    {
                        Variables.globalSettings.InventorySettings.inventoryCostingMethod = COSTING_METHOD.FIFO_COST;
                    }
                    else
                    {
                        Variables.globalSettings.InventorySettings.inventoryCostingMethod = COSTING_METHOD.AVERAGE_COST;
                    }
                    Variables.globalSettings.InventorySettings.allowSerialNums = ConvertFunctions.StringToBool(Functions.GetField(dataLine, ",", 6));
                    break;
                case ".HD18":   // Project - Budget
                    Variables.globalSettings.ProjectSettings.budgetProjects = ConvertFunctions.StringToBool(Functions.GetField(dataLine, ",", 1));
                    if (Variables.globalSettings.ProjectSettings.budgetProjects == true)
                    {
                        switch (Functions.GetField(dataLine, ",", 2))
                        {
                            case "Annual":
                                Variables.globalSettings.ProjectSettings.budgetPeriodFrequency = BUDGET_FREQUENCY.BUDGET_ANNUAL;
                                break;
                            case "Semi-annual":
                                Variables.globalSettings.ProjectSettings.budgetPeriodFrequency = BUDGET_FREQUENCY.BUDGET_SEMI_ANNUAL;
                                break;
                            case "Quarterly":
                                Variables.globalSettings.ProjectSettings.budgetPeriodFrequency = BUDGET_FREQUENCY.BUDGET_QUARTERLY;
                                break;
                            case "Bi-monthly":
                                Variables.globalSettings.ProjectSettings.budgetPeriodFrequency = BUDGET_FREQUENCY.BUDGET_BIMONTHLY;
                                break;
                            case "Monthly":
                                Variables.globalSettings.ProjectSettings.budgetPeriodFrequency = BUDGET_FREQUENCY.BUDGET_MONTHLY;
                                break;
                            case "13-period":
                                Variables.globalSettings.ProjectSettings.budgetPeriodFrequency = BUDGET_FREQUENCY.BUDGET_PERIOD;
                                break;
                            default:
                                {
                                    Functions.Verify(false, true, "Correct value sent");
                                    break;
                                }
                        }
                    }
                    break;
                case ".HD19":   // Project - Allocation							
                    switch (Functions.GetField(dataLine, ",", 1))
                    {
                        case "Amount":
                            Variables.globalSettings.ProjectSettings.payrollAllocationMethod = ALLOCATE_PAYROLL.ALLOCATE_AMOUNT;
                            break;
                        case "Percent":
                            Variables.globalSettings.ProjectSettings.payrollAllocationMethod = ALLOCATE_PAYROLL.ALLOCATE_PERCENT;
                            break;
                        case "Hours":
                            Variables.globalSettings.ProjectSettings.payrollAllocationMethod = ALLOCATE_PAYROLL.ALLOCATE_HOURS;
                            break;
                        default:
                            {
                                Functions.Verify(false, true, "Correct value sent");
                                break;
                            }
                    }
                    switch (Functions.GetField(dataLine, ",", 2))
                    {
                        case "Amount":
                            Variables.globalSettings.ProjectSettings.otherAllocationMethod = ALLOCATE_TRANSACTIONS.ALLOCATE_TRANS_AMOUNT;
                            break;
                        case "Percent":
                            Variables.globalSettings.ProjectSettings.otherAllocationMethod = ALLOCATE_TRANSACTIONS.ALLOCATE_TRANS_PERCENT;
                            break;
                        default:
                            {
                                Functions.Verify(false, true, "Correct value sent");
                                break;
                            }
                    }
                    Variables.globalSettings.ProjectSettings.warnIfAllocationIsNotComplete = ConvertFunctions.StringToBool(Functions.GetField(dataLine, ",", 3));
                    Variables.globalSettings.ProjectSettings.allowAccessToAllocateFieldUsingTab = ConvertFunctions.StringToBool(Functions.GetField(dataLine, ",", 4));
                    break;
                default:
                    {
                        Functions.Verify(false, true, "Valid extension type sent");
                        break;
                    }
            }
        }

        
        
        // //  WriteFile methods
        /*
        public void DataFile_Taxes_WriteFile()
        {
            string FUNCTION_ALIAS = "Tax";
            string EXTENSION_TAXES_CONTAINER = ".hdr";
            string EXTENSION_TAX_CODES_CONTAINER = ".dt1";
            string EXTENSION_TAXABLE_TAX_LIST_CONTAINER = ".dt2";
            string EXTENSION_TAX_CODE_DETAILS_CONTAINER = ".dt3";

            // Get the step count
            string stepCount = pGetExternalData(FILE_STEP_COUNTER, "stepCounter");

            // Create the path for the data file to be stored
            string dataPath = pGetExternalData(FILE_CURRENT_FOLDER,"silkLocation") + "\\" +
            "data\\SA" + SA_VERSION + "\\" +
            pGetExternalData(FILE_CURRENT_FOLDER,"userName") + "\\" +
            pGetExternalData(FILE_CURRENT_FOLDER,"dbName") + "\\" +
            pGetExternalData(FILE_CURRENT_FOLDER,"testName") + "\\";

            //assuming we always want all taxes for the datafiles, not just 1
            if (!(Settings.Instance.Window.Exists()))
            {
                Settings._SA_TaxesInvoke ();
            }
            else if (!(Settings.Taxes.Exists()))
            {
                Settings._SA_TaxesInvoke ();
            }

            List<TAX> lsTaxes = Settings._SA_ReadAllTaxes ();
            int x, y;
            for (int x = 1; x <= lsTaxes.Count; x++)
            {
                pWriteToDataFile(Settings.DataFile_Taxes_updateListOfStrings(EXTENSION_TAXES_CONTAINER, lsTaxes[x]), dataPath, FUNCTION_ALIAS, stepCount, EXTENSION_TAXES_CONTAINER);
                if (Functions.GoodData (lsTaxes[x].TaxAuthoritiesToBeCharged))
                {
                    for (int y = 1; y <= lsTaxes[x].TaxAuthoritiesToBeCharged.Count; y++)
                    {
                        pWriteToDataFile(Settings.DataFile_Taxes_updateListOfStrings(EXTENSION_TAXABLE_TAX_LIST_CONTAINER, lsTaxes[x], y), dataPath, FUNCTION_ALIAS, stepCount, EXTENSION_TAXABLE_TAX_LIST_CONTAINER);
                    }
                }
            }


            Settings._SA_TaxCodesInvoke ();
            List<TAX_CODE> lsTaxCodes = Settings._SA_ReadAllTaxCodes ();
            for (int x = 1; x <= lsTaxCodes.Count; x++)
            {
                pWriteToDataFile(Settings.DataFile_Taxes_updateListOfStrings(EXTENSION_TAX_CODES_CONTAINER, null, null, lsTaxCodes[x]), dataPath, FUNCTION_ALIAS, stepCount, EXTENSION_TAX_CODES_CONTAINER);
                {
                    for (int y = 1; y <= lsTaxCodes[x].TaxDetails.Count; y++)
                    {
                        pWriteToDataFile(Settings.DataFile_Taxes_updateListOfStrings(EXTENSION_TAX_CODE_DETAILS_CONTAINER, null, null, lsTaxCodes[x], y), dataPath, FUNCTION_ALIAS, stepCount, EXTENSION_TAX_CODE_DETAILS_CONTAINER);
                    }
                }
            }

        }
        

        // Update methods

        public static List<string> DataFile_Taxes_updateListOfStrings(string extension)
        {
            return DataFile_Taxes_updateListOfStrings(extension, null, null, null, null);
        }

        public static List<string> DataFile_Taxes_updateListOfStrings(string extension, TAX Tax)
        {
            return DataFile_Taxes_updateListOfStrings(extension, Tax, null, null, null);
        }

        public static List<string> DataFile_Taxes_updateListOfStrings(string extension, TAX Tax, int? iAuthLine)
        {
            return DataFile_Taxes_updateListOfStrings(extension, Tax, iAuthLine, null, null);
        }

        public static List<string> DataFile_Taxes_updateListOfStrings(string extension, TAX Tax, int? iAuthLine, TAX_CODE TaxCode)
        {
            return DataFile_Taxes_updateListOfStrings(extension, Tax, iAuthLine, TaxCode, null);
        }

        public static List<string> DataFile_Taxes_updateListOfStrings(string extension, TAX Tax, int? iAuthLine, TAX_CODE TaxCode, int? iTaxCodeDetailLine)
        {

            List<string> listContents = new List<string>();
            
            switch (extension.ToUpper())
            {
                case ".HDR":
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Tax.taxName)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Tax.taxID)));
                    listContents.Add(ConvertFunctions.BoolToString(Tax.exempt));
                    listContents.Add(ConvertFunctions.BoolToString(Tax.taxable));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Tax.acctTrackPurchases.acctNumber)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Tax.acctTrackSales.acctNumber)));
                    listContents.Add(ConvertFunctions.BoolToString(Tax.reportOnTaxes));
                    break;

                case ".DT1":
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(TaxCode.code)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(TaxCode.description)));
                                                
                    switch (TaxCode.useIn)
                    {
                        case TAX_USED_IN.TAX_ALL_JOURNALS:
                            listContents.Add("All journals");
                            break;
                        case TAX_USED_IN.TAX_SALES:
                            listContents.Add("Sales");
                            break;
                        case TAX_USED_IN.TAX_PURCHASES:
                            listContents.Add("Purchases");
                            break;
                    }
                    break;

                case ".DT2":                    
                    listContents.Add(Tax.taxName);
                    listContents.Add(Functions.Str(iAuthLine.Value));
                    listContents.Add(Tax.TaxAuthoritiesToBeCharged[iAuthLine.Value]);
                    listContents.Add(ConvertFunctions.BoolToString(true));
                    break;

                case ".DT3":                            
                    listContents.Add(TaxCode.code);
                    listContents.Add(Functions.Str(iTaxCodeDetailLine.Value));
                    listContents.Add(TaxCode.TaxDetails[iTaxCodeDetailLine.Value].Tax.taxName);
                    listContents.Add(TaxCode.TaxDetails[iTaxCodeDetailLine.Value].taxStatus.ToString());
                    listContents.Add(TaxCode.TaxDetails[iTaxCodeDetailLine.Value].rate);
                    if (TaxCode.TaxDetails[iTaxCodeDetailLine.Value].includedInPrice.Value)
                    {
                        listContents.Add("Yes");
                    }
                    else
                    {
                        listContents.Add("No");
                    }
                    if (TaxCode.TaxDetails[iTaxCodeDetailLine.Value].isRefundable.Value)
                    {
                        listContents.Add("Yes");
                    }
                    else
                    {
                        listContents.Add("No");
                    }
                    break;
                default:
                    {
                        Functions.Verify (false, true, "Extension type is correct");
                        break;
                    }
            }

            return listContents;
        }

        public List<string> DataFile_CompanyInfo_updateListOfstrings(string extension)
        {
            List<string> listContents = new List<string>();

            switch (extension.ToUpper())
            {
                case ".HDR":
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.CompanyInformation.companyName)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.CompanyInformation.Address.street1)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.CompanyInformation.Address.street2)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.CompanyInformation.Address.city)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.CompanyInformation.Address.province)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.CompanyInformation.Address.postalCode)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.CompanyInformation.Address.country)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.CompanyInformation.Address.phone1)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.CompanyInformation.Address.phone2)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.CompanyInformation.Address.fax)));

                    //CDN only
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.CompanyInformation.businessNum)));

                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.CompanyInformation.companyType)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.CompanyInformation.Address.provinceCode)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.CompanyInformation.fiscalStart)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.CompanyInformation.fiscalEnd)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.CompanyInformation.earliestTransaction)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.CompanySettings.LogoLocation)));

                    // US only
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.CompanyInformation.federalID)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.CompanyInformation.stateID)));

                    break;
                default:
                {
                    break;
                }
            }
            return listContents;
        }

        public static List<string> DataFile_CreditCard_updateListOfstrings(string extension,int iLine)
        {
            List<string> listContents = new List<string>();

            switch (extension.ToUpper())
            {
                case ".DT1":
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.CompanySettings.CreditCardSettings.CardsUsed[iLine].CardName)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.CompanySettings.CreditCardSettings.CardsUsed[iLine].PayableAccount.acctNumber)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.CompanySettings.CreditCardSettings.CardsUsed[iLine].PayableAccount.acctNumber)));                                        
                    break;
                case ".HDR":
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.CompanySettings.CreditCardSettings.CardsAccepted[iLine].CardName)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.CompanySettings.CreditCardSettings.CardsAccepted[iLine].Discount)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.CompanySettings.CreditCardSettings.CardsAccepted[iLine].ExpenseAccount.acctNumber)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.CompanySettings.CreditCardSettings.CardsAccepted[iLine].ExpenseAccount.acctNumber)));                                        
                    break;
                default:
                {
                    break;
                }
            }
            return listContents;
        }

        public static List<string> DataFile_Currency_updateListOfstrings(string extension)
        {
            return DataFile_Currency_updateListOfstrings(extension, null, null);
        }

        public static List<string> DataFile_Currency_updateListOfstrings(string extension, int? iForeignLine)
        {
            return DataFile_Currency_updateListOfstrings(extension, iForeignLine, null);
        }

        public static List<string> DataFile_Currency_updateListOfstrings(string extension, int? iForeignLine, int? iExchangeLine)
        {
            List<string> listContents = new List<string>();            

            switch (extension.ToUpper())
            {
                case ".HDR":
                    listContents.Add(ConvertFunctions.BoolToString(Variables.GlobalSettings.CompanySettings.CurrencySettings.allowForeignCurrency));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.CompanySettings.CurrencySettings.HomeCurrency.Currency)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.CompanySettings.CurrencySettings.HomeCurrency.currencyCode)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.CompanySettings.CurrencySettings.HomeCurrency.thousandsSeparator)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString( Variables.GlobalSettings.CompanySettings.CurrencySettings.HomeCurrency.symbol)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.CompanySettings.CurrencySettings.HomeCurrency.decimalSeparator)));
                    if (Variables.GlobalSettings.CompanySettings.CurrencySettings.HomeCurrency.symbolPosition == CURRENCY_SYMBOL.CURRENCY_LEADING)
                    {
                        listContents.Add("Leading");
                    }
                    else
                    {
                        listContents.Add("Trailing");
                    }
                    string sDec = Functions.Str(((int)Variables.GlobalSettings.CompanySettings.CurrencySettings.HomeCurrency.decimalPlaces-1));
                    listContents.Add(sDec);
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.CompanySettings.CurrencySettings.HomeCurrency.denomination)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.CompanySettings.CurrencySettings.HomeCurrency.roundingDifferencesAccount)));
                    break;
                case ".CNT":
                    listContents.Add(Variables.GlobalSettings.CompanySettings.CurrencySettings.ForeignCurrencies[iForeignLine.Value].Currency);
                    listContents.Add(Variables.GlobalSettings.CompanySettings.CurrencySettings.ForeignCurrencies[iForeignLine.Value].currencyCode);
                    listContents.Add(Variables.GlobalSettings.CompanySettings.CurrencySettings.ForeignCurrencies[iForeignLine.Value].symbol);
                    if (Variables.GlobalSettings.CompanySettings.CurrencySettings.ForeignCurrencies[iForeignLine.Value].symbolPosition == CURRENCY_SYMBOL.CURRENCY_LEADING)
                    {
                        listContents.Add("Leading");
                    }
                    else
                    {
                        listContents.Add("Trailing");
                    }
                    listContents.Add(Variables.GlobalSettings.CompanySettings.CurrencySettings.ForeignCurrencies[iForeignLine.Value].thousandsSeparator);
                    listContents.Add(Variables.GlobalSettings.CompanySettings.CurrencySettings.ForeignCurrencies[iForeignLine.Value].decimalSeparator);
                    sDec = Functions.Str(((int)Variables.GlobalSettings.CompanySettings.CurrencySettings.ForeignCurrencies[iForeignLine.Value].decimalPlaces-1));
                    listContents.Add(sDec);
                    listContents.Add(Variables.GlobalSettings.CompanySettings.CurrencySettings.ForeignCurrencies[iForeignLine.Value].denomination);
                    break;
                case ".DT1":
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.CompanySettings.CurrencySettings.ForeignCurrencies[iForeignLine.Value].Currency)));
                    listContents.Add(ConvertFunctions.BoolToString(Variables.GlobalSettings.CompanySettings.CurrencySettings.ForeignCurrencies[iForeignLine.Value].exchangeDisplayReminder));
                    string sRateRem;
                                             
                    switch (Variables.GlobalSettings.CompanySettings.CurrencySettings.ForeignCurrencies[iForeignLine.Value].exchangeRateReminder)
                    {
                        case CURRENCY_RATE_REMINDER.RATE_REMINDER_ONE_DAY:
                            sRateRem = "One Day";
                            break;
                        case CURRENCY_RATE_REMINDER.RATE_REMINDER_ONE_WEEK:
                            sRateRem = "One Week";
                            break;
                        case CURRENCY_RATE_REMINDER.RATE_REMINDER_ONE_MONTH:
                            sRateRem = "One Month";
                            break;
                        case CURRENCY_RATE_REMINDER.RATE_REMINDER_THREE_MONTHS:
                            sRateRem = "Three Months";
                            break;
                        case CURRENCY_RATE_REMINDER.RATE_REMINDER_SIX_MONTHS:
                            sRateRem = "Six Months";
                            break;
                        case CURRENCY_RATE_REMINDER.RATE_REMINDER_ONE_YEAR:
                            sRateRem = "One Year";
                            break;
                        default:
                            {
                                sRateRem = "";
                                Functions.Verify(false, true, "Valid value sent to switch");
                                break;
                            }
                    }
                    listContents.Add(sRateRem);
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.CompanySettings.CurrencySettings.ForeignCurrencies[iForeignLine.Value].exchangeWebsite)));
                    break;
                case ".CNT2":                            
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.CompanySettings.CurrencySettings.ForeignCurrencies[iForeignLine.Value].Currency)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.CompanySettings.CurrencySettings.ForeignCurrencies[iForeignLine.Value].ExchangeRates[iExchangeLine.Value].exchangeDate)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.CompanySettings.CurrencySettings.ForeignCurrencies[iForeignLine.Value].ExchangeRates[iExchangeLine.Value].exchangeRate)));
                    break;
                default:
                {
                    Functions.Verify(false, true, "Valid file type");
                    break;
                }
            }

            return listContents;
        }

        public static List<string> DataFile_Department_updateListOfstrings(string extension,int iLine)
        {
            List<string> listContents = new List<string>();

            switch (extension.ToUpper())
            {
                case ".HDR":
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.GeneralSettings.DepartmentalAccounting[iLine].code)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.GeneralSettings.DepartmentalAccounting[iLine].description)));
                    if (Variables.GlobalSettings.GeneralSettings.DepartmentalAccounting[iLine].ActiveStatus.Value)
                    {
                        listContents.Add("Active");
                    }
                    else
                    {
                        listContents.Add("Inactive");
                    }
                    break;
                default:
                 {
                    Functions.Verify(false, true, "Valid extension type sent");
                    break;
                 }
            }
            return listContents;
        }

        public static List<string> DataFile_Location_updateListOfstrings(string extension,int iLine)
        {
            List<string> listContents = new List<string>();

            switch (extension.ToUpper())
            {
                case ".HDR":
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.InventorySettings.Locations[iLine].code)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.InventorySettings.Locations[iLine].description)));
                    if (Variables.GlobalSettings.InventorySettings.Locations[iLine].ActiveStatus.Value)
                    {
                        listContents.Add("Active");
                    }
                    else
                    {
                        listContents.Add("Inactive");
                    }
                    break;
                default:
                {
                    Functions.Verify(false, true, "Valid extension type sent");
                    break;
                }
            }

            return listContents;
        }

        public static List<string> DataFile_Name_updateListOfstrings(string extension)
        {
            List<string> listContents = new List<string>();

            switch (extension.ToUpper())
            {
                case ".HDR":
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.GeneralSettings.AdditionalFields.Field1)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.GeneralSettings.AdditionalFields.Field2)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.GeneralSettings.AdditionalFields.Field3)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.GeneralSettings.AdditionalFields.Field4)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.GeneralSettings.AdditionalFields.Field5)));
                    break;
                case ".HD2":
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.PayableSettings.AdditionalFields.Field1)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.PayableSettings.AdditionalFields.Field2)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.PayableSettings.AdditionalFields.Field3)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.PayableSettings.AdditionalFields.Field4)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.PayableSettings.AdditionalFields.Field5)));
                    break;
                case ".HD3":
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.ReceivableSettings.AdditionalFields.Field1)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.ReceivableSettings.AdditionalFields.Field2)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.ReceivableSettings.AdditionalFields.Field3)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.ReceivableSettings.AdditionalFields.Field4)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.ReceivableSettings.AdditionalFields.Field5)));
                    break;
                case ".HD4":
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.PayrollSettings.AdditionalPayrollSettings.AdditionalFields.Field1)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.PayrollSettings.AdditionalPayrollSettings.AdditionalFields.Field2)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.PayrollSettings.AdditionalPayrollSettings.AdditionalFields.Field3)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.PayrollSettings.AdditionalPayrollSettings.AdditionalFields.Field4)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.PayrollSettings.AdditionalPayrollSettings.AdditionalFields.Field5)));
                    break;
                case ".HD5":
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.PayrollSettings.AdditionalPayrollSettings.provTax)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.PayrollSettings.AdditionalPayrollSettings.workersComp)));
                    
                    string s = "";
                    for (int x = 1; x <= 20; x++)
                    {
                        s = s + Variables.GlobalSettings.PayrollSettings.AdditionalIncome[x].Name;
                        if (x != 20)
                        {
                            s = s + ",";
                        }
                    }
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(s)));

                    s = "";
                    for (int x = 1; x <= 20; x++)
                    {
                        s = s + Variables.GlobalSettings.PayrollSettings.AdditionalDeduction[x].Name;
                        if (x != 20)
                        {
                            s = s + ",";
                        }
                        listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(s)));
                    }
                    break;
                 case ".HD6":
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.PayrollSettings.AdditionalPayrollSettings.ExpenseFields.Field1)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.PayrollSettings.AdditionalPayrollSettings.ExpenseFields.Field2)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.PayrollSettings.AdditionalPayrollSettings.ExpenseFields.Field3)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.PayrollSettings.AdditionalPayrollSettings.ExpenseFields.Field4)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.PayrollSettings.AdditionalPayrollSettings.ExpenseFields.Field5)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.PayrollSettings.AdditionalPayrollSettings.EntitlementFields.Field1)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.PayrollSettings.AdditionalPayrollSettings.EntitlementFields.Field2)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.PayrollSettings.AdditionalPayrollSettings.EntitlementFields.Field3)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.PayrollSettings.AdditionalPayrollSettings.EntitlementFields.Field4)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.PayrollSettings.AdditionalPayrollSettings.EntitlementFields.Field5)));
                    break;
                case ".HD7":
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.InventorySettings.AdditionalFields.Field1)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.InventorySettings.AdditionalFields.Field2)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.InventorySettings.AdditionalFields.Field3)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.InventorySettings.AdditionalFields.Field4)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.InventorySettings.AdditionalFields.Field5)));
                    break;
                case ".HD8":
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.ProjectSettings.ProjectTitle)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.ProjectSettings.AdditionalFields.Field1)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.ProjectSettings.AdditionalFields.Field2)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.ProjectSettings.AdditionalFields.Field3)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.ProjectSettings.AdditionalFields.Field4)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.ProjectSettings.AdditionalFields.Field5)));
                    break;
                case ".HD9":
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.CompanySettings.additionalInformationDate)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.CompanySettings.additionalInformationField)));
                    break;
                default:
                {
                    Functions.Verify(false, true, "Correct extension sent");
                    break;
                }
            }
            return listContents;
        }

        public static List<string> DataFile_Shipper_updateListOfstrings()
        {
            List<string> listContents = new List<string>();

            listContents.Add(ConvertFunctions.BoolToString(Variables.GlobalSettings.CompanySettings.ShipperSettings.TrackShipments));

            for (int x = 1; x <= 12; x++)
            {
                if (Variables.GlobalSettings.CompanySettings.ShipperSettings.ShipServices.Count < x)
                {
                    listContents.Add("");
                    listContents.Add("");
                }
                else
                {
                    if (Functions.GoodData (Variables.GlobalSettings.CompanySettings.ShipperSettings.ShipServices[x].Shipper))
                    {
                        listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.CompanySettings.ShipperSettings.ShipServices[x].Shipper)));
                    }
                    else
                    {
                        listContents.Add("");
                    }
                    if (Functions.GoodData (Variables.GlobalSettings.CompanySettings.ShipperSettings.ShipServices[x].TrackingSite))
                    {
                        listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.CompanySettings.ShipperSettings.ShipServices[x].TrackingSite)));
                    }
                    else
                    {
                        listContents.Add("");
                    }
                }
            }
            return listContents;
        }

        public static List<string> DataFile_PriceList_updateListOfstrings()
        {
            List<string> listContents = new List<string>();

            if (Functions.GoodData (Variables.GlobalSettings.InventorySettings.PriceList))
            {
                for (int x = 1; x <= Variables.GlobalSettings.InventorySettings.PriceList.Count; x++)
                {
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.InventorySettings.PriceList[x].description)));
                    if (Variables.GlobalSettings.InventorySettings.PriceList[x].ActiveStatus.Value)
                    {
                        listContents.Add("Active");
                    }
                    else
                    {
                        listContents.Add("Inactive");
                    }
                }
            }
            else
            {
                listContents.Add("");
                listContents.Add("");
            }
            return listContents;
        }

        public static List<string> DataFile_APLinkAccounts_updateListOfstrings(string extension)
        {
            return DataFile_APLinkAccounts_updateListOfstrings(extension, null);
        }

        public static List<string> DataFile_APLinkAccounts_updateListOfstrings(string extension, int? iLine)
        {
            List<string> listContents = new List<string>();
            switch (extension.ToUpper())
            {
                case ".HD2":
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.PayableSettings.principalBankAcct.acctNumber)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.PayableSettings.apAcct.acctNumber)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.PayableSettings.freightAcct.acctNumber)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.PayableSettings.earlyPayDiscountAcct.acctNumber)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.PayableSettings.prepaymentAcct.acctNumber)));
                    break;
                case ".CNT":
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.PayableSettings.CurrencyAccounts[iLine.Value].Currency)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.PayableSettings.CurrencyAccounts[iLine.Value].BankAccount.acctNumber)));
                    break;
                default:
                {
                    Functions.Verify(false, true, "Correct extension sent");
                    break;
                }
            }
            return listContents;
        }

        public static List<string> DataFile_ARLinkAccounts_updateListOfstrings(string extension)
        {
            return DataFile_ARLinkAccounts_updateListOfstrings(extension, null);
        }

        public static List<string> DataFile_ARLinkAccounts_updateListOfstrings(string extension, int? iLine)
        {
            List<string> listContents = new List<string>();
            switch (extension.ToUpper())
            {
                case ".HD3":
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.ReceivableSettings.principalBankAcct.acctNumber)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.ReceivableSettings.arAcct.acctNumber)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.ReceivableSettings.freightAcct.acctNumber)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.ReceivableSettings.earlyPayDiscountAcct.acctNumber)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.ReceivableSettings.depositsAcct.acctNumber)));
                    break;
                case ".CNT2":
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.ReceivableSettings.CurrencyAccounts[iLine.Value].Currency)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.ReceivableSettings.CurrencyAccounts[iLine.Value].BankAccount.acctNumber)));
                    break;
                default:
                {
                    Functions.Verify(false, true, "Correct extension sent");
                    break;
                }
            }
            return listContents;
        }

        public static List<string> DataFile_GLLinkAccounts_updateListOfstrings(string extension)
        {
            return DataFile_GLLinkAccounts_updateListOfstrings(extension, null);
        }

        public static List<string> DataFile_GLLinkAccounts_updateListOfstrings(string extension, int? iLine)
        {
            List<string> listContents = new List<string>();
            switch (extension.ToUpper())
            {
                case ".HDR":
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.GeneralSettings.RetainedEarnings.acctNumber)));
                    break;
                default:
                {
                    Functions.Verify(false, true, "Correct extension sent");
                    break;
                }
            }
            return listContents;
        }

        public static List<string> DataFile_InvLinkAccounts_updateListOfstrings(string extension)
        {
            return DataFile_InvLinkAccounts_updateListOfstrings(extension, null);
        }

        public static List<string> DataFile_InvLinkAccounts_updateListOfstrings(string extension, int? iLine)
        {
            List<string> listContents = new List<string>();
            switch (extension.ToUpper())
            {
                case ".HD8":
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.InventorySettings.itemAssemblyCosts.acctNumber)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.InventorySettings.adjustmentWriteOff.acctNumber)));
                    break;
                default:
                {
                    Functions.Verify(false, true, "Correct extension sent");
                    break;
                }
            }
            return listContents;
        }

        public static List<string> DataFile_PayrollLinkAccounts_updateListOfstrings(string extension)
        {
            List<string> listContents = new List<string>();
            
            switch (extension.ToUpper())
            {
                case ".HD4":
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.PayrollSettings.IncomeAccounts.principalBank.acctNumber)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.PayrollSettings.IncomeAccounts.vacation.acctNumber)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.PayrollSettings.IncomeAccounts.advances.acctNumber)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.PayrollSettings.IncomeAccounts.vacationEarnedLinkedAccount.acctNumber)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.PayrollSettings.IncomeAccounts.regularWageLinkedAccount.acctNumber)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.PayrollSettings.IncomeAccounts.ot1LinkedAccount.acctNumber)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.PayrollSettings.IncomeAccounts.ot2LinkedAccount.acctNumber)));
                    for (int x = 1; x <= 20; x++)
                    {
                        listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.PayrollSettings.AdditionalIncome[x].LinkedAccount.acctNumber)));
                    }
                    break;
                case ".HD5":
                    for (int x = 1; x <= 20; x++)
                    {
                        listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.PayrollSettings.AdditionalDeduction[x].LinkedAccount.acctNumber)));
                    }
                    break;
                case ".HD6":
                    // Canadian
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.PayrollSettings.TaxAccounts.payEI.acctNumber)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.PayrollSettings.TaxAccounts.payCPP.acctNumber)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.PayrollSettings.TaxAccounts.payTax.acctNumber)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.PayrollSettings.TaxAccounts.payWCB.acctNumber)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.PayrollSettings.TaxAccounts.payEHT.acctNumber)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.PayrollSettings.TaxAccounts.payQueTax.acctNumber)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.PayrollSettings.TaxAccounts.payQPP.acctNumber)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.PayrollSettings.TaxAccounts.payQHSF.acctNumber)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.PayrollSettings.TaxAccounts.exEI.acctNumber)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.PayrollSettings.TaxAccounts.exCPP.acctNumber)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.PayrollSettings.TaxAccounts.exWCB.acctNumber)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.PayrollSettings.TaxAccounts.exEHT.acctNumber)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.PayrollSettings.TaxAccounts.exQPP.acctNumber)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.PayrollSettings.TaxAccounts.exQHSF.acctNumber)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.PayrollSettings.TaxAccounts.payQPIP.acctNumber)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.PayrollSettings.TaxAccounts.exQPIP.acctNumber)));                    
                    break;
                case ".HD7":
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.PayrollSettings.UserDefinedAccounts.payable1Acct.acctNumber)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.PayrollSettings.UserDefinedAccounts.payable2Acct.acctNumber)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.PayrollSettings.UserDefinedAccounts.payable3Acct.acctNumber)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.PayrollSettings.UserDefinedAccounts.payable4Acct.acctNumber)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.PayrollSettings.UserDefinedAccounts.payable5Acct.acctNumber)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.PayrollSettings.UserDefinedAccounts.expense1Acct.acctNumber)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.PayrollSettings.UserDefinedAccounts.expense2Acct.acctNumber)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.PayrollSettings.UserDefinedAccounts.expense3Acct.acctNumber)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.PayrollSettings.UserDefinedAccounts.expense4Acct.acctNumber)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.PayrollSettings.UserDefinedAccounts.expense5Acct.acctNumber)));
                    break;
                default:
                {
                    Functions.Verify(false, true, "Correct extension sent");
                    break;
                }
            }
            return listContents;
        }

        public static List<string> DataFile_PayrollJob_updateListOfstrings(string extension)
        {
            return DataFile_PayrollJob_updateListOfstrings(extension, null);
        }

        public static List<string> DataFile_PayrollJob_updateListOfstrings(string extension, int? iLine)
        {
            List<string> listContents = new List<string>();
            switch (extension.ToUpper())
            {
                case ".HDR":
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.PayrollSettings.JobCategories[iLine.Value].Category)));
                    listContents.Add(ConvertFunctions.BoolToString( Variables.GlobalSettings.PayrollSettings.JobCategories[iLine.Value].SubmitTimeSlips));
                    listContents.Add(ConvertFunctions.BoolToString( Variables.GlobalSettings.PayrollSettings.JobCategories[iLine.Value].AreSalespersons));
                    if (Variables.GlobalSettings.PayrollSettings.JobCategories[iLine.Value].ActiveStatus.Value)
                    {
                        listContents.Add("Active");
                    }
                    else
                    {
                        listContents.Add("Inactive");
                    }
                    break;
                case ".DT1":
                    //STRING value
                    //for each value in JobCatAssignmentLine
                        //ListAppend (listContents, value)
                    List<string> lsEmpNames = new List<string>() {};

                    for (int x = 1; x <= Variables.GlobalSettings.PayrollSettings.JobCategories.Count; x++)
                    {
                        if (Functions.GoodData (Variables.GlobalSettings.PayrollSettings.JobCategories[x].EmployeesAssigned))
                        {                            
                            for (int y = 1; y <= Variables.GlobalSettings.PayrollSettings.JobCategories[x].EmployeesAssigned.Count; y++)
                            {                                
                                if (!lsEmpNames.Contains(Variables.GlobalSettings.PayrollSettings.JobCategories[x].EmployeesAssigned[y].employeeName)) 
                                {
                                    lsEmpNames.Add(Variables.GlobalSettings.PayrollSettings.JobCategories[x].EmployeesAssigned[y].employeeName);
                                }
                            }
                         }
                   }
                   if (Functions.GoodData (lsEmpNames))
                   {
                       for (int x = 1; x <= lsEmpNames.Count; x++)
                       {
                           listContents.Add(lsEmpNames[x]);
                       }
                   }
                   break;
                default:
                    {
                        Functions.Verify(false, true, "Correct extension sent");
                        break;
                    }                                
            }
            return listContents;
        }


        public static List<string> DataFile_MainSettings_updateListOfstrings(string extension)
        {
            return DataFile_MainSettings_updateListOfstrings(extension, null);
        }

        public static List<string> DataFile_MainSettings_updateListOfstrings(string extension, int? iLine)
        {
            //int x;
            List<string> listContents = new List<string>();
            switch (extension.ToUpper())
            {
                case ".HDR":	// Company - System page and Company - Backup page
                    listContents.Add(ConvertFunctions.BoolToString(Variables.GlobalSettings.CompanySettings.SystemSettings.useCashBasisAccounting));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.CompanySettings.SystemSettings.cashBasisDate)));
                    listContents.Add(ConvertFunctions.BoolToString(Variables.GlobalSettings.CompanySettings.SystemSettings.storeInvoiceLookupDetails));
                    listContents.Add(ConvertFunctions.BoolToString(Variables.GlobalSettings.CompanySettings.SystemSettings.useChequeNo));
                    listContents.Add(ConvertFunctions.BoolToString(Variables.GlobalSettings.CompanySettings.SystemSettings.doNotAllowTransactionsDatedBefore));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.CompanySettings.SystemSettings.lockingDate)));
                    listContents.Add(ConvertFunctions.BoolToString( Variables.GlobalSettings.CompanySettings.SystemSettings.allowTransactionsInTheFuture));
                    listContents.Add(ConvertFunctions.BoolToString( Variables.GlobalSettings.CompanySettings.SystemSettings.warnIfTransactionsAre));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.CompanySettings.SystemSettings.daysInTheFuture)));
                    listContents.Add(ConvertFunctions.BoolToString( Variables.GlobalSettings.CompanySettings.SystemSettings.warnIfAccountsAreNotBalanced));
                    listContents.Add(ConvertFunctions.BoolToString(Variables.GlobalSettings.CompanySettings.BackupSettings.displayReminderOnSession));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.CompanySettings.BackupSettings.reminderFrequency)));
                    listContents.Add(ConvertFunctions.BoolToString(Variables.GlobalSettings.CompanySettings.BackupSettings.displayReminderWhenClosing));
                    break;
                case ".HD2":	// Company - Features page
                    listContents.Add(ConvertFunctions.BoolToString(Variables.GlobalSettings.CompanySettings.FeatureSettings.ordersForVendors));
                    listContents.Add(ConvertFunctions.BoolToString(Variables.GlobalSettings.CompanySettings.FeatureSettings.quotesForVendors));
                    listContents.Add(ConvertFunctions.BoolToString(Variables.GlobalSettings.CompanySettings.FeatureSettings.ordersForCustomers));
                    listContents.Add(ConvertFunctions.BoolToString(Variables.GlobalSettings.CompanySettings.FeatureSettings.quotesForCustomers));
                    listContents.Add(ConvertFunctions.BoolToString(Variables.GlobalSettings.CompanySettings.FeatureSettings.projectCheckBox));
                    break;
                case ".HD3":	// Company - Forms page
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.CompanySettings.FormSettings.nextNumInvoice)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.CompanySettings.FormSettings.nextNumSalesQuote)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.CompanySettings.FormSettings.nextNumReceipt)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.CompanySettings.FormSettings.nextNumCustomerDeposit)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.CompanySettings.FormSettings.nextNumPurchaseOrder)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.CompanySettings.FormSettings.nextNumDirectDeposit)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.CompanySettings.FormSettings.nextNumTimeSlip)));

                    listContents.Add(ConvertFunctions.BoolToString(Variables.GlobalSettings.CompanySettings.FormSettings.verifyNumSeqInvoice));
                    listContents.Add(ConvertFunctions.BoolToString(Variables.GlobalSettings.CompanySettings.FormSettings.verifyNumSeqSalesQuote));
                    listContents.Add(ConvertFunctions.BoolToString(Variables.GlobalSettings.CompanySettings.FormSettings.verifyNumSeqReceipt));
                    listContents.Add(ConvertFunctions.BoolToString(Variables.GlobalSettings.CompanySettings.FormSettings.verifyNumSeqCustomerDeposit));
                    listContents.Add(ConvertFunctions.BoolToString(Variables.GlobalSettings.CompanySettings.FormSettings.verifyNumSeqPurchaseOrder));
                    listContents.Add(ConvertFunctions.BoolToString(Variables.GlobalSettings.CompanySettings.FormSettings.verifyNumSeqCheque));
                    listContents.Add(ConvertFunctions.BoolToString(Variables.GlobalSettings.CompanySettings.FormSettings.verifyNumSeqDirectDeposit));
                    listContents.Add(ConvertFunctions.BoolToString(Variables.GlobalSettings.CompanySettings.FormSettings.verifyNumSeqTimeSlips));
                    listContents.Add(ConvertFunctions.BoolToString(Variables.GlobalSettings.CompanySettings.FormSettings.verifyNumSeqDepositSlips));

                    listContents.Add(ConvertFunctions.BoolToString(Variables.GlobalSettings.CompanySettings.FormSettings.confirmPrintEmailInvoice));
                    listContents.Add(ConvertFunctions.BoolToString(Variables.GlobalSettings.CompanySettings.FormSettings.confirmPrintEmailSalesQuote));
                    listContents.Add(ConvertFunctions.BoolToString(Variables.GlobalSettings.CompanySettings.FormSettings.confirmPrintEmailSalesOrder));
                    listContents.Add(ConvertFunctions.BoolToString(Variables.GlobalSettings.CompanySettings.FormSettings.confirmPrintEmailReceipt));
                    listContents.Add(ConvertFunctions.BoolToString(Variables.GlobalSettings.CompanySettings.FormSettings.confirmPrintEmailPurchaseOrder));
                    listContents.Add(ConvertFunctions.BoolToString(Variables.GlobalSettings.CompanySettings.FormSettings.confirmPrintEmailChequesOrder));
                    listContents.Add(ConvertFunctions.BoolToString(Variables.GlobalSettings.CompanySettings.FormSettings.confirmPrintEmailDirectDeposit));
                    listContents.Add(ConvertFunctions.BoolToString(Variables.GlobalSettings.CompanySettings.FormSettings.confirmPrintEmailTimeSlips));
                    listContents.Add(ConvertFunctions.BoolToString(Variables.GlobalSettings.CompanySettings.FormSettings.confirmPrintEmailDepositSlips));

                    listContents.Add(ConvertFunctions.BoolToString(Variables.GlobalSettings.CompanySettings.FormSettings.printCompAddressInvoice));
                    listContents.Add(ConvertFunctions.BoolToString(Variables.GlobalSettings.CompanySettings.FormSettings.printCompAddressSalesQuotes));
                    listContents.Add(ConvertFunctions.BoolToString(Variables.GlobalSettings.CompanySettings.FormSettings.printCompAddressSalesOrders));
                    listContents.Add(ConvertFunctions.BoolToString(Variables.GlobalSettings.CompanySettings.FormSettings.printCompAddressReceipt));
                    listContents.Add(ConvertFunctions.BoolToString(Variables.GlobalSettings.CompanySettings.FormSettings.printCompAddressStatements));
                    listContents.Add(ConvertFunctions.BoolToString(Variables.GlobalSettings.CompanySettings.FormSettings.printCompAddressPurchaseOrders));
                    listContents.Add(ConvertFunctions.BoolToString(Variables.GlobalSettings.CompanySettings.FormSettings.printCompAddressCheque));
                    listContents.Add(ConvertFunctions.BoolToString(Variables.GlobalSettings.CompanySettings.FormSettings.printCompAddressDirectDeposit));

                    listContents.Add(ConvertFunctions.BoolToString(Variables.GlobalSettings.CompanySettings.FormSettings.printInBatchesInvoice));
                    listContents.Add(ConvertFunctions.BoolToString(Variables.GlobalSettings.CompanySettings.FormSettings.printInBatchesPackingSlip));
                    listContents.Add(ConvertFunctions.BoolToString(Variables.GlobalSettings.CompanySettings.FormSettings.printInBatchesSalesQuotes));
                    listContents.Add(ConvertFunctions.BoolToString(Variables.GlobalSettings.CompanySettings.FormSettings.printInBatchesSalesOrders));
                    listContents.Add(ConvertFunctions.BoolToString(Variables.GlobalSettings.CompanySettings.FormSettings.printInBatchesReceipt));
                    listContents.Add(ConvertFunctions.BoolToString(Variables.GlobalSettings.CompanySettings.FormSettings.printInBatchesPurchaseOrders));
                    listContents.Add(ConvertFunctions.BoolToString(Variables.GlobalSettings.CompanySettings.FormSettings.printInBatchesCheque));
                    listContents.Add(ConvertFunctions.BoolToString(Variables.GlobalSettings.CompanySettings.FormSettings.printInBatchesDirectDeposit));
                    listContents.Add(ConvertFunctions.BoolToString(Variables.GlobalSettings.CompanySettings.FormSettings.printInBatchesDepositSlip));

                    listContents.Add(ConvertFunctions.BoolToString(Variables.GlobalSettings.CompanySettings.FormSettings.checkForDupInvoices));
                    listContents.Add(ConvertFunctions.BoolToString(Variables.GlobalSettings.CompanySettings.FormSettings.checkForDupReceipts));
                    break;
                case ".HD4":	// Company - E-mail page
                    if (Variables.GlobalSettings.CompanySettings.EmailSettings.AttachmentFormat == EMAIL_ATTACH.EMAIL_PDF)
                    {
                        listContents.Add("PDF");
                    }
                    else
                    {
                        listContents.Add("RTF");
                    }
                    for (int x = 1; x <= 8; x++)
                    {
                        switch (Variables.GlobalSettings.CompanySettings.EmailSettings.Messages[x].Form)
                        {
                            case EMAIL_FORM_TYPE.FORM_INVOICES:
                                listContents.Add("Invoices");
                                break;
                            case EMAIL_FORM_TYPE.FORM_PURCHASE_ORDERS:
                                listContents.Add("Purchase Orders");
                                break;
                            case EMAIL_FORM_TYPE.FORM_SALES_ORDERS:
                                listContents.Add("Sales Orders");
                                break;
                            case EMAIL_FORM_TYPE.FORM_SALES_QUOTES:
                                listContents.Add("Sales Quotes");
                                break;
                            case EMAIL_FORM_TYPE.FORM_RECEIPTS:
                                listContents.Add("Receipts");
                                break;
                            case EMAIL_FORM_TYPE.FORM_STATEMENTS:
                                listContents.Add("Statements");
                                break;
                            case EMAIL_FORM_TYPE.FORM_PURCHASE_QUOTE_CONFIRMATIONS:
                                listContents.Add("Purchase Quote Confirmations");
                                break;
                            case EMAIL_FORM_TYPE.FORM_PURCHASE_INVOICE_CONFIRMATIONS:
                                listContents.Add("Purchase Invoice Confirmations");
                                break;
                            case EMAIL_FORM_TYPE.FORM_DIRECT_DEPOSIT_PAYROLL_STUB: //EMAIL_FORM_TYPE.FORM_DIRECT_DEPOSIT_STUB:
                                listContents.Add("Direct Deposit Stub");
                                break;
                        }
                        listContents.Add(Variables.GlobalSettings.CompanySettings.EmailSettings.Messages[x].Message);
                    }
                    break;
                case ".HD5":	// Company - Dates page
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.CompanySettings.Instance.DateSettings.Instance.shortDateFormat)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.CompanySettings.Instance.DateSettings.Instance.shortDateSeparator)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.CompanySettings.Instance.DateSettings.Instance.longDateFormat)));
                    if (Variables.GlobalSettings.CompanySettings.Instance.DateSettings.Instance.onScreenUse == DATE_SHORT)
                    {
                        listContents.Add("Short Date");
                    }
                    else
                    {
                        listContents.Add("Long Date");
                    }
                    if (Variables.GlobalSettings.CompanySettings.Instance.DateSettings.Instance.inReportsUse == DATE_SHORT)
                    {
                        listContents.Add("Short Date");
                    }
                    else
                    {
                        listContents.Add("Long Date");
                    }
                    break;
                case ".HD6":	// General - Budget page
                    listContents.Add(ConvertFunctions.BoolToString(Variables.GlobalSettings.GeneralSettings.Instance.budgetRevAndExAccts));

                    switch (Variables.GlobalSettings.GeneralSettings.Instance.budgetFrequency)
                    {
                        case BUDGET_ANNUAL:
                            listContents.Add("Annual");
                            break;
                        case  BUDGET_SEMI_ANNUAL:
                            listContents.Add("Semi-annual");
                            break;
                        case  BUDGET_QUARTERLY:
                            listContents.Add("Quarterly");
                            break;
                        case  BUDGET_BIMONTHLY:
                            listContents.Add("Bi-monthly");
                            break;
                        case  BUDGET_MONTHLY:
                            listContents.Add("Monthly");
                            break;
                        case  BUDGET_PERIOD:
                            listContents.Add("13-period");
                            break;
                    }
                    break;
                case ".HD7":	// General - Numbering page
                    listContents.Add(ConvertFunctions.BoolToString(Variables.GlobalSettings.GeneralSettings.Instance.showAcctNumInTransactions));
                    listContents.Add(ConvertFunctions.BoolToString(Variables.GlobalSettings.GeneralSettings.Instance.showAcctNumInReports));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.GeneralSettings.Instance.numOfDigitsInAcctNum)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.GeneralSettings.Instance.assetStartNum)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.GeneralSettings.Instance.assetEndNum)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.GeneralSettings.Instance.liabilityStartNum)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.GeneralSettings.Instance.liabilityEndNum)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.GeneralSettings.Instance.equityStartNum)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.GeneralSettings.Instance.equityEndNum)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.GeneralSettings.Instance.revStartNum)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.GeneralSettings.Instance.revEndNum)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.GeneralSettings.Instance.expStartNum)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.GeneralSettings.Instance.expEndNum)));
                    break;
                case ".HD8":	// Payables - Options
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.PayableSettings.Instance.agingPeriod1)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.PayableSettings.Instance.agingPeriod2)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.PayableSettings.Instance.agingPeriod3)));
                    listContents.Add(ConvertFunctions.BoolToString(Variables.GlobalSettings.PayableSettings.Instance.calculateDiscountsBeforeTax));
                    break;
                case ".HD9":	// Payables - Duty
                    listContents.Add(ConvertFunctions.BoolToString(Variables.GlobalSettings.PayableSettings.Instance.trackDutyOnImportedItems));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.PayableSettings.Instance.acctNumber)));
                    break;
                case ".HD11":	// Receivables - Options and Discounts
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.ReceivableSettings.Instance.agingPeriod1)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.ReceivableSettings.Instance.agingPeriod2)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.ReceivableSettings.Instance.agingPeriod3)));
                    listContents.Add(ConvertFunctions.BoolToString(Variables.GlobalSettings.ReceivableSettings.Instance.interestCharges));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.ReceivableSettings.Instance.interestPercent)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.ReceivableSettings.Instance.interestDays)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.ReceivableSettings.Instance.statementDays)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.ReceivableSettings.Instance.discountPercent)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.ReceivableSettings.Instance.discountDays)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.ReceivableSettings.Instance.netDays)));
                    listContents.Add(ConvertFunctions.BoolToString(Variables.GlobalSettings.ReceivableSettings.Instance.calculateLineItemDiscounts));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.ReceivableSettings.Instance.code)));
                    listContents.Add(ConvertFunctions.BoolToString(Variables.GlobalSettings.ReceivableSettings.Instance.printSalesperson));
                    break;
                case ".HD12":	// Receivables - Comments
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.ReceivableSettings.Instance.salesInvoiceComment)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.ReceivableSettings.Instance.salesOrderComment)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.ReceivableSettings.Instance.salesQuoteComment)));
                    break;
                case ".HD13":	// Payroll - Income and Taxes
                    // CDN only
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.PayrollSettings.Instance.TaxSettings.Instance.eiFactor)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.PayrollSettings.Instance.TaxSettings.Instance.wcbRate)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.PayrollSettings.Instance.TaxSettings.Instance.ehtFactor)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.PayrollSettings.Instance.TaxSettings.Instance.qhsfFactor)));
                    listContents.Add(ConvertFunctions.BoolToString(Variables.GlobalSettings.PayrollSettings.Instance.IncomeSettings.Instance.trackQuebecTips));
                    for (int x = 1; x <= 23; x++)	// must do 23, so if this is geting nulls, then our read function needs to be adjusted
                    {
                        switch (Variables.GlobalSettings.PayrollSettings.Instance.IncomeSettings.Instance.IncomeType)
                        {
                            case INCOME_TYPE_INCOME:
                                listContents.Add("Income");
                                break;
                            case INCOME_TYPE_BENEFIT:
                                listContents.Add("Benefit");
                                break;
                            case INCOME_TYPE_REIMBURSEMENT:
                                listContents.Add("Reimbursement");
                                break;
                            case INCOME_TYPE_HOURLY_RATE:
                                listContents.Add("Hourlty Rate");
                                break;
                            case INCOME_TYPE_PIECE_RATE:
                                listContents.Add("Piece Rate");
                                break;
                            case INCOME_TYPE_DIFFERENTIAL_RATE:
                                listContents.Add("Differential Rate");
                                break;
                        }
                        listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.PayrollSettings.Instance.IncomeSettings.Instance.unitofMeasure)));	// if this is all NULLs, then our read needs to be adjusted
                    }                   
                    break;
                case ".HD14":	// Payroll - Deductions
                    for (int x = 1; x <= 20; x++)
                            break;
                    {
                        if (Variables.GlobalSettings.PayrollSettings.Instance.DeductBy ==  DEDUCT_AMOUNT)
                        {
                            listContents.Add("Amount");
                        }
                        else
                        {
                            listContents.Add("Percent of Gross");
                        }
                    }
                    break;
                case ".HD15":	// Payroll - Entitlements
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.PayrollSettings.Instance.EntitlementSettings.Instance.numOfHrsInTheWorkDay)));
                    for (int x = 1; x <= 5; x++)
                    {
                        listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.PayrollSettings.Instance.EntitlementSettings.Instance.entitlementName)));
                        listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.PayrollSettings.Instance.EntitlementSettings.Instance.percentOfHrsWorked)));
                        listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.PayrollSettings.Instance.EntitlementSettings.Instance.maxDays)));
                        if (Variables.GlobalSettings.PayrollSettings.Instance.EntitlementSettings.Instance.clearDaysAtYearEnd)
                        {
                            listContents.Add("Yes");
                        }
                        else
                        {
                            listContents.Add("No");
                        }
                    }
                    break;
                case ".HD16":	// Payroll - Remittance
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(Variables.GlobalSettings.PayrollSettings.Instance.RemitVendor)));
                    break;
                case ".HD17":	// Inventory & Services - Options
                    if (Variables.GlobalSettings.InventorySettings.Instance.profitMethod == MARKUP)                            
                    {
                        listContents.Add("Markup");
                    }
                    else
                    {
                        listContents.Add("Margin");
                    }
                    if (Variables.GlobalSettings.InventorySettings.Instance.sortMethod == SORT_NUMBER)
                    {
                        listContents.Add("Number");
                    }
                    else
                    {
                       listContents.Add("Description");
                    }
                   if (Variables.GlobalSettings.InventorySettings.Instance.priceMethod == CALCULATED)
                   {
                         listContents.Add("Calculated");
                    }
                    else
                    {
                        listContents.Add("Taken");
                    }
                    listContents.Add(ConvertFunctions.BoolToString(Variables.GlobalSettings.InventorySettings.Instance.allowInventoryLevelsToGoBelowZero));
                    if (Variables.GlobalSettings.InventorySettings.Instance.inventoryCostingMethod == AVERAGE_COST)
                    {
                        listContents.Add("Average cost");
                    }
                    else
                    {
                        listContents.Add("First In)");
                    }
                    listContents.Add(ConvertFunctions.BoolToString(Variables.GlobalSettings.InventorySettings.Instance.allowSerialNums));
                    break;
                case ".HD18":	// Project - Budget
                    listContents.Add(ConvertFunctions.BoolToString(Variables.GlobalSettings.ProjectSettings.budgetProjects));

                    switch (Variables.GlobalSettings.ProjectSettings.budgetPeriodFrequency)
                    {
                        case BUDGET_ANNUAL:
                            listContents.Add("Annual");
                            break;
                        case BUDGET_SEMI_ANNUAL:
                            listContents.Add("Semi-annual");
                            break;
                        case BUDGET_QUARTERLY:
                            listContents.Add("Quarterly");
                            break;
                        case BUDGET_BIMONTHLY:
                            listContents.Add("Bi-monthly");
                            break;
                        case  BUDGET_MONTHLY:
                            listContents.Add("Monthly");
                            break;
                        case BUDGET_PERIOD:
                            listContents.Add("13-period");
                            break;
                    }
                    break;
                case ".HD19":	// Project - Allocation
                            break;
                    switch (Variables.GlobalSettings.ProjectSettings.payrollAllocationMethod)
                    {
                        case ALLOCATE_AMOUNT:
                            listContents.Add("Amount");
                            break;
                        case ALLOCATE_PERCENT:
                            listContents.Add("Percent");
                            break;
                        case ALLOCATE_HOURS:
                            listContents.Add("Hours");
                            break;
                    }
                    switch (Variables.GlobalSettings.ProjectSettings.otherAllocationMethod)
                    {
                        case ALLOCATE_TRANS_AMOUNT:
                            listContents.Add("Amount");
                            break;
                        case ALLOCATE_TRANS_PERCENT:
                            listContents.Add("Percent");
                            break;
                    }
                    listContents.Add(ConvertFunctions.BoolToString(Variables.GlobalSettings.ProjectSettings.warnIfAllocationIsNotComplete));
                    listContents.Add(ConvertFunctions.BoolToString(Variables.GlobalSettings.ProjectSettings.allowAccessToAllocateFieldUsingTab));
                    break;
                default:
                    {
                        FunctionsLib.VerifyFunction (false, true, "Correct extension sent");
                        break;
                    }
                return listContents;
            }
    
        }        
        */

    }





    public class TaxableTaxList
	{
		public static SettingsResFolders.TaxableTaxListAppFolder repo = SettingsRes.Instance.TaxableTaxList;	
	}
	
	public class TaxCodeDetails
	{
		public static SettingsResFolders.TaxCodeDetailsAppFolder repo = SettingsRes.Instance.TaxCodeDetails;
	}
	
	public class ExchangeRate
	{
		public static SettingsResFolders.ExchangeRateAppFolder repo = SettingsRes.Instance.ExchangeRate;
	}
	
	public class CategoryInformation
	{
		public static SettingsResFolders.CategoryInformationAppFolder repo = SettingsRes.Instance.CategoryInformation;
	}
	
	public class JobCategoryInformation
	{		
		public static SettingsResFolders.JobCategoryAppFolder repo = SettingsRes.Instance.JobCategory;
		
	}
	
	public class Sage50Process
	{
		public static SettingsResFolders.Sage50AccountingAppFolder repo = SettingsRes.Instance.Sage50Accounting;
	}
	
}