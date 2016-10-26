namespace Game.Kernel
{
    using Game.Utils;
    using System;

    [Serializable, EnumDescription("客户网站建立连接的通信协议")]
    public enum ConnectionProtocol
    {
        [EnumDescription("本地连接")]
        LocationSqlServer = 1,
        [EnumDescription("远程连接")]
        RemotingSqlServer = 2,
        [EnumDescription("未知协议")]
        UnKnowProtocol = 0,
        [EnumDescription("WCF")]
        WCF = 4,
        [EnumDescription("Web 服务")]
        WebService = 3
    }
}

