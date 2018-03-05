/*
 * Created by Ranorex
 * User: wonda05
 * Date: 8/22/2016
 * Time: 3:00 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

using Ranorex;
using Ranorex.Core;
using Ranorex.Core.Testing;
using Sage50.Classes;
using Sage50.Types;
using Sage50.Shared;


namespace Sage50.Scripts
{
    /// <summary>
    /// Description of test.
    /// </summary>
    [TestModule("B241AF1B-77C1-4E76-9C20-46C28C2F0331", ModuleType.UserCode, 1)]
    public class test : ITestModule
    {
        /// <summary>
        /// Constructs a new repo.
        /// </summary>
        public test()
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
            //InventoryServicesLedger.repo.Linked.InvAssetAcct.Items.
            //IList<Ranorex.Unknown> listX = ReceivablesLedger.repo.Taxes.TaxContainer.FindChildren<Ranorex.Unknown>();

           
            

            
            //ReceivablesLedger.DataFile_ReadFile(@"C:\SilkTest\data\SA2017\Audit\Audit Database\CL Records\", "1000");
            
            //System.Windows.Forms.MessageBox.Show(ReceivablesLedger.repo.Taxes.TaxContainer.GetAttributeValue("accessibledescription"));
            //ReceivablesLedger.repo.Taxes.TaxTable.Element.
//            foreach (Ranorex.Text txt in ReceivablesLedger.repo.Taxes.TaxContainer.FindChildren<Ranorex.Text>())
//            {
//            	System.Windows.Forms.MessageBox.Show(txt.TextValue);
//            }
            
			//System.Windows.Forms.MessageBox.Show(ReceivablesLedger.repo.Taxes.TaxContainer.FindChildren(<>)
			
			
			
			
			
			
			
			
//			Ranorex.NativeWindow nw = new Ranorex.NativeWindow(ReceivablesLedger.repo.Taxes.TaxContainer);
//			string winText = nw.WindowText;
//			System.Windows.Forms.MessageBox.Show(winText);
			
			//List<List <string>> ls= ReceivablesLedger.repo.Taxes.TaxContainer.GetContents(true);
			//Delay.SpeedFactor = 1.0;
			
			//System.Windows.Forms.MessageBox.Show(SalesModule.repo.CustomerCombo.Items.Count.ToString());
			
			
			//System.Windows.Forms.MessageBox.Show(ReceivablesLedger.repo.InactiveCustomer.Checked.ToString());
			
			
			//ReceivablesLedger.repo.Taxes.TaxContainer.Click("49;26");
			//ReceivablesLedger.repo.Taxes.TaxContainer.PressKeys("{TAB}");
			
			//ReceivablesLedger.repo.Taxes.TaxContainer.SetToLine(3);
			
			
//			string temp = Functions.GetField(ReceivablesLedger.repo.Taxes.TaxCode.SelectedItemText," - ",1);
//			
//			if(temp == "")
//			{
//				System.Windows.Forms.MessageBox.Show("No Tax!");
//			}
			
			
			//ReceivablesLedger._SA_Read();
			
			//System.Windows.Forms.MessageBox.Show(SalesModule.repo.InvoiceGrid.InvoiceGrid.Rows.Count.ToString());
			
			
			
//			string column = "Amount";
//			
//			//Ranorex.Cell myCel = SalesModule.repo.Self.FindSingle(String.Format("?/?/cell[@accessiblename='{0} Row 0']",column));
//			Ranorex.Cell myCel = SalesModule.repo.Self.FindSingle("?/?/cell[@childindex='5' and @accessiblename='*Row 0']");
//			myCel.Click();
			
			//SalesModule.repo.InvoiceGrid.Table.SelectCell("Amount",2);
			
			//Ranorex.Row myRow = SalesModule.repo.Self.FindSingle("/?/?/row[@accessiblename='Row 0']");
		
			//Accessible accValue = new Accessible(SalesModule.repo.Self.FindSingle("/?/?/row[@accessiblename='Row 0']"));
			//string DataSources = accValue.Value;
			
			//System.Windows.Forms.MessageBox.Show(SalesModule.repo.InvoiceGrid.Table.Rows.Count.ToString());
			
			//List<List <string>> lscontents = SalesJournal.repo.InvoiceGrid.Table.GetContents();
			
			//Delay.SpeedFactor = 1.0;
			
			
			//SalesJournal._SA_SetTransType("Contract");
			
			//SalesJournal.repo.TransContainer.Focus();
			
			//SalesJournal.repo.TransContainer.PressKeys("{LShiftKey down}{Tab}{LShiftKey up}");
            				
			
			//SalesJournal.repo.TransContainer.PressKeys("{a}");
			
			//SalesJournal.repo.QuantityRow0.Click();
			//SalesJournal.repo.QuantityRow0.PressKeys("20");
			//SalesJournal.repo.TransContainer.SelectCell("Quantity",1,"12");
			//SalesJournal.repo.TransContainer.SelectCell("ItemNumber",1, "C1020");
			
			
			//System.Windows.Forms.MessageBox.Show(SalesJournal.repo.TransContainer.Columns.Count.ToString());
			
			

//			Accessible accValue = new Accessible(SalesJournal.repo.TransContainer.FindSingle("/?/?/row[@accessiblename='Row 0']"));
//			string tableValues = accValue.Value;
				
			//System.Windows.Forms.MessageBox.Show(SalesJournal.repo.TransContainer.ColumnCount().ToString());
		
//			SALES s = new SALES();
//			
//			s = SalesJournal._SA_Read();
//			
//			Delay.SpeedFactor = 1.0;
//			
			//SalesJournal.repo.Self.PressKeys("{LMenu down}{Vkey}{LMenu up}{w}");
            //SalesJournal.repo.Self.PressKeys("w");	// restore to default
            
            //SalesJournal.repo.View.Click();
            //SalesJournal.repo.RestoreWindow.Click();
            
            
            //Ranorex.Button mybutton = SalesJournal.repo.Self.FindSingle(SalesJournal.repo.CustomerName.GetPath().ToString() + "/button[@accessiblename='Open']");
           
            //mybutton.Click();
            
                
                
            //SalesJournal.repo.Self.PressKeys("{LCtrl}{Kkey}{LCtrl up}");
            	
            
            //SalesJournal.repo.CustomerName.Select("v", true);
            //SalesJournal.repo.TransTypeCombo.Select("Order", true);
            
            //System.Windows.Forms.MessageBox.Show(SalesJournal.repo.CustomerName.GetPath().ToString());
            
            //SalesJournal.repoList.ListItemKay.Click();

//            Ranorex.ListItem myList = SalesJournal.repo.Self.FindSingle("/list[@controlid='1000']/listitem[@text='v']");
//            
//            myList.Click();

			//SalesJournal.repo.TransTypeCombo.Select("Order");
			//ReceivablesLedger.repo.ShipToAddress.AddressName.Select("Ship-to Address");
			
			//SalesJournal.repo.TransTypeCombo.Items[1].Select();
			//SalesJournal.repo.InvoiceNumber.TextValue = "sdafd";
			
			//Recording1.repo.AS
			
			//ReceivablesLedger.repo.ShipToAddress.AddressName.Select("Ship-to Address");
			
			//SalesJournal.repoAddOnFly.QuickAdd.Select();
			
			
			//System.Windows.Forms.MessageBox.Show(Functions.RandPick(InventoryServicesLedger.repo.Linked.InvRevenueAcct));
			
			
			
			
			//Simply.repo.Self.Activate();
			//Simply.repo.ReceivablesLink.Click();
			
			//Simply.repo.InventoryServicesLink.Click();
            //Simply.repo.InventoryServicesIcon.Click();
            
            //System.Windows.Forms.MessageBox.Show(InventoryServicesLedger.repo.InventoryType.Checked.ToString());
            
            //System.Windows.Forms.MessageBox.Show(InventoryServicesLedger.repo.Pricing.Currency.Visible.ToString());
            //SimplyMessage._SA_HandleMessage(SimplyMessage.repo.Yes, SimplyMessage.repo._Msg_LinkAssetMustBeAssignedTheAccountClassInventory);
            
            
            
            //if(SimplyMessage.repo.SelfInfo.Exists())
			//{
            //  // Press yes on message about changing accout type
            //   SimplyMessage._SA_HandleMessage(SimplyMessage.repo.Yes, SimplyMessage.repo._Msg_LinkAssetMustBeAssignedTheAccountClassInventory);
			//}
			
			//Ranorex.Button mybutton = InventoryServicesLedger.repo.Linked.InvAssetAcct.FindSingle(InventoryServicesLedger.repo.Linked.InvAssetAcct.GetPath().ToString() + "/button[@accessiblename='Open' or @text='>']");
			//mybutton
			//Delay.Milliseconds(500);
			
			//Keyboard.Press(System.Windows.Forms.Keys.Down | System.Windows.Forms.Keys.Alt, 80, Keyboard.DefaultKeyPressTime, 1, true);
			
			//InventoryServicesLedger.repo.Linked.InvAssetAcct.Focus();
			//InventoryServicesLedger.repo.Linked.InvAssetAcct.PressKeys("{ALT down}{Down}");
			
			//System.Windows.Forms.MessageBox.Show(InventoryServicesLedger.repo.Linked.InvAssetAcct.Items.ToString());
			//InventoryServicesLedger.repo.Linked.InvAssetAcct.PressKeys("{Down}");            
            //Keyboard.Press("{ALT down}{Down}");
            
            //Delay.SpeedFactor = 1.0;
            //InventoryServicesLedger.repo.Linked.InvAssetAcct.Select("10200 Cash to be deposited");
            //InventoryServicesLedger.repo.Linked.InvAssetAcct.PressKeys("10200 Cash to be deposited");
            
            //InventoryServicesLedger.repo.Linked.InvAssetAcct.RandPick();
            
            //Settings._SA_SetCompanyFeatureSettings();
            
            
//            Settings._SA_SetToGenericValues();
//			System.Threading.Thread.Sleep(5000);
//            
//            // Settings
//			Variables.globalSettings.InventorySettings.allowInventoryLevelsToGoBelowZero = true;
//			Settings._SA_SetInventoryOptionSettings();
//			Variables.globalSettings.ReceivableSettings.calculateLineItemDiscounts = false;
//			Variables.globalSettings.ReceivableSettings.interestCharges = true;
//			System.Threading.Thread.Sleep(5000);
//			Settings._SA_SetReceivablesOptionsAndDiscountSettings();
//			
//			
//			System.Threading.Thread.Sleep(5000);
//			
//			
//			// turn on orders/quotes
//			Variables.globalSettings.CompanySettings.FeatureSettings.ordersForCustomers = true;
//			Variables.globalSettings.CompanySettings.FeatureSettings.ordersForVendors = true;
//			Variables.globalSettings.CompanySettings.FeatureSettings.quotesForCustomers = true;
//			Variables.globalSettings.CompanySettings.FeatureSettings.quotesForVendors = true;
//			Settings._SA_SetCompanyFeatureSettings();
			// set currencies
			Variables.globalSettings.CompanySettings.CurrencySettings.allowForeignCurrency = true;
			Variables.globalSettings.CompanySettings.CurrencySettings.HomeCurrency.roundingDifferencesAccount = "5650 Currency Exchange & Rounding";
			Variables.globalSettings.CompanySettings.CurrencySettings.HomeCurrency.decimalPlaces = CURRENCY_DECIMAL.CURRENCY_DECIMAL_2;
			
			CURRENCY_DATA newCurr = new CURRENCY_DATA();
			
			newCurr.Currency = "United States Dollars";
			newCurr.decimalPlaces = CURRENCY_DECIMAL.CURRENCY_DECIMAL_2;
			Variables.globalSettings.CompanySettings.CurrencySettings.ForeignCurrencies.Add(newCurr);
			Settings._SA_SetCompanyCurrencySettings();   
			
        }
    }
}
