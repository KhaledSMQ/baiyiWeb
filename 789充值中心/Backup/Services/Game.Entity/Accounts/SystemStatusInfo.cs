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

namespace Game.Entity.Accounts
{
	/// <summary>
	/// 实体类 SystemStatusInfo。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class SystemStatusInfo  
	{
		#region 常量

		/// <summary>
		/// 表名
		/// </summary>
		public const string Tablename = "SystemStatusInfo" ;

		/// <summary>
		/// 状态名字
		/// </summary>
		public const string _StatusName = "StatusName" ;

		/// <summary>
		/// 状态数值
		/// </summary>
		public const string _StatusValue = "StatusValue" ;

		/// <summary>
		/// 状态字符
		/// </summary>
		public const string _StatusString = "StatusString" ;
		#endregion

		#region 私有变量
		private string m_statusName;			//状态名字
		private int m_statusValue;				//状态数值
		private string m_statusString;			//状态字符
		#endregion

		#region 构造方法

		/// <summary>
		/// 初始化SystemStatusInfo
		/// </summary>
		public SystemStatusInfo()
		{
			m_statusName="";
			m_statusValue=0;
			m_statusString="";
		}

		#endregion

		#region 公共属性

		/// <summary>
		/// 状态名字
		/// </summary>
		public string StatusName
		{
			get { return m_statusName; }
			set { m_statusName = value; }
		}

		/// <summary>
		/// 状态数值
		/// </summary>
		public int StatusValue
		{
			get { return m_statusValue; }
			set { m_statusValue = value; }
		}

		/// <summary>
		/// 状态字符
		/// </summary>
		public string StatusString
		{
			get { return m_statusString; }
			set { m_statusString = value; }
		}
		#endregion
	}
}
