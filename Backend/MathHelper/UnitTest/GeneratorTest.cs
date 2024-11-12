using MathHelperr.Service;
using MathHelperr.Service.LevelProvider;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace UnitTest;

public class GeneratorTest
{
    [Test]
    public void AlgebraGenerators_ShouldUseLevel1GeneratorExample_WhenLevelIs1()
    {
        // Arrange
        var mockContextProvider = new Mock<IContextProvider>();
        mockContextProvider.Setup(cp => cp.GetLevel()).Returns("1");

        var services = new ServiceCollection();
        services.AddScoped<IAlgebraExampleGenerator, Level1AlgebraExampleGenerator>();
        services.AddScoped<IContextProvider>(provider => mockContextProvider.Object);

        var serviceProvider = services.BuildServiceProvider();

        // Act
        var generator = serviceProvider.GetRequiredService<IAlgebraExampleGenerator>();

        // Assert
        Assert.IsInstanceOf<Level1AlgebraExampleGenerator>(generator);
    }
    
    [Test]
    public void AlgebraGenerators_ShouldUseLevel1GeneratorText_WhenLevelIs1()
    {
        // Arrange
        var mockContextProvider = new Mock<IContextProvider>();
        mockContextProvider.Setup(cp => cp.GetLevel()).Returns("1");

        var services = new ServiceCollection();
        services.AddScoped<IAlgebraTextGenerator, Level1AlgebraTextGenerator>();
        services.AddScoped<IContextProvider>(provider => mockContextProvider.Object);

        var serviceProvider = services.BuildServiceProvider();

        // Act
        var generator = serviceProvider.GetRequiredService<IAlgebraTextGenerator>();

        // Assert
        Assert.IsInstanceOf<Level1AlgebraTextGenerator>(generator);
    }
    
    [Test]
    public void AlgebraGenerators_ShouldUseLevel1GeneratorExample_WhenLevelIs100_ReturnsDefault()
    {
        // Arrange
        var mockContextProvider = new Mock<IContextProvider>();
        mockContextProvider.Setup(cp => cp.GetLevel()).Returns("100");

        var services = new ServiceCollection();
        services.AddScoped<IAlgebraExampleGenerator, Level1AlgebraExampleGenerator>();
        services.AddScoped<IContextProvider>(provider => mockContextProvider.Object);

        var serviceProvider = services.BuildServiceProvider();

        // Act
        var generator = serviceProvider.GetRequiredService<IAlgebraExampleGenerator>();

        // Assert
        Assert.IsInstanceOf<Level1AlgebraExampleGenerator>(generator);
    }
    
    [Test]
    public void AlgebraGenerators_ShouldUseLevel1GeneratorText_WhenLevelIs100_ReturnsDefault()
    {
        // Arrange
        var mockContextProvider = new Mock<IContextProvider>();
        mockContextProvider.Setup(cp => cp.GetLevel()).Returns("100");

        var services = new ServiceCollection();
        services.AddScoped<IAlgebraTextGenerator, Level1AlgebraTextGenerator>();
        services.AddScoped<IContextProvider>(provider => mockContextProvider.Object);

        var serviceProvider = services.BuildServiceProvider();

        // Act
        var generator = serviceProvider.GetRequiredService<IAlgebraTextGenerator>();

        // Assert
        Assert.IsInstanceOf<Level1AlgebraTextGenerator>(generator);
    }

}