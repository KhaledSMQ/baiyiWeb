using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using Game.Kernel;
using Game.Entity.Treasure;
using Game.Entity.Filter;
using Game.Entity.Accounts;

namespace Game.IData
{
    /// <summary>
    /// 金币库接口层
    /// </summary>
    public interface ITreasureDataProvider : IProvider
    {
        #region 分站银行

        /// <summary>
        /// 获取分站银行信息
        /// </summary>
        /// <param name="stationID"></param>
        /// <returns></returns>
        InsureStationInfo GetInsureStationInfo(int stationID);

        #endregion

        #region 充值服务

        /// <summary>
        /// 获取服务列表
        /// </summary>
        /// <returns></returns>
        IList<GlobalShareInfo> GetShareList();
        #endregion

        #region 在线充值

        ///// <summary>
        ///// 生成订单
        ///// </summary>
        ///// <param name="orderInfo"></param>
        ///// <returns></returns>
        //void RequestOrder(OnLineOrder orderInfo);


        // <summary>
        /// 写易宝返回记录
        /// </summary>
        /// <param name="returnYB"></param>
        Message WriteReturnYBDetail(ReturnYPDetailInfo returnYB);

        /// <summary>
        /// 生成订单
        /// </summary>
        /// <param name="orderInfo"></param>
        /// <returns></returns>
        Message RequestOrder(OnLineOrder orderInfo);

        /// <summary>
        /// 获取订单信息
        /// </summary>
        /// <param name="livcard"></param>
        /// <param name="livPassword"></param>
        /// <returns></returns>
        Message GetOnLineOrder(string orderID);

        /// <summary>
        /// 写快钱返回记录
        /// </summary>
        /// <param name="returnKQ"></param>
        void WriteReturnKQDetail(ReturnKQDetailInfo returnKQ);

        ///// <summary>
        ///// 在线充值
        ///// </summary>
        ///// <param name="olDetial"></param>
        ///// <returns></returns>
        //void FilliedOnline(ShareDetailInfo olDetial);


        /// <summary>
        /// 在线充值
        /// </summary>
        /// <param name="olDetial"></param>
        /// <returns></returns>
        Message FilliedOnline(ShareDetialInfo olDetial, int isVB);


        /// <summary>
        /// 申请兑换
        /// </summary>
        /// <param name="exchange"></param>
        /// <param name="loginPass"></param>
        /// <param name="InsurePass"></param>
        /// <returns></returns>
        Message ApplyRecordUserExchange(RecordUserExchange exchange, string loginPass, string InsurePass);

        #endregion

        #region 充值记录

        /// <summary>
        /// 充值记录
        /// </summary>
        /// <param name="insureSearchFilter"></param>
        /// <returns></returns>
        PagerSet GetShareDetailInfo(InsureSearchFilter insureSearchFilter);

        /// <summary>
        /// 充值成功记录
        /// </summary>
        /// <param name="whereQuery"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        PagerSet GetShareDetailInfo(string whereQuery, int pageIndex, int pageSize);

        /// <summary>
        /// 在线充值记录
        /// </summary>
        /// <param name="whereQuery"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        PagerSet GetOnLineOrder(string whereQuery, int pageIndex, int pageSize);

        /// <summary>
        /// 获取前 n 条抽奖记录信息
        /// </summary>
        /// <param name="issueType"></param>
        /// <param name="top"></param>
        /// <returns></returns>
        DataTable GetTopLotteryRecordWin();

        /// <summary>
        /// 获取前 n 条充值成功记录信息
        /// </summary>
        /// <param name="issueType"></param>
        /// <param name="top"></param>
        /// <returns></returns>
        IList<OnLineOrder> GetTopOnlineOrderSuccess(int top);

        /// <summary>
        /// 兑换记录
        /// </summary>
        /// <param name="insureSearchFilter"></param>
        /// <returns></returns>
        PagerSet GetRecordUserExchange(string whereQuery, int pageIndex, int pageSize);

        /// <summary>
        /// 实卡充值
        /// </summary>
        /// <param name="associator"></param>
        /// <param name="operUserID"></param>
        /// <param name="accounts"></param>
        /// <param name="ip"></param>
        /// <returns></returns>
        Message FilledLivcard(ShareDetialInfo detialInfo, string password);

        #endregion

        #region  抽水日志

        /// <summary>
        /// 根据用户ID获得单个用户抽水总额
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        double GetRecordRevenue(int userID, int startDateID, int endDateID);

        /// <summary>
        /// 获取抽水日志列表
        /// </summary>
        /// <param name="whereQuery"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        PagerSet GetStreamScoreInfo(string whereQuery, int pageIndex, int pageSize);

        /// <summary>
        /// 获取抽水记录列表
        /// </summary>
        /// <param name="whereQuery"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        PagerSet GetRecordUserRevenue(string whereQuery, int pageIndex, int pageSize);

        /// <summary>
        /// 抽水转换日志
        /// </summary>
        /// <param name="finance"></param>
        /// <returns></returns>
        Message TransferRecordUserFinance(RecordUserFinance finance);

