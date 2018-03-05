/*
 * Created by Ranorex
 * User: wonga01
 * Date: 11/27/2017
 * Time: 10:27
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
using Sage50.Shared;
using Sage50.Types;

namespace Sage50.Scripts.BVT
{
    /// <summary>
    /// Description of AdjustPurchaseInvoice.
    /// </summary>
    [TestModule("4FEAC5B2-C483-41A9-8ADD-87226482A724", ModuleType.UserCode, 1)]
    public class AdjustPurchaseInvoice : ITestModule
    {
        
        string _varVendor = "";
        [TestVariable("4131d195-c2b3-4c1e-9437-94d322f1a8a3")]
        public string varVendor
        {
        	get { return _varVendor; }
        	set { _varVendor = value; }
        }
        
        string _varItem = "";
        [TestVariable("5d4f585d-3be8-4a82-9076-f9fbbd03bc57")]
        public string varItem
        {
        	get { return _varItem; }
        	set { _varItem = value; }
        }
        
        string _varPInvoice = "";
        [TestVariable("bea16eb8-b233-4824-b689-63566ad67eab")]
        public string varPInvoice
        {
        	get { return _varPInvoice; }
        	set { _varPInvoice = value; }
        }
        
    	
    	
    	/// <summary>
        /// Constructs a new instance.
        /// </summary>
        public AdjustPurchaseInvoice()
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
			
            if(this.varItem == "")
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
			r.quantityReceived = Functions.RandCashAmount(2);
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

			// Adjust the invoice

			pi.GridRows[0].quantityReceived = Functions.RandCashAmount(2);
			PurchasesJournal._SA_Create(pi, true, true);
			PurchasesJournal._SA_Close();
						
        }
    }
}
