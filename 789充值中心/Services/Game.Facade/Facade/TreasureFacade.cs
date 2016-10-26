using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Game.Data.Factory;
using Game.IData;
using Game.Entity;
using Game.Entity.Treasure;
using Game.Kernel;
using Game.Utils;
using Game.Entity.Filter;
using Game.Entity.Accounts;
using System.Data;

namespace Game.Facade
{
    public class TreasureFacade
    {
        #region Fields

        private ITreasureDataProvider treasureData;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public TreasureFacade()
        {
            treasureData = ClassFactory.GetITreasureDataProvider();
        }
        #endregion

        #region 分站银行

        /// <summary>
        /// 获取分站银行信息
        /// </summary>
        /// <param name="stationID"></param>
        /// <returns></returns>
        public InsureStationInfo GetInsureStationInfo(int stationID)
        {
            return treasureData.GetInsureStationInfo(stationID);
        }

        #endregion

        #region 充值服务

        /// <summary>
        /// 获取服务列表
        /// </summary>
        /// <returns></returns>
        public IList<GlobalShareInfo> GetShareList()
        {
            return treasureData.GetShareList();
        }

        /// <summary>
        /// 获取服务别名
        /// </summary>
        /// <param name="shareID"></param>
        /// <returns></returns>
        public string GetShareAlias(int shareID)
        {
            IList<GlobalShareInfo> shareList = GetShareList();
            if (CollectionHelper.IsNullOrEmpty<GlobalShareInfo>(shareList))
            {
                return "";
            }

            GlobalShareInfo share = CollectionHelper.FindFirst<GlobalShareInfo>(shareList
                , delegate(GlobalShareInfo shareInfo) { return shareID == shareInfo.ShareID; });
            if (share == null) return "";

            return share.ShareAlias.Trim();
        }

        /// <summary>
        /// 获取服务名称
        /// </summary>
        /// <param name="shareID"></param>
        /// <returns></returns>
        public string GetShareName(int shareID)
        {
            IList<GlobalShareInfo> shareList = GetShareList();
            if (CollectionHelper.IsNullOrEmpty<GlobalShareInfo>(shareList))
            {
                return "";
            }

            GlobalShareInfo share = CollectionHelper.FindFirst<GlobalShareInfo>(shareList
                , delegate(GlobalShareInfo shareInfo) { return shareID == shareInfo.ShareID; });
            if (share == null) return "";

            return share.ShareName.Trim();
        }

        #endregion

        #region 在线充值

        ///// <summary>
        ///// 生成订单
        ///// </summary>
        ///// <param name="orderInfo"></param>
        ///// <returns></returns>
        //public void RequestOrder(OnLineOrder orderInfo)
        //{
        //    treasureData.RequestOrder(orderInfo);
        //}


        /// <summary>
        /// 生成订单
        /// </summary>
        /// <param name="orderInfo"></param>
        /// <returns></returns>
        public Message RequestOrder(OnLineOrder orderInfo)
        {
            return treasureData.RequestOrder(orderInfo);
        }

        /// <summary>
        /// 获取订单信息
        /// </summary>
        /// <param name="livcard"></param>
        /// <param name="livPassword"></param>
        /// <returns></returns>
        public Message GetOnLineOrder(string orderID)
        {
            return treasureData.GetOnLineOrder(orderID);
        }

        /// <summary>
        /// 写快钱返回记录
        /// </summary>
        /// <param name="returnKQ"></param>
        public void WriteReturnKQDetail(ReturnKQDetailInfo returnKQ)
        {
            treasureData.WriteReturnKQDetail(returnKQ);
        }

        ///// <summary>
        ///// 在线充值
        ///// </summary>
        ///// <param name="olDetial"></param>
        ///// <returns></returns>
        //public void FilliedOnline(ShareDetailInfo olDetial)
        //{
        //    treasureData.FilliedOnline(olDetial);
        //}


        /// <summary>
        /// 在线充值
        /// </summary>
        /// <param name="olDetial"></param>
        /// <returns></returns>
        public Message FilliedOnline(ShareDetialInfo olDetial, int isVB)
        {
            return treasureData.FilliedOnline(olDetial, isVB);
        }





