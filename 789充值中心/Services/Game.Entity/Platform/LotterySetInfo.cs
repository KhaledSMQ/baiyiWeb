/*
 * 版本：4.0
 * 时间：2012-2-23
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
	/// 实体类 LotterySet。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class LotterySetInfo
	{
		#region 常量

		/// <summary>
		/// 表名
		/// </summary>
		public const string Tablename = "LotterySet" ;

		/// <summary>
		/// 活动是否开启(1关闭,0开启)
		/// </summary>
		public const string _IsEnable = "IsEnable" ;

		/// <summary>
		/// 每日摇奖次数
		/// </summary>
		public const string _LotteryCount = "LotteryCount" ;

		/// <summary>
		/// 每次摇奖消耗最低幸运币数
		/// </summary>
		public const string _MinMoney = "MinMoney" ;


		#endregion

		#region 私有变量
		private byte m_IsEnable;				//活动是否开启(1关闭,0开启)
		private int m_nLotteryCount;			//每次摇奖次数
		private long m_lMinMoney;			    //每次摇奖消耗最低幸运币数
		#endregion

		#region 构造方法

		/// <summary>
		/// 初始化LotterySet
		/// </summary>
        public LotterySetInfo()
		{
            m_IsEnable = 0;
            m_nLotteryCount = 0;
            m_lMinMoney = 0;
		}

		#endregion

		#region 公共属性

		/// <summary>
        /// 活动是否开启(1关闭,0开启)
		/// </summary>
        public byte IsEnable
		{
			get { return m_IsEnable; }
            set { m_IsEnable = value; }
		}

		/// <summary>
        /// 每次摇奖次数
		/// </summary>
        public int LotteryCount
		{
            get { return m_nLotteryCount; }
            set { m_nLotteryCount = value; }
		}

		/// <summary>
        /// 每次摇奖消耗最低幸运币数
		/// </summary>
        public long MinMoney
		{
            get { return m_lMinMoney; }
            set { m_lMinMoney = value; }
		}

		#endregion
	}
}
