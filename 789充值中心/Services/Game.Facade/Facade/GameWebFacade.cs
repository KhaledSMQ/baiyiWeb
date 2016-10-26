using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Game.Data.Factory;
using Game.IData;
using Game.Entity;
using Game.Entity.GameWeb;
using Game.Kernel;
using Game.Utils;
using System.Data;

namespace Game.Facade
{
    /// <summary>
    /// 网站外观
    /// </summary>
    public class GameWebFacade
    {
        #region Fields

        private const int NEWS_TOP_MAX = 50;                                            //摘要列表
        private const int NEWS_TOP_MIN = 10;                                            //摘要列表
        private const int NEWS_PAGESIZE = 15;                                           //新闻列表每页数据数量
        private INativeWebDataProvider gamewebData;                                       //网站信息

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public GameWebFacade()
        {
            gamewebData = ClassFactory.GetINativeWebDataProvider();;
        }
        #endregion

        #region 网站新闻

        /// <summary>
        /// 获取新闻摘要列表
        /// </summary>
        /// <param name="newsType">新闻类别 0 全部,1 新闻,2 公告</param>
        /// <param name="hot">热点 1, 普通 0</param>
        /// <param name="elite">精华 1,普通 0</param>
        /// <param name="top">列表数目 最大值 50 条</param>
        /// <returns></returns>
        public IList<News> GetTopNewsList(int newsType, int hot, int elite, int top)
        {
            //参数检查
            if (top > NEWS_TOP_MAX)
            {
                top = NEWS_TOP_MAX;
            }
            else if (top <= 0)
            {
                top = NEWS_TOP_MIN;
            }

            if (newsType > 2 || newsType < 0)
            {
                newsType = 0;
            }

            if (hot > 1 || hot < 0)
            {
                hot = 0;
            }

            if (elite > 1 || elite < 0)
            {
                elite = 0;
            }

            NewsTypeStatus newsTypeStatus = (NewsTypeStatus)newsType;
            IList<News> newsTopList = gamewebData.GetTopNewsList(newsTypeStatus, hot, elite, top);
            return newsTopList;
        }

        /// <summary>
        /// 获取新闻列表
        /// </summary>
        /// <returns></returns>
        public IList<News> GetNewsList()
        {
            return gamewebData.GetNewsList();
        }

        /// <summary>
        /// 获取分页新闻列表
        /// </summary>
        /// <param name="pageIndex">当前要显示的页码</param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public PagerSet GetNewsList(int pageIndex, int pageSize)
        {
            if (pageSize <= 0)
                pageSize = GameWebFacade.NEWS_PAGESIZE;

            return gamewebData.GetNewsList(pageIndex, pageSize);
        }

        /// <summary>
        /// 获取新闻By NewsID
        /// </summary>
        /// <param name="newsID"></param>
        /// <param name="mode">模式选择, 0=当前主题, 1=上一主题, 2=下一主题</param>
        /// <returns></returns>
        public News GetNewsByNewsID(int newsID, byte mode)
        {
            return gamewebData.GetNewsByNewsID(newsID, mode);
        }

        /// <summary>
        /// 获取新闻类别
        /// </summary>
        /// <param name="newsType"></param>
        /// <returns></returns>
        public string GetNewsType(int newsType)
        {
            return EnumDescription.GetFieldText((NewsTypeStatus)newsType);
        }

        /// <summary>
        /// 获取公告列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetNoticeList()
        {
            return gamewebData.GetNoticeList();
        }

        /// <summary>
        /// 获取公告
        /// </summary>
        /// <param name="noticeID"></param>
        /// <returns></returns>
        public Notice GetNotice(int noticeID)
        {
            return gamewebData.GetNotice(noticeID);
        }

        /// <summary>
        /// 获取有效公告
        /// </summary>
        /// <returns></returns>
        public Notice GetNoticeByChannelID(int channelID)
        {
            return gamewebData.GetNoticeByChannelID(channelID);
        }

        /// <summary>
        /// 得到公告列表
        /// </summary>
        /// <returns></returns>
        public IList<Notice> GetNoticeList(int channelID)
        {
            return gamewebData.GetNoticeList(channelID);
        }

        #endregion

        #region 网站问题

        public IList<GameIssueInfo> GetTopIssueList(int top)
        {
            //参数检查
            if (top > NEWS_TOP_MAX)
            {
                top = NEWS_TOP_MAX;
            }
            else if (top <= 0)
            {
                top = NEWS_TOP_MIN;
            }

            IList<GameIssueInfo> issueTopList = gamewebData.GetTopIssueList(top);
            return issueTopList;
        }

        /// <summary>
        /// 获取问题列表
        /// </summary>
        /// <returns></returns>
        public IList<GameIssueInfo> GetIssueList()
        {
            return gamewebData.GetIssueList();
        }

        /// <summary>
        /// 获取分页问题列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public PagerSet GetIssueList(int pageIndex, int pageSize)
        {
            return gamewebData.GetIssueList(pageIndex, pageSize);
        }

        /// <summary>
        /// 获取问题实体
        /// </summary>
        /// <param name="issueID"></param>
        /// <param name="mode">模式选择, 0=当前主题, 1=上一主题, 2=下一主题</param>
        /// <returns></returns>
        public GameIssueInfo GetIssueByIssueID(int issueID, byte mode)
        {
            return gamewebData.GetIssueByIssueID(issueID, mode);
        }
        #endregion


        #region 反馈意见

        /// <summary>
        /// 获取分页反馈意见列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public PagerSet GetFeedbacklist(int pageIndex, int pageSize)
        {
            return gamewebData.GetFeedbacklist(pageIndex, pageSize);
        }

        /// <summary>
        /// 获取反馈意见实体
        /// </summary>
        /// <param name="issueID"></param>
        /// <param name="mode">模式选择, 0=当前主题, 1=上一主题, 2=下一主题</param>
        /// <returns></returns>
        public GameFeedbackInfo GetGameFeedBackInfo(int feedID, byte mode)
        {
            return gamewebData.GetGameFeedBackInfo(feedID, mode);
        }

        /// <summary>
        /// 更新浏览量
        /// </summary>
        /// <param name="feedID"></param>
        public void UpdateFeedbackViewCount(int feedID)
        {
            gamewebData.UpdateFeedbackViewCount(feedID);
        }

        /// <summary>
        /// 发表留言
        /// </summary>
        /// <returns></returns>
        public Message PublishFeedback(GameFeedbackInfo info)
        {
            return gamewebData.PublishFeedback(info);
        }

        #endregion

        #region 游戏帮助数据

        /// <summary>
        /// 获取游戏详细列表
        /// </summary>
        /// <returns></returns>
        public IList<GameRulesInfo> GetGameHelps(int top)
        {
            return gamewebData.GetGameHelps(top);
        }

        /// <summary>
        /// 获取游戏详细信息
        /// </summary>
        /// <param name="kindID"></param>
        /// <returns></returns>
        public GameRulesInfo GetGameHelpForIndex(int kindID)
        {
            return gamewebData.GetGameHelpForIndex(kindID);
        }

        /// <summary>
        /// 获取游戏详细信息
        /// </summary>
        /// <param name="kindID"></param>
        /// <returns></returns>
        public GameRulesInfo GetGameHelpForJoin(int kindID)
        {
            return gamewebData.GetGameHelpForJoin(kindID);
        }

        #endregion
    }
}
