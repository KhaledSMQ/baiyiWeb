using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;

using Game.Kernel;
using Game.IData;
using Game.Entity.Accounts;

namespace Game.Data
{
    public class AccountsDataProvider : BaseDataProvider, IAccountsDataProvider
    {
        #region 构造方法

        public AccountsDataProvider(string connString)
            : base(connString)
        {
            
        }
        #endregion
        /// <summary>
        /// 绑定电话
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="Phone"></param>
        /// <returns></returns>
        public Message BingPhone(int UserID, string Phone)
        {
            List<DbParameter> prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("dwUserID", UserID));
            prams.Add(Database.MakeInParam("UserPhone", Phone));
            prams.Add(Database.MakeOutParam("strErrorDescribe", typeof(string), 127));

            return MessageHelper.GetMessage(Database, "BingUserPhone", prams);
        }

        /// <summary>
        /// 绑定EMail
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="Email"></param>
        /// <returns></returns>
        public Message BingEmail(int UserID, string Email)
        {

            List<DbParameter> prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("dwUserID", UserID));
            prams.Add(Database.MakeInParam("UserEmail", Email));
            prams.Add(Database.MakeOutParam("strErrorDescribe", typeof(string), 127));

            return MessageHelper.GetMessage(Database, "BingUserEmail", prams);
        }
       
        #region 用户登录、注册

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="names">用户信息</param>
        /// <returns></returns>
        public Message Login(UserInfo user)
        {
            List<DbParameter> prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("strAccounts", user.Accounts));
            prams.Add(Database.MakeInParam("strPassword", user.LogonPass));
            prams.Add(Database.MakeInParam("strClientIP", user.LastLogonIP));
            prams.Add(Database.MakeOutParam("strErrorDescribe", typeof(string), 0x7f));

            return MessageHelper.GetMessageForObject<UserInfo>(Database, "NET_PW_EfficacyAccounts", prams);
        }

        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Message Register(UserInfo user, string parentAccount, int nType)
        {
            List<DbParameter> prams = new List<DbParameter>();
            if (nType == 0)
            {
                nType = 1;
            }
            prams.Add(Database.MakeInParam("strSpreader", parentAccount));
            prams.Add(Database.MakeInParam("strAccounts", user.Accounts));
            prams.Add(Database.MakeInParam("strNickname", user.NickName));
            prams.Add(Database.MakeInParam("strLogonPass", user.LogonPass));

            prams.Add(Database.MakeInParam("strInsurePass", user.InsurePass));
            prams.Add(Database.MakeInParam("strCompellation", user.Compellation));
            prams.Add(Database.MakeInParam("strPassPortID", user.PassPortID));

            prams.Add(Database.MakeInParam("dwFaceID", user.FaceID));
            prams.Add(Database.MakeInParam("dwGender", user.Gender));

            prams.Add(Database.MakeInParam("strClientIP", user.RegisterIP));
            prams.Add(Database.MakeInParam("cbRegType", nType));
            prams.Add(Database.MakeInParam("Url", user.Url));
            prams.Add(Database.MakeInParam("Host", user.Host));
            prams.Add(Database.MakeOutParam("strErrorDescribe", typeof(string), 127));

            return MessageHelper.GetMessageForObject<User>(Database, "NET_PW_RegisterAccounts", prams);
        }

        /// <summary>
        /// 判断用户名是否存在
        /// </summary>
        /// <param name="accounts"></param>
        /// <returns></returns>
        public Message IsAccountsExist(string accounts)
        {
            List<DbParameter> prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("strAccounts", accounts));
            prams.Add(Database.MakeOutParam("strErrorDescribe", typeof(string), 127));

            return MessageHelper.GetMessage(Database, "NET_PW_IsAccountsExists", prams);
        }


        public void InsertAdvKeywords(int soruceid, string url, string keycode, string name)
        {
            List<DbParameter> prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("SourceType",soruceid));
            prams.Add(Database.MakeInParam("SourceUrl",url));
            prams.Add(Database.MakeInParam("Keywords",keycode));
            prams.Add(Database.MakeInParam("UserName",name));
            MessageHelper.GetMessage(Database, "NET_PW_InsertAdvKeywords", prams);


        }

        public int GetGameIDByUserName(string userName)
        {
            string sql =string.Format("SELECT GameID FROM AccountsInfo(NOLOCK) WHERE Accounts='{0}'",userName);
            UserInfo us = base.Database.ExecuteObject<UserInfo>(sql);
            if (us != null)
            {

                return us.GameID;
            }
            return 0;
            
        }
        public void InsertAdsKeywordsRecored(int sourceType, string keywords, string sourceurl, string searchIP)
        {

            List<DbParameter> prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("SourceType", sourceType));
            prams.Add(Database.MakeInParam("Keywords", keywords));
            prams.Add(Database.MakeInParam("SourceUrl", sourceurl));
            prams.Add(Database.MakeInParam("SearchIP", searchIP));
            MessageHelper.GetMessage(Database, "NET_PW_InsertAdvKeywordsRecord", prams);


        }


        public void InsertAdvKeyWordsRegRecord(int sourceType, string keywords, string serchIP, int GameID)
        {
            List<DbParameter> prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("SourceType", sourceType));
            prams.Add(Database.MakeInParam("Keywords", keywords));
            prams.Add(Database.MakeInParam("SearchIP", serchIP));
            prams.Add(Database.MakeInParam("GameID", GameID));
            MessageHelper.GetMessage(Database, "NET_PW_InsertAdvKeywordsRegRecord", prams);




        }
        #endregion

        #region 获取用户信息

        /// <summary>
        /// 根据用户账号获取用户的信息
        /// </summary>
        /// <param name="slDomain"></param>
        /// <returns></returns>
        public AccountsInfo GetAccountsByAccontsName(string AccountsName)
        {
            StringBuilder sqlQuery = new StringBuilder();
            sqlQuery.Append("SELECT Accounts,UserID FROM AccountsInfo ")
                    .AppendFormat(" WHERE Accounts = '{0}'", AccountsName);

            return Database.ExecuteObject<AccountsInfo>(sqlQuery.ToString()) == null ? null : Database.ExecuteObject<AccountsInfo>(sqlQuery.ToString());
        }

        /// <summary>
        /// 根据父级节点得到相对应的用户列表
        /// </summary>
        /// <param name="parentID"></param>
        /// <returns></returns>
        public DataSet GetUsersByParentID(int parentID)
        {
            List<DbParameter> prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("dwParentID", parentID));

            DataSet ds = null;
            Database.RunProc("WSP_PM_GetAccountsByParentID", prams, out ds);

            return ds;
        }

        /// <summary>
        /// 根据父级节点得到相对应的用户列表(分页)
        /// </summary>
        /// <param name="parentID"></param>
        /// <returns></returns>
        public PagerSet GetUsersByParentID(string whereQuery,int pageIndex,int pageSize)
        {
            string orderQuery = "ORDER By UserID ASC";
            string[] returnField = new string[15] { "UserID", "ParentID", "StationID", "ProtectID", "Accounts", "Nullity", "StunDown", "MoorMachine", "WebLogonTimes", "GameLogonTimes", "PlayTimeCount", "OnLineTimeCount", "SpreaderScale", "RegisterDate", "Compellation" };
            PagerParameters pager = new PagerParameters(AccountsInfo.Tablename, orderQuery, whereQuery, pageIndex, pageSize);

            pager.Fields = returnField;
            pager.CacherSize = 2;

            return GetPagerSet2(pager);
        }

        /// <summary>
        /// 获取基本用户信息
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public UserInfo GetUserBaseInfoByUserID(int userID)
        {
            List<DbParameter> prams = new List<DbParameter>();
            prams.Add(base.Database.MakeInParam("dwUserID", userID));
            prams.Add(base.Database.MakeOutParam("strErrorDescribe", typeof(string), 0x7f));
            return base.Database.RunProcObject<UserInfo>("NET_PW_GetUserBaseInfo", prams);
        }

        /// <summary>
        /// 根据用户id获取基本用户信息
        /// </summary>
        /// <param name="userID">用户标识</param>
        /// <returns></returns>
        public UserInfo GetAccountInfoByUserID(int userID)
        {
            List<DbParameter> prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("dwUserID", userID));

            return Database.RunProcObject<UserInfo>("WEB_GetAccountsInfoByUserID", prams);
        }

        /// <summary>
        /// 获取基本用户信息 by accounts
        /// </summary>
        /// <param name="accounts">用户帐号</param>
        /// <returns></returns>
        public UserInfo GetAccountInfoByAccounts(string accounts)
        {
            List<DbParameter> prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("strAccounts", accounts));

            return Database.RunProcObject<UserInfo>("WEB_GetAccountsInfoByAccounts", prams);
        }

        /// <summary>
        /// 根据票证获取用户信息
        /// </summary>
        /// <param name="user">用户</param>     
        /// <returns>返回的对象实例</returns>
        public UserInfo GetUserInfo(UserTicketInfo user)
        {
            List<DbParameter> prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("dwUserID", user.UserID));
            prams.Add(Database.MakeInParam("dwGameID", 0));
            prams.Add(Database.MakeInParam("strAccounts", ""));
            prams.Add(Database.MakeOutParam("strErrorDescribe", typeof(string), 127));


            return Database.RunProcObject<UserInfo>("NET_PW_GetUserGlobalsInfo", prams);
        }

        /// <summary>
        /// 获取指定用户标识的用户个性资料(签名, 自定义头像) [by user]
        /// </summary>
        /// <param name="user">用户</param>
        /// <returns></returns> 		
        public IndividualDatum GetUserContactInfoByUserID(int userID)
        {
            List<DbParameter> parms = new List<DbParameter>();
            parms.Add(Database.MakeInParam("dwUserID", userID));

            parms.Add(Database.MakeOutParam("strErrorDescribe", typeof(string), 127));

            return Database.RunProcObject<IndividualDatum>("NET_PW_GetUserContactInfo", parms);
        }

        /// <summary>
        /// 获得用户信息
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="gameID">游戏ID</param>
        /// <param name="Accounts">用户名</param>
        /// <returns></returns> 		
        public Message GetUserGlobalInfo(int userID, int gameID, string Accounts)
        {
            List<DbParameter> parms = new List<DbParameter>();
            parms.Add(Database.MakeInParam("dwUserID", userID));
            parms.Add(Database.MakeInParam("dwGameID", gameID));
            parms.Add(Database.MakeInParam("strAccounts", Accounts));
            parms.Add(Database.MakeOutParam("strErrorDescribe", typeof(string), 0x7f));
            return MessageHelper.GetMessageForObject<UserInfo>(base.Database, "NET_PW_GetUserGlobalsInfo", parms);
        }

 

        #endregion

