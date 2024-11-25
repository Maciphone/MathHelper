using MathHelperr.Model;

namespace MathHelperr.Service;

public class Level1AlgebraTextGenerator : IAlgebraTextGenerator
{
    public int Level => 1;
    public string Answer(AlgebraResult example)
    {
        int a = example.A;
        int b = example.B;
        return $"{a} + {b} = ?";
    }
}