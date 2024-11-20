using MathHelperr.Service;
using MathHelperr.Service.AbstractImplementation;
using MathHelperr.Service.Factory;
using MathHelperr.Service.Math.Factory;
using MathHelperr.Utility;
using Moq;

namespace UnitTest;

public class Tests
{
    /*[Test]
    public void GetMathExcercise_ShouldReturnAlgebraExercise_WhenMathTypeIsAlgebra()
    {
        // Arrange
        var mockServiceProvider = new Mock<IServiceProvider>();
        var mockAlgebraExampleGenerator = new Mock<IAlgebraExampleGenerator>();
        var mockAlgebraTextGenerator = new Mock<IAlgebraTextGenerator>();

        mockServiceProvider
            .Setup(sp => sp.GetService(typeof(IAlgebraExampleGenerator)))
            .Returns(mockAlgebraExampleGenerator.Object);

        mockServiceProvider
            .Setup(sp => sp.GetService(typeof(IAlgebraTextGenerator)))
            .Returns(mockAlgebraTextGenerator.Object);

        var factory = new MathFactory(
            mockAlgebraExampleGenerator.Object, 
            mockAlgebraTextGenerator.Object, 
            null, null, 
            mockServiceProvider.Object, 
            null, null,
            null, null);

        // Act
        var result = factory.GetMathExercise(MathTypeName.Algebra);

        // Assert
        Assert.IsInstanceOf<AlgebraExerciseFromAbstract>(result);
    }

    [Test]
    public void GetMathExercise_ShouldReturnMultiplicationExercise_WhenMathTypeIsMultiplication()
    {
        // Arrange
        var mockMultiplicationExampleGenerator = new Mock<IMultiplicationExampleGenerator>();
        var mockMultiplicationTextGenerator = new Mock<IMultiplicationTextGenerator>();

        var factory = new MathFactory(
            null, null,
            mockMultiplicationExampleGenerator.Object,
            mockMultiplicationTextGenerator.Object,
            null, null, null,
            null, null);

        // Act
        var result = factory.GetMathExercise(MathTypeName.Multiplication);

        // Assert
        Assert.IsInstanceOf<MultiplicationExerciseFromAbstract>(result);
    }
    
    
    [Test]
    public void GetMathExercise_ShouldReturnDivisionExercise_WhenMathTypeIsDivision()
    {
        // Arrange
        var mockDivisionExampleGenerator = new Mock<IDivisionExampleGenerator>();
        var mockDivisionTextGenerator = new Mock<IDivisionTextGenerator>();

        var factory = new MathFactory(null, null,
            null, null,
            null,
            null, null,
            mockDivisionTextGenerator.Object, mockDivisionExampleGenerator.Object);

        // Act
        var result = factory.GetMathExercise(MathTypeName.Division);

        // Assert
        Assert.IsInstanceOf<DivisionExerciseFromAbstract>(result);
    }
    
    [Test]
    public void GetMathExercise_ShouldReturnRemainDivisionExercise_WhenMathTypeIsRemainDivision()
    {
        // Arrange
        var mockRemainDivisionExampleGenerator = new Mock<IRemainDivisonExampleGenerator>();
        var mockRemainDivisionTextGenerator = new Mock<IRemainDivisionTextGenerator>();

        var factory = new MathFactory(null, null,
            null, null,
            null,
            mockRemainDivisionTextGenerator.Object, mockRemainDivisionExampleGenerator.Object,
            null, null);

        // Act
        var result = factory.GetMathExercise(MathTypeName.RemainDivision);

        // Assert
        Assert.IsInstanceOf<RemainDivisionExerciseFromAbstract>(result);
    }

    
    [Test]
    public void GetMathExercise_ShouldThrowNotImplementedException_WhenMathTypeIsUnsupported()
    {
        // Arrange
        var mockServiceProvider = new Mock<IServiceProvider>();

        var factory = new MathFactory(
            null, null, null, null, 
            mockServiceProvider.Object, null, null, null, null);

        // Act & Assert
        Assert.Throws<NotImplementedException>(() =>
        {
            factory.GetMathExercise((MathTypeName)999); //not implemented mathtype
        });
    }

*/
}