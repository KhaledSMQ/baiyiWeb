namespace Game.Kernel
{
    using System;
    using System.Collections;
    using System.Runtime.CompilerServices;

    [Serializable]
    public class Message : IMessage
    {
        private int m_messageID;
        private bool m_success;

        public Message()
        {
            this.MessageID = 0;
            this.Success = true;
            this.Content = string.Empty;
            this.EntityList = new ArrayList();
        }

        public Message(bool isSuccess) : this(isSuccess, "")
        {
        }

        public Message(bool isSuccess, string content) : this()
        {
            this.MessageID = isSuccess ? 0 : -1;
            this.Content = content;
        }

        public Message(int messageID, string content) : this()
        {
            this.MessageID = messageID;
            this.Content = content;
        }

        public Message(bool isSuccess, string content, ArrayList entityList) : this(isSuccess, content)
        {
            this.EntityList = entityList;
        }

        public Message(int messageID, string content, ArrayList entityList) : this(messageID, content)
        {
            this.EntityList = entityList;
        }

        public void AddEntity(ArrayList entityList)
        {
            this.EntityList = entityList;
        }

        public void AddEntity(object entity)
        {
            this.EntityList.Add(entity);
        }

        public void ResetEntityList()
        {
            if (this.EntityList != null)
            {
                this.EntityList.Clear();
            }
        }

        public string Content { get; set; }

        public ArrayList EntityList { get; set; }

        public int MessageID
        {
            get
            {
                return this.m_messageID;
            }
            set
            {
                this.m_messageID = value;
                this.m_success = this.m_messageID == 0;
            }
        }

        public bool Success
        {
            get
            {
                return this.m_success;
            }
            set
            {
                this.m_success = value;
                if (this.m_success)
                {
                    this.m_messageID = 0;
                }
                else
                {
                    this.m_messageID = -1;
                }
            }
        }
    }
}

