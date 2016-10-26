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
	/// 实体类 GameFeedbackInfo。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class GameFeedbackInfo  
	{
		#region 常量

		/// <summary>
		/// 表名
		/// </summary>
		public const string Tablename = "GameFeedbackInfo" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _FeedbackID = "FeedbackID" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _FeedbackTitle = "FeedbackTitle" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _FeedbackContent = "FeedbackContent" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _Accounts = "Accounts" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _FeedbackDate = "FeedbackDate" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _ClientIP = "ClientIP" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _ViewCount = "ViewCount" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _RevertUserID = "RevertUserID" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _RevertContent = "RevertContent" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _RevertDate = "RevertDate" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _Nullity = "Nullity" ;
		#endregion

		#region 私有变量
		private int m_feedbackID;					//
		private string m_feedbackTitle;				//
		private string m_feedbackContent;			//
		private string m_accounts;					//
		private DateTime m_feedbackDate;			//
		private string m_clientIP;					//
		private int m_viewCount;					//
		private int m_revertUserID;					//
		private string m_revertContent;				//
		private DateTime m_revertDate;				//
		private byte m_nullity;						//
		#endregion

		#region 构造方法

		/// <summary>
		/// 初始化GameFeedbackInfo
		/// </summary>
		public GameFeedbackInfo()
		{
			m_feedbackID=0;
			m_feedbackTitle="";
			m_feedbackContent="";
			m_accounts="";
			m_feedbackDate=DateTime.Now;
			m_clientIP="";
			m_viewCount=0;
			m_revertUserID=0;
			m_revertContent="";
			m_revertDate=DateTime.Now;
			m_nullity=0;
		}

		#endregion

		#region 公共属性

		/// <summary>
		/// 
		/// </summary>
		public int FeedbackID
		{
			get { return m_feedbackID; }
			set { m_feedbackID = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public string FeedbackTitle
		{
			get { return m_feedbackTitle; }
			set { m_feedbackTitle = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public string FeedbackContent
		{
			get { return m_feedbackContent; }
			set { m_feedbackContent = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public string Accounts
		{
			get { return m_accounts; }
			set { m_accounts = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public DateTime FeedbackDate
		{
			get { return m_feedbackDate; }
			set { m_feedbackDate = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public string ClientIP
		{
			get { return m_clientIP; }
			set { m_clientIP = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public int ViewCount
		{
			get { return m_viewCount; }
			set { m_viewCount = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public int RevertUserID
		{
			get { return m_revertUserID; }
			set { m_revertUserID = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public string RevertContent
		{
			get { return m_revertContent; }
			set { m_revertContent = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public DateTime RevertDate
		{
			get { return m_revertDate; }
			set { m_revertDate = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public byte Nullity
		{
			get { return m_nullity; }
			set { m_nullity = value; }
		}
		#endregion
	}
}
