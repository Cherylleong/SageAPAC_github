/*
 * Created by Ranorex
 * User: wonga01
 * Date: 7/11/2017
 * Time: 16:07
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
    /// Description of PrintVendorForm.
    /// </summary>
    [TestModule("A5B5D3FB-3124-47D6-9557-314DB4B0E7A1", ModuleType.UserCode, 1)]
    public class PrintVendorForm : ITestModule
    {
        
    	
    	
    	string _varItem = "";
    	[TestVariable("dd8a5092-ee11-4ab2-84f6-07f60d9e0bf1")]
    	public string varItem
    	{
    		get { return _varItem; }
    		set { _varItem = value; }
    	}
    	
    	
    	/// <summary>
        /// Constructs a new instance.
        /// </summary>
        public PrintVendorForm()
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
            
            string sT5018Summary = @"C:\Users\Public\Documents\Simply Accounting\2018\Data\Summary of Contract Payments.pdf";// @"C:\Users\_sabvt\Documents\Simply Accounting\DATA\Summary of Contract Payments.pdf";
                        
            string sInstallPath = Simply._SA_GetProgramPath();
            string sDllPath = string.Format(@"{0}acPDFCreatorLib.Net.dll", sInstallPath);
            
            // Check Sage 50 pdf engine dll is installed before starting test case
            if (Functions.VerifyFileExists(sDllPath))
            {
            	// Remove existing pdf file
            	Functions.RemoveExistingFile(sT5018Summary);
            	
            	// Create a T5018 vendor
            	VENDOR tvendor = new VENDOR();
            	
            	tvendor.name = StringFunctions.RandStr("A(9)");
            	tvendor.includeFilingT5018CheckBox = true;
            	PayablesLedger._SA_Create(tvendor);
            	PayablesLedger._SA_Close();
            	
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
            		item.invOrServNumber = this.varItem;
            	}
            	
				// Create purchase invoice using T5018 vendor
				PURCHASE_INVOICE purInv = new PURCHASE_INVOICE();
				purInv.Vendor = tvendor;
				purInv.transNumber = StringFunctions.RandStr("9(8)");
				
				ROW firstRow = new ROW();
				firstRow.Item.invOrServNumber = item.invOrServNumber;
				firstRow.quantityReceived = Functions.RandCashAmount(2);
				firstRow.price = Functions.RandCashAmount();
				purInv.GridRows.Add(firstRow);
				
				PurchasesJournal._SA_Create(purInv);
				PurchasesJournal._SA_Close();
				
				// Print T5018 summary to a file
				string sCraNumber = "403381601RZ0001";				
				Simply._Print_T5018Summary(sCraNumber);
				
				
				// Verify printed file				
            	if (!Functions.VerifyFileExists(sT5018Summary))
				{
					Functions.Verify(false, true, "Printed T5018 form found");
				}
            	
            }
            else
            {
            	Functions.Verify(false, true, "PDF engine dll file found");
            }
            
            // Wait 5 seconds before moving on to next testcase
            Thread.Sleep(5000);
        }
    }
}