        /// <summary>
        /// 写易宝返回记录
        /// </summary>
        /// <param name="returnYB"></param>
        public Message WriteReturnYBDetail(ReturnYPDetailInfo returnYB)
        {
            return treasureData.WriteReturnYBDetail(returnYB);
        }


        /// <summary>
        /// 申请兑换
        /// </summary>
        /// <param name="exchange"></param>
        /// <param name="loginPass"></param>
        /// <param name="InsurePass"></param>
        /// <returns></returns>
        public Message ApplyRecordUserExchange(RecordUserExchange exchange, string loginPass, string InsurePass)
        {
            return treasureData.ApplyRecordUserExchange(exchange, loginPass, InsurePass);
        }

        #endregion

        #region 充值记录

        /// <summary>
        /// 充值记录
        /// </summary>
        /// <param name="insureSearchFilter"></param>
        /// <returns></returns>
        public PagerSet GetShareDetailInfo(InsureSearchFilter insureSearchFilter)
        {
            return treasureData.GetShareDetailInfo(insureSearchFilter);
        }

        /// <summary>
        /// 充值成功记录
        /// </summary>
        /// <param name="whereQuery"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public PagerSet GetShareDetailInfo(string whereQuery, int pageIndex, int pageSize)
        {
            return treasureData.GetShareDetailInfo(whereQuery, pageIndex, pageSize);
        }

        /// <summary>
        /// 在线充值记录
        /// </summary>
        /// <param name="whereQuery"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public PagerSet GetOnLineOrder(string whereQuery, int pageIndex, int pageSize)
        {
            return treasureData.GetOnLineOrder(whereQuery, pageIndex, pageSize);
        }

        /// <summary>
        /// 获取前 n 条抽奖记录信息
        /// </summary>
        /// <param name="issueType"></param>
        /// <param name="top"></param>
        /// <returns></returns>
        public DataTable GetTopLotteryRecordWin()
        {
            return treasureData.GetTopLotteryRecordWin();
        }

        /// <summary>
        /// 获取前 n 条充值成功记录信息
        /// </summary>
        /// <param name="issueType"></param>
        /// <param name="top"></param>
        /// <returns></returns>
        public IList<OnLineOrder> GetTopOnlineOrderSuccess(int top)
        {
            return treasureData.GetTopOnlineOrderSuccess(top);
        }


        /// <summary>
        /// 兑换记录
        /// </summary>
        /// <param name="insureSearchFilter"></param>
        /// <returns></returns>
        public PagerSet GetRecordUserExchange(string whereQuery, int pageIndex, int pageSize)
        {
            return treasureData.GetRecordUserExchange(whereQuery, pageIndex, pageSize);
        }

        /// <summary>
        /// 实卡充值
        /// </summary>
        /// <param name="associator"></param>
        /// <param name="operUserID"></param>
        /// <param name="accounts"></param>
        /// <param name="ip"></param>
        /// <returns></returns>
        public Message FilledLivcard(ShareDetialInfo detialInfo, string password)
        {
            return treasureData.FilledLivcard(detialInfo, password);
        }

        #endregion

        #region  抽水日志

        /// <summary>
        /// 根据用户ID获得单个用户抽水总额
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public double GetRecordRevenue(int userID, int startDateID, int endDateID)
        {
            return treasureData.GetRecordRevenue(userID, startDateID, endDateID);
        }

        /// <summary>
        /// 获取抽水日志列表
        /// </summary>
        /// <param name="whereQuery"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public PagerSet GetStreamScoreInfo(string whereQuery, int pageIndex, int pageSize)
        {
            return treasureData.GetStreamScoreInfo(whereQuery, pageIndex, pageSize);
        }

        /// <summary>
        /// 获取抽水记录列表
        /// </summary>
        /// <param name="whereQuery"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public PagerSet GetRecordUserRevenue(string whereQuery, int pageIndex, int pageSize)
        {
            return treasureData.GetRecordUserRevenue(whereQuery, pageIndex, pageSize);
        }

        /// <summary>
        /// 抽水转换日志
        /// </summary>
        /// <param name="finance"></param>
        /// <returns></returns>
        public Message TransferRecordUserFinance(RecordUserFinance finance)
        {
            return treasureData.TransferRecordUserFinance(finance);
        }

