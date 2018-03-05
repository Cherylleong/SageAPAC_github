// Created in C#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sage50.Types
{
    public class PURCHASE_INVOICE : PURCHASE_COMMON_ORDER_INVOICE
    {
        public PURCHASE_INVOICE()
        { }

        public string quoteOrderTransNumber;
        public List<TRANS_SERIAL> SerialNumberDetails = new List<TRANS_SERIAL>();
        public string earlyPaymentDiscount { get; set; }
        public string earlyPaymentDiscountPercent { get; set; }
        public string directDepositNumber { get; set; }
    }
}
