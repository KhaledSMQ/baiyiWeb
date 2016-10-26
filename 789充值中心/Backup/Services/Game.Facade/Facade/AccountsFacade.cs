using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Game.Data.Factory;
using Game.IData;
using Game.Kernel;
using Game.Utils;
using Game.Entity.Accounts;
using System.Data;

namespace Game.Facade
{
    /// <summary>
    /// 用户外观
    /// </summary>
    public class AccountsFacade
    {
        #region Fields

        private IAccountsDataProvider accountsData;
        private ITreasureDataProvider treasureData;
        private IPlatformDataProvider platformData;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public AccountsFacade()
        {
            accountsData = ClassFactory.GetIAccountsDataProvider();
            treasureData = ClassFactory.GetITreasureDataProvider();
            platformData = ClassFactory.GetIPlatformDataProvider();
        }
        #endregion
        public int GetAccountsId(string UserName)
        {
            return accountsData.GetAccountsId(UserName);
        }
        /// <summary>
        /// 绑定电话
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="phone"></param>
        /// <returns></returns>
        public Message BingUserPhone(int userid, string phone)
        {

            Message umsg = accountsData.BingPhone(userid, phone);
            return umsg;
        }


        public Message BingUserEmail(int userid, string Email)
        {

            Message umsg = accountsData.BingEmail(userid, Email);
            return umsg;
        }

        #region 用户登录,注册,注销
        /// <summary>
        /// 用户登录
        /// </summary>
        public Message Logon(UserInfo user, bool isEncryptPasswd)
        {
            Message umsg;
            if (!isEncryptPasswd)
            {
                user.LogonPass = TextEncrypt.EncryptPassword(user.LogonPass);
            }

            umsg = accountsData.Login(user);
            return umsg;
        }

        /// <summary>
        /// 用户登录，登录密码必须是密文。并且验证登录参数
        /// </summary>
        /// <param name="stationID">站点标识</param>
        /// <param name="accounts">用户名</param>
        /// <param name="logonPasswd">密文密码</param>
        /// <param name="ip">登录地址</param>
        /// <returns>返回网站消息，若登录成功将携带用户对象</returns>
        public Message Logon(int stationID, string accounts, string logonPasswd)
        {
            UserInfo suInfo = new UserInfo(0, accounts, stationID, logonPasswd);
            suInfo.LastLogonIP = GameRequest.GetUserIP();

            return Logon(suInfo, false);
        }

        /// <summary>
        /// 注册新用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Message Register(UserInfo users, string parentAccount, int nType)
        {
            Message umsg = accountsData.Register(users, parentAccount, nType);
            return umsg;
        }

        /// <summary>
        /// 验证用户名
        /// </summary>
        /// <param name="accounts">用户名</param>
        /// <returns>返回的实例对象</returns>
        public Message ValidAccountsExists(string accounts)
        {
            Message umsg = accountsData.IsAccountsExist(accounts);
            return umsg;
        }

        #endregion

        #region 获取用户信息

        /// <summary>
        /// 根据二级域名名称获取用户账号
        /// </summary>
        /// <param name="slDomain"></param>
        /// <returns></returns>
        public string GetAccountsBySLDomain(string slDomain)
        {
            return accountsData.GetAccountsBySLDomain(slDomain);
        }

        /// <summary>
        /// 根据父级节点得到相对应的用户列表
        /// </summary>
        /// <param name="parentID"></param>
        /// <returns></returns>
        public System.Data.DataSet GetUsersByParentID(int parentID)
        {
            return accountsData.GetUsersByParentID(parentID);
        }

        /// <summary>
        /// 根据父级节点得到相对应的用户列表（分页）
        /// </summary>
        /// <param name="parentID"></param>
        /// <returns></returns>
        public PagerSet GetUsersByParentID(string whereQuery, int pageIndex, int pageSize)
        {
            return accountsData.GetUsersByParentID(whereQuery, pageIndex, pageSize);
        }

        /// <summary>
        /// 获取基本用户信息
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public UserInfo GetUserBaseInfoByUserID(int userID)
        {
            return accountsData.GetUserBaseInfoByUserID(userID);
        }

        /// <summary>
        /// 根据用户id获取基本用户信息
        /// </summary>
        /// <param name="userID">用户标识</param>
        /// <returns></returns>
        public UserInfo GetAccountInfoByUserID(int userID)
        {
            return accountsData.GetAccountInfoByUserID(userID);
        }

