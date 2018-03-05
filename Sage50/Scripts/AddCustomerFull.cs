/*
 * Created by Ranorex
 * User: wonda05
 * Date: 4/29/2016
 * Time: 12:10 PM
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


using System.Windows.Forms;

namespace Sage50.BVT
{
    /// <summary>
    /// Description of UserCodeModule1.
    /// </summary>
    [TestModule("1ABADA37-2360-4F75-8EED-828E0E23DE00", ModuleType.UserCode, 1)]
    public class AddCustomerFull : ITestModule
    {
    	
    	
//    	string _varCustomer = "hello";
//    	[TestVariable("D0C7B7DD-3067-42CC-BAF3-6DB19B2D6CDC")]
//    	public string varCustomer
//    	{
//    		get { return _varCustomer; }
//    		set { _varCustomer = value; }
//    	}
    	
    	
        /// <summary>
        /// Constructs a new repo.
        /// </summary>
        public AddCustomerFull()
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
            
            // create record to hold customer data            
            CUSTOMER cus = new CUSTOMER();
            cus.name = "cust" + StringFunctions.RandStr("X(8)");
            cus.inactiveCheckBox = true;
            cus.internalCheckBox = true;
            
            // address
        	cus.Address.contact = "contact";
            cus.Address.street1 = "street 1";
            cus.Address.street2 = "street 2";
            cus.Address.city = "city";
            cus.Address.province = "province";
            cus.Address.postalCode = "V4F 16Y";
            cus.Address.country = "country";
            cus.Address.phone1 = "1234567894";
            cus.Address.phone2 = "1234567895";
            cus.Address.fax = "1234567896";
            cus.Address.email = "email";
            cus.Address.webSite = "website";
            //cus.salesPerson = "";
            cus.customerSince = "08/08/2000";
            //cus.department = "0200 Marketing";
            	
            // ship-to-address
            cus.defaultShipToAddressCheckbox = true;
            cus.ShipToAddress.contact = "ship contact";
            cus.ShipToAddress.street1 = "sh street 1";
            cus.ShipToAddress.street2 = "sh street 2";
            cus.ShipToAddress.city = "sh city";
            cus.ShipToAddress.province = "sh province";
            cus.ShipToAddress.postalCode = "V4F 16Y";
            cus.ShipToAddress.country = "sh country";
            cus.ShipToAddress.phone1 = "1234567894";
            cus.ShipToAddress.phone2 = "1234567895";
            cus.ShipToAddress.fax = "1234567896";
            cus.ShipToAddress.email = "sh email";
            cus.ShipToAddress.webSite = "sh website";
            
            // options
            //cus.revenueAccount.acctNumber = "10500 Petty Cash";
            cus.conductBusinessIn = "English";
            cus.priceList = "Preferred";
            //cus.usuallyShipItemFrom = "BC";
            cus.discountPercent = "5";
            cus.discountPeriod = "20";
            cus.termPeriod = "30";
            cus.produceStatementsForThisCustCheckbox = false;
            cus.synchronizeWithOutlook = true;
            cus.formsForThisCustomer = "Email";
            
            
            // taxes
            TAX_LEDGER tax = new TAX_LEDGER();
            tax.tax.taxName = "GST";
            tax.taxExempt = "Yes";
            tax.taxID = "1234567";
            
            cus.taxList.Add(tax);
            
            cus.taxCode.code = "GP";
            
            
//            // PAD
//            cus.custHasPADCheckbox = true;
//            cus.currencyAndLocation = "CAD bank account in Canada";
//            cus.branchNumber = "11111";
//            cus.institutionNumber = "111";
//            cus.accountNumber = "123456789";
            
            
      		// statistics
            cus.creditLimit = "2000";
            
            // memo
            cus.memo = "memo memo memo";
            cus.toDoDate = "08/30/2016";
            cus.displayCheckBox = true;
            
            
//            // import
//            cus.hasSage50CheckBox = true;
//            //cus.usesMyItemNumCheckBox = true;
//            
//            IMPORT import = new IMPORT();
//            
//            import.itemNumber = "12345";
//            import.myItemNumber = "C1020";
//            
//            cus.imports.Add(import);
            
            // additional info
            cus.additional1 = "add 1";
            cus.additional2 = "add 2";
            cus.additional3 = "add 3";
            cus.additional4 = "add 4";
            cus.additional5 = "add 5";
            
            cus.addCheckBox1 = true;
            cus.addCheckBox2 = true;
            cus.addCheckBox3 = true;
            cus.addCheckBox4 = true;
            cus.addCheckBox5 = true;
            
            // assign to global parameter to be used in latter test cases
            //varCustomer = cus.name;
           
            
            // call create method from class to add customer

            ReceivablesLedger._SA_Create(cus);
            ReceivablesLedger._SA_Close();
            
    	}
    }
}