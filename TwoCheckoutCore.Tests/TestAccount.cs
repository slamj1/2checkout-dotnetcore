using TwoCheckout;
using Xunit;

namespace Twocheckout.Tests
{
    public class TestAccount
    {
        public TestAccount()
        {
            // Set API Credentials
            TwoCheckoutConfig.ApiUsername = "yourapiaccount";
            TwoCheckoutConfig.ApiPassword = @"yourapiaccountpassword";
        }

        // Create Coupon
        [Fact]
        public void _002_TestAccountContactInfo()
        {
            var ServiceObject = new AccountService();
            var result = ServiceObject.ContactInfo();
            Assert.IsType<Contact>(result);
        }

        // Create Coupon
        [Fact]
        public void _002_TestAccountCompanyInfo()
        {
            var ServiceObject = new AccountService();
            var result = ServiceObject.CompanyInfo();
            Assert.IsType<Company>(result);
        }

        // Create Coupon
        [Fact]
        public void _002_TestAccountPendingPayment()
        {
            var ServiceObject = new AccountService();
            var result = ServiceObject.PendingPayment();
            Assert.IsType<Payment>(result);
        }

        // Create Coupon
        [Fact]
        public void _002_TestAccountListPayments()
        {
            var ServiceObject = new AccountService();
            var result = ServiceObject.ListPayments();

            if (result.Count > 0)
            {
                Assert.IsType<Payment>(result[0]);
            } 
            else
            {
                Assert.True(true);
            }
        }


    }
}
