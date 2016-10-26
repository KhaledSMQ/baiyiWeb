using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Game.Kernel;
using Game.Entity.Accounts;
using System.Data;

namespace Game.IData
{
    /// <summary>
    /// 帐号库数据层接口
    /// </summary>
    public interface IAccountsDataProvider : IProvider
    {
        #region 用户登录、注册

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="names">用户信息</param>
        /// <returns></returns>
        Message Login(UserInfo user);

        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Message Register(UserInfo user, string parentAccount, int nType);

        /// <summary>
        /// 判断用户名是否存在
        /// </summary>
        /// <param name="accounts"></param>
        /// <returns></returns>
        Message IsAccountsExist(string accounts);

      
        #endregion

        #region 获到用户信息

        /// <summary>
        /// 根据二级域名名称获取用户账号
        /// </summary>
        /// <param name="slDomain"></param>
        /// <returns></returns>
        string GetAccountsBySLDomain(string slDomain);

        /// <summary>
        /// 根据父级节点得到相对应的用户列表
        /// </summary>
        /// <param name="parentID"></param>
        /// <returns></returns>
        System.Data.DataSet GetUsersByParentID(int parentID);

        /// <summary>
        /// 根据父级节点得到相对应的用户列表(分页)
        /// </summary>
        /// <param name="parentID"></param>
        /// <returns></returns>
        PagerSet GetUsersByParentID(string whereQuery, int pageIndex, int pageSize);

        /// <summary>
        /// 获取基本用户信息
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        UserInfo GetUserBaseInfoByUserID(int userID);

        /// <summary>
        /// 根据用户id获取基本用户信息
        /// </summary>
        /// <param name="userID">用户标识</param>
        /// <returns></returns>
        UserInfo GetAccountInfoByUserID(int userID);

        /// <summary>
        /// 获取基本用户信息 by accounts
        /// </summary>
        /// <param name="accounts">用户帐号</param>
        /// <returns></returns>
        UserInfo GetAccountInfoByAccounts(string accounts);

        /// <summary>
        /// 根据票证获取用户信息
        /// </summary>
        /// <param name="user">用户</param>     
        /// <returns>返回 的对象实例</returns>
        UserInfo GetUserInfo(UserTicketInfo user);

        /// <summary>
        /// 获取指定用户标识的用户个性资料(签名, 自定义头像) [by user]
        /// </summary>
        /// <param name="user">用户</param>
        /// <returns></returns> 		
        IndividualDatum GetUserContactInfoByUserID(int userID);

        /// <summary>
        /// 获得用户信息
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="gameID">游戏ID</param>
        /// <param name="Accounts">用户名</param>
        /// <returns></returns> 		
        Message GetUserGlobalInfo(int userID, int gameID, string Accounts);

        int GetUserIDByNickName(string nickName);
        int GetGameIDByUserName(string userName);

        #endregion
        Message BingPhone(int UserID, string Phone);


        Message BingEmail(int UserID, string Email);
//         #region 添加账号
// 
//         /// <summary>
//         /// 添加账号
//         /// </summary>
//         /// <param name="user"></param>
//         /// <returns></returns>
//         Message AddAccounts(UserInfo user);

//        #endregion
        int GetAccountsId(string UserName);
        #region	 密码管理

        /// <summary>
        /// 重置登录密码
        /// </summary>
        /// <param name="sInfo">密保信息</param>       
        /// <returns></returns>
        Message ResetLogonPasswd(AccountsProtect sInfo);

        /// <summary>
        /// 重置银行密码
        /// </summary>
        /// <param name="sInfo">密保信息</param>      
        /// <returns></returns>
        Message ResetInsurePasswd(AccountsProtect sInfo);

        /// <summary>
        /// 修改登录密码
        /// </summary>
        /// <param name="userID">玩家标识</param>
        /// <param name="srcPassword">旧密码</param>
        /// <param name="dstPassword">新密码</param>
        /// <param name="ip">连接地址</param>
        /// <returns></returns>
        Message ModifyLogonPasswd(int userID, string srcPassword, string dstPassword, string ip);


        /// <summary>
        /// 修改银行密码
        /// </summary>
        /// <param name="userID">用户标识</param>
        /// <param name="srcPassword">登录密码</param>
        /// <param name="dstPassword">老密码</param>
        /// <param name="ip"></param>
        /// <returns></returns>
        Message ModifyInsurePasswd(int userID, string srcPassword, string dstPassword, string ip);


        #endregion 密码管理

        #region  密码保护管理

        /// <summary>
        /// 申请帐号保护
        /// </summary>
        /// <param name="sInfo">密保信息</param>
        /// <returns></returns>
        Message ApplyUserSecurity(AccountsProtect sInfo);

        /// <summary>
        /// 修改帐号保护
        /// </summary>
        /// <param name="oldInfo">旧密保信息</param>
        /// <param name="newInfo">新密保信息</param>
        /// <returns></returns>
        Message ModifyUserSecurity(AccountsProtect oldInfo, AccountsProtect newInfo);

        /// <summary>
        /// 验证密保信息 (Respose 答案)
        /// </summary>
        /// <param name="sInfo">密保对象</param>
        /// <returns>会员消息对象</returns>
        Message ValidUserSecurityByResponse(AccountsProtect sInfo);

        /// <summary>
        /// 验证密码保护 (Passport 证件号码)
        /// </summary>
        /// <param name="sInfo">密保对象</param>
        /// <returns>会员消息对象</returns>
        Message ValidUserSecurityPassport(AccountsProtect sInfo);

