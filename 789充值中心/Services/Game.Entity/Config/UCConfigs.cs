using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Utils;

namespace Game.Entity.Config
{
    /// <summary>
    /// 程序配置
    /// </summary>
    public class UCConfigs
    {
        private static System.Timers.Timer ucConfigTimer = new System.Timers.Timer(15000);
        private static UCConfigInfo m_configinfo;

        /// <summary>
        /// 静态构造函数初始化相应实例和定时器
        /// </summary>
        static UCConfigs()
        {
            m_configinfo = SerializationHelper.Deserialize(typeof(UCConfigInfo), TextUtility.GetRealPath("/config/pm.config")) as UCConfigInfo;

            ucConfigTimer.AutoReset = true;
            ucConfigTimer.Enabled = true;
            ucConfigTimer.Elapsed += new System.Timers.ElapsedEventHandler(Timer_Elapsed);
            ucConfigTimer.Start();
        }

        private static void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            ResetConfig();
        }

        /// <summary>
        /// 重设配置类实例
        /// </summary>
        public static void ResetConfig()
        {
            m_configinfo = SerializationHelper.Deserialize(typeof(UCConfigInfo), TextUtility.GetRealPath("/config/pm.config")) as UCConfigInfo;
        }

        /// <summary>
        /// 获取配置信息
        /// </summary>
        /// <returns></returns>
        public static UCConfigInfo GetConfig()
        {
            return m_configinfo;
        }
    }
}
