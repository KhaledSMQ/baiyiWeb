namespace Game.Kernel
{
    using System;

    public interface ISoapHeaderUC
    {
        string AccreditKey { get; set; }

        string ClusterID { get; set; }

        ConnectionProtocol ConnectStyle { get; set; }

        string EntropyTicks { get; set; }

        string IPAddress { get; set; }

        DBLineType LineType { get; set; }

        string MachineSerial { get; set; }

        AccreditType ServiceStyle { get; set; }

        string Signature { get; set; }

        string StationID { get; set; }

        string WebVersion { get; set; }
    }
}

