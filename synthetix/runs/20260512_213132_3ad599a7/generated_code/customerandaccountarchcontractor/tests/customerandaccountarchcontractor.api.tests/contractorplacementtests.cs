using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace CustomerAndAccountArchcontractor.Api.Tests;

public class ContractorPlacementTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public ContractorPlacementTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task GetContractorPlacementEntity_ReturnsSuccess()
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync("/contractorplacement/contractorplacemententitycontroller");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Contains("success", await response.Content.ReadAsStringAsync());
    }
}