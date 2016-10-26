namespace Game.Kernel
{
    using Game.Utils;
    using System;
    using System.Data;

    public abstract class BaseProvider : IProvider
    {
        protected BaseProvider()
        {
        }

        protected bool CheckedDataSet(DataSet ds)
        {
            return Validate.CheckedDataSet(ds);
        }

        public IProvider QueryInterface(Type refType, Version queryVer)
        {
            if (ApplicationEnvironment.InterfaceGuidCompare(ApplicationEnvironment.GetInterfaceGuid(refType), base.GetType()) && ApplicationEnvironment.InterfaceVersionCompare(ApplicationEnvironment.GetInterfaceVersion(refType), queryVer))
            {
                return this;
            }
            return null;
        }

        public Version ProviderVersion
        {
            get
            {
                return base.GetType().Assembly.GetName().Version;
            }
        }
    }
}

