using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;

using Game.Kernel;
using Game.Utils;
using Game.IData;
using Game.Entity.Platform;
using Game.Entity.Filter;
using Game.Entity;

namespace Game.Data
{
    /// <summary>
    /// 本地数据库访问层
    /// </summary>
    public class PlatformDataProvider : BaseDataProvider, IPlatformDataProvider
    {
        #region 构造方法

        public PlatformDataProvider(string connString)
            : base(connString)
        {

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
            var prams = new List<DbParameter>();

            prams.Add(Database.MakeInParam("dwStationID", stationID));
            prams.Add(Database.MakeInParam("strAccreditID", accreditKey));
            prams.Add(Database.MakeInParam("strClientIP", ip));
            prams.Add(Database.MakeOutParam("strErrorDescribe", typeof(string), 127));

            return MessageHelper.GetMessage(Database, "WEB_WS_ValidateAccredit", prams);
        }
        #endregion

        /// <summary>
        /// 根据游戏ID获得游戏名称
        /// </summary>
        /// <param name="kindID"></param>
        /// <returns></returns>
        public string GetGameKindNameByID(int kindID)
        {
            return this.GetKindByKindID(kindID) == null ? "" : this.GetKindByKindID(kindID).KindName.ToString();
        }

        /// <summary>
        /// 根据房间ID获取房间名称
        /// </summary>
        /// <param name="serverID"></param>
        /// <returns></returns>
        public string GetGameRoomName(int serverID)
        {
            StringBuilder sqlQuery = new StringBuilder();
            sqlQuery.Append("SELECT ServerID, ServerName, KindID, ServerType ")
                    .Append("FROM GameRoomInfo ")
                    .AppendFormat("WHERE ServerID={0} ", serverID);

            return Database.ExecuteObject<GameRoomInfo>(sqlQuery.ToString()) == null ? "" : Database.ExecuteObject<GameRoomInfo>(sqlQuery.ToString()).ServerName.ToString();
        }

        /// <summary>
        /// 根据ID 得到想在对应的配置信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PublicConfig GetPublicConfig(string key)
        {
            StringBuilder sqlQuery = new StringBuilder();
            sqlQuery.Append("SELECT ConfigID, ConfigKey, ConfigName, ConfigString, Field1, Field2, Field3, Field4, Field5, Field6, Field7, Field8 ")
                    .Append("FROM PublicConfig ")
                    .AppendFormat("WHERE ConfigKey='{0}' ", key);

            return Database.ExecuteObject<PublicConfig>(sqlQuery.ToString());
        }

        /// <summary>
        /// 得到游戏类型列表
        /// </summary>
        /// <returns></returns>
        public IList<GameTypeItem> GetGameTypes()
        {
            StringBuilder sqlQuery = new StringBuilder();
            sqlQuery.Append("SELECT TypeID,TypeName ")
                    .Append("FROM GameTypeItem ")
                    .Append("WHERE  Nullity=0")
                    .Append(" ORDER By SortID ASC,TypeID ASC");

            return Database.ExecuteObjectList<GameTypeItem>(sqlQuery.ToString());
        }

        /// <summary>
        /// 根据类型ID获取所对应的游戏列表
        /// </summary>
        /// <returns></returns>
        public IList<GameKindItem> GetKindIDByTypeID(int typeID)
        {
            StringBuilder sqlQuery = new StringBuilder();
            sqlQuery.Append("SELECT KindID,DownLoadUrl, GameID, TypeID, KindName ")
                    .Append("FROM GameKindItem ")
                    .AppendFormat("WHERE  Nullity=0 AND TypeID={0} ", typeID)
                    .Append(" ORDER By SortID ASC,KindID ASC");

            return Database.ExecuteObjectList<GameKindItem>(sqlQuery.ToString());
        }

        /// <summary>
        /// 得到所有的游戏列表
        /// </summary>
        /// <returns></returns>
        public IList<GameKindItem> GetAllKinds()
        {
            StringBuilder sqlQuery = new StringBuilder();
            sqlQuery.Append("SELECT k.KindID, k.GameID, TypeID, k.SortID, k.KindName, ProcessName, GameRuleUrl, DownLoadUrl, k.Nullity ")
                    .Append("FROM GameKindItem k,QPNativeWebDB.dbo.GameRulesInfo r ")
                    .Append(" WHERE k.KindID = r.KindID AND r.IsJoin=0 AND r.Nullity=0")
                    .Append(" ORDER By Nullity ASC, SortID ASC");

            return Database.ExecuteObjectList<GameKindItem>(sqlQuery.ToString());
        }

