/*
 * Created by Ranorex
 * User: wonga01
 * Date: 11/20/2017
 * Time: 2:51 PM
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
    /// Description of AddReciept.
    /// </summary>
    [TestModule("BF016E1C-149A-437D-8E69-032CB09ED89A", ModuleType.UserCode, 1)]
    public class AddReciept : ITestModule
    {
    	
    	
    	string _varCustomer = "";
    	[TestVariable("df60672e-30b5-4179-98ef-90d4dbdd6719")]
    	public string varCustomer
    	{
    		get { return _varCustomer; }
    		set { _varCustomer = value; }
    	}
    	
    	
    	string _varItem = "";
    	[TestVariable("e772982e-dc86-4bec-a468-d4e53826f25c")]
    	public string varItem
    	{
    		get { return _varItem; }
    		set { _varItem = value; }
    	}
    	
    	
    	string _varSInvoice = "";
    	[TestVariable("01a1a30b-8faf-4ca2-a5d2-08b16aa366dd")]
    	public string varSInvoice
    	{
    		get { return _varSInvoice; }
    		set { _varSInvoice = value; }
    	}
    	
    	
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public AddReciept()
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
            
            // Create an item to be used in invoice
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
            SALES_INVOICE sale = new SALES_INVOICE();            
            sale.Customer = cus;
       
			ROW r = new ROW();
			r.Item = item;
			r.quantityShipped = Functions.RandCashAmount(2);
			r.price = Functions.RandCashAmount();
			
			sale.GridRows.Add(r);
            
			if(this.varSInvoice == "")
			{
				SalesJournal._SA_Create(sale);
            	System.Threading.Thread.Sleep(1000);
           	 	SalesJournal._SA_Close();
			}
			else
			{
				sale.transNumber = this.varSInvoice;
			}
            
            // Create a receipt
            RECEIPT receipt = new RECEIPT();
            receipt.Customer = cus;
            RECEIPT_ROW receiptRow = new RECEIPT_ROW();
            receiptRow.Invoice = sale;
            receipt.GridRows.Add(receiptRow);
            
            ReceiptsJournal._SA_Create(receipt);
            System.Threading.Thread.Sleep(1000);
            ReceiptsJournal._SA_Close();     
            
        }
    }
}
