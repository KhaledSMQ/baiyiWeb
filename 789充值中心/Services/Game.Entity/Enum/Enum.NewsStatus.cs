using System;
using Game.Utils;

namespace Game.Entity
{
    /// <summary>
    ///   新闻状态枚举类型
    /// </summary>
    [Serializable]
    [EnumDescription("新闻状态类型")]
    public enum NewsStatus : byte
    {
        /// <summary>
        /// 置顶
        /// </summary>
        [EnumDescription("置顶",0)]
        OnTop = 0,

        /// <summary>
        /// 总置顶
        /// </summary>
        [EnumDescription("总置顶", 1)]
        OnTopAll = 1,

        /// <summary>
        /// 精华
        /// </summary>
        [EnumDescription("精华",2)]
        IsElite = 2,

        /// <summary>
        /// 热点
        /// </summary>
        [EnumDescription("热点",3)]
        IsHot  = 3,

        /// <summary>
        /// 删除
        /// </summary>
        [EnumDescription("删除", 4)]
        IsDelete = 4,

        /// <summary>
        /// 锁定
        /// </summary>
        [EnumDescription("锁定", 5)]
        IsLock = 5,

        /// <summary>
        /// 链接
        /// </summary>
        [EnumDescription("链接", 6)]
        IsLink = 6 
    }

    /// <summary>
    /// 新闻类别
    /// </summary>
    [Serializable]
    [EnumDescription("新闻类别")]
    public enum NewsTypeStatus:byte
    { 
        /// <summary>
        /// 错误类别
        /// </summary>
        [EnumDescription("错误类别",0)]
        NotSet=0,

        /// <summary>
        /// 新闻
        /// </summary>
        [EnumDescription("新闻",1)]
        News=1,

        /// <summary>
        /// 公告
        /// </summary>
        [EnumDescription("公告")]
        Affiche=2,

        /// <summary>
        /// 维护
        /// </summary>
        [EnumDescription("维护")]
        Events = 3
    }
}