//         #region 添加账号
// 
//         /// <summary>
//         /// 添加账号
//         /// </summary>
//         /// <param name="user"></param>
//         /// <returns></returns>
//         public Message AddAccounts(UserInfo user)
//         {
//             List<DbParameter> parms = new List<DbParameter>();
//             parms.Add(Database.MakeInParam("dwStationID", user.StationID));
//             parms.Add(Database.MakeInParam("strAccounts", user.Accounts));
//             parms.Add(Database.MakeInParam("strLogonPass", user.LogonPass));
//             parms.Add(Database.MakeInParam("strInsurePass", user.InsurePass));
// 
//             parms.Add(Database.MakeInParam("ParentID", user.ParentID));
//             parms.Add(Database.MakeInParam("strCompellation", user.Compellation));
// 
//             parms.Add(Database.MakeInParam("strRegisterIP",user.RegisterIP));
// 
//             return MessageHelper.GetMessage(Database, "WSP_PM_AddAccount", parms);
//         }
// 
//         #endregion

        public int GetAccountsId(string UserName)
        {
            string sqlQuery = string.Format("SELECT Accounts, UserID FROM AccountsInfo(NOLOCK) WHERE Accounts='{0}'", UserName);
            UserInfo user = Database.ExecuteObject<UserInfo>(sqlQuery);
            return user == null ? 0 : user.UserID;
        }
     
        #region	 密码管理

        /// <summary>
        /// 重置登录密码
        /// </summary>
        /// <param name="sInfo">密保信息</param>       
        /// <returns></returns>
        public Message ResetLogonPasswd(AccountsProtect sInfo)
        {
            List<DbParameter> parms = new List<DbParameter>();
            parms.Add(base.Database.MakeInParam("dwUserID", sInfo.UserID));
            parms.Add(base.Database.MakeInParam("strPassword", sInfo.LogonPass));
            parms.Add(base.Database.MakeInParam("strResponse1", sInfo.Response1));
            parms.Add(base.Database.MakeInParam("strResponse2", sInfo.Response2));
            parms.Add(base.Database.MakeInParam("strResponse3", sInfo.Response3));
            parms.Add(base.Database.MakeInParam("strClientIP", sInfo.LastLogonIP));
            parms.Add(base.Database.MakeOutParam("strErrorDescribe", typeof(string), 0x7f));
            return MessageHelper.GetMessage(base.Database, "NET_PW_ResetLogonPasswd", parms);

        }

        /// <summary>
        /// 重置银行密码
        /// </summary>
        /// <param name="sInfo">密保信息</param>      
        /// <returns></returns>
        public Message ResetInsurePasswd(AccountsProtect sInfo)
        {
            List<DbParameter> parms = new List<DbParameter>();
            parms.Add(base.Database.MakeInParam("dwUserID", sInfo.UserID));
            parms.Add(base.Database.MakeInParam("strInsurePass", sInfo.InsurePass));
            parms.Add(base.Database.MakeInParam("strResponse1", sInfo.Response1));
            parms.Add(base.Database.MakeInParam("strResponse2", sInfo.Response2));
            parms.Add(base.Database.MakeInParam("strResponse3", sInfo.Response3));
            parms.Add(base.Database.MakeInParam("strClientIP", sInfo.LastLogonIP));
            parms.Add(base.Database.MakeOutParam("strErrorDescribe", typeof(string), 0x7f));
            return MessageHelper.GetMessage(base.Database, "NET_PW_ResetInsurePasswd", parms);

        }

        /// <summary>
        /// 修改登录密码
        /// </summary>
        /// <param name="userID">玩家标识</param>
        /// <param name="srcPassword">旧密码</param>
        /// <param name="dstPassword">新密码</param>
        /// <param name="ip">连接地址</param>
        /// <returns></returns>
        public Message ModifyLogonPasswd(int userID, string srcPassword, string dstPassword, string ip)
        {
            List<DbParameter> parms = new List<DbParameter>();
            parms.Add(Database.MakeInParam("dwUserID", userID));
            parms.Add(Database.MakeInParam("strSrcPassword", srcPassword));
            parms.Add(Database.MakeInParam("strDesPassword", dstPassword));

            parms.Add(Database.MakeInParam("strClientIP", ip));
            parms.Add(Database.MakeOutParam("strErrorDescribe", typeof(string), 127));

            return MessageHelper.GetMessage(Database, "NET_PW_ModifyLogonPass", parms);
        }


        /// <summary>
        /// 修改银行密码
        /// </summary>
        /// <param name="userID">用户标识</param>
        /// <param name="srcPassword">登录密码</param>
        /// <param name="dstPassword">老密码</param>
        /// <param name="ip"></param>
        /// <returns></returns>
        public Message ModifyInsurePasswd(int userID, string srcPassword, string dstPassword, string ip)
        {
            List<DbParameter> parms = new List<DbParameter>();
            parms.Add(Database.MakeInParam("dwUserID", userID));
            parms.Add(Database.MakeInParam("strSrcPassword", srcPassword));
            parms.Add(Database.MakeInParam("strDesPassword", dstPassword));
            parms.Add(Database.MakeInParam("strClientIP", ip));
            parms.Add(Database.MakeOutParam("strErrorDescribe", typeof(string), 127));

            return MessageHelper.GetMessage(Database, "NET_PW_ModifyInsurePass", parms);
        }

        #endregion 密码管理

        #region  密码保护管理

        /// <summary>
        /// 申请帐号保护
        /// </summary>
        /// <param name="sInfo">密保信息</param>
        /// <returns></returns>
        public Message ApplyUserSecurity(AccountsProtect sInfo)
        {
            List<DbParameter> parms = new List<DbParameter>();
            parms.Add(Database.MakeInParam("dwUserID", sInfo.UserID));
            parms.Add(Database.MakeInParam("strQuestion1", sInfo.Question1));
            parms.Add(Database.MakeInParam("strResponse1", sInfo.Response1));
            parms.Add(Database.MakeInParam("strQuestion2", sInfo.Question2));
            parms.Add(Database.MakeInParam("strResponse2", sInfo.Response2));
            parms.Add(Database.MakeInParam("strQuestion3", sInfo.Question3));
            parms.Add(Database.MakeInParam("strResponse3", sInfo.Response3));

            parms.Add(Database.MakeInParam("dwPassportType", sInfo.PassportType));
            parms.Add(Database.MakeInParam("strPassportID", sInfo.PassportID));
            parms.Add(Database.MakeInParam("strSafeEmail", sInfo.SafeEmail));

            parms.Add(Database.MakeInParam("strClientIP", sInfo.CreateIP));
            parms.Add(Database.MakeOutParam("strErrorDescribe", typeof(string), 127));

            return MessageHelper.GetMessage(Database, "NET_PW_AddAccountProtect", parms);
        }

        /// <summary>
        /// 修改帐号保护
        /// </summary>
        /// <param name="oldInfo">旧密保信息</param>
        /// <param name="newInfo">新密保信息</param>
        /// <returns></returns>
        public Message ModifyUserSecurity(AccountsProtect oldInfo, AccountsProtect newInfo)
        {
            List<DbParameter> parms = new List<DbParameter>();
            parms.Add(Database.MakeInParam("dwUserID", newInfo.UserID));
            parms.Add(Database.MakeInParam("strQuestion1", newInfo.Question1));
            parms.Add(Database.MakeInParam("strResponse1", newInfo.Response1));
            parms.Add(Database.MakeInParam("strQuestion2", newInfo.Question2));
            parms.Add(Database.MakeInParam("strResponse2", newInfo.Response2));
            parms.Add(Database.MakeInParam("strQuestion3", newInfo.Question3));
            parms.Add(Database.MakeInParam("strResponse3", newInfo.Response3));
            parms.Add(Database.MakeInParam("strSafeEmail", newInfo.SafeEmail));
            parms.Add(Database.MakeInParam("strOldResponse1", oldInfo.Response1));
            parms.Add(Database.MakeInParam("strOldResponse2", oldInfo.Response2));
            parms.Add(Database.MakeInParam("strOldResponse3", oldInfo.Response3));

            parms.Add(Database.MakeInParam("strClientIP", newInfo.ModifyIP));
            parms.Add(Database.MakeOutParam("strErrorDescribe", typeof(string), 127));

            return MessageHelper.GetMessage(Database, "NET_PW_ModifyAccountProtect", parms);
        }

        /// <summary>
        /// 验证密保信息 (Respose 答案)
        /// </summary>
        /// <param name="sInfo">密保对象</param>
        /// <returns>会员消息对象</returns>
        public Message ValidUserSecurityByResponse(AccountsProtect sInfo)
        {
            List<DbParameter> parms = new List<DbParameter>();
            parms.Add(Database.MakeInParam("dwUserID", sInfo.UserID));

            parms.Add(Database.MakeInParam("strResponse1", sInfo.Response1));
            parms.Add(Database.MakeInParam("strResponse2", sInfo.Response2));
            parms.Add(Database.MakeInParam("strResponse3", sInfo.Response3));

            parms.Add(Database.MakeOutParam("strErrorDescribe", typeof(string), 127));

            return MessageHelper.GetMessage(Database, "WEB_ValidUserSecurityByResponse", parms);
        }

        /// <summary>
        /// 验证密码保护 (Passport 证件号码)
        /// </summary>
        /// <param name="sInfo">密保对象</param>
        /// <returns>会员消息对象</returns>
        public Message ValidUserSecurityPassport(AccountsProtect sInfo)
        {
            List<DbParameter> parms = new List<DbParameter>();
            parms.Add(Database.MakeInParam("dwUserID", sInfo.UserID));
            parms.Add(Database.MakeInParam("strPassportID", sInfo.PassportID));

            parms.Add(Database.MakeInParam("strResponse1", sInfo.Response1));
            parms.Add(Database.MakeInParam("strResponse2", sInfo.Response2));
            parms.Add(Database.MakeInParam("strResponse3", sInfo.Response3));

            parms.Add(Database.MakeOutParam("strErrorDescribe", typeof(string), 127));

            return MessageHelper.GetMessage(Database, "WEB_ValidUserSecurityPassport", parms);
        }

        /// <summary>
        /// 获取密保信息 (userID)
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public Message GetUserSecurityByUserID(int userID)
        {
            List<DbParameter> parms = new List<DbParameter>();
            parms.Add(Database.MakeInParam("dwUserID", userID));

            parms.Add(Database.MakeOutParam("strErrorDescribe", typeof(string), 127));

            return MessageHelper.GetMessageForObject<AccountsProtect>(Database, "NET_PW_GetAccountProtectByUserID", parms);
        }

        /// <summary>
        /// 获取密保信息 (Accounts)
        /// </summary>
        /// <param name="accounts">用户帐号</param>
        /// <returns></returns>
        public Message GetUserSecurityByAccounts(string accounts)
        {
            List<DbParameter> parms = new List<DbParameter>();
            parms.Add(base.Database.MakeInParam("strAccounts", accounts));
            parms.Add(base.Database.MakeOutParam("strErrorDescribe", typeof(string), 0x7f));
            return MessageHelper.GetMessageForObject<AccountsProtect>(base.Database, "NET_PW_GetAccountProtectByAccounts", parms);

        }

        public Message ConfirmUserSecurity(AccountsProtect info)
        {
            List<DbParameter> parms = new List<DbParameter>();
            parms.Add(Database.MakeInParam("dwUserID", info.UserID));

            parms.Add(Database.MakeInParam("strResponse1", info.Response1));
            parms.Add(Database.MakeInParam("strResponse2", info.Response2));
            parms.Add(Database.MakeInParam("strResponse3", info.Response3));

            parms.Add(Database.MakeOutParam("strErrorDescribe", typeof(string), 127));

            return MessageHelper.GetMessage(Database, "NET_PW_ConfirmAccountProtect", parms);
        }


        public int GetUserIDByNickName(string nickName)
        {
            string sqlQuery = string.Format("SELECT NickName, UserID FROM AccountsInfo(NOLOCK) WHERE NickName='{0}'", nickName);
            UserInfo user = base.Database.ExecuteObject<UserInfo>(sqlQuery);
            if (user != null)
            {
                return user.UserID;
            }
            return 0;
        }

      
        #endregion 保护管理

        #region 安全管理

        #region 固定机器

        /// <summary>
        /// 申请机器绑定
        /// </summary>
        /// <param name="sInfo">密保信息</param>
        /// <returns></returns>
        public Message ApplyUserMoorMachine(AccountsProtect sInfo)
        {
            List<DbParameter> parms = new List<DbParameter>();
            parms.Add(Database.MakeInParam("dwUserID", sInfo.UserID));
            parms.Add(Database.MakeInParam("strPassword", sInfo.LogonPass));

            parms.Add(Database.MakeInParam("strResponse1", sInfo.Response1));
            parms.Add(Database.MakeInParam("strResponse2", sInfo.Response2));
            parms.Add(Database.MakeInParam("strResponse3", sInfo.Response3));

            parms.Add(Database.MakeInParam("strClientIP", sInfo.LastLogonIP));
            parms.Add(Database.MakeOutParam("strErrorDescribe", typeof(string), 127));

            return MessageHelper.GetMessage(Database, "WEB_ApplyUserMoorMachine", parms);
        }

        /// <summary>
        /// 解除机器绑定
        /// </summary>
        /// <param name="sInfo">密保信息</param>
        /// <returns></returns>
        public Message RescindUserMoorMachine(AccountsProtect sInfo)
        {
            List<DbParameter> parms = new List<DbParameter>();
            parms.Add(Database.MakeInParam("dwUserID", sInfo.UserID));
            parms.Add(Database.MakeInParam("strPassword", sInfo.LogonPass));

            parms.Add(Database.MakeInParam("strResponse1", sInfo.Response1));
            parms.Add(Database.MakeInParam("strResponse2", sInfo.Response2));
            parms.Add(Database.MakeInParam("strResponse3", sInfo.Response3));

            parms.Add(Database.MakeInParam("strClientIP", sInfo.LastLogonIP));
            parms.Add(Database.MakeOutParam("strErrorDescribe", typeof(string), 127));

            return MessageHelper.GetMessage(Database, "WEB_RescindUserMoorMachine", parms);
        }

        /// <summary>
        /// 获取指定用户标识的用户的固定机器信息 [by userID]
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="logonPasswd"></param>
        /// <returns></returns>
        public Message GetUserMoorMachineByUserID(int userID, string logonPasswd)
        {
            List<DbParameter> parms = new List<DbParameter>();
            parms.Add(Database.MakeInParam("dwUserID", userID));
            parms.Add(Database.MakeInParam("strPassword", logonPasswd));

            parms.Add(Database.MakeOutParam("strErrorDescribe", typeof(string), 127));

            return MessageHelper.GetMessageForObject<AccountsProtect>(Database, "WEB_GetUserMoorMachineByUserID", parms);
        }

        #endregion 固定机器结束

        #region 帐号关闭

        /// <summary>
        /// 申请关闭帐号
        /// </summary>
        /// <param name="sInfo">密保信息</param>
        /// <returns></returns> 
        public Message ApplyUserStundown(AccountsProtect sInfo)
        {
            List<DbParameter> parms = new List<DbParameter>();
            parms.Add(Database.MakeInParam("dwUserID", sInfo.UserID));
            parms.Add(Database.MakeInParam("strPassword", sInfo.LogonPass));

            parms.Add(Database.MakeInParam("strResponse1", sInfo.Response1));
            parms.Add(Database.MakeInParam("strResponse2", sInfo.Response2));
            parms.Add(Database.MakeInParam("strResponse3", sInfo.Response3));

            parms.Add(Database.MakeInParam("strClientIP", sInfo.LastLogonIP));
            parms.Add(Database.MakeOutParam("strErrorDescribe", typeof(string), 127));

            return MessageHelper.GetMessage(Database, "WEB_ApplyUserStundown", parms);
        }

        /// <summary>
        /// 开通帐号
        /// </summary>
        /// <param name="sInfo">密保信息</param>
        /// <returns></returns> 
        public Message RescindUserStundown(AccountsProtect sInfo)
        {
            List<DbParameter> parms = new List<DbParameter>();
            parms.Add(Database.MakeInParam("dwUserID", sInfo.UserID));

            parms.Add(Database.MakeInParam("strResponse1", sInfo.Response1));
            parms.Add(Database.MakeInParam("strResponse2", sInfo.Response2));
            parms.Add(Database.MakeInParam("strResponse3", sInfo.Response3));

            parms.Add(Database.MakeInParam("strClientIP", sInfo.LastLogonIP));
            parms.Add(Database.MakeOutParam("strErrorDescribe", typeof(string), 127));

            return MessageHelper.GetMessage(Database, "WEB_RescindUserStundown", parms);
        }

        /// <summary>
        /// 获取指定用户标识的用户的关闭帐号信息 [by userID]
        /// </summary>
        /// <param name="userID">用户标识</param>
        /// <returns></returns>
        public Message GetUserStundownByUserID(int userID)
        {
            List<DbParameter> parms = new List<DbParameter>();
            parms.Add(Database.MakeInParam("dwUserID", userID));

            parms.Add(Database.MakeOutParam("strErrorDescribe", typeof(string), 127));

            return MessageHelper.GetMessageForObject<AccountsProtect>(Database, "WEB_GetUserStundownByUserID", parms);
        }

        /// <summary>
        /// 获取指定用户名的用户的关闭帐号信息 [by accounts]
        /// </summary>
        /// <param name="accounts">用户名</param>
        /// <returns></returns>
        public Message GetUserStundownByAccounts(string accounts)
        {
            List<DbParameter> parms = new List<DbParameter>();
            parms.Add(Database.MakeInParam("strAccounts", accounts));

            parms.Add(Database.MakeOutParam("strErrorDescribe", typeof(string), 127));

            return MessageHelper.GetMessageForObject<AccountsProtect>(Database, "WEB_GetUserStundownByAccounts", parms);
        }


        #endregion 帐号关闭

        #region 密码安全级别

        /// <summary>
        /// 获取指定用户标识的银行密码安全级别 [by userID]
        /// </summary>
        /// <param name="userID">用户标识</param>
        /// <param name="logonPass">登录密码</param>
        /// <returns></returns>
        public Message GetUserSecurityLevel(int userID, string logonPass)
        {
            List<DbParameter> parms = new List<DbParameter>();
            parms.Add(Database.MakeInParam("dwUserID", userID));
            parms.Add(Database.MakeInParam("strPassword", logonPass));

            parms.Add(Database.MakeOutParam("strErrorDescribe", typeof(string), 127));

            return MessageHelper.GetMessageForObject<AccountsProtect>(Database, "WEB_GetUserSecurityLevel", parms);
        }

        #endregion

        #endregion 安全相关

        #region 资料管理

        /// <summary>
        /// 修改登录帐号名
        /// </summary>
        /// <param name="user"></param>
        /// <param name="dstAccounts"></param>
        /// <param name="ip"></param>
        /// <returns></returns>
        public Message ModifyUserAccounts(UserTicketInfo user, string dstAccounts, string ip)
        {
            List<DbParameter> parms = new List<DbParameter>();
            parms.Add(Database.MakeInParam("dwUserID", user.UserID));
            parms.Add(Database.MakeInParam("strPassword", user.LogonPass));
            parms.Add(Database.MakeInParam("strDstAccounts", dstAccounts));

            parms.Add(Database.MakeInParam("strClientIP", ip));
            parms.Add(Database.MakeOutParam("strErrorDescribe", typeof(string), 127));

            return MessageHelper.GetMessage(Database, "WEB_ModifyUserAccounts", parms);
        }

        /// <summary>
        /// 修改玩家头像 [by user]
        /// </summary>
        /// <param name="user"></param>
        /// <param name="faceID"></param>
        /// <param name="ip"></param>
        /// <returns></returns>
        public Message ModifyUserface(UserTicketInfo user, int faceID, string ip)
        {
            List<DbParameter> parms = new List<DbParameter>();
            parms.Add(Database.MakeInParam("dwUserID", user.UserID));
            parms.Add(Database.MakeInParam("strPassword", user.LogonPass));
            parms.Add(Database.MakeInParam("wFaceID", faceID));

            parms.Add(Database.MakeInParam("strClientIP", ip));
            parms.Add(Database.MakeOutParam("strErrorDescribe", typeof(string), 127));

            return MessageHelper.GetMessage(Database, "WEB_ModifyUserface", parms);
        }

        /// <summary>
        /// 修改个性签名 [by user]
        /// </summary>
        /// <param name="user"></param>
        /// <param name="underwrite"></param>
        /// <param name="ip"></param>
        /// <returns></returns>
        public Message ModifyUnderwrite(UserTicketInfo user, string underwrite, string ip)
        {
            List<DbParameter> parms = new List<DbParameter>();
            parms.Add(Database.MakeInParam("dwUserID", user.UserID));
            parms.Add(Database.MakeInParam("strPassword", user.LogonPass));
            parms.Add(Database.MakeInParam("strUnderWrite", underwrite));

            parms.Add(Database.MakeInParam("strClientIP", ip));
            parms.Add(Database.MakeOutParam("strErrorDescribe", typeof(string), 127));

            return MessageHelper.GetMessage(Database, "WEB_ModifyUnderwrite", parms);
        }

        /// <summary>
        /// 更新个人资料
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Message ModifyUserIndividual(IndividualDatum user)
        {
            List<DbParameter> parms = new List<DbParameter>();
            parms.Add(Database.MakeInParam("dwUserID", user.UserID));
            parms.Add(Database.MakeInParam("dwGender", user.Gender));

            parms.Add(Database.MakeInParam("strUnderWrite", user.UnderWrite));

            parms.Add(Database.MakeInParam("strCompellation", user.Compellation));
            parms.Add(Database.MakeInParam("strQQ", user.QQ));
            parms.Add(Database.MakeInParam("strEmail", user.EMail));
            parms.Add(Database.MakeInParam("strSeatPhone", user.SeatPhone));
            parms.Add(Database.MakeInParam("strMobilePhone", user.MobilePhone));
            parms.Add(Database.MakeInParam("strDwellingPlace", user.DwellingPlace));
            parms.Add(Database.MakeInParam("strUserNote", user.UserNote));
            parms.Add(Database.MakeOutParam("strErrorDescribe", typeof(string), 127));

            return MessageHelper.GetMessage(Database, "NET_PW_ModifyUserInfo", parms);
        }

        #endregion 用户资料结束   
    
        
        #region 魅力兑换

        public Message UserConvertPresent(int userID, int present, int rate, string ip)
        {
            List<DbParameter> parms = new List<DbParameter>();
            parms.Add(Database.MakeInParam("dwUserID", userID));
            parms.Add(Database.MakeInParam("dwPresent", present));
            parms.Add(Database.MakeInParam("dwConvertRate", rate));
            parms.Add(Database.MakeInParam("strClientIP", ip));
            parms.Add(Database.MakeOutParam("strErrorDescribe", typeof(string), 127));

            return MessageHelper.GetMessage(Database, "NET_PW_ConvertPresent", parms);
        }

        /// <summary>
        /// 根据用户魅力排名(前10名)
        /// </summary>
        /// <returns></returns>
        public IList<UserInfo> GetUserInfoOrderByLoves()
        {
            StringBuilder sqlQuery = new StringBuilder();
            sqlQuery.Append("SELECT TOP 10 Accounts, GameID, LoveLiness, Present ")
                    .Append("FROM AccountsInfo ")
                    .Append("WHERE Nullity=0 ")
                    .Append(" ORDER By Present DESC,UserID ASC");
            return Database.ExecuteObjectList<UserInfo>(sqlQuery.ToString());
        }

        #endregion

        #region 奖牌兑换

        public Message UserConvertMedal(int userID, int medals, int rate, string ip)
        {
            List<DbParameter> parms = new List<DbParameter>();
            parms.Add(Database.MakeInParam("dwUserID", userID));
            parms.Add(Database.MakeInParam("dwMedals", medals));
            parms.Add(Database.MakeInParam("dwConvertRate", rate));
            parms.Add(Database.MakeInParam("strClientIP", ip));
            parms.Add(Database.MakeOutParam("strErrorDescribe", typeof(string), 127));

            return MessageHelper.GetMessage(Database, "NET_PW_ConvertMedal", parms);
        }

        #endregion

        #region 电子密保卡

        /// <summary>
        /// 检测密保卡序列号是否存在
        /// </summary>
        /// <param name="serialNumber">密保卡序列号</param>
        /// <returns></returns>
        public bool PasswordIDIsEnable(string serialNumber)
        {
            string sqlQuery = string.Format("SELECT UserID FROM AccountsInfo(NOLOCK) WHERE PasswordID='{0}'", serialNumber);
            AccountsInfo user = Database.ExecuteObject<AccountsInfo>(sqlQuery);
            if (user == null)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 检测用户是否绑定了密保卡
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="serialNumber">密保卡序列号</param>
        /// <returns></returns>
        public bool userIsBindPasswordCard(int userId)
        {
            string sqlQuery = string.Format("SELECT UserID FROM AccountsInfo(NOLOCK) WHERE PasswordID<>0 and userID={0}", userId);
            AccountsInfo user = Database.ExecuteObject<AccountsInfo>(sqlQuery);
            if (user == null)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 更新用户密保卡序列号
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="serialNumber">密保卡序列号</param>
        /// <returns></returns>
        public bool UpdateUserPasswordCardID(int userId, int serialNumber)
        {
            string sqlQuery = string.Format("UPDATE AccountsInfo SET PasswordID={0} WHERE UserID={1}", serialNumber, userId);
            int resutl = Database.ExecuteNonQuery(sqlQuery);
            if (resutl > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 取消密保卡绑定
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool ClearUserPasswordCardID(int userId)
        {
            string sqlQuery = string.Format("UPDATE AccountsInfo SET PasswordID=0 WHERE UserID={0}", userId);
            int resutl = Database.ExecuteNonQuery(sqlQuery);
            if (resutl > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 获取密保卡序列号
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        public string GetPasswordCardByUserID(int userId)
        {
            string sqlQuery = string.Format("SELECT PasswordID FROM AccountsInfo(NOLOCK) WHERE UserID={0}", userId);
            return Database.ExecuteScalarToStr(System.Data.CommandType.Text, sqlQuery);
        }
        #endregion

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
        /// 下载记录
        /// </summary>
        /// <param name="sqlQuery"></param>
        /// <returns></returns>
        public void DownsoftRecord(int nID, string strUrl, string strHost, string strIP)
        {
            List<DbParameter> prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("softid", nID));
            prams.Add(Database.MakeInParam("url", strUrl));
            prams.Add(Database.MakeInParam("host", strHost));
            prams.Add(Database.MakeInParam("ClientIP", strIP));

            MessageHelper.GetMessageForObject<UserInfo>(Database, "RecordDown", prams);
          
        }

      
        #endregion

        #region IAccountsDataProvider 成员


       
        #endregion
    }
}
