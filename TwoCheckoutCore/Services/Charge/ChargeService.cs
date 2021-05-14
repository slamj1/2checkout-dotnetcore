using System.Collections.Generic;
using System.Text;

namespace TwoCheckout
{
    public class ChargeService
    {
        public static string CheckoutUrl = string.Concat(TwoCheckoutConfig.BaseUrl, "checkout/purchase");

        public static string Form(Dictionary<string, string> args, string button)
        {
            StringBuilder Form = new StringBuilder();
            Form.Append("<form id=\"2checkout\" action=\"" + CheckoutUrl + "\" method=\"post\">\n");
            foreach (var parameter in args)
            {
                Form.Append("<input type=\"hidden\" name=\"" + parameter.Key + "\" value=\"" + parameter.Value + "\" />\n");
            }
            Form.Append("<input type=\"submit\" value=\"" + button + "\" />\n</form>\n");
            return Form.ToString();
        }

        public static string Submit(Dictionary<string, string> args, string button)
        {
            StringBuilder Form = new StringBuilder();
            Form.Append("<form id=\"2checkout\" action=\"" + CheckoutUrl + "\" method=\"post\">\n");
            foreach (var parameter in args)
            {
                Form.Append("<input type=\"hidden\" name=\"" + parameter.Key + "\" value=\"" + parameter.Value + "\" />\n");
            }
            Form.Append("</form>\n");
            Form.Append("<script type=\"text/javascript\">document.getElementById('2checkout').submit();</script>\n");
            return Form.ToString();
        }

        public static string Link(Dictionary<string, string> args)
        {
            StringBuilder Link = new StringBuilder(CheckoutUrl);
            string Parameters = TwoCheckoutUtil.DictionaryToQueryString(args);
            Link.Append("?");
            Link.Append(Parameters);
            return Link.ToString();
        }

        public Authorization Authorize(ChargeAuthorizeServiceOptions options)
        {
            options.privateKey = TwoCheckoutConfig.PrivateKey;
            options.sellerId = TwoCheckoutConfig.SellerID;
            string Result = TwoCheckoutUtil.Request("checkout/api/1/" + TwoCheckoutConfig.SellerID + "/rs/authService", "POST", "payment", options);
            return TwoCheckoutUtil.MapToObject<Authorization>(Result, "response");
        }
    }
}