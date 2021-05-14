using System;

namespace TwoCheckout
{
    public static class TwoCheckoutConfig
    {
        public static string ApiUsername { get; set; }
        public static string ApiPassword { get; set; }
        public static string SellerID { get; set; }
        public static string PrivateKey { get; set; }
        public static string SecretWord { get; set; }
        public static Boolean Demo { get; set; }
        public static string BaseUrl = "https://www.2checkout.com/";
        //public static string Version = "5.0.0";
        public static string Version = "6.0.0";

        //public static string BaseUrl = "https://api.2checkout.com/rest/6.0/";
        //public static string Version = "6.0.0";
    }
}
