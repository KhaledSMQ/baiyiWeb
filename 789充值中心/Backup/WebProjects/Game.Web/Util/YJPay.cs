using System;
using System.Collections.Generic;
using System.Text;
using RSA.Class;

namespace payapi_mobile_demo
{
    class YJPay
    {
        //商户账户编号
        public static string merchantAccount = Config.merchantAccount;

        //商户私钥（商户公钥对应的私钥）
        public static string merchantPrivatekey = Config.merchantPrivatekey;

        //易宝支付分配的公钥（进入商户后台公钥管理，报备商户的公钥后分派的字符串）
        public static string yibaoPublickey = Config.yibaoPublickey;

        //一键支付URL前缀
        public string apiprefix = APIURLConfig.apiprefix;
        //一键支付商户通用接口URL前缀
        public string apimercahntprefix = APIURLConfig.merchantPrefix;

        /// <summary>
        /// 易宝向用户发生短信验证码，即用户必须输入接收到的短信验证码后才能完成支付
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        public string sendValidateCode(string orderid)
        {
            SortedDictionary<string, object> sd = new SortedDictionary<string, object>();
            sd.Add("orderid", orderid);
            sd.Add("merchantaccount", merchantAccount);

            //发生短信验证码
            string uri = APIURLConfig.sendValidateCodeURI;

            string viewYbResult = createDataAndRequestYb(sd, uri,true);

            return viewYbResult;
        }

        /// <summary>
        /// 确认支付
        /// </summary>
        /// <param name="orderid">商户订单号</param>
        /// <param name="validatecode">短信验证码</param>
        /// <returns></returns>
        public string confirmPay(string orderid, string validatecode)
        {
            SortedDictionary<string, object> sd = new SortedDictionary<string, object>();
            sd.Add("orderid", orderid);
            if (validatecode != null)
            {
                if (validatecode != "")
                {
                    sd.Add("validatecode", validatecode);
                }
            }
            
            sd.Add("merchantaccount", merchantAccount);

            //确认支付
            string uri = APIURLConfig.confirmPayURI;

            string viewYbResult = createDataAndRequestYb(sd, uri,true);

            return viewYbResult;
        }

        /// <summary>
        /// 获取绑卡关系列表
        /// </summary>
        /// <param name="identityid">支付身份标识</param>
        /// <param name="identitytype">支付身份类型</param>
        /// <returns></returns>
        public string getBindList(string identityid, int identitytype)
        {
            SortedDictionary<string, object> sd = new SortedDictionary<string, object>();
            sd.Add("identityid", identityid);
            sd.Add("identitytype", identitytype);
            sd.Add("merchantaccount", merchantAccount);

            string uri = APIURLConfig.bindlistURI;

            string viewYbResult = createDataAndRequestYb(sd, uri, false);

            return viewYbResult;
        }

        /// <summary>
        /// 绑卡支付请求
        /// </summary>
        /// <param name="bindid">绑卡id</param>
        /// <param name="amount">支付金额（单位：分）</param>
        /// <param name="currency">币种</param>
        /// <param name="identityid">支付身份标识</param>
        /// <param name="identitytype">支付身份标识类型</param>
        /// <param name="orderid">商户订单号</param>
        /// <param name="other">其他用户身份信息</param>
        /// <param name="productcatalog">商品类别</param>
        /// <param name="productdesc">商品描述</param>
        /// <param name="productname">商品名称</param>
        /// <param name="transtime">交易时间</param>
        /// <param name="userip">用户ip</param>
        /// <param name="callbackurl">商户后台回调地址</param>
        /// <param name="fcallbackurl">商户前台回调地址</param>
        /// <param name="terminaltype">终端设备类型</param>
        /// <param name="terminalid">终端设备id</param>
        /// <returns></returns>
        public string bindPayRequest(string bindid, int amount, int currency, string identityid, int identitytype,
            string orderid, string other, string productcatalog, string productdesc, string productname, int transtime,
            string userip, string callbackurl, string fcallbackurl,int terminaltype, string terminalid)
        {
            SortedDictionary<string, object> sd = new SortedDictionary<string, object>();
            sd.Add("merchantaccount", merchantAccount);
            sd.Add("bindid", bindid);
            sd.Add("amount", amount);
            sd.Add("currency", currency);
            sd.Add("identityid", identityid);
            sd.Add("identitytype", identitytype);
            sd.Add("orderid", orderid);
            sd.Add("other", other);
            sd.Add("productcatalog", productcatalog);
            sd.Add("productdesc", productdesc);
            sd.Add("productname", productname);
            sd.Add("transtime", transtime);
            sd.Add("userip", userip);
            sd.Add("callbackurl", callbackurl);
            sd.Add("fcallbackurl", fcallbackurl);
            sd.Add("terminaltype", terminaltype);
            sd.Add("terminalid", terminalid);

            string uri = APIURLConfig.bindpayURI;

            string viewYbResult = createDataAndRequestYb(sd, uri, true);

            return viewYbResult;
        }

