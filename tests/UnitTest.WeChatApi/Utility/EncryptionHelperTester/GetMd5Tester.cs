using System.Text;
using WeChatApi.RestSharp.Tenpay.Utility;

namespace UnitTest.WeChatApi.Utility.EncryptionHelperTester;

public class GetMd5Tester
{
    [Fact]
    public void TestGetMd5()
    {
        string str = "123456";
        string md5 = EncryptHelper.GetMd5(str);
        Assert.Equal("e10adc3949ba59abbe56e057f20f883e", md5);
    }

    [Fact]
    public void TestGetMd5_2()
    {
        string str    = "This is a test";
        string actual = EncryptHelper.GetMd5(str);
        Assert.Equal("ce114e4501d2f4e2dcea3e17b546f339", actual);
    }
}
