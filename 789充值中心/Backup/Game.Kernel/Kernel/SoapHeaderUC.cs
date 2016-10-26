namespace Game.Kernel
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://tempuri.org/", IsNullable=false), XmlType(Namespace="http://tempuri.org/")]
    public class SoapHeaderUC : SoapHeader, ISoapHeaderUC
    {
        public SoapHeaderUC()
        {
        }

        public SoapHeaderUC(ISoapHeaderUC iuc)
        {
            this.StationID = iuc.StationID;
            this.AccreditKey = iuc.AccreditKey;
            this.WebVersion = iuc.WebVersion;
            this.ServiceStyle = iuc.ServiceStyle;
            this.ConnectStyle = iuc.ConnectStyle;
            this.MachineSerial = iuc.MachineSerial;
            this.IPAddress = iuc.IPAddress;
            this.ClusterID = iuc.ClusterID;
            this.EntropyTicks = iuc.EntropyTicks;
            this.Signature = iuc.Signature;
        }

        public SoapHeaderUC(string stationID, string accreditKey, string webVersion, AccreditType serviceStyle, ConnectionProtocol connectStyle, string machineSerial, string ipAddress, string clusterID, string entropyTicks, string signature)
        {
            this.StationID = stationID;
            this.AccreditKey = accreditKey;
            this.WebVersion = webVersion;
            this.ServiceStyle = serviceStyle;
            this.ConnectStyle = connectStyle;
            this.MachineSerial = machineSerial;
            this.IPAddress = ipAddress;
            this.ClusterID = clusterID;
            this.EntropyTicks = entropyTicks;
            this.Signature = signature;
        }

        public override string ToString()
        {
            return new StringBuilder().AppendLine(this.StationID).AppendLine(this.AccreditKey).AppendLine(this.WebVersion).AppendLine(this.ServiceStyle.ToString()).AppendLine(this.ConnectStyle.ToString()).AppendLine(this.MachineSerial).AppendLine(this.EntropyTicks).ToString();
        }

        public string AccreditKey { get; set; }

        public string ClusterID { get; set; }

        public ConnectionProtocol ConnectStyle { get; set; }

        public string EntropyTicks { get; set; }

        public string IPAddress { get; set; }

        public DBLineType LineType { get; set; }

        public string MachineSerial { get; set; }

        public AccreditType ServiceStyle { get; set; }

        public string Signature { get; set; }

        public string StationID { get; set; }

        public string WebVersion { get; set; }
    }
}

