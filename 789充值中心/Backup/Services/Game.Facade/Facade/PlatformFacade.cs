using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Game.Data.Factory;
using Game.IData;
using Game.Entity;
using Game.Entity.Platform;
using Game.Kernel;
using Game.Utils;

namespace Game.Facade
{
    /// <summary>
    /// 本地数据
    /// </summary>
    public class PlatformFacade
    {
        #region Fields

        private IPlatformDataProvider platformData;                                       //网站信息

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public PlatformFacade()
        {
            platformData = ClassFactory.GetIPlatformDataProvider();
        }
        #endregion

        #region 授权验证

        /// <summary>
        /// 验证授权
        /// </summary>
        /// <param name="stationID"></param>
        /// <param name="accreditKey"></param>
        /// <param name="ip"></param>
        /// <returns></returns>
        public Message ValidAccredit(int stationID, string accreditKey, string ip)
        {
            return platformData.ValidAccredit(stationID, accreditKey, ip);
        }
        #endregion

        /// <summary>
        /// 根据游戏ID获得游戏名称
        /// </summary>
        /// <param name="kindID"></param>
        /// <returns></returns>
        public string GetGameKindNameByID(int kindID)
        {
            return platformData.GetGameKindNameByID(kindID);
        }

        /// <summary>
        /// 根据房间ID获取房间名称
        /// </summary>
        /// <param name="serverID"></param>
        /// <returns></returns>
        public string GetGameRoomName(int serverID)
        {
            return platformData.GetGameRoomName(serverID);
        }

        /// <summary>
        /// 根据ID 得到想在对应的配置信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PublicConfig GetPublicConfig(string key)
        {
            return platformData.GetPublicConfig(key);
        }

        /// <summary>
        /// 得到游戏类型列表
        /// </summary>
        /// <returns></returns>
        public IList<GameTypeItem> GetGameTypes()
        {
            return platformData.GetGameTypes();
        }

        /// <summary>
        /// 根据类型ID
        /// </summary>
        /// <returns></returns>
        public IList<GameKindItem> GetKindIDByTypeID(int typeID)
        {
            return platformData.GetKindIDByTypeID(typeID);
        }

        /// <summary>
        /// 得到所有的游戏列表
        /// </summary>
        /// <returns></returns>
        public IList<GameKindItem> GetAllKinds()
        {
            return platformData.GetAllKinds();
        }

        /// <summary>
        /// 获取游戏实体
        /// </summary>
        /// <param name="kindID"></param>
        /// <returns></returns>
        public GameKindItem GetKindByKindID(int kindID)
        {
            return platformData.GetKindByKindID(kindID);
        }

        /// <summary>
        /// 根据游戏ID获得商品名称
        /// </summary>
        /// <param name="ShopID"></param>
        /// <returns></returns>
        public string GetShopNameByID(int ShopID)
        {
            return platformData.GetShopNameByID(ShopID);
        }

        /// <summary>
        /// 得到所有的商品列表
        /// </summary>
        /// <returns></returns>
        public IList<ShopListInfo> GetAllShops()
        {
            return platformData.GetAllShops();
        }

        /// <summary>
        /// 根据类型ID获取所对应的商品
        /// </summary>
        /// <returns></returns>
        public ShopListInfo GetShopInfoByShopID(int ShopID)
        {
            return platformData.GetShopInfoByShopID(ShopID);
        }

        /// <summary>
        /// 兑换商品
        /// </summary>
        /// <returns></returns>
        public Message UserConvertShop(RecordShopConvert ShopConvert)
        {
            return platformData.UserConvertShop(ShopConvert);
        }
        #region 公共

        /// <summary>
        /// 根据SQL语句查询一个值
        /// </summary>
        /// <param name="sqlQuery"></param>
        /// <returns></returns>
        public object GetObjectBySql(string sqlQuery)
        {
            return platformData.GetObjectBySql(sqlQuery);
        }

        /// <summary>
        /// 根据sql获取实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public T GetEntity<T>(string commandText)
        {
            return platformData.GetEntity<T>(commandText);
        }

        #endregion
    }
}
