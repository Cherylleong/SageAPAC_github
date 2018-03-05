/*
Created in C#
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sage50.Types
{
    public class PURCHASE
    {
        public PURCHASE()
        { }

        public VENDOR Vendor = new VENDOR();
        public ADDRESS Address = new ADDRESS();
        public ADDRESS ShippingAddress = new ADDRESS();
        public List<ROW> GridRows = new List<ROW>();
        public TAX_CODE FreightTaxCode = new TAX_CODE();
        public string action { get; set; } //only for datafile usage, to make backwards compatable        
        public string transNumber { get; set; }
        public string transDate { get; set; }
        public string shipDate { get; set; }
        public string shipToLocation { get; set; }
        public string exchangeRate { get; set; }
        public string termsPercent { get; set; }
        public string termsDays { get; set; }
        public string termsNetDays { get; set; }
        public string freightAmount { get; set; }
        public string freightTax1 { get; set; }
        public string freightTax2 { get; set; }
        public string freightTaxTotal { get; set; }	// when number of taxes > 2
        public string recurringName { get; set; }
        public string recurringFrequency { get; set; }
        public string shippedBy { get; set; }
        public string trackingNumber { get; set; }
    }
}
