using MathHelperr.Model;

namespace MathHelperr.Service;

public class Level1MultiplicationTextGenerator : IMultiplicationTextGenerator
{
    public string Answer(AlgebraResult example)
    {
        int a = example.A;
        int b = example.B;
        return $"{a} * {b} = ?";
    }
}