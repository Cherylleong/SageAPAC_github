/*
 * Created by Ranorex
 * User: wonda05
 * Date: 10/20/2017
 * Time: 3:38 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Sage50.Repository;
using Ranorex;
using Sage50.Classes;
using Sage50.Types;
using Sage50.Shared;
using System.IO;

using Ranorex.Core;
using Ranorex.Core.Testing;

namespace Sage50.Classes
{
    /// <summary>
    /// Description of GeneralJournal.
    /// </summary>
    [TestModule("B08A532C-BE64-49EA-98C7-8828C2D7F67D", ModuleType.UserCode, 1)]
    public class GeneralJournal
    {
 		public static GeneralJournalResFolders.GeneralJournalAppFolder repo = GeneralJournalRes.Instance.GeneralJournal;

 		 		
 		public static void _SA_Invoke(Boolean bOpenLedger)
		{
			// open ledger depending on view type
			
			if(Simply.isEnhancedView())
			{	
				Simply.repo.Self.Activate();
				Simply.repo.GeneralLink.Click();
				Simply.repo.GeneralJournalIcon.Click();
			}
			else
			{
				
			}
			
			if(VendorIcon.repo.SelfInfo.Exists())
			{
				if(bOpenLedger == true)
				{
					VendorIcon.repo.CreateNew.Click();
					VendorIcon.repo.Self.Close();
				}
			}			
		}
		
		public static void _SA_Invoke()
		{
			GeneralJournal._SA_Invoke(true);
			
		}	
 		
 		
		public static void _SA_Open(GENERAL_JOURNAL  GJRecord)
		{
			if (!GeneralJournal.repo.SelfInfo.Exists())
			{
				GeneralJournal._SA_Invoke();
			}
			GeneralJournal.repo.ToolBar.Adjust.Click();
			DialogJournalSearch._SA_SelectLookupDateRange();
			
			// Assume there are no diplicate sources
			DialogJournalSearch.repo.Source.TextValue = GJRecord.source;
			DialogJournalSearch.repo.OK.Click();
		}

		public static void _SA_MatchDefaults(GENERAL_JOURNAL  GJRecord)
		{
			//need to fill this in
		}

		public static void _SA_Create(GENERAL_JOURNAL GJRecord)
		{
			_SA_Create(GJRecord, true, false, false);
		}

		public static void _SA_Create(GENERAL_JOURNAL GJRecord, bool bSave)
		{
			_SA_Create(GJRecord, bSave, false, false);
		}

		public static void _SA_Create(GENERAL_JOURNAL GJRecord, bool bSave, bool bEdit)
		{
			_SA_Create(GJRecord, bSave, bEdit, false);
		}

		public static void _SA_Create(GENERAL_JOURNAL GJRecord, bool bSave, bool bEdit, bool bRecur)
		{
			
			bool bCheckGlobalProject = false;
			
			if (!GeneralJournal.repo.SelfInfo.Exists())
			{
				GeneralJournal._SA_Invoke();
			}
			
			if (bEdit)
			{
				if (GeneralJournal.repo.Source.TextValue != GJRecord.source)	// load the journal entry for editing
				{
					GeneralJournal._SA_Open (GJRecord);
				}
				Ranorex.Report.Info("Adjusting General Journal " + GJRecord.source + "");
			}
			else if (!(bRecur))
			{
				Ranorex.Report.Info("Creating General Journal " + GJRecord.source + "");
			}
			
			if (Functions.GoodData (GJRecord.source))
			{
				GeneralJournal.repo.Source.TextValue = GJRecord.source;
			}
			if (Functions.GoodData (GJRecord.journalDate))
			{
				GeneralJournal.repo.JournalDate.TextValue =  GJRecord.journalDate;
			}
			if (Functions.GoodData (GJRecord.comment))
			{
				GeneralJournal.repo.Comment.TextValue = GJRecord.comment;
			}
			
			if(Functions.GoodData(GJRecord.currCode))
			{
				if(GeneralJournal.repo.CurrencyCodeInfo.Exists())
				{
					GeneralJournal.repo.CurrencyCode.Select (GJRecord.currCode);
					if ((bEdit && Variables.bUseDataFiles))
					{
						// dw SimplyMessage.repo._SA_HandleMessage(SimplyMessage.YES_LOC, SimplyMessage._MSG_YOUHAVESELECTEDADIFFERENTFOREIGNCURRENCY_LOC, false, true);
					}
				}
			}
			if(Functions.GoodData(GJRecord.exchRate))
			{
				if(GeneralJournal.repo.ExchangeRateInfo.Exists() && Functions.GoodData (GJRecord.exchRate))
				{
					GeneralJournal.repo.ExchangeRate.TextValue = GJRecord.exchRate;
				}
			}

            // click to first cell
            GeneralJournal.repo.DetailsContainer.ClickFirstCell();

            //int x;
            if (bEdit)
            {
                //clear all the existing lines in the grid
                int x = 0;
                while (GeneralJournal.repo.DetailsContainer.GetContents().Count != 0 )
                {
                    if (x > 100)
                    {
                        Functions.Verify(false, true, "able to clear all details of table on edit");
                        break;
                    }
                    GeneralJournal.repo.DetailsContainer.PressKeys("<Alt+e>");	// to remove line
                    GeneralJournal.repo.DetailsContainer.PressKeys("r");
                    x++;
                }
            }
									
            if (Functions.GoodData (GJRecord.GridRows))
            {
                double nTotalDebit = 0;	    // total debit amount
                double nTotalCredit = 0;	// total credit amount
                //int iDelay = 2;	// sleep 2 sec

                for (int x = 0; x < GJRecord.GridRows.Count; x++)
                {
                    // enter account
                   	GeneralJournal.repo.DetailsContainer.PressKeys(GJRecord.GridRows[x].Account.acctNumber + "{Tab}");
                   
                    // enter Debit/Crecit amount
                    if(x == 0)
                    {
                        if (Functions.GoodData(GJRecord.GridRows[x].debitAmt) && GJRecord.GridRows[x].debitAmt != "0")
                        {
                            GeneralJournal.repo.DetailsContainer.PressKeys(GJRecord.GridRows[x].debitAmt);                            
                            nTotalDebit  += Convert.ToDouble(GJRecord.GridRows[x].debitAmt);
                            GeneralJournal.repo.DetailsContainer.MoveRight();
                        }
                        else
                        {
                        	GeneralJournal.repo.DetailsContainer.MoveRight();
                            if (Functions.GoodData (GJRecord.GridRows[x].creditAmt))
                            {
                                GeneralJournal.repo.DetailsContainer.SetText(GJRecord.GridRows[x].creditAmt);
                                nTotalCredit += Convert.ToDouble(GJRecord.GridRows[x].creditAmt);
                            }
                            GeneralJournal.repo.DetailsContainer.MoveRight();
                        }
                    }
                    // All lines after the first one will be depending on the total debit amount and total credit amount
                    else
                    {
                        //if((GoodData (GJRecord.GridRows[1].debitAmt) && (GJRecord.GridRows[1].debitAmt != "0")))
                        if (nTotalDebit > nTotalCredit)	// focus set to the credit field
                        {
                            if (Functions.GoodData(GJRecord.GridRows[x].creditAmt) && (GJRecord.GridRows[x].creditAmt != "0"))
                            {
                                GeneralJournal.repo.DetailsContainer.SetText(GJRecord.GridRows[x].creditAmt);
                                nTotalCredit += Convert.ToDouble(GJRecord.GridRows[x].creditAmt);
                                GeneralJournal.repo.DetailsContainer.MoveRight();
                            }
                            else
                            {
                                GeneralJournal.repo.DetailsContainer.PressKeys("{Delete}");
                                GeneralJournal.repo.DetailsContainer.PressKeys("{LShiftKey down}{Tab}{LShiftKey up}");
                                GeneralJournal.repo.DetailsContainer.SetText(GJRecord.GridRows[x].debitAmt);
                                nTotalDebit  += Convert.ToDouble(GJRecord.GridRows[x].debitAmt);
                                GeneralJournal.repo.DetailsContainer.MoveRight();
                            }
                        }
                        else	// focus set to the debit field
                        {
                            if (Functions.GoodData(GJRecord.GridRows[x].debitAmt) && (GJRecord.GridRows[x].debitAmt != "0"))
                            {
                                GeneralJournal.repo.DetailsContainer.SetText(GJRecord.GridRows[x].debitAmt);
                                nTotalDebit  += Convert.ToDouble(GJRecord.GridRows[x].debitAmt);
                                GeneralJournal.repo.DetailsContainer.MoveRight();
                            }
                            else
                            {
                                GeneralJournal.repo.DetailsContainer.PressKeys("{Delete}");
                                GeneralJournal.repo.DetailsContainer.MoveRight();
                                GeneralJournal.repo.DetailsContainer.SetText(GJRecord.GridRows[x].creditAmt);
                                nTotalCredit += Convert.ToDouble(GJRecord.GridRows[x].creditAmt);
                                GeneralJournal.repo.DetailsContainer.MoveRight();
                            }
                        }						
                    }
					
                    // enter line comment
                    if (Functions.GoodData (GJRecord.GridRows[x].lineComment))
                    {
                        GeneralJournal.repo.DetailsContainer.SetText(GJRecord.GridRows[x].lineComment);
                    }
					
                    // enter project allocation for the line
                    if (GeneralJournal.repo.AllocateToProject.Enabled && GJRecord.GridRows[x].Projects.Count > 0)
                    {
                        if (Functions.GoodData(GJRecord.GridRows[x].Projects))
                        {
                            // get global settings if haven't alreayd 
                            if (!bCheckGlobalProject)
                            {
                                Settings._SA_Get_AllProjectSettings();
                                GeneralJournal.repo.Self.Activate();
                                bCheckGlobalProject = true;
                            }

                            GeneralJournal.repo.AllocateToProject.Click();
                            
                            if (ProjectAllocationDialog.repo.SelfInfo.Exists())
                            {
                                ProjectAllocationDialog._SA_EnterProjectAllocationDetails(GJRecord.GridRows[x].Projects);
                            }
                        }
                    }
					
                    // move to the next line
                    GeneralJournal.repo.DetailsContainer.MoveRight();
                }
            }
									
//			if (bRecur && !bSave)
//			{
//				Ranorex.Report.Info("Storing the recurring entry " + GJRecord.recurrName + ", " + GJRecord.recurrFrequency + "");
//				GeneralJournal.repo.Self.PressKeys("<Ctrl+t>");
//				StoreRecurringDialog.repo._SA_DoStoreRecurring (GJRecord.recurrName, GJRecord.recurrFrequency);
//				GeneralJournal.repo.ToolBar.Undo.Click();
//			}

			if (bSave)
			{
				GeneralJournal.repo.Post.Click();
				if (Variables.bUseDataFiles)	// handle possible messages
				{
					while (!GeneralJournal.repo.Self.Enabled)
					{						
						// dw SimplyMessage.repo._SA_HandleMessage(SimplyMessage.YES_LOC, SimplyMessage._MSG_THISTRANSACTIONUSESLINKEDTAXACCOUNTS_LOC, false, true);
						// dw SimplyMessage.repo._SA_HandleMessage(SimplyMessage.YES_LOC, SimplyMessage._MSG_THISENTRYUSESLINKEDCONTROLACCOUNTS_LOC, false, true);
						// dw SimplyMessage.repo._SA_HandleMessage(SimplyMessage.YES_LOC, SimplyMessage._MSG_THERECURRINGTRANSACTIONHASBEENCHANGED_LOC, false, true);
					}
				}
                // dw SimplyMessage.repo._SA_HandleMessage(SimplyMessage.NO_LOC, SimplyMessage._MSG_DOYOUWANTTOSAVENEWEXCHANGERATE_LOC, false, true);
			}
		}

        public static GENERAL_JOURNAL _SA_Read()
        {
            return _SA_Read(null);
        }

        public static GENERAL_JOURNAL _SA_Read(string sIDToRead) //  method will read all fields and store the data in a RECEIPT record
        {
            GENERAL_JOURNAL GJE = new GENERAL_JOURNAL();
			
            if (Functions.GoodData (sIDToRead))
            {
                GJE.source = sIDToRead;
                if (GeneralJournal.repo.Source.TextValue != GJE.source)
                {
                    GeneralJournal._SA_Open (GJE);
                }
            }
			
            GJE.journalDate = GeneralJournal.repo.JournalDate.TextValue;
            GJE.comment = GeneralJournal.repo.Comment.TextValue;
            if(GeneralJournal.repo.SelfInfo.Exists())
            {
                GJE.currCode = GeneralJournal.repo.CurrencyCode.SelectedItem.Text;
            }

            GJE.GridRows.Clear();
            List<List <string>>  lsContents = GeneralJournal.repo.DetailsContainer.GetContents();
            //if (Functions.GoodData (lsContents))   NC - no longer need to check since the lsContents.Count sort of does the same thing
            //{
                for (int x = 0; x < lsContents.Count; x++)
                {
                    // check for blank line
                    if(lsContents[x][0] != "")
                    {
                        GJ_ROW GR = new GJ_ROW();
                       
                        GR.Account.acctNumber = lsContents[x][0];
							
                        if(lsContents[x][1].Trim() == "--")
                        {
                            GR.debitAmt = "0";
                        }
                        else
                        {
                            GR.debitAmt = lsContents[x][1];
								
                        }

                        if(lsContents[x][2].Trim() == "--")
                        {
                            GR.creditAmt =  "0";
                        }
                        else
                        {
                            GR.creditAmt =  lsContents[x][2];
								
                        }

                        GR.lineComment = lsContents[x][3];
							
                        GJE.GridRows.Add(GR);                        
                    }
                }
            //}
			
			
            // Get project allocations
            // Move to first cell
            GeneralJournal.repo.DetailsContainer.ClickFirstCell();
			
            if (Functions.GoodData (GJE.GridRows))
            {
                for (int x = 0; x < GJE.GridRows.Count; x++)
                {
                    if (GeneralJournal.repo.AllocateToProject.Enabled)
                    {
                        GJE.GridRows[x].Projects.Clear();
                        GeneralJournal.repo.AllocateToProject.Click();

                        if (ProjectAllocationDialog.repo.SelfInfo.Exists())
                        {
                            //List<List<string>> lsContents = ProjectAllocationDialog.repo.DataGrid.GetContents();

                            //if (Functions.GoodData (lsContents))
                            //{
                            //    for (int y = 0; y < lsContents.Count; y++)
                            //    {
                            //        if (Functions.GetField.Trim(' '), 1)) == "")
                            //        {
                            //            break;
                            //        }
                            //        else
                            //        {
                            //            PROJECT_ALLOCATION PA = new PROJECT_ALLOCATION();
                            //            PA.Project.name = Functions.Functions.GetField (lsContents[y], "	", 1);
                            //            if(Functions.GetField.Trim(' '), 2)) == "--")
                            //            {
                            //                PA.Amount = "0";
                            //            }
                            //            else
                            //            {
                            //                PA.Amount = Functions.Functions.GetField (lsContents[y], "	", 2);
                            //            }
                            //            if(Functions.GetField.Trim(' '), 3)) == "--")
                            //            {
                            //                PA.Percent = "0";
                            //            }
                            //            else
                            //            {
                            //                PA.Percent = Functions.Functions.GetField (lsContents[y], "	", 3);
                            //            }
                            //            GJE.GridRows[x].Projects.Add(PA);
                            //        }
                            //    }

                            GJE.GridRows[x].Projects = ProjectAllocationDialog._SA_GetProjectAllocationDetails();
                        }
                        ProjectAllocationDialog.repo.Cancel.Click();
                    }
                    GeneralJournal.repo.DetailsContainer.PressKeys("<Down>");
                }
            }        
									
            return GJE;
        }

		public static void _SA_Delete(GENERAL_JOURNAL GJRecord)
		{
			
			Trace.WriteLine ("Deleting the general jorunal entry " + GJRecord.source + "");
			
			GeneralJournal._SA_Open(GJRecord);
			
			GeneralJournal.repo.ToolBar.Reverse.Click();
			
			// dw SimplyMessage.repo._SA_HandleMessage(SimplyMessage.YES_LOC, SimplyMessage._MSG_AREYOUSUREYOUWANTTOREMOVE_LOC);
		}
		
		public static void _SA_Close()
		{
			System.Threading.Thread.Sleep(2000);
			GeneralJournal.repo.Self.Close();
		}

//        public void _SA_RecallRecurring(GENERAL_JOURNAL GJRecord)
//        {
//
//
//            if (Functions.GoodData(GJRecord.recurrName))
//            {
//                Trace.WriteLine("Recalling the recurring entry " + GJRecord.recurrName + "");
//
//                if (!GeneralJournal.repo.SelfInfo.Exists())
//                {
//                    GeneralJournal._SA_Invoke();
//                }
//
//                GeneralJournal.repo.Self.PressKeys("<Ctrl+r>");	// invoke Recall Recurring dialog
//                RecallRecurringDialog.repo._SA_SelectEntryToRecall(GJRecord.recurrName);
//
//                // we have to comment this out for now because of the behavior of the container.  it's different depending if there's already something there
//                // since the container is not .net, there's no way for us to know the current focus.  So, this will have to be a limitation for now where u can't edit a
//                // recalled transaction
//                //GeneralJournal._SA_Create(GJRecord)
//
//                // at least put in source and date
//                if (Functions.GoodData(GJRecord.source))
//                {
//                    GeneralJournal.repo.Source.TextValue = GJRecord.source;
//                }
//                if (Functions.GoodData(GJRecord.journalDate))
//                {
//                    GeneralJournal.repo.JournalDate.TextValue = GJRecord.journalDate;
//                }
//
//                GeneralJournal.repo.Post.Click();
//
//                if (Variables.bUseDataFiles)	// handle possible messages
//                {
//                    while (!(GeneralJournal.repo.Window.Enabled))
//                    {
//                        // dw SimplyMessage.repo._SA_HandleMessage(SimplyMessage.NO_LOC, SimplyMessage._MSG_DOYOUWANTTOSAVENEWEXCHANGERATE_LOC, false, true);
//                        // dw SimplyMessage.repo._SA_HandleMessage(SimplyMessage.YES_LOC, SimplyMessage._MSG_THISTRANSACTIONUSESLINKEDTAXACCOUNTS_LOC, false, true);
//                        // dw SimplyMessage.repo._SA_HandleMessage(SimplyMessage.YES_LOC, SimplyMessage._MSG_THISENTRYUSESLINKEDCONTROLACCOUNTS_LOC, false, true);
//                        // dw SimplyMessage.repo._SA_HandleMessage(SimplyMessage.YES_LOC, SimplyMessage._MSG_THERECURRINGTRANSACTIONHASBEENCHANGED_LOC, false, true);
//                    }
//                }
//            }
//            else
//            {
//                Functions.Verify(false, true, "recurring name found");
//            }
//        }

//        public void DataFile_ReadFile(string sDataLocation, string fileCounter)
//        {
//            StreamReader FileHDR;	// File handle for the header file
//            StreamReader FileDT1;	// File handle for the transaction detail file
//            StreamReader FilePRJ;	// File handle for the project allocation file
//
//            string dataLine;	// Stores the current field data from file
//            string dataPath;	// The name and path of the data file
//
//            // Get the data path from file
//            dataPath = sDataLocation + "GJ" + fileCounter;
//
//            List<GENERAL_JOURNAL> lGJE = new List<GENERAL_JOURNAL>();
//
//            // Only open header file
//            FileHDR = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_HEADER, "FM_READ"));
//            while ((dataLine = FileHDR.ReadLine()) != null)
//            {
//                GENERAL_JOURNAL GJE = new GENERAL_JOURNAL();
//                GJE.GridRows.Clear();
//                GeneralJournal.repo.DataFile_setDataStructure(EXTENSION_HEADER, dataLine, GJE);
//
//                // Only open transaction detail file if it exists
//                if (File.Exists(dataPath + EXTENSION_TRANS_DETAILS))
//                {
//                    using (FileDT1 = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_TRANS_DETAILS, "FM_READ")))
//                    {
//                        while ((dataLine = FileDT1.ReadLine()) != null)
//                        {
//                            GeneralJournal.repo.DataFile_setDataStructure(EXTENSION_TRANS_DETAILS, dataLine, GJE);
//                        }
//                    }
//                    //Functions.FileClose(FileDT1);
//                }
//
//                // Only open project file if it exists
//                if (File.Exists(dataPath + EXTENSION_PROJECT))
//                {
//                    using (FilePRJ = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_PROJECT, "FM_READ")))
//                    {
//                        while ((dataLine = FilePRJ.ReadLine()) != null)
//                        {
//                            GeneralJournal.repo.DataFile_setDataStructure(EXTENSION_PROJECT, dataLine, GJE);
//                        }
//                    }
//                    //Functions.FileClose(FilePRJ);
//                }
//
//                lGJE.Add(GJE);
//            }
//            FileHDR.Close();
//
//            for (int x = 0; x < lGJE.Count; x++)
//            {
//                GENERAL_JOURNAL GJTrans = lGJE[x];
//                // Determine the type
//                switch (GJTrans.action)
//                {
//                    case "CREATE":
//                        GeneralJournal._SA_Create(GJTrans);
//                        break;
//                    case "ADJUST":                        
//                        GeneralJournal._SA_Create(GJTrans, true, true);
//                        break;
//                    case "STORE_RECURRING":
//                        GeneralJournal._SA_Create(GJTrans, false, false, true);
//                        break;
//                    case "RECALL_RECURRING":
//                        GeneralJournal._SA_RecallRecurring(GJTrans);
//                        break;
//                    default:
//                        {
//                            Functions.Verify(false, true, "Valid value for action");
//                            break;
//                        }
//                }
//            }
//        }
//
//        public void DataFile_setDataStructure(string extension,string dataLine,GENERAL_JOURNAL GJRecord)
//        {       			
//            switch (extension.ToUpper())
//            {
//                case EXTENSION_HEADER:
//                    GJRecord.source = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 1));
//                    GJRecord.journalDate = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 2));
//                    GJRecord.comment = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 3));
//                    GJRecord.currCode = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 4));
//                    GJRecord.exchRate = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 5));
//                    GJRecord.action = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 6));
//                    GJRecord.recurrName = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 7));
//                    GJRecord.recurrFrequency = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 8));
//                    break;
//                case EXTENSION_TRANS_DETAILS:
//                    if ((GJRecord.source == Functions.GetField (dataLine, ",", 1)) && (GJRecord.journalDate == Functions.GetField (dataLine, ",", 2)))
//                    {
//                        GJ_ROW GR = new GJ_ROW();
//                        GR.Account.acctNumber = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 3));
//                        GR.debitAmt = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 4));
//                        GR.creditAmt = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 5));
//                        GR.alloFlag = ConvertFunctions.StringToBool(Functions.GetField (dataLine, ",", 6));
//                        GR.lineComment = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 7));
//                        GJRecord.GridRows.Add(GR);
//                    }
//                    break;
//                case EXTENSION_PROJECT:
//                    if (GJRecord.source == Functions.GetField (dataLine, ",", 1))
//                    {
//						
//                        PROJECT_ALLOCATION PA = new PROJECT_ALLOCATION();
//                        bool bFound = false;
//                        int x;
//                        for (x = 0; x < GJRecord.GridRows.Count; x++)
//                        {
//                            if ((GJRecord.GridRows[x].Account.acctNumber == Functions.GetField(dataLine, ",", 2)) && (GJRecord.GridRows[x].alloFlag == true))
//                            {
//                                bFound = true;
//                                break;
//                            }
//                        }
//                        if (bFound)	// you found the line, so now add the project allocation
//                        {
//                            PA.Project.name = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 3));
//                            PA.Amount = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 4));
//                            PA.Percent = Functions.PrepStringsFromDataFiles(Functions.GetField (dataLine, ",", 5));
//                            GJRecord.GridRows[x].Projects.Add(PA);
//                        }
//                        else
//                        {
//                            Functions.Verify(false, true, "Row found for project allocation");
//                        }
//                    }
//                    break;
//                default:
//                {
//                    Functions.Verify(false, true, "Correct file types sent");
//                    break;
//                }
//            }
//        }

        //public void DataFile_WriteFile()
        //{            
        //    bool allRecordsFlag;	    // Flag to see if all records
        //    int numberOfRecords = 1;	// The number of transactions to record
        //    string dataPath;	        // The name and path of the data file
        //    string stepCount;	        // The current step number
			
        //    // Get the step count
        //    stepCount = Functions.GetExternalData(Variables.FILE_STEP_COUNTER, "stepCounter");
			
        //    // Create the path for the data file to be stored
        //    dataPath = Functions.GetExternalData(Variables.FILE_CURRENT_FOLDER,"silkLocation") + "\\" +
        //    "data\\SA" + Variables.sSimplyVersionNumber + "\\" +
        //    Functions.GetExternalData(Variables.FILE_CURRENT_FOLDER,"userName") + "\\" +
        //    Functions.GetExternalData(Variables.FILE_CURRENT_FOLDER,"dbName") + "\\" +
        //    Functions.GetExternalData(Variables.FILE_CURRENT_FOLDER,"testName") + "\\";
			
        //    // Determine if it's for one record or all
        //    if (Functions.GetExternalData(Variables.FILE_FUNCTION_OPTION,"SingleOrAll").ToUpper() == "ALLRECORDS")
        //    {
        //        allRecordsFlag = true;
        //    }
        //    else
        //    {
        //        allRecordsFlag = false;
        //    }
			
			
        //    // Get the function action:  ADD, EDIT, or DELETE
        //    string sAction = Functions.GetExternalData(Variables.FILE_FUNCTION_OPTION, "functionOption");
			
			
        //    if (!s_desktop.Exists(GENERALJOURNAL_LOC))
        //    {
        //        GeneralJournal.repo._SA_Invoke ();
        //    }

        //    if (allRecordsFlag)
        //    {
        //        // go to adjust (can't do the lookup since all the fields are read-only in lookup mode)
        //        GeneralJournal.repo.Window.TypeKeys("<Ctrl-a>");
        //        DialogJournalSearch.repo._SA_SelectLookupDateRange();
        //        DialogJournalSearch.repo.OK.Click();

        //        // select the entry to load into the adjustment mode
        //        SelectGJEntry.repo.Descending.Click();  // having the latest on the top
        //        //SelectGJEntry.repo.LookupContainer.InitializeTable();	// Initialize lookup table
        //        numberOfRecords = SelectGJEntry.repo.LookupContainer.GetCount();//m_TotalDetailLines;   // remember the total number of records here
        //        //SelectGJEntry.repo.Select.Click();  // selecting the 1st entry. will do within the following loop now
        //    }
        //    //else we assume you are reading the record you are on
			
        //    for (int x = 0; x < numberOfRecords; x++)	// have to loop through the number of items in case you are doing all records
        //    {
        //        if (allRecordsFlag) // select entry to read
        //        {
        //            if (!s_desktop.Exists(DialogJournalSearch.JOURNALSEARCH_LOC))
        //            {
        //                GeneralJournal.repo.Window.TypeKeys("<alt-a>");
        //            }
        //            else
        //            {
        //                if (!s_desktop.Exists(SelectGJEntry.SELECTGJENTRY_LOC))
        //                {
        //                    DialogJournalSearch.repo.OK.Click();
        //                    SelectGJEntry.repo.Descending.Click();
        //                }
        //                else // select the entry for adjustment
        //                {
        //                    //SelectGJEntry.repo.LookupContainer.InitializeTable();	// Initialize lookup table                    
        //                    SelectGJEntry.repo.LookupContainer.SetToLine(x);
        //                    SelectGJEntry.repo.Select.Click();
        //                }
        //            }
        //        }

        //        GENERAL_JOURNAL GJ = GeneralJournal.repo._SA_Read ();
        //        GJ.action = sAction;
				
        //        // Create the HDR file
        //        Functions.WriteToDataFile(GeneralJournal.repo.DataFile_updateListOfStrings(EXTENSION_HEADER, GJ), dataPath, FUNCTION_ALIAS, stepCount, EXTENSION_HEADER);
        //        if (Functions.GoodData (GJ.GridRows))
        //        {
        //            for (int iCnt = 0; iCnt < GJ.GridRows.Count; iCnt++)
        //            {
        //                Functions.WriteToDataFile(GeneralJournal.repo.DataFile_updateListOfStrings(EXTENSION_TRANS_DETAILS, GJ, iCnt), dataPath, FUNCTION_ALIAS, stepCount, EXTENSION_TRANS_DETAILS);
        //                {
        //                    if (Functions.GoodData (GJ.GridRows[iCnt].Projects))
        //                    {
        //                        for (int iProj = 0; iProj < GJ.GridRows[iCnt].Projects.Count; iProj++)
        //                        {
        //                            Functions.WriteToDataFile(GeneralJournal.repo.DataFile_updateListOfStrings(EXTENSION_PROJECT, GJ, iCnt, iProj), dataPath, FUNCTION_ALIAS, stepCount, EXTENSION_PROJECT);
        //                        }
        //                    }
        //                }
        //            }
        //        }				           
        //    }
			
        //    // Close the  window if recording all records
        //    if (allRecordsFlag)
        //    {
        //        GeneralJournal.repo._SA_CloseWin ();
        //    }
        //}

//        public List<string> DataFile_updateListOfStrings(string extension, GENERAL_JOURNAL GJRecord)
//        {
//            return DataFile_updateListOfStrings(extension, GJRecord, 0, 0);
//        }
//
//        public List<string> DataFile_updateListOfStrings(string extension, GENERAL_JOURNAL GJRecord, int iGridLine)
//        {
//            return DataFile_updateListOfStrings(extension, GJRecord, iGridLine, 0);
//        }
//
//        public List<string> DataFile_updateListOfStrings(string extension, GENERAL_JOURNAL GJRecord, int iGridLine, int iProjLine)
//        {      
//			string EXTENSION_HEADER = ".hdr";
//			string EXTENSION_TRANS_DETAILS = ".dtl";
//			string EXTENSION_PROJECT = ".prj";
//
//
//
//        	
//            List<string> listContents = new List<string>();
//			
//            switch (extension.ToUpper())
//            {
//                case EXTENSION_HEADER:
//                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(GJRecord.source)));
//                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(GJRecord.journalDate)));
//                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(GJRecord.comment)));
//                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(GJRecord.currCode)));
//                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(GJRecord.exchRate)));
//                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(GJRecord.action)));
//                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(GJRecord.recurrName)));
//                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(GJRecord.recurrFrequency)));
//                    break;
//                case EXTENSION_TRANS_DETAILS:
//                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(GJRecord.source)));
//                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(GJRecord.journalDate)));
//                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(GJRecord.GridRows[iGridLine].Account.acctNumber)));
//                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(GJRecord.GridRows[iGridLine].debitAmt)));
//                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(GJRecord.GridRows[iGridLine].creditAmt)));
//                    if (Functions.GoodData (GJRecord.GridRows[iGridLine].Projects))
//                    {
//                        listContents.Add(ConvertFunctions.BoolToString(true));
//                    }
//                    else
//                    {
//                        listContents.Add(ConvertFunctions.BoolToString(false));
//                    }
//                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(GJRecord.GridRows[iGridLine].lineComment)));
//                    break;
//                case EXTENSION_PROJECT:
//                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(GJRecord.source)));
//                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(GJRecord.GridRows[iGridLine].Account.acctNumber)));
//                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(GJRecord.GridRows[iGridLine].Projects[iProjLine].Project.name)));
//                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(GJRecord.GridRows[iGridLine].Projects[iProjLine].Amount)));
//                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(GJRecord.GridRows[iGridLine].Projects[iProjLine].Percent)));
//                    break;
//                default:
//                {
//                    Functions.Verify(false, true, "Valid extension used");
//                    break;
//                }
//            }
//			
//            return listContents;			
//        }
//

    }
        
}
