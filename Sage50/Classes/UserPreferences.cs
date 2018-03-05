/*
 * Created by Ranorex
 * User: wonda05
 * Date: 10/4/2017
 * Time: 11:21 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
// using System.Windows.Forms;
using System.Diagnostics;
using Sage50.Types;
using Sage50.Shared;
using Sage50.Classes;
using Sage50.Repository;
using Ranorex;
using System.Data;
using System.IO;

namespace Sage50.Classes
{
	/// <summary>
	/// Description of UserPreferences.
	/// </summary>
    public class UserPreferences
    {
    	
    	public static UserPreferencesResFolders.UserPreferencesAppFolder repo = UserPreferencesRes.Instance.UserPreferences;
    	public static Sage50.Repository.UserPreferencesRes repo2 = Sage50.Repository.UserPreferencesRes.Instance;
       	
    	public static void _SA_setUserPreferences()
		{
			
			
			Ranorex.Report.Info("Setting user preferences...");
			
		
			Simply.repo.Self.Activate();
			Simply.repo.SetupMenu.Click();
			Simply.repo.UserPrefMenuItem.Click();
			
			
			if (!UserPreferences.repo.SelfInfo.Exists())	// log the error and return
			{
				Functions.Verify(false, true, "User Preferences TestObject is found");
				return;
			}
			else
			{
				UserPreferences.repo.Self.Activate();



               

				//////////////////////////
				// Options tab //
				//////////////////////////
                UserPreferences.repo.UserPrefTree.Options.Select();
                UserPreferences.repo.Options.UseAccountingTerms.Select();
                if(UserPreferences.repo.Options.InTheHomeWindowOpenRecordAndTransInfo.Exists())
                {
					UserPreferences.repo.Options.DoubleClick.Select();
                }
				
				UserPreferences.repo.Options.ShowAListOfInventoryItems.Check();
				
				
				// Set refresh flag accordingly
				if(Variables.refreshFlag)
				{
					if(UserPreferences.repo.Options.CalculateRecordBalances.Checked == false)
					{
						UserPreferences.repo.Options.CalculateRecordBalances.Check();
					}
					UserPreferences.repo.Options.AutomaticallyRefreshRecordBalances.Check();
					
				}
				else
				{
					if(UserPreferences.repo.Options.CalculateRecordBalances.Checked == true)
					{
						UserPreferences.repo.Options.CalculateRecordBalances.Uncheck();
					}
				}
				
				/////////////////////
				// View tab //
				/////////////////////
				// if (pSelectTreeItem(UserPreferences.Options.TreeView1, "/View" ))
				UserPreferences.repo.UserPrefTree.View.Select();
				
				// Turn off DBM at startup and after changing session date
				UserPreferences.repo.View.ShowAtStartupDBM.Uncheck();

				UserPreferences.repo.View.ShowAfterSessionDateDBM.Uncheck();
				//FunctionsLib.Options(true); DW
				
				// Turn off Checklists at startup and after changing sessiond ate
				UserPreferences.repo.View.ShowAtStartupChklst.Uncheck();
				//FunctionsLib.Options(false);
				UserPreferences.repo.View.ShowAfterSessionDateChklst.Uncheck();
				
				// Turn off Automatic Advice
				UserPreferences.repo.View.AutomaticAdvice.Uncheck();
				
				// Turn on the option to show session date dialog at Startup
				UserPreferences.repo.View.ShowChangeSessionDateAtStartup.Uncheck();
				//FunctionsLib.Options(true);
				
				// Turn on all the modules in home window
				UserPreferences.repo.View.GLMod.Check();
				UserPreferences.repo.View.VLMod.Check();
				UserPreferences.repo.View.CLMod.Check();
				UserPreferences.repo.View.ELMod.Check();
				UserPreferences.repo.View.ISLMod.Check();
				UserPreferences.repo.View.PLMod.Check();
				UserPreferences.repo.View.PLicon.Check();
				
				

				
                // timeslips will not be in pro version
                if (UserPreferences.repo.View.TimeBillModInfo.Exists())
                {
                	UserPreferences.repo.View.TimeBillMod.Check();
                }
				
				// Check to set flag for if Time and Billing exists, ie. in Job Category the time slip column does not exist thus container is diff
				if (UserPreferences.repo.View.TimeBillModInfo.Exists())
				{
					Variables.bTimeBillingEnabled = true;
				}
				else
				{
					Variables.bTimeBillingEnabled = false;
				}
				
				// else
				{
					// Setup_Message(4)
				}
				
				//////////////////////////////////////////////////////////
				// Transaction Confirmation tab //
				//////////////////////////////////////////////////////////
				// Not working properly in US right now so comment out, it checks it when it's suppose to uncheck
			    UserPreferences.repo.UserPrefTree.TransactionConfirmation.Select();
				UserPreferences.repo.TransConfirm.DisplayTransConfirmationMsg.Uncheck();
			
				UserPreferences.repo.OK.Click ();
			}
		}

       
    	
    	// CREATED this method to get the Transaction confirmation, as when running US audit, this seems to get checked on during the run some how.
		// Doing this check before each test to narrow it down.
		public static void _SA_getUserPreferences(string testname)
		{
			//////////////////////////////////////////////////////////
			// Transaction Confirmation tab //
			//////////////////////////////////////////////////////////
			
	
			Simply.repo.Self.Activate();
            UserPreferences.repo.UserPrefTree.TransactionConfirmation.Select();
			
			if (UserPreferences.repo.TransConfirm.DisplayTransConfirmationMsg.Checked == true)
			{
				Ranorex.Report.Info("Setting user preferences...");
				//pWriteToLogFile("c:\\TransactionConfirmation.txt", FunctionsLib.PrintfToString(testname + " -  true"));  DW
			}
			else
			{
				// pWriteToLogFile("c:\\TransactionConfirmation.txt", FunctionsLib.PrintfToString(testname + " -  false"));DW
			}
			
			
			UserPreferences.repo.Cancel.Click ();
		}
    	
    	
    	
    }
}