        /// <summary>
        /// 获取抽水转换日志
        /// </summary>
        /// <param name="RecordID"></param>
        /// <returns></returns>
        public RecordUserFinance GetRecordUserFinanceByRecordID(int RecordID)
        {
            return treasureData.GetRecordUserFinanceByRecordID(RecordID);
        }

        /// <summary>
        /// 根据用户ID获得单个用户抽水总额
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public decimal GetRecordAmountsByUserID(int userID)
        {
            return treasureData.GetRecordAmountsByUserID(userID);
        }

        #endregion


        #region 用户金币

        /// <summary>
        /// 获取指定用户的财富信息 [by userID]
        /// </summary>
        /// <param name="userTicket"></param>
        /// <returns></returns>
        public Message GetTreasureInfo(UserTicketInfo userTicket)
        {
            return treasureData.GetTreasureInfo(userTicket);
        }

        /// <summary>
        /// 根据用户ID得到金币信息
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public Message GetTreasureInfo2(int UserID)
        {
            return treasureData.GetTreasureInfo2(UserID);
        }

        /// <summary>
        /// 获取用户的金币详情 {输赢和逃}
        /// </summary>
        /// <param name="userTicket"></param>
        /// <returns></returns>
        public Message GetGoldScoreInfo(UserTicketInfo userTicket)
        {
            return treasureData.GetGoldScoreInfo(userTicket);
        }

        #endregion

        #region 银行操作

        /// <summary>
        /// 金币存入
        /// </summary>
        /// <param name="insure"></param>
        /// <returns></returns>
        public Message InsureIn(int userID, int TradeScore, int minTradeScore, string clientIP, string note)
        {
            return this.treasureData.InsureIn(userID, TradeScore, minTradeScore, clientIP, note);
        }

        /// <summary>
        /// 取出金币
        /// </summary>
        /// <param name="insure"></param>
        /// <returns></returns>
        public Message InsureOut(int userID, string insurePass, int TradeScore, int minTradeScore, string clientIP, string note)
        {
            return this.treasureData.InsureOut(userID, insurePass, TradeScore, minTradeScore, clientIP, note);
        }

        /// <summary>
        /// 金币转账
        /// </summary>
        /// <param name="insure"></param>
        /// <returns></returns>
        public Message InsureTransfer(int srcUserID, string insurePass, int dstUserID, int TradeScore, int minTradeScore, string clientIP, string note)
        {
            return this.treasureData.InsureTransfer(srcUserID, insurePass, dstUserID, TradeScore, minTradeScore, clientIP, note);
        }

        /// <summary>
        /// 交易记录
        /// </summary>
        /// <param name="insureSearchFilter"></param>
        /// <returns></returns>
        public PagerSet GetInsureTradeRecord(InsureSearchFilter insureSearchFilter)
        {
            return treasureData.GetInsureTradeRecord(insureSearchFilter);
        }

        #endregion

        #region 金币赠送

        /// <summary>
        /// 注册送金
        /// </summary>
        /// <param name="user">用户对象</param>
        /// <returns></returns>
        public Message RegisterPresentScore(UserInfo user, decimal goldPresent)
        {
            return treasureData.RegisterPresentScore(user, goldPresent);
        }

        #endregion

        #region 进出记录

        //进出记录
        public PagerSet GetUserInoutRecord(InoutSearchFilter inoutSearchFilter)
        {
            return treasureData.GetUserInoutRecord(inoutSearchFilter);
        }

        #endregion

        #region 金币榜单

        /// <summary>
        /// 金币排行
        /// </summary>
        /// <param name="stationID"></param>
        /// <param name="topN"></param>
        /// <returns></returns>
        public IList<GameScoreInfo> GetChartsScore(int stationID, int topN)
        {
            return treasureData.GetChartsScore(stationID, topN);
        }

        /// <summary>
        /// 金币排名
        /// </summary>
        /// <param name="stationID"></param>
        /// <param name="topN"></param>
        /// <returns></returns>
        public DataSet GetChartsScoreForTable(int stationID, int topN)
        {
            return treasureData.GetChartsScoreForTable(stationID, topN);
        }

        /// <summary>
        /// 用户排名
        /// </summary>
        /// <param name="stationID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public GameScoreInfo GetChartsUser(int stationID, int userID)
        {
            return treasureData.GetChartsUser(stationID, userID);
        }

