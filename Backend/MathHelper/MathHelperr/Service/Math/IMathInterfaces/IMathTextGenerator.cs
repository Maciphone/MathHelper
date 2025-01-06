using MathHelperr.Model;

namespace MathHelperr.Service;

public interface IMathTextGenerator : IMathFactoryMarking
{
    int Level { get; }
    string Answer(AlgebraResult example);
}