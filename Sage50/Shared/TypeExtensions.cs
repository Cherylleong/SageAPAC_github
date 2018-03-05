/*
 * Created by Ranorex
 * User: wonda05
 * Date: 8/26/2016
 * Time: 11:15 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ranorex;

namespace Sage50.Shared
{
	/// <summary>
	/// Description of Class1.
	/// </summary>
	public static class TypeExtensions
	{
		public static void SetState(this Ranorex.CheckBox checkBox, bool? state)
		{
			if(state == true)
			{
				checkBox.Check();
			}
			else
			{
				checkBox.Uncheck();
			}
		}
		
		
		public static void Select(this Ranorex.ComboBox combobox, string item)
		{
			
			// create button
			Ranorex.Button mybutton = combobox.FindSingle(combobox.GetPath().ToString() + "/button[@accessiblename='Open' or @text='>']");
			mybutton.Click();

			// create list item to click on
	        Ranorex.ListItem myList = combobox.FindSingle(String.Format("/list[@controlid='1000']/listitem[@text='{0}']",item));
            myList.Click();
			
		}
		
		public static void SelectListItem (this Ranorex.List aList, string item)
		{			
			int idx = 0;			
			
			foreach(ListItem currentItem in aList.Items)
			{
				if (currentItem.Text == item)
				{
					break;
				}
				idx++;
			}
			aList.Items[idx].Select();
		}
		
		// To be tested
		public static void SelectTreeItem (this Ranorex.Tree aTree, string item)
		{
			aTree[item].Click();
		}
		
		
		public static void RandPick(this Ranorex.ComboBox combobox)
        {
            Random randNum = new Random();
            int r = randNum.Next(combobox.Items.Count-1);
            combobox.Select(combobox.Items[r].Text);
        }
		
		
		public static string RandPick(this Ranorex.Tree tree)
		{
			return RandPick(tree, false);
		}
		public static string RandPick(this Ranorex.Tree tree, bool bAccountNumber)
        {
            Random randNum = new Random();
            int r = randNum.Next(1,tree.Items.Count-1);
            tree.Items[r].Select();
            
            string sAccountNum;
            if (bAccountNumber)
            {            	
            	// get account number
            	sAccountNum = System.Text.RegularExpressions.Regex.Replace(tree.Items[r].ToString(), @"[^\d]", "");            	
            }
           	else
           	{
           		sAccountNum = null;
           	}
            return sAccountNum;            	            
        }

//		
//		public static void Select(this Ranorex.ComboBox combobox, string item)
//        {
//        	int index = 0;
//        	
//        	IList<ListItem> lTemp = combobox.Items;
//        	
//        	foreach(ListItem currentItem in lTemp)
//        	{
//        		if(currentItem.Text == item)
//        		{
//        			break;
//        		}
//        		index++;
//        	}
//        	combobox.Items[index].Select();	
//        }
		
		public static string GetFieldText(this Ranorex.Text fieldtext, string elementNum)
		{											
			return Text.FromElement(String.Format("?/?/element[@controlid='{0}']/text[@accessiblerole='Text']",elementNum)).TextValue;			
		}
		
		public static void SetFieldText(this Ranorex.Text fieldtxt, string elementNum, string txt)
		{
			Text.FromElement(String.Format("?/?/element[@controlid='{0}']/text[@accessiblerole='Text']",elementNum)).PressKeys(txt);
		}
		
		
		#region Container Extensions
	
		
		
		public static List<List <string>> GetContents(this Ranorex.Unknown container)
		{
			return GetContents(container, true);
		}
		
		public static List<List <string>> GetContents(this Ranorex.Unknown container, bool bRemoveHeader)
		{
			if (container.Visible)
            {		
				Ranorex.NativeWindow containerText = new Ranorex.NativeWindow(container);
				
				
                // gets all non-blank lines from container
                // if a container has blank lines in the middle, may cause problems, just remove option in call
                List<string> lsRows = containerText.WindowText.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries).ToList(); // split the string into an array of rows separated by new line characters

                // removes the container header line
                if (bRemoveHeader)
                {
                    lsRows.RemoveAt(0);
                }

                // split fields from each row                                                            
                List<List <string>> contents = new List<List <string>>(); // contents contains rows and columns
            
                foreach(string currentString in lsRows)
                {            	
                	contents.Add(currentString.Split(new char[] { '\t' }).ToList()); // split each row into cells separated by tabs
                }

                return contents;
            }
            else
            {
                return null;
            }	
		}
		
		
		public static void ClickFirstCell(this Ranorex.Unknown container)
		{
			container.Click("49;26");
		}
		
		public static void ClickFirstCell(this Ranorex.Unknown container, Location loc)
		{
			container.Click(loc);
		}
		
		public static void SetToLine(this Ranorex.Unknown container, int rowNumber)
		{
			container.ClickFirstCell();
			
			for (int i = 0; i < rowNumber; i++)
            {
                container.PressKeys("{Down}");
            }
			
		}
		
		public static void Toggle(this Ranorex.Unknown container)
		{
			container.PressKeys("{Space}");
		}
		
		public static void MoveRight(this Ranorex.Unknown container)
		{
			container.PressKeys("{Tab}");
		}
		public static void MoveRight(this Ranorex.Unknown container, int moveNum)
		{
			for (int i = 0; i < moveNum; i++)
			{
				container.PressKeys("{Tab}");
			}
		}
		
		public static void MoveLeft(this Ranorex.Unknown container)
		{
			container.PressKeys("{Shift down}{Tab}{Shift up}");
		}
		public static void MoveLeft(this Ranorex.Unknown container, int moveNum)
		{
			for (int i = 0; i < moveNum; i++)
			{
			container.PressKeys("{Shift down}{Tab}{Shift up}");
			}
		}
		
		public static void MoveDown(this Ranorex.Unknown container)
		{
			container.PressKeys("{Down}");
		}
		
		public static void MoveDown(this Ranorex.Unknown container, int moveNum)
		{
			for (int i = 0; i < moveNum; i++)
			{
				container.PressKeys("{Down}");
			}
		}
		
		public static void MoveToField(this Ranorex.Unknown container, int rowNum, int colNum)
		{
			container.SetToLine(rowNum);
			
			for (int i = 0; i < colNum; i++)
			{
				container.PressKeys("{Tab}");
			}
		}
		
		public static void SetText(this Ranorex.Unknown container, string text)
		{
			if(Functions.GoodData(text))
			{
				container.PressKeys(text);
			}
		}				
		
		
		#endregion
		

		#region .Dot grids
		
		public static void SelectCell(this Ranorex.Table grid, string cellName, int rowNumber)
		{	
			SelectCell(grid, cellName, rowNumber, "");
		}
		
		public static void SelectCell(this Ranorex.Table grid, string cellName, int rowNumber, string text)
		{
			rowNumber--;
			
			Ranorex.Cell myCel = grid.FindSingle(String.Format("?/?/cell[@accessiblename='{0} Row {1}']",cellName, rowNumber.ToString()));
			// myCel.Click();
			// myCel.ClickWithoutBoundsCheck(Location.Center);
			myCel.ClickWithoutBoundsCheck(Location.CenterLeft);
			
			if(text != "")
			{
				myCel.PressKeys(text);				
			}
		}
				
		public static List<List <string>> GetContents(this Ranorex.Table table)
		{
			if (table.Visible)
            {		
				List<List <string>> contents = new List<List <string>>(); // contents contains rows and columns
				int rows = table.Rows.Count; // Get total rows first
				
				rows--;
				
				for(int x = 0; x < rows; x++)
				{
					// get text from row
					Accessible accValue = new Accessible(table.FindSingle(string.Format("/?/?/row[@accessiblename='Row {0}']",x)));
			    	string tableValues = accValue.Value;
				
			    	// remove all "(null)" here
			    	// figure out how to remove space
			    	tableValues = tableValues.Replace("(null)"," ");
               
                	// split line into a list and add into list of list
                	List<string> lsRow = tableValues.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).ToList(); // split the string into an array of rows separated by new line characters
                	contents.Add(lsRow);
				}
                return contents;
            }
            else
            {
                return null;
            }	
		}
		
		
		public static int ColumnCount(this Ranorex.Table table)
		{
			Accessible accValue = new Accessible(table.FindSingle("/?/?/row[@accessiblename='Row 0']"));
			string tableValues = accValue.Value;
               
            List<string> lsRow = tableValues.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).ToList(); // split the string into an array of rows separated by new line characters
            return lsRow.Count;
		}
		
		public static string GetCell(this Ranorex.Table grid, string cellName, int rowNumber)
		{
			rowNumber--;
			
			Ranorex.Cell myCel = grid.FindSingle(String.Format("?/?/cell[@accessiblename='{0} Row {1}']",cellName, rowNumber.ToString()));
			
			return myCel.Text;
			
			
		}
		
		#endregion
	}
}