        /// <summary>
        /// 排在自己前面和自己的用户信息
        /// </summary>
        /// <param name="stationID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public IList<GameScoreInfo> GetChartsUserBefore(int stationID, int userID)
        {
            return treasureData.GetChartsUserBefore(stationID, userID);
        }

        #endregion

        #region   游戏记录

        /// <summary>
        /// 获取玩家每局游戏情况列表
        /// </summary>
        /// <param name="whereQuery"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public PagerSet GetRecordDrawInfo(string whereQuery, int pageIndex, int pageSize)
        {
            return treasureData.GetRecordDrawInfo(whereQuery, pageIndex, pageSize);
        }

        /// <summary>
        /// 获取每局所有玩家的游戏信息列表
        /// </summary>
        /// <param name="whereQuery"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public PagerSet GetRecordDrawScore(string whereQuery, int pageIndex, int pageSize)
        {
            return treasureData.GetRecordDrawScore(whereQuery, pageIndex, pageSize);
        }

        #endregion

        #region 抽奖日志

        /// <summary>
        /// 获取抽奖日志列表
        /// </summary>
        /// <param name="whereQuery"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public PagerSet GetGameLotteryRecord(string whereQuery, int pageIndex, int pageSize)
        {
            return treasureData.GetGameLotteryRecord(whereQuery, pageIndex, pageSize);
        }

        /// <summary>
        /// 根据用户ID得到相对应的抽奖日志
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="clientIP"></param>
        /// <returns></returns>
        public Message GetGameLotteryInfo(int userID, string clientIP)
        {
            return treasureData.GetGameLotteryInfo(userID, clientIP);
        }

        #endregion

        #region  玩家缺水日志

        /// <summary>
        /// 分页显示玩家今日的抽水信息
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public DataSet GetAccountRevenueToday(int userID, int pageIndex, int pageSize)
        {
            return treasureData.GetAccountRevenueToday(userID, pageIndex, pageSize);
        }

        /// <summary>
        /// 分页显示今日玩家进行游戏的下线信息
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public DataSet GetAccountPlayedToday(int userID, int pageIndex, int pageSize)
        {
            return treasureData.GetAccountPlayedToday(userID, pageIndex, pageSize);
        }

        #endregion

        #region 获取订单信息
        /// <summary>
        /// 获取订单信息
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public OnLineOrder GetOnlineOrder(string orderID)
        {
            return treasureData.GetOnlineOrder(orderID);
        }

#endregion

        #region  兑换充值币
        public Message ChangeMoney(string Accounts, decimal money, string IpAddress)
        {

            return treasureData.ChangeMoney(Accounts, money, IpAddress);

        }


        #endregion


        #region  根据用户名查询自己的金币
        public object GetMoneyCountByAccount(string sqlQuery)
        {
            return treasureData.GetMoneyCountByAccount(sqlQuery);

        }

        #endregion
        #region 推广中心

        /// <summary>
        /// 获取用户推广信息
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public Message GetUserSpreadInfo(int userID)
        {
            return treasureData.GetUserSpreadInfo(userID);
        }

        /// <summary>
        /// 用户推广结算信息
        /// </summary>
        /// <param name="balance"></param>
        /// <param name="userID"></param>
        /// <param name="ip"></param>
        /// <returns></returns>
        public Message GetUserSpreadBalance(int balance, int userID, string ip)
        {
            return treasureData.GetUserSpreadBalance(balance, userID, ip);
        }

        /// <summary>
        /// 推广记录
        /// </summary>
        /// <param name="whereQuery"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public PagerSet GetSpreaderRecord(string whereQuery, int pageIndex, int pageSize)
        {
            return treasureData.GetSpreaderRecord(whereQuery, pageIndex, pageSize);
        }

        /// <summary>
        /// 单个用户下所有被推荐人的推广信息
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public DataSet GetUserSpreaderList(int userID, int pageIndex, int pageSize)
        {
            return treasureData.GetUserSpreaderList(userID, pageIndex, pageSize);
        }

        /// <summary>
        /// 获取单个结算总额
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public long GetUserSpreaderTotal(string sWhere)
        {
            return treasureData.GetUserSpreaderTotal(sWhere);
        }

        #endregion
    }
}
