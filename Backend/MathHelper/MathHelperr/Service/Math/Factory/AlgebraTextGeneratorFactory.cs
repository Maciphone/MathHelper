namespace MathHelperr.Service.Math.Factory;

[Obsolete("structure changed, this factory is not in use. Was a manually way to solve a problem, " +
          "shows an old picture of the code, and how it started")]
public class AlgebraTextGeneratorFactory
{
    public IAlgebraTextGenerator GetTextGenerator(int level)
    {
        return level switch
        {
            // 1 => new Level1AlgebraTextGenerator(),
            // 2 => new Level2AlgebraTextGenerator(),
            // 3 => new Level3AlgebraTextGenerator(),
            // _ => new Level1AlgebraTextGenerator()
        };
    }
}