        /// <summary>
        /// 获取基本用户信息 by accounts
        /// </summary>
        /// <param name="accounts">用户帐号</param>
        /// <returns></returns>
        public UserInfo GetAccountInfoByAccounts(string accounts)
        {
            return accountsData.GetAccountInfoByAccounts(accounts);
        }

        /// <summary>
        /// 根据票证获取用户信息
        /// </summary>
        /// <param name="user">用户</param>     
        /// <returns>返回的对象实例</returns>
        public UserInfo GetUserInfo(UserTicketInfo user)
        {
            return accountsData.GetUserInfo(user);
        }

        /// <summary>
        /// 获取指定用户标识的用户个性资料(签名, 自定义头像) [by userID]
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns> 		
        public IndividualDatum GetUserContactInfoByUserID(int userID)
        {
            return accountsData.GetUserContactInfoByUserID(userID);

        }

        /// <summary>
        /// 账户是否存在
        /// </summary>
        /// <param name="accounts"></param>
        /// <returns></returns> 	
        public Message IsAccountsExist(string accounts)
        {
            return this.accountsData.IsAccountsExist(accounts);
        }

        public Message GetUserGlobalInfo(int userID, int gameID, string Accounts)
        {
            return this.accountsData.GetUserGlobalInfo(userID, gameID, Accounts);
        }

        #endregion

        #region	 密码管理

        /// <summary>
        /// 重置登录密码
        /// </summary>
        /// <param name="userID">玩家标识</param>
        /// <param name="password">新密码(密文)</param>
        /// <param name="response1">答案一</param>
        /// <param name="response2">答案二</param>
        /// <param name="response3">答案三</param>
        /// <param name="ip">连接地址</param>
        /// <returns></returns>
        public Message ResetLogonPasswd(int userID, string password, string response1, string response2, string response3, string ip)
        {
            AccountsProtect sInfo = new AccountsProtect();
            sInfo.UserID = userID;
            sInfo.LogonPass = password;
            sInfo.Response1 = response1;
            sInfo.Response2 = response2;
            sInfo.Response3 = response3;
            sInfo.LastLogonIP = ip == "" || ip == null ? GameRequest.GetUserIP() : ip;

            return ResetLogonPasswd(sInfo);
        }

        /// <summary>
        /// 重置登录密码
        /// </summary>
        /// <param name="sInfo">密保信息</param>       
        /// <returns></returns>
        public Message ResetLogonPasswd(AccountsProtect sInfo)
        {
            return accountsData.ResetLogonPasswd(sInfo);
        }

        /// <summary>
        /// 重置银行密码
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="logonPass">登录密码</param>
        /// <param name="insurePass">新设密码</param>
        /// <param name="response1">答案一</param>
        /// <param name="response2">答案二</param>
        /// <param name="response3">答案三</param>
        /// <param name="ip">连接地址</param>
        /// <returns></returns>
        public Message ResetInsurePasswd(int userID, string logonPass, string insurePass
            , string response1, string response2, string response3, string ip)
        {
            AccountsProtect sInfo = new AccountsProtect();
            sInfo.UserID = userID;
            sInfo.LogonPass = logonPass;
            sInfo.InsurePass = insurePass;
            sInfo.Response1 = response1;
            sInfo.Response2 = response2;
            sInfo.Response3 = response3;
            sInfo.LastLogonIP = ip == "" || ip == null ? GameRequest.GetUserIP() : ip;

            return ResetInsurePasswd(sInfo);
        }

        /// <summary>
        /// 重置银行密码
        /// </summary>
        /// <param name="sInfo">密保信息</param>      
        /// <returns></returns>
        public Message ResetInsurePasswd(AccountsProtect sInfo)
        {
            return accountsData.ResetInsurePasswd(sInfo);
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
            return accountsData.ModifyLogonPasswd(userID, srcPassword, dstPassword, ip);
        }

        /// <summary>
        /// 修改银行密码
        /// </summary>
        /// <param name="userID">用户标识</param>
        /// <param name="logonPasswd">登录密码</param>
        /// <param name="srcPassword">登录密码</param>
        /// <param name="dstPassword">老密码</param>
        /// <param name="ip"></param>
        /// <returns></returns>
        public Message ModifyInsurePasswd(int userID, string srcPassword, string dstPassword, string ip)
        {
            return accountsData.ModifyInsurePasswd(userID, srcPassword, dstPassword, ip);
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
            return accountsData.ApplyUserSecurity(sInfo);
        }

