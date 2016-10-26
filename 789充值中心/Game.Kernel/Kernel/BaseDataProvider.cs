namespace Game.Kernel
{
    using System;

    public abstract class BaseDataProvider : BaseProvider
    {
        private string m_connectionString;
        private DbHelper m_database;
        private PagerManager m_pagerHelper;

        protected internal BaseDataProvider()
        {
        }

        protected internal BaseDataProvider(DbHelper database)
        {
            this.m_database = database;
            this.m_connectionString = database.ConnectionString;
            this.m_pagerHelper = new PagerManager(this.m_database);
        }

        protected internal BaseDataProvider(string connectionString)
        {
            this.m_connectionString = connectionString;
            this.m_database = new DbHelper(connectionString);
            this.m_pagerHelper = new PagerManager(this.m_database);
        }

        protected virtual PagerSet GetPagerSet(PagerParameters prams)
        {
            return this.PagerHelper.GetPagerSet(prams);
        }

        protected virtual PagerSet GetPagerSet2(PagerParameters prams)
        {
            return this.PagerHelper.GetPagerSet2(prams);
        }

        protected virtual ITableProvider GetTableProvider(string tableName)
        {
            return new TableProvider(this.Database, tableName);
        }

        protected internal string ConnectionString
        {
            get
            {
                return this.m_connectionString;
            }
        }

        protected internal DbHelper Database
        {
            get
            {
                return this.m_database;
            }
        }

        protected internal PagerManager PagerHelper
        {
            get
            {
                return this.m_pagerHelper;
            }
        }
    }
}