        /// <summary>
        /// 获取密保信息 (userID)
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        Message GetUserSecurityByUserID(int userID);

        /// <summary>
        /// 获取密保信息 (Accounts)
        /// </summary>
        /// <param name="accounts">用户帐号</param>
        /// <returns></returns>
        Message GetUserSecurityByAccounts(string accounts);

        /// <summary>
        /// 密保确认
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        Message ConfirmUserSecurity(AccountsProtect info);

        #endregion 保护管理

        #region 安全管理

        #region 固定机器

        /// <summary>
        /// 申请机器绑定
        /// </summary>
        /// <param name="sInfo">密保信息</param>
        /// <returns></returns>
        Message ApplyUserMoorMachine(AccountsProtect sInfo);

        /// <summary>
        /// 解除机器绑定
        /// </summary>
        /// <param name="sInfo">密保信息</param>
        /// <returns></returns>
        Message RescindUserMoorMachine(AccountsProtect sInfo);

        /// <summary>
        /// 获取指定用户标识的用户的固定机器信息 [by userID]
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="logonPasswd"></param>
        /// <returns></returns>
        Message GetUserMoorMachineByUserID(int userID, string logonPasswd);

        #endregion 固定机器结束

        #region 帐号关闭

        /// <summary>
        /// 申请关闭帐号
        /// </summary>
        /// <param name="sInfo">密保信息</param>
        /// <returns></returns> 
        Message ApplyUserStundown(AccountsProtect sInfo);

        /// <summary>
        /// 开通帐号
        /// </summary>
        /// <param name="sInfo">密保信息</param>
        /// <returns></returns> 
        Message RescindUserStundown(AccountsProtect sInfo);

        /// <summary>
        /// 获取指定用户标识的用户的关闭帐号信息 [by userID]
        /// </summary>
        /// <param name="userID">用户标识</param>
        /// <returns></returns>
        Message GetUserStundownByUserID(int userID);

        /// <summary>
        /// 获取指定用户名的用户的关闭帐号信息 [by accounts]
        /// </summary>
        /// <param name="accounts">用户名</param>
        /// <returns></returns>
        Message GetUserStundownByAccounts(string accounts);


        #endregion 帐号关闭

        #region 密码安全级别

        /// <summary>
        /// 获取指定用户标识的银行密码安全级别 [by userID]
        /// </summary>
        /// <param name="userID">用户标识</param>
        /// <param name="logonPass">登录密码</param>
        /// <returns></returns>
        Message GetUserSecurityLevel(int userID, string logonPass);

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
        Message ModifyUserAccounts(UserTicketInfo user, string dstAccounts, string ip);

        /// <summary>
        /// 修改玩家头像 [by user]
        /// </summary>
        /// <param name="user"></param>
        /// <param name="faceID"></param>
        /// <param name="ip"></param>
        /// <returns></returns>
        Message ModifyUserface(UserTicketInfo user, int faceID, string ip);

        /// <summary>
        /// 修改个性签名 [by user]
        /// </summary>
        /// <param name="user"></param>
        /// <param name="underwrite"></param>
        /// <param name="ip"></param>
        /// <returns></returns>
        Message ModifyUnderwrite(UserTicketInfo user, string underwrite, string ip);

        /// <summary>
        /// 更新个人资料
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Message ModifyUserIndividual(IndividualDatum user);

        #endregion 用户资料结束       

        #region 密保卡信息

        /// <summary>
        /// 检测密保卡序列号是否存在
        /// </summary>
        /// <param name="serialNumber">密保卡序列号</param>
        /// <returns></returns>
        bool PasswordIDIsEnable(string serialNumber);

        /// <summary>
        /// 检测用户是否绑定了密保卡
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="serialNumber">密保卡序列号</param>
        /// <returns></returns>
        bool userIsBindPasswordCard(int userId);

        /// <summary>
        /// 更新用户密保卡序列号
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="serialNumber">密保卡序列号</param>
        /// <returns></returns>
        bool UpdateUserPasswordCardID(int userId, int serialNumber);

        /// <summary>
        /// 取消密保卡绑定
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        bool ClearUserPasswordCardID(int userId);

        /// <summary>
        /// 获取密保卡序列号
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        string GetPasswordCardByUserID(int userId);
        #endregion

        #region 魅力兑换

        Message UserConvertPresent(int userID, int medals, int rate, string ip);

        /// <summary>
        /// 根据用户魅力排名(前10名)
        /// </summary>
        /// <returns></returns>
        IList<UserInfo> GetUserInfoOrderByLoves();

        #endregion

        #region 奖牌兑换

        Message UserConvertMedal(int userID, int medals, int rate, string ip);

        #endregion

        #region 公共

        /// <summary>
        /// 根据SQL语句查询一个值
        /// </summary>
        /// <param name="sqlQuery"></param>
        /// <returns></returns>
        object GetObjectBySql(string sqlQuery);

        void DownsoftRecord(int nID, string strUrl, string strHost, string strIP);

        void  InsertAdvKeywords(int soruceid, string url, string keycode,string name);
        void InsertAdsKeywordsRecored(int sourceType,string keywords,string sourceurl,string searchIP);
        void InsertAdvKeyWordsRegRecord(int sourceType,string keywords,string serchIP,int GameID);
        #endregion
    }
}
