/*
 * Created by Ranorex
 * User: wonga01
 * Date: 5/26/2017
 * Time: 14:29
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
	/// Description of OpenCompany.
	/// </summary>
	public class OpenCompany
	{
		public static OpenCompanyResFolders.OpenCompanyAppFolder repo = OpenCompanyRes.Instance.OpenCompany;
					
		public static void _SA_OpenCompany(string dbPath)
		{            
			if (!OpenCompany.repo.SelfInfo.Exists())
			{				
				OpenCompany._Invoke();
				
				SimplyMessage._SA_HandleMessage(SimplyMessage.repo.Yes, SimplyMessage.repo._Msg_AreYouSureYouAreFinishedWithThisCompany);				
				
				if (SimplyMessage.repo._Msg_DoyouwanttobackupthiscompanybeforeclosingInfo.Exists())                
				{
					SimplyMessage.repo.NoRadioBtn.Click();
					SimplyMessage.repo.DoNotAskMeAgain.Check();
					SimplyMessage.repo.OK.Click();					
				}
			}
            
			OpenCompany.repo.FileName.TextValue = dbPath;
			OpenCompany.repo.Open.Click();
			
			// if necessary convert db, then handle messages to get to home window
			Simply._SA_GotoHomeWindow();
			
			// set flavor
			if ((!Functions.GoodData(Variables.bAcctEd)) || (Variables.bAcctEd))
			{
				Simply._SA_SetFlavorVariables ();
			}

            // if we don't do this it will populate with previous db
            Settings._SA_GetCompanyInformation();
		}
		
		public static void _Invoke()
		{
			Simply.repo.File.Click();
			Simply.repo.OpenCompany.Click();
		}
	}
}
