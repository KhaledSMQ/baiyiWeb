namespace Game.Kernel
{
    using Game.Utils;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public class TableProvider : BaseDataProvider, ITableProvider
    {
        private string m_tableName;

        public TableProvider(DbHelper database, string tableName) : base(database)
        {
            this.m_tableName = "";
            this.m_tableName = tableName;
        }

        public TableProvider(string connectionString, string tableName) : base(connectionString)
        {
            this.m_tableName = "";
            this.m_tableName = tableName;
        }

        public void BatchCommitData(DataSet dataSet, string[][] columnMapArray)
        {
            this.BatchCommitData(dataSet.Tables[0], columnMapArray);
        }

        public void BatchCommitData(DataTable table, string[][] columnMapArray)
        {
            using (SqlBulkCopy copy = new SqlBulkCopy(base.Database.ConnectionString))
            {
                copy.DestinationTableName = this.TableName;
                foreach (string[] strArray in columnMapArray)
                {
                    copy.ColumnMappings.Add(strArray[0], strArray[1]);
                }
                copy.WriteToServer(table);
                copy.Close();
            }
        }

        public void CommitData(DataTable dt)
        {
            DataSet dataSet = this.ConstructDataSet(dt);
            base.Database.UpdateDataSet(dataSet, this.TableName);
        }

        private DataSet ConstructDataSet(DataTable dt)
        {
            DataSet set = null;
            if (dt.DataSet != null)
            {
                return dt.DataSet;
            }
            set = new DataSet();
            set.Tables.Add(dt);
            return set;
        }

        public void Delete(string where)
        {
            string commandText = string.Format("DELETE FROM {0} {1}", this.TableName, where);
            base.Database.ExecuteNonQuery(commandText);
        }

        public DataSet Get(string where)
        {
            string commandText = string.Format("SELECT * FROM {0} {1}", this.TableName, where);
            return base.Database.ExecuteDataset(commandText);
        }

        public DataTable GetEmptyTable()
        {
            DataTable emptyTable = base.Database.GetEmptyTable(this.TableName);
            emptyTable.TableName = this.TableName;
            return emptyTable;
        }

        public T GetObject<T>(string where)
        {
            DataRow one = this.GetOne(where);
            if (one == null)
            {
                return default(T);
            }
            return DataHelper.ConvertRowToObject<T>(one);
        }

        public IList<T> GetObjectList<T>(string where)
        {
            DataSet ds = this.Get(where);
            if (base.CheckedDataSet(ds))
            {
                return DataHelper.ConvertDataTableToObjects<T>(ds.Tables[0]);
            }
            return null;
        }

        public DataRow GetOne(string where)
        {
            DataSet set = this.Get(where);
            if (set.Tables[0].Rows.Count > 0)
            {
                return set.Tables[0].Rows[0];
            }
            return null;
        }

        public int GetRecordsCount(string where)
        {
            if (where == null)
            {
                where = "";
            }
            string commandText = string.Format("SELECT COUNT(*) FROM {0} {1}", this.TableName, where);
            return int.Parse(base.Database.ExecuteScalarToStr(CommandType.Text, commandText));
        }

        public void Insert(DataRow row)
        {
            DataTable emptyTable = this.GetEmptyTable();
            try
            {
                DataRow row2 = emptyTable.NewRow();
                for (int i = 0; i < emptyTable.Columns.Count; i++)
                {
                    row2[i] = row[i];
                }
                emptyTable.Rows.Add(row2);
                this.CommitData(emptyTable);
            }
            catch
            {
                throw;
            }
            finally
            {
                emptyTable.Rows.Clear();
                emptyTable.AcceptChanges();
            }
        }

        public DataRow NewRow()
        {
            DataTable emptyTable = this.GetEmptyTable();
            DataRow row = emptyTable.NewRow();
            for (int i = 0; i < emptyTable.Columns.Count; i++)
            {
                row[i] = DBNull.Value;
            }
            return row;
        }

        public string TableName
        {
            get
            {
                return this.m_tableName;
            }
        }
    }
}

