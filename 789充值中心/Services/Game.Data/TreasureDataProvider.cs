using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;

using Game.Kernel;
using Game.Utils;
using Game.Utils.Utils;
using Game.IData;
using Game.Entity.Treasure;
using Game.Entity.Filter;
using Game.Entity.Accounts;
using Game.Entity;
namespace Game.Data
{
    /// <summary>
    /// 金币库数据访问层
    /// </summary>
    public class TreasureDataProvider : BaseDataProvider, ITreasureDataProvider
    {
        #region 构造方法

        public TreasureDataProvider(string connString)
            : base(connString)
        {

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
            var parms = new List<DbParameter>();
            parms.Add(Database.MakeInParam("dwStationID", stationID));

            return Database.RunProcObject<InsureStationInfo>("WEB_GetInsureStationInfo", parms);
        }

        #endregion

        #region 充值服务

        /// <summary>
        /// 获取服务列表
        /// </summary>
        /// <returns></returns>
        public IList<GlobalShareInfo> GetShareList()
        {
            IList<GlobalShareInfo> list = Database.RunProcObjectList<GlobalShareInfo>("WEB_GetGlobalShareInfoList");
            return list;
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
            string sqlQuery = string.Format("SELECT * FROM OnLineOrder(NOLOCK) WHERE OrderID='{0}'", orderID);
            OnLineOrder order = Database.ExecuteObject<OnLineOrder>(sqlQuery);

            return order;
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
        //    List<DbParameter> prams = new List<DbParameter>();
        //    prams.Add(Database.MakeInParam("dwOperUserID", orderInfo.OperUserID));
        //    prams.Add(Database.MakeInParam("dwShareID", orderInfo.ShareID));
        //    prams.Add(Database.MakeInParam("dwUserID", orderInfo.UserID));
        //    prams.Add(Database.MakeInParam("strAccounts", orderInfo.Accounts));

        //    prams.Add(Database.MakeInParam("strOrderID", orderInfo.OrderID));
        //    prams.Add(Database.MakeInParam("dcPresentScore", orderInfo.PresentScore));
        //    prams.Add(Database.MakeInParam("dcDiscountScale", orderInfo.DiscountScale));
        //    prams.Add(Database.MakeInParam("dcOrderAmount", orderInfo.OrderAmount));
        //    prams.Add(Database.MakeInParam("dcPayAmount", orderInfo.PayAmount));
        //    prams.Add(Database.MakeInParam("dwOrderStatus", orderInfo.OrderStatus));
        //    prams.Add(Database.MakeInParam("strClientIP", orderInfo.IPAddress));

        //    Database.RunProc("WEB_RequisitionOrder", prams);   
        //}


        /// <summary>
        /// 生成订单
        /// </summary>
        /// <param name="orderInfo"></param>
        /// <returns></returns>
        public Message RequestOrder(OnLineOrder orderInfo)
        {
            List<DbParameter> prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("dwOperUserID", orderInfo.OperUserID));
            prams.Add(Database.MakeInParam("dwShareID", orderInfo.ShareID));
            prams.Add(Database.MakeInParam("strAccounts", orderInfo.Accounts));
            prams.Add(Database.MakeInParam("strOrderID", orderInfo.OrderID));
            prams.Add(Database.MakeInParam("dwCardTypeID", orderInfo.CardTypeID));
            prams.Add(Database.MakeInParam("dwCardTotal", orderInfo.CardTotal));
            prams.Add(Database.MakeInParam("dcOrderAmount", orderInfo.OrderAmount));
            prams.Add(Database.MakeInParam("strTelPhone", orderInfo.TelPhone));
            prams.Add(Database.MakeInParam("strIPAddress", orderInfo.IPAddress));
            prams.Add(Database.MakeOutParam("strErrorDescribe", typeof(string), 127));

            return MessageHelper.GetMessageForObject<OnLineOrder>(Database, ApplicationSettings.Get("onlineOrder"), prams);
        }

        /// <summary>
        /// 获取订单信息
        /// </summary>
        /// <param name="livcard"></param>
        /// <param name="livPassword"></param>
        /// <returns></returns>
        public Message GetOnLineOrder(string orderID)
        {
            var parms = new List<DbParameter>();
            parms.Add(Database.MakeInParam("strOrderID", orderID));
            return MessageHelper.GetMessageForObject<OnLineOrder>(Database, "WEB_GetOnLineOrder", parms);
        }

