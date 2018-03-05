// Created in C#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sage50.Types
{
    public class PURCHASE_ORDER : PURCHASE_COMMON_ORDER_INVOICE
    {
        public PURCHASE_ORDER()
        { }

        public string prepayRefNumber { get; set; }
        public string prepaymentAmount { get; set; }
    }
}