        /// <summary>
        /// 修改帐号保护
        /// </summary>
        /// <param name="oldInfo">旧密保信息</param>
        /// <param name="newInfo">新密保信息</param>
        /// <returns></returns>
        public Message ModifyUserSecurity(AccountsProtect oldInfo, AccountsProtect newInfo)
        {
            return accountsData.ModifyUserSecurity(oldInfo, newInfo);
        }

        /// <summary>
        /// 验证密保信息 (Respose 答案)
        /// </summary>
        /// <param name="userID">用户标识</param>
        /// <param name="response1">答案一</param>
        /// <param name="response2">答案二</param>
        /// <param name="response3">答案三</param>
        /// <returns></returns>
        public Message ValidUserSecurityByResponse(int userID, string response1, string response2, string response3)
        {
            AccountsProtect sInfo = new AccountsProtect();
            sInfo.UserID = userID;
            sInfo.Response1 = response1;
            sInfo.Response2 = response2;
            sInfo.Response3 = response3;

            return ValidUserSecurityByResponse(sInfo);
        }

        /// <summary>
        /// 验证密保信息 (Respose 答案)
        /// </summary>
        /// <param name="sInfo">密保对象</param>
        /// <returns>会员消息对象</returns>
        public Message ValidUserSecurityByResponse(AccountsProtect sInfo)
        {
            return accountsData.ValidUserSecurityByResponse(sInfo);
        }

        /// <summary>
        /// 验证密码保护 (Passport 证件号码)
        /// </summary>
        /// <param name="userID">玩家标识</param>
        /// <param name="response1">答案一</param>
        /// <param name="response2">答案二</param>
        /// <param name="response3">答案三</param>
        /// <param name="passportID"></param>
        /// <returns>会员消息对象</returns>
        public Message ValidUserSecurityPassport(int userID
            , string response1, string response2, string response3, string passportID)
        {
            AccountsProtect sInfo = new AccountsProtect();
            sInfo.UserID = userID;
            sInfo.Response1 = response1;
            sInfo.Response2 = response2;
            sInfo.Response3 = response3;
            sInfo.PassportID = passportID;

            return accountsData.ValidUserSecurityPassport(sInfo);
        }

        /// <summary>
        /// 验证密码保护 (Passport 证件号码)
        /// </summary>
        /// <param name="sInfo">密保对象</param>
        /// <returns>会员消息对象</returns>
        public Message ValidUserSecurityPassport(AccountsProtect sInfo)
        {
            return accountsData.ValidUserSecurityPassport(sInfo);
        }

        /// <summary>
        /// 获取密保信息 (userID)
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public Message GetUserSecurityByUserID(int userID)
        {
            return accountsData.GetUserSecurityByUserID(userID);
        }

        /// <summary>
        /// 获取密保信息 (Accounts)
        /// </summary>
        /// <param name="accounts">用户帐号</param>
        /// <returns></returns>
        public Message GetUserSecurityByAccounts(string accounts)
        {
            return accountsData.GetUserSecurityByAccounts(accounts);
        }

        public Message ConfirmUserSecurity(AccountsProtect info)
        {
            return accountsData.ConfirmUserSecurity(info);
        }

        public int GetUserIDByNickName(string nickName)
        {
            return this.accountsData.GetUserIDByNickName(nickName);
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
            return accountsData.ApplyUserMoorMachine(sInfo);
        }

        /// <summary>
        /// 解除机器绑定
        /// </summary>
        /// <param name="sInfo">密保信息</param>
        /// <returns></returns>
        public Message RescindUserMoorMachine(AccountsProtect sInfo)
        {
            return accountsData.RescindUserMoorMachine(sInfo);
        }

        /// <summary>
        /// 获取指定用户标识的用户的固定机器信息 [by userID]
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="logonPasswd"></param>
        /// <returns></returns>
        public Message GetUserMoorMachineByUserID(int userID, string logonPasswd)
        {
            return accountsData.GetUserMoorMachineByUserID(userID, logonPasswd);
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
            return accountsData.ApplyUserStundown(sInfo);
        }

        /// <summary>
        /// 开通帐号
        /// </summary>
        /// <param name="sInfo">密保信息</param>
        /// <returns></returns> 
        public Message RescindUserStundown(AccountsProtect sInfo)
        {
            return accountsData.RescindUserStundown(sInfo);
        }

