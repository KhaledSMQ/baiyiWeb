using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Entity.Filter
{
    /// <summary>
    /// 银行过滤条件
    /// </summary>
    [Serializable]
    public class InsureSearchFilter:BaseSearchFilter
    {
        #region 构造方法

        /// <summary>
        /// 
        /// </summary>
        public InsureSearchFilter()
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="tradeType"></param>
        /// <param name="operateDateScope"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        public InsureSearchFilter(int userID, InsureTradeType tradeType, DateTime[] operateDateScope, int pageIndex, int pageSize)
            :base(userID,pageIndex,pageSize)
        {
            m_tradeType = tradeType;
            m_operateDateScope = operateDateScope;
        }

        #endregion

        #region 公开属性
        private InsureTradeType m_tradeType;

        /// <summary>
        /// 交易类别
        /// </summary>
        public InsureTradeType TradeType
        {
            get { return m_tradeType; }
            set { m_tradeType = value; }
        }

        private DateTime[] m_operateDateScope;

        /// <summary>
        /// 时间跨度
        /// </summary>
        public DateTime[] OperateDateScope
        {
            get { return m_operateDateScope; }
            set { m_operateDateScope = value; }
        }
        #endregion
    }
}
