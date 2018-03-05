/*
 * Created by Ranorex
 * User: wonga01
 * Date: 11/22/2017
 * Time: 12:57
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
    /// Description of AdjustSalesInvoice.
    /// </summary>
    [TestModule("C5270471-58E5-4BBD-B7DD-1E45BBE473D1", ModuleType.UserCode, 1)]
    public class AdjustSalesInvoice : ITestModule
    {
        
    	
    	string _varCustomer = "";
    	[TestVariable("17baf679-b5f0-42df-9992-6ff16c3b8746")]
    	public string varCustomer
    	{
    		get { return _varCustomer; }
    		set { _varCustomer = value; }
    	}
    	
    	
    	string _varItem = "";
    	[TestVariable("208becda-980e-48f4-8e93-dd8bf30f191a")]
    	public string varItem
    	{
    		get { return _varItem; }
    		set { _varItem = value; }
    	}
    	
    	string _varSInvoice = "";
    	[TestVariable("d8d5b4a0-409a-4856-87d6-94e51d661beb")]
    	public string varSInvoice
    	{
    		get { return _varSInvoice; }
    		set { _varSInvoice = value; }
    	}
    	
    	/// <summary>
        /// Constructs a new instance.
        /// </summary>
        public AdjustSalesInvoice()
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
            
            // create an item to use in invoice
            ITEM item = new ITEM();
			
			item.ItemPrices.Add(new ITEM_PRICE ("Canadian Dollars"));
			item.ItemPrices[0].priceList = "Regular";
			item.ItemPrices[0].pricePerSellingUnit = Functions.RandCashAmount();
			
			if(this.varCustomer =="")
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
			
			if(this._varSInvoice == "")
			{
            	SalesJournal._SA_Create(sale);
            	// sleep is setup in _SA_Close method
            	SalesJournal._SA_Close();
			}
			else
			{
				sale.transNumber = this.varSInvoice;
				
			}
            
            // Adjust invoice
            ROW adjR = new ROW();
            r.quantityShipped = Functions.RandCashAmount(2);
            sale.GridRows.Add(adjR);
            
            SalesJournal._SA_Create(sale, true, true);            
            SalesJournal._SA_Close();            
        }
    }
}