        /// <summary>
        /// 写快钱返回记录
        /// </summary>
        /// <param name="returnKQ"></param>
        public void WriteReturnKQDetail(ReturnKQDetailInfo returnKQ)
        {
            var parms = new List<DbParameter>();
            parms.Add(Database.MakeInParam("strMerchantAcctID", returnKQ.MerchantAcctID));
            parms.Add(Database.MakeInParam("strVersion", returnKQ.Version));
            parms.Add(Database.MakeInParam("dwLanguage", returnKQ.Language));
            parms.Add(Database.MakeInParam("dwSignType", returnKQ.SignType));
            parms.Add(Database.MakeInParam("strPayType", returnKQ.PayType));
            parms.Add(Database.MakeInParam("strBankID", returnKQ.BankID));
            parms.Add(Database.MakeInParam("strOrderID", returnKQ.OrderID));
            parms.Add(Database.MakeInParam("dtOrderTime", returnKQ.OrderTime));
            parms.Add(Database.MakeInParam("dcOrderAmount", returnKQ.OrderAmount));
            parms.Add(Database.MakeInParam("strDealID", returnKQ.DealID));
            parms.Add(Database.MakeInParam("strBankDealID", returnKQ.BankDealID));
            parms.Add(Database.MakeInParam("dtDealTime", returnKQ.DealTime));
            parms.Add(Database.MakeInParam("dcPayAmount", returnKQ.PayAmount));
            parms.Add(Database.MakeInParam("dcFee", returnKQ.Fee));
            parms.Add(Database.MakeInParam("strPayResult", returnKQ.PayResult));
            parms.Add(Database.MakeInParam("strErrCode", returnKQ.ErrCode));
            parms.Add(Database.MakeInParam("strSignMsg", returnKQ.SignMsg));
            parms.Add(Database.MakeInParam("strExt1", returnKQ.Ext1));
            parms.Add(Database.MakeInParam("strExt2", returnKQ.Ext2));

            Database.RunProc("WEB_WriteReturnKQDetail", parms);
        }

        /// <summary>
        /// 写易宝返回记录
        /// </summary>
        /// <param name="returnYB"></param>
        public Message WriteReturnYBDetail(ReturnYPDetailInfo returnYB)
        {
            var parms = new List<DbParameter>();
            parms.Add(Database.MakeInParam("p1_MerId", returnYB.P1_MerId));
            parms.Add(Database.MakeInParam("r0_Cmd", returnYB.R0_Cmd));
            parms.Add(Database.MakeInParam("r1_Code", returnYB.R1_Code));
            parms.Add(Database.MakeInParam("r2_TrxId", returnYB.R2_TrxId));
            parms.Add(Database.MakeInParam("r3_Amt", returnYB.R3_Amt));
            parms.Add(Database.MakeInParam("r4_Cur", returnYB.R4_Cur));
            parms.Add(Database.MakeInParam("r5_Pid", returnYB.R5_Pid));
            parms.Add(Database.MakeInParam("r6_Order", returnYB.R6_Order));
            parms.Add(Database.MakeInParam("r7_Uid", returnYB.R7_Uid));
            parms.Add(Database.MakeInParam("r8_MP", returnYB.R8_MP));
            parms.Add(Database.MakeInParam("r9_BType", returnYB.R9_BType));
            parms.Add(Database.MakeInParam("rb_BankId", returnYB.Rb_BankId));
            parms.Add(Database.MakeInParam("ro_BankOrderId", returnYB.Ro_BankOrderId));
            parms.Add(Database.MakeInParam("rp_PayDate", returnYB.Rp_PayDate));
            parms.Add(Database.MakeInParam("rq_CardNo", returnYB.Rq_CardNo));
            parms.Add(Database.MakeInParam("ru_Trxtime", returnYB.Ru_Trxtime));
            parms.Add(Database.MakeInParam("hmac", returnYB.Hmac));

            return MessageHelper.GetMessage(Database, "NET_PW_AddReturnYBInfo", parms); ;
        }



        ///// <summary>
        ///// 在线充值
        ///// </summary>
        ///// <param name="olDetial"></param>
        ///// <returns></returns>
        //public void FilliedOnline(ShareDetailInfo olDetial)
        //{
        //    var parms = new List<DbParameter>();
        //    parms.Add(Database.MakeInParam("OperUserID", olDetial.OperUserID));
        //    parms.Add(Database.MakeInParam("ShareID", olDetial.ShareID));
        //    parms.Add(Database.MakeInParam("UserID", olDetial.UserID));
        //    parms.Add(Database.MakeInParam("Accounts", olDetial.Accounts));

