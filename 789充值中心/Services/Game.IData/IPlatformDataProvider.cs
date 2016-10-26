using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Game.Kernel;
using Game.Entity.Platform;
using Game.Entity.Filter;

namespace Game.IData
{
    /// <summary>
    /// 本地数据库接口层
    /// </summary>
    public interface IPlatformDataProvider : IProvider
    {
        #region 授权验证

        /// <summary>
        /// 验证授权
        /// </summary>
        /// <param name="stationID"></param>
        /// <param name="accreditKey"></param>
        /// <param name="ip"></param>
        /// <returns></returns>
        Message ValidAccredit(int stationID, string accreditKey, string ip);
        
        #endregion

        /// <summary>
        /// 根据游戏ID获得游戏名称
        /// </summary>
        /// <param name="kindID"></param>
        /// <returns></returns>
        string GetGameKindNameByID(int kindID);

        /// <summary>
        /// 根据房间ID获取房间名称
        /// </summary>
        /// <param name="serverID"></param>
        /// <returns></returns>
        string GetGameRoomName(int serverID);

        /// <summary>
        /// 根据ID 得到想在对应的配置信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        PublicConfig GetPublicConfig(string key);

        /// <summary>
        /// 得到游戏类型列表
        /// </summary>
        /// <returns></returns>
        IList<GameTypeItem> GetGameTypes();

        /// <summary>
        /// 根据类型ID
        /// </summary>
        /// <returns></returns>
        IList<GameKindItem> GetKindIDByTypeID(int typeID);

        /// <summary>
        /// 得到所有的游戏列表
        /// </summary>
        /// <returns></returns>
        IList<GameKindItem> GetAllKinds();

        /// <summary>
        /// 获取游戏实体
        /// </summary>
        /// <param name="kindID"></param>
        /// <returns></returns>
        GameKindItem GetKindByKindID(int kindID);


        /// <summary>
        /// 根据商品ID获得商品名称
        /// </summary>
        /// <param name="ShopID"></param>
        /// <returns></returns>
        string GetShopNameByID(int ShopID);

        /// <summary>
        /// 得到所有的商品列表
        /// </summary>
        /// <returns></returns>
        IList<ShopListInfo> GetAllShops();

        /// <summary>
        /// 根据类型ID获取所对应的商品
        /// </summary>
        /// <returns></returns>
        ShopListInfo GetShopInfoByShopID(int ShopID);


        /// <summary>
        /// 兑换商品
        /// </summary>
        /// <returns></returns>
        Message UserConvertShop(RecordShopConvert ShopConvert);

        #region 公共

        /// <summary>
        /// 根据SQL语句查询一个值
        /// </summary>
        /// <param name="sqlQuery"></param>
        /// <returns></returns>
        object GetObjectBySql(string sqlQuery);

        /// <summary>
        /// 根据sql获取实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="commandText"></param>
        /// <returns></returns>
        T GetEntity<T>(string commandText);

        #endregion
    }
}
