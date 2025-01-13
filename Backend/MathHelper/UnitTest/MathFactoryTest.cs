using MathHelperr.Service;
using MathHelperr.Service.AbstractImplementation;
using MathHelperr.Service.Factory;
using MathHelperr.Service.Math.Factory;
using MathHelperr.Utility;
using NUnit.Framework;

using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace UnitTest;

public class MathFactoryTest
{
    [Test]
    public void GetMathExcercise_ShouldReturnAlgebraExercise_WhenMathTypeIsAlgebra()
    {
        // Arrange
        
        var mockAlgebraExampleGenerator = new Mock<IAlgebraExampleGenerator>();
        var mockAlgebraTextGenerator = new Mock<IAlgebraTextGenerator>();
        var mockMathgeneratorFactory = new Mock<IMathGeneratorFactory>();

       
        mockMathgeneratorFactory
            .Setup(factory => factory.GetGenerator<IAlgebraExampleGenerator>( It.IsAny<int>()))
            .Returns(mockAlgebraExampleGenerator.Object);

      

        var factory = new MathFactoryMarkingWithFactory(
            mockMathgeneratorFactory.Object, mockAlgebraTextGenerator.Object,
            null, null, null);

        // Act
        var result = factory.GetMathExercise(MathTypeName.Algebra, 1);

        // Assert
       // Assert.IsInstanceOf<AlgebraExerciseFromAbstract>(result);
        Assert.That(result, Is.InstanceOf<AlgebraExerciseFromAbstract>());
    }

    [Test]
    public void GetMathExercise_ShouldReturnMultiplicationExercise_WhenMathTypeIsMultiplication()
    {
        // Arrange
       var mockMultiplicationExampleGenerator = new Mock<IMultiplicationExampleGenerator>();
        var mockMultiplicationTextGenerator = new Mock<IMultiplicationTextGenerator>();
        var mockMathgeneratorFactory = new Mock<IMathGeneratorFactory>();

       
        mockMathgeneratorFactory
            .Setup(factory => factory.GetGenerator<IMultiplicationExampleGenerator>( It.IsAny<int>()))
            .Returns(mockMultiplicationExampleGenerator.Object);

      

        var factory = new MathFactoryMarkingWithFactory(
            mockMathgeneratorFactory.Object, null,
            null, mockMultiplicationTextGenerator.Object, null);

        // Act
        var result = factory.GetMathExercise(MathTypeName.Multiplication, 1);

        // Assert
        //Assert.IsInstanceOf<MultiplicationExerciseFromAbstract>(result);
        Assert.That(result, Is.InstanceOf<MultiplicationExerciseFromAbstract>());
    }
    
    
    [Test]
    public void GetMathExercise_ShouldReturnDivisionExercise_WhenMathTypeIsDivision()
    {
        var mockExampleGenerator = new Mock<IDivisionExampleGenerator>();
        var mockTextGenerator = new Mock<IDivisionTextGenerator>();
        var mockMathgeneratorFactory = new Mock<IMathGeneratorFactory>();

       
        mockMathgeneratorFactory
            .Setup(factory => factory.GetGenerator<IDivisionExampleGenerator>( It.IsAny<int>()))
            .Returns(mockExampleGenerator.Object);

      

        var factory = new MathFactoryMarkingWithFactory(
            mockMathgeneratorFactory.Object, null,
            mockTextGenerator.Object, null,null);

        
        
        // Act
        var result = factory.GetMathExercise(MathTypeName.Division, 1);

        // Assert
        //Assert.IsInstanceOf<DivisionExerciseFromAbstract>(result);
        Assert.That(result, Is.InstanceOf<DivisionExerciseFromAbstract>());
    }
    
    [Test]
    public void GetMathExercise_ShouldReturnRemainDivisionExercise_WhenMathTypeIsRemainDivision()
    {
        // Arrange
        var mockExampleGenerator = new Mock<IRemainDivisonExampleGenerator>();
        var mockTextGenerator = new Mock<IRemainDivisionTextGenerator>();
        var mockMathgeneratorFactory = new Mock<IMathGeneratorFactory>();

       
        mockMathgeneratorFactory
            .Setup(factory => factory.GetGenerator<IRemainDivisonExampleGenerator>( It.IsAny<int>()))
            .Returns(mockExampleGenerator.Object);

      

        var factory = new MathFactoryMarkingWithFactory(
            mockMathgeneratorFactory.Object, null,
            null, null,mockTextGenerator.Object);
        
        // Act
        var result = factory.GetMathExercise(MathTypeName.RemainDivision, 1);

        // Assert
        //Assert.IsInstanceOf<RemainDivisionExerciseFromAbstract>(result);
        Assert.That(result, Is.InstanceOf<RemainDivisionExerciseFromAbstract>());
    }

    
    [Test]
    public void GetMathExercise_ShouldThrowArgumentOutOfRangeException_WhenMathTypeIsUnsupported()
    {
        // Arrange
        var mockServiceProvider = new Mock<IServiceProvider>();

        var factory = new MathFactoryMarkingWithFactory(
            null, null,
            null, null,null);

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            factory.GetMathExercise((MathTypeName)999, 1); //not implemented mathtype
        });
    }

    
}