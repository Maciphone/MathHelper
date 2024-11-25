using MathHelperr.Model;

namespace MathHelperr.Service;

public class Level3MultiplicationTextGenerator : IMultiplicationTextGenerator
{
    public int Level => 3;
    public string Answer(AlgebraResult example)
    {
        int a = example.A;
        int b = example.B;
        return $"{a} * {b} = ?";
    }
}