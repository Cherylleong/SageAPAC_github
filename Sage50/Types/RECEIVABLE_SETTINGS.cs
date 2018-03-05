/*
 * Created by Ranorex
 * User: wonga01
 * Date: 9/6/2016
 * Time: 10:33
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace Sage50.Types
{
	/// <summary>
	/// Description of RECEIVABLE_SETTINGS.
	/// </summary>
	public class RECEIVABLE_SETTINGS
	{
		public RECEIVABLE_SETTINGS()
		{
		}
		
		public RECEIVABLE_SETTINGS(string City, string Province, string Country, string agingPeriod1, string agingPeriod2, string agingPeriod3, Nullable<bool> interestCharges, string interestPercent, string interestDays, TAX_CODE taxCodeForNewCustomers, Nullable<bool> printSalesperson, string statementDays, string discountPercent, string discountDays, string netDays, Nullable<bool> calculateEarlyPaymentDiscountsB4Tax, Nullable<bool> calculateLineItemDiscounts, string salesInvoiceComment, string salesOrderComment, string salesQuoteComment, ADDITIONAL_FIELD_NAMES AdditionalFields, GL_ACCOUNT principalBankAcct, GL_ACCOUNT arAcct, GL_ACCOUNT freightAcct, GL_ACCOUNT earlyPayDiscountAcct, GL_ACCOUNT depositsAcct, List<CURRENCY_ACCOUNT> CurrencyAccounts)
        {
            this.City = City;
            this.Province = Province;
            this.Country = Country;
            this.agingPeriod1 = agingPeriod1;
            this.agingPeriod2 = agingPeriod2;
            this.agingPeriod3 = agingPeriod3;
            this.interestCharges = interestCharges;
            this.interestPercent = interestPercent;
            this.interestDays = interestDays;
		    this.taxCodeForNewCustomers = taxCodeForNewCustomers;
            this.printSalesperson = printSalesperson;
            this.statementDays = statementDays;
            this.discountPercent = discountPercent;
            this.discountDays= discountDays;
            this.netDays = netDays;
            this.calculateEarlyPaymentDiscountsB4Tax  = calculateEarlyPaymentDiscountsB4Tax;
            this.calculateLineItemDiscounts = calculateLineItemDiscounts;
            this.salesInvoiceComment = salesInvoiceComment;
            this.salesOrderComment = salesOrderComment;
            this.salesQuoteComment = salesQuoteComment;
            this.AdditionalFields = AdditionalFields;
		    this.principalBankAcct = principalBankAcct;
		    this.arAcct = arAcct;
		    this.freightAcct = freightAcct;
		    this.earlyPayDiscountAcct = earlyPayDiscountAcct;
		    this.depositsAcct  = depositsAcct;
            this.CurrencyAccounts = CurrencyAccounts;
        }

        public string City { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }
        public string agingPeriod1 { get; set; }
        public string agingPeriod2 { get; set; }
        public string agingPeriod3 { get; set; }
        public Nullable<bool> interestCharges { get; set; }
        public string interestPercent { get; set; }
        public string interestDays { get; set; }
		public TAX_CODE taxCodeForNewCustomers = new TAX_CODE();
        public Nullable<bool> printSalesperson { get; set; }
        public string statementDays { get; set; }
        public string discountPercent { get; set; }								// Discount page
        public string discountDays { get; set; }								// Discount page
        public string netDays { get; set; }										// Discount page
        public Nullable<bool> calculateEarlyPaymentDiscountsB4Tax { get; set; }	// Discount page
        public Nullable<bool> calculateLineItemDiscounts { get; set; }				// Discount page
        public string salesInvoiceComment { get; set; }
        public string salesOrderComment { get; set; }
        public string salesQuoteComment { get; set; }
        public ADDITIONAL_FIELD_NAMES AdditionalFields = new ADDITIONAL_FIELD_NAMES();
		public GL_ACCOUNT principalBankAcct = new GL_ACCOUNT();
		public GL_ACCOUNT arAcct = new GL_ACCOUNT();
		public GL_ACCOUNT freightAcct = new GL_ACCOUNT();
		public GL_ACCOUNT earlyPayDiscountAcct = new GL_ACCOUNT();
		public GL_ACCOUNT depositsAcct = new GL_ACCOUNT();
		public List<CURRENCY_ACCOUNT> CurrencyAccounts = new List<CURRENCY_ACCOUNT>();
	}
}
