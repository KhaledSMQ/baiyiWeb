namespace Game.Kernel
{
    using System;

    public interface IProvider
    {
        IProvider QueryInterface(Type refType, Version queryVer);

        Version ProviderVersion { get; }
    }
}

