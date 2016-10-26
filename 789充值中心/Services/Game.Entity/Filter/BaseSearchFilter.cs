using System;
using System.Text;

using Game.Entity.Accounts;

namespace Game.Entity.Filter
{
    /// <summary>
    /// 基类过滤条件
    /// </summary>
    [Serializable]
    public class BaseSearchFilter
    {
        #region 构造方法

        /// <summary>
        /// 
        /// </summary>
        public BaseSearchFilter()
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userTicket"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        public BaseSearchFilter(UserTicketInfo userTicket, int pageIndex, int pageSize)
            : this(userTicket.UserID, pageIndex,pageSize)
        { 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        public BaseSearchFilter(int userID, int pageIndex, int pageSize)
        {
            m_userID = userID;
            m_pageIndex = pageIndex;
            m_pageSize = pageSize;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="rowsCount"></param>
        public BaseSearchFilter(int userID, int pageIndex, int pageSize, int rowsCount)
            :this(userID,pageIndex,pageSize)
        {
            m_rowsCount = rowsCount;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="stationID"></param>
        /// <param name="currencyID"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="rowsCount"></param>
        public BaseSearchFilter(int userID, int stationID, int currencyID, int pageIndex, int pageSize, int rowsCount)
            : this(userID, pageIndex, pageSize, rowsCount)
        {
            m_stationID = stationID;
            m_currencyID = currencyID;
        }

        #endregion

        #region 公开属性

        private int m_userID;

        /// <summary>
        /// 用户标识
        /// </summary>
        public int UserID
        {
            get { return m_userID; }
            set { m_userID = value; }
        }

        private int m_stationID;
        /// <summary>
        /// 站点标识
        /// </summary>
        public int StationID
        {
            get { return m_stationID; }
            set { m_stationID = value; }
        }
        private int m_currencyID;
        /// <summary>
        /// 货币标识
        /// </summary>
        public int CurrencyID
        {
            get { return m_currencyID; }
            set { m_currencyID = value; }
        }

        private int m_pageIndex;

        /// <summary>
        /// 页面索引
        /// </summary>
        public int PageIndex
        {
            get { return m_pageIndex; }
            set { m_pageIndex = value; }
        }

        private int m_pageSize;
        /// <summary>
        /// 页码大小
        /// </summary>
        public int PageSize
        {
            get { return m_pageSize; }
            set { m_pageSize = value; }
        }

        private int m_rowsCount;
        /// <summary>
        /// 总记录数
        /// </summary>
        public int RowsCount
        {
            get { return m_rowsCount; }
            set { m_rowsCount = value; }
        }

        #endregion
    }
}
