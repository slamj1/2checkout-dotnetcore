using System;

namespace TwoCheckout
{
    public class TwoCheckoutException : ApplicationException
    {
        public string Code { get; set; }

        public TwoCheckoutException(string message)
            : base(message)
        {

        }
    }
}

