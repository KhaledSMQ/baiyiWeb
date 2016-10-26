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
	/// 实体类 RecordUserFinance。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class RecordUserFinance  
	{
		#region 常量

		/// <summary>
		/// 表名
		/// </summary>
		public const string Tablename = "RecordUserFinance" ;

		/// <summary>
		/// 记录标识
		/// </summary>
		public const string _RecordID = "RecordID" ;

		/// <summary>
		/// 用户标识
		/// </summary>
		public const string _UserID = "UserID" ;

		/// <summary>
		/// 金额
		/// </summary>
		public const string _Amount = "Amount" ;

		/// <summary>
		/// 结算前金额
		/// </summary>
		public const string _PreAmount = "PreAmount" ;

		/// <summary>
		/// 收集时间
		/// </summary>
		public const string _CreateDate = "CreateDate" ;

		/// <summary>
		/// 创建人
		/// </summary>
		public const string _CreateBy = "CreateBy" ;

		/// <summary>
		/// 备注
		/// </summary>
		public const string _CollectNote = "CollectNote" ;
		#endregion

		#region 私有变量
		private int m_recordID;					//记录标识
		private int m_userID;					//用户标识
		private decimal m_amount;				//金额
		private decimal m_preAmount;			//结算前金额
		private DateTime m_createDate;			//收集时间
		private string m_createBy;				//创建人
		private string m_collectNote;			//备注
		#endregion

		#region 构造方法

		/// <summary>
		/// 初始化RecordUserFinance
		/// </summary>
		public RecordUserFinance()
		{
			m_recordID=0;
			m_userID=0;
			m_amount=0;
			m_preAmount=0;
			m_createDate=DateTime.Now;
			m_createBy="";
			m_collectNote="";
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
		/// 用户标识
		/// </summary>
		public int UserID
		{
			get { return m_userID; }
			set { m_userID = value; }
		}

		/// <summary>
		/// 金额
		/// </summary>
		public decimal Amount
		{
			get { return m_amount; }
			set { m_amount = value; }
		}

		/// <summary>
		/// 结算前金额
		/// </summary>
		public decimal PreAmount
		{
			get { return m_preAmount; }
			set { m_preAmount = value; }
		}

		/// <summary>
		/// 收集时间
		/// </summary>
		public DateTime CreateDate
		{
			get { return m_createDate; }
			set { m_createDate = value; }
		}

		/// <summary>
		/// 创建人
		/// </summary>
		public string CreateBy
		{
			get { return m_createBy; }
			set { m_createBy = value; }
		}

		/// <summary>
		/// 备注
		/// </summary>
		public string CollectNote
		{
			get { return m_collectNote; }
			set { m_collectNote = value; }
		}
		#endregion
	}
}
