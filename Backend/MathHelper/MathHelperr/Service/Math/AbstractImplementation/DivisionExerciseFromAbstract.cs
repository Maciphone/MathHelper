namespace MathHelperr.Service.AbstractImplementation;

public class DivisionExerciseFromAbstract : AbstractMathExercise<IDivisionExampleGenerator, IDivisionTextGenerator>, IDivisionExcercise
{
    public DivisionExerciseFromAbstract(IDivisionExampleGenerator exampleGenerator, IDivisionTextGenerator textGenerator) : base(exampleGenerator, textGenerator)
    {
    }
}