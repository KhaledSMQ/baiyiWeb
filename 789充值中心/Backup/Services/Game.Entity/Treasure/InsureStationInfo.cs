/*
 * 版本：4.0
 * 时间：2011-5-17
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
	/// 实体类 InsureStationInfo。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class InsureStationInfo  
	{
		#region 常量

		/// <summary>
		/// 表名
		/// </summary>
		public const string Tablename = "InsureStationInfo" ;

		/// <summary>
		/// 站点标识
		/// </summary>
		public const string _StationID = "StationID" ;

		/// <summary>
		/// 银行密码
		/// </summary>
		public const string _Password = "Password" ;

		/// <summary>
		/// 金币余额
		/// </summary>
		public const string _GoldBalance = "GoldBalance" ;

		/// <summary>
		/// 金币信用
		/// </summary>
		public const string _GoldCredited = "GoldCredited" ;

		/// <summary>
		/// 渠道分成
		/// </summary>
		public const string _ChannelScale = "ChannelScale" ;

		/// <summary>
		/// 渠道收入
		/// </summary>
		public const string _StatChannelGold = "StatChannelGold" ;

		/// <summary>
		/// 创建时间
		/// </summary>
		public const string _CollectDate = "CollectDate" ;

		/// <summary>
		/// 统计时间
		/// </summary>
		public const string _StatLastDate = "StatLastDate" ;
		#endregion

		#region 私有变量
		private int m_stationID;					//站点标识
		private string m_password;					//银行密码
		private decimal m_goldBalance;				//金币余额
		private decimal m_goldCredited;				//金币信用
		private decimal m_channelScale;				//渠道分成
		private decimal m_statChannelGold;			//渠道收入
		private DateTime m_collectDate;				//创建时间
		private DateTime m_statLastDate;			//统计时间
		#endregion

		#region 构造方法

		/// <summary>
		/// 初始化InsureStationInfo
		/// </summary>
		public InsureStationInfo()
		{
			m_stationID=0;
			m_password="";
			m_goldBalance=0;
			m_goldCredited=0;
			m_channelScale=0;
			m_statChannelGold=0;
			m_collectDate=DateTime.Now;
			m_statLastDate=DateTime.Now;
		}

		#endregion

		#region 公共属性

		/// <summary>
		/// 站点标识
		/// </summary>
		public int StationID
		{
			get { return m_stationID; }
			set { m_stationID = value; }
		}

		/// <summary>
		/// 银行密码
		/// </summary>
		public string Password
		{
			get { return m_password; }
			set { m_password = value; }
		}

		/// <summary>
		/// 金币余额
		/// </summary>
		public decimal GoldBalance
		{
			get { return m_goldBalance; }
			set { m_goldBalance = value; }
		}

		/// <summary>
		/// 金币信用
		/// </summary>
		public decimal GoldCredited
		{
			get { return m_goldCredited; }
			set { m_goldCredited = value; }
		}

		/// <summary>
		/// 渠道分成
		/// </summary>
		public decimal ChannelScale
		{
			get { return m_channelScale; }
			set { m_channelScale = value; }
		}

		/// <summary>
		/// 渠道收入
		/// </summary>
		public decimal StatChannelGold
		{
			get { return m_statChannelGold; }
			set { m_statChannelGold = value; }
		}

		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime CollectDate
		{
			get { return m_collectDate; }
			set { m_collectDate = value; }
		}

		/// <summary>
		/// 统计时间
		/// </summary>
		public DateTime StatLastDate
		{
			get { return m_statLastDate; }
			set { m_statLastDate = value; }
		}
		#endregion
	}
}
