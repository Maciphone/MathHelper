using MathHelperr.Model;

namespace MathHelperr.Service;

public class DivisionTextGeneratorGeneral : IDivisionTextGenerator
{
    public int Level => 1;
    public string Answer(AlgebraResult example)
    {
        switch (example.Level)
        {
            case 1:
                return Level1(example);
            default:
                return Level1(example);
        }
    }
    
    public string Level1(AlgebraResult example)
    {
        int a = example.A;
        int b = example.B;
        return $"{a} / {b} = ?";
    }
}