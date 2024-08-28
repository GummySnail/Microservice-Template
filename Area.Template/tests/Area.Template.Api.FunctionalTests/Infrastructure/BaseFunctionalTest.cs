namespace Area.Template.Api.FunctionalTests.Infrastructure;

public abstract class BaseFunctionalTest(FunctionalTestWebAppFactory factory)
    : IClassFixture<FunctionalTestWebAppFactory>, IDisposable
{
    protected readonly HttpClient HttpClient = factory.CreateClient();

    public void Dispose()
    {
        HttpClient.Dispose();
    }
}
