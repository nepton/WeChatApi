using System.Text;
using WeChatApi.RestSharp.Tenpay.Utility;

namespace UnitTest.WeChatApi.Utility.EncryptionHelperTester;

public class GetHmacSha256Tester
{
    [Fact]
    public void TestGetHmacSha256()
    {
        string str = "This is a test string";
        string actual = EncryptHelper.GetHmacSha256(str, "123456");
        Assert.Equal("3bdc06c0e4a465fa19f658faf37e5dcd23fc73a0ae0762e7d005e840e5960113", actual);
    }
}
