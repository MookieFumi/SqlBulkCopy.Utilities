using System;
using System.Data;
using System.Linq;

namespace SqlBulkCopy.Utilities.Extensions
{
    public static class DataTableExtensions
    {
        public static void SortColumns(this DataTable dataTable, params String[] columnNames)
        {
            var columns = columnNames.ToList();
            foreach (var column in columns)
            {
                dataTable.Columns[column].SetOrdinal(columns.IndexOf(column));
            }
        }
    }
}