namespace Game.Kernel
{
    using System;

    public interface IConfigFileManager
    {
        IConfigInfo LoadConfig();
        bool SaveConfig();
    }
}