        //    parms.Add(Database.MakeInParam("BeforeGold", olDetial.BeforeGold));
        //    parms.Add(Database.MakeInParam("OrderAmount", olDetial.OrderAmount));
        //    parms.Add(Database.MakeInParam("PresentScore", olDetial.PresentScore));
        //    parms.Add(Database.MakeInParam("DiscountScale", olDetial.DiscountScale));
        //    parms.Add(Database.MakeInParam("strOrderID", olDetial.OrderID));
        //    parms.Add(Database.MakeInParam("dcPayAmount", olDetial.PayAmount));

        //    parms.Add(Database.MakeInParam("strClientIP", olDetial.IPAddress));

        //    Database.RunProc("WEB_FilledOnline", parms);   
        //}


        /// <summary>
        /// 在线充值
        /// </summary>
        /// <param name="olDetial"></param>
        /// <returns></returns>
        public Message FilliedOnline(ShareDetialInfo olDetial, int isVB)
        {
            var parms = new List<DbParameter>();
            parms.Add(Database.MakeInParam("strOrdersID", olDetial.OrderID));
            parms.Add(Database.MakeInParam("strOrderAmount", olDetial.PayAmount));
            parms.Add(Database.MakeInParam("isVB", isVB));
            parms.Add(Database.MakeInParam("strIPAddress", olDetial.IPAddress));
            parms.Add(Database.MakeOutParam("strErrorDescribe", typeof(string), 127));

            return MessageHelper.GetMessage(Database,ApplicationSettings.Get("backforcard"), parms);
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
            List<DbParameter> parms = new List<DbParameter>();
            parms.Add(Database.MakeInParam("dwUserID", exchange.UserID));
            parms.Add(Database.MakeInParam("strSrcLogonPass", loginPass));
            parms.Add(Database.MakeInParam("strSrcInsurePass", InsurePass));

            parms.Add(Database.MakeInParam("dcApplyMoney", exchange.ApplyMoney));
            parms.Add(Database.MakeInParam("dwAccountType", exchange.AccountType));
            parms.Add(Database.MakeInParam("strAccountNo", exchange.AccountNo));
            parms.Add(Database.MakeInParam("strAccountName", exchange.AccountName));
            parms.Add(Database.MakeInParam("strBankName", exchange.BankName));
            parms.Add(Database.MakeInParam("strBankAddress", exchange.BankAddress));

            parms.Add(Database.MakeInParam("strClientIP", GameRequest.GetUserIP()));
            parms.Add(Database.MakeOutParam("strErrorDescribe", typeof(string), 127));

            return MessageHelper.GetMessage(Database, "WEB_ApplyExchange", parms);
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
            string whereForAll = string.Format("WHERE UserID={0} AND (ApplyDate BETWEEN '{1}' AND '{2}')", insureSearchFilter.UserID, insureSearchFilter.OperateDateScope[0], insureSearchFilter.OperateDateScope[1]);
            string orderQuery = "ORDER By ApplyDate DESC";
            PagerParameters pager = new PagerParameters("ShareDetailInfo", orderQuery, whereForAll, insureSearchFilter.PageIndex, insureSearchFilter.PageSize);
            pager.CacherSize = 2;

            return GetPagerSet2(pager);
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
            string orderQuery = "ORDER By ApplyDate DESC";
            string[] returnField = new string[14] { "DetailID", "OperUserID", "ShareID", "UserID", "Accounts", "OrderID", "BeforeGold", "OrderAmount", "PresentScore", "PresentMedal", "DiscountScale", "PayAmount", "IPAddress", "ApplyDate" };
            PagerParameters pager = new PagerParameters(ShareDetailInfo.Tablename, orderQuery, whereQuery, pageIndex, pageSize);

            pager.Fields = returnField;
            pager.CacherSize = 2;

            return GetPagerSet2(pager);
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
            string orderQuery = "ORDER By ApplyDate DESC";
            string[] returnField = new string[13] { "OnLineID", "OperUserID", "ShareID", "UserID", "Accounts", "OrderID", "OrderAmount", "PresentScore", "DiscountScale", "PayAmount", "OrderStatus", "IPAddress", "ApplyDate" };
            PagerParameters pager = new PagerParameters(OnLineOrder.Tablename, orderQuery, whereQuery, pageIndex, pageSize);

            pager.Fields = returnField;
            pager.CacherSize = 2;

            return GetPagerSet2(pager);
        }

