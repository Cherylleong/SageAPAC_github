/*
 * Created by Ranorex
 * User: wonga01
 * Date: 5/19/2017
 * Time: 15:02
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Threading;
using System.Collections.Generic;
using Sage50.Repository;
using Sage50.Shared;
using Sage50.Types;
using System.IO;
using Ranorex;

namespace Sage50.Classes
{
	/// <summary>
	/// Description of NewCompany.
	/// </summary>
	public class NewCompanyWizard
	{
		public static NewCompanyWizardResFolders.NewCompanyWizardAppFolder repo = NewCompanyWizardRes.Instance.NewCompanyWizard;
		
			
		public static void _SA_Create(COMPANY Company)
		{
			Simply.repo.Self.Activate();
			Simply.repo.File.Click();
			Simply.repo.NewCompany.Click();
			
			SimplyMessage._SA_HandleMessage(SimplyMessage.repo.Yes, SimplyMessage.repo._Msg_AreYouSureYouAreFinishedWithThisCompany);
			
			NewCompanyWizard.repo.Next.Click ();
						
			if (NewCompanyWizard.repo.QuantumInfo.Exists())
			{	
                switch ((int)Company.edition)
                {
                    case 1:
                        NewCompanyWizard.repo.First.Click();
                        break;
                    case 2:
                        NewCompanyWizard.repo.Pro.Click();
                        break;
                    case 3:
                        NewCompanyWizard.repo.Premium.Click();
                        break;
                    case 4:
                        NewCompanyWizard.repo.Quantum.Click();
                        break;
                    default:
                        NewCompanyWizard.repo.Premium.Click();
                        break;
                }				
				
				NewCompanyWizard.repo.Next.Click ();
			}
						
			NewCompanyWizard.repo.Name.TextValue = Company.companyInformation.companyName;
			if (Functions.GoodData (Company.companyInformation.Address) && Functions.GoodData (Company.companyInformation.Address.street1))
			{
				NewCompanyWizard.repo.Street1.TextValue = Company.companyInformation.Address.street1;
			}
			if (Functions.GoodData (Company.companyInformation.Address) && Functions.GoodData (Company.companyInformation.Address.street2))
			{
				NewCompanyWizard.repo.Street2.TextValue = Company.companyInformation.Address.street2;
			}
			if (Functions.GoodData (Company.companyInformation.Address) && Functions.GoodData (Company.companyInformation.Address.city))
			{
				NewCompanyWizard.repo.City.TextValue = Company.companyInformation.Address.city;
			}
//			if  (Variables.productVersion ==  "Canadian")
//			{
				if (Functions.GoodData (Company.companyInformation.Address) && Functions.GoodData (Company.companyInformation.Address.provinceCode))
				{
					NewCompanyWizard.repo.Province.Select(Company.companyInformation.Address.provinceCode);
				}
				if (Functions.GoodData (Company.companyInformation.Address) && Functions.GoodData (Company.companyInformation.Address.province))
				{
					NewCompanyWizard.repo.ProvinceName.TextValue = Company.companyInformation.Address.province;
				}
//			}
//			else
//			{
//				if (Functions.GoodData (Company.companyInformation.Address) && Functions.GoodData (Company.companyInformation.Address.State))
//				{
//					NewCompanyWizard.Instance.State.SetText (Company.companyInformation.Address.State);
//				}
//			}
			if (Functions.GoodData (Company.companyInformation.Address) && Functions.GoodData (Company.companyInformation.Address.country))
			{
				NewCompanyWizard.repo.Country.TextValue = Company.companyInformation.Address.country;
			}
			if (Functions.GoodData (Company.companyInformation.Address) && Functions.GoodData (Company.companyInformation.Address.postalCode))
			{
				NewCompanyWizard.repo.Postal.TextValue = Company.companyInformation.Address.postalCode;
			}
			if (Functions.GoodData (Company.companyInformation.Address) && Functions.GoodData (Company.companyInformation.Address.phone1))
			{
				NewCompanyWizard.repo.Phone1.TextValue = Company.companyInformation.Address.phone1;
			}
			if (Functions.GoodData (Company.companyInformation.Address) && Functions.GoodData (Company.companyInformation.Address.phone2))
			{
				NewCompanyWizard.repo.Phone2.TextValue = Company.companyInformation.Address.phone2;
			}
			if (Functions.GoodData (Company.companyInformation.Address) && Functions.GoodData (Company.companyInformation.Address.fax))
			{
				NewCompanyWizard.repo.Fax.TextValue = Company.companyInformation.Address.fax;
			}
			NewCompanyWizard.repo.Next.Click();
			
			if (!(Functions.GoodData (Company.companyInformation.fiscalStart)))
			{
				Company.companyInformation.fiscalStart = "1/1/" + Variables.sLongYear + "";
			}
			NewCompanyWizard.repo.FiscalStart.TextValue = Company.companyInformation.fiscalStart;
			if (Functions.GoodData (Company.companyInformation.earliestTransaction))
			{
				NewCompanyWizard.repo.EarliestTransaction.TextValue = Company.companyInformation.earliestTransaction;
			}
			else
			{
				Company.companyInformation.earliestTransaction = NewCompanyWizard.repo.EarliestTransaction.TextValue;
			}
			if  (Functions.GoodData (Company.companyInformation.fiscalEnd))
			{
				NewCompanyWizard.repo.FiscalEnd.TextValue = Company.companyInformation.fiscalEnd;
			}
			else
			{
				Company.companyInformation.fiscalEnd = NewCompanyWizard.repo.FiscalEnd.TextValue;
			}
			NewCompanyWizard.repo.Next.Click();
			
			// Select default the list of accounts
			NewCompanyWizard.repo.Next.Click();
			
			if (Company.ownership != 0)
			{
				NewCompanyWizard.repo.Ownership.Select(Company.ownership.ToString());
			}
			else
			{
				Company.ownership = (OWNERSHIP)NewCompanyWizard.repo.Ownership.SelectedItemIndex;
			}
			if (Company.industryType != 0)
			{
				NewCompanyWizard.repo.IndustryType.Select(Company.industryType.ToString());
			}
			else
			{
				Company.industryType = (INDUSTRY_TYPE)NewCompanyWizard.repo.IndustryType.SelectedItemIndex;
			}
			if (Functions.GoodData(Company.companyType))
			{
				NewCompanyWizard.repo.CompanyType.SelectListItem(Company.companyType);
			}
			else
			{
				Company.companyType = NewCompanyWizard.repo.CompanyType.SelectedItemText;
			}
						
			// Account ranges
            if (Company.AccountDetails.accountNumberDigits !=0)  // temp using till good data is properly updated
			{
				NewCompanyWizard.repo.AccountRanges.Click();
				
				if (Functions.GoodData(Company.AccountDetails.accountNumberDigits))
				{
					AccountInformation.repo.Digits.Select(Company.AccountDetails.accountNumberDigits.ToString());
										
				}
				else
				{
					Company.AccountDetails.accountNumberDigits = 4;
				}
				
				AccountInformation.repo.AccountContainer.MoveRight();
				if (Functions.GoodData(Company.AccountDetails.startingAssetAccountNumber))
				{
					AccountInformation.repo.AccountContainer.SetText(Company.AccountDetails.startingAssetAccountNumber);
				}
				AccountInformation.repo.AccountContainer.MoveRight();
				if (Functions.GoodData(Company.AccountDetails.endingAssetAccountNumber))
				{
					AccountInformation.repo.AccountContainer.SetText(Company.AccountDetails.endingAssetAccountNumber);
				}
				AccountInformation.repo.AccountContainer.MoveRight();
				if (Functions.GoodData(Company.AccountDetails.startingLiabilityAccountNumber))
				{
					AccountInformation.repo.AccountContainer.SetText(Company.AccountDetails.startingLiabilityAccountNumber);
				}
				AccountInformation.repo.AccountContainer.MoveRight();
				if (Functions.GoodData(Company.AccountDetails.endingLiabilityAccountNumber))
				{
					AccountInformation.repo.AccountContainer.SetText(Company.AccountDetails.endingLiabilityAccountNumber);
				}
				AccountInformation.repo.AccountContainer.MoveRight();
				if (Functions.GoodData(Company.AccountDetails.startingEquityAccountNumber))
				{
					AccountInformation.repo.AccountContainer.SetText(Company.AccountDetails.startingEquityAccountNumber);
				}
				AccountInformation.repo.AccountContainer.MoveRight();
				if (Functions.GoodData(Company.AccountDetails.endingEquityAccountNumber))
				{
					AccountInformation.repo.AccountContainer.SetText(Company.AccountDetails.endingEquityAccountNumber);
				}
				AccountInformation.repo.AccountContainer.MoveRight();
				if (Functions.GoodData(Company.AccountDetails.startingRevenueAccountNumber))
				{
					AccountInformation.repo.AccountContainer.SetText (Company.AccountDetails.startingRevenueAccountNumber);
				}
				AccountInformation.repo.AccountContainer.MoveRight();
				if (Functions.GoodData(Company.AccountDetails.endingRevenueAccountNumber))
				{
					AccountInformation.repo.AccountContainer.SetText (Company.AccountDetails.endingRevenueAccountNumber);
				}
				AccountInformation.repo.AccountContainer.MoveRight();
				if (Functions.GoodData(Company.AccountDetails.startingExpenseAccountNumber))
				{
					AccountInformation.repo.AccountContainer.SetText (Company.AccountDetails.startingExpenseAccountNumber);
				}
				AccountInformation.repo.AccountContainer.MoveRight();
				if (Functions.GoodData(Company.AccountDetails.endingExpenseAccountNumber))
				{
					AccountInformation.repo.AccountContainer.SetText (Company.AccountDetails.endingExpenseAccountNumber);
				}
				AccountInformation.repo.AccountContainer.MoveRight();
				AccountInformation.repo.OK.Click();
			}
			else
			{
				Company.AccountDetails.accountNumberDigits = 4;
			}
			NewCompanyWizard.repo.Next.Click ();
			
			if (Functions.GoodData(Company.companyNameFile))
			{
				NewCompanyWizard.repo.CompanyName.TextValue = Company.companyNameFile;
			}
			else
			{
				Company.companyNameFile = NewCompanyWizard.repo.CompanyName.TextValue;
			}
			if (Functions.GoodData (Company.companyFileLocation))
			{
				NewCompanyWizard.repo.Location.TextValue = Company.companyFileLocation;
			}
			else
			{
				Company.companyFileLocation = NewCompanyWizard.repo.Location.TextValue;
			}
			NewCompanyWizard.repo.Next.Click ();
			
			//SimplyMessage._SA_HandleMessage(SimplyMessage.Yes, SimplyMessage._Msg_TheFolderAndFileDoNotExist)
//			//SimplyMessage._SA_HandleMessage (SimplyMessage.Yes, SimplyMessage._Msg_ReplaceExistingFile)
			if (SimplyMessage.repo.YesInfo.Exists())
			{
				SimplyMessage.repo.Yes.Click();
			}
			
			NewCompanyWizard.repo.Finish.Click();
			
			while (!NewCompanyWizard.repo.CloseInfo.Exists())
			{
				Thread.Sleep(1000);
			}

			NewCompanyWizard.repo.Close.Click();
			  
			// Getting started always exists when a new company is created
			while (!GettingStarted.repo.SelfInfo.Exists())
			{
				Thread.Sleep(500);
			}
			GettingStarted.repo.Show.Uncheck();
			GettingStarted.repo.Close.Click();			
			
			
			Simply.isEnhancedView();
//            Simply.Instance.SwitchViewLink.Click();
			// FunctionsLib.WUEn (Simply.Instance.SwitchToEnhancedViewLink);DW
			
			if ((!Functions.GoodData(Variables.bAcctEd)) || (Variables.bAcctEd))
			{
				Simply._SA_SetFlavorVariables();
			}
			
 			Settings._SA_SetToGenericValues();	// need to do this after getting a new, clean company
			Variables.bHistoryMode = true;			
		}
				
	}	
	
	public static class AccountInformation
	{
		public static NewCompanyWizardResFolders.AccountInformationAppFolder repo = NewCompanyWizardRes.Instance.AccountInformation;
	}
	
	
}