       /// <summary>
       /// 获取抽水转换日志
       /// </summary>
       /// <param name="RecordID"></param>
       /// <returns></returns>
        RecordUserFinance GetRecordUserFinanceByRecordID(int RecordID);

        /// <summary>
        /// 根据用户ID获得单个用户抽水总额
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        decimal GetRecordAmountsByUserID(int userID);

        #endregion


        #region 用户金币

        /// <summary>
        /// 获取指定用户的财富信息 [by userID]
        /// </summary>
        /// <param name="userTicket"></param>
        /// <returns></returns>
        Message GetTreasureInfo(UserTicketInfo userTicket);

        /// <summary>
        /// 根据用户ID得到金币信息
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        Message GetTreasureInfo2(int UserID);

        /// <summary>
        /// 获取用户的金币详情 {输赢和逃}
        /// </summary>
        /// <param name="userTicket"></param>
        /// <returns></returns>
        Message GetGoldScoreInfo(UserTicketInfo userTicket);

        #endregion

        #region 银行操作

        /// <summary>
        /// 
        /// </summary>
        /// <param name="insure"></param>
        /// <returns></returns>
        Message InsureIn(int userID, int TradeScore, int minTradeScore, string clientIP, string note);

        //取出
        Message InsureOut(int userID, string insurePass, int TradeScore, int minTradeScore, string clientIP, string note);

        //转帐
        Message InsureTransfer(int srcUserID, string insurePass, int dstUserID, int TradeScore, int minTradeScore, string clientIP, string note);

        //交易记录
        PagerSet GetInsureTradeRecord(InsureSearchFilter insureSearchFilter);

        #endregion

        #region 金币赠送

        /// <summary>
        /// 注册送金
        /// </summary>
        /// <param name="user">用户对象</param>
        /// <returns></returns>
        Message RegisterPresentScore(UserInfo user, decimal goldPresent);

        #endregion

        #region 进出记录

        //进出记录
        PagerSet GetUserInoutRecord(InoutSearchFilter inoutSearchFilter);

        #endregion

        #region 金币榜单

        //金币榜单
        IList<GameScoreInfo> GetChartsScore(int stationID, int topN);

        //金币榜单
        DataSet GetChartsScoreForTable(int stationID, int topN);

        //用户排名
        GameScoreInfo GetChartsUser(int stationID, int userID);

        //自己排名与排在自己前面排名
        IList<GameScoreInfo> GetChartsUserBefore(int stationID, int userID);

        #endregion

        #region 抽奖日志

        /// <summary>
        /// 获取抽奖日志列表
        /// </summary>
        /// <param name="whereQuery"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        PagerSet GetGameLotteryRecord(string whereQuery, int pageIndex, int pageSize);

        /// <summary>
        /// 根据用户ID得到相对应的抽奖日志
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="clientIP"></param>
        /// <returns></returns>
        Message GetGameLotteryInfo(int userID, string clientIP);

        #endregion

        #region   游戏记录

        /// <summary>
        /// 获取玩家每局游戏情况列表
        /// </summary>
        /// <param name="whereQuery"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        PagerSet GetRecordDrawInfo(string whereQuery, int pageIndex, int pageSize);

        /// <summary>
        /// 获取每局所有玩家的游戏信息列表
        /// </summary>
        /// <param name="whereQuery"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        PagerSet GetRecordDrawScore(string whereQuery, int pageIndex, int pageSize);

        #endregion

        #region  玩家缺水日志

        /// <summary>
        /// 分页显示玩家今日的抽水信息
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        DataSet GetAccountRevenueToday(int userID, int pageIndex, int pageSize);

        /// <summary>
        /// 分页显示今日玩家进行游戏的下线信息
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        DataSet GetAccountPlayedToday(int userID, int pageIndex, int pageSize);

        #endregion

        #region 获取订单信息
        /// <summary>
        /// 获取订单信息
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        OnLineOrder GetOnlineOrder(string orderID);

        #endregion

        #region  兑换充值币
        Message ChangeMoney(string Accounts,decimal money,string IpAddress);


        #endregion

        #region 查询自己的充值币数量

        object GetMoneyCountByAccount(string sqlQuery);
       


        #endregion

        #region 推广中心

        /// <summary>
        /// 获取用户推广信息
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        Message GetUserSpreadInfo(int userID);

        /// <summary>
        /// 用户推广结算
        /// </summary>
        /// <param name="balance"></param>
        /// <param name="userID"></param>
        /// <param name="ip"></param>
        /// <returns></returns>
        Message GetUserSpreadBalance(int balance, int userID, string ip);

        /// <summary>
        /// 推广记录
        /// </summary>
        /// <param name="whereQuery"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        PagerSet GetSpreaderRecord(string whereQuery, int pageIndex, int pageSize);

        /// <summary>
        /// 单个用户下所有被推荐人的推广信息
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        DataSet GetUserSpreaderList(int userID, int pageIndex, int pageSize);

        /// <summary>
        /// 获取单个结算总额
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        long GetUserSpreaderTotal(string sWhere);

        #endregion

    }
}
