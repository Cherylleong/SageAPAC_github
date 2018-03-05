/*
 * Created by Ranorex
 * User: wonga01
 * Date: 9/7/2017
 * Time: 13:51
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
    /// Description of AddPurchaseInvoice.
    /// </summary>
    [TestModule("06A311BB-6938-4121-BECB-5A71F942BF67", ModuleType.UserCode, 1)]
    public class AddPurchaseInvoice : ITestModule
    {
    	
    	string _varVendor = "";
    	[TestVariable("f7972525-a828-42d0-98a2-bd10f5384682")]
    	public string varVendor
    	{
    		get { return _varVendor; }
    		set { _varVendor = value; }
    	}
    	
    	string _varItem = "";
    	[TestVariable("504bc4c5-1d39-40ab-bdfc-1c3d102ae317")]
    	public string varItem
    	{
    		get { return _varItem; }
    		set { _varItem = value; }
    	}
    	
    	string _varPInvoice = "";
    	[TestVariable("00429f78-bbbe-449c-98fb-39e7d32bf6d7")]
    	public string varPInvoice
    	{
    		get { return _varPInvoice; }
    		set { _varPInvoice = value; }
    	}
    	
    	
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public AddPurchaseInvoice()
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
			pi.transNumber = StringFunctions.RandStr("9(8)");
			
			ROW r = new ROW();
			r.Item = item;
			//r.Item.invOrServNumber = "C1020";	// tmp
			r.quantityReceived = Functions.RandCashAmount(2);
			r.price = Functions.RandCashAmount();
			
			pi.GridRows.Add(r);
			
			PurchasesJournal._SA_Create(pi);
			System.Threading.Thread.Sleep(2000);
			PurchasesJournal._SA_Close();
			
			this.varPInvoice = pi.transNumber;	
        }
    }
}
