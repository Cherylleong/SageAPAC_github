/*
 * Created by Ranorex
 * User: wonga01
 * Date: 9/6/2016
 * Time: 10:26
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace Sage50.Types
{
	/// <summary>
	/// Description of PAYABLE_SETTINGS.
	/// </summary>
	public class PAYABLE_SETTINGS
	{
		public PAYABLE_SETTINGS()
		{
		}
		
		public PAYABLE_SETTINGS(string City,string Province, string Country, string agingPeriod1, string agingPeriod2, string agingPeriod3, Nullable<bool> calculateDiscountsBeforeTax, Nullable<bool> trackDutyOnImportedItems, GL_ACCOUNT importDutyAcct, ADDITIONAL_FIELD_NAMES AdditionalFields, GL_ACCOUNT principalBankAcct, GL_ACCOUNT apAcct, GL_ACCOUNT freightAcct, GL_ACCOUNT earlyPayDiscountAcct, GL_ACCOUNT prepaymentAcct, List<CURRENCY_ACCOUNT> CurrencyAccounts)
        { 
            this.City = City;
            this.Province = Province;
            this.Country = Country;
            this.agingPeriod1 = agingPeriod1;
            this.agingPeriod2 = agingPeriod2;
            this.agingPeriod3 = agingPeriod3;
            this.calculateDiscountsBeforeTax = calculateDiscountsBeforeTax;
            this.trackDutyOnImportedItems = trackDutyOnImportedItems;
		    this.importDutyAcct = importDutyAcct;
		    this.AdditionalFields = AdditionalFields;
		    this.principalBankAcct = principalBankAcct;
		    this.apAcct = apAcct;
		    this.freightAcct = freightAcct;
		    this.earlyPayDiscountAcct = earlyPayDiscountAcct;
		    this.prepaymentAcct = prepaymentAcct;
		    this.CurrencyAccounts = CurrencyAccounts;
        }

        public string City { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }
        public string agingPeriod1 { get; set; }
        public string agingPeriod2 { get; set; }
        public string agingPeriod3 { get; set; }
        public Nullable<bool> calculateDiscountsBeforeTax { get; set; }
        public Nullable<bool> trackDutyOnImportedItems { get; set; }
		public GL_ACCOUNT importDutyAcct = new GL_ACCOUNT();
		public ADDITIONAL_FIELD_NAMES AdditionalFields = new ADDITIONAL_FIELD_NAMES();
		public GL_ACCOUNT principalBankAcct = new GL_ACCOUNT();
		public GL_ACCOUNT apAcct = new GL_ACCOUNT();
		public GL_ACCOUNT freightAcct = new GL_ACCOUNT();
		public GL_ACCOUNT earlyPayDiscountAcct = new GL_ACCOUNT();
		public GL_ACCOUNT prepaymentAcct = new GL_ACCOUNT();
		public List<CURRENCY_ACCOUNT> CurrencyAccounts = new List<CURRENCY_ACCOUNT>();
	}
}
