namespace MathHelperr.Service.Math.Factory.GeneratorFactory.AbstractText;

[Obsolete("new factroy method implemented, but have worked on it, so I did not delete it")]

public  class GenericTextGenerator<T> where T: IMathTextGenerator
{
    private readonly Dictionary<int, T> _generators;


    public GenericTextGenerator(Dictionary<int, T> generators)
    {
        _generators = generators;
    }

    public T GetGenerator(int level)
    {
        if (_generators.TryGetValue(level, out var generator))
        {
            return generator;
        }
        throw new InvalidOperationException($"No generator found for level {level}");
    }
}