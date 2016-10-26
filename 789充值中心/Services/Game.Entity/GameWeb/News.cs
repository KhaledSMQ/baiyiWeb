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
	/// 实体类 News。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class News  
	{
		#region 常量

		/// <summary>
		/// 表名
		/// </summary>
		public const string Tablename = "News" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _NewsID = "NewsID" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _PopID = "PopID" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _Subject = "Subject" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _Subject1 = "Subject1" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _OnTop = "OnTop" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _OnTopAll = "OnTopAll" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _IsElite = "IsElite" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _IsHot = "IsHot" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _IsLock = "IsLock" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _IsDelete = "IsDelete" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _IsLinks = "IsLinks" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _LinkUrl = "LinkUrl" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _Body = "Body" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _FormattedBody = "FormattedBody" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _HighLight = "HighLight" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _ClassID = "ClassID" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _UserID = "UserID" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _IssueIP = "IssueIP" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _LastModifyIP = "LastModifyIP" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _IssueDate = "IssueDate" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _LastModifyDate = "LastModifyDate" ;
		#endregion

		#region 私有变量
		private int m_newsID;						//
		private int m_popID;						//
		private string m_subject;					//
		private string m_subject1;					//
		private byte m_onTop;						//
		private byte m_onTopAll;					//
		private byte m_isElite;						//
		private byte m_isHot;						//
		private byte m_isLock;						//
		private byte m_isDelete;					//
		private byte m_isLinks;						//
		private string m_linkUrl;					//
		private string m_body;						//
		private string m_formattedBody;				//
		private string m_highLight;					//
		private byte m_classID;						//
		private int m_userID;						//
		private string m_issueIP;					//
		private string m_lastModifyIP;				//
		private DateTime m_issueDate;				//
		private DateTime m_lastModifyDate;			//
		#endregion

		#region 构造方法

		/// <summary>
		/// 初始化News
		/// </summary>
		public News()
		{
			m_newsID=0;
			m_popID=0;
			m_subject="";
			m_subject1="";
			m_onTop=0;
			m_onTopAll=0;
			m_isElite=0;
			m_isHot=0;
			m_isLock=0;
			m_isDelete=0;
			m_isLinks=0;
			m_linkUrl="";
			m_body="";
			m_formattedBody="";
			m_highLight="";
			m_classID=0;
			m_userID=0;
			m_issueIP="";
			m_lastModifyIP="";
			m_issueDate=DateTime.Now;
			m_lastModifyDate=DateTime.Now;
		}

		#endregion

		#region 公共属性

		/// <summary>
		/// 
		/// </summary>
		public int NewsID
		{
			get { return m_newsID; }
			set { m_newsID = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public int PopID
		{
			get { return m_popID; }
			set { m_popID = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public string Subject
		{
			get { return m_subject; }
			set { m_subject = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public string Subject1
		{
			get { return m_subject1; }
			set { m_subject1 = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public byte OnTop
		{
			get { return m_onTop; }
			set { m_onTop = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public byte OnTopAll
		{
			get { return m_onTopAll; }
			set { m_onTopAll = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public byte IsElite
		{
			get { return m_isElite; }
			set { m_isElite = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public byte IsHot
		{
			get { return m_isHot; }
			set { m_isHot = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public byte IsLock
		{
			get { return m_isLock; }
			set { m_isLock = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public byte IsDelete
		{
			get { return m_isDelete; }
			set { m_isDelete = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public byte IsLinks
		{
			get { return m_isLinks; }
			set { m_isLinks = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public string LinkUrl
		{
			get { return m_linkUrl; }
			set { m_linkUrl = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public string Body
		{
			get { return m_body; }
			set { m_body = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public string FormattedBody
		{
			get { return m_formattedBody; }
			set { m_formattedBody = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public string HighLight
		{
			get { return m_highLight; }
			set { m_highLight = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public byte ClassID
		{
			get { return m_classID; }
			set { m_classID = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public int UserID
		{
			get { return m_userID; }
			set { m_userID = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public string IssueIP
		{
			get { return m_issueIP; }
			set { m_issueIP = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public string LastModifyIP
		{
			get { return m_lastModifyIP; }
			set { m_lastModifyIP = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public DateTime IssueDate
		{
			get { return m_issueDate; }
			set { m_issueDate = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public DateTime LastModifyDate
		{
			get { return m_lastModifyDate; }
			set { m_lastModifyDate = value; }
		}
		#endregion
	}
}
