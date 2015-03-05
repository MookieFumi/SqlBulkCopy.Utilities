using Dapper;
using EntityFramework.MappingAPI.Extensions;
using SqlBulkCopy.Utilities.Extensions;
using SqlBulkCopy.Utilities.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;

public class SqlBulkUtilities
{
    private static string ConnectionString;

    public SqlBulkUtilities(string connectionString)
    {
        ConnectionString = connectionString;
    }

    public SortedDataTable GetSortedDataTable<T>(DbContext context, IEnumerable<T> items) where T : IContextEntity
    {
        var entityMap = context.Db<T>();
        var tableName = entityMap.TableName;
        return GetSortedDataTable(tableName, items);
    }

    public SortedDataTable GetSortedDataTable<T>(string tableName, IEnumerable<T> items) where T : IContextEntity
    {
        var properties = GetTableColumns(tableName);
        var dataTable = items.ToDataTable();
        dataTable.SortColumns(properties);

        for (var i = dataTable.Columns.Count - 1; i >= 0; i--)
        {
            var column = dataTable.Columns[i];
            if (!properties.Contains(column.ColumnName))
            {
                dataTable.Columns.Remove(column.ColumnName);
            }
        }

        return new SortedDataTable
        {
            DataTable = dataTable,
            TableName = tableName
        };
    }

    public void WriteToServer(SortedDataTable sortedDataTable, int timeOut = 0)
    {
        using (var sqlBulkCopy = new System.Data.SqlClient.SqlBulkCopy(ConnectionString, SqlBulkCopyOptions.CheckConstraints | SqlBulkCopyOptions.KeepNulls)
                                 {
                                     DestinationTableName = sortedDataTable.TableName,
                                     BulkCopyTimeout = timeOut
                                 })
        {
            sqlBulkCopy.WriteToServer(sortedDataTable.DataTable);
        }
    }

    private static string[] GetTableColumns(string tableName)
    {
        var sqlConnection = new SqlConnection(ConnectionString);
        return sqlConnection.Query<string>(String.Format(@"SELECT  COLUMN_NAME AS ColumnName
                                                           FROM    INFORMATION_SCHEMA.COLUMNS
                                                           WHERE   TABLE_NAME = '{0}'
                                                           ORDER BY ORDINAL_POSITION", 
                                                                                     tableName)).ToArray();
    }
}