        /// <summary>
        /// 查询支付结果（可以在确认支付接口后调用）
        /// </summary>
        /// <param name="orderid">商户订单号</param>
        /// <returns></returns>
        public string queryPayResult(string orderid)
        {
            SortedDictionary<string, object> sd = new SortedDictionary<string, object>();
            sd.Add("merchantaccount", merchantAccount);
            sd.Add("orderid", orderid);

            string uri = APIURLConfig.queryPayResultURI;

            string viewYbResult = createDataAndRequestYb(sd, uri, false);

            return viewYbResult;
        }

        /// <summary>
        /// 解绑卡
        /// </summary>
        /// <param name="identityid">用户支付身份标识</param>
        /// <param name="identitytype">用支付身份标识类型</param>
        /// <param name="bindid">绑卡id</param>
        /// <returns></returns>
        public string unbind(string identityid, int identitytype, string bindid)
        {
            SortedDictionary<string, object> sd = new SortedDictionary<string, object>();
            sd.Add("identityid", identityid);
            sd.Add("identitytype", identitytype);
            sd.Add("merchantaccount", merchantAccount);
            sd.Add("bindid", bindid);

            string uri = APIURLConfig.unbindURI;

            string viewYbResult = createDataAndRequestYb(sd, uri, true);

            return viewYbResult;
        }

        /// <summary>
        /// 交易订单查询（商户通用接口）
        /// </summary>
        /// <param name="orderid">商户订单号</param>
        /// <param name="yborderid">易宝返回的订单号</param>
        /// <returns></returns>
        public string queryPayOrderInfo(string orderid, string yborderid)
        {
            SortedDictionary<string, object> sd = new SortedDictionary<string, object>();
            sd.Add("merchantaccount", merchantAccount);
            if (orderid != null)
            {
                if (orderid.Trim() != "")
                {
                    sd.Add("orderid", orderid);
                }
            }
            if (yborderid != null)
            {
                if (yborderid.Trim() != "")
                {
                    sd.Add("yborderid", yborderid);
                }
            }
            string uri = APIURLConfig.queryOrderURI;

            string viewYbResult = createMerchantDataAndRequestYb(sd, uri, false);

            return viewYbResult;
            
        }

        /// <summary>
        /// 直接退款（商户通用接口）
        /// </summary>
        /// <param name="orderid">商户退款订单号</param>
        /// <param name="origyborderid">原来易宝支付交易订单号</param>
        /// <param name="amount">退款金额（单位：分）</param>
        /// <param name="currency">币种</param>
        /// <param name="cause">退款原因</param>
        /// <returns></returns>
        public string directRefund(string orderid, string origyborderid,int amount,int currency,string cause )
        {
            SortedDictionary<string, object> sd = new SortedDictionary<string, object>();
            sd.Add("merchantaccount", merchantAccount);
            sd.Add("orderid", orderid);
            sd.Add("origyborderid", origyborderid);
            sd.Add("amount", amount);
            sd.Add("currency", currency);
            sd.Add("cause", cause);

            string uri = APIURLConfig.directFundURI;

            string viewYbResult = createMerchantDataAndRequestYb(sd, uri, true);

            return viewYbResult;

        }

        /// <summary>
        /// 查询退款订单信息（商户通用接口）
        /// </summary>
        /// <param name="orderid">商户退货订单号</param>
        /// <param name="yborderid">原来易宝支付退款流水号</param>
        /// <returns></returns>
        public string queryRefundOrder(string orderid, string yborderid)
        {
            SortedDictionary<string, object> sd = new SortedDictionary<string, object>();
            sd.Add("merchantaccount", merchantAccount);
            sd.Add("orderid", orderid);
            sd.Add("yborderid", yborderid);

            string uri = APIURLConfig.queryRefundURI;

            string viewYbResult = createMerchantDataAndRequestYb(sd, uri, false);

            return viewYbResult;

        }


        /// <summary>
        /// 根据银行卡卡号检查银行卡是否可以使用一键支付
        /// </summary>
        /// <param name="cardno">银行卡号</param>
        /// <returns></returns>
        public string bankCardCheck(string cardno)
        {   
            SortedDictionary<string, object> sd = new SortedDictionary<string, object>();
            sd.Add("cardno", cardno);
            sd.Add("merchantaccount", merchantAccount);
            
            //根据银行卡卡号检查银行卡是否可以使用一键支付接口
            string uri = APIURLConfig.bankcardCheckURI;

            string viewYbResult = createDataAndRequestYb(sd, uri,true);

            return viewYbResult;
        }

        /// <summary>
        /// 获取消费清算对账单（商户通用接口）
        /// </summary>
        /// <param name="startdate">开始日期，如:2014-03-01</param>
        /// <param name="enddate">结束日期，如:2014-03-01</param>
        /// <returns></returns>
        public string getClearPayData(string startdate, string enddate)
        {
            SortedDictionary<string, object> sd = new SortedDictionary<string, object>();
            sd.Add("merchantaccount", merchantAccount);
            sd.Add("startdate", startdate);
            sd.Add("enddate", enddate);

            string uri = APIURLConfig.clearPayDataURI;

            string viewYbResult = createMerchantDataAndRequestYb2(sd, uri, false);

            return viewYbResult;

        }

