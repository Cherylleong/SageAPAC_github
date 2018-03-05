/*
 * Created by Ranorex
 * User: wonda05
 * Date: 10/16/2017
 * Time: 3:14 PM
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
    /// Description of AddProject.
    /// </summary>
    [TestModule("85616B7E-0891-45FD-B627-623F133B0D76", ModuleType.UserCode, 1)]
    public class AddProject : ITestModule
    {
    	
    	string _varProject = "";
    	[TestVariable("e582bed3-a4e6-475b-a5d3-8c23dbc6ca69")]
    	public string varProject
    	{
    		get { return _varProject; }
    		set { _varProject = value; }
    	}
    	
    	
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public AddProject()
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
            PROJECT project = new PROJECT();
            project.name = "project" + StringFunctions.RandStr("X(8)");        
            
           	// call create method from class to add project
            ProjectLedger._SA_Create(project);
            ProjectLedger._SA_Close();
            
            this.varProject = project.name;
            
        }
    }
}
