using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using Shouldly;
using Xunit;

namespace Practia.AppDemo.Tests.General
{
    // ReSharper disable once InconsistentNaming
    public class ConnectionString_Tests
    {
        [Fact]
        public void SqlConnectionStringBuilder_Test()
        {
            var csb = new SqlConnectionStringBuilder("Server=localhost; Database=AppDemo; Trusted_Connection=True;");
            csb["Database"].ShouldBe("AppDemo");
        }
    }
}
