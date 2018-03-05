/*
 * Created by Ranorex
 * User: wonga01
 * Date: 7/10/2017
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
    /// Description of PrintSimplyForm.
    /// </summary>
    [TestModule("5689CE34-E2DF-4313-A786-D1C97941FBD3", ModuleType.UserCode, 1)]
    public class PrintSimplyForm : ITestModule
    {
        
    	
    	string _varCustomer = "";
    	[TestVariable("89ad0c3f-70b4-4459-98b4-41e2e7c27cb7")]
    	public string varCustomer
    	{
    		get { return _varCustomer; }
    		set { _varCustomer = value; }
    	}
    	
    	
    	string _varItem = "";
    	[TestVariable("3de56fe1-95c4-4b28-81a0-96195b6dfd34")]
    	public string varItem
    	{
    		get { return _varItem; }
    		set { _varItem = value; }
    	}
    	
    	
    	/// <summary>
        /// Constructs a new instance.
        /// </summary>
        public PrintSimplyForm()
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
            
            // Remove existing print file
            string printedInvoice = @"C:\Users\Public\Documents\Simply Accounting\2018\Data\Invoice.pdf"; // @"C:\Users\_sabvt\Documents\Simply Accounting\DATA\Invoice.pdf";            
            Functions.RemoveExistingFile(printedInvoice);
            
            // Create a customer            
            CUSTOMER cus = new CUSTOMER();
            
            if(this.varCustomer == "")
            {
            	cus.name = "cust" + StringFunctions.RandStr("X(8)");
            
            	ReceivablesLedger._SA_Create(cus);
            	ReceivablesLedger._SA_Close();
            }
            else
            {
            	cus.name = this.varCustomer;
            }
                                                
            
            // Create an item
        	ITEM item = new ITEM();
		
			ITEM_PRICE itemPrice = new ITEM_PRICE();
			itemPrice.currency = "Canadian Dollars";								
			itemPrice.priceList = "Regular";
			itemPrice.pricePerSellingUnit = Functions.RandCashAmount();
			item.ItemPrices.Add(itemPrice);
			
			if(this.varItem == "")
			{
				item.invOrServNumber = StringFunctions.RandStr("A(9)");
				InventoryServicesLedger._SA_Create(item);
				InventoryServicesLedger._SA_Close();
			}
			else
			{
				item.invOrServNumber = this._varItem;
				
			}
			
			// Create an invoice and print a Simply form
			SALES_INVOICE salesInv = new SALES_INVOICE();
			salesInv.Customer = cus;
			
			ROW firstRow = new ROW();
			firstRow.Item.invOrServNumber = item.invOrServNumber;
			firstRow.quantityShipped = Functions.RandCashAmount(2);
			salesInv.GridRows.Add(firstRow);
			
			SalesJournal._SA_Create(salesInv, false);
			
			// Print. Install, then setup pdf printer to be default printer first
			SalesJournal._SA_PrintToFile(printedInvoice);
			
			// wait for file to be created
			Thread.Sleep(13000);
			
			// undo and close journal
			SalesJournal.UndoChanges();
			SalesJournal._SA_Close();
			
			// Verify file has been printed
			if (!Functions.VerifyFileExists(printedInvoice))
			{
				Functions.Verify(false, true, "Printed Simply form found");				
			}
            
        }
    }
}
