using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Entity.Config
{
    /// <summary>
    /// 站点授权信息
    /// </summary>
    [Serializable]
    public class UCConfigInfo
    {
        private int m_stationID;
        private string m_accreditID;

        /// <summary>
        /// 初始化模块页面
        /// </summary>
        public UCConfigInfo()
        { }

        /// <summary>
        /// 初始化模块页面
        /// </summary>
        /// <param name="stationID"></param>
        /// <param name="accreditID"></param>
        public UCConfigInfo(int stationID, string accreditID)
        {
            m_stationID = stationID;
            m_accreditID = accreditID;
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
        /// 授权码
        /// </summary>
        public string AccreditKey
        {
            get { return m_accreditID; }
            set { m_accreditID = value; }
        }
    }
}
