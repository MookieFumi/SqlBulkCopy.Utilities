using NUnit.Framework;
using SqlBulkCopy.Utilities.Extensions;

namespace SqlBulkCopy.Utilities.Test
{
    [TestFixture]
    public class DataTableExtensionsTest
    {
        private const string FULL_NAME = "FullName";
        private const string DATE = "Date";
        private const string IP = "Ip";
        private const string ID = "Id";

        [TestFixtureSetUp]
        public void SetUp()
        {
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
        }

        [Test]
        public void When_SetSortColumns_is_called_all_columns_are_sorted()
        {
            // Arrange
            var accessLogList = TestHelper.GenerateAccessLogList(150);

            // Act
            var dataTable = accessLogList.ToDataTable();
            dataTable.SortColumns(FULL_NAME, DATE, IP, ID);

            // Assert
            Assert.AreEqual(dataTable.Columns[0].ColumnName, FULL_NAME);
            Assert.AreEqual(dataTable.Columns[1].ColumnName, DATE);
            Assert.AreEqual(dataTable.Columns[2].ColumnName, IP);
            Assert.AreEqual(dataTable.Columns[3].ColumnName, ID);
        }

    }
}