        /// <summary>
        /// 获取前 n 条抽奖记录信息
        /// </summary>
        /// <param name="issueType"></param>
        /// <param name="top"></param>
        /// <returns></returns>
        public DataTable GetTopLotteryRecordWin()
        {
            DataSet ds;
            Database.RunProc("WEB_GetLotteryNotice", out ds);
            return ds.Tables[0];


        }

        /// <summary>
        /// 获取前 n 条充值成功记录信息
        /// </summary>
        /// <param name="issueType"></param>
        /// <param name="top"></param>
        /// <returns></returns>
        public IList<OnLineOrder> GetTopOnlineOrderSuccess(int top)
        {
            StringBuilder sqlQuery = new StringBuilder()
            .AppendFormat("SELECT TOP({0}) ", top)
            .Append("OnLineID, OperUserID, ShareID, UserID, Accounts, OrderID, OrderAmount, PresentScore, DiscountScale, PayAmount, OrderStatus, IPAddress, ApplyDate ")
            .Append("FROM OnLineOrder ");

            //查询条件
            sqlQuery.Append(" WHERE OrderStatus=2 ");

            //排序
            sqlQuery.Append(" ORDER By ApplyDate DESC");

            return Database.ExecuteObjectList<OnLineOrder>(sqlQuery.ToString());
        }


        /// <summary>
        /// 兑换记录
        /// </summary>
        /// <param name="insureSearchFilter"></param>
        /// <returns></returns>
        public PagerSet GetRecordUserExchange(string whereQuery, int pageIndex, int pageSize)
        {
            string orderQuery = "ORDER By CollectDate DESC";
            string[] returnField = new string[15] { "RecordID", "UserID", "Accounts", "OrderID", "ApplyMoney", "AccountType", "AccountNo", "AccountName", "BankName", "BankAddress", "ApplyStatus", "IPAddress", "DealDate", "DealNote", "CollectDate" };
            PagerParameters pager = new PagerParameters(RecordUserExchange.Tablename, orderQuery, whereQuery, pageIndex, pageSize);

            pager.Fields = returnField;
            pager.CacherSize = 2;

            return GetPagerSet2(pager);
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
            List<DbParameter> prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("dwOperUserID", detialInfo.OperUserID));

            prams.Add(Database.MakeInParam("strSerialID", detialInfo.SerialID));
            prams.Add(Database.MakeInParam("strPassword", password));
            prams.Add(Database.MakeInParam("strAccounts", detialInfo.Accounts));

            prams.Add(Database.MakeInParam("strClientIP", detialInfo.IPAddress));
            prams.Add(Database.MakeOutParam("strErrorDescribe", typeof(string), 127));

            return MessageHelper.GetMessage(Database, "NET_PW_FilledLivcard", prams);
        }

        #endregion

        #region  抽水日志

        /// <summary>
        /// 获取玩家总的抽水总额
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public double GetRecordRevenue(int userID, int startDateID, int endDateID)
        {
            List<DbParameter> parms = new List<DbParameter>();
            parms.Add(Database.MakeInParam("dwUserID", userID));
            parms.Add(Database.MakeInParam("dwStartDateID", startDateID));
            parms.Add(Database.MakeInParam("dwEndDateID", endDateID));

            DataSet ds = null;
            Database.RunProc("WSP_PM_GetAccountTotalRevenue", parms, out ds);
            return ds.Tables[0].Rows[0][0].ToString() == "" ? 0 : Convert.ToDouble(ds.Tables[0].Rows[0][0].ToString());
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
            string orderQuery = "ORDER By FirstCollectDate DESC";
            string[] returnField = new string[9] { "DateID", "UserID", "StationID", "WinScore", "LostScore", "Revenue", "PlayTimeCount", "FirstCollectDate", "LastCollectDate" };
            PagerParameters pager = new PagerParameters(StreamScoreInfo.Tablename, orderQuery, whereQuery, pageIndex, pageSize);

            pager.Fields = returnField;
            pager.CacherSize = 2;

            return GetPagerSet2(pager);
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
            string orderQuery = "ORDER By CollectDate DESC";
            string[] returnField = new string[23] { "RecordID", "DateID", "UserID", "StationID", "Revenue", "UserID1", "Scale1", "Revenue1", "UserID2", "Scale2", "Revenue2", "UserID3", "Scale3", "Revenue3", "UserID4", "Scale4", "Revenue4", "UserID5", "Scale5", "Revenue5", "CompanyScale", "CompanyRevenue", "CollectDate" };
            PagerParameters pager = new PagerParameters(RecordUserRevenue.Tablename, orderQuery, whereQuery, pageIndex, pageSize);

            pager.Fields = returnField;
            pager.CacherSize = 2;

            return GetPagerSet2(pager);
        }


