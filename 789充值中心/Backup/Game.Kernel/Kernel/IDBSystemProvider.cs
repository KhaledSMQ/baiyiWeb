namespace Game.Kernel
{
    using System;
    using System.Collections.Generic;

    public interface IDBSystemProvider
    {
        void BackupDb(string dbName, string bakFilePath);
        void CreateDb(string newDbName);
        IList<string> GetAllDbNames();
        void RemoveDb(string dbName);
        void RestoreDb(string bakFilePath, string dbName);
    }
}

