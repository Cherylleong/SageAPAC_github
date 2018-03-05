/*
 * Created by Ranorex
 * User: wonga01
 * Date: 10/4/2017
 * Time: 13:21
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

namespace Sage50.Scripts.BVT
{
    /// <summary>
    /// Description of AddPayment.
    /// </summary>
    [TestModule("79628B55-5365-491B-8B60-EF0ECBC4741A", ModuleType.UserCode, 1)]
    public class AddPayment : ITestModule
    {
    	
    	string _varVendor = "";
    	[TestVariable("c4eb12a4-2054-40fd-9a37-256750eeb2a3")]
    	public string varVendor
    	{
    		get { return _varVendor; }
    		set { _varVendor = value; }
    	}
    	
    	
    	string _varItem = "";
    	[TestVariable("a48fa8b7-2a14-48ef-b40e-ab42b64242a0")]
    	public string varItem
    	{
    		get { return _varItem; }
    		set { _varItem = value; }
    	}
    	
    	string _varPInvoice = "";
    	[TestVariable("59ed6fb4-4ad3-4932-bf99-3b7dcecf9a85")]
    	public string varPInvoice
    	{
    		get { return _varPInvoice; }
    		set { _varPInvoice = value; }
    	}
    	
    	
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public AddPayment()
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
            
            // Create a vendor
            VENDOR ven = new VENDOR();

            if(this.varVendor == "")
            {
				ven.name = "Vend" + StringFunctions.RandStr("X(8)");
				PayablesLedger._SA_Create(ven);
				PayablesLedger._SA_Close();
            }
            else
            {
            	ven.name = this.varVendor;
            }
			

		
			// Create an item
			ITEM item = new ITEM();
			item.ItemPrices.Add(new ITEM_PRICE ("Canadian Dollars"));
			item.ItemPrices[0].priceList = "Regular";
			item.ItemPrices[0].pricePerSellingUnit = Functions.RandCashAmount();
			
			if(this.varItem == "")
			{
				item.invOrServNumber = StringFunctions.RandStr("A(9)");
				InventoryServicesLedger._SA_Create(item);
				InventoryServicesLedger._SA_Close();
			}
			else
			{
				item.invOrServNumber = this.varItem;
			}
					
	
			// Create an Invoice
			PURCHASE_INVOICE pi = new PURCHASE_INVOICE();
			
			pi.Vendor = ven;
			
			
			
			ROW r = new ROW();
			r.Item = item;
			r.Item.invOrServNumber = item.invOrServNumber;
			r.quantityReceived = Functions.RandCashAmount(2);
			// need a pause to create a different random number..
			System.Threading.Thread.Sleep(1000);
			r.price = Functions.RandCashAmount();
			
			pi.GridRows.Add(r);
			
			if(this.varPInvoice == "")
			{
				pi.transNumber = StringFunctions.RandStr("9(8)");
			
				PurchasesJournal._SA_Create(pi);
				System.Threading.Thread.Sleep(2000);
				PurchasesJournal._SA_Close();			
			}
			else
			{
				pi.transNumber = this.varPInvoice;
			}
			
			// Create a payment
			PAYMENT_PURCH payment = new PAYMENT_PURCH();
			payment.Vendor = ven;
			PAY_ROW row1 = new PAY_ROW();
			row1.Invoice = pi;
			payment.GridRows.Add(row1);			
			
			PaymentsJournal._SA_CreatePayment(payment);
			System.Threading.Thread.Sleep(1000);
			PaymentsJournal._SA_Close();
			
        }
    }
}