        /// <summary>
        /// 抽水转换日志
        /// </summary>
        /// <param name="finance"></param>
        /// <returns></returns>
        public Message TransferRecordUserFinance(RecordUserFinance finance)
        {
            List<DbParameter> parms = new List<DbParameter>();
            parms.Add(Database.MakeInParam("dwUserID", finance.UserID));
            parms.Add(Database.MakeInParam("dcInMoney", finance.Amount));

            parms.Add(Database.MakeOutParam("strErrorDescribe", typeof(string), 127));

            return MessageHelper.GetMessage(Database, "WEB_TransferRevenue", parms);
        }

        /// <summary>
        /// 获取抽水转换日志
        /// </summary>
        /// <param name="RecordID"></param>
        /// <returns></returns>
        public RecordUserFinance GetRecordUserFinanceByRecordID(int RecordID)
        {
            return Database.ExecuteObject<RecordUserFinance>(string.Format("SELECT * FROM RecordUserFinance(NOLOCK) WHERE RecordID={0}", RecordID));
        }

        /// <summary>
        /// 根据用户ID获得单个用户抽水总额
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public decimal GetRecordAmountsByUserID(int userID)
        {
            RecordUserFinance finance = Database.ExecuteObject<RecordUserFinance>(string.Format("SELECT sum(Amount) as Amount FROM RecordUserFinance(NOLOCK) WHERE UserID={0}", userID));
            return finance.Amount;
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
            List<DbParameter> parms = new List<DbParameter>();
            parms.Add(Database.MakeInParam("dwUserID", userTicket.UserID));
            parms.Add(Database.MakeInParam("strLogonPass", userTicket.LogonPass));
            parms.Add(Database.MakeOutParam("strErrorDescribe", typeof(string), 127));

            return MessageHelper.GetMessageForObject<GameScoreInfo>(Database, "NET_GetTreasureInfo", parms);
        }

        /// <summary>
        /// 根据用户ID得到金币信息
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public Message GetTreasureInfo2(int UserID)
        {
            List<DbParameter> parms = new List<DbParameter>();
            parms.Add(Database.MakeInParam("dwUserID", UserID));

            return MessageHelper.GetMessageForObject<GameScoreInfo>(Database, "NET_GetTreasureInfo2", parms);
        }

        /// <summary>
        /// 获取用户的金币详情 {输赢和逃}
        /// </summary>
        /// <param name="userTicket"></param>
        /// <returns></returns>
        public Message GetGoldScoreInfo(UserTicketInfo userTicket)
        {
            List<DbParameter> parms = new List<DbParameter>();
            parms.Add(Database.MakeInParam("dwUserID", userTicket.UserID));
            parms.Add(Database.MakeInParam("strLogonPasswd", userTicket.LogonPass));
            parms.Add(Database.MakeOutParam("strErrorDescribe", typeof(string), 127));

            return MessageHelper.GetMessageForObject<GameScoreInfo>(Database, "WEB_GetGoldScoreInfo", parms);
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
            List<DbParameter> parms = new List<DbParameter>();
            parms.Add(base.Database.MakeInParam("dwUserID", userID));
            parms.Add(base.Database.MakeInParam("dwMinSwapScore", minTradeScore));
            parms.Add(base.Database.MakeInParam("dwSwapScore", TradeScore));
            parms.Add(base.Database.MakeInParam("strClientIP", clientIP));
            parms.Add(base.Database.MakeInParam("strCollectNote", note));
            parms.Add(base.Database.MakeOutParam("strErrorDescribe", typeof(string), 0x7f));
            return MessageHelper.GetMessageForObject<GameScoreInfo>(base.Database, "NET_PW_InsureIn", parms);
        }

        /// <summary>
        /// 取出金币
        /// </summary>
        /// <param name="insure"></param>
        /// <returns></returns>
        public Message InsureOut(int userID, string insurePass, int TradeScore, int minTradeScore, string clientIP, string note)
        {
            List<DbParameter> parms = new List<DbParameter>();
            parms.Add(base.Database.MakeInParam("dwUserID", userID));
            parms.Add(base.Database.MakeInParam("strInsurePass", insurePass));
            parms.Add(base.Database.MakeInParam("dwMinSwapScore", minTradeScore));
            parms.Add(base.Database.MakeInParam("dwSwapScore", TradeScore));
            parms.Add(base.Database.MakeInParam("strClientIP", clientIP));
            parms.Add(base.Database.MakeInParam("strCollectNote", note));
            parms.Add(base.Database.MakeOutParam("strErrorDescribe", typeof(string), 0x7f));
            return MessageHelper.GetMessageForObject<GameScoreInfo>(base.Database, "NET_PW_InsureOut", parms);
        }


