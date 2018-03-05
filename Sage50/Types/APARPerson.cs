/*
 * Created by Ranorex
 * User: wonda05
 * Date: 8/5/2016
 * Time: 10:43 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace Sage50.Types
{
	/// <summary>
	/// Description of APARPerson.
	/// </summary>
	public class APARPerson : Person
    {
        public APARPerson()
        { }

        public APARPerson(string name, String nameEdit, bool inactive)
        :base(name, nameEdit, inactive)
        {
        }

        public string currency { get; set; }
        public string conductBusinessIn { get; set; }
        public string discountPercent { get; set; }
        public string discountPeriod { get; set; }
        public string termPeriod { get; set; }
        public List<TAX_LEDGER> taxList = new List<TAX_LEDGER>();
        public TAX_CODE taxCode = new TAX_CODE();
        public string currencyAndLocation { get; set; }
        public string branchNumber { get; set; }
        public string institutionNumber { get; set; }
        public string accountNumber { get; set; }
        public string routingNumber { get; set; }
        public string accountType { get; set; }
        public Nullable<bool> eftReferenceCheckbox { get; set; }
        public string eftReference { get; set; }
       	public Nullable<bool> eftTransCodeCheckbox { get; set; }
        public string eftTransCode { get; set; }
        public Nullable<bool> hasSage50CheckBox { get; set; }
        public Nullable<bool> usesMyItemNumCheckBox { get; set; }
        public string balanceOwingInHome { get; set; }	// balance owing in home currency
        public string balanceOwingInForeign { get; set; }	// balance owing in foreign currency
        public string currencyCode { get; set; }
        public Nullable<bool> synchronizeWithOutlook { get; set; }
        public List<IMPORT> imports = new List<IMPORT>();

        public LEDGER_HISTORY recordType = new LEDGER_HISTORY();
    }
}
