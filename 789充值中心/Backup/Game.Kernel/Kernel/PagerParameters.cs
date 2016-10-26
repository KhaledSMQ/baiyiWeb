namespace Game.Kernel
{
    using System;

    public class PagerParameters
    {
        private bool m_ascending;
        private int m_cacherSize;
        private string[] m_fieldAlias;
        private string[] m_fields;
        private int m_pageIndex;
        private int m_pageSize;
        private string m_pkey;
        private string m_table;
        private string m_whereStr;

        public PagerParameters()
        {
            this.m_ascending = true;
            this.m_pageIndex = 1;
            this.m_pageSize = 100;
            this.m_cacherSize = 0;
            this.m_pkey = "";
            this.m_whereStr = "";
            this.m_table = "";
        }

        public PagerParameters(string table, string pkey, int pageIndex)
        {
            this.m_ascending = true;
            this.m_pageSize = 20;
            this.m_cacherSize = 0;
            this.m_table = table;
            this.m_pkey = pkey;
            this.m_pageIndex = pageIndex;
        }

        public PagerParameters(string table, string pkey, int pageIndex, int pageSize) : this(table, pkey, pageIndex)
        {
            this.m_pageSize = pageSize;
        }

        public PagerParameters(string table, string pkey, int pageIndex, string whereStr) : this(table, pkey, pageIndex)
        {
            this.m_whereStr = whereStr;
        }

        public PagerParameters(string table, string pkey, string whereStr, int pageIndex, int pageSize) : this(table, pkey, pageIndex, whereStr)
        {
            this.m_pageSize = pageSize;
        }

        public PagerParameters(string table, string pkey, string whereStr, int pageIndex, int pageSize, string[] fields) : this(table, pkey, whereStr, pageIndex, pageSize)
        {
            this.Fields = fields;
        }

        public PagerParameters(string table, string pkey, string whereStr, int pageIndex, int pageSize, string[] fields, string[] fieldAlias) : this(table, pkey, whereStr, pageIndex, pageSize, fields)
        {
            this.FieldAlias = fieldAlias;
        }

        public bool Ascending
        {
            get
            {
                return this.m_ascending;
            }
            set
            {
                this.m_ascending = value;
            }
        }

        public int CacherSize
        {
            get
            {
                return this.m_cacherSize;
            }
            set
            {
                this.m_cacherSize = value;
            }
        }

        public string[] FieldAlias
        {
            get
            {
                return this.m_fieldAlias;
            }
            set
            {
                this.m_fieldAlias = value;
            }
        }

        public string[] Fields
        {
            get
            {
                return this.m_fields;
            }
            set
            {
                this.m_fields = value;
            }
        }

        public int PageIndex
        {
            get
            {
                return this.m_pageIndex;
            }
            set
            {
                this.m_pageIndex = value;
            }
        }

        public int PageSize
        {
            get
            {
                return this.m_pageSize;
            }
            set
            {
                this.m_pageSize = value;
            }
        }

        public string PKey
        {
            get
            {
                return this.m_pkey;
            }
            set
            {
                this.m_pkey = value;
            }
        }

        public string Table
        {
            get
            {
                return this.m_table;
            }
            set
            {
                this.m_table = value;
            }
        }

        public string WhereStr
        {
            get
            {
                return this.m_whereStr;
            }
            set
            {
                this.m_whereStr = value;
            }
        }
    }
}

