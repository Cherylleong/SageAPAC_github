/*
 * Created by Ranorex
 * User: wonda05
 * Date: 5/19/2016
 * Time: 2:52 PM
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
    /// Description of AddSalesInvoice.
    /// </summary>
    [TestModule("DA7543D6-375E-4689-B6A4-116F3C7184DA", ModuleType.UserCode, 1)]
    public class AddSalesInvoice : ITestModule
    {
    	
		// variabled used to populated global var in test case	
    	string _varCustomer = "";
    	[TestVariable("9382533f-4b5b-440f-91bd-ae97be2b115b")]
    	public string varCustomer
    	{
    		get { return _varCustomer; }
    		set { _varCustomer = value; }
    	}
    	
    	
    	string _varItem = "";
    	[TestVariable("caaafdc1-2d99-4715-a51e-8a0a4e557f41")]
    	public string varItem
    	{
    		get { return _varItem; }
    		set { _varItem = value; }
    	}
    	
    	
    	string _varSInvoice = "";
    	[TestVariable("4ac59c3a-cbf7-4fdb-93ba-00085e9462e9")]
    	public string varSInvoice
    	{
    		get { return _varSInvoice; }
    		set { _varSInvoice = value; }
    	}
    	
    	
        /// <summary>
        /// Constructs a new repo.
        /// </summary>
        public AddSalesInvoice()
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
            
            // only create a new customer if running test case alone
            if(this._varCustomer == "")
            {
            	cus.name = "cust" + StringFunctions.RandStr("X(8)");
            
            	ReceivablesLedger._SA_Create(cus);
            	ReceivablesLedger._SA_Close();
            }
            else
            {
            	// other wise take customer name from global variable
            	cus.name = this._varCustomer;
            }
                      
            // create item to use in invoice
            ITEM item = new ITEM();
            item.ItemPrices.Add(new ITEM_PRICE ("Canadian Dollars"));
			item.ItemPrices[0].priceList = "Regular";
			item.ItemPrices[0].pricePerSellingUnit = Functions.RandCashAmount();
            
            
			 // only create a new item if running test case alone
			if(this._varItem == "")
			{
				item.invOrServNumber = StringFunctions.RandStr("A(9)");
				InventoryServicesLedger._SA_Create(item);
				InventoryServicesLedger._SA_Close();
			}
			else
			{
				item.invOrServNumber = this._varItem;
			}
                      
             // Create an Invoice
            SALES_INVOICE sale = new SALES_INVOICE();
            
            sale.Customer = cus;
       
			ROW r = new ROW();
			r.Item = item;
			r.quantityShipped = Functions.RandCashAmount(2);
			r.price = Functions.RandCashAmount();
			
			sale.GridRows.Add(r);
			
            SalesJournal._SA_Create(sale);
            System.Threading.Thread.Sleep(2000);
            SalesJournal._SA_Close();
            
            this.varSInvoice = sale.transNumber;
        }
    }
}
