/*
 * Created by Ranorex
 * User: wonda05
 * Date: 5/4/2016
 * Time: 11:59 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Threading;
using Sage50.Repository;
using Sage50.Shared;
using Sage50.Types;
using System.IO;
using Ranorex;

namespace Sage50.Classes
{
	/// <summary>
	/// Description of SimplyClass.
	/// </summary>
	public class Simply
	{		
		public static SimplyResFolders.SimplyHomeWindowAppFolder repo = SimplyRes.Instance.SimplyHomeWindow;
		
		private static int iCmTry = 0;
		
		
		public static void Open()
		{
			
		}
		
		public static bool isEnhancedView()
		{
			return true;
		}
		
		public static string _SA_GetProgramPath()
        {
            return _SA_GetProgramPath(false);
        }

        public static string _SA_GetProgramPath(bool bNotCDN)
        {
            string sInstallPath;

            // the registry path is different between versions - 1 for CDN and 2 for US
            if (bNotCDN)
            {
                sInstallPath = Variables.sRegPath + "2";
            }
            else
            {
                sInstallPath = Variables.sRegPath + "1";
            }

            return Functions.SYS_GetRegistryValue(RegInc.HKEY_LOCAL_MACHINE, sInstallPath, "Winsim");
        }
		
		public static void _SA_SetFlavorVariables()
        {
            Variables.bAcctEd = false;
            if (Simply._SA_IsItAccountantsEdition())
            {
                Variables.bAcctEd = true;
            }
			
            if (Simply._SA_IsItFlavor(FLAVOR.ENTERPRISE))
            {
                Variables.SimplyFlavor = FLAVOR.ENTERPRISE;
            }
            else if (Simply._SA_IsItFlavor(FLAVOR.PREMIUM))
            {
                Variables.SimplyFlavor = FLAVOR.PREMIUM;
            }
            else if (Simply._SA_IsItFlavor(FLAVOR.PRO))
            {
                Variables.SimplyFlavor = FLAVOR.PRO;
            }
            else if (Simply._SA_IsItFlavor(FLAVOR.FIRST_STEP))
            {
                Variables.SimplyFlavor = FLAVOR.FIRST_STEP;                
            }
            else
            {
                Functions.Verify(false, true, "Able to set flavor variable");
            }
        }
		
		 public static bool _SA_IsItAccountantsEdition()
        {
            bool b = false;            
                       	
            if (Simply.repo.AccountantMenuItemInfo.Exists())
            {
                b = true;
            }
            return b;
        }

        public static bool _SA_IsItFlavor(FLAVOR Flavor)
        {
            bool b = false	;
            bool bFirst = false;
            string sFlavor;

            if (Simply.repo.CompanyFlavorInfo.Exists())	// toolbar doesn't exist for FirstStep
            {
                sFlavor = Simply.repo.CompanyFlavor.TextValue;
            }
            else
            {
                bFirst = true;
                sFlavor = "First Step";
            }
            switch (Flavor)
            {
                case FLAVOR.ENTERPRISE:
                    if (sFlavor.Contains("Sage 50 Quantum Accounting"))
                    {
                        b = true;
                    }
                    break;
                case FLAVOR.PREMIUM:
                    if (sFlavor.Contains("Simply Accounting Premium " + Variables.sSimplyVersionNumber))    // In Accountant's Edition, Premium is displayed on a different label in the toolbar, to be fixed if we starts using C# code. GW
                    {
                        b = true;
                    }
                    break;
                case FLAVOR.PRO:
                    if (sFlavor.Contains("Simply Accounting Pro " + Variables.sSimplyVersionNumber))
                    {
                        b = true;
                    }
                    break;
                case FLAVOR.FIRST_STEP:
                    if (bFirst)
                    {
                        b = true;
                    }
                    break;
            }
            return b;
        }
        
        public static void _SA_StartProgram()
        {
            _SA_StartProgram(false, null);
        }        
        public static void _SA_StartProgram(bool bUseSample)
        {
            _SA_StartProgram(bUseSample, null);
        }
        public static void _SA_StartProgram(bool bUseSample, string CompanyPath)    	
        {
        	// or hard wait?
        	while (!SelectCompany.repo.SelfInfo.Exists(Variables.iExistWaitTime))
        	{
        		Thread.Sleep(1000);
        	}
        	
            if(!SelectCompany.repo.Self.Visible)
            {               
            	_SA_StartSage50();
            	
//                // handle the PU download dialog
//                if (DownloadUpdate.Instance.Window.Exists())
//                {
//                    DownloadUpdate.Instance.Window.SetActive();
//                    DownloadUpdate.Instance.DownloadLater.Click();
//                }
				 
                // check if restart message shows
                // SimplyMessage._SA_HandleMessage(SimplyMessage.repo.No);
            }
			
            // we have to check this when the product is not yet registered
            // the below window only shows when company is already registered, therefore we need it to skip everything with registration window shows
            while (!SelectCompany.repo.Self.Visible)
            {
            	Thread.Sleep(1000);
            }            
                        
        	SelectCompany.repo.Self.Activate();
        	
            if (Functions.GoodData(CompanyPath))
            {
            	SelectCompany.repo.SelectAnExistingCompany.Click();
            	SelectCompany.repo.OK.Click();
                
                OpenCompany.repo.Self.Activate();
                Simply._SA_OpenCompany(CompanyPath);
            }
            else
            {
                if(bUseSample)
                {
                	SelectCompany.repo.OpenSampleCompany.Click();
                }
                else	// defaults to last company opened
                {
                	// A Recently used company radio button and Last company you worked on are the same 
                	if (SelectCompany.repo.OpenARecentlyUsedCompanyInfo.Exists())
                    {
                		SelectCompany.repo.OpenARecentlyUsedCompany.Click();
                    }
                    else	// log error
                    {
                        Functions.Verify(false, true, "The radio list item to open the last company used is found");
                    }
					
                }
                SelectCompany.repo.Self.Activate();
                SelectCompany.repo.OK.Click();
                
				// If necessary wait for CM service to start								
	            // Handle CM error, in main machine CM service is not started when opening sample db
	            while (!SimplyMessage.repo.CmMessageTextInfo.Exists())
				{	            	
	            	if (UpgradeCompany.repo.SelfInfo.Exists() || SimplyMessage.repo.SelfInfo.Exists())
					{
						break;
					}
				}
				
				if (SimplyMessage.repo.CmMessageTextInfo.Exists())	// Ranorex see select company as SimplyMessage as well
            	{
					if (SimplyMessage.repo.CmMessageText.TextValue.Contains(SimplyMessage.sCmNotRunningMsg) || SimplyMessage.repo.CmMessageText.TextValue.Contains(SimplyMessage.sCmNotFoundMsg))
					{
						try
						{														
							iCmTry++;
							// Maximum 7 tries							
							if (iCmTry > 8)
							{
								Functions.Verify(false, true, "CM service started");
							}
							else
							{
								// wait
								System.Threading.Thread.Sleep(10000);
								SimplyMessage.repo.Self.Activate();							
								// SimplyMessage.repo.TryAgain.Click();							
								SimplyMessage.repo.Close.Click();	// if tryagain doesn't work
								// Try from select company dialog again
								_SA_StartProgram(bUseSample, CompanyPath);																
							}
						}
						catch (Exception e)
						{
							Console.WriteLine(e.Message);
						}											
					}
            	}	            
                else
                {
	                // for beta
	                // SimplyMessage.Instance._SA_HandleMessage(SimplyMessage.OK_LOC);
					
	                // handle messages and stuff to get to home window
	                Simply._SA_GotoHomeWindow();
					
	                // get the build number
	                //binfo.sBuildNumber =GetSimplyBuildNumber( + Simply.Instance._SA_GetProgramPath () + "simplyaccounting.exe"); NC
					
	                // set flavor
	                Simply._SA_SetFlavorVariables();
					
		            // get version Cdn or US
		            // Simply._SA_getVersion (true);
                }
            }
        }
        
        public static void _SA_GotoHomeWindow()
        {
            // wait 10 seconds or more is needed?            
            if (UpgradeCompany.repo.SelfInfo.Exists(10000))
            {
                UpgradeCompany._SA_ConvertDatabase();
            }                       
            	
            _SA_CheckStartupMessages();
						            
            Thread.Sleep(10000);	// to make sure all ledgers info is loaded            
                      
            if (GettingStarted.repo.SelfInfo.Exists())            		
            {
            	GettingStarted.repo.Self.Activate();
                GettingStarted.repo.Show.Uncheck();
                GettingStarted.repo.Close.Click();
            }

//            if (Simply.Instance.SwitchViewLink.Text == "Switch to Classic View")
//            {
//                Simply.Instance.SwitchViewLink.Click();
//            }
        }

        public static void _SA_CheckStartupMessages()
        {
            int x = 1;

            // while (!s_desktop.Exists(Simply.SIMPLY_LOC) || !s_desktop.Exists(Simply.SWITCHVIEWLINK_LOC))
            while (!Simply.repo.SelfInfo.Exists())
            {
                if (x > 100)
                {
                    Functions.Verify(false, true, "Able to open company");
                    break;
                }
				
                // handle all the information messages (including session date), and such as tax legislation....
                if (SimplyMessage.repo.SelfInfo.Exists() && SimplyMessage.repo.OKInfo.Exists())
                {
                    // SimplyMessage._SA_HandleMessage(SimplyMessage.repo.OK);
                    SimplyMessage.repo.OK.Click();
                }
               
                if (SimplyMessage.repo.DoNotShowMeAgainInfo.Exists())
                {                    
                    // SimplyMessage._SA_HandleMessage(SimplyMessage.repo.DoNotShowMeAgain);
                    SimplyMessage.repo.DoNotShowMeAgain.Click();
                }
//                if (s_desktop.Exists(AutomaticUpdates.AUTOMATICUPDATES_LOC))
//                {
//                    AutomaticUpdates.Instance.CheckForProductUpdates.Uncheck();
//                    AutomaticUpdates.Instance.OK.Click();
//                }
//                if (s_desktop.Exists(HSTUpdateWizard.HSTUPDATEWIZARD_LOC))
//                {
//                    HSTUpdateWizard.Instance.HowDoYouWantToProceed.Select("I have already updated my tax information");
//                    HSTUpdateWizard.Instance.Finish.Click();
//                }
                // New install PEP message
                if (PEPNotification.repo.SelfInfo.Exists())
                {
                	PEPNotification.repo.OK.Click();
                }

//                if (s_desktop.Exists(SimplyMessage.SURVEYCHOICELIST_LOC))	// Net Survey dialog
//                {
//                    SimplyMessage.Instance.SurveyChoiceList.Select("No, do not ask me again.");
//                    SimplyMessage.Instance._SA_HandleMessage(SimplyMessage.OK_LOC);
//                }                
                
                Thread.Sleep(1000);
                x++;
            }
        }
        
        public static void _SA_CloseProgram()
        {        	
        	Simply.repo.Self.Activate();
        	Simply.repo.File.Click();
        	Simply.repo.Exit.Click();
        	
        	int x = 1;        	
        	while (Simply.repo.SelfInfo.Exists())
        	{
        		if (x > 100)
        		{
        			Functions.Verify(false, true, "Sage50 closed");
        		}
        		Thread.Sleep(1000);
        		
        		// Backup dialog
        		if (SimplyMessage.repo.NoRadioBtnInfo.Exists())
        		{
        			SimplyMessage.repo.NoRadioBtn.Click();
        			SimplyMessage.repo.OK.Click();
        		}
        		// Update Sage 50 dialog
        		if (SimplyMessage.repo.InstallPromptTextInfo.Exists())
        		{
        			if (SimplyMessage.repo.InstallPromptText.TextValue.Contains(SimplyMessage.sInstallPromptMsg))
        			{
        				SimplyMessage.repo.Cancel.Click();
        			}
        		}
        		
        		x++;
        	}
        }
        
        public static void _SA_OpenCompany(string Path)
    	{		
        	OpenCompany._SA_OpenCompany(Path);
    	}
        
        public static void _SA_StartSage50()
        {
        	string sStartupLocation = Simply._SA_GetProgramPath() + Variables.sExecutable;
            Functions.LaunchAProgram(sStartupLocation);
            Thread.Sleep(1000);
			            
        }
        
        public static bool _SA_TurnOffAutoUpdate()
        {
        	return _SA_TurnOffAutoUpdate(false);
        }
        public static bool _SA_TurnOffAutoUpdate(bool bAutoDownloadOff)
        {
        	bool bSuccess;
        	
        	try
        	{
        		Simply.repo.Self.Activate();
	        	Simply.repo.Help.Click();
	        	Simply.repo.AboutSage50.Click();
	        	
	        	AboutSage50Dialog.repo.Support.Click();
	        	SimplyMessage.repo.ProductUpdateSettings.Click();
	        	if (SimplyMessage.repo.AutoInstallUpdate.Checked)
	        	{
	        		SimplyMessage.repo.AutoInstallUpdate.Uncheck();
	        	}
	        	if (bAutoDownloadOff)
	        	{
	        		SimplyMessage.repo.AutoDownloadUpdate.Uncheck();
	        	}
	        	SimplyMessage.repo.Yes.Click();	// Save has the same locator as Yes
	        	SimplyMessage.repo.OK.Click();
	        	AboutSage50Dialog.repo.OK.Click();
	        	
	        	// Wait for home window focus
	        	while (!Simply.repo.Self.Active)
	        	{
	        		Thread.Sleep(500);        		
	        	}
	        	bSuccess = true;
        	}
        	catch (Exception e)
        	{
        		Ranorex.Report.Info(e.Message.ToString());
        		bSuccess = false;
        	}        	        		
        	        	
        	return bSuccess;        	
        }
        
        public static void _SA_FinishHistory()
        {
        	Simply.repo.Self.Activate();
        	Simply.repo.History.Click();
        	Simply.repo.FinishEnteringHistory.Click();
        	SimplyMessage._SA_HandleMessage(SimplyMessage.repo.Proceed, SimplyMessage.sFinishHistoryMsg);
        	
        	// wait
        	while (!Simply.repo.Self.Active)
        	{
        		Thread.Sleep(1000);
        	}
        	
        	Variables.bHistoryMode = false;        	
        }
        
        public static void _Print_T5018Summary(string sCraNum)
        {
        	Simply.repo.Self.Activate();
        	Simply.repo.Reports_Item.Click();
        	Simply.repo.Rpt_Item_Payables.Click();
        	Simply.repo.Rpt_PrintT5018Slips.Click();
        	        	
        	T5018Options.repo.T5018Statements.Uncheck();
        	T5018Options.repo.CraBusinessNo.TextValue = sCraNum;
        	T5018Options.repo.Print.Click();
        	
        	// Save CRA number message
        	if (SimplyMessage.repo.SelfInfo.Exists())
        	{
        		if (SimplyMessage.repo.MessageText.TextValue.Contains(SimplyMessage.sUpdateCraNumMsg))
        		{
        			SimplyMessage._SA_HandleMessage(SimplyMessage.repo.Yes);
        		}
        	}
        	
        	// Printing changes message        	
        	if (SimplyMessage.repo.SelfInfo.Exists())
        	{
        		if (SimplyMessage.repo.PrintChgT5018MsgText.TextValue.Contains(SimplyMessage.sPrintChgT5018Msg))
        		{
        			SimplyMessage._SA_HandleMessage(SimplyMessage.repo.Continue);
        		}
        	}
        	
        	// send to File automatically.         	        	
        }
        
        public static void _SA_StartNewYear()
        {
        	_SA_StartNewYear(false);
        }
        public static void _SA_StartNewYear(bool bClearDataFlag)
        {        	
        	try
        	{        		
        		// Open dialog
        		Simply.repo.Self.Activate();
        		Simply.repo.MaintenanceMenu.Click();
        		Simply.repo.StartNewYearMenuItem.Click();
        		
        		// When fiscal year and calendar year are different
        		if (StartNewYear.repo.SelfInfo.Exists())        			
        		{        			
        			StartNewYear.repo.FiscalYear.Click();
        			StartNewYear.repo.OK.Click();
        		}
        		        		
        		// If fiscal and calendar year are the same, this is the first dialog
        		if (SimplyMessage.repo.CmMessageTextInfo.Exists(Variables.iExistWaitTime))
        		{
        			if (SimplyMessage.repo.CmMessageText.TextValue.Contains(SimplyMessage.sStartNewFiscalAndCalendarYear))
        			{
        				SimplyMessage.repo.NoRadioBtn.Click();
        				SimplyMessage.repo.OK.Click();
        			}
        		}
        		
        		// Wait for the Success message
        		while (!SimplyMessage.repo.CmMessageText.TextValue.Contains(SimplyMessage.sSuccessfullyStartedNewYear))
        		{
        			System.Threading.Thread.Sleep(1000);
        		}
        		
        		SimplyMessage._SA_HandleMessage(SimplyMessage.repo.OK, SimplyMessage.sSuccessfullyStartedNewYear, true, false);
        		        		        		        		        		        		
        		// Clear existing data or not
        		if (bClearDataFlag)
        		{
        			SimplyMessage._SA_HandleMessage(SimplyMessage.repo.Yes, SimplyMessage.sClearData, false, false);
        		}
        		else
        		{
        			SimplyMessage._SA_HandleMessage(SimplyMessage.repo.No, SimplyMessage.sClearData, false, false);
        		}
        		
        		// HST Tax information wizard. Not sure if this is still available
        		//
        	}
        	catch (Exception ex)
        	{
        		Ranorex.Report.Info(ex.Message.ToString());        		
        	}        	        	
        }
        
        
	}
	
	public static class GettingStarted
	{
		public static SimplyResFolders.GettingStartedAppFolder repo = SimplyRes.Instance.GettingStarted;
		
	}
	
	public static class SelectCompany
	{
		public static SimplyResFolders.SelectCompanyAppFolder repo = SimplyRes.Instance.SelectCompany;
				
	}
	
	public static class UpgradeCompany
	{
		public static SimplyResFolders.UpgradeCompanyAppFolder repo = SimplyRes.Instance.UpgradeCompany;
		
		public static void _SA_ConvertDatabase()
		{			
			UpgradeCompany.repo.Self.Activate();
			UpgradeCompany.repo.AutoBackup.Uncheck();
			UpgradeCompany.repo.Start.Click();
		}
	}
	
	public static class ChangeSessionDate
	{
		public static SimplyResFolders.ChangeSessionDateAppFolder repo = SimplyRes.Instance.ChangeSessionDate;
		
	}
	
	public static class PEPNotification
	{
		public static SimplyResFolders.PEPNotificationAppFolder repo = SimplyRes.Instance.PEPNotification;
		
	}
	
	public static class AboutSage50Dialog
	{
		public static SimplyResFolders.AboutSage50DialogAppFolder repo = SimplyRes.Instance.AboutSage50Dialog;		
	}
	
	public static class T5018Options
	{
		public static SimplyResFolders.T5018SlipsOptionsAppFolder repo = SimplyRes.Instance.T5018SlipsOptions;
		
	}
	
	public static class DotNetComboBoxList
	{
		public static SimplyResFolders.DotNetComboBoxListAppFolder repo = SimplyRes.Instance.DotNetComboBoxList;
		
	}
	
	public static class StartNewYear
	{
		public static SimplyResFolders.StartNewYearAppFolder repo = SimplyRes.Instance.StartNewYear;
	}
}
