using MathHelperr.Utility;

namespace MathHelperr.Service.Factory;

public interface IMathFactory
{
    IMathExcercise GetMathExercise(MathTypeName operationType, int level);
}