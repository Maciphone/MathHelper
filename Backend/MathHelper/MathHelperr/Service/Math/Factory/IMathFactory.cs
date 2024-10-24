using MathHelperr.Utility;

namespace MathHelperr.Service.Factory;

public interface IMathFactory
{
    IMathExcercise getMathExcercise(MathTypeName operationType);
}