        /// <summary>
        /// 金币转账
        /// </summary>
        /// <param name="insure"></param>
        /// <returns></returns>
        public Message InsureTransfer(int srcUserID, string insurePass, int dstUserID, int TradeScore, int minTradeScore, string clientIP, string note)
        {
            List<DbParameter> parms = new List<DbParameter>();
            parms.Add(base.Database.MakeInParam("dwSrcUserID", srcUserID));
            parms.Add(base.Database.MakeInParam("strInsurePass", insurePass));
            parms.Add(base.Database.MakeInParam("dwDstUserID", dstUserID));
            parms.Add(base.Database.MakeInParam("dwMinSwapScore", minTradeScore));
            parms.Add(base.Database.MakeInParam("dwSwapScore", TradeScore));
            parms.Add(base.Database.MakeInParam("strClientIP", clientIP));
            parms.Add(base.Database.MakeInParam("strCollectNote", note));
            parms.Add(base.Database.MakeOutParam("strErrorDescribe", typeof(string), 0x7f));
            return MessageHelper.GetMessage(base.Database, "NET_PW_InsureTransfer", parms);
        }


        /// <summary>
        /// 交易记录
        /// </summary>
        /// <param name="insureSearchFilter"></param>
        /// <returns></returns>
        public PagerSet GetInsureTradeRecord(InsureSearchFilter insureSearchFilter)
        {
            string whereForAll = "";
            string whereForTradeType = "";
            string whereForTradeType2 = "";
            if (insureSearchFilter.OperateDateScope != null)
            {
                whereForAll = string.Format("WHERE (CollectDate BETWEEN '{1}' AND '{2}') AND (SourceUserID={0} OR TargetUserID={0})", insureSearchFilter.UserID, insureSearchFilter.OperateDateScope[0], insureSearchFilter.OperateDateScope[1]);
                whereForTradeType = string.Format("WHERE (CollectDate BETWEEN '{2}' AND '{3}') AND TradeType ={1} AND SourceUserID={0}", new object[] { insureSearchFilter.UserID, (int)insureSearchFilter.TradeType, insureSearchFilter.OperateDateScope[0], insureSearchFilter.OperateDateScope[1] });
                whereForTradeType2 = string.Format("WHERE (CollectDate BETWEEN '{2}' AND '{3}') AND TradeType ={1} AND TargetUserID={0}", new object[] { insureSearchFilter.UserID, 3, insureSearchFilter.OperateDateScope[0], insureSearchFilter.OperateDateScope[1] });
            }
            else
            {
                whereForAll = string.Format("WHERE (SourceUserID={0} OR TargetUserID={0})", insureSearchFilter.UserID);
                whereForTradeType = string.Format("WHERE TradeType ={1} AND SourceUserID={0}", insureSearchFilter.UserID, (int)insureSearchFilter.TradeType);
                whereForTradeType2 = string.Format("WHERE TradeType ={1} AND TargetUserID={0}", insureSearchFilter.UserID, 3);
            }
            string orderQuery = "ORDER By CollectDate DESC";
            string[] returnField = new string[] { "SourceUserID", "TargetUserID", "SwapScore", "Revenue", "TradeType", "CollectDate", "CollectNote" };
            PagerParameters pager = new PagerParameters("RecordInsure", orderQuery, "RecordID", insureSearchFilter.PageIndex, insureSearchFilter.PageSize);
            pager.Fields = returnField;
            pager.CacherSize = 2;
            if (insureSearchFilter.TradeType == InsureTradeType.Unknown)
            {
                pager.WhereStr = whereForAll;
            }
            else if (insureSearchFilter.TradeType == InsureTradeType.TransferIn)
            {
                pager.WhereStr = whereForTradeType2;
            }
            else
            {
                pager.WhereStr = whereForTradeType;
            }
            return this.GetPagerSet2(pager);
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
            var parms = new List<DbParameter>();
            parms.Add(Database.MakeInParam("dwUserID", user.UserID));
            parms.Add(Database.MakeInParam("strLogonPasswd", user.LogonPass));
            parms.Add(Database.MakeInParam("dcGoldPresent", goldPresent));
            parms.Add(Database.MakeInParam("strClientIP", user.LastLogonIP));
            parms.Add(Database.MakeOutParam("strErrorDescribe", typeof(string), 127));

            return MessageHelper.GetMessageForObject<User>(Database, "WEB_RegisterPresentScore", parms);
        }

