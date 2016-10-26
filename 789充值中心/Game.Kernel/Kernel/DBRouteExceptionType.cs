namespace Game.Kernel
{
    using Game.Utils;
    using System;

    [Serializable, EnumDescription("集群路由异常")]
    public enum DBRouteExceptionType
    {
        [EnumDescription("调用方IP地址受限", 0x65)]
        AddressEnjoin = 0x65,
        [EnumDescription("调用方IP地址授权已过期", 0x68)]
        AddressInvalidDate = 0x68,
        [EnumDescription("调用方机器受限", 100)]
        MachineEnjoin = 100,
        [EnumDescription("站点授权已被终止", 0x67)]
        StationEnjoin = 0x67,
        [EnumDescription("站点标识错误", 0x66)]
        StationIDError = 0x66
    }
}

