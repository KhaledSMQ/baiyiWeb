/*
 * 版本：4.0
 * 时间：2011-7-8
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
	/// 实体类 GameLotteryRecord。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class GameLotteryRecord  
	{
		#region 常量

		/// <summary>
		/// 表名
		/// </summary>
		public const string Tablename = "GameLotteryRecord" ;

		/// <summary>
		/// 抽奖标识
		/// </summary>
		public const string _LotteryID = "LotteryID" ;

		/// <summary>
		/// 用户标识
		/// </summary>
		public const string _UserID = "UserID" ;

		/// <summary>
		/// 支付奖牌
		/// </summary>
		public const string _PayUserMedal = "PayUserMedal" ;

		/// <summary>
		/// 支付前奖牌
		/// </summary>
		public const string _BeforeUserMedal = "BeforeUserMedal" ;

		/// <summary>
		/// 是否中奖(0:没有,1:有)
		/// </summary>
		public const string _IsWin = "IsWin" ;

		/// <summary>
		/// 抽奖前身上积分
		/// </summary>
		public const string _BeforeScore = "BeforeScore" ;

		/// <summary>
		/// 抽奖前银行积分
		/// </summary>
		public const string _BeforeInsureScore = "BeforeInsureScore" ;

		/// <summary>
		/// 抽奖赢取积分
		/// </summary>
		public const string _WinScore = "WinScore" ;

		/// <summary>
		/// 抽奖地址
		/// </summary>
		public const string _IPAddress = "IPAddress" ;

		/// <summary>
		/// 抽奖时间
		/// </summary>
		public const string _CollectDate = "CollectDate" ;
		#endregion

		#region 私有变量
		private int m_lotteryID;					//抽奖标识
		private int m_userID;						//用户标识
		private long m_payUserMedal;				//支付奖牌
        private long m_beforeUserMedal;			//支付前奖牌
		private int m_isWin;						//是否中奖(0:没有,1:有)
        private long m_beforeScore;				//抽奖前身上积分
        private long m_beforeInsureScore;		//抽奖前银行积分
        private string m_winScore;					//抽奖赢取积分
		private string m_iPAddress;					//抽奖地址
		private DateTime m_collectDate;				//抽奖时间
		#endregion

		#region 构造方法

		/// <summary>
		/// 初始化GameLotteryRecord
		/// </summary>
		public GameLotteryRecord()
		{
			m_lotteryID=0;
			m_userID=0;
			m_payUserMedal=0;
			m_beforeUserMedal=0;
			m_isWin=0;
			m_beforeScore=0;
			m_beforeInsureScore=0;
            m_winScore = "";
			m_iPAddress="";
			m_collectDate=DateTime.Now;
		}

		#endregion

		#region 公共属性

		/// <summary>
		/// 抽奖标识
		/// </summary>
		public int LotteryID
		{
			get { return m_lotteryID; }
			set { m_lotteryID = value; }
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
		/// 支付奖牌
		/// </summary>
		public long PayUserMedal
		{
			get { return m_payUserMedal; }
			set { m_payUserMedal = value; }
		}

		/// <summary>
		/// 支付前奖牌
		/// </summary>
        public long BeforeUserMedal
		{
			get { return m_beforeUserMedal; }
			set { m_beforeUserMedal = value; }
		}

		/// <summary>
		/// 是否中奖(0:没有,1:有)
		/// </summary>
		public int IsWin
		{
			get { return m_isWin; }
			set { m_isWin = value; }
		}

		/// <summary>
		/// 抽奖前身上积分
		/// </summary>
        public long BeforeScore
		{
			get { return m_beforeScore; }
			set { m_beforeScore = value; }
		}

		/// <summary>
		/// 抽奖前银行积分
		/// </summary>
        public long BeforeInsureScore
		{
			get { return m_beforeInsureScore; }
			set { m_beforeInsureScore = value; }
		}

		/// <summary>
		/// 抽奖赢取积分
		/// </summary>
        public string WinScore
		{
			get { return m_winScore; }
			set { m_winScore = value; }
		}

		/// <summary>
		/// 抽奖地址
		/// </summary>
		public string IPAddress
		{
			get { return m_iPAddress; }
			set { m_iPAddress = value; }
		}

		/// <summary>
		/// 抽奖时间
		/// </summary>
		public DateTime CollectDate
		{
			get { return m_collectDate; }
			set { m_collectDate = value; }
		}
		#endregion
	}
}
