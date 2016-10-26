namespace Game.Kernel
{
    using Game.Utils;
    using System;

    [Serializable, EnumDescription("授权异常")]
    public enum StationPermExceptionType
    {
        [EnumDescription("授权码已经停用", 0xc9)]
        AccreditIDStopped = 0xc9,
        [EnumDescription("无效的授权码", 200)]
        InvokeIllegal = 200,
        [EnumDescription("签名验证失败", -2)]
        SignatureFailure = -2,
        [EnumDescription("授权正确", 0)]
        Success = 0,
        [EnumDescription("未知错误", -1)]
        UnknowError = -1
    }
}

