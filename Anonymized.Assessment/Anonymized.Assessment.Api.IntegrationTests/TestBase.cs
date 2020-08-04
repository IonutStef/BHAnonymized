using Xunit.Abstractions;
using Xunit.Fixture.Mvc;
using Xunit.Fixture.Mvc.Extensions;

namespace Anonymized.Assessment.Api.IntegrationTests
{
    public class TestBase
    {
        private readonly ITestOutputHelper _output;
        private readonly string _pathBase;

        public TestBase(ITestOutputHelper output, string pathBase = "/api")
        {
            _output = output;
            _pathBase = pathBase;
        }

        protected IMvcFunctionalTestFixture GivenMvcFixture() =>
            new MvcFunctionalTestFixture<Startup>(_output)
               .HavingPathBase(_pathBase);
    }
}