namespace Game.Utils
{
    using System;

    [Flags]
    public enum FilterType
    {
        AHrefScript = 4,
        All = 0x10,
        BadWords = 8,
        Frameset = 6,
        Html = 2,
        Iframe = 5,
        Object = 3,
        Script = 1,
        Src = 7
    }
}

