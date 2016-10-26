using System;
using System.Text;
using System.Web;
using System.Configuration;
namespace tenpayApp
{
	/// <summary>
	/// TenpayUtil 的摘要说明。
	/// </summary>
	public class TenpayUtil
	{
        public static string tenpay = ConfigurationManager.AppSettings["tenPay"];
        public static string bargainor_id = ConfigurationManager.AppSettings["tenPayUID"];      //财付通商户号
        public static string tenpay_key = ConfigurationManager.AppSettings["tenPayKey"];  		//财付通密钥;
        public static string tenpay_return = ConfigurationManager.AppSettings["tenPayCallback"];//显示支付通知页面;
        public static string tenpay_notify = ConfigurationManager.AppSettings["tenPayNotifyback"]; //支付完成后的回调处理页面;

		public TenpayUtil()
		{

            tenpay = ConfigurationManager.AppSettings["tenPay"];
            bargainor_id = ConfigurationManager.AppSettings["tenPayUID"];
            tenpay_key = ConfigurationManager.AppSettings["tenPayKey"];
            tenpay_return = ConfigurationManager.AppSettings["tenPayCallback"];
            tenpay_notify = ConfigurationManager.AppSettings["tenPayNotifyback"];
		}
		/** 对字符串进行URL编码 */
		public static string UrlEncode(string instr, string charset)
		{
			//return instr;
			if(instr == null || instr.Trim() == "")
				return "";
			else
			{
				string res;
				
				try
				{
					res = HttpUtility.UrlEncode(instr,Encoding.GetEncoding(charset));

				}
				catch (Exception ex)
				{
					res = HttpUtility.UrlEncode(instr,Encoding.GetEncoding("GB2312"));
				}
				
		
				return res;
			}
		}

		/** 对字符串进行URL解码 */
		public static string UrlDecode(string instr, string charset)
		{
			if(instr == null || instr.Trim() == "")
				return "";
			else
			{
				string res;
				
				try
				{
					res = HttpUtility.UrlDecode(instr,Encoding.GetEncoding(charset));

				}
				catch (Exception ex)
				{
					res = HttpUtility.UrlDecode(instr,Encoding.GetEncoding("GB2312"));
				}
				
		
				return res;

			}
		}

		/** 取时间戳生成随即数,替换交易单号中的后10位流水号 */
		public static UInt32 UnixStamp()
		{
			TimeSpan ts = DateTime.Now - TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
			return Convert.ToUInt32(ts.TotalSeconds);
		}
		/** 取随机数 */
		public static string BuildRandomStr(int length) 
		{
			Random rand = new Random();

			int num = rand.Next();

			string str = num.ToString();

			if(str.Length > length)
			{
				str = str.Substring(0,length);
			}
			else if(str.Length < length)
			{
				int n = length - str.Length;
				while(n > 0)
				{
					str.Insert(0, "0");
					n--;
				}
			}
			
			return str;
		}
	}
}