namespace Game.Utils
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Xml;

    public class Caching3
    {
        private ICacheStrategy cacheStrategy;
        private XmlElement objectXmlMap;
        private XmlDocument rootXml = new XmlDocument();

        public Caching3(ICacheStrategy icache)
        {
            this.cacheStrategy = icache;
            this.objectXmlMap = this.rootXml.CreateElement("Cache");
            this.rootXml.AppendChild(this.objectXmlMap);
        }

        private XmlNode CreateNode(string xpath)
        {
            string[] strArray = xpath.Split(new char[] { '/' });
            string str = "";
            XmlNode objectXmlMap = this.objectXmlMap;
            for (int i = 1; i < strArray.Length; i++)
            {
                if (this.objectXmlMap.SelectSingleNode(str + "/" + strArray[i]) == null)
                {
                    XmlElement newChild = this.objectXmlMap.OwnerDocument.CreateElement(strArray[i]);
                    objectXmlMap.AppendChild(newChild);
                }
                str = str + "/" + strArray[i];
                objectXmlMap = this.objectXmlMap.SelectSingleNode(str);
            }
            return objectXmlMap;
        }

        public T Get<T>(string xpath)
        {
            object obj2 = this.Get(xpath);
            if (!((obj2 != null) && (obj2 is T)))
            {
                return default(T);
            }
            return (T) obj2;
        }

        public virtual object Get(string xpath)
        {
            object obj2 = null;
            XmlNode node = this.objectXmlMap.SelectSingleNode(this.PrepareXpath(xpath));
            if (node != null)
            {
                string key = node.Attributes["objectKey"].Value;
                obj2 = this.cacheStrategy.Get(key);
            }
            return obj2;
        }

        public virtual IList<T> GetList<T>(string xpath)
        {
            XmlNodeList list = this.objectXmlMap.SelectSingleNode(this.PrepareXpath(xpath)).SelectNodes(this.PrepareXpath(xpath) + "/*[@objectKey]");
            List<T> list2 = new List<T>();
            string key = null;
            foreach (XmlNode node2 in list)
            {
                key = node2.Attributes["objectKey"].Value;
                list2.Add(this.cacheStrategy.Get<T>(key));
            }
            return list2;
        }

        public virtual object[] GetList(string xpath)
        {
            XmlNodeList list = this.objectXmlMap.SelectSingleNode(this.PrepareXpath(xpath)).SelectNodes(this.PrepareXpath(xpath) + "/*[@objectKey]");
            ArrayList list2 = new ArrayList();
            string key = null;
            foreach (XmlNode node2 in list)
            {
                key = node2.Attributes["objectKey"].Value;
                list2.Add(this.cacheStrategy.Get(key));
            }
            return (object[]) list2.ToArray(typeof(object));
        }

        private string PrepareXpath(string xpath)
        {
            string[] strArray = xpath.Split(new char[] { '/' });
            xpath = "/Cache";
            foreach (string str in strArray)
            {
                if (str != "")
                {
                    xpath = xpath + "/" + str;
                }
            }
            return xpath;
        }

        public virtual void Remove(string xpath)
        {
            XmlNode oldChild = this.objectXmlMap.SelectSingleNode(this.PrepareXpath(xpath));
            if (oldChild != null)
            {
                string str;
                if (oldChild.HasChildNodes)
                {
                    XmlNodeList list = oldChild.SelectNodes("*[@objectKey]");
                    str = "";
                    foreach (XmlNode node2 in list)
                    {
                        str = node2.Attributes["objectKey"].Value;
                        node2.ParentNode.RemoveChild(node2);
                        this.cacheStrategy.Remove(str);
                    }
                }
                else if (oldChild.Attributes.Count > 0)
                {
                    str = oldChild.Attributes["objectKey"].Value;
                    oldChild.ParentNode.RemoveChild(oldChild);
                    this.cacheStrategy.Remove(str);
                }
            }
        }

        public virtual void Set(string xpath, object obj)
        {
            string str = this.PrepareXpath(xpath);
            int length = str.LastIndexOf("/");
            string str2 = str.Substring(0, length);
            string name = str.Substring(length + 1);
            XmlNode node = this.objectXmlMap.SelectSingleNode(str2);
            if (node == null)
            {
                lock (this)
                {
                    node = this.CreateNode(str2);
                }
            }
            string key = Guid.NewGuid().ToString();
            XmlElement newChild = this.objectXmlMap.OwnerDocument.CreateElement(name);
            XmlAttribute attribute = this.objectXmlMap.OwnerDocument.CreateAttribute("objectKey");
            attribute.Value = key;
            newChild.Attributes.Append(attribute);
            node.AppendChild(newChild);
            this.cacheStrategy.Set(key, obj);
        }

        public virtual void Set(string xpath, object obj, DateTime expiresAt)
        {
            string str = this.PrepareXpath(xpath);
            int length = str.LastIndexOf("/");
            string str2 = str.Substring(0, length);
            string name = str.Substring(length + 1);
            XmlNode node = this.objectXmlMap.SelectSingleNode(str2);
            if (node == null)
            {
                lock (this)
                {
                    node = this.CreateNode(str2);
                }
            }
            string key = Guid.NewGuid().ToString();
            XmlElement newChild = this.objectXmlMap.OwnerDocument.CreateElement(name);
            XmlAttribute attribute = this.objectXmlMap.OwnerDocument.CreateAttribute("objectKey");
            attribute.Value = key;
            newChild.Attributes.Append(attribute);
            node.AppendChild(newChild);
            this.cacheStrategy.Set(key, obj, expiresAt);
        }
    }
}

