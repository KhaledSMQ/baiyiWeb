/*
 * 版本：4.0
 * 时间：2011-5-11
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
	/// 实体类 GameStationInfo。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class GameStationInfo  
	{
		#region 常量

		/// <summary>
		/// 表名
		/// </summary>
		public const string Tablename = "GameStationInfo" ;

		/// <summary>
		/// 索引标识
		/// </summary>
		public const string _ID = "ID" ;

		/// <summary>
		/// 站点标识
		/// </summary>
		public const string _StationID = "StationID" ;

		/// <summary>
		/// 授权号码
		/// </summary>
		public const string _AccreditID = "AccreditID" ;

		/// <summary>
		/// 站点名字
		/// </summary>
		public const string _StationName = "StationName" ;

		/// <summary>
		/// 网站别名
		/// </summary>
		public const string _StationAlias = "StationAlias" ;

		/// <summary>
		/// 站点域名
		/// </summary>
		public const string _StationDomain = "StationDomain" ;

		/// <summary>
		/// 联系人
		/// </summary>
		public const string _ContactUser = "ContactUser" ;

		/// <summary>
		/// 联系邮箱
		/// </summary>
		public const string _ContactMail = "ContactMail" ;

		/// <summary>
		/// 联系电话
		/// </summary>
		public const string _ContactPhone = "ContactPhone" ;

		/// <summary>
		/// 移动电话
		/// </summary>
		public const string _ContactMobile = "ContactMobile" ;

		/// <summary>
		/// 联系地址
		/// </summary>
		public const string _ContactAddress = "ContactAddress" ;

		/// <summary>
		/// 禁止服务
		/// </summary>
		public const string _Nullity = "Nullity" ;

		/// <summary>
		/// 创建时间
		/// </summary>
		public const string _CreateDateTime = "CreateDateTime" ;
		#endregion

		#region 私有变量
		private int m_iD;						//索引标识
		private int m_stationID;				//站点标识
		private string m_accreditID;			//授权号码
		private string m_stationName;			//站点名字
		private string m_stationAlias;			//网站别名
		private string m_stationDomain;			//站点域名
		private string m_contactUser;			//联系人
		private string m_contactMail;			//联系邮箱
		private string m_contactPhone;			//联系电话
		private string m_contactMobile;			//移动电话
		private string m_contactAddress;		//联系地址
		private byte m_nullity;					//禁止服务
		private DateTime m_createDateTime;		//创建时间
		#endregion

		#region 构造方法

		/// <summary>
		/// 初始化GameStationInfo
		/// </summary>
		public GameStationInfo()
		{
			m_iD=0;
			m_stationID=0;
			m_accreditID="";
			m_stationName="";
			m_stationAlias="";
			m_stationDomain="";
			m_contactUser="";
			m_contactMail="";
			m_contactPhone="";
			m_contactMobile="";
			m_contactAddress="";
			m_nullity=0;
			m_createDateTime=DateTime.Now;
		}

		#endregion

		#region 公共属性

		/// <summary>
		/// 索引标识
		/// </summary>
		public int ID
		{
			get { return m_iD; }
			set { m_iD = value; }
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
		/// 授权号码
		/// </summary>
		public string AccreditID
		{
			get { return m_accreditID; }
			set { m_accreditID = value; }
		}

		/// <summary>
		/// 站点名字
		/// </summary>
		public string StationName
		{
			get { return m_stationName; }
			set { m_stationName = value; }
		}

		/// <summary>
		/// 网站别名
		/// </summary>
		public string StationAlias
		{
			get { return m_stationAlias; }
			set { m_stationAlias = value; }
		}

		/// <summary>
		/// 站点域名
		/// </summary>
		public string StationDomain
		{
			get { return m_stationDomain; }
			set { m_stationDomain = value; }
		}

		/// <summary>
		/// 联系人
		/// </summary>
		public string ContactUser
		{
			get { return m_contactUser; }
			set { m_contactUser = value; }
		}

		/// <summary>
		/// 联系邮箱
		/// </summary>
		public string ContactMail
		{
			get { return m_contactMail; }
			set { m_contactMail = value; }
		}

		/// <summary>
		/// 联系电话
		/// </summary>
		public string ContactPhone
		{
			get { return m_contactPhone; }
			set { m_contactPhone = value; }
		}

		/// <summary>
		/// 移动电话
		/// </summary>
		public string ContactMobile
		{
			get { return m_contactMobile; }
			set { m_contactMobile = value; }
		}

		/// <summary>
		/// 联系地址
		/// </summary>
		public string ContactAddress
		{
			get { return m_contactAddress; }
			set { m_contactAddress = value; }
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
		/// 创建时间
		/// </summary>
		public DateTime CreateDateTime
		{
			get { return m_createDateTime; }
			set { m_createDateTime = value; }
		}
		#endregion
	}
}
