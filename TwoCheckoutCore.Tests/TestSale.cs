using TwoCheckout;
using Xunit;

namespace Twocheckout.Tests
{
    public class TestSale
    {
        // Sale Info
        long sale_id = 4828598838;
        long lineitem_id = 4828598922;
        long invoice_id = 4828598847;

        public TestSale()
        {
            // Set API Credentials
            TwoCheckoutConfig.ApiUsername = "yourapiaccount";
            TwoCheckoutConfig.ApiPassword = @"yourapiaccountpassword";
        }

        // List Sales
        [Fact]
        public void _002_TestSaleList()
        {
            try
            {
                var ServiceObject = new SaleService();
                var ArgsObject = new SaleListServiceOptions();
                ArgsObject.customer_email = "noreply@2co.com";
                var result = ServiceObject.List(ArgsObject);
                Assert.IsType<SaleList>(result);
            }
            catch (TwoCheckoutException e)
            {
                Assert.IsType<TwoCheckoutException>(e);
            }
        }

        // Detail Sale
        [Fact]
        public void _003_TestSaleRetrieve()
        {
            try
            {
                var ServiceObject = new SaleService();
                var ArgsObject = new SaleRetrieveServiceOptions();
                ArgsObject.sale_id = sale_id;
                var result = ServiceObject.Retrieve(ArgsObject);
                Assert.IsType<Sale>(result);
            }
            catch (TwoCheckoutException e)
            {
                Assert.IsType<TwoCheckoutException>(e);
            }
        }

        // Sale Stop Recurring Sale
        [Fact]
        public void _004_TestSaleStop()
        {
            try
            {
                var ServiceObject = new SaleService();
                var ArgsObject = new SaleStopServiceOptions();
                ArgsObject.sale_id = sale_id;
                var result = ServiceObject.Stop(ArgsObject);
                Assert.IsType<TwoCheckoutResponse>(result);
            }
            catch (TwoCheckoutException e)
            {
                Assert.IsType<TwoCheckoutException>(e);
            }
        }

        // Sale Stop Recurring Lineitem
        [Fact]
        public void _005_TestLineitemStop()
        {
            try
            {
                var ServiceObject = new SaleService();
                var ArgsObject = new SaleStopServiceOptions();
                ArgsObject.lineitem_id = lineitem_id;
                var result = ServiceObject.Stop(ArgsObject);
                Assert.IsType<TwoCheckoutResponse>(result);
            }
            catch (TwoCheckoutException e)
            {
                Assert.IsType<TwoCheckoutException>(e);
            }
        }

        // Sale Refund Sale
        [Fact]
        public void _006_TestSaleRefund()
        {
            try
            {
                var ServiceObject = new SaleService();
                var ArgsObject = new SaleRefundServiceOptions();
                ArgsObject.sale_id = sale_id;
                ArgsObject.comment = "test refund";
                ArgsObject.category = 5;
                var result = ServiceObject.Refund(ArgsObject);
                Assert.IsType<TwoCheckoutResponse>(result);
            }
            catch (TwoCheckoutException e)
            {
                Assert.IsType<TwoCheckoutException>(e);
            }
        }

        // Sale Refund Invoice
        [Fact]
        public void _007_TestInvoiceRefund()
        {
            try
            {
                var ServiceObject = new SaleService();
                var ArgsObject = new SaleRefundServiceOptions();
                ArgsObject.invoice_id = invoice_id;
                ArgsObject.comment = "test refund";
                ArgsObject.category = 5;
                var result = ServiceObject.Refund(ArgsObject);
                Assert.IsType<TwoCheckoutResponse>(result);
            }
            catch (TwoCheckoutException e)
            {
                Assert.IsType<TwoCheckoutException>(e);
            }
        }

        // Sale Refund Linitem
        [Fact]
        public void _008_TestLineitemRefund()
        {
            try
            {
                var ServiceObject = new SaleService();
                var ArgsObject = new SaleRefundServiceOptions();
                ArgsObject.lineitem_id = lineitem_id;
                ArgsObject.comment = "test refund";
                ArgsObject.category = 5;
                var result = ServiceObject.Refund(ArgsObject);
                Assert.IsType<TwoCheckoutResponse>(result);
            }
            catch (TwoCheckoutException e)
            {
                Assert.IsType<TwoCheckoutException>(e);
            }
        }

        // Sale Ship
        [Fact]
        public void _010_TestSaleShipped()
        {
            try
            {
                var ServiceObject = new SaleService();
                var ArgsObject = new SaleShipServiceOptions();
                ArgsObject.sale_id = sale_id;
                var result = ServiceObject.Ship(ArgsObject);
                Assert.IsType<TwoCheckoutResponse>(result);
            }
            catch (TwoCheckoutException e)
            {
                Assert.IsType<TwoCheckoutException>(e);
            }
        }

        // Sale Comment
        [Fact]
        public void _011_TestSaleComment()
        {
            try
            {
                var ServiceObject = new SaleService();
                var ArgsObject = new SaleCommentServiceOptions();
                ArgsObject.sale_id = sale_id;
                ArgsObject.sale_comment = "Test";
                var result = ServiceObject.Comment(ArgsObject);
                Assert.IsType<TwoCheckoutResponse>(result);
            }
            catch (TwoCheckoutException e)
            {
                Assert.IsType<TwoCheckoutException>(e);
            }
        }
    }
}
