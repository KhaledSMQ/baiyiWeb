namespace Game.Kernel
{
    using Game.Utils;
    using System;

    [Serializable, Flags, EnumDescription("授权类型")]
    public enum AccreditType
    {
        [EnumDescription("主站模式", 0)]
        MainStation = 0,
        [EnumDescription("租用模式", 2)]
        Singleness = 2,
        [EnumDescription("分站模式", 1)]
        Substation = 1,
        [EnumDescription("未授权", 4)]
        Unauthorized = 4
    }
}

