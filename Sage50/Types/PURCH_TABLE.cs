// Created in VS
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sage50.Types
{
    public class PURCH_TABLE
    {
        public PURCH_TABLE()
        { }

        public int iItem { get; set; }          // Item Number
        public int iQuantity { get; set; }      // Quantity
        public int iOrder { get; set; }         // Quanitty Ordered
        public int iBackOrder { get; set; }     // Quantity Backordered
        public int iUnit { get; set; }          // Unit
        public int iDescription { get; set; }   // Item Description
        public int iPrice { get; set; }         // Price
        public int iTax { get; set; }               // Tax Code
                                                    // int iTax1Amount	// Tax Amount	for tax 1. not used.
                                                    // int iTax2Amount	// Tax Amount for tax 2. not used and hidden when more than taxes defined
        public int iAmount { get; set; }        // Amount
        public int iAccount { get; set; }       // Account
                                                //int iProjects		// Project Allocation. not used.
        public int iDutyPercent { get; set; }   // Duty charge percentage. (only shows when duty is turned on in settings and vendor ledger)
        public int iDutyAmount { get; set; }	// Duty charge amount.
    }
}
