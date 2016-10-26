namespace Game.Utils
{
    using System;
    using System.Data;
    using System.Text.RegularExpressions;
    using System.Xml;

    public class XmlDocumentExtender : XmlDocument
    {
        public XmlDocumentExtender()
        {
        }

        public XmlDocumentExtender(XmlNameTable nt) : base(new XmlImplementation(nt))
        {
        }

        public bool AppendChildElementByDataRow(ref XmlElement xmlElement, DataColumnCollection dcc, DataRow dr)
        {
            return this.AppendChildElementByDataRow(ref xmlElement, dcc, dr, null);
        }

        public bool AppendChildElementByDataRow(ref XmlElement xmlElement, DataColumnCollection dcc, DataRow dr, string removecols)
        {
            if ((xmlElement != null) && (xmlElement.OwnerDocument != null))
            {
                foreach (DataColumn column in dcc)
                {
                    if (((removecols == null) || (removecols == "")) || (("," + removecols + ",").ToLower().IndexOf("," + column.Caption.ToLower() + ",") < 0))
                    {
                        XmlElement newChild = xmlElement.OwnerDocument.CreateElement(column.Caption);
                        newChild.InnerText = this.FiltrateControlCharacter(dr[column.Caption].ToString().Trim());
                        xmlElement.AppendChild(newChild);
                    }
                }
                return true;
            }
            return false;
        }

        public bool AppendChildElementByNameValue(ref XmlElement xmlElement, string childElementName, object childElementValue)
        {
            return this.AppendChildElementByNameValue(ref xmlElement, childElementName, childElementValue, false);
        }

        public bool AppendChildElementByNameValue(ref XmlNode xmlNode, string childElementName, object childElementValue)
        {
            return this.AppendChildElementByNameValue(ref xmlNode, childElementName, childElementValue, false);
        }

        public bool AppendChildElementByNameValue(ref XmlElement xmlElement, string childElementName, object childElementValue, bool IsCDataSection)
        {
            if ((xmlElement != null) && (xmlElement.OwnerDocument != null))
            {
                XmlElement element;
                if (IsCDataSection)
                {
                    XmlCDataSection newChild = xmlElement.OwnerDocument.CreateCDataSection(childElementName);
                    newChild.InnerText = this.FiltrateControlCharacter(childElementValue.ToString());
                    element = xmlElement.OwnerDocument.CreateElement(childElementName);
                    element.AppendChild(newChild);
                    xmlElement.AppendChild(element);
                }
                else
                {
                    element = xmlElement.OwnerDocument.CreateElement(childElementName);
                    element.InnerText = this.FiltrateControlCharacter(childElementValue.ToString());
                    xmlElement.AppendChild(element);
                }
                return true;
            }
            return false;
        }

        public bool AppendChildElementByNameValue(ref XmlNode xmlNode, string childElementName, object childElementValue, bool IsCDataSection)
        {
            if ((xmlNode != null) && (xmlNode.OwnerDocument != null))
            {
                XmlElement element;
                if (IsCDataSection)
                {
                    XmlCDataSection newChild = xmlNode.OwnerDocument.CreateCDataSection(childElementName);
                    newChild.InnerText = this.FiltrateControlCharacter(childElementValue.ToString());
                    element = xmlNode.OwnerDocument.CreateElement(childElementName);
                    element.AppendChild(newChild);
                    xmlNode.AppendChild(element);
                }
                else
                {
                    element = xmlNode.OwnerDocument.CreateElement(childElementName);
                    element.InnerText = this.FiltrateControlCharacter(childElementValue.ToString());
                    xmlNode.AppendChild(element);
                }
                return true;
            }
            return false;
        }

        public XmlNode CreateNode(string xmlpath)
        {
            string[] strArray = xmlpath.Split(new char[] { '/' });
            string xpath = "";
            XmlNode node = this;
            for (int i = 1; i < strArray.Length; i++)
            {
                if (base.SelectSingleNode(xpath + "/" + strArray[i]) == null)
                {
                    XmlElement newChild = base.CreateElement(strArray[i]);
                    node.AppendChild(newChild);
                }
                xpath = xpath + "/" + strArray[i];
                node = base.SelectSingleNode(xpath);
            }
            return node;
        }

        private string FiltrateControlCharacter(string content)
        {
            return Regex.Replace(content, "[\0-\b|\v-\f|\x000e-\x001f]", "");
        }

        public string GetSingleNodeValue(XmlNode xmlnode, string path)
        {
            if ((xmlnode != null) && (xmlnode.SelectSingleNode(path) != null))
            {
                if (xmlnode.SelectSingleNode(path).LastChild != null)
                {
                    return xmlnode.SelectSingleNode(path).LastChild.Value;
                }
                return "";
            }
            return null;
        }

        public XmlNode InitializeNode(string xmlpath)
        {
            XmlNode node = base.SelectSingleNode(xmlpath);
            if (node != null)
            {
                node.RemoveAll();
                return node;
            }
            return this.CreateNode(xmlpath);
        }

        public override void Load(string filename)
        {
            if (!FileManager.Exists(filename, FsoMethod.File))
            {
                throw new UCException("文件: " + filename + " 不存在!");
            }
            base.Load(filename);
        }

        public void RemoveNodeAndChildNode(string xmlpath)
        {
            XmlNodeList list = base.SelectNodes(xmlpath);
            if (list.Count > 0)
            {
                foreach (XmlNode node in list)
                {
                    node.RemoveAll();
                    node.ParentNode.RemoveChild(node);
                }
            }
        }
    }
}

