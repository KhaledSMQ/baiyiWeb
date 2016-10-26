/*
 * 版本：4.0
 * 时间：2011-7-1
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
	/// 实体类 RecordUserExchange。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class RecordUserExchange  
	{
		#region 常量

		/// <summary>
		/// 表名
		/// </summary>
		public const string Tablename = "RecordUserExchange" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _RecordID = "RecordID" ;

		/// <summary>
		/// 用户标识
		/// </summary>
		public const string _UserID = "UserID" ;

		/// <summary>
		/// 用户帐号
		/// </summary>
		public const string _Accounts = "Accounts" ;

		/// <summary>
		/// 申请单号
		/// </summary>
		public const string _OrderID = "OrderID" ;

		/// <summary>
		/// 申请兑换
		/// </summary>
		public const string _ApplyMoney = "ApplyMoney" ;

		/// <summary>
		/// 帐号类别(1:银行,2:支付宝,3:财付通)
		/// </summary>
		public const string _AccountType = "AccountType" ;

		/// <summary>
		/// 银行帐号
		/// </summary>
		public const string _AccountNo = "AccountNo" ;

		/// <summary>
		/// 开户名
		/// </summary>
		public const string _AccountName = "AccountName" ;

		/// <summary>
		/// 银行名称
		/// </summary>
		public const string _BankName = "BankName" ;

		/// <summary>
		/// 银行地址
		/// </summary>
		public const string _BankAddress = "BankAddress" ;

		/// <summary>
		/// 申请状态(0:申请中,1:已处理,2:拒绝,3:已提现)
		/// </summary>
		public const string _ApplyStatus = "ApplyStatus" ;

		/// <summary>
		/// 申请地址
		/// </summary>
		public const string _IPAddress = "IPAddress" ;

		/// <summary>
		/// 处理日期
		/// </summary>
		public const string _DealDate = "DealDate" ;

		/// <summary>
		/// 处理备注
		/// </summary>
		public const string _DealNote = "DealNote" ;

		/// <summary>
		/// 收集日期
		/// </summary>
		public const string _CollectDate = "CollectDate" ;
		#endregion

		#region 私有变量
		private int m_recordID;				//
		private int m_userID;				//用户标识
		private string m_accounts;			//用户帐号
		private string m_orderID;			//申请单号
		private decimal m_applyMoney;		//申请兑换
		private int m_accountType;			//帐号类别(1:银行,2:支付宝,3:财付通)
		private string m_accountNo;			//银行帐号
		private string m_accountName;		//开户名
		private string m_bankName;			//银行名称
		private string m_bankAddress;		//银行地址
		private int m_applyStatus;			//申请状态(0:申请中,1:已处理,2:拒绝,3:已提现)
		private string m_iPAddress;			//申请地址
		private DateTime m_dealDate;		//处理日期
		private string m_dealNote;			//处理备注
		private DateTime m_collectDate;		//收集日期
		#endregion

		#region 构造方法

		/// <summary>
		/// 初始化RecordUserExchange
		/// </summary>
		public RecordUserExchange()
		{
			m_recordID=0;
			m_userID=0;
			m_accounts="";
			m_orderID="";
			m_applyMoney=0;
			m_accountType=0;
			m_accountNo="";
			m_accountName="";
			m_bankName="";
			m_bankAddress="";
			m_applyStatus=0;
			m_iPAddress="";
			m_dealDate=DateTime.Now;
			m_dealNote="";
			m_collectDate=DateTime.Now;
		}

		#endregion

		#region 公共属性

		/// <summary>
		/// 
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
		/// 用户帐号
		/// </summary>
		public string Accounts
		{
			get { return m_accounts; }
			set { m_accounts = value; }
		}

		/// <summary>
		/// 申请单号
		/// </summary>
		public string OrderID
		{
			get { return m_orderID; }
			set { m_orderID = value; }
		}

		/// <summary>
		/// 申请兑换
		/// </summary>
		public decimal ApplyMoney
		{
			get { return m_applyMoney; }
			set { m_applyMoney = value; }
		}

		/// <summary>
		/// 帐号类别(1:银行,2:支付宝,3:财付通)
		/// </summary>
		public int AccountType
		{
			get { return m_accountType; }
			set { m_accountType = value; }
		}

		/// <summary>
		/// 银行帐号
		/// </summary>
		public string AccountNo
		{
			get { return m_accountNo; }
			set { m_accountNo = value; }
		}

		/// <summary>
		/// 开户名
		/// </summary>
		public string AccountName
		{
			get { return m_accountName; }
			set { m_accountName = value; }
		}

		/// <summary>
		/// 银行名称
		/// </summary>
		public string BankName
		{
			get { return m_bankName; }
			set { m_bankName = value; }
		}

		/// <summary>
		/// 银行地址
		/// </summary>
		public string BankAddress
		{
			get { return m_bankAddress; }
			set { m_bankAddress = value; }
		}

		/// <summary>
		/// 申请状态(0:申请中,1:已处理,2:拒绝,3:已提现)
		/// </summary>
		public int ApplyStatus
		{
			get { return m_applyStatus; }
			set { m_applyStatus = value; }
		}

		/// <summary>
		/// 申请地址
		/// </summary>
		public string IPAddress
		{
			get { return m_iPAddress; }
			set { m_iPAddress = value; }
		}

		/// <summary>
		/// 处理日期
		/// </summary>
		public DateTime DealDate
		{
			get { return m_dealDate; }
			set { m_dealDate = value; }
		}

		/// <summary>
		/// 处理备注
		/// </summary>
		public string DealNote
		{
			get { return m_dealNote; }
			set { m_dealNote = value; }
		}

		/// <summary>
		/// 收集日期
		/// </summary>
		public DateTime CollectDate
		{
			get { return m_collectDate; }
			set { m_collectDate = value; }
		}
		#endregion
	}
}
