namespace MathHelperr.Service.Math.Factory;

public class AlgebraTextGeneratorFactory
{
    public IAlgebraTextGenerator GetTextGenerator(int level)
    {
        return level switch
        {
            1 => new Level1AlgebraTextGenerator(),
            2 => new Level2AlgebraTextGenerator(),
            3 => new Level3AlgebraTextGenerator(),
            _ => new Level1AlgebraTextGenerator()
        };
    }
}