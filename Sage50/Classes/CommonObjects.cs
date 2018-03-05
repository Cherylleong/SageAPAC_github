/*
 * Created by Ranorex
 * User: wonga01
 * Date: 7/10/2017
 * Time: 15:44
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Threading;
using System.Collections.Generic;
using Sage50.Repository;
using Sage50.Shared;
using Sage50.Types;
using System.IO;
using Ranorex;

namespace Sage50.Classes
{
	/// <summary>
	/// Description of CommonObjects.
	/// </summary>
	public class CommonObjects
	{
		// There are no objects in common objects class. Create a separate class for each object.	
	}
	
	public class SaveTheFileAsDialog
	{
		public static CommonObjectsResFolders.SaveTheFileAsAppFolder repo = CommonObjectsRes.Instance.SaveTheFileAs;
		
		public static void _SaveFileAs(string sFileName)
		{
			repo.Self.Activate();
			repo.FileName.TextValue = sFileName;
			repo.Save.Click();
		}
	}
				
	public class DialogJournalSearch
	{
		public static CommonObjectsResFolders.DialogJournalSearchAppFolder repo = CommonObjectsRes.Instance.DialogJournalSearch;
		
		public static void _SA_SelectLookupDateRange()
		{
			int first = 0;
			int last; 
			
			if(repo.FinishDate.Items.Count > 1)
			{
				last = repo.FinishDate.Items.Count -1;
			}
			else
			{
				last = 0;
			}
			
			repo.StartDate.Items[first].Select();
			repo.FinishDate.Items[last].Select();
		}
	}
	
	public class PrintToFileDialog
	{
		public static CommonObjectsResFolders.PrintToFileAppFolder repo = CommonObjectsRes.Instance.PrintToFile;
		
		public static void Print(string fileName)
		{
			PrintToFileDialog.repo.Self.Activate();
			PrintToFileDialog.repo.OutputFileName.TextValue = fileName + ".txt";
			PrintToFileDialog.repo.OK.Click();
		
//			if (s_desktop.Exists(PrintToFileConfirm.PRINTTOFILECONFIRM_LOC))
//            {
//                PrintToFileConfirm.Instance.OK.Click();
//
//            }  
		}
	}
	
	public class ProjectAllocationDialog
	{
		public static CommonObjectsResFolders.ProjAllocationAppFolder repo= CommonObjectsRes.Instance.ProjAllocation;
		
		public static void _SA_EnterProjectAllocationDetails(List<PROJECT_ALLOCATION> projects)
		{
			//ProjectAllocationDialogDotNet.Instance.PRJ_Container.SelectItem(0,0);   // go to first cell
            for (int x = 0; x < projects.Count; x++)
            {
                ProjectAllocationDialog.repo.DataGrid.PressKeys(projects[x].Project.name); 
                ProjectAllocationDialog.repo.DataGrid.PressKeys("{Tab}");
                if (Variables.globalSettings.ProjectSettings.otherAllocationMethod == ALLOCATE_TRANSACTIONS.ALLOCATE_TRANS_AMOUNT && projects[x].Amount != null || 
                    Variables.globalSettings.ProjectSettings.payrollAllocationMethod == ALLOCATE_PAYROLL.ALLOCATE_AMOUNT && projects[x].Amount != null)
                {
                    ProjectAllocationDialog.repo.DataGrid.PressKeys(projects[x].Amount);
                }
                else
                {
                   ProjectAllocationDialog.repo.DataGrid.PressKeys(projects[x].Percent);
                }                
                ProjectAllocationDialog.repo.DataGrid.PressKeys("{Tab}");
            }            
            
            ProjectAllocationDialog.repo.OK.Click();
		}
		
		 public static List<PROJECT_ALLOCATION> _SA_GetProjectAllocationDetails()
        {
             List<PROJECT_ALLOCATION>  lsAllocations = new List<PROJECT_ALLOCATION>() {};

             List<List<string>> lsContents = ProjectAllocationDialog.repo.DataGrid.GetContents(); // ConvertFunctions.DataGridItemsToListOfString(this.PRJ_Container.Items, this.PRJ_Container.ColumnCount);

             foreach (List<string> row in lsContents)
             {
                 PROJECT_ALLOCATION PA = new PROJECT_ALLOCATION();

                 PA.Project.name = row[0];
                 PA.Amount = row[1];
                 PA.Percent = row[2];

                 lsAllocations.Add(PA);
             }

             return lsAllocations;
        }
	}
	
	public class SelectAccountDialog
	{
		public static CommonObjectsResFolders.SelectAccountAppFolder repo = CommonObjectsRes.Instance.SelectAccount;		
				
	}
		
	public static class DotNetJournalSearch
	{
		public static CommonObjectsResFolders.DotNetJournalSearchAppFolder repo = CommonObjectsRes.Instance.DotNetJournalSearch;
		
		public static void _SA_SelectLookupDateRange()
		{
			int first = 0;
			int last;
						
			DotNetJournalSearch.repo.OpenStartDate.Click();
			DotNetComboBoxList.repo.Self.Items[first].Click();
			
			
			DotNetJournalSearch.repo.OpenFinishDate.Click();			
			if (DotNetComboBoxList.repo.Self.Items.Count > 1)			
			{
				last = DotNetComboBoxList.repo.Self.Items.Count -1;
			}
			else
			{
				last = 0;
			}
			DotNetComboBoxList.repo.Self.Items[last].Click();			
		}
	}
	
	
}
