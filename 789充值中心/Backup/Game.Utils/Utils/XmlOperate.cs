namespace Game.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Diagnostics;
    using System.Text;
    using System.Xml;

    public class XmlOperate
    {
        private string _fileContent;
        private string _filePath;
        private OperateXmlMethod _Method;
        private string _xPath;

        public XmlOperate()
        {
        }

        public XmlOperate(string Path)
        {
            this.filePath = Path;
        }

        public void ChangeNode(DataTable dt)
        {
            XmlDocument document = new XmlDocument();
            document.Load(this.filePath);
            XmlNode node = document.SelectSingleNode(this.xPath);
            if (node != null)
            {
                XmlElement element = (XmlElement) node;
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    XmlNodeList childNodes;
                    Debug.WriteLine(string.Format("XML 操作：{0}", i.ToString()));
                    if (dt.Columns[i].ColumnName.StartsWith("@"))
                    {
                        string name = dt.Columns[i].ColumnName.Replace("@", "");
                        if (element.HasAttribute(name))
                        {
                            element.SetAttribute(name, dt.Rows[0][i].ToString());
                        }
                        continue;
                    }
                    if (element.HasChildNodes)
                    {
                        childNodes = element.ChildNodes;
                    }
                    else
                    {
                        continue;
                    }
                    foreach (XmlNode node2 in childNodes)
                    {
                        XmlElement element2;
                        try
                        {
                            element2 = (XmlElement) node2;
                        }
                        catch
                        {
                            continue;
                        }
                        try
                        {
                            if (element2.Name == dt.Columns[i].ColumnName)
                            {
                                try
                                {
                                    element2.InnerXml = dt.Rows[0][i].ToString();
                                }
                                catch
                                {
                                    element2.InnerText = dt.Rows[0][i].ToString();
                                }
                            }
                        }
                        catch
                        {
                            break;
                        }
                    }
                }
                document.Save(this.filePath);
            }
        }

        public void ChangeNode(string parentNodeName, int type, string thename, string thevalue)
        {
            XmlDocument document = new XmlDocument();
            document.Load(this.filePath);
            XmlNodeList list = document.SelectNodes(parentNodeName);
            foreach (XmlNode node in list)
            {
                XmlElement element = (XmlElement) node;
                if (type == 0)
                {
                    if (element.HasAttribute(thename))
                    {
                        element.SetAttribute(thename, thevalue);
                    }
                }
                else if (type == 1)
                {
                    XmlNodeList childNodes = element.ChildNodes;
                    foreach (XmlNode node2 in childNodes)
                    {
                        XmlElement element2 = (XmlElement) node2;
                        if (element2.Name == thename)
                        {
                            try
                            {
                                element2.InnerXml = thevalue;
                            }
                            catch
                            {
                                element2.InnerText = thevalue;
                            }
                        }
                    }
                }
            }
            document.Save(this.filePath);
        }

        public bool CheckXml(string Name)
        {
            bool flag = false;
            XmlDocument document = new XmlDocument();
            document.Load(this.filePath);
            XmlElement element = (XmlElement) document.SelectSingleNode(this.xPath);
            switch (this.Method)
            {
                case OperateXmlMethod.XmlProperty:
                    if (element.HasAttribute(Name))
                    {
                        flag = true;
                    }
                    return flag;

                case OperateXmlMethod.XmlNodes:
                    for (int i = 0; i < element.ChildNodes.Count; i++)
                    {
                        if (element.ChildNodes.Item(i).Name == Name)
                        {
                            flag = true;
                        }
                    }
                    return flag;
            }
            return flag;
        }

        public DataTable ConvertXmlNodeListDataTable(XmlNodeList xlist)
        {
            DataTable table = new DataTable();
            for (int i = 0; i < xlist.Count; i++)
            {
                DataRow row = table.NewRow();
                XmlElement element = (XmlElement) xlist.Item(i);
                int index = 0;
                while (index < element.Attributes.Count)
                {
                    if (!table.Columns.Contains("@" + element.Attributes[index].Name))
                    {
                        table.Columns.Add("@" + element.Attributes[index].Name);
                    }
                    row["@" + element.Attributes[index].Name] = element.Attributes[index].Value;
                    index++;
                }
                for (index = 0; index < element.ChildNodes.Count; index++)
                {
                    if (!table.Columns.Contains(element.ChildNodes.Item(index).Name))
                    {
                        table.Columns.Add(element.ChildNodes.Item(index).Name);
                    }
                    row[element.ChildNodes.Item(index).Name] = element.ChildNodes.Item(index).InnerText;
                }
                table.Rows.Add(row);
            }
            return table;
        }

        public DataTable ConvertXmlNodeListDataTable(XmlNodeList xlist, int type)
        {
            DataTable table = new DataTable();
            for (int i = 0; i < xlist.Count; i++)
            {
                int num2;
                DataRow row = table.NewRow();
                XmlElement element = (XmlElement) xlist.Item(i);
                if (type == 0)
                {
                    num2 = 0;
                    while (num2 < element.Attributes.Count)
                    {
                        if (!table.Columns.Contains("@" + element.Attributes[num2].Name))
                        {
                            table.Columns.Add("@" + element.Attributes[num2].Name);
                        }
                        row["@" + element.Attributes[num2].Name] = element.Attributes[num2].Value;
                        num2++;
                    }
                }
                else if (type == 1)
                {
                    for (num2 = 0; num2 < element.ChildNodes.Count; num2++)
                    {
                        if (!table.Columns.Contains(element.ChildNodes.Item(num2).Name))
                        {
                            table.Columns.Add(element.ChildNodes.Item(num2).Name);
                        }
                        row[element.ChildNodes.Item(num2).Name] = element.ChildNodes.Item(num2).InnerText;
                    }
                }
                table.Rows.Add(row);
            }
            return table;
        }

        public void CreateNode(string nodeName, DataTable dt)
        {
            XmlDocument document = new XmlDocument();
            document.Load(this.filePath);
            XmlNode node = document.SelectSingleNode(this.xPath);
            XmlElement newChild = document.CreateElement(nodeName);
            XmlElement element2 = null;
            if (!object.Equals(dt, null))
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (dt.Columns[i].ColumnName.StartsWith("@"))
                    {
                        string name = dt.Columns[i].ColumnName.Replace("@", "");
                        newChild.SetAttribute(name, dt.Rows[0][i].ToString());
                    }
                    else
                    {
                        element2 = document.CreateElement(dt.Columns[i].ColumnName);
                        try
                        {
                            element2.InnerXml = dt.Rows[0][i].ToString();
                        }
                        catch
                        {
                            element2.InnerText = dt.Rows[0][i].ToString();
                        }
                        newChild.AppendChild(element2);
                    }
                }
            }
            node.AppendChild(newChild);
            document.Save(this.filePath);
        }

        public void CreateNodes(string nodeName, DataTable dt)
        {
            XmlDocument document = new XmlDocument();
            document.Load(this.filePath);
            XmlNode node = document.SelectSingleNode(this.xPath);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                XmlElement newChild = document.CreateElement(nodeName);
                XmlElement element2 = null;
                if (!object.Equals(dt, null))
                {
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        if (dt.Columns[j].ColumnName.StartsWith("@"))
                        {
                            string name = dt.Columns[j].ColumnName.Replace("@", "");
                            newChild.SetAttribute(name, dt.Rows[i][j].ToString());
                        }
                        else
                        {
                            element2 = document.CreateElement(dt.Columns[j].ColumnName);
                            try
                            {
                                element2.InnerXml = dt.Rows[i][j].ToString();
                            }
                            catch
                            {
                                element2.InnerText = dt.Rows[i][j].ToString();
                            }
                            newChild.AppendChild(element2);
                        }
                    }
                }
                node.AppendChild(newChild);
            }
            document.Save(this.filePath);
        }

        public void CreateNodes(string nodeName, DataTable dt, bool CreateNull)
        {
            XmlDocument document = new XmlDocument();
            document.Load(this.filePath);
            XmlNode node = document.SelectSingleNode(this.xPath);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                XmlElement newChild = document.CreateElement(nodeName);
                XmlElement element2 = null;
                if (!object.Equals(dt, null))
                {
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        if (dt.Columns[j].ColumnName.StartsWith("@"))
                        {
                            string name = dt.Columns[j].ColumnName.Replace("@", "");
                            if (CreateNull)
                            {
                                newChild.SetAttribute(name, dt.Rows[i][j].ToString());
                            }
                            else if (dt.Rows[i][j].ToString() != "")
                            {
                                newChild.SetAttribute(name, dt.Rows[i][j].ToString());
                            }
                        }
                        else
                        {
                            element2 = document.CreateElement(dt.Columns[j].ColumnName);
                            if (CreateNull)
                            {
                                try
                                {
                                    element2.InnerXml = dt.Rows[i][j].ToString();
                                }
                                catch
                                {
                                    element2.InnerText = dt.Rows[i][j].ToString();
                                }
                                newChild.AppendChild(element2);
                            }
                            else if (dt.Rows[i][j].ToString() != "")
                            {
                                try
                                {
                                    element2.InnerXml = dt.Rows[i][j].ToString();
                                }
                                catch
                                {
                                    element2.InnerText = dt.Rows[i][j].ToString();
                                }
                                newChild.AppendChild(element2);
                            }
                        }
                    }
                }
                node.AppendChild(newChild);
            }
            document.Save(this.filePath);
        }

        public void CreateXml()
        {
            FileManager.Create(this.filePath, FsoMethod.File);
            XmlTextWriter writer = new XmlTextWriter(this.filePath, Encoding.GetEncoding("gb2312")) {
                Formatting = Formatting.Indented,
                Indentation = 3
            };
            writer.WriteStartDocument();
            writer.Flush();
            writer.Close();
        }

        public void CreateXml(string rootNodeName)
        {
            FileManager.Create(this.filePath, FsoMethod.File);
            XmlTextWriter writer = new XmlTextWriter(this.filePath, Encoding.GetEncoding("gb2312")) {
                Formatting = Formatting.Indented,
                Indentation = 3
            };
            writer.WriteStartDocument();
            writer.WriteStartElement(rootNodeName);
            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Flush();
            writer.Close();
        }

        public void DeleteNode(string nodeName)
        {
            XmlDocument document = new XmlDocument();
            document.Load(this.filePath);
            XmlNode node = document.SelectSingleNode(this.xPath);
            XmlNode oldChild = node.SelectSingleNode(nodeName);
            if (oldChild != null)
            {
                node.RemoveChild(oldChild);
                document.Save(this.filePath);
            }
        }

        public IList<string> GetXml(string name)
        {
            List<string> list = new List<string>();
            int num = 0;
            XmlDocument document = new XmlDocument();
            document.Load(this.filePath);
            XmlNodeList list2 = document.SelectNodes(this.xPath);
            foreach (XmlNode node in list2)
            {
                XmlElement element = (XmlElement) node;
                switch (this.Method)
                {
                    case OperateXmlMethod.XmlProperty:
                        if (element.HasAttribute(name))
                        {
                            list.Add(element.GetAttribute(name));
                        }
                        break;

                    case OperateXmlMethod.XmlNodes:
                    {
                        XmlNodeList childNodes = element.ChildNodes;
                        foreach (XmlNode node2 in childNodes)
                        {
                            XmlElement element2 = (XmlElement) node2;
                            if (element2.Name == name)
                            {
                                list.Add(element2.InnerText);
                            }
                        }
                        break;
                    }
                }
                num++;
            }
            return list;
        }

        public IList<string> GetXml(string nodes, int type, string name)
        {
            List<string> list = new List<string>();
            int num = 0;
            XmlDocument document = new XmlDocument();
            document.Load(this.filePath);
            XmlNodeList list2 = document.SelectNodes(nodes);
            foreach (XmlNode node in list2)
            {
                XmlElement element = (XmlElement) node;
                switch (type)
                {
                    case 0:
                        if (element.HasAttribute(name))
                        {
                            list.Add(element.GetAttribute(name));
                        }
                        break;

                    case 1:
                    {
                        XmlNodeList childNodes = element.ChildNodes;
                        foreach (XmlNode node2 in childNodes)
                        {
                            XmlElement element2 = (XmlElement) node2;
                            if (element2.Name == name)
                            {
                                list.Add(element2.InnerText);
                            }
                        }
                        break;
                    }
                }
                num++;
            }
            return list;
        }

        public XmlElement GetXmlElement(string nodes)
        {
            XmlDocument document = new XmlDocument();
            document.Load(this.filePath);
            return (XmlElement) document.SelectSingleNode(nodes);
        }

        public XmlNodeList GetXmlNodeList(string nodes, int method)
        {
            XmlDocument document = new XmlDocument();
            if (!string.IsNullOrEmpty(this.fileContent))
            {
                document.LoadXml(this.fileContent);
            }
            else
            {
                document.Load(this.filePath);
            }
            switch (method)
            {
                case 0:
                    return document.SelectSingleNode(nodes).ChildNodes;

                case 1:
                    return document.SelectNodes(nodes);
            }
            return null;
        }

        public string fileContent
        {
            get
            {
                return this._fileContent;
            }
            set
            {
                this._fileContent = value;
            }
        }

        public string filePath
        {
            get
            {
                return this._filePath;
            }
            set
            {
                this._filePath = value;
            }
        }

        public OperateXmlMethod Method
        {
            get
            {
                return this._Method;
            }
            set
            {
                this._Method = value;
            }
        }

        public string xPath
        {
            get
            {
                return this._xPath;
            }
            set
            {
                this._xPath = value;
            }
        }
    }
}