        #endregion

        #region 进出记录

        //进出记录
        public PagerSet GetUserInoutRecord(InoutSearchFilter inoutSearchFilter)
        {
            var parms = new List<DbParameter>();
            parms.Add(Database.MakeInParam("dwUserID", inoutSearchFilter.UserID));
            parms.Add(Database.MakeInParam("dwKindID", inoutSearchFilter.KindID));
            parms.Add(Database.MakeInParam("dwLastDays", inoutSearchFilter.LastDays));
            parms.Add(Database.MakeInParam("dwPageSize", inoutSearchFilter.PageSize));
            parms.Add(Database.MakeInParam("dwPageIndex", inoutSearchFilter.PageIndex));
            parms.Add(Database.MakeOutParam("dwRowsCount", inoutSearchFilter.RowsCount, typeof(int), 4));

            DataSet inoutDs = null;
            Database.RunProc("WEB_GetUserInoutRecord", parms, out inoutDs);
            int rowsCount = TypeParse.StrToInt(parms[parms.Count - 2].Value, 0);
            PagerSet pagerSet = new PagerSet(inoutSearchFilter.PageIndex, inoutSearchFilter.PageSize, 0, rowsCount, inoutDs);
            return pagerSet;
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
            var parms = new List<DbParameter>();
            parms.Add(Database.MakeInParam("dwStationID", stationID));
            parms.Add(Database.MakeInParam("dwTopN", topN));

            return Database.RunProcObjectList<GameScoreInfo>("WEB_GetChartsScore", parms);
        }

        /// <summary>
        /// 金币排名
        /// </summary>
        /// <param name="stationID"></param>
        /// <param name="topN"></param>
        /// <returns></returns>
        public DataSet GetChartsScoreForTable(int stationID, int topN)
        {
            var parms = new List<DbParameter>();
            parms.Add(Database.MakeInParam("dwStationID", stationID));
            parms.Add(Database.MakeInParam("dwTopN", topN));

            DataSet ds = null;
            Database.RunProc("WEB_GetChartsScore", parms, out ds);
            return ds;
        }

        /// <summary>
        /// 用户排名
        /// </summary>
        /// <param name="stationID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public GameScoreInfo GetChartsUser(int stationID, int userID)
        {
            var parms = new List<DbParameter>();
            parms.Add(Database.MakeInParam("dwStationID", stationID));
            parms.Add(Database.MakeInParam("dwUserID", userID));

            return Database.RunProcObject<GameScoreInfo>("WEB_GetChartsUser", parms);
        }

        /// <summary>
        /// 排在自己前面和自己的用户信息
        /// </summary>
        /// <param name="stationID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public IList<GameScoreInfo> GetChartsUserBefore(int stationID, int userID)
        {
            var parms = new List<DbParameter>();
            parms.Add(Database.MakeInParam("dwStationID", stationID));
            parms.Add(Database.MakeInParam("dwUserID", userID));

            return Database.RunProcObjectList<GameScoreInfo>("WEB_GetChartsUserBefore", parms);
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
            string orderQuery = "ORDER By InsertTime DESC";
            string[] returnField = new string[13] { "DrawID", "KindID", "ServerID", "TableID", "UserCount", "AndroidCount", "Waste", "Revenue", "UserMedal", "StartTime", "ConcludeTime", "InsertTime", "DrawCourse" };
            PagerParameters pager = new PagerParameters(RecordDrawInfo.Tablename, orderQuery, whereQuery, pageIndex, pageSize);

            pager.Fields = returnField;
            pager.CacherSize = 2;

            return GetPagerSet2(pager);
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
            string orderQuery = "ORDER By InsertTime DESC";
            string[] returnField = new string[9] { "DrawID", "UserID", "ChairID", "Score", "Grade", "Revenue", "UserMedal", "PlayTimeCount", "InsertTime" };
            PagerParameters pager = new PagerParameters(RecordDrawScore.Tablename, orderQuery, whereQuery, pageIndex, pageSize);

            pager.Fields = returnField;
            pager.CacherSize = 2;

            return GetPagerSet2(pager);
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
            string orderQuery = "ORDER By CollectDate DESC";
            string[] returnField = new string[10] { "LotteryID", "UserID", "PayUserMedal", "BeforeUserMedal", "IsWin", "BeforeScore", "BeforeInsureScore", "WinScore", "IPAddress", "CollectDate" };
            PagerParameters pager = new PagerParameters(GameLotteryRecord.Tablename, orderQuery, whereQuery, pageIndex, pageSize);

            pager.Fields = returnField;
            pager.CacherSize = 2;

            return GetPagerSet2(pager);
        }

