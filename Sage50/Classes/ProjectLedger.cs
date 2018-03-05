/*
 * Created by Ranorex
 * User: wonda05
 * Date: 10/18/2016
 * Time: 10:40 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using Sage50.Repository;
using Sage50.Shared;
using Sage50.Types;
using System.IO;

namespace Sage50.Classes
{
	/// <summary>
	/// Description of ProjectLedger.
	/// </summary>
	public static class ProjectLedger
	{
        
        public static ProjectLedgerResFolders.ProjectLedgerAppFolder repo = ProjectLedgerRes.Instance.ProjectLedger;
        
        
        // audit related members
        private const string FUNCTION_ALIAS = "PL";
        private const string EXTENSION_HEADER = ".HDR";
        private const string EXTENSION_BUDGET = ".DT1";
        private const string EXTENSION_BUDGET_TABLE = ".CNT";
        private const string EXTENSION_ADDITIONAL_INFO = ".DT2";

        // need to add repository first
        // public static ProjectLedgerResFolders 

        public static void _SA_Invoke()
        {
            ProjectLedger._SA_Invoke(true);
        }

        public static void _SA_Invoke(Boolean bOpenLedger)
        {
            if (Simply.isEnhancedView())
            {
                Simply.repo.Self.Activate();
                Simply.repo.ProjectsLink.Click();
                Simply.repo.ProjectsIcon.Click();
            }
            else
            {

            }

            if (ProjectIcon.repo.SelfInfo.Exists())
            {
                if (bOpenLedger == true)
                {
                    ProjectIcon.repo.CreateNew.Click();
                    ProjectIcon.repo.Self.Close();
                }
            }            
        }

        public static void _SA_Create(PROJECT ProjectRecord)
        {
            _SA_Create(ProjectRecord, true, false);
        }

        public static void _SA_Create(PROJECT ProjectRecord, bool bSave)
        {
            _SA_Create(ProjectRecord, bSave, false);
        }

        public static void _SA_Create(PROJECT ProjectRecord, bool bSave, bool bEdit)
        {
            if(!Variables.bUseDataFiles && !bEdit)
            {
                ProjectLedger._SA_MatchDefaults(ProjectRecord);
            }

            if (!ProjectLedger.repo.SelfInfo.Exists())
            {
                ProjectLedger._SA_Invoke();
            }

            if (bEdit)
            {
                if (ProjectLedger.repo.SelectRecord.SelectedItemText != ProjectRecord.name)
                {
                    ProjectLedger._SA_Open(ProjectRecord);
                }

                if (Functions.GoodData(ProjectRecord.nameEdit))
                {
                    ProjectLedger.repo.Project.ProjectName.TextValue = ProjectRecord.nameEdit;
                    ProjectRecord.name = ProjectRecord.nameEdit;
                }

                // print to log or results
                Ranorex.Report.Info(String.Format("Modifying project {0}", ProjectRecord.name));
            }
            else
            {
                ProjectLedger.repo.CreateANewToolButton.Click();
                ProjectLedger.repo.Project.ProjectName.TextValue = ProjectRecord.name;
                Ranorex.Report.Info(String.Format("Creating project {0}", ProjectRecord.name));
            }

            // Project tab
            ProjectLedger.repo.Project.Tab.Click();
            if (Functions.GoodData(ProjectRecord.startDate))
            {
                ProjectLedger.repo.Project.StartDate.TextValue = ProjectRecord.startDate;
            }

            if (Functions.GoodData(ProjectRecord.endDate))
            {
                ProjectLedger.repo.Project.EndDate.TextValue = ProjectRecord.endDate;
            }

            if (Functions.GoodData(ProjectRecord.revenue))
            {
                ProjectLedger.repo.Project.Revenue.TextValue = ProjectRecord.revenue;
            }

            if (Functions.GoodData(ProjectRecord.expense))
            {
                ProjectLedger.repo.Project.Expense.TextValue = ProjectRecord.expense;
            }

            if (ProjectRecord.status != 0)
            {
            	ProjectLedger.repo.Project.Status.Items[((int)ProjectRecord.status)].Select();
            }

            if (Functions.GoodData(ProjectRecord.inactiveCheckBox))
            {
                ProjectLedger.repo.InactiveProject.SetState(ProjectRecord.inactiveCheckBox);
            }

            // check if budget tab exists, will take a long time since object times are acccumalitive
            if(ProjectLedger.repo.Budget.TabInfo.Exists())
            {
                ProjectLedger.repo.Budget.Tab.Click();

                if (Functions.GoodData(ProjectRecord.budgetCheckBox))
                {
                    ProjectLedger.repo.Budget.BudgetThisProject.SetState(ProjectRecord.budgetCheckBox);

                    // Budget details
                    if (ProjectLedger.repo.Budget.BudgetThisProject.Enabled)
                    {
                        int BUDGET_REVENUE_COLUMN = 2;
                        int BUDGET_EXPENSES_COLUMN = 3;

                        ProjectLedger.repo.Budget.BudgetContainer.ClickFirstCell();
                       
                        for (int x = 0; x < ProjectRecord.Budgets.Count; x++)
                        {
                            if (Functions.GoodData(ProjectRecord.Budgets[x].revenue))
                            {
                                ProjectLedger.repo.Budget.BudgetContainer.MoveToField(x, BUDGET_REVENUE_COLUMN);
                                ProjectLedger.repo.Budget.BudgetContainer.SetText(ProjectRecord.Budgets[x].revenue);
                            }

                            if (Functions.GoodData(ProjectRecord.Budgets[x].expense))
                            {
                                ProjectLedger.repo.Budget.BudgetContainer.MoveToField(x, BUDGET_EXPENSES_COLUMN);
                                ProjectLedger.repo.Budget.BudgetContainer.SetText(ProjectRecord.Budgets[x].expense);
                            }
                        }
                    }               
                }
            }


            // Additional info
            ProjectLedger.repo.AdditionalInfo.Tab.Click();

            if (ProjectLedger.repo.AdditionalInfo.Additional1Info.Exists())
            {
                if (Functions.GoodData(ProjectRecord.additional1))
                {
                    ProjectLedger.repo.AdditionalInfo.Additional1.TextValue = ProjectRecord.additional1;
                }

                if (Functions.GoodData(ProjectRecord.addCheckBox1))
                {
                    ProjectLedger.repo.AdditionalInfo.AddCheckBox1.SetState(ProjectRecord.addCheckBox1);
                }
            }

            if (ProjectLedger.repo.AdditionalInfo.Additional2Info.Exists())
            {
                if (Functions.GoodData(ProjectRecord.additional2))
                {
                    ProjectLedger.repo.AdditionalInfo.Additional2.TextValue = ProjectRecord.additional2;
                }

                if (Functions.GoodData(ProjectRecord.addCheckBox2))
                {
                    ProjectLedger.repo.AdditionalInfo.AddCheckBox2.SetState(ProjectRecord.addCheckBox2);
                }
            }

            if (ProjectLedger.repo.AdditionalInfo.Additional3Info.Exists())
            {
                if (Functions.GoodData(ProjectRecord.additional3))
                {
                    ProjectLedger.repo.AdditionalInfo.Additional3.TextValue = ProjectRecord.additional3;
                }

                if (Functions.GoodData(ProjectRecord.addCheckBox3))
                {
                    ProjectLedger.repo.AdditionalInfo.AddCheckBox3.SetState(ProjectRecord.addCheckBox3);
                }
            }

            if (ProjectLedger.repo.AdditionalInfo.Additional4Info.Exists())
            {
                if (Functions.GoodData(ProjectRecord.additional4))
                {
                    ProjectLedger.repo.AdditionalInfo.Additional4.TextValue = ProjectRecord.additional4;
                }

                if (Functions.GoodData(ProjectRecord.addCheckBox4))
                {
                    ProjectLedger.repo.AdditionalInfo.AddCheckBox2.SetState(ProjectRecord.addCheckBox4);
                }
            }

            if (ProjectLedger.repo.AdditionalInfo.Additional5Info.Exists())
            {
                if (Functions.GoodData(ProjectRecord.additional5))
                {
                    ProjectLedger.repo.AdditionalInfo.Additional5.TextValue = ProjectRecord.additional5;
                }

                if (Functions.GoodData(ProjectRecord.addCheckBox5))
                {
                    ProjectLedger.repo.AdditionalInfo.AddCheckBox2.SetState(ProjectRecord.addCheckBox5);
                }
            }

            if (bSave)
            {
                ProjectLedger.repo.Save.Click();
            }
        }

        public static void _SA_MatchDefaults(PROJECT ProjectRecord)
        {
            if (Functions.GoodData(ProjectRecord.inactiveCheckBox))
            {
                ProjectRecord.inactiveCheckBox = false;
            }

            if (ProjectRecord.status != 0)
            {
                ProjectRecord.status = PROJECT_STATUS.PROJECT_PENDING;
            }

            if (!Functions.GoodData(ProjectRecord.budgetCheckBox))
            {
                ProjectRecord.budgetCheckBox = false;
            }

            if (!Functions.GoodData(ProjectRecord.addCheckBox1))
            {
                ProjectRecord.addCheckBox1 = false;
            }

            if (!Functions.GoodData(ProjectRecord.addCheckBox2))
            {
                ProjectRecord.addCheckBox2 = false;
            }

            if (!Functions.GoodData(ProjectRecord.addCheckBox3))
            {
                ProjectRecord.addCheckBox3 = false;
            }

            if (!Functions.GoodData(ProjectRecord.addCheckBox4))
            {
                ProjectRecord.addCheckBox4 = false;
            }

            if (!Functions.GoodData(ProjectRecord.addCheckBox5))
            {
                ProjectRecord.addCheckBox5 = false;
            }
        }

        public static void _SA_Close()
        {
            repo.Self.Close();
        }

        public static void _SA_Delete(PROJECT ProjectRecord)
        {
            if (!ProjectLedger.repo.SelfInfo.Exists())
            {
                ProjectLedger._SA_Invoke();
            }

            if (ProjectLedger.repo.SelectRecord.SelectedItemText != ProjectRecord.name)
            {
                ProjectLedger._SA_Open(ProjectRecord);
            }

            //ProjectLedger.repo.ClickRemove(); // remove method to be updated

            //SimplyMessage.Instance._SA_HandleMessage(SimplyMessage.YES_LOC, SimplyMessage._MSG_AREYOUSUREYOUWANTTOREMOVE_LOC, true);
        }

        public static void _SA_Open(PROJECT ProjectRecord)
        {
            if (!ProjectLedger.repo.SelfInfo.Exists())
            {
                ProjectLedger._SA_Invoke();
            }
            ProjectLedger.repo.SelectRecord.Select(ProjectRecord.name);
        }

        public static PROJECT _SA_Read()
        {
            return _SA_Read(null);
        }

        public static PROJECT _SA_Read(string sIDToRead)
        {
            PROJECT ProjectRecord = new PROJECT();
            if (Functions.GoodData(sIDToRead))
            {
                ProjectRecord.name = sIDToRead;
                if (ProjectLedger.repo.SelectRecord.SelectedItemText != ProjectRecord.name)
                {
                    ProjectLedger._SA_Open(ProjectRecord);
                }
            }

            ProjectLedger.repo.Project.Tab.Click();
            ProjectRecord.startDate = ProjectLedger.repo.Project.StartDate.TextValue;
            ProjectRecord.endDate = ProjectLedger.repo.Project.EndDate.TextValue;
            ProjectRecord.revenue = ProjectLedger.repo.Project.Revenue.TextValue;
            ProjectRecord.expense = ProjectLedger.repo.Project.Expense.TextValue;
            ProjectRecord.status = (PROJECT_STATUS)ProjectLedger.repo.Project.Status.SelectedItemIndex;
            ProjectRecord.inactiveCheckBox = ProjectLedger.repo.InactiveProject.Checked;

            if (ProjectLedger.repo.Budget.TabInfo.Exists())
            {
                ProjectLedger.repo.Budget.Tab.Click();
                ProjectRecord.budgetCheckBox = ProjectLedger.repo.Budget.BudgetThisProject.Checked;
                // Read grid
                if (ProjectRecord.budgetCheckBox == true)
                {
                    //ProjectLedger.repo.Budget.BudgetContainer.InitializeTable();
                    
                    List<List<string>> lsContents = ProjectLedger.repo.Budget.BudgetContainer.GetContents();
                    for (int i = 0; i < lsContents.Count; i++)
                    {
                        PROJECT_BUDGET PB = new PROJECT_BUDGET();
                        PB.revenue = lsContents[i][i];
                        PB.expense = lsContents[1][2];

                        ProjectRecord.Budgets.Add(PB);
                    }
                }
            }

            ProjectLedger.repo.AdditionalInfo.Tab.Click();
            if (ProjectLedger.repo.AdditionalInfo.Additional1Info.Exists())
            {
                ProjectRecord.additional1 = ProjectLedger.repo.AdditionalInfo.Additional1.TextValue;
            }           

            if (ProjectLedger.repo.AdditionalInfo.AddCheckBox1Info.Exists())
            {
                ProjectRecord.addCheckBox1 = ProjectLedger.repo.AdditionalInfo.AddCheckBox1.Checked;
            }

            if (ProjectLedger.repo.AdditionalInfo.Additional2Info.Exists())
            {
                ProjectRecord.additional2 = ProjectLedger.repo.AdditionalInfo.Additional2.TextValue;
            }

            if (ProjectLedger.repo.AdditionalInfo.AddCheckBox2Info.Exists())
            {
                ProjectRecord.addCheckBox2 = ProjectLedger.repo.AdditionalInfo.AddCheckBox2.Checked;
            }

            if (ProjectLedger.repo.AdditionalInfo.Additional3Info.Exists())
            {
                ProjectRecord.additional3 = ProjectLedger.repo.AdditionalInfo.Additional3.TextValue;
            }

            if (ProjectLedger.repo.AdditionalInfo.AddCheckBox3Info.Exists())
            {
                ProjectRecord.addCheckBox3 = ProjectLedger.repo.AdditionalInfo.AddCheckBox3.Checked;
            }

            if (ProjectLedger.repo.AdditionalInfo.Additional4Info.Exists())
            {
                ProjectRecord.additional4 = ProjectLedger.repo.AdditionalInfo.Additional4.TextValue;
            }

            if (ProjectLedger.repo.AdditionalInfo.AddCheckBox4Info.Exists())
            {
                ProjectRecord.addCheckBox4 = ProjectLedger.repo.AdditionalInfo.AddCheckBox4.Checked;
            }

            if (ProjectLedger.repo.AdditionalInfo.Additional5Info.Exists())
            {
                ProjectRecord.additional5 = ProjectLedger.repo.AdditionalInfo.Additional5.TextValue;
            }

            if (ProjectLedger.repo.AdditionalInfo.AddCheckBox5Info.Exists())
            {
                ProjectRecord.addCheckBox5 = ProjectLedger.repo.AdditionalInfo.AddCheckBox5.Checked;
            }

            return ProjectRecord;
        }

        // DataFile
        public static void DataFile_ReadFile(string sDataLocation, string fileCounter)
        {
            StreamReader FileHDR;	// File handle for Project tab info
            StreamReader FileDT1;	// File handle for the Additional Info tab info
            StreamReader FileCNT;	// File handle for the Additional Info tab info
            StreamReader FileDT2;	// File handle for the Additional Info tab info

            string dataLine;	// Stores the current field data from file
            string dataPath;	// The name and path to the data files location

            dataPath = sDataLocation + "PL" + fileCounter;

            List<PROJECT> lProjects = new List<PROJECT>();

            FileHDR = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_HEADER, "FM_READ"));
            while ((dataLine = FileHDR.ReadLine()) != null)
            {
                PROJECT Proj = new PROJECT();

                // Set header info
                Proj = DataFile_setDataStructure(EXTENSION_HEADER, dataLine, Proj);
                
                // Only open budget details file exists
                if (File.Exists(dataPath + EXTENSION_BUDGET))
                {
                    using (FileDT1 = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_BUDGET, "FM_READ")))
                    {
                        while ((dataLine = FileDT1.ReadLine()) != null)
                        {
                            Proj = DataFile_setDataStructure(EXTENSION_BUDGET, dataLine, Proj);
                        }
                    }
                }

                // Only open budget container file exists
                if (File.Exists(dataPath + EXTENSION_BUDGET_TABLE))
                {
                    using (FileCNT = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_BUDGET_TABLE, "FM_READ")))
                    {
                        while ((dataLine = FileCNT.ReadLine()) != null)
                        {
                            Proj = DataFile_setDataStructure(EXTENSION_BUDGET_TABLE, dataLine, Proj);
                        }
                    }
                }

                // Open Additional file and set structure
                if (File.Exists(dataPath + EXTENSION_ADDITIONAL_INFO))
                {
                    using (FileDT2 = new StreamReader(Functions.FileOpen(dataPath + EXTENSION_ADDITIONAL_INFO, "FM_READ")))
                    {
                        while ((dataLine = FileDT2.ReadLine()) != null)
                        {
                            Proj = DataFile_setDataStructure(EXTENSION_ADDITIONAL_INFO, dataLine, Proj);
                        }
                    }
                }

                lProjects.Add(Proj);
            }
            FileHDR.Close();
            
            foreach (PROJECT pj in lProjects)
            {
                switch(pj.action)
                {
                    case "A":
                        ProjectLedger._SA_Create(pj);
                        break;
                    case "B":
                        ProjectLedger._SA_Create(pj, true, true);
                        break;
                    case "D":
                        ProjectLedger._SA_Delete(pj);
                        break;
                    default:
                        {
                            Functions.Verify(false, true, "Valid action sent for record");
                            break;
                        }
                }
            }
            ProjectLedger._SA_Close();            
        }

        public static PROJECT DataFile_setDataStructure(string extension, string dataLine, PROJECT Proj)
        {
            PROJECT ProjectRecord = Proj;

            switch (extension.ToUpper())
            {
                case EXTENSION_HEADER:
                    ProjectRecord.action = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 1));
                    ProjectRecord.name = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 2));
                    ProjectRecord.startDate = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 3));
                    ProjectRecord.revenue = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 4));
                    ProjectRecord.expense = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 5));
                    ProjectRecord.inactiveCheckBox = ConvertFunctions.StringToBool(Functions.GetField(dataLine, ",", 6));
                    ProjectRecord.endDate = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 7));

                    switch (Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 8)))
                    {
                        case "Pending":
                            ProjectRecord.status = PROJECT_STATUS.PROJECT_PENDING;
                            break;
                        case "In Progress":
                            ProjectRecord.status = PROJECT_STATUS.PROJECT_IN_PROGRESS;
                            break;
                        case "Cancelled":
                            ProjectRecord.status = PROJECT_STATUS.PROJECT_CANCELLED;
                            break;
                        case "Completed":
                            ProjectRecord.status = PROJECT_STATUS.PROJECT_COMPLETED;
                            break;
                        default:
                            {
                                Functions.Verify(false, true, "Valid value for project status");
                                break;
                            }
                    }
                    ProjectRecord.nameEdit = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 9));
                    break;
                case EXTENSION_BUDGET:
                    if (ProjectRecord.name == Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 1)))
                    {
                        ProjectRecord.budgetCheckBox = ConvertFunctions.StringToBool(Functions.GetField(dataLine, ",", 2));
                    }
                    break;
                case EXTENSION_BUDGET_TABLE:
                    if (ProjectRecord.name == Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 1)))
                    {
                        PROJECT_BUDGET PB = new PROJECT_BUDGET();
                        PB.revenue = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 3));
                        PB.expense = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 4));
                        if (Functions.GoodData(ProjectRecord.Budgets))
                        {
                            ProjectRecord.Budgets.Add(PB);
                        }
                        else
                        {
                            ProjectRecord.Budgets = new List<PROJECT_BUDGET> { PB };
                        }
                    }
                    break;
                case EXTENSION_ADDITIONAL_INFO:
                    if (ProjectRecord.name == Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 1)))
                    {
                        ProjectRecord.additional1 = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 2));
                        ProjectRecord.additional2 = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 3));
                        ProjectRecord.additional3 = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 4));
                        ProjectRecord.additional4 = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 5));
                        ProjectRecord.additional5 = Functions.PrepStringsFromDataFiles(Functions.GetField(dataLine, ",", 6));
                        ProjectRecord.addCheckBox1 = ConvertFunctions.StringToBool(Functions.GetField(dataLine, ",", 7));
                        ProjectRecord.addCheckBox2 = ConvertFunctions.StringToBool(Functions.GetField(dataLine, ",", 8));
                        ProjectRecord.addCheckBox3 = ConvertFunctions.StringToBool(Functions.GetField(dataLine, ",", 9));
                        ProjectRecord.addCheckBox4 = ConvertFunctions.StringToBool(Functions.GetField(dataLine, ",", 10));
                        ProjectRecord.addCheckBox5 = ConvertFunctions.StringToBool(Functions.GetField(dataLine, ",", 11));
                    }
                    break;
                default:
                    {
                        Functions.Verify(false, true, "Valid value of extension sent to function");
                        break;
                    }
            }
            return ProjectRecord;
        }

        public static List<string> DataFile_updateListOfStrings(string extension, PROJECT ProjectRecord)
        {
            return DataFile_updateListOfStrings(extension, ProjectRecord, 0);
        }

        public static List<string> DataFile_updateListOfStrings(string extension, PROJECT ProjectRecord, int iBudgetLine)
        {

            List<string> listContents = new List<string>();

            switch (extension.ToUpper())
            {
                case EXTENSION_HEADER:
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(ProjectRecord.action)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(ProjectRecord.name)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(ProjectRecord.startDate)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(ProjectRecord.revenue)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(ProjectRecord.expense)));
                    listContents.Add(ConvertFunctions.BoolToString(ProjectRecord.inactiveCheckBox));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(ProjectRecord.endDate)));
                    switch (ProjectRecord.status)
                    {
                        case PROJECT_STATUS.PROJECT_PENDING:
                            listContents.Add("Pending");
                            break;
                        case PROJECT_STATUS.PROJECT_IN_PROGRESS:
                            listContents.Add("In Progress");
                            break;
                        case PROJECT_STATUS.PROJECT_CANCELLED:
                            listContents.Add("Cancelled");
                            break;
                        case PROJECT_STATUS.PROJECT_COMPLETED:
                            listContents.Add("Completed");
                            break;
                        default:
                            Functions.Verify(false, true, "Valid value for project status");
                            break;
                    }
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(ProjectRecord.nameEdit)));
                    break;
                case EXTENSION_BUDGET:
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(ProjectRecord.name)));
                    listContents.Add(ConvertFunctions.BoolToString(ProjectRecord.budgetCheckBox));
                    break;
                case EXTENSION_BUDGET_TABLE:
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(ProjectRecord.name)));
                    listContents.Add(Convert.ToString(iBudgetLine));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(ProjectRecord.Budgets[iBudgetLine].revenue)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(ProjectRecord.Budgets[iBudgetLine].expense)));
                    break;
                case EXTENSION_ADDITIONAL_INFO:
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(ProjectRecord.name)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(ProjectRecord.additional1)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(ProjectRecord.additional2)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(ProjectRecord.additional3)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(ProjectRecord.additional4)));
                    listContents.Add(ConvertFunctions.CommaToText(ConvertFunctions.NullToBlankString(ProjectRecord.additional5)));
                    listContents.Add(ConvertFunctions.BoolToString(ProjectRecord.addCheckBox1));
                    listContents.Add(ConvertFunctions.BoolToString(ProjectRecord.addCheckBox2));
                    listContents.Add(ConvertFunctions.BoolToString(ProjectRecord.addCheckBox3));
                    listContents.Add(ConvertFunctions.BoolToString(ProjectRecord.addCheckBox4));
                    listContents.Add(ConvertFunctions.BoolToString(ProjectRecord.addCheckBox5));
                    break;
                default:
                    {
                        break;
                    }                    
            }
            return listContents;
        }
    } 
	
	
	public static class ProjectIcon
	{
		public static ProjectLedgerResFolders.ProjectIconAppFolder repo = ProjectLedgerRes.Instance.ProjectIcon;
		
	}
}
