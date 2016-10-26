namespace Game.Kernel
{
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class SqlSystemProvider : BaseDataProvider, IDBSystemProvider
    {
        public SqlSystemProvider(string connectionString) : base(connectionString)
        {
        }

        public void BackupDb(string dbName, string bakFilePath)
        {
            string commandText = string.Format("backup database {0} to disk = '{1}';", dbName, bakFilePath);
            base.Database.ExecuteCommandWithSplitter(commandText);
        }

        public void CreateDb(string newDbName)
        {
            string commandText = string.Format("Create database {0}", newDbName);
            base.Database.ExecuteCommandWithSplitter(commandText);
        }

        public IList<string> GetAllDbNames()
        {
            IList<string> list = new List<string>();
            DataSet set = base.Database.ExecuteDataset("sp_helpdb");
            for (int i = 0; i < set.Tables[0].Rows.Count; i++)
            {
                string item = set.Tables[0].Rows[i]["Name"].ToString();
                list.Add(item);
            }
            return list;
        }

        public void RemoveDb(string dbName)
        {
            string commandText = string.Format("Drop database {0}", dbName);
            base.Database.ExecuteCommandWithSplitter(commandText);
        }

        public void RestoreDb(string bakFilePath, string dbName)
        {
            string commandText = string.Format("Alter Database {0} Set Offline with Rollback immediate ; restore database {0} from disk = '{1}' ;Alter Database {0} Set OnLine With rollback Immediate", dbName, bakFilePath);
            base.Database.ExecuteCommandWithSplitter(commandText);
        }
    }
}