        /// <summary>
        /// 获取消费清算对账单（商户通用接口）
        /// </summary>
        /// <param name="startdate">开始日期，如:2014-03-01</param>
        /// <param name="enddate">结束日期，如:2014-03-01</param>
        /// <returns></returns>
        public string getClearRefundData(string startdate, string enddate)
        {
            SortedDictionary<string, object> sd = new SortedDictionary<string, object>();
            sd.Add("merchantaccount", merchantAccount);
            sd.Add("startdate", startdate);
            sd.Add("enddate", enddate);

            string uri = APIURLConfig.clearRefundDataURI;

            string viewYbResult = createMerchantDataAndRequestYb2(sd, uri, false);

            return viewYbResult;

        }



        /// <summary>
        /// 将请求接口中的业务明文参数加密并请求一键支付接口
        /// </summary>
        /// <param name="sd"></param>
        /// <param name="apiUri"></param>
        /// <returns></returns>
        private string createDataAndRequestYb(SortedDictionary<string, object> sd,string apiUri,bool ispost)
        {
            //随机生成商户AESkey
            string merchantAesKey = AES.GenerateAESKey();

            //生成RSA签名
            string sign = EncryptUtil.handleRSA(sd, merchantPrivatekey);
            sd.Add("sign", sign);


            //将对象转换为json字符串
            string bpinfo_json = Newtonsoft.Json.JsonConvert.SerializeObject(sd);
            string datastring = AES.Encrypt(bpinfo_json, merchantAesKey);
           
            //将商户merchantAesKey用RSA算法加密
            string encryptkey = RSAFromPkcs8.encryptData(merchantAesKey, yibaoPublickey, "UTF-8");

            String ybResult = "";

            if (ispost)
            {
                 ybResult = YJPayUtil.payAPIRequest(apiprefix + apiUri, datastring, encryptkey, true);
            }
            else
            {
                ybResult = YJPayUtil.payAPIRequest(apiprefix + apiUri, datastring, encryptkey, false);
            }
            String viewYbResult = YJPayUtil.checkYbResult(ybResult);

            return viewYbResult;

        }

        /// <summary>
        /// 将请求接口中的业务明文参数加密并请求一键支付接口--商户通用接口
        /// </summary>
        /// <param name="sd"></param>
        /// <param name="apiUri"></param>
        /// <returns></returns>
        private string createMerchantDataAndRequestYb(SortedDictionary<string, object> sd, string apiUri, bool ispost)
        {
            //随机生成商户AESkey
            string merchantAesKey = AES.GenerateAESKey();

            //生成RSA签名
            string sign = EncryptUtil.handleRSA(sd, merchantPrivatekey);
            sd.Add("sign", sign);


            //将对象转换为json字符串
            string bpinfo_json = Newtonsoft.Json.JsonConvert.SerializeObject(sd);
            string datastring = AES.Encrypt(bpinfo_json, merchantAesKey);

            //将商户merchantAesKey用RSA算法加密
            string encryptkey = RSAFromPkcs8.encryptData(merchantAesKey, yibaoPublickey, "UTF-8");

            String ybResult = "";

            if (ispost)
            {
                ybResult = YJPayUtil.payAPIRequest(apimercahntprefix + apiUri, datastring, encryptkey, true);
            }
            else
            {
                ybResult = YJPayUtil.payAPIRequest(apimercahntprefix + apiUri, datastring, encryptkey, false);
            }
            String viewYbResult = YJPayUtil.checkYbResult(ybResult);

            return viewYbResult;

        }

        /// <summary>
        /// 将请求接口中的业务明文参数加密并请求一键支付接口，单不对返回的数据进行解密，用于获取清算对账单接口--商户通用接口
        /// </summary>
        /// <param name="sd"></param>
        /// <param name="apiUri"></param>
        /// <returns></returns>
        private string createMerchantDataAndRequestYb2(SortedDictionary<string, object> sd, string apiUri, bool ispost)
        {
            //随机生成商户AESkey
            string merchantAesKey = AES.GenerateAESKey();

            //生成RSA签名
            string sign = EncryptUtil.handleRSA(sd, merchantPrivatekey);
            sd.Add("sign", sign);


            //将对象转换为json字符串
            string bpinfo_json = Newtonsoft.Json.JsonConvert.SerializeObject(sd);
            string datastring = AES.Encrypt(bpinfo_json, merchantAesKey);

            //将商户merchantAesKey用RSA算法加密
            string encryptkey = RSAFromPkcs8.encryptData(merchantAesKey, yibaoPublickey, "UTF-8");

            String ybResult = "";

            if (ispost)
            {
                ybResult = YJPayUtil.payAPIRequest(apimercahntprefix + apiUri, datastring, encryptkey, true);
            }
            else
            {
                ybResult = YJPayUtil.payAPIRequest(apimercahntprefix + apiUri, datastring, encryptkey, false);
            }
            
            return YJPayUtil.checkYbClearResult(ybResult);

        }
    }
}
