/*
 * Created by Ranorex
 * User: wonga01
 * Date: 5/30/2017
 * Time: 14:58
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Threading;
using System.Diagnostics;
using Microsoft.Win32;
using Sage50.Repository;
using Sage50.Shared;
using Sage50.Types;
using System.IO;
using Ranorex;

namespace Sage50.Classes
{
	/// <summary>
	/// Description of SimplyInstall.
	/// </summary>
	public class SimplyInstall
	{
		public static SimplyInstallResFolders.InstallShieldSage50AppFolder repo = SimplyInstallRes.Instance.InstallShieldSage50;
		
		
		
		public static bool _SA_Install(string sDirToInstallFrom, string serial1, string serial2)
		{
			return _SA_Install(sDirToInstallFrom, serial1, serial2, false);
		}
		public static bool _SA_Install(string sDirToInstallFrom, string serial1, string serial2, bool bServerInstall)
		{
			string sPathToInstallFrom;
			bool bSucess = true;
			
			try
			{
				// Compose install path
				if (Directory.Exists(string.Format(@"{0}\Setup", sDirToInstallFrom)))
				{
					sPathToInstallFrom = string.Format(@"{0}\Setup\setup.exe", sDirToInstallFrom);
				}
				else if (Directory.Exists(string.Format(@"{0}\cd\setup", sDirToInstallFrom)))
				{
					sPathToInstallFrom = string.Format(@"{0}\cd\setup\setup.exe", sDirToInstallFrom);
				}
				else
				{
					Functions.Verify(false, true, "Valid install path found");
					bSucess = false;
					return bSucess;
				}
								
				// Start setup.exe
				Functions.LaunchAProgram(sPathToInstallFrom);
				
				while (!SimplyInstall.repo.SelfInfo.Exists())
				{
					Thread.Sleep(1000);
				}
				
				// Select language
				SimplyInstall.repo.SelectLanguage.Select("English");
				SimplyInstall.repo.OK.Click();	
				
				// C++ packages
				if (SimplyInstall.repo.InstallInfo.Exists())
				{
					SimplyInstall.repo.Install.Click();
					
					// Known issue: failed to install lower version C++ package, can continue install
					int x = 1;
					while (!SimplyInstall.repo.UninstallDialogTextInfo.Exists())
					{					
						if (x > 60)
						{						
							break;
						}
						Thread.Sleep(1000);					
						x++;
					}
					if (SimplyInstall.repo.UninstallDialogTextInfo.Exists())
					{
						if (SimplyInstall.repo.UninstallDialogText.TextValue.Contains(SimplyMessage.sInstall_FailedInstallPackage))
						{
							SimplyInstall.repo.Yes.Click();				
						}
					}
				}
												
				// Wait for Windows update page or Firwall or Installation page
				while (!SimplyInstall.repo.YesInfo.Exists() && !SimplyInstall.repo.NextInfo.Exists())
				{
					Thread.Sleep(300);
				}
				
				// If Windows update question pops up							
				if (SimplyInstall.repo.SelfInfo.Exists() && SimplyInstall.repo.UninstallDialogTextInfo.Exists())
				{
					if (SimplyInstall.repo.UninstallDialogText.TextValue.Contains(SimplyMessage.sInstall_WindowsUpdateMsg))
					{
						SimplyInstall.repo.Yes.Click();					
						Thread.Sleep(500);
					}
				}
				
				// If Windows firewall is on
				if (SimplyInstall.repo.InstallDialogTextInfo.Exists())
				{
					if (SimplyInstall.repo.InstallDialogText.TextValue.Contains("Firewall"))
					{
						SimplyInstall.repo.Next.Click();
					}
				}
				
				// Installation type
				// Ranorex nullexception message when using: (SimplyInstall.repo.InstallDialogText.TextValue.Contains("Type of installation") && bServerInstall)
				if (bServerInstall && SimplyInstall.repo.AdvancedBtnInfo.Exists())
				{
					SimplyInstall.repo.AdvancedBtn.Click();
					SimplyInstall.repo.Next.Click();
					
					// Next page
					SimplyInstall.repo.ServerOnlyBtn.Click();
					SimplyInstall.repo.Next.Click();
					
					// Next page
					SimplyInstall.repo.Next.Click();
				}
				else	// Full install
				{
					SimplyInstall.repo.Next.Click();
					
					// Serial number
					if (SimplyInstall.repo.InstallDialogText.TextValue.Contains("Product installation"))
					{
						SimplyInstall.repo.Serial1.TextValue = serial1;
						SimplyInstall.repo.Serial2.TextValue = serial2;
						SimplyInstall.repo.Next.Click();
					}					
				}
				
				// License agreement
				if (SimplyInstall.repo.InstallDialogText.TextValue.Contains("License agreement"))
				{
					SimplyInstall.repo.AgreeToLicenseAgreement.Click(); // need to use click to enable Install button
					SimplyInstall.repo.Install.Click();
				}
												
				// Wait for Installation to complete
				int y = 1;
				while (!SimplyInstall.repo.FinishInfo.Exists())
				{
					if (y > 1000)
					{
						bSucess = false;
						Functions.Verify(false, true, "Install - Finish button found");
						break;
					}
					Thread.Sleep(1000);
					y++;
				}
				
				// Finish
				if (bServerInstall)
				{
					SimplyInstall.repo.Finish.Click();
					while (!InstallationGuide.repo.SelfInfo.Exists())
					{
						Thread.Sleep(500);
					}
					InstallationGuide.repo.Self.Close();				
				}
				else
				{
					SimplyInstall.repo.OpenReadMe.SetState(false);
					SimplyInstall.repo.OpenSage50.SetState(false);
					SimplyInstall.repo.Finish.Click();
				}
				
			}
			
			catch (Exception e)
			{
				Ranorex.Report.Info(e.Message);
				bSucess = false;
			}
			
			if (bSucess)
			{
				Ranorex.Report.Info(string.Format("Program installed successfully to {0}", Simply._SA_GetProgramPath()));
			}
			
			Functions.Verify(bSucess, true, "Program installed successfully");
			
			return bSucess;
		}
	}
	
	public static class SimplyUninstall
	{
		public static SimplyInstallResFolders.InstallShieldSage50AppFolder repo = SimplyInstallRes.Instance.InstallShieldSage50;
		
		
				
		public static bool _SA_Uninstall()
		{						
			string sUninstallString;
			
			if (Directory.Exists(Variables.s64BitDirectory))
			{
				sUninstallString = String.Format(@"{0}\InstallShield Installation Information", Variables.s64BitDirectory);
			}
			else
			{
				sUninstallString = String.Format(@"{0}\InstallShield Installation Information", Variables.sProgramFilesPath);
			}
			
			bool bSuccess = true;
			
			try
			{
				string sUninstallExe = String.Format(@"{0}\{1}\setup.exe", sUninstallString, Variables.sGuid);
				// Step 1 - Start setup.exe
				// comment out during testing, if computer has UAC on.
				Functions.LaunchAProgram(sUninstallExe);
								
				while (!SimplyUninstall.repo.YesInfo.Exists() && !SimplyUninstall.repo.OKInfo.Exists() && !SimplyUninstall.repo.RemoveProgramBtnInfo.Exists())
				{
					Thread.Sleep(1000);																			
				}
				
				// Windows update question
				if (SimplyUninstall.repo.SelfInfo.Exists() && SimplyUninstall.repo.UninstallDialogText.TextValue.Contains(SimplyMessage.sInstall_WindowsUpdateMsg))
				{
					SimplyUninstall.repo.Yes.Click();
					Thread.Sleep(500);
				}
				
				// Sage 50 already installed
				if (SimplyUninstall.repo.SelfInfo.Exists() && SimplyUninstall.repo.UninstallDialogText.TextValue.Contains(SimplyMessage.sInstall_Sage50AlreadyInstalledMsg))
				{
					SimplyUninstall.repo.OK.Click();
				}
				
				// Installshield modify program dialog
				if (SimplyUninstall.repo.SelfInfo.Exists() && SimplyUninstall.repo.RemoveProgramBtnInfo.Exists())
				{
					SimplyUninstall.repo.RemoveProgramBtn.Click();
					SimplyUninstall.repo.Next.Click();
				}
				
				SimplyUninstall.repo.Yes.Click();
				
				int x = 1;
				while (!SimplyUninstall.repo.FinishInfo.Exists())
				{
					// handle Outlook related messages, when MS Office is installed
					// Did not see this message in Win10 laptop with O365
					
					if (x > 700)
					{
						Functions.Verify(false, true, "Uninstall Finish button found");
						bSuccess = false;
						break;
					}
					Thread.Sleep(1000);
					x++;										
				}
				
				// Step 6 - Click Finish
				try
				{
					// handle two Windows 8.1/10 message
					WindowsFeatures._Cancel();										
					WindowsFeatures._Cancel();
					
					SimplyUninstall.repo.Self.Activate();
					SimplyUninstall.repo.Finish.Click();										
				}
				catch (Exception ex)
				{
					Ranorex.Report.Info(ex.Message);
					bSuccess = false;
				}
				
			}
			catch (Exception ex)
			{
				Ranorex.Report.Info(ex.Message);
				bSuccess = false;
			}												
			
			// Cleanup remaining winsim folder and registry keys
			string s64CmPath = string.Format(@"{0}\winsim\ConnectionManager\SimplyConnectionManager.exe", Variables.s64BitDirectory);
			string sCmPath = string.Format(@"{0}\winsim\ConnectionManager\SimplyConnectionManager.exe", Variables.sProgramFilesPath);
			string s64TiPath = string.Format(@"{0}\winsim\ConnectionManager\Simply.SystemTrayIcon.exe", Variables.s64BitDirectory);
			string sTiPath = string.Format(@"{0}\winsim\ConnectionManager\Simply.SystemTrayIcon.exe", Variables.sProgramFilesPath);
			string s64WinsimPath = string.Format(@"{0}\winsim", Variables.s64BitDirectory);
			string sWinsimPath = string.Format(@"{0}\winsim", Variables.sProgramFilesPath);
			
			// Stop connection manager 
			if (File.Exists(s64CmPath) || File.Exists(sCmPath))
			{								
				foreach (var cm_process in Process.GetProcessesByName("SimplyConnectionManager"))
				{
					cm_process.Kill();
				}								
			}
			
			// Stop tray icon exe
			if (File.Exists(s64TiPath) || File.Exists(sTiPath))
			{
				foreach (var ti_process in Process.GetProcessesByName("Simply.SystemTrayIcon"))
				{
					ti_process.Kill();									
				}
			}
			
			// Delete winsim folder if exists
			try
			{
				if (Directory.Exists(s64WinsimPath))
				{
					Directory.Delete(s64WinsimPath, true);
				}
				else if (Directory.Exists(sWinsimPath))
				{
					Directory.Delete(sWinsimPath, true);
				}
				else
				{
					// Continue script
				}
					
			}
			catch (Exception e)
			{
				Ranorex.Report.Info(e.Message);				
			}
			
			// Verify connection manager is deleted
			if (File.Exists(s64CmPath) || File.Exists(sCmPath))
			{
				Functions.Verify(false, true, "SimplyConnectionManager.exe deleted");
				bSuccess = false;
				return bSuccess;
			}
			
			// Delete Sage 50 registry keys in HKLM
			if (Directory.Exists(Variables.s64BitDirectory))
			{
				Registry.LocalMachine.DeleteSubKeyTree(@"Software\Wow6432Node\Sage Software");				    
			}
			else if (Directory.Exists(Variables.sProgramFilesPath))
			{
				Registry.LocalMachine.DeleteSubKeyTree(@"Software\Sage Software");
			}
			else
			{
				Functions.Verify(false, true, "Program Files folder found");
			}
												
			return bSuccess;
		}
		
	}
	
	public class Register
	{
		public static SimplyInstallResFolders.RegistrationAppFolder repo = SimplyInstallRes.Instance.Registration;
		
		public static bool _SA_Register(string sCompanyName, string sSerial1, string sSerial2, string sClientID)
		{
			return _SA_Register(sCompanyName, sSerial1, sSerial2, sClientID, null, null, null, null, null);
		}
		public static bool _SA_Register(string sCompanyName, string sSerial1, string sSerial2, string sClientID, string sKeyCode1, string sKeyCode2, string sKeyCode3, string sKeyCode4, string sKeyCode5)		
		{
			bool bEnterKeyCode = true;
			bool bSuccess = true;
			
			if (!Functions.GoodData(sKeyCode1) || !Functions.GoodData(sKeyCode2) || !Functions.GoodData(sKeyCode3) || !Functions.GoodData(sKeyCode4) || !Functions.GoodData(sKeyCode5))
			{
				bEnterKeyCode = false;
			}
		
			while (!Register.repo.ActivateInfo.Exists())
			{
				Thread.Sleep(500);
			}
			
			Register.repo.Activate.Click();
			
			while (!ProductActivation.repo.SelfInfo.Exists())
			{
				Thread.Sleep(500);
			}
			
			ProductActivation.repo.CompanyName.TextValue = sCompanyName;
			ProductActivation.repo.Serial1.TextValue = sSerial1;
			ProductActivation.repo.Serial2.TextValue = sSerial2;
			ProductActivation.repo.ClientID.TextValue = sClientID;
			
			if(bEnterKeyCode)
			{
				ProductActivation.repo.UseThisKeyCode.Click();
				ProductActivation.repo.KeyCode1.TextValue = sKeyCode1;
				ProductActivation.repo.KeyCode2.TextValue = sKeyCode2;
				ProductActivation.repo.KeyCode3.TextValue = sKeyCode3;
				ProductActivation.repo.KeyCode4.TextValue = sKeyCode4;
				ProductActivation.repo.KeyCode5.TextValue = sKeyCode5;
			}
			
			ProductActivation.repo.KeycodeOK.Click();			
			
			// handle upsku message?
			//
			
			
			ProductActivation.repo.ActivationOK.Click();	//  new separate dialog but with same header
												
			return bSuccess;
		}
	}
	
	public static class InstallationGuide
	{
		public static SimplyInstallResFolders.InstallationGuideAppFolder repo = SimplyInstallRes.Instance.InstallationGuide;
	}
	
	public static class ProductActivation
	{
		public static SimplyInstallResFolders.ProductActivationAppFolder repo = SimplyInstallRes.Instance.ProductActivation;
	}
	
	public static class WindowsFeatures
	{
		public static SimplyInstallResFolders.WindowsFeaturesAppFolder repo = SimplyInstallRes.Instance.WindowsFeatures;
		
		public static void _Cancel()
		{
			if (WindowsFeatures.repo.SelfInfo.Exists())
			{
				WindowsFeatures.repo.Self.Activate();
				WindowsFeatures.repo.Cancel.Click();
			}
		}
	}
	
	
}
