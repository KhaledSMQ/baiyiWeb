/*
 * 版本：4.0
 * 时间：2011-5-11
 * 作者：http://www.foxuc.com
 *
 * 描述：实体类
 *
 */

using System;
using System.Collections.Generic;

namespace Game.Entity.Platform
{
	/// <summary>
	/// 实体类 RecordShopConvert。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class RecordShopConvert  
	{
		#region 常量

		/// <summary>
		/// 表名
		/// </summary>
		public const string Tablename = "RecordShopConvert" ;

		/// <summary>
		/// 类型标识
		/// </summary>
		public const string _ConvertID = "ConvertID" ;

		/// <summary>
		/// 用户ID
		/// </summary>
		public const string _UserID = "UserID" ;

		/// <summary>
		/// 商品ID
		/// </summary>
		public const string _ShopID = "ShopID" ;

		/// <summary>
		/// 身份证号码
		/// </summary>
		public const string _PassPortID = "PassPortID" ;

        /// <summary>
		/// 身份证号码
		/// </summary>
		public const string _Compellation = "Compellation" ;

        /// <summary>
		/// QQ
		/// </summary>
		public const string _QQ = "QQ" ;

        /// <summary>
		/// 邮箱
		/// </summary>
		public const string _EMail = "EMail" ;
        
		/// <summary>
		/// 手机号码
		/// </summary>
		public const string _MobilePhone = "MobilePhone" ;

        /// <summary>
		/// 详细地址
		/// </summary>
		public const string _DwellingPlace = "DwellingPlace" ;
        
        /// <summary>
		/// 邮政编码
		/// </summary>
		public const string _PostalCode = "PostalCode" ;

        /// <summary>
		/// 兑换数量
		/// </summary>
		public const string _ConvertNum = "ConvertNum" ;

        /// <summary>
		/// 兑换地址
		/// </summary>
		public const string _ConvertIP = "ConvertIP" ;

        /// <summary>
        /// 是否发货
		/// </summary>
		public const string _SendGood = "SendGood" ;


		#endregion

		#region 私有变量
		private int m_ShopID;				//商品ID
		private int m_ConvertID;		    //兑换标示
		private int m_UserID;			    //用户ID
		private string m_PassPortID;	    //身份证号码
		private string m_Compellation;		//真实姓名
		private string m_QQ;		        //QQ号码
		private string m_EMail;		        //邮箱
		private string m_MobilePhone;		//手机号码
        private string m_DwellingPlace;     //真实地址
        private string m_PostalCode;        //邮政编码
        private int m_ConvertNum;           //兑换个数
        private byte m_SendGood;            //是否发货
        private string m_ConvertIP;         //兑换地址
		#endregion

		#region 构造方法

		/// <summary>
		/// 初始化GameTypeItem
		/// </summary>
        public RecordShopConvert()
		{
            m_ShopID = 0;
            m_ConvertID = 0;
            m_UserID = 0;
            m_PassPortID = "";
            m_Compellation = "";
            m_QQ = "";
            m_EMail = "";
            m_MobilePhone = "";
            m_DwellingPlace = "";
            m_PostalCode = "";
            m_ConvertNum = 0;
            m_SendGood = 0;
            m_ConvertIP = "";
            
		}

		#endregion

		#region 公共属性

		/// <summary>
		/// 商品ID
		/// </summary>
        public int ShopID
		{
            get { return m_ShopID; }
            set { m_ShopID = value; }
		}

		/// <summary>
		/// 兑换ID
		/// </summary>
        public int ConvertID
		{
            get { return m_ConvertID; }
            set { m_ConvertID = value; }
		}

		/// <summary>
		/// 用户ID
		/// </summary>
        public int UserID
		{
            get { return m_UserID; }
            set { m_UserID = value; }
		}

		/// <summary>
		/// 身份证号码
		/// </summary>
        public string PassPortID
		{
            get { return m_PassPortID; }
            set { m_PassPortID = value; }
		}

        /// <summary>
        /// 真实姓名
        /// </summary>
        public string Compellation
        {
            get { return m_Compellation; }
            set { m_Compellation = value; }
        }

        /// <summary>
        /// QQ
        /// </summary>
        public string QQ
        {
            get { return m_QQ; }
            set { m_QQ = value; }
        }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string EMail
        {
            get { return m_EMail; }
            set { m_EMail = value; }
        }

		/// <summary>
		/// 手机号码
		/// </summary>
        public string MobilePhone
		{
            get { return m_MobilePhone; }
            set { m_MobilePhone = value; }
		}

        /// <summary>
        /// 邮政编码
        /// </summary>
        public string PostalCode
        {
            get { return m_PostalCode; }
            set { m_PostalCode = value; }
        }

        /// <summary>
        /// 详细地址
        /// </summary>
        public string DwellingPlace
        {
            get { return m_DwellingPlace; }
            set { m_DwellingPlace = value; }
        }

        /// <summary>
        /// 兑换数目
        /// </summary>
        public int ConvertNum
        {
            get { return m_ConvertNum; }
            set { m_ConvertNum = value; }
        }

        /// <summary>
        /// 是否发货
        /// </summary>
        public byte SendGood
        {
            get { return m_SendGood; }
            set { m_SendGood = value; }
        }

        /// <summary>
        /// 兑换地址
        /// </summary>
        public string ConvertIP
        {
            get { return m_ConvertIP; }
            set { m_ConvertIP = value; }
        }

		#endregion
	}
}
