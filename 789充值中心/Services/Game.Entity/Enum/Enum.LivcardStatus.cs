using System;
using Game.Utils;

namespace Game.Entity
{
    /// <summary>
    /// 实卡充值方式
    /// </summary>
    [Serializable]
    [EnumDescription("充值方式")]
    public enum CardFilledMode
    { 
        /// <summary>
        /// 网管代充
        /// </summary>
        [EnumDescription("网管代充", 0)]
        Grant = 0,

        /// <summary>
        /// 实卡直充
        /// </summary>
        [EnumDescription("实卡直充", 1)]
        Myself = 1
    }

    /// <summary>
    /// 订单状态
    /// </summary>
    [Serializable]
    [EnumDescription("订单状态")]
    public enum OrderStatus:byte
    {
        //订单状态  0:申请订单;1:付款完成;2:附加服务;3:订单完成
        /// <summary>
        /// 申请订单(未付款)
        /// </summary>
        [EnumDescription("申请订单", 0)]
        Requisition = 0,

        /// <summary>
        /// 付款结束(订单队列)
        /// </summary>
        [EnumDescription("付款结束", 1)]
        PaymentOver = 1,

        /// <summary>
        /// 附加服务
        /// </summary>
        [EnumDescription("附加服务", 2)]
        Dispatch = 2,

        /// <summary>
        /// 订单结束
        /// </summary>
        [EnumDescription("订单结束", 3)]
        Success = 3        
    }

    /// <summary>
    /// 付款状态
    /// </summary>
    [Serializable]
    [EnumDescription("付款状态")]
    public enum PayStatus : byte
    {
        /// <summary>
        /// 未付款
        /// </summary>
        [EnumDescription("未付款", 0)]
        NonPayment = 0,

        /// <summary>
        /// 付款完成
        /// </summary>
        [EnumDescription("付款完成", 1)]
        Payment = 1,
    }   
}