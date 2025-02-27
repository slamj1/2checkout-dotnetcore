﻿using System;
using System.Collections.Generic;

namespace TwoCheckout
{
    public class AccountService
    {

        public Company CompanyInfo()
        {
            string Result = TwoCheckoutUtil.Request("api/acct/detail_company_info", "GET", "admin");
            return TwoCheckoutUtil.MapToObject<Company>(Result, "vendor_company_info");
        }

        public Contact ContactInfo()
        {
            string Result = TwoCheckoutUtil.Request("api/acct/detail_contact_info", "GET", "admin");
            return TwoCheckoutUtil.MapToObject<Contact>(Result, "vendor_contact_info");
        }

        public Payment PendingPayment()
        {
            string Result = TwoCheckoutUtil.Request("api/acct/detail_pending_payment", "GET", "admin");
            return TwoCheckoutUtil.MapToObject<Payment>(Result, "payment");
        }

        public List<Payment> ListPayments()
        {
            string Result = TwoCheckoutUtil.Request("api/acct/list_payments", "GET", "admin");
            return TwoCheckoutUtil.MapToObject <List<Payment>>(Result, "payments");
        }

    }
}
