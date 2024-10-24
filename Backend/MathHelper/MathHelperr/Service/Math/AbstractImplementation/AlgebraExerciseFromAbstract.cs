namespace MathHelperr.Service.AbstractImplementation;

public class AlgebraExerciseFromAbstract : AbstractMathExercise<IAlgebraExampleGenerator, IAlgebraTextGenerator>, IAlgebraExcercise
{
    public AlgebraExerciseFromAbstract(IAlgebraExampleGenerator exampleGenerator, IAlgebraTextGenerator textGenerator) : base(exampleGenerator, textGenerator)
    {
    }
}