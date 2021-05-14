using TwoCheckout;
using Xunit;

namespace Twocheckout.Tests
{
    public class TestNotification
    {
        // Set Credentials
        [Fact]
        public void _002_SetKey()
        {
            TwoCheckoutConfig.SecretWord = "tango";
            TwoCheckoutConfig.SellerID = "1817037";
        }

        // API Authorization
        [Fact]
        public void _001_TestNotificationCheck()
        {
            var Notification = new NotificationService();
            var Args = new NotificationCheckServiceOptions();
            Args.invoice_id = "4632527490";
            Args.sale_id = "4632527448";
            Args.md5_hash = "4FB7CD1CD57BBEFCCA462F3DE823C50A";
            var result = Notification.Check(Args);
            Assert.True(result);
        }
    }
}
