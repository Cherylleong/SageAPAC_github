/*
 * Created by Ranorex
 * User: wonda05
 * Date: 10/7/2016
 * Time: 11:25 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Sage50.Types
{
	/// <summary>
	/// Description of Class1.
	/// </summary>
	public class VENDOR : APARPerson
	{
		public VENDOR()
		{
		}
		
		public GL_ACCOUNT expenseAccount = new GL_ACCOUNT();        
        public string taxID { get; set; }        
        public string vendorSince { get; set; }
        public Nullable<bool> calDisBeforeTaxCheckBox { get; set; }
        public Nullable<bool> printContactCheckBox { get; set; }
        public Nullable<bool> emailConfirmCheckBox { get; set; }
        public Nullable<bool> includeFilingT5018CheckBox { get; set; }
        public Nullable<bool> chargeDutyCheckBox { get; set; }
        public string ordersForThisVendor { get; set; }        
        public string usuallyStoreItemIn { get; set; }
        public string ytdPurchases { get; set; }
        public string lyPurchases { get; set; }
        public string ytdPayments { get; set; }
        public string previousYtdPayments { get; set; }
        public string foreignYtdPurchases { get; set; }
        public string foreignLyPurchases { get; set; }
        public string foreignYtdPayments { get; set; }
        public string foreignPreviousYtdPayments { get; set; }
        public string invoiceNumber { get; set; }
        public string invoiceDate { get; set; }
        public string percentOrTaken { get; set; }
        public string daysOrAmountPaid { get; set; }
        public string NetDaysOrExchangeRate { get; set; }
        public string amount { get; set; }
        public string tax { get; set; }
        public string exchangeRate { get; set; }
        public string homeAmount { get; set; }
        public Nullable<bool> allowDirectDepCheckBox { get; set; }
		
	}
}
