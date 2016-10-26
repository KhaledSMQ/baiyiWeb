using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Utils;
using Game.Kernel;
using Game.IData;

namespace Game.Data.Factory
{
    /// <summary>
    /// 数据服务创建工厂
    /// </summary>
    public class ClassFactory
    {
        /// <summary>
        /// 创建帐号库对象实例
        /// </summary>
        /// <returns></returns>
        public static IAccountsDataProvider GetIAccountsDataProvider()
        {
            return ProxyFactory.CreateInstance<AccountsDataProvider>(ApplicationSettings.Get("DBAccounts"));
        }

        /// <summary>
        /// 创建金币库对象实例
        /// </summary>
        /// <returns></returns>
        public static ITreasureDataProvider GetITreasureDataProvider()
        {
            return ProxyFactory.CreateInstance<TreasureDataProvider>(ApplicationSettings.Get("DBTreasure"));
        }

        /// <summary>
        /// 创建金币库对象实例
        /// </summary>
        /// <returns></returns>
        public static INativeWebDataProvider GetINativeWebDataProvider()
        {
            return ProxyFactory.CreateInstance<NativeWebDataProvider>(ApplicationSettings.Get("DBNativeWeb"));
        }

        /// <summary>
        /// 创建本地库对象实例
        /// </summary>
        /// <returns></returns>
        public static IPlatformDataProvider GetIPlatformDataProvider()
        {
            return ProxyFactory.CreateInstance<PlatformDataProvider>(ApplicationSettings.Get("DBPlatform"));
        }

        /// <summary>
        /// 创建本地库对象实例
        /// </summary>
        /// <returns></returns>
        public static IRecordDataProvider GetIRecordDataProvider()
        {
            return ProxyFactory.CreateInstance<RecordDataProvider>(ApplicationSettings.Get("DBRecord"));
        }
    }
}
