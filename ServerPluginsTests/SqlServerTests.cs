using ServerPlugins.SqlServer;
using System;
using ServerInterfaces;
using Xunit;
using ServerCore.Logger;

namespace ServerPluginsTests
{
    public class SqlServerTests
    {
        [Fact]
        public void SqlServerResponseGenerator_IsInterestedMethod_ReturnsCorrectValue_ForExplicitRoutes()
        {
            var connectionString = @"Server=SKL-TOSHO-TODOR\SQLEXPRESS;Database=Books;Trusted_Connection=True;";
            var sqlResponseGen = new SqlServerResponseGenerator("Books", connectionString);

            var requestPathWithoutSlash = new Request() {Path = "sql/books" };
            var requestPathWithSlash = new Request() {Path = "sql/books/"};
            var requestPathWithAdditionalRoutes = new Request() {Path = "sql/books/tables"};
            var requestPathWithWrongAddition = new Request() {Path = "sql/books-should-not-work"};

            Assert.True(sqlResponseGen.IsInterested(requestPathWithoutSlash, null));
            Assert.True(sqlResponseGen.IsInterested(requestPathWithSlash, null));
            Assert.True(sqlResponseGen.IsInterested(requestPathWithAdditionalRoutes, null));
            Assert.False(sqlResponseGen.IsInterested(requestPathWithWrongAddition, null));
        }

        [Fact]
        public void SqlServerResponseGenerator_IfServerIsNotResponding_ReturnInternalServerError()
        {
            var connectionString = @"Server=SKL-TOSHO-TODOR\SQLEXPRESS;Database=Books232323;Trusted_Connection=True;";
            var sqlResponseGen = new SqlServerResponseGenerator("Books", connectionString);

            var request = new Request() {Path = "sql/books"};
            Response response = sqlResponseGen.Generate(request, null).Result;

            Assert.Equal(ResponseCode.InternalServerError, response.ResponseCode);
        }
    }
}