        /// <summary>
        /// 根据用户ID得到相对应的抽奖日志
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="clientIP"></param>
        /// <returns></returns>
        public Message GetGameLotteryInfo(int userID, string clientIP)
        {
            List<DbParameter> parms = new List<DbParameter>();
            parms.Add(Database.MakeInParam("dwUserID", userID));
            parms.Add(Database.MakeInParam("strClientIP", clientIP));
            parms.Add(Database.MakeOutParam("strErrorDescribe", typeof(string), 127));

            return MessageHelper.GetMessageForObject<GameLotteryRecord>(Database, "NET_PW_GameLottery", parms);
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
            List<DbParameter> prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("dwUserID", userID));
            prams.Add(Database.MakeInParam("dwPageSize", pageSize));
            prams.Add(Database.MakeInParam("dwPageIndex", pageIndex));

            DataSet ds = null;
            Database.RunProc("WEB_GetAccountRevenueToday", prams, out ds);

            return ds;
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
            List<DbParameter> prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("dwUserID", userID));
            prams.Add(Database.MakeInParam("dwPageSize", pageSize));
            prams.Add(Database.MakeInParam("dwPageIndex", pageIndex));

            DataSet ds = null;
            Database.RunProc("WEB_GetAccountPlayedToday", prams, out ds);

            return ds;
        }

        #endregion


        #region  兑换充值币
        public Message ChangeMoney(string Accounts, decimal money, string IpAddress)
        {

            var parms = new List<DbParameter>();
            parms.Add(Database.MakeInParam("UserName", Accounts));
            parms.Add(Database.MakeInParam("ChangeMoney", money));
            parms.Add(Database.MakeInParam("strIPAddress", IpAddress));
            parms.Add(Database.MakeOutParam("strErrorDescribe", typeof(string), 127));

            return MessageHelper.GetMessage(Database, "NET_PW_MoneyChange", parms);


        }


        #endregion

        #region 查询自己拥有的金币数量  根据用户名
        public object GetMoneyCountByAccount(string sqlQuery)
        {
            return Database.ExecuteScalar(System.Data.CommandType.Text, sqlQuery);

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
            List<DbParameter> prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("dwUserID", userID));

            prams.Add(Database.MakeOutParam("strErrorDescribe", typeof(string), 127));

            return MessageHelper.GetMessageForObject<RecordSpreadInfo>(Database, "NET_PW_GetUserSpreadInfo", prams);
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
            List<DbParameter> prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("dwUserID", userID));
            prams.Add(Database.MakeInParam("dwBalance", balance));

            prams.Add(Database.MakeInParam("strClientIP", ip));
            prams.Add(Database.MakeOutParam("strErrorDescribe", typeof(string), 127));

            return MessageHelper.GetMessage(Database, "NET_PW_SpreadBalance", prams);
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
            string orderQuery = "ORDER By CollectDate DESC";
            string[] returnField = new string[8] { "RecordID", "UserID", "Score", "TypeID", "ChildrenID", "InsureScore", "CollectDate", "CollectNote" };
            PagerParameters pager = new PagerParameters(RecordSpreadInfo.Tablename, orderQuery, whereQuery, pageIndex, pageSize);

            pager.Fields = returnField;
            pager.CacherSize = 2;

            return GetPagerSet2(pager);
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
            List<DbParameter> prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("dwUserID", userID));
            prams.Add(Database.MakeInParam("dwPageIndex", pageIndex));
            prams.Add(Database.MakeInParam("dwPageSize", pageSize));

            DataSet ds = new DataSet();
            Database.RunProc("NET_PW_GetAllChildrenInfoByUserID", prams, out ds);

            return ds;
        }

        /// <summary>
        /// 获取单个结算总额
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public long GetUserSpreaderTotal(string sWhere)
        {
            string sql = "SELECT SUM(Score) AS Score FROM RecordSpreadInfo ";
            if (sWhere != "" && sWhere != null)
            {
                sql += sWhere;
            }

            RecordSpreadInfo spreader = Database.ExecuteObject<RecordSpreadInfo>(sql);
            return spreader == null ? 0 : spreader.Score;
        }

        #endregion
    }
}
