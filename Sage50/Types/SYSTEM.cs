/*
 * Created by Ranorex
 * User: wonga01
 * Date: 9/6/2016
 * Time: 10:41
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Sage50.Types
{
	/// <summary>
	/// Description of SYSTEM.
	/// </summary>
	public class SYSTEM
	{
		public SYSTEM()
		{
		}
		
		public SYSTEM(Nullable<bool> useCashBasisAccounting, string cashBasisDate, Nullable<bool> storeInvoiceLookupDetails, Nullable<bool> useChequeNo, Nullable<bool> doNotAllowTransactionsDatedBefore, string lockingDate, Nullable<bool> allowTransactionsInTheFuture, Nullable<bool> warnIfTransactionAre, string daysInTheFuture, Nullable<bool> warnIfAccountsAreNotBalanced)
        {
            this.useCashBasisAccounting = useCashBasisAccounting;
            this.cashBasisDate = cashBasisDate;
            this.storeInvoiceLookupDetails = storeInvoiceLookupDetails;
            this.useChequeNo = useChequeNo;
            this.doNotAllowTransactionsDatedBefore = doNotAllowTransactionsDatedBefore;
            this.lockingDate = lockingDate;
            this.allowTransactionsInTheFuture = allowTransactionsInTheFuture;
            this.warnIfTransactionsAre = warnIfTransactionAre;
            this.daysInTheFuture = daysInTheFuture;
            this.warnIfAccountsAreNotBalanced = warnIfTransactionsAre;
        }


		public Nullable<bool> useCashBasisAccounting { get; set; }
		public string cashBasisDate { get; set; }
		public Nullable<bool> storeInvoiceLookupDetails { get; set; }
		public Nullable<bool> useChequeNo { get; set; }
		public Nullable<bool> doNotAllowTransactionsDatedBefore { get; set; }
		public string lockingDate { get; set; }
		public Nullable<bool> allowTransactionsInTheFuture { get; set; }
		public Nullable<bool> warnIfTransactionsAre { get; set; }
		public string daysInTheFuture { get; set; }
		public Nullable<bool> warnIfAccountsAreNotBalanced { get; set; }
	}
}
