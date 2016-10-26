using System;
using System.Collections.Generic;
using System.Text;

using Game.Entity.Accounts;

namespace Game.Entity.Filter
{
    /// <summary>
    /// 进出记录过滤
    /// </summary>
    public class InoutSearchFilter : BaseSearchFilter
    {
        #region 构造方法

        /// <summary>
        /// 
        /// </summary>
        public InoutSearchFilter()
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userTicket"></param>
        /// <param name="kindID"></param>
        /// <param name="lastDays"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        public InoutSearchFilter(UserTicketInfo userTicket, int kindID, int lastDays, int pageIndex, int pageSize)
        { }
                
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="stationID"></param>
        /// <param name="currencyID"></param>
        /// <param name="kindID"></param>
        /// <param name="lastDays"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="rowsCount"></param>
        public InoutSearchFilter(int userID, int kindID, int lastDays, int pageIndex, int pageSize, int rowsCount)
        {
            m_kindID = kindID;
            m_lastDays = lastDays;
        }

        #endregion

        #region 公开属性        

        private int m_kindID;
        /// <summary>
        /// 游戏标识
        /// </summary>
        public int KindID
        {
            get { return m_kindID; }
            set { m_kindID = value; }
        }

        private int m_lastDays;
        /// <summary>
        /// 最近N天
        /// </summary>
        public int LastDays
        {
            get { return m_lastDays; }
            set { m_lastDays = value; }
        }

        #endregion
    }
}