        /// <summary>
        /// 获取指定用户标识的用户的关闭帐号信息 [by userID]
        /// </summary>
        /// <param name="userID">用户标识</param>
        /// <returns></returns>
        public Message GetUserStundownByUserID(int userID)
        {
            return accountsData.GetUserStundownByUserID(userID);
        }

        /// <summary>
        /// 获取指定用户名的用户的关闭帐号信息 [by accounts]
        /// </summary>
        /// <param name="accounts">用户名</param>
        /// <returns></returns>
        public Message GetUserStundownByAccounts(string accounts)
        {
            return accountsData.GetUserStundownByAccounts(accounts);
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
            return accountsData.GetUserSecurityLevel(userID, logonPass);
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
            ip = ip == "" || ip == null ? GameRequest.GetUserIP() : ip;
            return accountsData.ModifyUserAccounts(user, dstAccounts, ip);
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
            ip = ip == "" || ip == null ? GameRequest.GetUserIP() : ip;
            return accountsData.ModifyUserface(user, faceID, ip);
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
            ip = ip == "" || ip == null ? GameRequest.GetUserIP() : ip;
            return accountsData.ModifyUnderwrite(user, underwrite, ip);
        }

        /// <summary>
        /// 更新个人资料
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Message ModifyUserIndividual(IndividualDatum user)
        {
            return accountsData.ModifyUserIndividual(user);
        }

        #endregion 用户资料结束

        #region 密保卡

        /// <summary>
        /// 检测密保卡序列号是否存在
        /// </summary>
        /// <param name="serialNumber">密保卡序列号</param>
        /// <returns></returns>
        public bool PasswordIDIsEnable(string serialNumber)
        {
            return accountsData.PasswordIDIsEnable(serialNumber);
        }

        /// <summary>
        /// 检测用户是否绑定了密保卡
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="serialNumber">密保卡序列号</param>
        /// <returns></returns>
        public bool userIsBindPasswordCard(int userId)
        {
            return accountsData.userIsBindPasswordCard(userId);
        }


        /// <summary>
        /// 更新用户密保卡序列号
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="serialNumber">密保卡序列号</param>
        /// <returns></returns>
        public bool UpdateUserPasswordCardID(int userId, int serialNumber)
        {
            return accountsData.UpdateUserPasswordCardID(userId, serialNumber);
        }

        /// <summary>
        /// 取消密保卡绑定
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool ClearUserPasswordCardID(int userId)
        {
            return accountsData.ClearUserPasswordCardID(userId);
        }

        /// <summary>
        /// 获取密保卡序列号
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        public string GetPasswordCardByUserID(int userId)
        {
            return accountsData.GetPasswordCardByUserID(userId);
        }
        #endregion

        #region 魅力兑换

        public Message UserConvertPresent(int userID, int present, int rate, string ip)
        {
            return accountsData.UserConvertPresent(userID, present, rate, ip);
        }

        /// <summary>
        /// 根据用户魅力排名(前10名)
        /// </summary>
        /// <returns></returns>
        public IList<UserInfo> GetUserInfoOrderByLoves()
        {
            return accountsData.GetUserInfoOrderByLoves();
        }

        #endregion

        #region 奖牌兑换

        public Message UserConvertMedal(int userID, int medals, int rate, string ip)
        {
            return accountsData.UserConvertMedal(userID, medals, rate, ip);
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
            return accountsData.GetObjectBySql(sqlQuery);
        }

        public void DownsoftRecord(int nID, string strUrl, string strHost, string strIP)
        {
            accountsData.DownsoftRecord(nID, strUrl, strHost, strIP);
        }
        public void InsertAdvKeywords(int soruceid, string url, string keycode, string name)
        {

            accountsData.InsertAdvKeywords(soruceid, url, keycode, name);
        }
        public void InsertAdsKeywordsRecored(int sourceType, string keywords, string sourceurl, string searchIP)
        {
            accountsData.InsertAdsKeywordsRecored(sourceType, keywords, sourceurl, searchIP);
        }
        public void InsertAdvKeyWordsRegRecord(int sourceType, string keywords, string serchIP, int GameID)
        {
            accountsData.InsertAdvKeyWordsRegRecord(sourceType, keywords, serchIP, GameID);
        }
        public int GetGameIDByUserName(string userName)
        {
            return accountsData.GetGameIDByUserName(userName);
        }
        #endregion
    }
}
