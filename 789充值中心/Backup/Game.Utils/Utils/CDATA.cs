namespace Game.Utils
{
    using System;
    using System.Xml;
    using System.Xml.Schema;
    using System.Xml.Serialization;

    public class CDATA : IXmlSerializable
    {
        private string text;

        public CDATA()
        {
        }

        public CDATA(string text)
        {
            this.text = text;
        }

        XmlSchema IXmlSerializable.GetSchema()
        {
            return null;
        }

        void IXmlSerializable.ReadXml(XmlReader reader)
        {
            this.text = reader.ReadElementContentAsString();
        }

        void IXmlSerializable.WriteXml(XmlWriter writer)
        {
            writer.WriteCData(this.text);
        }

        public string Text
        {
            get
            {
                return this.text;
            }
        }
    }
}

