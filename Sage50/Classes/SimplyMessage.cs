/*
 * Created by Ranorex
 * User: wonga01
 * Date: 9/19/2016
 * Time: 17:40
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using Sage50.Repository;
using Sage50.Shared;

namespace Sage50.Classes
{
	/// <summary>
	/// Description of SimplyMessage.
	/// </summary>
	public class SimplyMessage
	{				
		public static SimplyMessageResFolders.SimplyMessageAppFolder repo = SimplyMessageRes.Instance.SimplyMessage;				
		
		// Install/Uninstall message dialog
		public const string sInstall_WindowsUpdateMsg = "An update has been detected on your computer, which may affect one";
		public const string sInstall_Sage50AlreadyInstalledMsg = "You currently have Sage 50 installed. If you want to install a different Sage 50";
		public const string sInstall_FailedInstallPackage = "Redistributable Package (x86) appears to have failed. Do you want to continue the installation?";
		
		// Home Window
		public const string sInstallPromptMsg = "An update to your Sage 50 solution has been downloaded and is ready to be installed";
		public const string sFinishHistoryMsg = "You are about to finish entering history.";
		public const string sUpdateCraNumMsg = "You have updated your business account number.  Do you want these changes saved and used next time?";
		public const string sPrintChgT5018Msg = "T5018 slips are now printed on plain paper, rather than pre-printed government forms";
		
		// CM
		public const string sCmNotRunningMsg = "The Sage 50 Connection Manager is installed but it isn’t running on the computer where you store your data.";
		public const string sCmNotFoundMsg = "Sage 50 cannot find the Sage 50 Connection Manager.";
		
		// Journals - Generic
		public const string sDiscardJournalMsg = "Are you sure you want to discard this transaction?";
		public const string sChqNumIsOutOfSequence = "The cheque number you entered is greater than the next number in your cheque numbering sequence.";
		
		// Sales Journal
		public const string sInvoiceLogoMsg = "You can include a company logo on your invoices.";
				
		
		// Payment Journal
		public const string sReversePaymentMsg = "Are you sure you want to reverse this payment?";
		
		// Receipt Journal
		public const string sReverseReceiptMsg = "Are you sure you want to reverse this receipt?";
		
		// Payroll Journal
		public const string sNonPositiveAmountMsg = "This cheque does not have a positive amount";
		public const string sNonValidFormulaMsg = "The payroll formulas being used are valid only for";
		public const string sNoRecordedHoursMsg = "you have not recorded any hours";
		public const string sChangesMadeToPaychqMsg = "s paycheque that require taxes to be recalculated.";
		public const string sPaychqNumOutOfSequenceMsg = "The cheque number you entered is out of sequence. Would you like to use the next available cheque number instead?";
		
		// Start new year
		public const string sStartNewFiscalAndCalendarYear = "Your data is about to be updated for a new fiscal and calendar year";
		public const string sSuccessfullyStartedNewYear = "You have successfully started a new fiscal and calendar year.";
		public const string sClearData = "The program is about to clear old data from the system.  Do you want to clear this data?";
		public const string sSuccessfullyStartedCalendarYear = "You have successfully started a new calendar year.";
		public const string sSuccessfullyStartedFiscalYear = "You have successfully started a new fiscal year.";
			
		
		public static void _SA_HandleMessage(Ranorex.Button button)
		{
			_SA_HandleMessage(button, null, false, false);
		}
		
		public static void _SA_HandleMessage(Ranorex.Button button, Ranorex.Text message)
		{
			_SA_HandleMessage(button, message.TextValue, false, false);
		}

		public static void _SA_HandleMessage(Ranorex.Button button, string message)
		{
			_SA_HandleMessage(button, message, false, false);
		}			
						
		public static void _SA_HandleMessage(Ranorex.Button button, Ranorex.Text message, bool bErrorIfWrongOrNoMessage)
		{
			_SA_HandleMessage(button, message.TextValue, bErrorIfWrongOrNoMessage, false);
		}				
		
		public static void _SA_HandleMessage(Ranorex.Button button, string message, bool bErrorIfWrongOrNoMessage)
		{
			_SA_HandleMessage(button, message, bErrorIfWrongOrNoMessage, false);
		}
		
		public static void _SA_HandleMessage(Ranorex.Button button, string message, bool bErrorIfWrongOrNoMessage, bool bQuitFunctionOnWrongMessage)
		{
			Boolean bContinue = true;
			Boolean bPrintMsg = true;
//			Boolean bMsg = false;
//			
//			if(SimplyMessage.repo.MessageTextInfo.Exists())
//			{
//				bMsg = true;
//			}
						
			if(SimplyMessage.repo.SelfInfo.Exists())
			{
				if(Functions.GoodData(message))
				{
					// compare text
					if (!SimplyMessage.repo.MessageText.TextValue.Contains(message))
					{
						if (bQuitFunctionOnWrongMessage)
						{
							bContinue = false;
						}
						else if (bErrorIfWrongOrNoMessage)
						{
							Functions.Verify(false, true, "Correct message text appears");
							bPrintMsg = false;
						}
					}
				}
				if (bPrintMsg)
				{
					// print out message for debugging
				}
				if (bContinue)
				{
					try
					{
						button.Click();
					}
					catch
					{
						Functions.Verify(false, true, "Able to click on the " + button.ToString() + " button");
					}
				}
				
			}
			else
			{
				if (bErrorIfWrongOrNoMessage)
				{
					Functions.Verify(false, true, "Message pop up as expected");
				}
			}
			
			// In some sceneriosthere are more than one message						
		}
	}
}
