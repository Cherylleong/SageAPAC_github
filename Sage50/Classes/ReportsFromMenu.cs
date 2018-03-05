/*
 * Created by Ranorex
 * User: wonga01
 * Date: 11/30/2017
 * Time: 16:18
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
using System.Drawing;

namespace Sage50.Classes
{
	/// <summary>
	/// Description of Reports.
	/// </summary>
	public class ReportsFromMenu
	{				
		public ReportsFromMenu()
		{
		}				
		
		public static void Click_Rpt_Financials()
		{
			Simply.repo.Self.Activate();
			Simply.repo.Reports_Item.Click();
			Simply.repo.Rpt_Item_Financials.Click();
		}
		
		public static void Click_Rpt_JournalEntries()
		{
			Simply.repo.Self.Activate();
			Simply.repo.Reports_Item.Click();
			Simply.repo.Rpt_Item_JournalEntries.Click();
		}
		
		public static void Click_Rpt_Receivables()
		{
			Simply.repo.Self.Activate();
			Simply.repo.Reports_Item.Click();
			Simply.repo.Rpt_Item_Receivables.Click();
		}
		
		public static void Click_Rpt_Payables()
		{
			Simply.repo.Self.Activate();
			Simply.repo.Reports_Item.Click();
			Simply.repo.Rpt_Item_Payables.Click();
		}
		
		public static void Click_Rpt_Payroll()
		{
			Simply.repo.Self.Activate();
			Simply.repo.Reports_Item.Click();
			Simply.repo.Rpt_Item_Payroll.Click();
		}
		
		public static void Click_Rpt_InventoryServices()
		{
			Simply.repo.Self.Activate();
			Simply.repo.Reports_Item.Click();
			Simply.repo.Rpt_Item_InventoryServices.Click();
		}
		
		public static void _Rpt_Financials_BalanceSheet_Standard()
		{	
			Click_Rpt_Financials();
			Simply.repo.Rpt_BalanceSheetStd.Click();
			ReportOptionsDialog._OpenReport();			
			while (!ReportDisplay.repo.SelfInfo.Exists())
			{
				Thread.Sleep(500);
			}			
			ReportDisplay._SA_Close();
		}
		
		public static void _Rpt_Financials_AllJournalEntries()
		{
			Click_Rpt_JournalEntries();
			Simply.repo.Rpt_JournalEntries_All.Click();
			ReportOptionsDialog._OpenReport();
			while (!ReportDisplay.repo.SelfInfo.Exists())
			{
				Thread.Sleep(500);
			}
			ReportDisplay._SA_Close();
		}
		
		public static void _Rpt_Receivables_CustomerSalesDetail()
		{
			Click_Rpt_Receivables();
			Simply.repo.Rpt_CustomerSales.Click();
			ReportOptionsDialog.repo.DetailBtn.Click();
			ReportOptionsDialog._OpenReport();
			while (!ReportDisplay.repo.SelfInfo.Exists())
			{
				Thread.Sleep(500);
			}
			ReportDisplay._SA_Close();
		}
		
		public static void _Rpt_Payables_VendorAgedDetail()
		{
			Click_Rpt_Payables();
			Simply.repo.Rpt_VendorAged.Click();
			ReportOptionsDialog.repo.DetailBtn.Click();
			ReportOptionsDialog.repo.VendorAgedDetailBtn.Click();
			ReportOptionsDialog._OpenReport();
			while (!ReportDisplay.repo.SelfInfo.Exists())
			{
				Thread.Sleep(500);
			}
			ReportDisplay._SA_Close();
		}
		
		public static void _Rpt_Payroll_EmployeeDetail()
		{
			Click_Rpt_Payroll();
			Simply.repo.Rpt_Employee.Click();
			ReportOptionsDialog.repo.EmployeeDetailBtn.Click();
			ReportOptionsDialog._OpenReport();
			while (!ReportDisplay.repo.SelfInfo.Exists())
			{
				Thread.Sleep(500);
			}
			ReportDisplay._SA_Close();
		}
		
		public static void _Rpt_InventoryServices_TransactionDetails()
		{
			Click_Rpt_InventoryServices();
			Simply.repo.Rpt_InventoryTransactions.Click();
			ReportOptionsDialog.repo.DetailBtn.Click();
			ReportOptionsDialog._OpenReport();
			while (!ReportDisplay.repo.SelfInfo.Exists())
			{
				Thread.Sleep(500);
			}
			ReportDisplay._SA_Close();
		}
		
	}

	public class ReportOptionsDialog
	{
		public static ReportCentreResFolders.ReportOptionsAppFolder repo = ReportCentreRes.Instance.ReportOptions;
		
		public static void _OpenReport()
		{
			ReportOptionsDialog.repo.OK.Click();	
		}
	}
	
	public class ReportDisplay
	{
		public static ReportCentreResFolders.ReportDisplayAppFolder repo = ReportCentreRes.Instance.ReportDisplay;
		
		public static void _SA_Close()
		{
			ReportDisplay.repo.Self.Activate();
			ReportDisplay.repo.Self.Close();
		}
	}
	
	public class ReportCentre
	{
		public static ReportCentreResFolders.ReportCentreAppFolder repo = ReportCentreRes.Instance.ReportCentre;
		
		public static void _SA_Invoke()
		{
			if (!ReportCentre.repo.SelfInfo.Exists())
			{
				Simply.repo.Self.Activate();
				Simply.repo.Reports_Item.Click();
				Simply.repo.ReportCentre_Item.Click();
			}
		}
		
		public static void _SA_DisplayReport(Reports wReport)
		{
			if (!ReportCentre.repo.SelfInfo.Exists())
			{
				ReportCentre._SA_Invoke();
			}
			
			ReportCentre._SA_SelectReport(wReport);
			ReportCentre.repo.Display.Click();
			while (!ReportDisplay.repo.SelfInfo.Exists())
			{
				Thread.Sleep(1000);
			}
		}
		
		public static void _SA_SelectReport(Reports wReport)
        {
            //example - wReport = ReportCentre._RptType_Receivables._Rpt_CustomerList

            if (!ReportCentre.repo.SelfInfo.Exists())
            {
                ReportCentre._SA_Invoke();
            }

            // select the report type
            wReport.SelectReportType();
    		
            //	Dont need to expand the tree
//            // expand the sub-area if applicable
//            if (wReport.SubArea != null)
//            {
//                ReportCentre.repo.ReportsTree.Expand(wReport.SubArea); // expand only works on a tree in c#
//            }

            // select the report
           //  ReportCentre.repo.ReportsTree.SelectTreeItem(wReport.Path);            
        }
		
		public static void _SA_SelectReportType(string sReportType)
        {
            while (!ReportCentre.repo.ReportType.Visible)
            {
            	Thread.Sleep(500);
            }
            ReportCentre.repo.Self.Activate();
            ReportCentre.repo.ReportType.Click(new Point(25, 10));	// get into the report type control            
            
            int x = 15;
            while (Functions.GetField(ReportCentre.repo.Details.TextValue, "\r", 1) != sReportType)   // "\r" = carriage return
            {            	
                try
                {
                	ReportCentre.repo.ReportType.Click(new Point(25, x));
                }
                catch
                {
                    Functions.Verify(false, true, "Report '" + sReportType + "' is found in the Report Types list");
                    //break;
                }
                x = x + 25;
            }
        }
		
		
		
		// Abstract classes
		public abstract class Reports
        {
            public abstract void SelectReportType();

            public abstract string Path { get; }

            public abstract string SubArea { get; }
        }		
		
		public abstract class _RptType_MyReports : Reports
        {
            public override void SelectReportType()
            {
                ReportCentre._SA_SelectReportType("My Reports");                
            }
            
            public class _Rpt_YearToDate_BalanceSheetYTD : _RptType_MyReports
            {
                /// <summary>
                /// the section hearder in the report list tree
                /// </summary>
                public override string SubArea
                {
                    get 
                    {
                        return "Year to Date";
                    }
                }

                /// <summary>
                /// path to the report, including the sections header if any, in the report list tree
                /// </summary>
                public override string Path
                {
                    get
                    {
                        return "/"+SubArea+"/Balance Sheet YTD";
                    }
                }
            }
            public class _Rpt_YearToDate_ClientAgedSummaryYTD : _RptType_MyReports
            {
                /// <summary>
                /// the section hearder in the report list tree
                /// </summary>
                public override string SubArea
                {
                    get
                    {
                        return "Year to Date";
                    }
                }

                /// <summary>
                /// path to the report, including the sections header if any, in the report list tree
                /// </summary>
                public override string Path
                {
                    get
                    {
                        return "/" + SubArea + "/Client Aged Summary YTD";
                    }
                }
            }
            public class _Rpt_YearToDate_SupplierAgedSummaryYTD : _RptType_MyReports
            {
                /// <summary>
                /// the section hearder in the report list tree
                /// </summary>
                public override string SubArea
                {
                    get
                    {
                        return "Year to Date";
                    }
                }

                /// <summary>
                /// path to the report, including the sections header if any, in the report list tree
                /// </summary>
                public override string Path
                {
                    get
                    {
                        return "/" + SubArea + "/Supplier Aged Summary YTD";
                    }
                }
            }
        }
		
		
	}
}
