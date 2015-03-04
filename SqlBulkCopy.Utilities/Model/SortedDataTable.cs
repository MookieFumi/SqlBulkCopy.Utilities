using System.Data;

namespace SqlBulkCopy.Utilities.Model
{
    public sealed class SortedDataTable
    {
        public DataTable DataTable { get; set; }
        public string TableName { get; set; }
    }
}
