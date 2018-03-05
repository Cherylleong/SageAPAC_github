/*
 * Created by Ranorex
 * User: wonda05
 * Date: 6/30/2017
 * Time: 3:18 PM
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
    /// Description of AddItem.
    /// </summary>
    [TestModule("CFA858B4-DFC1-44AB-BA69-AEBB09223842", ModuleType.UserCode, 1)]
    public class AddItem : ITestModule
    {
    	
    	
    	string _varItem = "";
    	[TestVariable("7bfda043-c020-4276-81c6-bc69e7ec081d")]
    	public string varItem
    	{
    		get { return _varItem; }
    		set { _varItem = value; }
    	}
    	
    	
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public AddItem()
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
            
            
            ITEM item = new ITEM();
			item.invOrServNumber = StringFunctions.RandStr("A(9)");
			//item.ItemPrices[0].currency = "Canadian Dollars";
			item.ItemPrices.Add(new ITEM_PRICE ("Canadian Dollars"));
			item.ItemPrices[0].priceList = "Regular";
			item.ItemPrices[0].pricePerSellingUnit = Functions.RandCashAmount();
		
			InventoryServicesLedger._SA_Create(item);
			InventoryServicesLedger._SA_Close();

        	// set to global
			this.varItem = item.invOrServNumber;
			    
        }
    }
}
