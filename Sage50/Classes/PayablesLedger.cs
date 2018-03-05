/*
 * Created by Ranorex
 * User: wonda05
 * Date: 5/20/2016
 * Time: 2:08 PM
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
	/// Description of MyClass.
	/// </summary>
	public static class PayablesLedger
	{
		// TODO: Add Code to call testing functions - for example:	
		// Automation.Start()
		public static PayablesLedgerResFolders.PayablesLedgerAppFolder repo = PayablesLedgerRes.Instance.PayablesLedger;
		
		

		public static void _SA_Invoke(Boolean bOpenLedger)
		{
			// open ledger depending on view type
			
			if(Simply.isEnhancedView())
			{	
				Simply.repo.Self.Activate();
				Simply.repo.PayablesLink.Click();
				Simply.repo.VendorIcon.Click();
			}
			else
			{
				
			}
			
			if(VendorIcon.repo.SelfInfo.Exists())
			{
				if(bOpenLedger == true)
				{
					VendorIcon.repo.CreateNew.Click();
					VendorIcon.repo.Self.Close();
				}
			}			
		}
		
		public static void _SA_Invoke()
		{
			PayablesLedger._SA_Invoke(true);
			

		}	
		public static void _SA_Open(VENDOR vend)
		{
			if(!PayablesLedger.repo.SelfInfo.Exists())
			{
				PayablesLedger._SA_Invoke();
			}
			PayablesLedger.repo.SelectRecord.Select(vend.name);
		}	
		public static void _SA_Create(VENDOR vend)
		{
			
			PayablesLedger._SA_Create(vend, true, false);
		}	
		public static void _SA_Create(VENDOR vend, bool bSave, bool bEdit )
		{
			if(!Variables.bUseDataFiles && !bEdit)
			{
				PayablesLedger._SA_MatchDefaults(vend);
			}
			
			
			if(!PayablesLedger.repo.SelfInfo.Exists())
			{
				PayablesLedger._SA_Invoke();
			}
			
			
			if(bEdit)
			{
				if(PayablesLedger.repo.SelectRecord.SelectedItemText != vend.name)
				{
					PayablesLedger._SA_Open(vend);	
				}
	
				if(Functions.GoodData(vend.nameEdit))
				{
					PayablesLedger.repo.VendorName.TextValue = vend.nameEdit;
					vend.name = vend.nameEdit;
				}
					
				// print to log or results
				Ranorex.Report.Info(String.Format("Modifying vendor {0}",vend.name));
				
			}
			else
			{
				PayablesLedger.repo.CreateANewToolButton.Click();
				PayablesLedger.repo.VendorName.TextValue = vend.name;
				Ranorex.Report.Info(String.Format("Creating vendor {0}",vend.name));
			}
		
			
			#region Address Tab

			// Select tab 
			PayablesLedger.repo.Address.Tab.Click();
			
			if(Functions.GoodData(vend.Address.contact))
			{
				PayablesLedger.repo.Address.Contact.TextValue = vend.Address.contact;
			}
			
			if(Functions.GoodData(vend.Address.street1))
			{
				PayablesLedger.repo.Address.Street1.TextValue = vend.Address.street1;
			}
			
			if(Functions.GoodData(vend.Address.street2))
			{
				PayablesLedger.repo.Address.Street2.TextValue = vend.Address.street2;
			}
			
			if(Functions.GoodData(vend.Address.city))
			{
				PayablesLedger.repo.Address.City.TextValue = vend.Address.city;
			}
			
			if(Functions.GoodData(vend.Address.province))
			{
				PayablesLedger.repo.Address.Province.TextValue = vend.Address.province;
			}
								
			if(Functions.GoodData(vend.Address.postalCode))
			{
				PayablesLedger.repo.Address.PostalCode.TextValue = vend.Address.postalCode;
			}
			
			if(Functions.GoodData(vend.Address.country))
			{
				PayablesLedger.repo.Address.Country.TextValue = vend.Address.country;
			}
			
			if(Functions.GoodData(vend.Address.phone1))
			{
				PayablesLedger.repo.Address.Phone1.TextValue = vend.Address.phone1;
			}
			
			if(Functions.GoodData(vend.Address.phone2))
			{
				PayablesLedger.repo.Address.Phone2.TextValue = vend.Address.phone2;
			}
			
			if(Functions.GoodData(vend.Address.fax))
			{
				PayablesLedger.repo.Address.Fax.TextValue = vend.Address.fax;
			}
			
			if(Functions.GoodData(vend.taxID))
			{
				PayablesLedger.repo.Address.TaxID.TextValue = vend.taxID;
			}	
			
			if(Functions.GoodData(vend.Address.email))
			{
				PayablesLedger.repo.Address.Email.TextValue = vend.Address.email;
			}
								
			if(Functions.GoodData(vend.Address.webSite))
			{
				PayablesLedger.repo.Address.WebSite.TextValue = vend.Address.webSite;
			}
			
			if(PayablesLedger.repo.Address.DepartmentInfo.Exists())
			{
				if(Functions.GoodData(vend.department))
				{
					PayablesLedger.repo.Address.Department.Select(vend.department);
				}
			}
			
			if(Functions.GoodData(vend.vendorSince))
			{
				PayablesLedger.repo.Address.VendorSince.TextValue = vend.vendorSince;
			}
			
			if(Functions.GoodData(vend.inactiveCheckBox))
			{
				PayablesLedger.repo.InactiveVendor.SetState(vend.inactiveCheckBox);
			}
						
			
			#endregion
			
			
			#region Options Tab
			PayablesLedger.repo.Options.Tab.Click();
			
			if(Functions.GoodData(vend.expenseAccount.acctNumber))
			{
				PayablesLedger.repo.Options.ExpenseAccount.Select(vend.expenseAccount.acctNumber);
			}
			
			if(Functions.GoodData(vend.usuallyStoreItemIn) && vend.usuallyStoreItemIn !="")
			{
				if(PayablesLedger.repo.Options.UsuallyStoreItemInInfo.Exists())
				{
					PayablesLedger.repo.Options.UsuallyStoreItemIn.Select(vend.usuallyStoreItemIn);
				}
			}
			
			if(Functions.GoodData(vend.currencyCode))
			{
				if(PayablesLedger.repo.Options.CurrencyInfo.Exists() && PayablesLedger.repo.Options.Currency.Enabled)
				{
					if(Variables.bUseDataFiles) // check if item is in lists
					{
						PayablesLedger.repo.Options.Currency.Items[2].Select();
					}
					else
					{
						PayablesLedger.repo.Options.Currency.Select(vend.currencyCode);
					}
				}
			}
			
			if(Functions.GoodData(vend.conductBusinessIn))
			{
				PayablesLedger.repo.Options.ConductBusinessIn.Select(vend.conductBusinessIn);
			}
			
			if(Functions.GoodData(vend.discountPercent))
			{
				PayablesLedger.repo.Options.DiscountPercent.TextValue = vend.discountPercent;	
			}
			
			if(Functions.GoodData(vend.discountPeriod))
			{
				PayablesLedger.repo.Options.DiscountPeriod.TextValue = vend.discountPeriod;	
			}
			
			if(Functions.GoodData(vend.termPeriod))
			{
				PayablesLedger.repo.Options.TermPeriod.TextValue = vend.termPeriod;	
			}
			
			if(Functions.GoodData(vend.calDisBeforeTaxCheckBox))
			{
			   	
				PayablesLedger.repo.Options.CalculateDiscountBeforeTax.SetState(vend.calDisBeforeTaxCheckBox);
			}
			
			if(Functions.GoodData(vend.printContactCheckBox))
			{
				PayablesLedger.repo.Options.PrintContactOnCheques.SetState(vend.printContactCheckBox);	
			}
			
			if(Functions.GoodData(vend.ordersForThisVendor))
			{
				PayablesLedger.repo.Options.OrdersForVendor.Select(vend.ordersForThisVendor);	
			}
			
			if(Functions.GoodData(vend.emailConfirmCheckBox))
			{
				PayablesLedger.repo.Options.EmailConfirmationOfPurchases.SetState(vend.emailConfirmCheckBox);
			}
			
			if(Functions.GoodData(vend.synchronizeWithOutlook) && PayablesLedger.repo.Options.SynchronizeWithMicrosoftOutInfo.Exists())
			{
				PayablesLedger.repo.Options.SynchronizeWithMicrosoftOut.SetState(vend.synchronizeWithOutlook);
			}
			
			if(Functions.GoodData(vend.chargeDutyCheckBox) && PayablesLedger.repo.Options.ChargeDutyOnItemsPurchasedInfo.Exists())
			{
				PayablesLedger.repo.Options.ChargeDutyOnItemsPurchased.SetState(vend.chargeDutyCheckBox);
			}
			
			#endregion
			
			#region Taxes Tab
			PayablesLedger.repo.Taxes.Tab.Click();
			
			if(Functions.GoodData(vend.taxList))
			{
				PayablesLedger.repo.Self.Activate();
			
				bool bFound;
				
				List<List <string>> lsContents = PayablesLedger.repo.Taxes.TaxContainer.GetContents();
				List<TAX_LEDGER> lsTempTaxes = new List<TAX_LEDGER>();
				
				// Loop through entire container
				
				foreach(List<string> currentRow in lsContents)
				{
					bFound = false;
					
					foreach(TAX_LEDGER currentTax in vend.taxList)
					{
						if(currentTax.tax.taxName == currentRow[0])
						{
							bFound = true;
							lsTempTaxes.Add(currentTax);
							break;
						}
					}
					
					if(!bFound)
					{
						TAX_LEDGER taxLine = new TAX_LEDGER();
						taxLine.tax.taxName = currentRow[0];
						lsTempTaxes.Add(taxLine);
					}
				}
				
				vend.taxList = lsTempTaxes;
				
				// enter into container here
				for (int x = 0; x < vend.taxList.Count; x++)
                {
                    // go to correct line
                    PayablesLedger.repo.Taxes.TaxContainer.SetToLine(x);
                    PayablesLedger.repo.Taxes.TaxContainer.MoveRight();
                    // only set tax exempt if not null
                    if (Functions.GoodData(vend.taxList[x].taxExempt))
                    {
                        if (lsContents[x][1] != vend.taxList[x].taxExempt)
                        {
                             PayablesLedger.repo.Taxes.TaxContainer.Toggle();
                        }
                    }
                }
			}
			
			if(Functions.GoodData(vend.taxCode) && (vend.taxCode.code != Variables.sNoTax))
			{
				// Loop through each item and check if the tax code matches
                for (int i = 0; i < PayablesLedger.repo.Taxes.TaxCode.Items.Count; i++)
                {

                	PayablesLedger.repo.Taxes.TaxCode.Items[i].Select();

                    // Exit for if tax code is matched
                    if (Functions.GetField(PayablesLedger.repo.Taxes.TaxCode.Text, "-", 1).Trim() == vend.taxCode.code)
                    {
                        break;
                    }
                }	
			}
			else
			{
				PayablesLedger.repo.Taxes.TaxCode.Select(" - No Tax");
			}
			
					
			#endregion
			                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                
			
			#region Direct Deposit Tab
			PayablesLedger.repo.DirectDeposit.Tab.Click();
			
			if(Functions.GoodData(vend.allowDirectDepCheckBox))
			{
				PayablesLedger.repo.DirectDeposit.AllowDirectDeposits.SetState(vend.allowDirectDepCheckBox);
			}
			
			if(Functions.GoodData(vend.currencyAndLocation))
			{
				PayablesLedger.repo.DirectDeposit.CurrencyAndLocation.Select(vend.currencyAndLocation);	
			}
			
			if(Functions.GoodData(vend.branchNumber))
			{
				PayablesLedger.repo.DirectDeposit.BranchNumber.TextValue = vend.branchNumber;
			}
			
			if(Functions.GoodData(vend.institutionNumber))
			{
				PayablesLedger.repo.DirectDeposit.InstitutionNumber.TextValue = vend.institutionNumber;
			}
						
			if(Functions.GoodData(vend.accountNumber))
			{
				PayablesLedger.repo.DirectDeposit.AccountNumber.TextValue = vend.accountNumber;
			}

			if(Functions.GoodData(vend.eftReferenceCheckbox))
			{
				PayablesLedger.repo.DirectDeposit.EFTReferenceCheckBox.SetState(vend.eftReferenceCheckbox);
			}
			
			if(Functions.GoodData(vend.eftReference))
			{
				PayablesLedger.repo.DirectDeposit.EFTReference.TextValue = vend.eftReference;
			}

			if(Functions.GoodData(vend.eftReferenceCheckbox))
			{
				PayablesLedger.repo.DirectDeposit.TransCodeCheckBox.SetState(vend.eftTransCodeCheckbox);
			}

			if(Functions.GoodData(vend.eftTransCode))
			{
				PayablesLedger.repo.DirectDeposit.TransCode.TextValue = vend.eftTransCode;
			}
		
			// US banks
			if(Functions.GoodData(vend.routingNumber) && PayablesLedger.repo.DirectDeposit.RoutingNumberInfo.Exists())
			{
				PayablesLedger.repo.DirectDeposit.RoutingNumber.TextValue = vend.routingNumber;
			}
			
			if(Functions.GoodData(vend.accountType) && PayablesLedger.repo.DirectDeposit.AccountTypeInfo.Exists())
			{
				PayablesLedger.repo.DirectDeposit.AccountType.Select(vend.accountType);
			}
			
			#endregion
			
			#region Statistics
			PayablesLedger.repo.Statistics.Tab.Click();
			
			if(Functions.GoodData(vend.ytdPurchases))
			{
			   	if(PayablesLedger.repo.Statistics.YTDPurchasesInfo.Exists())
			   	{
			   		PayablesLedger.repo.Statistics.YTDPurchases.TextValue = vend.ytdPurchases;
			   	}
			}
			   
			if(Functions.GoodData(vend.lyPurchases))
			{
			   	if(PayablesLedger.repo.Statistics.LYPurchasesInfo.Exists())
			   	{
			   		PayablesLedger.repo.Statistics.LYPurchases.TextValue = vend.lyPurchases;
			   	}
			}
			
			if(Functions.GoodData(vend.ytdPayments))
			{
			   	PayablesLedger.repo.Statistics.YTDPayments.TextValue = vend.ytdPayments;
			}
			
			if(Functions.GoodData(vend.foreignYtdPurchases))
			{
			   	if(PayablesLedger.repo.Statistics.ForYTDPurchasesInfo.Exists())
			   	{
			   		PayablesLedger.repo.Statistics.ForYTDPurchases.TextValue = vend.foreignYtdPurchases;
			   	}
			}
			
			if(Functions.GoodData(vend.foreignLyPurchases))
			{
			   	if(PayablesLedger.repo.Statistics.ForYTDPurchasesInfo.Exists())
			   	{
			   		PayablesLedger.repo.Statistics.ForYTDPurchases.TextValue = vend.foreignYtdPurchases;
			   	}
			}
						
			if(Functions.GoodData(vend.foreignYtdPayments))
			{
			   	if(PayablesLedger.repo.Statistics.ForYTDPaymentsInfo.Exists())
			   	{
			   		PayablesLedger.repo.Statistics.ForYTDPayments.TextValue = vend.foreignYtdPayments;
			   	}
			}
			
			if(Functions.GoodData(vend.foreignPreviousYtdPayments))
			{
			   	if(PayablesLedger.repo.Statistics.ForPreviousYTDPaymentsInfo.Exists())
			   	{
			   		PayablesLedger.repo.Statistics.ForPreviousYTDPayments.TextValue = vend.foreignPreviousYtdPayments;
			   	}
			}	
			
			#endregion
			
			
			#region Memo Tab
			
			PayablesLedger.repo.Memo.Tab.Click();
			
			if(Functions.GoodData(vend.memo))
			{
				PayablesLedger.repo.Memo.Memo.TextValue = vend.memo;
			}
			
			if(Functions.GoodData(vend.toDoDate))
			{
				PayablesLedger.repo.Memo.ToDoDate.TextValue = vend.toDoDate;
			}
			
			if(Functions.GoodData(vend.displayCheckBox))
			{
				PayablesLedger.repo.Memo.DisplayThisMemoInTheDaily.SetState(vend.displayCheckBox);
			}
			#endregion
			
			
			#region Import Tab		
			PayablesLedger.repo.ImportExport.Tab.Click();
			
			if(PayablesLedger.repo.ImportExport.ThisVendorHasSage50Info.Exists())
			{
				if(Functions.GoodData(vend.hasSage50CheckBox))
				{
					PayablesLedger.repo.ImportExport.ThisVendorHasSage50.SetState(vend.hasSage50CheckBox);
				}
				
				if(Functions.GoodData(vend.usesMyItemNumCheckBox))
				{
					PayablesLedger.repo.ImportExport.ThisVendorUsesMyItemNumbers.SetState(vend.usesMyItemNumCheckBox);
				}
				
				if(vend.imports.Count !=0)
				{
					if(Functions.GoodData(vend.usesMyItemNumCheckBox) && vend.usesMyItemNumCheckBox == false)
					{
						// enter import container here
						
                        int nLine = 1;
                        int i = 1;
						
                        // Restore containter to default
                        PayablesLedger.repo.Self.Activate();
                        //PayablesLedger.repo.RestoreWindow.Select();
						
                        // Set the first cell X position for SilkTest to click (try not to click over text in the cell, so as far right in the cell as possible)
                        PayablesLedger.repo.ImportExport.ImportContainer.ClickFirstCell();
						
                        while (true)
                        {
                            PayablesLedger.repo.ImportExport.ImportContainer.SetToLine(nLine);

                            i = nLine - 1;

                            // Do vendomer's item number
                            if (Functions.GoodData (vend.imports[i].itemNumber))
                            {
                                PayablesLedger.repo.ImportExport.ImportContainer.SetText (vend.imports[i].itemNumber) ;
                            }
							
                            // Can only enter either item number or account number
                            if (Functions.GoodData (vend.imports[i].myItemNumber) && (vend.imports[i].myItemNumber != ""))
                            {
								
                                // Do my item number field
                                PayablesLedger.repo.ImportExport.ImportContainer.MoveRight();
                                PayablesLedger.repo.ImportExport.ImportContainer.SetText (vend.imports[i].myItemNumber);
                            }
                            else
                            {
                                // Do my account number
								
                                // Move to the my account number field
                                PayablesLedger.repo.ImportExport.ImportContainer.MoveRight();
								
                                // If adding then we need to tab over again
                                if (!bEdit)
                                {
                                    PayablesLedger.repo.ImportExport.ImportContainer.MoveRight();
                                }
								
                                if (Functions.GoodData (vend.imports[i].myAccount) && Functions.GoodData(vend.imports[i].myAccount.acctNumber))
                                {
                                    PayablesLedger.repo.ImportExport.ImportContainer.SetText (vend.imports[i].myAccount.acctNumber);
                                }
                            }
												
                            nLine++;
                            if (nLine > vend.imports.Count)
                            {
                                break;
                            }
                        }			
					}
				}
			}
			
			#endregion
						
			
			#region Additional Info
			
			PayablesLedger.repo.AdditionalInfo.Tab.Click();
						
			if(Functions.GoodData(vend.additional1))
			{
				PayablesLedger.repo.AdditionalInfo.Additional1.TextValue = vend.additional1;
			}
			
			if(Functions.GoodData(vend.additional2))
			{
				PayablesLedger.repo.AdditionalInfo.Additional2.TextValue = vend.additional2;
			}
			
			if(Functions.GoodData(vend.additional3))
			{
				PayablesLedger.repo.AdditionalInfo.Additional3.TextValue = vend.additional3;
			}
						
			if(Functions.GoodData(vend.additional4))
			{
				PayablesLedger.repo.AdditionalInfo.Additional4.TextValue = vend.additional4;
			}
			
			if(Functions.GoodData(vend.additional5))
			{
				PayablesLedger.repo.AdditionalInfo.Additional5.TextValue = vend.additional5;
			}
			
			if(Functions.GoodData(vend.addCheckBox1))
			{
				PayablesLedger.repo.AdditionalInfo.AddCheckBox1.SetState(vend.addCheckBox1);
			}
			
			if(Functions.GoodData(vend.addCheckBox2))
			{
				PayablesLedger.repo.AdditionalInfo.AddCheckBox2.SetState(vend.addCheckBox2);
			}
						
			if(Functions.GoodData(vend.addCheckBox3))
			{
				PayablesLedger.repo.AdditionalInfo.AddCheckBox3.SetState(vend.addCheckBox3);
			}
						
			if(Functions.GoodData(vend.addCheckBox4))
			{
				PayablesLedger.repo.AdditionalInfo.AddCheckBox4.SetState(vend.addCheckBox4);
			}
									
			if(Functions.GoodData(vend.addCheckBox5))
			{
				PayablesLedger.repo.AdditionalInfo.AddCheckBox5.SetState(vend.addCheckBox5);
			}
			
			#endregion
			
			#region T4A& T5018
			
			PayablesLedger.repo.T4A.Tab.Click();
			
			if(Functions.GoodData(vend.includeFilingT5018CheckBox))
			{
				if(PayablesLedger.repo.T4A.IncludeThisVendorWhenFiling.Enabled)
				{
					// Need to click on checkbox to activate options
					if (vend.includeFilingT5018CheckBox == true)
					{
						PayablesLedger.repo.T4A.IncludeThisVendorWhenFiling.Click();
						PayablesLedger.repo.T4A.T5018.Click();
					}					
				}
				
			}
			
			
			#endregion
			
			if(bSave)
			{
				PayablesLedger.repo.Save.Click();
				
				if(Variables.bUseDataFiles)
				{
					//SimplyMessage._SA_HandleMessage(SimplyMessage.No, SimplyMessage._Msg_YouHaveEnterdDirectDeposit);	
				}
				

			}
			
		}
		
		public static void _SA_MatchDefaults(VENDOR vend)
		{
            if (!Functions.GoodData(vend.inactiveCheckBox))
            {
                vend.inactiveCheckBox = false;
            }
            
            if (!Functions.GoodData(vend.usuallyStoreItemIn) && (Variables.globalSettings.InventorySettings.UseMultipleLocations == true))
            {
                vend.usuallyStoreItemIn = Variables.globalSettings.InventorySettings.Locations[0].code;
            }
            
            if (!Functions.GoodData(vend.conductBusinessIn) && (Variables.globalSettings.CompanySettings.FeatureSettings.Language == true))
            {
                vend.conductBusinessIn = "English";
            }
            
            if (!Functions.GoodData(vend.calDisBeforeTaxCheckBox))
            {
                vend.calDisBeforeTaxCheckBox = false;
            }
            
            if ((Variables.productVersion == "Canadian") && !Functions.GoodData(vend.includeFilingT5018CheckBox))
            {
                vend.includeFilingT5018CheckBox = false;
            }
            if (!Functions.GoodData(vend.printContactCheckBox))
            {
                vend.printContactCheckBox = false;
            }
            if (!Functions.GoodData(vend.ordersForThisVendor))
            {
                vend.ordersForThisVendor = "Print";
            }
            if (!Functions.GoodData(vend.emailConfirmCheckBox))
            {
                vend.emailConfirmCheckBox = false;
            }
            if (!Functions.GoodData(vend.synchronizeWithOutlook))
            {
                vend.synchronizeWithOutlook = false;
            }
            if (!Functions.GoodData(vend.taxCode))
            {
                vend.taxCode.description = Variables.sNoTax;
            }
            if (!Functions.GoodData(vend.displayCheckBox))
            {
                vend.displayCheckBox = false;
            }
            if (!Functions.GoodData(vend.hasSage50CheckBox))
            {
                vend.hasSage50CheckBox = false;
            }
            if (!Functions.GoodData(vend.usesMyItemNumCheckBox))
            {
                vend.usesMyItemNumCheckBox = false;
            }
            if ((!Functions.GoodData(vend.addCheckBox1) && Functions.GoodData(Variables.globalSettings.ReceivableSettings.AdditionalFields.Field1)))
            {
                vend.addCheckBox1 = false;
            }
            if ((!Functions.GoodData(vend.addCheckBox2) && Functions.GoodData(Variables.globalSettings.ReceivableSettings.AdditionalFields.Field2)))
            {
                vend.addCheckBox2 = false;
            }
            if ((!Functions.GoodData(vend.addCheckBox3) && Functions.GoodData(Variables.globalSettings.ReceivableSettings.AdditionalFields.Field3)))
            {
                vend.addCheckBox3 = false;
            }
            if ((!Functions.GoodData(vend.addCheckBox4) && Functions.GoodData(Variables.globalSettings.ReceivableSettings.AdditionalFields.Field4)))
            {
                vend.addCheckBox4 = false;
            }
            if ((!Functions.GoodData(vend.addCheckBox5) && Functions.GoodData(Variables.globalSettings.ReceivableSettings.AdditionalFields.Field5)))
            {
                vend.addCheckBox5 = false;
            }
		}
		
		public static VENDOR _SA_Read()
		{
			return PayablesLedger._SA_Read(null);
		}
		public static VENDOR _SA_Read(string sIdToRead)
		{
			VENDOR vend = new VENDOR();
			
			if(!PayablesLedger.repo.SelfInfo.Exists())
			{
				PayablesLedger._SA_Invoke();
			}
				
			
			if(Functions.GoodData(sIdToRead))
			{
				
				vend.name = sIdToRead;
				
				if(PayablesLedger.repo.SelectRecord.SelectedItemText != vend.name)
				{
					PayablesLedger._SA_Open(vend);	
				}
			}
			
			vend.name = PayablesLedger.repo.SelectRecord.SelectedItemText;
			vend.nameEdit = PayablesLedger.repo.VendorName.TextValue;
			vend.inactiveCheckBox = PayablesLedger.repo.InactiveVendor.Checked;
			
			
			#region Address Tab
			PayablesLedger.repo.Address.Tab.Click();
			vend.Address.contact = PayablesLedger.repo.Address.Contact.TextValue;
			vend.Address.street1 = PayablesLedger.repo.Address.Street1.TextValue;
			vend.Address.street2 = PayablesLedger.repo.Address.Street2.TextValue;
			vend.Address.city = PayablesLedger.repo.Address.City.TextValue;
			vend.Address.province = PayablesLedger.repo.Address.Province.TextValue;
			vend.Address.postalCode = PayablesLedger.repo.Address.PostalCode.TextValue;
			vend.Address.country = PayablesLedger.repo.Address.Country.TextValue;
			vend.Address.phone1 = PayablesLedger.repo.Address.Phone1.TextValue;
			vend.Address.phone2= PayablesLedger.repo.Address.Phone2.TextValue;
			vend.Address.fax = PayablesLedger.repo.Address.Fax.TextValue;
			vend.taxID = PayablesLedger.repo.Address.TaxID.TextValue;
			vend.Address.email = PayablesLedger.repo.Address.Email.TextValue;
			vend.Address.webSite = PayablesLedger.repo.Address.WebSite.TextValue;
			if(PayablesLedger.repo.Address.DepartmentInfo.Exists())
			{
				vend.department = PayablesLedger.repo.Address.Department.SelectedItemText;
			}
			vend.vendorSince = PayablesLedger.repo.Address.VendorSince.TextValue;
			#endregion
			
			
			#region Options Tab
			PayablesLedger.repo.Options.Tab.Click();
			
			vend.expenseAccount.acctNumber = PayablesLedger.repo.Options.ExpenseAccount.SelectedItemText;
			
			if(PayablesLedger.repo.Options.UsuallyStoreItemInInfo.Exists())
			{
				vend.usuallyStoreItemIn = PayablesLedger.repo.Options.UsuallyStoreItemIn.SelectedItemText;
			}
			
			if(PayablesLedger.repo.Options.CurrencyInfo.Exists())
			{
				vend.currencyCode = PayablesLedger.repo.Options.Currency.SelectedItemText;
			}
	
			vend.conductBusinessIn = PayablesLedger.repo.Options.ConductBusinessIn.SelectedItemText;
			vend.discountPercent = PayablesLedger.repo.Options.DiscountPercent.TextValue;
			vend.discountPeriod = PayablesLedger.repo.Options.DiscountPeriod.TextValue;
			vend.termPeriod = PayablesLedger.repo.Options.TermPeriod.TextValue;
			vend.calDisBeforeTaxCheckBox = PayablesLedger.repo.Options.CalculateDiscountBeforeTax.Checked;
			vend.printContactCheckBox = PayablesLedger.repo.Options.PrintContactOnCheques.Checked;
			vend.ordersForThisVendor = PayablesLedger.repo.Options.OrdersForVendor.SelectedItemText;
			vend.emailConfirmCheckBox = PayablesLedger.repo.Options.EmailConfirmationOfPurchases.Checked;

			if(PayablesLedger.repo.Options.SynchronizeWithMicrosoftOutInfo.Exists())
			{
				vend.synchronizeWithOutlook = PayablesLedger.repo.Options.SynchronizeWithMicrosoftOut.Checked;
			}
			else
			{
				vend.synchronizeWithOutlook = false;	
			}
			
			if(PayablesLedger.repo.Options.ChargeDutyOnItemsPurchasedInfo.Exists())
			{
				vend.chargeDutyCheckBox = PayablesLedger.repo.Options.ChargeDutyOnItemsPurchased.Checked;
			}
			else
			{
				vend.chargeDutyCheckBox = false;
			}
						
			#endregion
			
			
			#region Taxes Tab
			PayablesLedger.repo.Taxes.Tab.Click();
			
			// get container contents here
			
			vend.taxCode.code = Functions.GetField(PayablesLedger.repo.Taxes.TaxCode.SelectedItemText," - ",1);
			
			// if first item of no tax, assign proper string of no tax
			if(!Functions.GoodData(vend.taxCode) || vend.taxCode.code == "")
			{
				vend.taxCode.code = Variables.sNoTax;
			}
		
			PayablesLedger.repo.Self.Activate();
			// restore window here
			
			
			// get container contents here
            foreach (List<string> taxLine in PayablesLedger.repo.Taxes.TaxContainer.GetContents())
            {    
            	TAX_LEDGER TA = new TAX_LEDGER();
                TAX TX = new TAX ();
                TX.taxID = taxLine[0];
                TA.tax = TX;
                TA.taxExempt = taxLine[1];
     
				
                vend.taxList.Add(TA);
            }						
			
			#endregion
			
		
			#region PAD Tab
			PayablesLedger.repo.DirectDeposit.Tab.Click();
			vend.allowDirectDepCheckBox = PayablesLedger.repo.DirectDeposit.AllowDirectDeposits.Checked;
			vend.currencyAndLocation = PayablesLedger.repo.DirectDeposit.CurrencyAndLocation.SelectedItemText;
			vend.branchNumber = PayablesLedger.repo.DirectDeposit.BranchNumber.TextValue;
			vend.institutionNumber = PayablesLedger.repo.DirectDeposit.InstitutionNumber.TextValue;
			vend.accountNumber = PayablesLedger.repo.DirectDeposit.AccountNumber.TextValue;
			if(PayablesLedger.repo.DirectDeposit.EFTReferenceCheckBoxInfo.Exists())
			{
				vend.eftReferenceCheckbox = PayablesLedger.repo.DirectDeposit.EFTReferenceCheckBox.Checked;
				vend.eftReference = PayablesLedger.repo.DirectDeposit.EFTReference.TextValue;
				vend.eftTransCodeCheckbox = PayablesLedger.repo.DirectDeposit.TransCodeCheckBox.Checked;
				vend.eftTransCode = PayablesLedger.repo.DirectDeposit.TransCode.TextValue;
			}
			#endregion
			
			#region Statistics Tab
			PayablesLedger.repo.Statistics.Tab.Click();
			
			vend.ytdPurchases = PayablesLedger.repo.Statistics.YTDPurchases.TextValue;
			vend.lyPurchases = PayablesLedger.repo.Statistics.LYPurchases.TextValue;
			vend.ytdPayments = PayablesLedger.repo.Statistics.YTDPayments.TextValue;
			vend.previousYtdPayments = PayablesLedger.repo.Statistics.PreviousYTDPayments.TextValue;
	
			if(PayablesLedger.repo.Statistics.ForYTDPurchasesInfo.Exists())
			{
				vend.foreignYtdPurchases = PayablesLedger.repo.Statistics.ForYTDPurchases.TextValue;
				vend.foreignLyPurchases = PayablesLedger.repo.Statistics.ForLYPurchases.TextValue;
				vend.foreignYtdPayments = PayablesLedger.repo.Statistics.ForYTDPayments.TextValue;
				vend.foreignPreviousYtdPayments = PayablesLedger.repo.Statistics.ForPreviousYTDPayments.TextValue;
			}		
			#endregion
			
			#region Memo Tab
			PayablesLedger.repo.Memo.Tab.Click();
			vend.memo = PayablesLedger.repo.Memo.Memo.TextValue;
			vend.toDoDate = PayablesLedger.repo.Memo.ToDoDate.TextValue;
			vend.displayCheckBox = PayablesLedger.repo.Memo.DisplayThisMemoInTheDaily.Checked;
			
			#endregion
			
			#region Import/Export Tab
			PayablesLedger.repo.ImportExport.Tab.Click();
			if(PayablesLedger.repo.ImportExport.ThisVendorHasSage50Info.Exists())
			{
				vend.hasSage50CheckBox = PayablesLedger.repo.ImportExport.ThisVendorHasSage50.Checked;
				vend.usesMyItemNumCheckBox = PayablesLedger.repo.ImportExport.ThisVendorUsesMyItemNumbers.Checked;
				if(vend.usesMyItemNumCheckBox == false)
				{
					// add container extract here
					
					
					string myNumOrAccount;
                    string tempText;


                    // Restore containter to default
                    PayablesLedger.repo.Self.Activate();
                    //PayablesLedger.repo.RestoreWindow.Select();

         
                    foreach (List<string> importLine in PayablesLedger.repo.ImportExport.ImportContainer.GetContents())
                    {
                        IMPORT imp = new IMPORT();
                        imp.itemNumber = importLine[0];

                        // Gets the My item number or my account number
                        myNumOrAccount = importLine[1];

                        // Determine if item number or account number by seeing if more than one word
                        tempText = Functions.GetField(myNumOrAccount, " ", 2);

                        // Item number is always one word so second field should return ""
                        if (tempText == "")
                        {
                            imp.myItemNumber = myNumOrAccount;
                        }
                        else
                        {
                            imp.myAccount.acctNumber = myNumOrAccount;
                        }

                        vend.imports.Add(imp);
                    }				
				}
			}
			else
			{
				vend.hasSage50CheckBox = false;
				vend.usesMyItemNumCheckBox = false;
			}
			
			#endregion
			
			#region Additional Tab
			PayablesLedger.repo.AdditionalInfo.Tab.Click();
			if(PayablesLedger.repo.AdditionalInfo.Additional1Info.Exists())
			{
				vend.additional1 = PayablesLedger.repo.AdditionalInfo.Additional1.TextValue;
				vend.addCheckBox1 = PayablesLedger.repo.AdditionalInfo.AddCheckBox1.Checked;
			}
			if(PayablesLedger.repo.AdditionalInfo.Additional2Info.Exists())
			{
				vend.additional2 = PayablesLedger.repo.AdditionalInfo.Additional2.TextValue;
				vend.addCheckBox2 = PayablesLedger.repo.AdditionalInfo.AddCheckBox2.Checked;
			}
			if(PayablesLedger.repo.AdditionalInfo.Additional3Info.Exists())
			{
				vend.additional3 = PayablesLedger.repo.AdditionalInfo.Additional3.TextValue;
				vend.addCheckBox3 = PayablesLedger.repo.AdditionalInfo.AddCheckBox3.Checked;
			}
			if(PayablesLedger.repo.AdditionalInfo.Additional4Info.Exists())
			{
				vend.additional4 = PayablesLedger.repo.AdditionalInfo.Additional4.TextValue;
				vend.addCheckBox4 = PayablesLedger.repo.AdditionalInfo.AddCheckBox4.Checked;
			}
			if(PayablesLedger.repo.AdditionalInfo.Additional5Info.Exists())
			{
				vend.additional5 = PayablesLedger.repo.AdditionalInfo.Additional5.TextValue;
				vend.addCheckBox5 = PayablesLedger.repo.AdditionalInfo.AddCheckBox5.Checked;
			}
			
			#endregion
			
			
			return vend;
			
		}	
		public static void _SA_Close()
		{
			repo.Self.Close();
	
		}
		public static void _SA_Delete(VENDOR vend)
		{
			
		}
		public static void _SA_History(VENDOR vend)
		{
			
		}
		public static void DataFile_ReadFile(string sDataLocation, string fileCounter)
		{
			string EXTENSION_HEADER = ".hdr";
            string EXTENSION_OPTIONS = ".dt1";
            string EXTENSION_TAXES = ".dt2";
            string EXTENSION_STATISTICS = ".dt3";
            string EXTENSION_MEMO = ".dt4";
            string EXTENSION_IMPORT_EXPORT = ".dt5";
            string EXTENSION_ADDITIONAL_INFO = ".dt6";
            string EXTENSION_HISTORY = ".dt7";
            string EXTENSION_DIRECT_DEP = "dt8";
            string EXTENSION_TAX_TABLE = ".cnt";
            string EXTENSION_IMPORT_TABLE = ".cnt2";
           
            // Local variables
            string dataLine;	// Stores the current field data from file
            string dataPath;	// The name and path of the data file
            
            StreamReader FileHDR;	// File handle for header / address tab info
            StreamReader FileDT1;	// File handle for the ship-to address tab info
            StreamReader FileDT2;	// File handle for the options tab info
            StreamReader FileDT3;	// File handle for the taxes Info tab info
            StreamReader FileDT4;	// File handle for the statistics Info tab info
            StreamReader FileDT5;	// File handle for the memo Info tab info
            StreamReader FileDT6;	// File handle for the Additional Info tab info
            StreamReader FileDT7;	// File handle for the history tab info
            StreamReader FileDT8;	// File handle for the import/export tab info
            StreamReader FileCNT;	// File handle for the tax container Info
            StreamReader FileCN2;	// File handle for the import container file
           
            
            // Get the data path from file
            dataPath = sDataLocation + "VL" + fileCounter;


            List<VENDOR> lVends = new List<VENDOR>() { };

            // Open all data files and set data structure, if they exist, if not then flag not to do the info for the missing file
            // Open header file
            using (FileHDR = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_HEADER, "FM_READ")))
            {
                // While not EOF read the header file
                while ((dataLine = FileHDR.ReadLine()) != null)
                {
                    VENDOR vend = new VENDOR();
                    // Set header info
                    vend = PayablesLedger.DataFile_setDataStructure(EXTENSION_HEADER, dataLine, vend);

                    // Open DT1 file and set structure
                    if (File.Exists(dataPath + EXTENSION_OPTIONS))
                    {
                        using (FileDT1 = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_OPTIONS, "FM_READ")))
                        {
                            while ((dataLine = FileDT1.ReadLine()) != null)
                            {
                                vend = PayablesLedger.DataFile_setDataStructure(EXTENSION_OPTIONS, dataLine, vend);
                            }
                            FileDT1.Close();
                        }
                    }
                    // Open DT2 file and set structure
                    if (File.Exists(dataPath + EXTENSION_TAXES))
                    {
                        using (FileDT2 = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_TAXES, "FM_READ")))
                        {
                            while ((dataLine = FileDT2.ReadLine()) != null)
                            {
                                vend = PayablesLedger.DataFile_setDataStructure(EXTENSION_TAXES, dataLine, vend);
                            }
                            
                        }
                    }

                    // Open DT3 file and set structure
                    if (File.Exists(dataPath + EXTENSION_STATISTICS))
                    {
                        using(FileDT3 = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_STATISTICS, "FM_READ")))
                        {
                            while ((dataLine = FileDT3.ReadLine()) != null)
                            {
                                vend = PayablesLedger.DataFile_setDataStructure(EXTENSION_STATISTICS, dataLine, vend);
                            }
                            FileDT3.Close();
                        }
                    }

                    // Open DT4 file and set structure
                    if (File.Exists(dataPath + EXTENSION_MEMO))
                    {
                        using (FileDT4 = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_MEMO, "FM_READ")))
                        {
                            while ((dataLine = FileDT4.ReadLine()) != null)
                            {
                                vend = PayablesLedger.DataFile_setDataStructure(EXTENSION_MEMO, dataLine, vend);
                            }
                            FileDT4.Close();
                        }
                    }

                    // Open DT5 file and set structure
                    if (File.Exists(dataPath + EXTENSION_IMPORT_EXPORT))
                    {
                        using(FileDT5 = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_IMPORT_EXPORT, "FM_READ")))
                        {
                            while ((dataLine = FileDT5.ReadLine()) != null)
                            {
                                vend = PayablesLedger.DataFile_setDataStructure(EXTENSION_IMPORT_EXPORT, dataLine, vend);
                            }
                            FileDT5.Close();
                        }
                    }

                    // Open DT6 file and set structure
                    if (File.Exists(dataPath + EXTENSION_ADDITIONAL_INFO))
                    {
                        using (FileDT6 = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_ADDITIONAL_INFO, "FM_READ")))
                        {
                            while ((dataLine = FileDT6.ReadLine()) != null)
                            {
                                vend = PayablesLedger.DataFile_setDataStructure(EXTENSION_ADDITIONAL_INFO, dataLine, vend);
                            }
                            FileDT6.Close();
                        }
                    }

                    // Only open history detail file if it exists
                    if (File.Exists(dataPath + EXTENSION_HISTORY))
                    {
//                        using (FileDT7 = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_HISTORY, "FM_READ")))
//                        {
//                            while ((dataLine = FileDT7.ReadLine()) != null)
//                            {
//                                vend = PayablesLedger.DataFile_setDataStructure(EXTENSION_HISTORY, dataLine, vend);
//                            }
//                        }
                    }
                    
                    // Open and set direct deposit file
                    if (File.Exists(dataPath + EXTENSION_DIRECT_DEP))
                    {
                        using(FileDT8 = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_DIRECT_DEP, "FM_READ")))
                        {
                            while ((dataLine = FileDT8.ReadLine()) != null)
                            {
                                vend = PayablesLedger.DataFile_setDataStructure(EXTENSION_DIRECT_DEP, dataLine, vend);
                            }
                            FileDT8.Close();
                        }
                    }

                    // Open and set tax container file
                    if (File.Exists(dataPath + EXTENSION_TAX_TABLE))
                    {
                        using(FileCNT = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_TAX_TABLE, "FM_READ")))
                        {
                            while ((dataLine = FileCNT.ReadLine()) != null)
                            {
                                vend = PayablesLedger.DataFile_setDataStructure(EXTENSION_TAX_TABLE, dataLine, vend);
                            }
                            FileCNT.Close();
                        }
                    }

                    // Only open import container file if it exists
                    if (File.Exists(dataPath + EXTENSION_IMPORT_TABLE))
                    {
                        using(FileCN2 = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_IMPORT_TABLE, "FM_READ")))
                        {
                            while ((dataLine = FileCN2.ReadLine()) != null)
                            {
                                vend = PayablesLedger.DataFile_setDataStructure(EXTENSION_IMPORT_TABLE, dataLine, vend);
                            }
                            FileCN2.Close();
                        }
                    }

                    lVends.Add(vend);
                }
            }

            for (int x = 0; x < lVends.Count; x++)
            {
                VENDOR vend = lVends[x];
                // Determine the mode
                switch (vend.action)
                {
                    case "A":
                        PayablesLedger._SA_Create(vend);

                        break;
                    case "E":
                        PayablesLedger._SA_Create(vend, true, true);

                        break;
                    case "D":
                        PayablesLedger._SA_Delete(vend);

                        break;
                    case "H":
                        PayablesLedger._SA_History(vend);

                        break;
                    default:
                        {
                            Functions.Verify(false, true, "Action set properly");
                            //PayablesLedger.ClickUndoChanges();
                            break;
                        }
                }
                PayablesLedger._SA_Close();
            }
		}
		public static VENDOR DataFile_setDataStructure(string extension, string dataLine, VENDOR vend)
		{
			VENDOR vendorRecord = vend;
			
            switch (extension.ToUpper())
            {
                case ".HDR":
                    vendorRecord.action = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 1));
                    vendorRecord.name = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 2));
                    vendorRecord.Address.contact = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 3));
                    vendorRecord.Address.street1 = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 4));
                    vendorRecord.Address.street2 = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 5));
                    vendorRecord.Address.city = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 6));
                    vendorRecord.Address.province = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 7));
                    vendorRecord.Address.postalCode = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 8));
                    vendorRecord.Address.country = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 9));
                    vendorRecord.Address.phone1 = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 10));
                    vendorRecord.Address.phone2 = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 11));
                    vendorRecord.Address.fax = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 12));                   
                    vendorRecord.taxID = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 13));
                   	vendorRecord.Address.email = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 14));
                    vendorRecord.Address.webSite = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 15));
                    vendorRecord.department = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 16));
                    vendorRecord.vendorSince = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 17));
                    vendorRecord.inactiveCheckBox = ConvertFunctions.StringToBool(Functions.GetField (dataLine, ",", 18));
                    vendorRecord.nameEdit = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 20));
          
                    break;
                case ".DT1":
                    if (vendorRecord.name == Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 1)))
                    {
                        vendorRecord.discountPercent = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 2));
                        vendorRecord.discountPeriod = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 3));
                        vendorRecord.termPeriod = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 4));
                        vendorRecord.calDisBeforeTaxCheckBox = ConvertFunctions.StringToBool(Functions.GetField (dataLine, ",", 5));
                        vendorRecord.printContactCheckBox = ConvertFunctions.StringToBool(Functions.GetField (dataLine, ",", 6));
                        vendorRecord.emailConfirmCheckBox = ConvertFunctions.StringToBool(Functions.GetField (dataLine, ",", 7));
                        vendorRecord.includeFilingT5018CheckBox = ConvertFunctions.StringToBool(Functions.GetField (dataLine, ",", 8));
                        vendorRecord.chargeDutyCheckBox = ConvertFunctions.StringToBool(Functions.GetField (dataLine, ",", 9));
                        vendorRecord.ordersForThisVendor = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 10));
                        vendorRecord.currency = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 11));
                        vendorRecord.expenseAccount.acctNumber = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 12));                       
                        vendorRecord.conductBusinessIn = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 13));                       
                        vendorRecord.synchronizeWithOutlook = ConvertFunctions.StringToBool(Functions.GetField (dataLine, ",", 14));
                        vendorRecord.usuallyStoreItemIn = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 15));                 
                    }
				
                    break;
                case ".DT2":
                    if (vendorRecord.name == Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 1)))
                    {
						vendorRecord.taxCode.code = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 2));
                    }
				
                    break;
                case ".DT3":
                    if (vendorRecord.name == Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 1)))
                    {
                        vendorRecord.ytdPurchases = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 2));
                        vendorRecord.lyPurchases = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 3));
                        vendorRecord.ytdPayments = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 4));
                        vendorRecord.previousYtdPayments = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 5));
                        vendorRecord.foreignYtdPurchases = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 6));
                        vendorRecord.foreignLyPurchases = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 7));
                        vendorRecord.foreignYtdPayments = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 8));
                        vendorRecord.foreignPreviousYtdPayments = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 9));
                    }
				
                    break;
                case ".DT4":
                    if (vendorRecord.name == Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 1)))
                    {
                        vendorRecord.memo = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 2));
                        vendorRecord.toDoDate = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 3));
                        vendorRecord.displayCheckBox = ConvertFunctions.StringToBool(Functions.GetField(dataLine, ",", 4));
                    }
				
                    break;
                case ".DT5":
                    if (vendorRecord.name == Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 1)))
                    {
                        vendorRecord.hasSage50CheckBox = ConvertFunctions.StringToBool(Functions.GetField (dataLine, ",", 2));
                        vendorRecord.usesMyItemNumCheckBox = ConvertFunctions.StringToBool(Functions.GetField (dataLine, ",", 3));
                    }
				
                    break;
                case ".DT6":
                    if (vendorRecord.name == Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 1)))
                    {
                        vendorRecord.additional1= Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 2));
                        vendorRecord.additional2 = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 3));
                        vendorRecord.additional3 = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 4));
                        vendorRecord.additional4 = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 5));
                        vendorRecord.additional5 = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 6));
                        vendorRecord.addCheckBox1 = ConvertFunctions.StringToBool(Functions.GetField (dataLine, ",", 7));
                        vendorRecord.addCheckBox2 = ConvertFunctions.StringToBool(Functions.GetField (dataLine, ",", 8));
                        vendorRecord.addCheckBox3 = ConvertFunctions.StringToBool(Functions.GetField (dataLine, ",", 9));
                        vendorRecord.addCheckBox4 = ConvertFunctions.StringToBool(Functions.GetField (dataLine, ",", 10));
                        vendorRecord.addCheckBox5 = ConvertFunctions.StringToBool(Functions.GetField (dataLine, ",", 11));
                    }
				
                    break;
                case ".DT7":
                    if (vendorRecord.name == Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 1)))
                    {
                        if (Functions.GetField(dataLine, "", 2) == "i")
                        {
                            vendorRecord.recordType = LEDGER_HISTORY.HIST_INVOICE;
                        }
                        else
                        {
                        	vendorRecord.recordType = LEDGER_HISTORY.HIST_PAYMENT;
                        }
                           
                        vendorRecord.invoiceNumber = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 3));
                        vendorRecord.invoiceDate = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 4));
                        vendorRecord.percentOrTaken = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 5));
                        vendorRecord.daysOrAmountPaid = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 6));
                        vendorRecord.NetDaysOrExchangeRate = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 7));
                        vendorRecord.amount = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 8));
                        vendorRecord.tax = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 9));
                        vendorRecord.exchangeRate = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 10));
                        vendorRecord.homeAmount = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 11));
                    }
				
                    break;
                    case ".DT8":
                    if (vendorRecord.name == Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 1)))
                    {
                        vendorRecord.allowDirectDepCheckBox= ConvertFunctions.StringToBool(Functions.GetField (dataLine, ",", 2));
                        vendorRecord.currencyAndLocation = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 3));
                        vendorRecord.branchNumber = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 4));
                        vendorRecord.institutionNumber = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 5));
                        vendorRecord.accountNumber = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 6));
                    }
                    
                    break;             
                case ".CNT":
                    if (vendorRecord.name == Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 1)))
                    {
                        TAX_LEDGER TA = new TAX_LEDGER ();
                        TAX TX = new TAX();
                        TX.taxID = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 2));
                        TA.taxExempt = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 3));
                        
                        if (Functions.GoodData (vendorRecord.taxList))
                        {
                            vendorRecord.taxList.Add(TA);
                        }
                        else
                        {
                            vendorRecord.taxList = new List<TAX_LEDGER>() {TA};
                        }
                    }
				
                    break;
                case ".CNT2":
                    if (vendorRecord.name == Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 1)))
                    {
                        IMPORT IMP = new IMPORT();
                        IMP.itemNumber = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 2));
                        IMP.myItemNumber = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 3));
                        IMP.myAccount.acctNumber = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 4));
                        if (vendorRecord.imports.Count !=0)
                        {
                            vendorRecord.imports.Add(IMP);
                        }
                        else
                        {
                            vendorRecord.imports = new List<IMPORT>{IMP};
                        }
                    }
				
                    break;
                default:
                {
                    Functions.Verify(false, true, "Valid extension used");
                    break;
                }
            }
            return (vend);
		}
	}

	public static class VendorIcon
	{
		public static PayablesLedgerResFolders.PayablesIconAppFolder repo = PayablesLedgerRes.Instance.PayablesIcon;
	}
}