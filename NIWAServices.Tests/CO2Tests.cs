using NIWAServices.Models;
using NUnit.Framework;

namespace NIWAServices.Tests;

[TestFixture]
public class CO2Tests
{
    [Test]
    public void TestThatBlankKeyThrowsException()
    {
        Assert.Throws<ArgumentException>(() => new CO2Service(string.Empty));
    }

    [Test]
    public async Task TestThatInvalidKeyThrowUnauthorizedAccessException()
    {
        CO2Service service = new CO2Service("123");
        Assert.ThrowsAsync<UnauthorizedAccessException>(async () => { await service.Get(); });
    }

    [Test]
    public async Task TestThatValidKeyReturnsData()
    {
        CO2Service service = new CO2Service("YOURKEY");
        CO2Model value = await service.Get();

        Assert.NotNull(value);
        Assert.NotNull(value.Date);
        Assert.NotNull(value.LatestDailyAverage);
        Assert.NotNull(value.OneDecadeAgo);
        Assert.NotNull(value.OneYearAgo);
    }
}