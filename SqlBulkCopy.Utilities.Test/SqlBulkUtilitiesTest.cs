using System.Data.Entity;
using System.Linq;
using EntityFramework.Extensions;
using EntityFramework.MappingAPI.Extensions;
using NUnit.Framework;
using SqlBulkCopy.Utilities.Test.Model;
using System.Configuration;

namespace SqlBulkCopy.Utilities.Test
{
    [TestFixture]
    public class SQLBulkUtilitiesTest
    {
        private readonly SqlBulkUtilities _sut = new SqlBulkUtilities(ConnectionString);
        private static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["ModelContext"].ConnectionString;
        private readonly ModelContext _dbContext = TestHelper.GetDbContext();

        [TestFixtureSetUp]
        public void SetUp()
        {
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
        }

        [TestCase(150)]
        [TestCase(1500)]
        [TestCase(15000)]
        public void When_GetSortedDataTable_is_called_all_items_are_included(int numberOfItems)
        {
            // Arrange
            var accessLogList = TestHelper.GenerateAccessLogList(numberOfItems);

            // Act
            var sortedDataTable = _sut.GetSortedDataTable(_dbContext, accessLogList);

            // Assert
            Assert.AreEqual(sortedDataTable.DataTable.Rows.Count, numberOfItems);
        }

        [TestCase(150)]
        [TestCase(1500)]
        [TestCase(15000)]
        [TestCase(150000)]
        public void When_WriteToServer_error_is_not_thrown_if_all_values_are_valid(int numberOfItems)
        {
            // Arrange
            var accessLogList = TestHelper.GenerateAccessLogList(numberOfItems);
            var tableName = (_dbContext as DbContext).Db<AccessLog>().TableName;
            var sortedDataTable = _sut.GetSortedDataTable(tableName, accessLogList);
            DeleteExistingData();

            // Act


            // Assert
            Assert.DoesNotThrow(() => _sut.WriteToServer(sortedDataTable));
        }

        private void DeleteExistingData()
        {
            _dbContext.AccessLog.Delete();
            _dbContext.SaveChanges();
        }
    }
}
