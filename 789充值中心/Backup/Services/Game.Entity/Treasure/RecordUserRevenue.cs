/*
 * 版本：4.0
 * 时间：2011-6-23
 * 作者：http://www.foxuc.com
 *
 * 描述：实体类
 *
 */

using System;
using System.Collections.Generic;

namespace Game.Entity.Treasure
{
	/// <summary>
	/// 实体类 RecordUserRevenue。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class RecordUserRevenue  
	{
		#region 常量

		/// <summary>
		/// 表名
		/// </summary>
		public const string Tablename = "RecordUserRevenue" ;

		/// <summary>
		/// 记录标识
		/// </summary>
		public const string _RecordID = "RecordID" ;

		/// <summary>
		/// 日期标识
		/// </summary>
		public const string _DateID = "DateID" ;

		/// <summary>
		/// 贡献用户
		/// </summary>
		public const string _UserID = "UserID" ;

		/// <summary>
		/// 站点标识
		/// </summary>
		public const string _StationID = "StationID" ;

		/// <summary>
		/// 税收
		/// </summary>
		public const string _Revenue = "Revenue" ;

		/// <summary>
		/// 1级抽水用户
		/// </summary>
		public const string _UserID1 = "UserID1" ;

		/// <summary>
		/// 1级抽水比例
		/// </summary>
		public const string _Scale1 = "Scale1" ;

		/// <summary>
		/// 1级抽水金额
		/// </summary>
		public const string _Revenue1 = "Revenue1" ;

		/// <summary>
		/// 2级抽水用户
		/// </summary>
		public const string _UserID2 = "UserID2" ;

		/// <summary>
		/// 2级抽水比例
		/// </summary>
		public const string _Scale2 = "Scale2" ;

		/// <summary>
		/// 2级抽水金额
		/// </summary>
		public const string _Revenue2 = "Revenue2" ;

		/// <summary>
		/// 3级抽水用户
		/// </summary>
		public const string _UserID3 = "UserID3" ;

		/// <summary>
		/// 3级抽水比例
		/// </summary>
		public const string _Scale3 = "Scale3" ;

		/// <summary>
		/// 3级抽水金额
		/// </summary>
		public const string _Revenue3 = "Revenue3" ;

		/// <summary>
		/// 4级抽水用户
		/// </summary>
		public const string _UserID4 = "UserID4" ;

		/// <summary>
		/// 4级抽水比例
		/// </summary>
		public const string _Scale4 = "Scale4" ;

		/// <summary>
		/// 4级抽水金额
		/// </summary>
		public const string _Revenue4 = "Revenue4" ;

		/// <summary>
		/// 5级抽水用户
		/// </summary>
		public const string _UserID5 = "UserID5" ;

		/// <summary>
		/// 5级抽水比例
		/// </summary>
		public const string _Scale5 = "Scale5" ;

		/// <summary>
		/// 5级抽水金额
		/// </summary>
		public const string _Revenue5 = "Revenue5" ;

		/// <summary>
		/// 公司抽水比例
		/// </summary>
		public const string _CompanyScale = "CompanyScale" ;

		/// <summary>
		/// 公司税收
		/// </summary>
		public const string _CompanyRevenue = "CompanyRevenue" ;

		/// <summary>
		/// 收集时间
		/// </summary>
		public const string _CollectDate = "CollectDate" ;
		#endregion

		#region 私有变量
		private int m_recordID;						//记录标识
		private int m_dateID;						//日期标识
		private int m_userID;						//贡献用户
		private int m_stationID;					//站点标识
		private decimal m_revenue;					//税收
		private int m_userID1;						//1级抽水用户
		private decimal m_scale1;					//1级抽水比例
		private decimal m_revenue1;					//1级抽水金额
		private int m_userID2;						//2级抽水用户
		private decimal m_scale2;					//2级抽水比例
		private decimal m_revenue2;					//2级抽水金额
		private int m_userID3;						//3级抽水用户
		private decimal m_scale3;					//3级抽水比例
		private decimal m_revenue3;					//3级抽水金额
		private int m_userID4;						//4级抽水用户
		private decimal m_scale4;					//4级抽水比例
		private decimal m_revenue4;					//4级抽水金额
		private int m_userID5;						//5级抽水用户
		private decimal m_scale5;					//5级抽水比例
		private decimal m_revenue5;					//5级抽水金额
		private decimal m_companyScale;				//公司抽水比例
		private decimal m_companyRevenue;			//公司税收
		private DateTime m_collectDate;				//收集时间
		#endregion

		#region 构造方法

		/// <summary>
		/// 初始化RecordUserRevenue
		/// </summary>
		public RecordUserRevenue()
		{
			m_recordID=0;
			m_dateID=0;
			m_userID=0;
			m_stationID=0;
			m_revenue=0;
			m_userID1=0;
			m_scale1=0;
			m_revenue1=0;
			m_userID2=0;
			m_scale2=0;
			m_revenue2=0;
			m_userID3=0;
			m_scale3=0;
			m_revenue3=0;
			m_userID4=0;
			m_scale4=0;
			m_revenue4=0;
			m_userID5=0;
			m_scale5=0;
			m_revenue5=0;
			m_companyScale=0;
			m_companyRevenue=0;
			m_collectDate=DateTime.Now;
		}

		#endregion

		#region 公共属性

		/// <summary>
		/// 记录标识
		/// </summary>
		public int RecordID
		{
			get { return m_recordID; }
			set { m_recordID = value; }
		}

		/// <summary>
		/// 日期标识
		/// </summary>
		public int DateID
		{
			get { return m_dateID; }
			set { m_dateID = value; }
		}

		/// <summary>
		/// 贡献用户
		/// </summary>
		public int UserID
		{
			get { return m_userID; }
			set { m_userID = value; }
		}

		/// <summary>
		/// 站点标识
		/// </summary>
		public int StationID
		{
			get { return m_stationID; }
			set { m_stationID = value; }
		}

		/// <summary>
		/// 税收
		/// </summary>
		public decimal Revenue
		{
			get { return m_revenue; }
			set { m_revenue = value; }
		}

		/// <summary>
		/// 1级抽水用户
		/// </summary>
		public int UserID1
		{
			get { return m_userID1; }
			set { m_userID1 = value; }
		}

		/// <summary>
		/// 1级抽水比例
		/// </summary>
		public decimal Scale1
		{
			get { return m_scale1; }
			set { m_scale1 = value; }
		}

		/// <summary>
		/// 1级抽水金额
		/// </summary>
		public decimal Revenue1
		{
			get { return m_revenue1; }
			set { m_revenue1 = value; }
		}

		/// <summary>
		/// 2级抽水用户
		/// </summary>
		public int UserID2
		{
			get { return m_userID2; }
			set { m_userID2 = value; }
		}

		/// <summary>
		/// 2级抽水比例
		/// </summary>
		public decimal Scale2
		{
			get { return m_scale2; }
			set { m_scale2 = value; }
		}

		/// <summary>
		/// 2级抽水金额
		/// </summary>
		public decimal Revenue2
		{
			get { return m_revenue2; }
			set { m_revenue2 = value; }
		}

		/// <summary>
		/// 3级抽水用户
		/// </summary>
		public int UserID3
		{
			get { return m_userID3; }
			set { m_userID3 = value; }
		}

		/// <summary>
		/// 3级抽水比例
		/// </summary>
		public decimal Scale3
		{
			get { return m_scale3; }
			set { m_scale3 = value; }
		}

		/// <summary>
		/// 3级抽水金额
		/// </summary>
		public decimal Revenue3
		{
			get { return m_revenue3; }
			set { m_revenue3 = value; }
		}

		/// <summary>
		/// 4级抽水用户
		/// </summary>
		public int UserID4
		{
			get { return m_userID4; }
			set { m_userID4 = value; }
		}

		/// <summary>
		/// 4级抽水比例
		/// </summary>
		public decimal Scale4
		{
			get { return m_scale4; }
			set { m_scale4 = value; }
		}

		/// <summary>
		/// 4级抽水金额
		/// </summary>
		public decimal Revenue4
		{
			get { return m_revenue4; }
			set { m_revenue4 = value; }
		}

		/// <summary>
		/// 5级抽水用户
		/// </summary>
		public int UserID5
		{
			get { return m_userID5; }
			set { m_userID5 = value; }
		}

		/// <summary>
		/// 5级抽水比例
		/// </summary>
		public decimal Scale5
		{
			get { return m_scale5; }
			set { m_scale5 = value; }
		}

		/// <summary>
		/// 5级抽水金额
		/// </summary>
		public decimal Revenue5
		{
			get { return m_revenue5; }
			set { m_revenue5 = value; }
		}

		/// <summary>
		/// 公司抽水比例
		/// </summary>
		public decimal CompanyScale
		{
			get { return m_companyScale; }
			set { m_companyScale = value; }
		}

		/// <summary>
		/// 公司税收
		/// </summary>
		public decimal CompanyRevenue
		{
			get { return m_companyRevenue; }
			set { m_companyRevenue = value; }
		}

		/// <summary>
		/// 收集时间
		/// </summary>
		public DateTime CollectDate
		{
			get { return m_collectDate; }
			set { m_collectDate = value; }
		}
		#endregion
	}
}
