namespace MathHelperr.Service.Factory;

public interface IMathFactory
{
    IMathExcercise getMathExcercise(string operationType);
}