/*
 * Created by Ranorex
 * User: wonda05
 * Date: 5/29/2017
 * Time: 9:43 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Threading;
using WinForms = System.Windows.Forms;

using Ranorex;
using Ranorex.Core;
using Ranorex.Core.Testing;
using Sage50.Classes;
using Sage50.Types;
using Sage50.Shared;

namespace Sage50.BVT
{
    /// <summary>
    /// Description of CreateNewCompany.
    /// </summary>
    [TestModule("C983C80E-B6C1-4A9A-A4AD-357E44B53F83", ModuleType.UserCode, 1)]
    public class CreateNewCompany : ITestModule
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public CreateNewCompany()
        {
            // Do not delete - a parameterless constructor is required!
        }

        /// <summary>
        /// Performs the playback of actions in this module.
        /// </summary>
        /// <remarks>You should not call this method directly, instead pass the module
        /// instance to the <see cref="TestModuleRunner.Run(ITestModule)"/> method
        /// that will in turn invoke this method.</remarks>
        void ITestModule.Run()
        {
            Mouse.DefaultMoveTime = 300;
            Keyboard.DefaultKeyPressTime = 100;
            Delay.SpeedFactor = 1.0;
            
            string sCompanyPath = string.Format( @"C:\Users\Public\Documents\Simply Accounting\{0}\Data", Variables.sSimplyVersionNumber);
			            
            COMPANY aComp = new COMPANY();
            aComp.companyInformation.companyName = "New" + StringFunctions.RandStr("9(8)");
//   			aComp.edition = EDITION.ENTERPRISE_EDITION;
//   			aComp.companyInformation.Address.street1 = "100 Mobile Road";
//  	 		aComp.companyInformation.Address.street2 = "Hollywood";
//   			aComp.companyInformation.Address.city = "Richmond";
//   			aComp.companyInformation.Address.provinceCode = "BC";
//   			aComp.companyInformation.Address.province = "British Columbia";   
//   			aComp.companyInformation.Address.country = "Canada";
   			aComp.companyFileLocation = sCompanyPath;
      
   			NewCompanyWizard._SA_Create(aComp);
   			   			
   			// Set peferences
			UserPreferences._SA_setUserPreferences();
			Settings._SA_SetToGenericValues();
			System.Threading.Thread.Sleep(5000);
	
			// Settings
			Variables.globalSettings.InventorySettings.allowInventoryLevelsToGoBelowZero = true;
			Settings._SA_SetInventoryOptionSettings();
			Variables.globalSettings.ReceivableSettings.calculateLineItemDiscounts = false;
			Variables.globalSettings.ReceivableSettings.interestCharges = true;
			System.Threading.Thread.Sleep(5000);
			Settings._SA_SetReceivablesOptionsAndDiscountSettings();
			
			System.Threading.Thread.Sleep(5000);
			
			// turn on orders/quotes
			Variables.globalSettings.CompanySettings.FeatureSettings.ordersForCustomers = true;
			Variables.globalSettings.CompanySettings.FeatureSettings.ordersForVendors = true;
			Variables.globalSettings.CompanySettings.FeatureSettings.quotesForCustomers = true;
			Variables.globalSettings.CompanySettings.FeatureSettings.quotesForVendors = true;
			Settings._SA_SetCompanyFeatureSettings();
			 
			// set currencies
			Variables.globalSettings.CompanySettings.CurrencySettings.allowForeignCurrency = true;
			Variables.globalSettings.CompanySettings.CurrencySettings.HomeCurrency.roundingDifferencesAccount = "5650 Currency Exchange & Rounding";
			
			// have to explicitly stat 2 decimals cause it thinks data is good when in class
			Variables.globalSettings.CompanySettings.CurrencySettings.HomeCurrency.decimalPlaces = CURRENCY_DECIMAL.CURRENCY_DECIMAL_2;
			
			CURRENCY_DATA newCurr = new CURRENCY_DATA();
			
			newCurr.Currency = "United States Dollars";
			newCurr.decimalPlaces = CURRENCY_DECIMAL.CURRENCY_DECIMAL_2;
			Variables.globalSettings.CompanySettings.CurrencySettings.ForeignCurrencies.Add(newCurr);
			Settings._SA_SetCompanyCurrencySettings();           
        }
    }
}
