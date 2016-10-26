namespace Game.Kernel
{
    using Game.Utils;
    using System;

    [EnumDescription("服务器线路")]
    public enum DBLineType
    {
        [EnumDescription("电信", 1)]
        DX = 1,
        [EnumDescription("本地", 0)]
        Local = 0,
        [EnumDescription("铁通", 4)]
        TT = 4,
        [EnumDescription("网通", 2)]
        WT = 2
    }
}

