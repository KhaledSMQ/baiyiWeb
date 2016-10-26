/*
 * 版本：4.0
 * 时间：2011-6-2
 * 作者：http://www.foxuc.com
 *
 * 描述：实体类
 *
 */

using System;
using System.Collections.Generic;

namespace Game.Entity.GameWeb
{
	/// <summary>
	/// 实体类 GameIssueInfo。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class GameIssueInfo  
	{
		#region 常量

		/// <summary>
		/// 表名
		/// </summary>
		public const string Tablename = "GameIssueInfo" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _IssueID = "IssueID" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _IssueTitle = "IssueTitle" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _IssueContent = "IssueContent" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _Nullity = "Nullity" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _CollectDate = "CollectDate" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _ModifyDate = "ModifyDate" ;
		#endregion

		#region 私有变量
		private int m_issueID;					//
		private string m_issueTitle;			//
		private string m_issueContent;			//
		private byte m_nullity;					//
		private DateTime m_collectDate;			//
		private DateTime m_modifyDate;			//
		#endregion

		#region 构造方法

		/// <summary>
		/// 初始化GameIssueInfo
		/// </summary>
		public GameIssueInfo()
		{
			m_issueID=0;
			m_issueTitle="";
			m_issueContent="";
			m_nullity=0;
			m_collectDate=DateTime.Now;
			m_modifyDate=DateTime.Now;
		}

		#endregion

		#region 公共属性

		/// <summary>
		/// 
		/// </summary>
		public int IssueID
		{
			get { return m_issueID; }
			set { m_issueID = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public string IssueTitle
		{
			get { return m_issueTitle; }
			set { m_issueTitle = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public string IssueContent
		{
			get { return m_issueContent; }
			set { m_issueContent = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public byte Nullity
		{
			get { return m_nullity; }
			set { m_nullity = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public DateTime CollectDate
		{
			get { return m_collectDate; }
			set { m_collectDate = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public DateTime ModifyDate
		{
			get { return m_modifyDate; }
			set { m_modifyDate = value; }
		}
		#endregion
	}
}
