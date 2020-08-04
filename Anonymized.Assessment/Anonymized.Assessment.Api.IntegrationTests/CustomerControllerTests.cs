using Anonymized.Assessment.Api.Models.Responses;
using FluentAssertions;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using Xunit.Fixture.Mvc.Extensions;

namespace Anonymized.Assessment.Api.IntegrationTests
{
    public class CustomerControllerTests : TestBase
    {
        private const string CustomerPath = "Customer";

        public CustomerControllerTests(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public Task When_attempting_to_get_customer_with_invalid_id() =>
            GivenMvcFixture()
                .WhenGettingById(CustomerPath, int.MaxValue)
                .ShouldReturnNotFound()
                .RunAsync();

        [Fact]
        public Task When_getting_customer() =>
            GivenMvcFixture()
                .WhenGettingById(CustomerPath, 1)
                .ShouldReturnSuccessfulStatus()
                .ShouldReturnJson<CustomerModel>(c => 
                {
                    c.Accounts.Should().BeEmpty();
                    c.Balance.Should().Be(0);
                    c.Id.Should().Be("1");
                })
                .RunAsync();
    }
}