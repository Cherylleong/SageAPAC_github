/*
 * Created by Ranorex
 * User: wonda05
 * Date: 4/28/2016
 * Time: 10:24 AM
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
	public static class ReceivablesLedger
	{
		// TODO: Add Code to call testing functions - for example:	
		// Automation.Start()
		public static ReceivablesLedgerResFolders.ReceivablesLedgerAppFolder repo = ReceivablesLedgerRes.Instance.ReceivablesLedger;
		

		public static void _SA_Invoke(Boolean bOpenLedger)
		{
			// open ledger depending on view type
			
			if(Simply.isEnhancedView())
			{	
				Simply.repo.Self.Activate();
				Simply.repo.ReceivablesLink.Click();
				Simply.repo.CustomerIcon.Click();
			}
			else
			{
				
			}
			
			if(CustomerIcon.repo.SelfInfo.Exists())
			{
				if(bOpenLedger == true)
				{
					CustomerIcon.repo.CreateNew.Click();
					CustomerIcon.repo.Self.Close();
				}
			}			
		}
		
		public static void _SA_Invoke()
		{
			ReceivablesLedger._SA_Invoke(true);
			

		}	
		public static void _SA_Open(CUSTOMER cust)
		{
			if(!ReceivablesLedger.repo.SelfInfo.Exists())
			{
				ReceivablesLedger._SA_Invoke();
			}
			ReceivablesLedger.repo.SelectRecord.Select(cust.name);
		}	
		public static void _SA_Create(CUSTOMER cust)
		{
			
			ReceivablesLedger._SA_Create(cust, true, false);
		}	
		public static void _SA_Create(CUSTOMER cust, bool bSave, bool bEdit )
		{
			if(!Variables.bUseDataFiles && !bEdit)
			{
				ReceivablesLedger._SA_MatchDefaults(cust);
			}
			
			
			if(!ReceivablesLedger.repo.SelfInfo.Exists())
			{
				ReceivablesLedger._SA_Invoke();
			}
			
			
			if(bEdit)
			{
				if(ReceivablesLedger.repo.SelectRecord.SelectedItemText != cust.name)
				{
					ReceivablesLedger._SA_Open(cust);	
				}
	
				if(Functions.GoodData(cust.nameEdit))
				{
					ReceivablesLedger.repo.CustomerName.TextValue = cust.nameEdit;
					cust.name = cust.nameEdit;	
				}
					
				// print to log or results
				Ranorex.Report.Info(String.Format("Modifying customer {0}",cust.name));
				
			}
			else
			{
				ReceivablesLedger.repo.CreateANewToolButton.Click();
				ReceivablesLedger.repo.CustomerName.TextValue = cust.name;
				Ranorex.Report.Info(String.Format("Creating customer {0}",cust.name));
			}
		
			
			#region Address Tab

			// Select tab 
			ReceivablesLedger.repo.Address.Tab.Click();
			
			if(Functions.GoodData(cust.Address.contact))
			{
				ReceivablesLedger.repo.Address.Contact.TextValue = cust.Address.contact;
			}
			
			if(Functions.GoodData(cust.Address.street1))
			{
				ReceivablesLedger.repo.Address.Street1.TextValue = cust.Address.street1;
			}
			
			if(Functions.GoodData(cust.Address.street2))
			{
				ReceivablesLedger.repo.Address.Street2.TextValue = cust.Address.street2;
			}
			
			if(Functions.GoodData(cust.Address.city))
			{
				ReceivablesLedger.repo.Address.City.TextValue = cust.Address.city;
			}
			
			if(Functions.GoodData(cust.Address.province))
			{
				ReceivablesLedger.repo.Address.Province.TextValue = cust.Address.province;
			}
								
			if(Functions.GoodData(cust.Address.postalCode))
			{
				ReceivablesLedger.repo.Address.Postalcode.TextValue = cust.Address.postalCode;
			}
			
			if(Functions.GoodData(cust.Address.country))
			{
				ReceivablesLedger.repo.Address.Country.TextValue = cust.Address.country;
			}
			
			if(Functions.GoodData(cust.Address.phone1))
			{
				ReceivablesLedger.repo.Address.Phone1.TextValue = cust.Address.phone1;
			}
			
			if(Functions.GoodData(cust.Address.phone2))
			{
				ReceivablesLedger.repo.Address.Phone2.TextValue = cust.Address.phone2;
			}
			
			if(Functions.GoodData(cust.Address.fax))
			{
				ReceivablesLedger.repo.Address.Fax.TextValue = cust.Address.fax;
			}
			
			if(Functions.GoodData(cust.Address.email))
			{
				ReceivablesLedger.repo.Address.Email.TextValue = cust.Address.email;
			}
								
			if(Functions.GoodData(cust.Address.webSite))
			{
				ReceivablesLedger.repo.Address.Website.TextValue = cust.Address.webSite;
			}
			
			if(ReceivablesLedger.repo.Address.DepartmentInfo.Exists())
			{
				if(Functions.GoodData(cust.department))
				{
					ReceivablesLedger.repo.Address.Department.Select(cust.department);
				}
			}
			
			if(Functions.GoodData(cust.salesPerson))
			{
				
				ReceivablesLedger.repo.Address.Salesperson.Select(cust.salesPerson);
			}
			
			if(Functions.GoodData(cust.customerSince))
			{
				ReceivablesLedger.repo.Address.CustomerSince.TextValue = cust.customerSince;
			}
			
			if(Functions.GoodData(cust.inactiveCheckBox))
			{
				ReceivablesLedger.repo.InactiveCustomer.SetState(cust.inactiveCheckBox);
			}
						
			if(Functions.GoodData(cust.internalCheckBox))
			{
				ReceivablesLedger.repo.InternalCustomer.SetState(cust.internalCheckBox);
			}
			
			
			#endregion
			
			#region Ship To Address Tab
			// ship to address tab
			ReceivablesLedger.repo.ShipToAddress.Tab.Click();
				
			if(ReceivablesLedger.repo.ShipToAddress.AddressNameInfo.Exists())
			{
				//Functions.SelectComboBox(ReceivablesLedger.repo.ShipToAddress.AddressName,"Ship-to Address");
				ReceivablesLedger.repo.ShipToAddress.AddressName.Select("Ship-to Address");
			}
			
			if(Functions.GoodData(cust.defaultShipToAddressCheckbox))
			{
			   	
				ReceivablesLedger.repo.ShipToAddress.DefaultShipTo.SetState(cust.defaultShipToAddressCheckbox);
			}
			
			if(Functions.GoodData(cust.ShipToAddress.contact))
			{
				if(ReceivablesLedger.repo.ShipToAddress.Contact.Enabled)
				{
					ReceivablesLedger.repo.ShipToAddress.Contact.TextValue = cust.ShipToAddress.contact;
				}
			}
			
			if(Functions.GoodData(cust.ShipToAddress.street1))
			{
				if(ReceivablesLedger.repo.ShipToAddress.Street1.Enabled)
				{
					ReceivablesLedger.repo.ShipToAddress.Street1.TextValue = cust.ShipToAddress.street1;
				}
			}
			
			if(Functions.GoodData(cust.ShipToAddress.street2))
			{
				if(ReceivablesLedger.repo.ShipToAddress.Street2.Enabled)
				{
					ReceivablesLedger.repo.ShipToAddress.Street2.TextValue = cust.ShipToAddress.street2;
				}
			}
			
			if(Functions.GoodData(cust.ShipToAddress.city))
			{
				if(ReceivablesLedger.repo.ShipToAddress.City.Enabled)
				{
					ReceivablesLedger.repo.ShipToAddress.City.TextValue = cust.ShipToAddress.city;
				}
			}
			
			if(Functions.GoodData(cust.ShipToAddress.province))
			{
				if(ReceivablesLedger.repo.ShipToAddress.Province.Enabled)
				{
					ReceivablesLedger.repo.ShipToAddress.Province.TextValue = cust.ShipToAddress.province;
				}
			}
			
			if(Functions.GoodData(cust.ShipToAddress.postalCode))
			{
				if(ReceivablesLedger.repo.ShipToAddress.Postalcode.Enabled)
				{
					ReceivablesLedger.repo.ShipToAddress.Postalcode.TextValue = cust.ShipToAddress.postalCode;
				}
			}
			
			if(Functions.GoodData(cust.ShipToAddress.country))
			{
				if(ReceivablesLedger.repo.ShipToAddress.Country.Enabled)
				{
					ReceivablesLedger.repo.ShipToAddress.Country.TextValue = cust.ShipToAddress.country;
				}
			}
			
			if(Functions.GoodData(cust.ShipToAddress.phone1))
			{
				if(ReceivablesLedger.repo.ShipToAddress.Phone1.Enabled)
				{
					ReceivablesLedger.repo.ShipToAddress.Phone1.TextValue = cust.ShipToAddress.phone1;
				}
			}
			
			if(Functions.GoodData(cust.ShipToAddress.phone2))
			{
				if(ReceivablesLedger.repo.ShipToAddress.Phone2.Enabled)
				{
					ReceivablesLedger.repo.ShipToAddress.Phone2.TextValue = cust.ShipToAddress.phone2;
				}
			}
			
			if(Functions.GoodData(cust.ShipToAddress.fax))
			{
				if(ReceivablesLedger.repo.ShipToAddress.Fax.Enabled)
				{
					ReceivablesLedger.repo.ShipToAddress.Fax.TextValue = cust.ShipToAddress.fax;
				}
			}
			
			if(Functions.GoodData(cust.ShipToAddress.email))
			{
				if(ReceivablesLedger.repo.ShipToAddress.Email.Enabled)
				{
					ReceivablesLedger.repo.ShipToAddress.Email.TextValue = cust.ShipToAddress.email;
				}
			}
			#endregion
			
			#region Options Tab
			ReceivablesLedger.repo.Options.Tab.Click();
			
			if(Functions.GoodData(cust.revenueAccount.acctNumber))
			{
				ReceivablesLedger.repo.Options.RevenueAccount.Select(cust.revenueAccount.acctNumber);
			}
			
			if(Functions.GoodData(cust.currencyCode))
			{
				if(ReceivablesLedger.repo.Options.CurrencyInfo.Exists() && ReceivablesLedger.repo.Options.Currency.Enabled)
				{
					if(Variables.bUseDataFiles) // check if item is in lists
					{
						ReceivablesLedger.repo.Options.Currency.Items[2].Select();
					}
					else
					{
						ReceivablesLedger.repo.Options.Currency.Select(cust.currencyCode);
					}
				}
			}
			
			if(Functions.GoodData(cust.priceList))
			{
				ReceivablesLedger.repo.Options.PriceList.Select(cust.priceList);
			}
			
			if(Functions.GoodData(cust.conductBusinessIn))
			{
				ReceivablesLedger.repo.Options.ConductBusinessIn.Select(cust.conductBusinessIn);
			}
			
			
			if(Functions.GoodData(cust.usuallyShipItemFrom) && cust.usuallyShipItemFrom !="")
			{
				if(ReceivablesLedger.repo.Options.UsuallyShipItemFromInfo.Exists())
				{
					ReceivablesLedger.repo.Options.UsuallyShipItemFrom.Select(cust.usuallyShipItemFrom);
				}
			}
			
			if(Functions.GoodData(cust.standardDiscount))
			{
				if(ReceivablesLedger.repo.Options.StandardDiscountInfo.Exists())
				{
					ReceivablesLedger.repo.Options.StandardDiscount.TextValue = cust.standardDiscount;
				}
			}
			
			if(Functions.GoodData(cust.discountPercent))
			{
				ReceivablesLedger.repo.Options.DiscountPercent.TextValue = cust.discountPercent;	
			}
			
			if(Functions.GoodData(cust.discountPeriod))
			{
				ReceivablesLedger.repo.Options.DiscountPeriod.TextValue = cust.discountPeriod;	
			}
			
			if(Functions.GoodData(cust.termPeriod))
			{
				ReceivablesLedger.repo.Options.TermPeriod.TextValue = cust.termPeriod;	
			}
			
			if(Functions.GoodData(cust.produceStatementsForThisCustCheckbox))
			{
			   	
				ReceivablesLedger.repo.Options.ProduceStatementsForThisCustomer.SetState(cust.produceStatementsForThisCustCheckbox);
			}
			
			if(Functions.GoodData(cust.formsForThisCustomer))
			{
				ReceivablesLedger.repo.Options.FormsForThisCustomer.Select(cust.formsForThisCustomer);	
			}
			
			if(Functions.GoodData(cust.synchronizeWithOutlook) && ReceivablesLedger.repo.Options.SyncWithOutlookInfo.Exists())
			{
				ReceivablesLedger.repo.Options.SyncWithOutlook.SetState(cust.synchronizeWithOutlook);
			}
			
			#endregion
			
			#region Taxes Tab
			ReceivablesLedger.repo.Taxes.Tab.Click();
			
			if(Functions.GoodData(cust.taxList))
			{
				ReceivablesLedger.repo.Self.Activate();
			
				bool bFound;
				
				List<List <string>> lsContents = ReceivablesLedger.repo.Taxes.TaxContainer.GetContents();
				List<TAX_LEDGER> lsTempTaxes = new List<TAX_LEDGER>();
				
				// Loop through entire container
				
				foreach(List<string> currentRow in lsContents)
				{
					bFound = false;
					
					foreach(TAX_LEDGER currentTax in cust.taxList)
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
				
				cust.taxList = lsTempTaxes;
				
				// enter into container here
				for (int x = 0; x < cust.taxList.Count; x++)
                {
                    // go to correct line
                    ReceivablesLedger.repo.Taxes.TaxContainer.SetToLine(x);
                    ReceivablesLedger.repo.Taxes.TaxContainer.MoveRight();
                    // only set tax exempt if not null
                    if (Functions.GoodData(cust.taxList[x].taxExempt))
                    {
                        if (lsContents[x][1] != cust.taxList[x].taxExempt)
                        {
                             ReceivablesLedger.repo.Taxes.TaxContainer.Toggle();
                        }
                    }
                    // set tax id
                    ReceivablesLedger.repo.Taxes.TaxContainer.MoveRight();
                    ReceivablesLedger.repo.Taxes.TaxContainer.SetText(cust.taxList[x].taxID);
                }
			}
			
			if(Functions.GoodData(cust.taxCode) && (cust.taxCode.code != Variables.sNoTax))
			{
				// Loop through each item and check if the tax code matches
                for (int i = 0; i < ReceivablesLedger.repo.Taxes.TaxCode.Items.Count; i++)
                {

                	ReceivablesLedger.repo.Taxes.TaxCode.Items[i].Select();

                    // Exit for if tax code is matched
                    if (Functions.GetField(ReceivablesLedger.repo.Taxes.TaxCode.Text, "-", 1).Trim() == cust.taxCode.code)
                    {
                        break;
                    }
                }	
			}
			else
			{
				ReceivablesLedger.repo.Taxes.TaxCode.Select(" - No Tax");
			}
			
					
			#endregion
			                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                
			
			#region PAD Tab
			ReceivablesLedger.repo.PAD.Tab.Click();
			
			if(Functions.GoodData(cust.custHasPADCheckbox))
			{
				ReceivablesLedger.repo.PAD.CustHasPAD.SetState(cust.custHasPADCheckbox);
			}
			
			if(Functions.GoodData(cust.currencyAndLocation))
			{
				ReceivablesLedger.repo.PAD.CurrencyAndLocation.Select(cust.currencyAndLocation);	
			}
			
			if(Functions.GoodData(cust.branchNumber))
			{
				ReceivablesLedger.repo.PAD.BranchNumber.TextValue = cust.branchNumber;
			}
			
			if(Functions.GoodData(cust.institutionNumber))
			{
				ReceivablesLedger.repo.PAD.InstitutionNumber.TextValue = cust.institutionNumber;
			}
						
			if(Functions.GoodData(cust.accountNumber))
			{
				ReceivablesLedger.repo.PAD.AccountNumber.TextValue = cust.accountNumber;
			}

			if(Functions.GoodData(cust.eftReferenceCheckbox))
			{
				ReceivablesLedger.repo.PAD.EftRefCheckBox.SetState(cust.eftReferenceCheckbox);
			}
			
			if(Functions.GoodData(cust.eftReference))
			{
				ReceivablesLedger.repo.PAD.EftRerence.TextValue = cust.eftReference;
			}

			if(Functions.GoodData(cust.eftTransCodeCheckbox))
			{
				ReceivablesLedger.repo.PAD.TransCodeCheckBox.SetState(cust.eftTransCodeCheckbox);
			}

			if(Functions.GoodData(cust.eftTransCode))
			{
				ReceivablesLedger.repo.PAD.TransCode.TextValue = cust.eftTransCode;
			}
		
			#endregion
			ReceivablesLedger.repo.Statistics.Tab.Click();
			
			if(Functions.GoodData(cust.ytdSales))
			{
			   	if(ReceivablesLedger.repo.Statistics.YTDSalesInfo.Exists())
			   	{
			   		ReceivablesLedger.repo.Statistics.YTDSales.TextValue = cust.ytdSales;
			   	}
			}
			   
			   
			if(Functions.GoodData(cust.lySales))
			{
			   	if(ReceivablesLedger.repo.Statistics.LYSalesInfo.Exists())
			   	{
			   		ReceivablesLedger.repo.Statistics.LYSales.TextValue = cust.lySales;
			   	}
			}
			
			if(Functions.GoodData(cust.creditLimit))
			{
			   	if(ReceivablesLedger.repo.Statistics.CreditLimitInfo.Exists())
			   	{
			   		ReceivablesLedger.repo.Statistics.CreditLimit.TextValue = cust.creditLimit;
			   	}
			}
			
			if(Functions.GoodData(cust.foreignYtdSales))
			{
			   	if(ReceivablesLedger.repo.Statistics.ForeignYTDSalesInfo.Exists())
			   	{
			   		ReceivablesLedger.repo.Statistics.ForeignYTDSales.TextValue = cust.foreignYtdSales;
			   	}
			}
			
			if(Functions.GoodData(cust.foreignLySales))
			{
			   	if(ReceivablesLedger.repo.Statistics.ForeignYTDSalesInfo.Exists())
			   	{
			   		ReceivablesLedger.repo.Statistics.YTDSales.TextValue = cust.foreignYtdSales;
			   	}
			}
			
			if(Functions.GoodData(cust.foreignCreditLimit))
			{
			   	if(ReceivablesLedger.repo.Statistics.ForeignCreditLimitInfo.Exists())
			   	{
			   		ReceivablesLedger.repo.Statistics.ForeignCreditLimit.TextValue = cust.foreignCreditLimit;
			   	}
			}
			
			        	
			#region Statistics
			
			
			
			#endregion
			
			
			#region Memo Tab
			
			ReceivablesLedger.repo.Memo.Tab.Click();
			
			if(Functions.GoodData(cust.memo))
			{
				ReceivablesLedger.repo.Memo.Memo.TextValue = cust.memo;
			}
			
			if(Functions.GoodData(cust.toDoDate))
			{
				ReceivablesLedger.repo.Memo.ToDoDate.TextValue = cust.toDoDate;
			}
			
			if(Functions.GoodData(cust.displayCheckBox))
			{
				ReceivablesLedger.repo.Memo.DisplayInDBM.SetState(cust.displayCheckBox);
			}
			#endregion
			
			
			#region Import Tab		
			ReceivablesLedger.repo.ImportExport.Tab.Click();
			
			if(ReceivablesLedger.repo.ImportExport.ThisCustHasSage50Info.Exists())
			{
				if(Functions.GoodData(cust.hasSage50CheckBox))
				{
					ReceivablesLedger.repo.ImportExport.ThisCustHasSage50.SetState(cust.hasSage50CheckBox);
				}
				
				if(Functions.GoodData(cust.usesMyItemNumCheckBox))
				{
					ReceivablesLedger.repo.ImportExport.ThisCustUsesMyItemNum.SetState(cust.usesMyItemNumCheckBox);
				}
				
				if(cust.imports.Count !=0)
				{
					if(Functions.GoodData(cust.usesMyItemNumCheckBox) && cust.usesMyItemNumCheckBox == false)
					{
						// enter import container here
						
                        int nLine = 1;
                        int i = 1;
						
                        // Restore containter to default
                        ReceivablesLedger.repo.Self.Activate();
                        //ReceivablesLedger.repo.RestoreWindow.Select();
						
                        // Set the first cell X position for SilkTest to click (try not to click over text in the cell, so as far right in the cell as possible)
                        ReceivablesLedger.repo.ImportExport.ImportContainer.ClickFirstCell();
						
                        while (true)
                        {
                            ReceivablesLedger.repo.ImportExport.ImportContainer.SetToLine(nLine);

                            i = nLine - 1;

                            // Do customer's item number
                            if (Functions.GoodData (cust.imports[i].itemNumber))
                            {
                                ReceivablesLedger.repo.ImportExport.ImportContainer.SetText (cust.imports[i].itemNumber) ;
                            }
							
                            // Can only enter either item number or account number
                            if (Functions.GoodData (cust.imports[i].myItemNumber) && (cust.imports[i].myItemNumber != ""))
                            {
								
                                // Do my item number field
                                ReceivablesLedger.repo.ImportExport.ImportContainer.MoveRight();
                                ReceivablesLedger.repo.ImportExport.ImportContainer.SetText (cust.imports[i].myItemNumber);
                            }
                            else
                            {
                                // Do my account number
								
                                // Move to the my account number field
                                ReceivablesLedger.repo.ImportExport.ImportContainer.MoveRight();
								
                                // If adding then we need to tab over again
                                if (!bEdit)
                                {
                                    ReceivablesLedger.repo.ImportExport.ImportContainer.MoveRight();
                                }
								
                                if (Functions.GoodData (cust.imports[i].myAccount) && Functions.GoodData(cust.imports[i].myAccount.acctNumber))
                                {
                                    ReceivablesLedger.repo.ImportExport.ImportContainer.SetText (cust.imports[i].myAccount.acctNumber);
                                }
                            }
												
                            nLine++;
                            if (nLine > cust.imports.Count)
                            {
                                break;
                            }
                        }			
					}
				}
			}
			
			#endregion
			
			
			
			#region Additional Info
			
			ReceivablesLedger.repo.AdditionalInfo.Tab.Click();
						
			if(Functions.GoodData(cust.additional1))
			{
				ReceivablesLedger.repo.AdditionalInfo.Additional1.TextValue = cust.additional1;
			}
			
			if(Functions.GoodData(cust.additional2))
			{
				ReceivablesLedger.repo.AdditionalInfo.Additional2.TextValue = cust.additional2;
			}
			
			if(Functions.GoodData(cust.additional3))
			{
				ReceivablesLedger.repo.AdditionalInfo.Additional3.TextValue = cust.additional3;
			}
						
			if(Functions.GoodData(cust.additional4))
			{
				ReceivablesLedger.repo.AdditionalInfo.Additional4.TextValue = cust.additional4;
			}
			
			if(Functions.GoodData(cust.additional5))
			{
				ReceivablesLedger.repo.AdditionalInfo.Additional5.TextValue = cust.additional5;
			}
			
			if(Functions.GoodData(cust.addCheckBox1))
			{
				ReceivablesLedger.repo.AdditionalInfo.AddCheckBox1.SetState(cust.addCheckBox1);
			}
			
			if(Functions.GoodData(cust.addCheckBox2))
			{
				ReceivablesLedger.repo.AdditionalInfo.AddCheckBox2.SetState(cust.addCheckBox2);
			}
						
			if(Functions.GoodData(cust.addCheckBox3))
			{
				ReceivablesLedger.repo.AdditionalInfo.AddCheckBox3.SetState(cust.addCheckBox3);
			}
						
			if(Functions.GoodData(cust.addCheckBox4))
			{
				ReceivablesLedger.repo.AdditionalInfo.AddCheckBox4.SetState(cust.addCheckBox4);
			}
									
			if(Functions.GoodData(cust.addCheckBox5))
			{
				ReceivablesLedger.repo.AdditionalInfo.AddCheckBox5.SetState(cust.addCheckBox5);
			}
			
			#endregion
			
			
			
			
		
			
			
			if(bSave)
			{
				ReceivablesLedger.repo.Save.Click();
			}
			
		}	
		public static void _SA_MatchDefaults(CUSTOMER cust)
		{
			if(!Functions.GoodData(cust.inactiveCheckBox))
			{
				cust.inactiveCheckBox = false;
			}
			
			if(!Functions.GoodData(cust.internalCheckBox))
			{
				cust.internalCheckBox = false;
			}
			
			if(!Functions.GoodData(cust.defaultShipToAddressCheckbox))
			{
				cust.defaultShipToAddressCheckbox = false;
			}
			
			if(!Functions.GoodData(cust.priceList))
			{
				cust.priceList = "Regular";
			}
			
			if(!Functions.GoodData(cust.currencyCode) && Variables.globalSettings.CompanySettings.CurrencySettings.allowForeignCurrency == true)
			{
				cust.currencyCode = Variables.globalSettings.CompanySettings.CurrencySettings.HomeCurrency.currencyCode;
			}
			
			
			if(!Functions.GoodData(cust.conductBusinessIn) && Variables.globalSettings.CompanySettings.FeatureSettings.Language == true)
			{
				cust.currencyCode = "English";
			}
			
			if(!Functions.GoodData(cust.discountPercent) && Functions.GoodData(Variables.globalSettings.ReceivableSettings.discountPercent))
			{
				cust.discountPercent = Variables.globalSettings.ReceivableSettings.discountPercent;
			}
			
			if(!Functions.GoodData(cust.discountPeriod) && Functions.GoodData(Variables.globalSettings.ReceivableSettings.discountDays))
			{
				cust.discountPeriod = Variables.globalSettings.ReceivableSettings.discountDays;
			}
			
			if(!Functions.GoodData(cust.termPeriod) && Functions.GoodData(Variables.globalSettings.ReceivableSettings.netDays))
			{
				cust.termPeriod = Variables.globalSettings.ReceivableSettings.netDays;
			}
			
			if(!Functions.GoodData(cust.produceStatementsForThisCustCheckbox))
			{
				cust.produceStatementsForThisCustCheckbox = true;
			}
			
			if(!Functions.GoodData(cust.formsForThisCustomer))
			{
				cust.formsForThisCustomer = "Print";
			}
			
			if(!Functions.GoodData(cust.synchronizeWithOutlook))
			{
				cust.synchronizeWithOutlook = false;
			}
			
			if(!Functions.GoodData(cust.taxCode))
			{
				cust.taxCode.description = "No Tax";
			}
						
			if(!Functions.GoodData(cust.displayCheckBox))
			{
				cust.displayCheckBox = false;
			}
			
			if(!Functions.GoodData(cust.hasSage50CheckBox))
			{
				cust.hasSage50CheckBox = false;
			}
			
			if(!Functions.GoodData(cust.usesMyItemNumCheckBox))
			{
				cust.usesMyItemNumCheckBox = false;
			}
					
			if(!Functions.GoodData(cust.addCheckBox1) && Functions.GoodData(Variables.globalSettings.ReceivableSettings.AdditionalFields.Field1))
			{
				cust.addCheckBox1 = false;
			}			
			
			if(!Functions.GoodData(cust.addCheckBox2) && Functions.GoodData(Variables.globalSettings.ReceivableSettings.AdditionalFields.Field2))
			{
				cust.addCheckBox2 = false;
			}	
						
			if(!Functions.GoodData(cust.addCheckBox3) && Functions.GoodData(Variables.globalSettings.ReceivableSettings.AdditionalFields.Field3))
			{
				cust.addCheckBox3 = false;
			}	
			
			if(!Functions.GoodData(cust.addCheckBox4) && Functions.GoodData(Variables.globalSettings.ReceivableSettings.AdditionalFields.Field4))
			{
				cust.addCheckBox4 = false;
			}	
			
			if(!Functions.GoodData(cust.addCheckBox5) && Functions.GoodData(Variables.globalSettings.ReceivableSettings.AdditionalFields.Field5))
			{
				cust.addCheckBox5 = false;
			}	
				
				
			if(!Functions.GoodData(cust.synchronizeWithOutlook))
			{
				cust.synchronizeWithOutlook = false;
			}
		}
		
		public static CUSTOMER _SA_Read()
		{
			return ReceivablesLedger._SA_Read(null);
		}
		public static CUSTOMER _SA_Read(string sIdToRead)
		{
			CUSTOMER cust = new CUSTOMER();
			
			if(!ReceivablesLedger.repo.SelfInfo.Exists())
			{
				ReceivablesLedger._SA_Invoke();
			}
				
			
			if(Functions.GoodData(sIdToRead))
			{
				
				cust.name = sIdToRead;
				
				if(ReceivablesLedger.repo.SelectRecord.SelectedItemText != cust.name)
				{
					ReceivablesLedger._SA_Open(cust);	
				}
			}
			
			cust.name = ReceivablesLedger.repo.SelectRecord.SelectedItemText;
			cust.nameEdit = ReceivablesLedger.repo.CustomerName.TextValue;
			cust.inactiveCheckBox = ReceivablesLedger.repo.InactiveCustomer.Checked;
			
			if(ReceivablesLedger.repo.InternalCustomerInfo.Exists())
			{
				cust.internalCheckBox = ReceivablesLedger.repo.InternalCustomer.Checked;
			}
			else
			{
				cust.internalCheckBox = false;
			}
			
			cust.balanceOwingInHome = ReceivablesLedger.repo.BalanceOwingInHome.TextValue;
			
			if(ReceivablesLedger.repo.BalanceOwingInForeignInfo.Exists())
			{
				cust.balanceOwingInForeign = ReceivablesLedger.repo.BalanceOwingInForeign.TextValue;
			}
			cust.dateOfLastSale = ReceivablesLedger.repo.DateOfLastSale.TextValue;
			
			#region Address Tab
			ReceivablesLedger.repo.Address.Tab.Click();
			cust.Address.contact = ReceivablesLedger.repo.Address.Contact.TextValue;
			cust.Address.street1 = ReceivablesLedger.repo.Address.Street1.TextValue;
			cust.Address.street2 = ReceivablesLedger.repo.Address.Street2.TextValue;
			cust.Address.city = ReceivablesLedger.repo.Address.City.TextValue;
			cust.Address.province = ReceivablesLedger.repo.Address.Province.TextValue;
			cust.Address.postalCode = ReceivablesLedger.repo.Address.Postalcode.TextValue;
			cust.Address.country = ReceivablesLedger.repo.Address.Country.TextValue;
			cust.Address.phone1 = ReceivablesLedger.repo.Address.Phone1.TextValue;
			cust.Address.phone2= ReceivablesLedger.repo.Address.Phone2.TextValue;
			cust.Address.fax = ReceivablesLedger.repo.Address.Fax.TextValue;
			cust.Address.email = ReceivablesLedger.repo.Address.Email.TextValue;
			cust.Address.webSite = ReceivablesLedger.repo.Address.Website.TextValue;
			if(ReceivablesLedger.repo.Address.DepartmentInfo.Exists())
			{
				cust.department = ReceivablesLedger.repo.Address.Department.SelectedItemText;
			}
			cust.salesPerson = ReceivablesLedger.repo.Address.Salesperson.SelectedItemText;
			cust.customerSince = ReceivablesLedger.repo.Address.CustomerSince.TextValue;
			#endregion
			
			
			#region Shipping Address Tab
			ReceivablesLedger.repo.ShipToAddress.Tab.Click();
			cust.defaultShipToAddressCheckbox = ReceivablesLedger.repo.ShipToAddress.DefaultShipTo.Checked;
			cust.ShipToAddress.contact = ReceivablesLedger.repo.ShipToAddress.Contact.TextValue;
			cust.ShipToAddress.street1 = ReceivablesLedger.repo.ShipToAddress.Street1.TextValue;
			cust.ShipToAddress.street2 = ReceivablesLedger.repo.ShipToAddress.Street2.TextValue;
			cust.ShipToAddress.city = ReceivablesLedger.repo.ShipToAddress.City.TextValue;
			cust.ShipToAddress.province = ReceivablesLedger.repo.ShipToAddress.Province.TextValue;
			cust.ShipToAddress.postalCode = ReceivablesLedger.repo.ShipToAddress.Postalcode.TextValue;
			cust.ShipToAddress.country = ReceivablesLedger.repo.ShipToAddress.Country.TextValue;
			if(ReceivablesLedger.repo.ShipToAddress.Phone1Info.Exists())
			{
				cust.ShipToAddress.phone1 = ReceivablesLedger.repo.ShipToAddress.Phone1.TextValue;
			}
			if(ReceivablesLedger.repo.ShipToAddress.Phone2Info.Exists())
			{
				cust.ShipToAddress.phone2 = ReceivablesLedger.repo.ShipToAddress.Phone2.TextValue;
			}
			if(ReceivablesLedger.repo.ShipToAddress.FaxInfo.Exists())
			{
				cust.ShipToAddress.fax = ReceivablesLedger.repo.ShipToAddress.Fax.TextValue;
			}
			if(ReceivablesLedger.repo.ShipToAddress.EmailInfo.Exists())
			{
				cust.ShipToAddress.email = ReceivablesLedger.repo.ShipToAddress.Email.TextValue;
			}
			#endregion
			
			#region Options Tab
			ReceivablesLedger.repo.Options.Tab.Click();
			cust.revenueAccount.acctNumber = ReceivablesLedger.repo.Options.RevenueAccount.SelectedItemText;
			if(ReceivablesLedger.repo.Options.CurrencyInfo.Exists())
			{
				cust.currencyCode = ReceivablesLedger.repo.Options.Currency.SelectedItemText;
			}
			cust.priceList = ReceivablesLedger.repo.Options.PriceList.SelectedItemText;
			cust.conductBusinessIn = ReceivablesLedger.repo.Options.ConductBusinessIn.SelectedItemText;
			if(ReceivablesLedger.repo.Options.UsuallyShipItemFromInfo.Exists())
			{
				cust.usuallyShipItemFrom = ReceivablesLedger.repo.Options.UsuallyShipItemFrom.SelectedItemText;
			}
			if(ReceivablesLedger.repo.Options.StandardDiscountInfo.Exists())
			{
				cust.standardDiscount = ReceivablesLedger.repo.Options.StandardDiscount.TextValue;
			}
			cust.discountPercent = ReceivablesLedger.repo.Options.DiscountPercent.TextValue;
			cust.discountPeriod = ReceivablesLedger.repo.Options.DiscountPeriod.TextValue;
			cust.termPeriod = ReceivablesLedger.repo.Options.TermPeriod.TextValue;
			cust.produceStatementsForThisCustCheckbox = ReceivablesLedger.repo.Options.ProduceStatementsForThisCustomer.Checked;
			cust.formsForThisCustomer = ReceivablesLedger.repo.Options.FormsForThisCustomer.SelectedItemText;
			if(ReceivablesLedger.repo.Options.SyncWithOutlookInfo.Exists())
			{
				cust.synchronizeWithOutlook = ReceivablesLedger.repo.Options.SyncWithOutlook.Checked;
			}
			
			#endregion
			
			
			#region Taxes Tab
			ReceivablesLedger.repo.Taxes.Tab.Click();
			
			// get container contents here
			
		 	
			cust.taxCode.code = Functions.GetField(ReceivablesLedger.repo.Taxes.TaxCode.SelectedItemText," - ",1);
			
			// if first item of no tax, assign proper string of no tax
			if(cust.taxCode.code == "")
			{
				cust.taxCode.code = Variables.sNoTax;
			}
		
			ReceivablesLedger.repo.Self.Activate();
			// restore window here
			
			
			// get container contents here
            foreach (List<string> taxLine in ReceivablesLedger.repo.Taxes.TaxContainer.GetContents())
            {    
            	TAX_LEDGER TA = new TAX_LEDGER();
                TAX TX = new TAX ();
                TX.taxID = taxLine[0];
                TA.tax = TX;
                TA.taxExempt = taxLine[1];
                TA.taxID = taxLine[2];
				
                cust.taxList.Add(TA);
            }						
			
			#endregion
			
		
			#region PAD Tab
			ReceivablesLedger.repo.PAD.Tab.Click();
			cust.custHasPADCheckbox = ReceivablesLedger.repo.PAD.CustHasPAD.Checked;
			cust.currencyAndLocation = ReceivablesLedger.repo.PAD.CurrencyAndLocation.SelectedItemText;
			cust.branchNumber = ReceivablesLedger.repo.PAD.BranchNumber.TextValue;
			cust.institutionNumber = ReceivablesLedger.repo.PAD.InstitutionNumber.TextValue;
			cust.accountNumber = ReceivablesLedger.repo.PAD.AccountNumber.TextValue;
			if(ReceivablesLedger.repo.PAD.EftRefCheckBoxInfo.Exists())
			{
				cust.eftReferenceCheckbox = ReceivablesLedger.repo.PAD.EftRefCheckBox.Checked;
				cust.eftReference = ReceivablesLedger.repo.PAD.EftRerence.TextValue;
				cust.eftTransCodeCheckbox = ReceivablesLedger.repo.PAD.TransCodeCheckBox.Checked;
				cust.eftTransCode = ReceivablesLedger.repo.PAD.TransCode.TextValue;
			}
			#endregion
			
			#region Statistics Tab
			ReceivablesLedger.repo.Statistics.Tab.Click();
			cust.ytdSales = ReceivablesLedger.repo.Statistics.YTDSales.TextValue;
			cust.lySales = ReceivablesLedger.repo.Statistics.LYSales.TextValue;
			cust.creditLimit = ReceivablesLedger.repo.Statistics.CreditLimit.TextValue;
			
			if(ReceivablesLedger.repo.Statistics.ForeignYTDSalesInfo.Exists())
			{
				cust.foreignYtdSales = ReceivablesLedger.repo.Statistics.ForeignYTDSales.TextValue;
				cust.foreignLySales = ReceivablesLedger.repo.Statistics.ForeighLYSales.TextValue;
				cust.foreignCreditLimit = ReceivablesLedger.repo.Statistics.ForeignCreditLimit.TextValue;
			}
			
			
			#endregion
			
			#region Memo Tab
			ReceivablesLedger.repo.Memo.Tab.Click();
			cust.memo = ReceivablesLedger.repo.Memo.Memo.TextValue;
			cust.toDoDate = ReceivablesLedger.repo.Memo.ToDoDate.TextValue;
			cust.displayCheckBox = ReceivablesLedger.repo.Memo.DisplayInDBM.Checked;
			
			#endregion
			
			#region Import/Export Tab
			ReceivablesLedger.repo.ImportExport.Tab.Click();
			if(ReceivablesLedger.repo.ImportExport.ThisCustHasSage50Info.Exists())
			{
				cust.hasSage50CheckBox = ReceivablesLedger.repo.ImportExport.ThisCustHasSage50.Checked;
				cust.usesMyItemNumCheckBox = ReceivablesLedger.repo.ImportExport.ThisCustUsesMyItemNum.Checked;
				if(cust.usesMyItemNumCheckBox == false)
				{
					// add container extract here
					
					
					string myNumOrAccount;
                    string tempText;


                    // Restore containter to default
                    ReceivablesLedger.repo.Self.Activate();
                    //ReceivablesLedger.repo.RestoreWindow.Select();

         
                    foreach (List<string> importLine in ReceivablesLedger.repo.ImportExport.ImportContainer.GetContents())
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

                        cust.imports.Add(imp);
                    }				
				}
			}
			else
			{
				cust.hasSage50CheckBox = false;
				cust.usesMyItemNumCheckBox = false;
			}
			
			#endregion
			
			#region Additional Tab
			ReceivablesLedger.repo.AdditionalInfo.Tab.Click();
			if(ReceivablesLedger.repo.AdditionalInfo.Additional1Info.Exists())
			{
				cust.additional1 = ReceivablesLedger.repo.AdditionalInfo.Additional1.TextValue;
				cust.addCheckBox1 = ReceivablesLedger.repo.AdditionalInfo.AddCheckBox1.Checked;
			}
			if(ReceivablesLedger.repo.AdditionalInfo.Additional2Info.Exists())
			{
				cust.additional2 = ReceivablesLedger.repo.AdditionalInfo.Additional2.TextValue;
				cust.addCheckBox2 = ReceivablesLedger.repo.AdditionalInfo.AddCheckBox2.Checked;
			}
			if(ReceivablesLedger.repo.AdditionalInfo.Additional3Info.Exists())
			{
				cust.additional3 = ReceivablesLedger.repo.AdditionalInfo.Additional3.TextValue;
				cust.addCheckBox3 = ReceivablesLedger.repo.AdditionalInfo.AddCheckBox3.Checked;
			}
			if(ReceivablesLedger.repo.AdditionalInfo.Additional4Info.Exists())
			{
				cust.additional4 = ReceivablesLedger.repo.AdditionalInfo.Additional4.TextValue;
				cust.addCheckBox4 = ReceivablesLedger.repo.AdditionalInfo.AddCheckBox4.Checked;
			}
			if(ReceivablesLedger.repo.AdditionalInfo.Additional5Info.Exists())
			{
				cust.additional5 = ReceivablesLedger.repo.AdditionalInfo.Additional5.TextValue;
				cust.addCheckBox5 = ReceivablesLedger.repo.AdditionalInfo.AddCheckBox5.Checked;
			}
			
			#endregion
			
			
			return cust;
			
		}	
		public static void _SA_Close()
		{
			repo.Self.Close();
	
		}
		public static void _SA_Delete(CUSTOMER cust)
		{
			
		}
		public static void _SA_History(CUSTOMER cust)
		{
			
		}
		public static void DataFile_ReadFile(string sDataLocation, string fileCounter)
		{
			string EXTENSION_HEADER = ".hdr";
            string EXTENSION_SHIP_TO_ADDRESS = ".dt1";
            string EXTENSION_OPTIONS = ".dt2";
            string EXTENSION_TAXES = ".dt3";
            string EXTENSION_STATISTICS = ".dt4";
            string EXTENSION_MEMO = ".dt5";
            string EXTENSION_IMPORT_EXPORT = ".dt8";
            string EXTENSION_ADDITIONAL_INFO = ".dt6";
            string EXTENSION_HISTORY = ".dt7";
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
            StreamReader FileDT8;	// File handle for the import/export tab info
            StreamReader FileDT6;	// File handle for the Additional Info tab info
            StreamReader FileDT7;	// File handle for the history tab info
            StreamReader FileCNT;	// File handle for the tax container Info
            StreamReader FileCN2;	// File handle for the import container file
           
            
            // Get the data path from file
            dataPath = sDataLocation + "CL" + fileCounter;


            List<CUSTOMER> lCusts = new List<CUSTOMER>() { };

            // Open all data files and set data structure, if they exist, if not then flag not to do the info for the missing file
            // Open header file
            using (FileHDR = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_HEADER, "FM_READ")))
            {
                // While not EOF read the header file
                while ((dataLine = FileHDR.ReadLine()) != null)
                {
                    CUSTOMER CustomerRecord = new CUSTOMER();
                    // Set header info
                    CustomerRecord = ReceivablesLedger.DataFile_setDataStructure(EXTENSION_HEADER, dataLine, CustomerRecord);

                    // Open DT1 file and set structure
                    if (File.Exists(dataPath + EXTENSION_SHIP_TO_ADDRESS))
                    {
                        using (FileDT1 = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_SHIP_TO_ADDRESS, "FM_READ")))
                        {
                            while ((dataLine = FileDT1.ReadLine()) != null)
                            {
                                CustomerRecord = ReceivablesLedger.DataFile_setDataStructure(EXTENSION_SHIP_TO_ADDRESS, dataLine, CustomerRecord);
                            }
                            FileDT1.Close();
                        }
                    }
                    // Open DT2 file and set structure
                    if (File.Exists(dataPath + EXTENSION_OPTIONS))
                    {
                        using (FileDT2 = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_OPTIONS, "FM_READ")))
                        {
                            while ((dataLine = FileDT2.ReadLine()) != null)
                            {
                                CustomerRecord = ReceivablesLedger.DataFile_setDataStructure(EXTENSION_OPTIONS, dataLine, CustomerRecord);
                            }
                            FileDT2.Close();
                        }
                    }

                    // Open DT3 file and set structure
                    if (File.Exists(dataPath + EXTENSION_TAXES))
                    {
                        using(FileDT3 = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_TAXES, "FM_READ")))
                        {
                            while ((dataLine = FileDT3.ReadLine()) != null)
                            {
                                CustomerRecord = ReceivablesLedger.DataFile_setDataStructure(EXTENSION_TAXES, dataLine, CustomerRecord);
                            }
                            FileDT3.Close();
                        }
                    }

                    // Open DT4 file and set structure
                    if (File.Exists(dataPath + EXTENSION_STATISTICS))
                    {
                        using (FileDT4 = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_STATISTICS, "FM_READ")))
                        {
                            while ((dataLine = FileDT4.ReadLine()) != null)
                            {
                                CustomerRecord = ReceivablesLedger.DataFile_setDataStructure(EXTENSION_STATISTICS, dataLine, CustomerRecord);
                            }
                            FileDT4.Close();
                        }
                    }

                    // Open DT5 file and set structure
                    if (File.Exists(dataPath + EXTENSION_MEMO))
                    {
                        using(FileDT5 = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_MEMO, "FM_READ")))
                        {
                            while ((dataLine = FileDT5.ReadLine()) != null)
                            {
                                CustomerRecord = ReceivablesLedger.DataFile_setDataStructure(EXTENSION_MEMO, dataLine, CustomerRecord);
                            }
                            FileDT5.Close();
                        }
                    }

                    // Open DT8 file and set structure
                    if (File.Exists(dataPath + EXTENSION_IMPORT_EXPORT))
                    {
                        using(FileDT8 = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_IMPORT_EXPORT, "FM_READ")))
                        {
                            while ((dataLine = FileDT8.ReadLine()) != null)
                            {
                                CustomerRecord = ReceivablesLedger.DataFile_setDataStructure(EXTENSION_IMPORT_EXPORT, dataLine, CustomerRecord);
                            }
                            FileDT8.Close();
                        }
                    }

                    // Open DT6 file and set structure
                    if (File.Exists(dataPath + EXTENSION_ADDITIONAL_INFO))
                    {
                        using (FileDT6 = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_ADDITIONAL_INFO, "FM_READ")))
                        {
                            while ((dataLine = FileDT6.ReadLine()) != null)
                            {
                                CustomerRecord = ReceivablesLedger.DataFile_setDataStructure(EXTENSION_ADDITIONAL_INFO, dataLine, CustomerRecord);
                            }
                            FileDT6.Close();
                        }
                    }

                    // Only open history detail file if it exists
                    if (File.Exists(dataPath + EXTENSION_HISTORY))
                    {
                        using (FileDT7 = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_HISTORY, "FM_READ")))
                        {
                            while ((dataLine = FileDT7.ReadLine()) != null)
                            {
                                CustomerRecord = ReceivablesLedger.DataFile_setDataStructure(EXTENSION_HISTORY, dataLine, CustomerRecord);
                            }
                            FileDT7.Close();
                        }
                    }

                    // Open and set tax container file
                    if (File.Exists(dataPath + EXTENSION_TAX_TABLE))
                    {
                        using(FileCNT = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_TAX_TABLE, "FM_READ")))
                        {
                            while ((dataLine = FileCNT.ReadLine()) != null)
                            {
                                CustomerRecord = ReceivablesLedger.DataFile_setDataStructure(EXTENSION_TAX_TABLE, dataLine, CustomerRecord);
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
                                CustomerRecord = ReceivablesLedger.DataFile_setDataStructure(EXTENSION_IMPORT_TABLE, dataLine, CustomerRecord);
                            }
                            FileCN2.Close();
                        }
                    }

                    lCusts.Add(CustomerRecord);
                }
                FileHDR.Close();
            }

            for (int x = 0; x < lCusts.Count; x++)
            {
                CUSTOMER CustomerRecord = lCusts[x];
                // Determine the mode
                switch (CustomerRecord.action)
                {
                    case "A":
                        ReceivablesLedger._SA_Create(CustomerRecord);

                        break;
                    case "E":
                        ReceivablesLedger._SA_Create(CustomerRecord, true, true);

                        break;
                    case "D":
                        ReceivablesLedger._SA_Delete(CustomerRecord);

                        break;
                    case "H":
                        ReceivablesLedger._SA_History(CustomerRecord);

                        break;
                    default:
                        {
                            Functions.Verify(false, true, "Action set properly");
                            //ReceivablesLedger.ClickUndoChanges();
                            break;
                        }
                }
                ReceivablesLedger._SA_Close();
            }
		}
		public static CUSTOMER DataFile_setDataStructure(string extension, string dataLine, CUSTOMER cust)
		{
			CUSTOMER CustomerRecord = cust;
			
            switch (extension.ToUpper())
            {
                case ".HDR":
                    CustomerRecord.action = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 1));
                    CustomerRecord.name = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 2));
                    CustomerRecord.inactiveCheckBox = ConvertFunctions.StringToBool(Functions.GetField (dataLine, ",", 3));
                    CustomerRecord.internalCheckBox = ConvertFunctions.StringToBool(Functions.GetField(dataLine, ",", 4));
                    CustomerRecord.Address.contact = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 5));
                    CustomerRecord.Address.street1 = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 6));
                    CustomerRecord.Address.street2 = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 7));
                    CustomerRecord.Address.city = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 8));
                    CustomerRecord.Address.province = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 9));
                    CustomerRecord.Address.postalCode = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 10));
                    CustomerRecord.Address.country = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 11));
                    CustomerRecord.Address.phone1 = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 12));
                    CustomerRecord.Address.phone2 = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 13));
                    CustomerRecord.Address.fax = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 14));
                    CustomerRecord.Address.email = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 15));
                    CustomerRecord.Address.webSite = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 16));
                    CustomerRecord.department = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 17));
                    CustomerRecord.customerSince = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 18));
                    CustomerRecord.nameEdit = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 19));
                    CustomerRecord.salesPerson = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 20));
				
                    break;
                case ".DT1":
                    if (CustomerRecord.name == Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 1)))
                    {
                        CustomerRecord.defaultShipToAddressCheckbox = ConvertFunctions.StringToBool(Functions.GetField (dataLine, ",", 2));
                        CustomerRecord.ShipToAddress.contact = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 3));
                        CustomerRecord.ShipToAddress.street1 = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 4));
                        CustomerRecord.ShipToAddress.street2 = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 5));
                        CustomerRecord.ShipToAddress.city = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 6));
                        CustomerRecord.ShipToAddress.province = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 7));
                        CustomerRecord.ShipToAddress.postalCode = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 8));
                        CustomerRecord.ShipToAddress.country = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 9));
                        CustomerRecord.ShipToAddress.phone1 = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 10));
                        CustomerRecord.ShipToAddress.phone2 = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 11));
                        CustomerRecord.ShipToAddress.fax = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 12));
                        CustomerRecord.ShipToAddress.email = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 13));
                    }
				
                    break;
                case ".DT2":
                    if (CustomerRecord.name == Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 1)))
                    {
                        CustomerRecord.discountPercent = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 2));
                        CustomerRecord.discountPeriod = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 3));
                        CustomerRecord.termPeriod = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 4));
                        CustomerRecord.produceStatementsForThisCustCheckbox = ConvertFunctions.StringToBool(Functions.GetField (dataLine, ",", 5));
                        CustomerRecord.formsForThisCustomer = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 6));
                        CustomerRecord.currencyCode = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 7));
                        CustomerRecord.conductBusinessIn = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 8));
                        CustomerRecord.revenueAccount.acctNumber = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 9));
                        CustomerRecord.priceList = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 10));
                        CustomerRecord.synchronizeWithOutlook = ConvertFunctions.StringToBool(Functions.GetField (dataLine, ",", 11));
                        CustomerRecord.usuallyShipItemFrom = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 12));
                        CustomerRecord.standardDiscount = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 13));
                    }
				
                    break;
                case ".DT3":
                    if (CustomerRecord.name == Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 1)))
                    {
						CustomerRecord.taxCode.code = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 2));
                    }
				
                    break;
                case ".DT4":
                    if (CustomerRecord.name == Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 1)))
                    {
                        CustomerRecord.ytdSales = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 2));
                        CustomerRecord.lySales = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 3));
                        CustomerRecord.creditLimit = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 4));
                        CustomerRecord.foreignYtdSales = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 5));
                        CustomerRecord.foreignLySales = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 6));
                        CustomerRecord.foreignCreditLimit = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 7));
                    }
				
                    break;
                case ".DT5":
                    if (CustomerRecord.name == Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 1)))
                    {
                        CustomerRecord.memo = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 2));
                        CustomerRecord.toDoDate = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 3));
                        CustomerRecord.displayCheckBox = ConvertFunctions.StringToBool(Functions.GetField(dataLine, ",", 4));
                    }
				
                    break;
                case ".DT8":
                    if (CustomerRecord.name == Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 1)))
                    {
                        CustomerRecord.hasSage50CheckBox = ConvertFunctions.StringToBool(Functions.GetField (dataLine, ",", 2));
                        CustomerRecord.usesMyItemNumCheckBox = ConvertFunctions.StringToBool(Functions.GetField (dataLine, ",", 3));
                    }
				
                    break;
                case ".DT6":
                    if (CustomerRecord.name == Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 1)))
                    {
                        CustomerRecord.additional1= Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 2));
                        CustomerRecord.additional2 = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 3));
                        CustomerRecord.additional3 = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 4));
                        CustomerRecord.additional4 = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 5));
                        CustomerRecord.additional5 = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 6));
                        CustomerRecord.addCheckBox1 = ConvertFunctions.StringToBool(Functions.GetField (dataLine, ",", 7));
                        CustomerRecord.addCheckBox2 = ConvertFunctions.StringToBool(Functions.GetField (dataLine, ",", 8));
                        CustomerRecord.addCheckBox3 = ConvertFunctions.StringToBool(Functions.GetField (dataLine, ",", 9));
                        CustomerRecord.addCheckBox4 = ConvertFunctions.StringToBool(Functions.GetField (dataLine, ",", 10));
                        CustomerRecord.addCheckBox5 = ConvertFunctions.StringToBool(Functions.GetField (dataLine, ",", 11));
                    }
				
                    break;
                case ".DT7":
                    if (CustomerRecord.name == Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 1)))
                    {
                        if (Functions.GetField(dataLine, "", 2) == "i")
                        {
                            CustomerRecord.recordType = LEDGER_HISTORY.HIST_INVOICE;
                            CustomerRecord.historicalInvoice.invoiceNumber = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 3));
                            CustomerRecord.historicalInvoice.transDate = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 4));
                            CustomerRecord.historicalInvoice.termsPercent = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 5));
                            CustomerRecord.historicalInvoice.termsDays = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 6));
                            CustomerRecord.historicalInvoice.termsNetDays = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 7));
                            CustomerRecord.historicalInvoice.amount = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 8));
                            CustomerRecord.historicalInvoice.tax = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 9));
                            CustomerRecord.historicalInvoice.exchangeRate = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 10));
                            CustomerRecord.historicalInvoice.homeAmount = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 11));
                        }
                        else
                        {
                            CustomerRecord.recordType = LEDGER_HISTORY.HIST_PAYMENT;
                            CustomerRecord.historicalPayment.paymentNumber = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 3));
                            CustomerRecord.historicalPayment.transDate = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 4));
                            HISTORICAL_PAYMENT_ROW row = new HISTORICAL_PAYMENT_ROW ();
                            row.discountTaken = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 5));
                            row.amountPaid = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 6));
                            CustomerRecord.historicalPayment.PayGridRows.Add(row);
                            CustomerRecord.historicalPayment.exchangeRate = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 7));
                        }
                    }
				
                    break;
                case ".CNT":
                    if (CustomerRecord.name == Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 1)))
                    {
                        TAX_LEDGER TA = new TAX_LEDGER ();
                        TAX TX = new TAX();
                        TX.taxID = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 2));
                        TA.tax = TX;	// PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 2))
                        TA.taxExempt = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 3));
                        TA.taxID = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 4));
                        if (Functions.GoodData (CustomerRecord.taxList))
                        {
                            CustomerRecord.taxList.Add(TA);
                        }
                        else
                        {
                            CustomerRecord.taxList = new List<TAX_LEDGER>() {TA};
                        }
                    }
				
                    break;
                case ".CNT2":
                    if (CustomerRecord.name == Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 1)))
                    {
                        IMPORT IMP = new IMPORT();
                        IMP.itemNumber = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 2));
                        IMP.myItemNumber = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 3));
                        IMP.myAccount.acctNumber = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 4));
                        if (CustomerRecord.imports.Count !=0)
                        {
                            CustomerRecord.imports.Add(IMP);
                        }
                        else
                        {
                            CustomerRecord.imports = new List<IMPORT>{IMP};
                        }
                    }
				
                    break;
                default:
                {
                    Functions.Verify(false, true, "Valid extension used");
                    break;
                }
            }
            return (CustomerRecord);
		}
	}

	public static class CustomerIcon
	{
		public static ReceivablesLedgerResFolders.ReceivablesIconAppFolder repo = ReceivablesLedgerRes.Instance.ReceivablesIcon;
	}
}