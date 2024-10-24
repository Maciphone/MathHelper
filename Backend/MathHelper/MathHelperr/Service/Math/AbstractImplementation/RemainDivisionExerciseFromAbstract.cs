using MathHelperr.Model;

namespace MathHelperr.Service.AbstractImplementation;

public class RemainDivisionExerciseFromAbstract : AbstractMathExercise<IRemainDivisonExampleGenerator,IRemainDivisionTextGenerator>, IRemainDivisionExcercise
{
    public RemainDivisionExerciseFromAbstract(IRemainDivisonExampleGenerator exampleGenerator, IRemainDivisionTextGenerator textGenerator) : base(exampleGenerator, textGenerator)
    {
    }
}