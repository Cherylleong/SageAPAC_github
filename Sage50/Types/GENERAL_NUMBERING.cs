/*
 * Created by Ranorex
 * User: wonga01
 * Date: 9/2/2016
 * Time: 16:24
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Sage50.Types
{
	/// <summary>
	/// Description of GENERAL_NUMBERING.
	/// </summary>
	public class GENERAL_NUMBERING
	{
		public GENERAL_NUMBERING()
		{
		}
		
		 public GENERAL_NUMBERING(Nullable<bool> showAcctNumInTransactions,Nullable<bool> showAcctNumInReports,string numOfDigitsInAcctNum, string assetStartNum, string assetEndNum, string liabilityStartNum, string liabilityEndNum, string equityStartNum, string equityEndNum, string revStartNum, string revEndNum, string expStartNum, string expEndNum) 
        {
            this.showAcctNumInTransactions = showAcctNumInTransactions;
		    this.showAcctNumInReports = showAcctNumInReports;
		    this.numOfDigitsInAcctNum = numOfDigitsInAcctNum;
		    this.assetStartNum = assetStartNum;
		    this.assetEndNum = assetEndNum;
		    this.liabilityStartNum = liabilityStartNum;
		    this.liabilityEndNum = liabilityEndNum;
		    this.equityStartNum = equityStartNum;
		    this.equityEndNum = equityEndNum;
		    this.revStartNum = revStartNum;
		    this.revEndNum = revEndNum;
		    this.expStartNum = expStartNum;
		    this.expEndNum = expEndNum;
        }

		public Nullable<bool> showAcctNumInTransactions { get; set; }
		public Nullable<bool> showAcctNumInReports { get; set; }
		public string numOfDigitsInAcctNum { get; set; }
		public string assetStartNum { get; set; }
		public string assetEndNum { get; set; }
		public string liabilityStartNum { get; set; }
		public string liabilityEndNum { get; set; }
		public string equityStartNum { get; set; }
		public string equityEndNum { get; set; }
		public string revStartNum { get; set; }
		public string revEndNum { get; set; }
		public string expStartNum { get; set; }
		public string expEndNum { get; set; }
	}
}
