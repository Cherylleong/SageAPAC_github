// Created in C#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sage50.Types
{
    public class PURCHASE_COMMON_ORDER_INVOICE : PURCHASE
    {
        public GL_ACCOUNT PaidFromAccount = new GL_ACCOUNT();
        public string paymentMethod { get; set; }
        public string chequeNumber { get; set; }
    }
}
