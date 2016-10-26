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
	/// 实体类 StreamScoreInfo。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class StreamScoreInfo  
	{
		#region 常量

		/// <summary>
		/// 表名
		/// </summary>
		public const string Tablename = "StreamScoreInfo" ;

		/// <summary>
		/// 日期标识
		/// </summary>
		public const string _DateID = "DateID" ;

		/// <summary>
		/// 用户标识
		/// </summary>
		public const string _UserID = "UserID" ;

		/// <summary>
		/// 站点标识
		/// </summary>
		public const string _StationID = "StationID" ;

		/// <summary>
		/// 赢积分和
		/// </summary>
		public const string _WinScore = "WinScore" ;

		/// <summary>
		/// 输积分和
		/// </summary>
		public const string _LostScore = "LostScore" ;

		/// <summary>
		/// 税收
		/// </summary>
		public const string _Revenue = "Revenue" ;

		/// <summary>
		/// 游戏时长
		/// </summary>
		public const string _PlayTimeCount = "PlayTimeCount" ;

		/// <summary>
		/// 开始统计时间
		/// </summary>
		public const string _FirstCollectDate = "FirstCollectDate" ;

		/// <summary>
		/// 最后统计时间
		/// </summary>
		public const string _LastCollectDate = "LastCollectDate" ;
		#endregion

		#region 私有变量
		private int m_dateID;						//日期标识
		private int m_userID;						//用户标识
		private int m_stationID;					//站点标识
		private decimal m_winScore;					//赢积分和
		private decimal m_lostScore;				//输积分和
		private decimal m_revenue;					//税收
		private int m_playTimeCount;				//游戏时长
		private DateTime m_firstCollectDate;		//开始统计时间
		private DateTime m_lastCollectDate;			//最后统计时间
		#endregion

		#region 构造方法

		/// <summary>
		/// 初始化StreamScoreInfo
		/// </summary>
		public StreamScoreInfo()
		{
			m_dateID=0;
			m_userID=0;
			m_stationID=0;
			m_winScore=0;
			m_lostScore=0;
			m_revenue=0;
			m_playTimeCount=0;
			m_firstCollectDate=DateTime.Now;
			m_lastCollectDate=DateTime.Now;
		}

		#endregion

		#region 公共属性

		/// <summary>
		/// 日期标识
		/// </summary>
		public int DateID
		{
			get { return m_dateID; }
			set { m_dateID = value; }
		}

		/// <summary>
		/// 用户标识
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
		/// 赢积分和
		/// </summary>
		public decimal WinScore
		{
			get { return m_winScore; }
			set { m_winScore = value; }
		}

		/// <summary>
		/// 输积分和
		/// </summary>
		public decimal LostScore
		{
			get { return m_lostScore; }
			set { m_lostScore = value; }
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
		/// 游戏时长
		/// </summary>
		public int PlayTimeCount
		{
			get { return m_playTimeCount; }
			set { m_playTimeCount = value; }
		}

		/// <summary>
		/// 开始统计时间
		/// </summary>
		public DateTime FirstCollectDate
		{
			get { return m_firstCollectDate; }
			set { m_firstCollectDate = value; }
		}

		/// <summary>
		/// 最后统计时间
		/// </summary>
		public DateTime LastCollectDate
		{
			get { return m_lastCollectDate; }
			set { m_lastCollectDate = value; }
		}
		#endregion
	}
}
