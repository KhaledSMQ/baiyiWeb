using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace payapi_mobile_demo
{
    class Config
    {
        static Config()
        {
            //商户账户编号
            merchantAccount = ConfigurationManager.AppSettings["fastYeepayPartner"].ToString();

            //商户RSA密钥对——公钥，商户通过openssl工具生成(请见“RSA密钥对生成说明.txt”)，该公钥需要在商户后台向易宝支付报备
            //商户后台(测试环境http://mobiletest.yeepay.com/merchant,正式环境http://ok.yeepay.com/merchant)
            merchantPublickey = "MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQDYl0hJ9tq3staIHS9paqvozogy/pb8I7yqpEcUWy2RsbBnbsZPSp9JumAPvja+TKSGUYZfXSrCzjiuII3JhXC3bmyVieLzblgUsOmeyNazEl166pxxCLQyQy82RqrxFDIi+iPiwyixXCLSaGNX1X2Bgk5FHMGgxreQoqN+Uz7RgQIDAQAB";

            //商户RSA密钥对——私钥，商户通过openssl工具生成(请见“RSA密钥对生成说明.txt”)
            merchantPrivatekey = "MIICdQIBADANBgkqhkiG9w0BAQEFAASCAl8wggJbAgEAAoGBANiXSEn22rey1ogdL2lqq+jOiDL+lvwjvKqkRxRbLZGxsGduxk9Kn0m6YA++Nr5MpIZRhl9dKsLOOK4gjcmFcLdubJWJ4vNuWBSw6Z7I1rMSXXrqnHEItDJDLzZGqvEUMiL6I+LDKLFcItJoY1fVfYGCTkUcwaDGt5Cio35TPtGBAgMBAAECgYByrp/DMicbL2FyjumEysudqIXrYmx1s0J5pCRSvfiB9XDvQ3NTlrKC6mFk1JXN620OBeq9Yep7XZAbevc4ZiSIslHfQcFOhpq3euBpapdbdRa+mWadhSoZ+YB99zkixCDcyOaQKADi4KJr26UnmdHWWY5Fh6kbTbRbaMM118EvcQJBAO//CqeK2A5HDcxZih33r/Z+gpB3WxQ4MX0q0FjcKDm6AfjK75LkuysHcmLi73b+5vcnwgd4TxG8fOkWXv28zwUCQQDnCLDBL7bO9d7UZH5RYPDiNVxX1Y00FkLzba8Enh3WijYbKlyg1AEodWHKo1C3uEkeBy/2qJNDW6NHa3KpNelNAkBkA0W+Ykb9VDD02s+LA4Ap2bixWXv0JiLBhYkDruN4gwJ1WqSR843oNZc+jFG8pic8Ei5yjHlu67ymKfN2DCu9AkADLR5o+YP04nJ2zw7hhYiqQ3uKhZgUYD35ZMekM5xLZ8kIpJNzbpa5fKukgoxIilMPA6BILtcfdPIQuExyQRh9AkAVWFGUluLsvaLLRartiu6Eg8DOS+zx1rqs4R6nvUebYEwlYsac07l7+CUkHizfh0cEtCBcltXnnQNGYvyHQiNj";

            //易宝支付分配的公钥，该公钥由商户进入商户后台先上报自己的公钥再获取，商户后台目录为（产品管理——RSA公钥管理）
            //商户后台(测试环境http://mobiletest.yeepay.com/merchant,正式环境http://ok.yeepay.com/merchant)
            yibaoPublickey = "MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQC85XC2uTnYyaHvj1bedZ45o83j+y9cRG7hpOMdlgi4XHmIFcp2iMIOYNPQUl7Zt7IjfnzRXQmT1n+vzsRImWh81X106hnzWLOORzG2lJHUXrRDZJQU+f4e+74EzsoG8sN7GJaR058AOe+fJo/PGIQovvD+LVdNWvuJ51Deno+KsQIDAQAB";
        }

        public static string merchantAccount
        { get; set; }

        public static string merchantPublickey
        { get; set; }

        public static string merchantPrivatekey
        { get; set; }

        public static string yibaoPublickey
        { get; set; }
    }
}
