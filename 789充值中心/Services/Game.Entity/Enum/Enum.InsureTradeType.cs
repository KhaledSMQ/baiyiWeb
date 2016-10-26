using System;
using Game.Utils;

namespace Game.Entity
{
    /// <summary>
    /// 银行交易类别
    /// </summary>
    [Serializable]
    [EnumDescription("银行交易类别")]
    public enum InsureTradeType : int
    {
        /// <summary>
        /// 未知
        /// </summary>
        [EnumDescription("未知", 0)]
        Unknown = 0,
        /// <summary>
        /// 存款
        /// </summary>
        [EnumDescription("存款", 1)]
        In = 1,

        /// <summary>
        /// 取款
        /// </summary>
        [EnumDescription("取款", 2)]
        Out = 2,

        /// <summary>
        /// 转账(转出)
        /// </summary>
        [EnumDescription("转出", 3)]
        TransferOut = 3,

        /// <summary>
        /// 转账(转入)
        /// </summary>
        [EnumDescription("转入", 4)]
        TransferIn = 4
    }
}
