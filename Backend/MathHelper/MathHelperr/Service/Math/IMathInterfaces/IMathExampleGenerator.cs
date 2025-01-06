using MathHelperr.Model;

namespace MathHelperr.Service;

public interface IMathExampleGenerator :IMathFactoryMarking
{
    int Level { get; }
    AlgebraResult Example();

}