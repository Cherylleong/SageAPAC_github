/*
 * Created by Ranorex
 * User: wonga01
 * Date: 10/10/2017
 * Time: 11:00
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
    /// Description of AddReceipt.
    /// </summary>
    [TestModule("F0EB82CC-6F91-4943-824F-F61243BDE127", ModuleType.UserCode, 1)]
    public class AddReceipt : ITestModule
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public AddReceipt()
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
            cus.name = "cust" + StringFunctions.RandStr("X(8)");
            //cus.name = "International Oil";
            
            ReceivablesLedger._SA_Create(cus);
            ReceivablesLedger._SA_Close();
                      
            // Create an item
            ITEM item = new ITEM();
			item.invOrServNumber = StringFunctions.RandStr("A(9)");
			//item.invOrServNumber = "C1020";
			item.ItemPrices.Add(new ITEM_PRICE ("Canadian Dollars"));
			item.ItemPrices[0].priceList = "Regular";
			item.ItemPrices[0].pricePerSellingUnit = Functions.RandCashAmount();
			
			InventoryServicesLedger._SA_Create(item);
			InventoryServicesLedger._SA_Close();
                      
             // Create an Invoice
            SALES_INVOICE sale = new SALES_INVOICE();
            
            sale.Customer = cus;
            sale.transNumber = StringFunctions.RandStr("9(8)");
       
			ROW r = new ROW();
			r.Item = item;
			r.quantityShipped = Functions.RandCashAmount(2);
			r.price = Functions.RandCashAmount();
			
			sale.GridRows.Add(r);
            
            SalesJournal._SA_Create(sale);
            System.Threading.Thread.Sleep(2000);
            SalesJournal._SA_Close();
            
//            // tmp
//            CUSTOMER cus = new CUSTOMER();
//            cus.name = "custYXTngnQw";
//            ITEM item = new ITEM();
//            item.invOrServNumber = "aqlUMddep";
//            SALES_INVOICE sale = new SALES_INVOICE();
//            sale.Customer = cus;
//            sale.transNumber = "15883414";
//            
            // Create an receipt
            RECEIPT receipt = new RECEIPT();
            receipt.Customer = cus;
            RECEIPT_ROW row1 = new RECEIPT_ROW();
            row1.Invoice = sale;            
            receipt.GridRows.Add(row1);
            
            ReceiptsJournal._SA_Create(receipt);            
            System.Threading.Thread.Sleep(1000);
            ReceiptsJournal._SA_Close();
            
        }
    }
}
