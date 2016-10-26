namespace Game.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Reflection;

    public class DataHelper
    {
        public static IList<TEntity> ConvertDataTableToObjects<TEntity>(DataTable dt)
        {
            if (dt == null)
            {
                return null;
            }
            IList<TEntity> list = new List<TEntity>();
            foreach (DataRow row in dt.Rows)
            {
                list.Add(ConvertRowToObject<TEntity>(row));
            }
            return list;
        }

        public static TEntity ConvertRowToObject<TEntity>(DataRow row)
        {
            if (row == null)
            {
                return default(TEntity);
            }
            Type objType = typeof(TEntity);
            return (TEntity) ConvertRowToObject(objType, row);
        }

        public static object ConvertRowToObject(Type objType, DataRow row)
        {
            if (row == null)
            {
                return null;
            }
            DataTable table = row.Table;
            object target = Activator.CreateInstance(objType);
            foreach (DataColumn column in table.Columns)
            {
                PropertyInfo property = objType.GetProperty(column.ColumnName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                if (property == null)
                {
                    throw new PropertyNotFoundException(column.ColumnName);
                }
                Type propertyType = property.PropertyType;
                object obj3 = null;
                bool flag = true;
                try
                {
                    obj3 = TypeHelper.ChangeType(propertyType, row[column.ColumnName]);
                }
                catch
                {
                    flag = false;
                }
                if (flag)
                {
                    object[] args = new object[] { obj3 };
                    objType.InvokeMember(column.ColumnName, BindingFlags.SetProperty | BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase, null, target, args);
                }
            }
            return target;
        }

        public static IList<string> DistillCommandParameter(string sqlStatement, string paraPrefix)
        {
            sqlStatement = sqlStatement + " ";
            IList<string> paraList = new List<string>();
            DoDistill(sqlStatement, paraList, paraPrefix);
            if (paraList.Count > 0)
            {
                string item = paraList[paraList.Count - 1].Trim();
                if (item.EndsWith("\""))
                {
                    item.TrimEnd(new char[] { '"' });
                    paraList.RemoveAt(paraList.Count - 1);
                    paraList.Add(item);
                }
            }
            return paraList;
        }

        private static void DoDistill(string sqlStatement, IList<string> paraList, string paraPrefix)
        {
            sqlStatement.TrimStart(new char[] { ' ' });
            int index = sqlStatement.IndexOf(paraPrefix);
            if (index >= 0)
            {
                int startIndex = sqlStatement.IndexOf(" ", index);
                int length = startIndex - index;
                string str = sqlStatement.Substring(index, length);
                paraList.Add(str.Replace(paraPrefix, ""));
                DoDistill(sqlStatement.Substring(startIndex), paraList, paraPrefix);
            }
        }

        public static void FillCommandParameterValue(IDbCommand command, object entityOrRow)
        {
            foreach (IDbDataParameter parameter in command.Parameters)
            {
                parameter.Value = GetColumnValue(entityOrRow, parameter.SourceColumn);
                if (parameter.Value == null)
                {
                    parameter.Value = DBNull.Value;
                }
            }
        }

        public static object GetColumnValue(object entityOrRow, string columnName)
        {
            DataRow row = entityOrRow as DataRow;
            if (row != null)
            {
                return row[columnName];
            }
            return entityOrRow.GetType().InvokeMember(columnName, BindingFlags.GetProperty | BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase, null, entityOrRow, null);
        }

        public static object GetSafeDbValue(object val)
        {
            if (val != null)
            {
                return val;
            }
            return DBNull.Value;
        }

        public static void RefreshEntityFields(object entity, DataRow row)
        {
            DataTable table = row.Table;
            IList<string> refreshFields = new List<string>();
            foreach (DataColumn column in table.Columns)
            {
                refreshFields.Add(column.ColumnName);
            }
            RefreshEntityFields(entity, row, refreshFields);
        }

        public static void RefreshEntityFields(object entity, DataRow row, IList<string> refreshFields)
        {
            Type type = entity.GetType();
            foreach (string str in refreshFields)
            {
                string name = str;
                PropertyInfo property = type.GetProperty(name, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                if (property == null)
                {
                    throw new PropertyNotFoundException(name);
                }
                Type propertyType = property.PropertyType;
                object obj2 = null;
                bool flag = true;
                try
                {
                    obj2 = TypeHelper.ChangeType(propertyType, row[name]);
                }
                catch
                {
                    flag = false;
                }
                if (flag)
                {
                    object[] args = new object[] { obj2 };
                    type.InvokeMember(name, BindingFlags.SetProperty | BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase, null, entity, args);
                }
            }
        }
    }
}

