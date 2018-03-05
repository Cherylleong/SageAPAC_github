/*
 * Created by Ranorex
 * User: wonda05
 * Date: 8/5/2016
 * Time: 10:54 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Sage50.Types
{
	/// <summary>
	/// Description of GL_account.
	/// </summary>
	public class GL_ACCOUNT : LedgerData
	{
		public GL_ACCOUNT ()
		{}

		public GL_ACCT_TYPE acctType = new GL_ACCT_TYPE();
		public GL_ACCT_CLASS acctClass = new GL_ACCT_CLASS();

		public string acctNumber { get; set; }	// contains both account number and account name
		public string editedAcctNumber { get; set; }
		public string editedAcctName { get; set; }  // for data files
		public string acctNameFre { get; set; } // account name in French
		public string openBalance { get; set; }
		public string frgnOpenBalance { get; set; }
		public string gifi { get; set; }
		public Nullable<bool> omitFromChkBox { get; set; }
		public Nullable<bool> allowPrjAllocChkBox { get; set; }
		public string currencyCode { get; set; }
		public string institution { get; set; } //only for Bank classed accounts
		public string branchName { get; set; } //only for Bank classed accounts
		public string transNumber { get; set; } //only for Bank classed accounts
		public string bankAcctNum { get; set; } //only for Bank classed accounts
		public string bankAcctType { get; set; } //only for Bank classed accounts
		public string nextDepositNo { get; set; } //only for Bank or Cash classed accounts
		public string homePage { get; set; } //only for online banking
		public string login { get; set; } //only for online banking
		public Nullable<bool> useForOnlineChkBox { get; set; } //only for Bank classed accounts
		public Nullable<bool> budgetChkBox { get; set; }
	}
}
