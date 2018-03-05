/*
 * Created by Ranorex
 * User: wonga01
 * Date: 8/29/2016
 * Time: 17:50
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace Sage50.Types
{
	 public class SALES
    {
        public SALES()
        { }

        public string action { get; set; } //only for datafile usage, to make backwards compatable
        public CUSTOMER Customer = new CUSTOMER();
        public string transNumber { get; set; }
        public string transDate { get; set; }
        public string shipDate { get; set; }
        public EMPLOYEE SalesPerson = new EMPLOYEE();
        public ADDRESS Address = new ADDRESS();
        public ADDRESS ShipToAddress = new ADDRESS();
        public string shipFrom { get; set; }
        public string exchangeRate { get; set; }
        public List<ROW> GridRows = new List<ROW>();
        public string termsPercent { get; set; }
        public string termsDays { get; set; }
        public string termsNetDays { get; set; }
        public string freightAmount { get; set; }
        public TAX_CODE FreightTaxCode = new TAX_CODE();
        public string freightTax1 { get; set; }
        public string freightTax2 { get; set; }
        public string freightTaxTotal { get; set; }	// when number of taxes > 2
        public string message { get; set; }
        public string recurringName { get; set; }
        public string recurringFrequency { get; set; }
        public string calcTotal { get; set; }
    }	
}