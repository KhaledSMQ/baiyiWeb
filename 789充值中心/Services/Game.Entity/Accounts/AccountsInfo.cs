/*
 * 版本：4.0
 * 时间：2011-8-23
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
	/// 实体类 AccountsInfo。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class AccountsInfo  
	{
		#region 常量

		/// <summary>
		/// 表名
		/// </summary>
		public const string Tablename = "AccountsInfo" ;

		/// <summary>
		/// 用户标识
		/// </summary>
		public const string _UserID = "UserID" ;

		/// <summary>
		/// 上级标识
		/// </summary>
		public const string _ParentID = "ParentID" ;

		/// <summary>
		/// 站点标识
		/// </summary>
		public const string _StationID = "StationID" ;

		/// <summary>
		/// 密保标识
		/// </summary>
		public const string _ProtectID = "ProtectID" ;

		/// <summary>
		/// 用户帐号
		/// </summary>
		public const string _Accounts = "Accounts" ;

		/// <summary>
		/// 用户昵称
		/// </summary>
		public const string _NickName = "NickName" ;

		/// <summary>
		/// 注册帐号
		/// </summary>
		public const string _RegAccounts = "RegAccounts" ;

		/// <summary>
		/// 身份证号
		/// </summary>
		public const string _PassPortID = "PassPortID" ;

		/// <summary>
		/// 真实名字
		/// </summary>
		public const string _Compellation = "Compellation" ;

		/// <summary>
		/// 登录密码
		/// </summary>
		public const string _LogonPass = "LogonPass" ;

		/// <summary>
		/// 银行密码
		/// </summary>
		public const string _InsurePass = "InsurePass" ;

		/// <summary>
		/// 头像标识
		/// </summary>
		public const string _FaceID = "FaceID" ;

		/// <summary>
		/// 经验数值
		/// </summary>
		public const string _Experience = "Experience" ;

		/// <summary>
		/// 奖牌
		/// </summary>
		public const string _UserMedal = "UserMedal" ;

		/// <summary>
		/// 用户权限
		/// </summary>
		public const string _UserRight = "UserRight" ;

		/// <summary>
		/// 管理权限
		/// </summary>
		public const string _MasterRight = "MasterRight" ;

		/// <summary>
		/// 服务权限
		/// </summary>
		public const string _ServiceRight = "ServiceRight" ;

		/// <summary>
		/// 管理等级
		/// </summary>
		public const string _MasterOrder = "MasterOrder" ;

		/// <summary>
		/// 会员等级
		/// </summary>
		public const string _MemberOrder = "MemberOrder" ;

		/// <summary>
		/// 过期日期
		/// </summary>
		public const string _MemberOverDate = "MemberOverDate" ;

		/// <summary>
		/// 切换时间
		/// </summary>
		public const string _MemberSwitchDate = "MemberSwitchDate" ;

		/// <summary>
		/// 用户性别
		/// </summary>
		public const string _Gender = "Gender" ;

		/// <summary>
		/// 禁止服务
		/// </summary>
		public const string _Nullity = "Nullity" ;

		/// <summary>
		/// 关闭标志
		/// </summary>
		public const string _StunDown = "StunDown" ;

		/// <summary>
		/// 固定机器
		/// </summary>
		public const string _MoorMachine = "MoorMachine" ;

		/// <summary>
		/// 登录次数
		/// </summary>
		public const string _WebLogonTimes = "WebLogonTimes" ;

		/// <summary>
		/// 登录次数
		/// </summary>
		public const string _GameLogonTimes = "GameLogonTimes" ;

		/// <summary>
		/// 游戏时间
		/// </summary>
		public const string _PlayTimeCount = "PlayTimeCount" ;

		/// <summary>
		/// 在线时间
		/// </summary>
		public const string _OnLineTimeCount = "OnLineTimeCount" ;

		/// <summary>
		/// 登录地址
		/// </summary>
		public const string _LastLogonIP = "LastLogonIP" ;

		/// <summary>
		/// 登录时间
		/// </summary>
		public const string _LastLogonDate = "LastLogonDate" ;

		/// <summary>
		/// 登录手机
		/// </summary>
		public const string _LastLogonMobile = "LastLogonMobile" ;

		/// <summary>
		/// 登录机器
		/// </summary>
		public const string _LastLogonMachine = "LastLogonMachine" ;

		/// <summary>
		/// 注册地址
		/// </summary>
		public const string _RegisterIP = "RegisterIP" ;

		/// <summary>
		/// 注册时间
		/// </summary>
		public const string _RegisterDate = "RegisterDate" ;

		/// <summary>
		/// 注册手机
		/// </summary>
		public const string _RegisterMobile = "RegisterMobile" ;

		/// <summary>
		/// 注册机器
		/// </summary>
		public const string _RegisterMachine = "RegisterMachine" ;

		/// <summary>
		/// 是否机器人
		/// </summary>
		public const string _IsAndroid = "IsAndroid" ;

		/// <summary>
		/// 是否为推广员(0:不是,1:是)
		/// </summary>
		public const string _IsSpreader = "IsSpreader" ;

		/// <summary>
		/// 一级推广比例
		/// </summary>
		public const string _SpreaderScale = "SpreaderScale" ;

		/// <summary>
		/// 二级域名
		/// </summary>
		public const string _SLDomain = "SLDomain" ;
		#endregion

		#region 私有变量
		private int m_userID;						//用户标识
		private int m_parentID;						//上级标识
		private int m_stationID;					//站点标识
		private int m_protectID;					//密保标识
		private string m_accounts;					//用户帐号
		private string m_nickName;					//用户昵称
		private string m_regAccounts;				//注册帐号
		private string m_passPortID;				//身份证号
		private string m_compellation;				//真实名字
		private string m_logonPass;					//登录密码
		private string m_insurePass;				//银行密码
		private short m_faceID;						//头像标识
		private int m_experience;					//经验数值
		private decimal m_userMedal;				//奖牌
		private int m_userRight;					//用户权限
		private int m_masterRight;					//管理权限
		private int m_serviceRight;					//服务权限
		private byte m_masterOrder;					//管理等级
		private byte m_memberOrder;					//会员等级
		private DateTime m_memberOverDate;			//过期日期
		private DateTime m_memberSwitchDate;		//切换时间
		private byte m_gender;						//用户性别
		private byte m_nullity;						//禁止服务
		private byte m_stunDown;					//关闭标志
		private byte m_moorMachine;					//固定机器
		private int m_webLogonTimes;				//登录次数
		private int m_gameLogonTimes;				//登录次数
		private int m_playTimeCount;				//游戏时间
		private int m_onLineTimeCount;				//在线时间
		private string m_lastLogonIP;				//登录地址
		private DateTime m_lastLogonDate;			//登录时间
		private string m_lastLogonMobile;			//登录手机
		private string m_lastLogonMachine;			//登录机器
		private string m_registerIP;				//注册地址
		private DateTime m_registerDate;			//注册时间
		private string m_registerMobile;			//注册手机
		private string m_registerMachine;			//注册机器
		private byte m_isAndroid;					//是否机器人
		private byte m_isSpreader;					//是否为推广员(0:不是,1:是)
		private decimal m_spreaderScale;			//一级推广比例
		private string m_sLDomain;					//二级域名
		#endregion

		#region 构造方法

		/// <summary>
		/// 初始化AccountsInfo
		/// </summary>
		public AccountsInfo()
		{
			m_userID=0;
			m_parentID=0;
			m_stationID=0;
			m_protectID=0;
			m_accounts="";
			m_nickName="";
			m_regAccounts="";
			m_passPortID="";
			m_compellation="";
			m_logonPass="";
			m_insurePass="";
			m_faceID=0;
			m_experience=0;
			m_userMedal=0;
			m_userRight=0;
			m_masterRight=0;
			m_serviceRight=0;
			m_masterOrder=0;
			m_memberOrder=0;
			m_memberOverDate=DateTime.Now;
			m_memberSwitchDate=DateTime.Now;
			m_gender=0;
			m_nullity=0;
			m_stunDown=0;
			m_moorMachine=0;
			m_webLogonTimes=0;
			m_gameLogonTimes=0;
			m_playTimeCount=0;
			m_onLineTimeCount=0;
			m_lastLogonIP="";
			m_lastLogonDate=DateTime.Now;
			m_lastLogonMobile="";
			m_lastLogonMachine="";
			m_registerIP="";
			m_registerDate=DateTime.Now;
			m_registerMobile="";
			m_registerMachine="";
			m_isAndroid=0;
			m_isSpreader=0;
			m_spreaderScale=0;
			m_sLDomain="";
		}

		#endregion

		#region 公共属性

		/// <summary>
		/// 用户标识
		/// </summary>
		public int UserID
		{
			get { return m_userID; }
			set { m_userID = value; }
		}

		/// <summary>
		/// 上级标识
		/// </summary>
		public int ParentID
		{
			get { return m_parentID; }
			set { m_parentID = value; }
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
		/// 密保标识
		/// </summary>
		public int ProtectID
		{
			get { return m_protectID; }
			set { m_protectID = value; }
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
		/// 用户昵称
		/// </summary>
		public string NickName
		{
			get { return m_nickName; }
			set { m_nickName = value; }
		}

		/// <summary>
		/// 注册帐号
		/// </summary>
		public string RegAccounts
		{
			get { return m_regAccounts; }
			set { m_regAccounts = value; }
		}

		/// <summary>
		/// 身份证号
		/// </summary>
		public string PassPortID
		{
			get { return m_passPortID; }
			set { m_passPortID = value; }
		}

		/// <summary>
		/// 真实名字
		/// </summary>
		public string Compellation
		{
			get { return m_compellation; }
			set { m_compellation = value; }
		}

		/// <summary>
		/// 登录密码
		/// </summary>
		public string LogonPass
		{
			get { return m_logonPass; }
			set { m_logonPass = value; }
		}

		/// <summary>
		/// 银行密码
		/// </summary>
		public string InsurePass
		{
			get { return m_insurePass; }
			set { m_insurePass = value; }
		}

		/// <summary>
		/// 头像标识
		/// </summary>
		public short FaceID
		{
			get { return m_faceID; }
			set { m_faceID = value; }
		}

		/// <summary>
		/// 经验数值
		/// </summary>
		public int Experience
		{
			get { return m_experience; }
			set { m_experience = value; }
		}

		/// <summary>
		/// 奖牌
		/// </summary>
		public decimal UserMedal
		{
			get { return m_userMedal; }
			set { m_userMedal = value; }
		}

		/// <summary>
		/// 用户权限
		/// </summary>
		public int UserRight
		{
			get { return m_userRight; }
			set { m_userRight = value; }
		}

		/// <summary>
		/// 管理权限
		/// </summary>
		public int MasterRight
		{
			get { return m_masterRight; }
			set { m_masterRight = value; }
		}

		/// <summary>
		/// 服务权限
		/// </summary>
		public int ServiceRight
		{
			get { return m_serviceRight; }
			set { m_serviceRight = value; }
		}

		/// <summary>
		/// 管理等级
		/// </summary>
		public byte MasterOrder
		{
			get { return m_masterOrder; }
			set { m_masterOrder = value; }
		}

		/// <summary>
		/// 会员等级
		/// </summary>
		public byte MemberOrder
		{
			get { return m_memberOrder; }
			set { m_memberOrder = value; }
		}

		/// <summary>
		/// 过期日期
		/// </summary>
		public DateTime MemberOverDate
		{
			get { return m_memberOverDate; }
			set { m_memberOverDate = value; }
		}

		/// <summary>
		/// 切换时间
		/// </summary>
		public DateTime MemberSwitchDate
		{
			get { return m_memberSwitchDate; }
			set { m_memberSwitchDate = value; }
		}

		/// <summary>
		/// 用户性别
		/// </summary>
		public byte Gender
		{
			get { return m_gender; }
			set { m_gender = value; }
		}

		/// <summary>
		/// 禁止服务
		/// </summary>
		public byte Nullity
		{
			get { return m_nullity; }
			set { m_nullity = value; }
		}

		/// <summary>
		/// 关闭标志
		/// </summary>
		public byte StunDown
		{
			get { return m_stunDown; }
			set { m_stunDown = value; }
		}

		/// <summary>
		/// 固定机器
		/// </summary>
		public byte MoorMachine
		{
			get { return m_moorMachine; }
			set { m_moorMachine = value; }
		}

		/// <summary>
		/// 登录次数
		/// </summary>
		public int WebLogonTimes
		{
			get { return m_webLogonTimes; }
			set { m_webLogonTimes = value; }
		}

		/// <summary>
		/// 登录次数
		/// </summary>
		public int GameLogonTimes
		{
			get { return m_gameLogonTimes; }
			set { m_gameLogonTimes = value; }
		}

		/// <summary>
		/// 游戏时间
		/// </summary>
		public int PlayTimeCount
		{
			get { return m_playTimeCount; }
			set { m_playTimeCount = value; }
		}

		/// <summary>
		/// 在线时间
		/// </summary>
		public int OnLineTimeCount
		{
			get { return m_onLineTimeCount; }
			set { m_onLineTimeCount = value; }
		}

		/// <summary>
		/// 登录地址
		/// </summary>
		public string LastLogonIP
		{
			get { return m_lastLogonIP; }
			set { m_lastLogonIP = value; }
		}

		/// <summary>
		/// 登录时间
		/// </summary>
		public DateTime LastLogonDate
		{
			get { return m_lastLogonDate; }
			set { m_lastLogonDate = value; }
		}

		/// <summary>
		/// 登录手机
		/// </summary>
		public string LastLogonMobile
		{
			get { return m_lastLogonMobile; }
			set { m_lastLogonMobile = value; }
		}

		/// <summary>
		/// 登录机器
		/// </summary>
		public string LastLogonMachine
		{
			get { return m_lastLogonMachine; }
			set { m_lastLogonMachine = value; }
		}

		/// <summary>
		/// 注册地址
		/// </summary>
		public string RegisterIP
		{
			get { return m_registerIP; }
			set { m_registerIP = value; }
		}

		/// <summary>
		/// 注册时间
		/// </summary>
		public DateTime RegisterDate
		{
			get { return m_registerDate; }
			set { m_registerDate = value; }
		}

		/// <summary>
		/// 注册手机
		/// </summary>
		public string RegisterMobile
		{
			get { return m_registerMobile; }
			set { m_registerMobile = value; }
		}

		/// <summary>
		/// 注册机器
		/// </summary>
		public string RegisterMachine
		{
			get { return m_registerMachine; }
			set { m_registerMachine = value; }
		}

		/// <summary>
		/// 是否机器人
		/// </summary>
		public byte IsAndroid
		{
			get { return m_isAndroid; }
			set { m_isAndroid = value; }
		}

		/// <summary>
		/// 是否为推广员(0:不是,1:是)
		/// </summary>
		public byte IsSpreader
		{
			get { return m_isSpreader; }
			set { m_isSpreader = value; }
		}

		/// <summary>
		/// 一级推广比例
		/// </summary>
		public decimal SpreaderScale
		{
			get { return m_spreaderScale; }
			set { m_spreaderScale = value; }
		}

		/// <summary>
		/// 二级域名
		/// </summary>
		public string SLDomain
		{
			get { return m_sLDomain; }
			set { m_sLDomain = value; }
		}
		#endregion
	}
}
