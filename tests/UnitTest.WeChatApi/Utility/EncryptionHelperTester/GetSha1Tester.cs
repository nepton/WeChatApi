using System.Text;
using WeChatApi.RestSharp.Tenpay.Utility;

namespace UnitTest.WeChatApi.Utility.EncryptionHelperTester;

public class GetSha1Tester
{
    [Fact]
    public void TestGetSha1()
    {
        string str = "This is a test";
        string md5 = EncryptHelper.GetSha1(str);
        Assert.Equal("a54d88e06612d820bc3be72877c74f257b561b19", md5);
    }
}
