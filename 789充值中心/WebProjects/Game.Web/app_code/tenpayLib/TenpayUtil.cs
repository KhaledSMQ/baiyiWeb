using System;
using System.Text;
using System.Web;
using System.Configuration;
namespace tenpayApp
{
	/// <summary>
	/// TenpayUtil ��ժҪ˵����
	/// </summary>
	public class TenpayUtil
	{
        public static string tenpay = ConfigurationManager.AppSettings["tenPay"];
        public static string bargainor_id = ConfigurationManager.AppSettings["tenPayUID"];      //�Ƹ�ͨ�̻���
        public static string tenpay_key = ConfigurationManager.AppSettings["tenPayKey"];  		//�Ƹ�ͨ��Կ;
        public static string tenpay_return = ConfigurationManager.AppSettings["tenPayCallback"];//��ʾ֧��֪ͨҳ��;
        public static string tenpay_notify = ConfigurationManager.AppSettings["tenPayNotifyback"]; //֧����ɺ�Ļص�����ҳ��;

		public TenpayUtil()
		{

            tenpay = ConfigurationManager.AppSettings["tenPay"];
            bargainor_id = ConfigurationManager.AppSettings["tenPayUID"];
            tenpay_key = ConfigurationManager.AppSettings["tenPayKey"];
            tenpay_return = ConfigurationManager.AppSettings["tenPayCallback"];
            tenpay_notify = ConfigurationManager.AppSettings["tenPayNotifyback"];
		}
		/** ���ַ�������URL���� */
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

		/** ���ַ�������URL���� */
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

		/** ȡʱ��������漴��,�滻���׵����еĺ�10λ��ˮ�� */
		public static UInt32 UnixStamp()
		{
			TimeSpan ts = DateTime.Now - TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
			return Convert.ToUInt32(ts.TotalSeconds);
		}
		/** ȡ����� */
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