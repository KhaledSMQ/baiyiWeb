namespace Game.Utils
{
    using System;

    public class LocalIPAndMac
    {
        private string m_IPAddress;
        private string m_MACAddress;

        public LocalIPAndMac()
        {
        }

        public LocalIPAndMac(string ip, string mac)
        {
            this.m_MACAddress = mac;
            this.m_IPAddress = ip;
        }

        public string IPAddress
        {
            get
            {
                return this.m_IPAddress;
            }
            set
            {
                this.m_IPAddress = value;
            }
        }

        public string MACAddress
        {
            get
            {
                return this.m_MACAddress;
            }
            set
            {
                this.m_MACAddress = value;
            }
        }
    }
}

