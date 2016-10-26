using System;
using System.Collections.Generic;
using System.Text;

///
///一键支付API返回结果对象
///
namespace payapi_mobile_demo
{
    class RespondJson
    {
        /// <summary>
        ///加密的响应结果
        /// </summary>
        public string data;

        /// <summary>
        /// 加密的密文
        /// </summary>
        public string encryptkey;
    }
}
