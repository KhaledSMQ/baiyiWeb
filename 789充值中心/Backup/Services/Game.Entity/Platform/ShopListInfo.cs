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
	/// 实体类 GameTypeItem。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class ShopListInfo  
	{
		#region 常量

		/// <summary>
		/// 表名
		/// </summary>
		public const string Tablename = "ShopInfo" ;

		/// <summary>
		/// 类型标识
		/// </summary>
		public const string _ShopID = "ShopID" ;

		/// <summary>
		/// 商品名称
		/// </summary>
		public const string _ShopName = "ShopName" ;

		/// <summary>
		/// 商品价格
		/// </summary>
		public const string _ShopPrice = "ShopPrice" ;

		/// <summary>
		/// 商品个数
		/// </summary>
		public const string _ShopNum = "ShopNum" ;

        /// <summary>
		/// 商品介绍
		/// </summary>
		public const string _ShopIntro = "ShopIntro" ;

        /// <summary>
		/// 商品介绍图片
		/// </summary>
		public const string _ShopIntroImg = "ShopIntroImg" ;

        /// <summary>
		/// 商品介绍图片
		/// </summary>
		public const string _ShopIndexImg = "ShopIndexImg" ;
        
		/// <summary>
		/// 无效标志
		/// </summary>
		public const string _Nullity = "Nullity" ;


		#endregion

		#region 私有变量
		private int m_ShopID;				//类型标识
		private string m_ShopName;				//商品名称
		private int m_ShopPrice;			//成品价格
		private int m_ShopNum;			//商品个数
		private string m_ShopIntro;			//商品介绍
		private string m_ShopIntroImg;		//商品介绍图片
		private string m_ShopIndexImg;		//商品介绍图片
		private byte m_Nullity;				//无效标志
		#endregion

		#region 构造方法

		/// <summary>
		/// 初始化GameTypeItem
		/// </summary>
        public ShopListInfo()
		{
            m_ShopID = 0;
            m_ShopName = "";
            m_ShopPrice = 0;
            m_ShopNum = 0;
            m_ShopIntro = "";
            m_ShopIntroImg = "";
            m_ShopIndexImg = "";
            
            m_Nullity = 0;
		}

		#endregion

		#region 公共属性

		/// <summary>
		/// 类型标识
		/// </summary>
        public int ShopID
		{
			get { return m_ShopID; }
			set { m_ShopID = value; }
		}

		/// <summary>
		/// 商品名称
		/// </summary>
        public string ShopName
		{
            get { return m_ShopName; }
            set { m_ShopName = value; }
		}

		/// <summary>
		/// 商品价格
		/// </summary>
        public int ShopPrice
		{
            get { return m_ShopPrice; }
            set { m_ShopPrice = value; }
		}

		/// <summary>
		/// 商品个数
		/// </summary>
        public int ShopNum
		{
            get { return m_ShopNum; }
            set { m_ShopNum = value; }
		}

        /// <summary>
        /// 商品介绍
        /// </summary>
        public string ShopIntro
        {
            get { return m_ShopIntro; }
            set { m_ShopIntro = value; }
        }

        /// <summary>
        /// 商品图片
        /// </summary>
        public string ShopIntroImg
        {
            get { return m_ShopIntroImg; }
            set { m_ShopIntroImg = value; }
        }

        /// <summary>
        /// 商品图片
        /// </summary>
        public string ShopIndexImg
        {
            get { return m_ShopIndexImg; }
            set { m_ShopIndexImg = value; }
        }

		/// <summary>
		/// 无效标志
		/// </summary>
		public byte Nullity
		{
			get { return m_Nullity; }
			set { m_Nullity = value; }
		}
		#endregion
	}
}
