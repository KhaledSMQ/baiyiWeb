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

namespace Game.Entity.GameWeb
{
	/// <summary>
	/// 实体类 Notice。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Notice  
	{
		#region 常量

		/// <summary>
		/// 表名
		/// </summary>
		public const string Tablename = "Notice" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _NoticeID = "NoticeID" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _Subject = "Subject" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _IsLink = "IsLink" ;

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
		public const string _Location = "Location" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _Width = "Width" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _Height = "Height" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _StartDate = "StartDate" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _OverDate = "OverDate" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _Nullity = "Nullity" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _CollectDate = "CollectDate" ;
		#endregion

		#region 私有变量
		private int m_noticeID;					//
		private string m_subject;				//
		private byte m_isLink;					//
		private string m_linkUrl;				//
		private string m_body;					//
		private string m_location;				//
		private int m_width;					//
		private int m_height;					//
		private DateTime m_startDate;			//
		private DateTime m_overDate;			//
		private byte m_nullity;					//
		private DateTime m_collectDate;			//
		#endregion

		#region 构造方法

		/// <summary>
		/// 初始化Notice
		/// </summary>
		public Notice()
		{
			m_noticeID=0;
			m_subject="";
			m_isLink=0;
			m_linkUrl="";
			m_body="";
			m_location="";
			m_width=0;
			m_height=0;
			m_startDate=DateTime.Now;
			m_overDate=DateTime.Now;
			m_nullity=0;
			m_collectDate=DateTime.Now;
		}

		#endregion

		#region 公共属性

		/// <summary>
		/// 
		/// </summary>
		public int NoticeID
		{
			get { return m_noticeID; }
			set { m_noticeID = value; }
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
		public byte IsLink
		{
			get { return m_isLink; }
			set { m_isLink = value; }
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
		public string Location
		{
			get { return m_location; }
			set { m_location = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public int Width
		{
			get { return m_width; }
			set { m_width = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public int Height
		{
			get { return m_height; }
			set { m_height = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public DateTime StartDate
		{
			get { return m_startDate; }
			set { m_startDate = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public DateTime OverDate
		{
			get { return m_overDate; }
			set { m_overDate = value; }
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
		#endregion
	}
}
