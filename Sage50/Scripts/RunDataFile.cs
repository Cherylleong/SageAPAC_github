/*
 * Created by Ranorex
 * User: wonda05
 * Date: 4/29/2016
 * Time: 2:24 PM
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

namespace Sage50.BVT
{
    /// <summary>
    /// Description of RunDataFile.
    /// </summary>
    [TestModule("269D9CAF-7A65-45AB-975B-1E113418540A", ModuleType.UserCode, 1)]
    public class RunDataFile : ITestModule
    {
        /// <summary>
        /// Constructs a new repo.
        /// </summary>
        public RunDataFile()
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
            


             Ranorex.Core.Data.CsvDataConnector csvConnector = new Ranorex.Core.Data.CsvDataConnector("csvConnector",@"c:\temp\CL Records\CL1000.hdr", false);
             	
                                                                                             
             // Load all Data from the csv file
             Ranorex.Core.Data.ColumnCollection columnCollection;
             Ranorex.Core.Data.RowCollection rowCollection;
             csvConnector.LoadData(out columnCollection, out rowCollection);
             	
             // We can now use the data - e.g. add all Texts of the first Excel column to a List of Strings
             List<String> allStringsFromFirstRow = new List<String>();
             foreach(var item in rowCollection)
             {
             	// populate customer record
             	
             	// run create function
             	
             	
             	allStringsFromFirstRow.Add(item.Values[0]);
             	
             	
             }
             	
        }
    }
}
