using MathHelperr.Model;

namespace MathHelperr.Service.AbstractImplementation;

public class MultiplicationExerciseFromAbstract : AbstractMathExercise<IMultiplicationExampleGenerator,IMultiplicationTextGenerator>, IMultiplicationExcercise
{
    public MultiplicationExerciseFromAbstract(IMultiplicationExampleGenerator exampleGenerator, IMultiplicationTextGenerator textGenerator) : base(exampleGenerator, textGenerator)
    {
    }
}