        /// <summary>
        /// 获取游戏实体
        /// </summary>
        /// <param name="kindID"></param>
        /// <returns></returns>
        public GameKindItem GetKindByKindID(int kindID)
        {
            StringBuilder sqlQuery = new StringBuilder();
            sqlQuery.Append("SELECT KindID, GameID, TypeID, SortID, KindName, ProcessName, GameRuleUrl, DownLoadUrl, Nullity ")
                    .Append("FROM GameKindItem ")
                    .AppendFormat("WHERE KindID={0} ", kindID);

            return Database.ExecuteObject<GameKindItem>(sqlQuery.ToString());
        }

        /// <summary>
        /// 根据类型ID获取所对应的商品
        /// </summary>
        /// <returns></returns>
        public ShopListInfo GetShopInfoByShopID(int ShopID)
        {
            StringBuilder sqlQuery = new StringBuilder();
            sqlQuery.Append("SELECT ShopID, ShopName, ShopPrice, ShopNum, ShopIntro, ShopIntroImg, ShopIndexImg ")
                    .Append("FROM ShopInfo ")
                    .AppendFormat("WHERE  Nullity=0 AND ShopID={0} ", ShopID);


            return Database.ExecuteObject<ShopListInfo>(sqlQuery.ToString());
        }

        /// <summary>
        /// 根据商品ID获得商品名称
        /// </summary>
        /// <param name="kindID"></param>
        /// <returns></returns>
        public string GetShopNameByID(int ShopID)
        {
            return this.GetShopInfoByShopID(ShopID).ShopName;
        }

        /// <summary>
        /// 得到所有的商品列表
        /// </summary>
        /// <returns></returns>
        public IList<ShopListInfo> GetAllShops()
        {
            StringBuilder sqlQuery = new StringBuilder();
            sqlQuery.Append("SELECT ShopID, ShopName, ShopPrice, ShopNum, ShopIntro, ShopIntroImg, ShopIndexImg ")
                    .Append("FROM ShopInfo ")
                    .Append("WHERE Nullity=0 ")
                    .Append("ORDER By ShopID ASC");

            return Database.ExecuteObjectList<ShopListInfo>(sqlQuery.ToString());
        }

        /// <summary>
        /// 兑换商品
        /// </summary>
        /// <returns></returns>
        public Message UserConvertShop(RecordShopConvert ShopConvert)
        {
            List<DbParameter> parms = new List<DbParameter>();
            parms.Add(Database.MakeInParam("nUserID", ShopConvert.UserID));
            parms.Add(Database.MakeInParam("nShopID", ShopConvert.ShopID));
            parms.Add(Database.MakeInParam("nConvertNum", ShopConvert.ConvertNum));
            parms.Add(Database.MakeInParam("PassPortID", ShopConvert.PassPortID));
            parms.Add(Database.MakeInParam("Compellation", ShopConvert.Compellation));
            parms.Add(Database.MakeInParam("QQ", ShopConvert.QQ));
            parms.Add(Database.MakeInParam("EMail", ShopConvert.EMail));
            parms.Add(Database.MakeInParam("MobilePhone", ShopConvert.MobilePhone));
            parms.Add(Database.MakeInParam("DwellingPlace", ShopConvert.DwellingPlace));
            parms.Add(Database.MakeInParam("PostalCode", ShopConvert.PostalCode));
            parms.Add(Database.MakeInParam("strClientIP", ShopConvert.ConvertIP));
            parms.Add(Database.MakeOutParam("strRetrun", typeof(string), 127));

            return MessageHelper.GetMessage(Database, "WEB_SHOP_CONVERT", parms);
        }

        #region 公共

        /// <summary>
        /// 根据SQL语句查询一个值
        /// </summary>
        /// <param name="sqlQuery"></param>
        /// <returns></returns>
        public object GetObjectBySql(string sqlQuery)
        {
            return Database.ExecuteScalar(System.Data.CommandType.Text, sqlQuery);
        }

        /// <summary>
        /// 根据sql获取实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public T GetEntity<T>(string commandText)
        {
            return Database.ExecuteObject<T>(commandText);
        }

        #endregion
    }
}
